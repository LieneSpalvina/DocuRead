using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using DocumentReaderApp.Services;
using DocumentReaderApp.Commands;
using DocumentReaderApp.Models;



namespace DocumentReaderApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly OCRService _ocrService;
        private string _selectedFilePath = string.Empty;
        private string _extractedText = string.Empty;
        private string _selectedLanguage = string.Empty;
        private readonly DocumentService _documentService;
        private readonly AuthService _authService;


        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set { _selectedFilePath = value; OnPropertyChanged(nameof(SelectedFilePath)); }
        }

        public string ExtractedText
        {
            get => _extractedText;
            set { _extractedText = value; OnPropertyChanged(nameof(ExtractedText)); }
        }

        public List<string> AvailableLanguages { get; } = new List<string> { "eng", "deu", "fra" };

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; OnPropertyChanged(nameof(SelectedLanguage)); }
        }

        public ICommand BrowseFileCommand { get; }
        public ICommand ExtractTextCommand { get; }
        public ICommand SaveTextCommand { get; }
        public ICommand SignOutCommand { get; }

        public HomeViewModel(AuthService authService)
        {
            _ocrService = new OCRService();
            _documentService = new DocumentService();
            _authService = authService;

            BrowseFileCommand = new RelayCommand(BrowseFile);
            ExtractTextCommand = new RelayCommand(ExtractText);
            SaveTextCommand = new RelayCommand(SaveText);
            SignOutCommand = new RelayCommand(SignOut);
        }

        private void BrowseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select an Image or PDF",
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|PDF Files (*.pdf)|*.pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
            }
        }

        private void ExtractText()
        {
            if (string.IsNullOrWhiteSpace(SelectedFilePath))
            {
                MessageBox.Show("Please select a valid file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(SelectedLanguage))
            {
                MessageBox.Show("Please select a valid language.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 1. Copy file to /ScannedDocuments/
            string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScannedDocuments");
            Directory.CreateDirectory(inputFolder);

            string savedFileName = Path.GetFileName(SelectedFilePath);
            string savedFilePath = Path.Combine(inputFolder, savedFileName);
            File.Copy(SelectedFilePath, savedFilePath, overwrite: true);

            // 2. Extract plain OCR text
            ExtractedText = _ocrService.ExtractText(savedFilePath, SelectedLanguage);

            // 3. Save extracted text to /ExtractedText/
            string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtractedText");
            Directory.CreateDirectory(outputFolder);
            string outputTextPath = Path.Combine(outputFolder, $"output_{savedFileName}.txt");
            File.WriteAllText(outputTextPath, ExtractedText);

            // 4. Create and save document to DB
            var document = new Document
            {
                FileName = savedFileName,
                FilePath = savedFilePath,
                ExtractedText = ExtractedText,
                Language = SelectedLanguage,
                ExtractionDate = DateTime.Now
            };

            _documentService.AddDocument(document);

            // 5. Reload saved document (to get its ID)
            var savedDoc = _documentService.GetAllDocuments()
                .LastOrDefault(d => d.FilePath == savedFilePath);

            if (savedDoc != null)
            {
                // 6. Extract detailed OCR and save as JSON
                var detailedResults = _ocrService.ExtractDetailedResults(savedFilePath, SelectedLanguage, savedDoc.Id);
                _ocrService.SaveResultsToJson(detailedResults, savedDoc.Id);
            }

            MessageBox.Show("Text extracted and files saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void SaveText()
        {
            if (string.IsNullOrWhiteSpace(ExtractedText))
            {
                MessageBox.Show("There is no text to save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text File (*.txt)|*.txt",
                FileName = "ExtractedText.txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, ExtractedText);
                MessageBox.Show("Text saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SignOut()
        {
            
            new MainWindow(_authService).Show();
            Application.Current.Windows[0]?.Close();
        }

        


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}