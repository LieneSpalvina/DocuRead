using System.Collections.ObjectModel;
using System.ComponentModel;
using DocumentReaderApp.Models;
using DocumentReaderApp.Services;
using System.Windows;
using System.Windows.Input;
using DocumentReaderApp.Commands;
using DocumentReaderApp.Views;
using System.Linq;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows.Input;
using System.IO;

namespace DocumentReaderApp.ViewModels
{
    public class DocumentsViewModel : INotifyPropertyChanged
    {
        private readonly DocumentService _documentService;
        private readonly AuthService _authService;

        private ObservableCollection<Document> _documents;

        public int TotalDocuments => _allDocuments.Count;

        public string MostRecentScan =>
            _allDocuments.Any() ? _allDocuments.Max(d => d.ExtractionDate).ToString("g") : "N/A";

        public Dictionary<string, int> LanguageCounts =>
            _allDocuments
                .GroupBy(d => d.Language)
                .ToDictionary(g => g.Key, g => g.Count());

        public ObservableCollection<Document> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged(nameof(Documents));
            }
        }

        private void LoadDocuments()
        {
            var docs = _documentService.GetAllDocuments();
            Documents = new ObservableCollection<Document>(docs);

            _allDocuments = _documentService.GetAllDocuments();
            AvailableLanguages = new ObservableCollection<string>(_allDocuments
                .Select(d => d.Language)
                .Distinct()
                .OrderBy(l => l));
            OnPropertyChanged(nameof(AvailableLanguages));

            ApplyFilters();

            OnPropertyChanged(nameof(TotalDocuments));
            OnPropertyChanged(nameof(MostRecentScan));
            OnPropertyChanged(nameof(LanguageCounts));


        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand LogOutCommand { get; }

        public ICommand BackCommand { get; }

        public ICommand DeleteDocumentCommand { get; }

        public DocumentsViewModel(AuthService authService)
        {
            _authService = authService;
            LogOutCommand = new RelayCommand(Logout);
            BackCommand = new RelayCommand(Back);
            _documentService = new DocumentService();
            LoadDocuments();
            ClearFiltersCommand = new RelayCommand(ClearFilters);
            DeleteDocumentCommand = new RelayCommand<Document>(DeleteDocument);


        }

        private void Logout()
        {
            new MainWindow(_authService).Show();
            Application.Current.Windows[0]?.Close();
        }

        private void Back()
        {
            new HomeWindow(_authService).Show();
            Application.Current.Windows[0]?.Close();
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                ApplyFilters();
            }
        }
        private string _searchText;

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
                ApplyFilters();
            }
        }
        private string _selectedLanguage;

        public ObservableCollection<string> AvailableLanguages { get; set; }

        public ICommand ClearFiltersCommand { get; }

        private List<Document> _allDocuments;

        private void ApplyFilters()
        {
            var filtered = _allDocuments.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(d =>
                    d.FileName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    d.Language.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(SelectedLanguage))
            {
                filtered = filtered.Where(d => d.Language == SelectedLanguage);
            }

            Documents = new ObservableCollection<Document>(filtered);
        }

        private void ClearFilters()
        {
            SearchText = string.Empty;
            SelectedLanguage = null;
        }

        public void AddNewDocument(string filePath, string language)
        {
            // 1. Copy uploaded file to local folder
            string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScannedDocuments");
            Directory.CreateDirectory(inputFolder);

            string savedFileName = Path.GetFileName(filePath);
            string savedFilePath = Path.Combine(inputFolder, savedFileName);
            File.Copy(filePath, savedFilePath, overwrite: true);

            var document = new Document
            {
                FileName = savedFileName,
                FilePath = savedFilePath,
                Language = language,
                ExtractionDate = DateTime.Now
            };

            var ocrService = new OCRService();

            // 2. Extract text and assign to Document
            document.ExtractedText = ocrService.ExtractText(savedFilePath, language);

            // 3. Save extracted text to a .txt file
            string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtractedText");
            Directory.CreateDirectory(outputFolder);

            string outputTextPath = Path.Combine(outputFolder, $"output_{savedFileName}.txt");
            File.WriteAllText(outputTextPath, document.ExtractedText);

            // 4. Save document record to DB
            _documentService.AddDocument(document);

            // 5. Retrieve it (to get the ID)
            var savedDoc = _documentService.GetAllDocuments()
                .LastOrDefault(d => d.FilePath == savedFilePath);

            if (savedDoc != null)
            {
                // 6. Extract detailed OCR results and save to JSON
                var results = ocrService.ExtractDetailedResults(savedFilePath, language, savedDoc.Id);
                ocrService.SaveResultsToJson(results, savedDoc.Id);

                // 7. Refresh UI
                LoadDocuments();
            }
        }

        public void DeleteDocument(Document doc)
        {
            if (doc == null) return;

            // 1. Delete files
            try
            {
                if (File.Exists(doc.FilePath))
                    File.Delete(doc.FilePath);

                string textPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtractedText", $"output_{doc.FileName}.txt");
                if (File.Exists(textPath))
                    File.Delete(textPath);

                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OcrData", $"ocr_{doc.Id}.json");
                if (File.Exists(jsonPath))
                    File.Delete(jsonPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete some files: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // 2. Delete from DB
            _documentService.DeleteDocument(doc.Id);

            // 3. Refresh UI
            LoadDocuments();
        }


    }
}
