using System.Windows;
using DocumentReaderApp.Services;
using DocumentReaderApp.ViewModels;
using Microsoft.Win32;
using DocumentReaderApp.Models;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;

namespace DocumentReaderApp.Views
{
    public partial class DocumentsWindow : Window
    {
        public DocumentsWindow(AuthService authService)
        {
            InitializeComponent();
            DataContext = new DocumentsViewModel(authService);
        }

        private void OpenOriginal_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Document doc && File.Exists(doc.FilePath))
                Process.Start("explorer", $"\"{doc.FilePath}\"");
            else
                MessageBox.Show("Original file not found.");
        }

        private void OpenText_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Document doc)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtractedText", $"output_{doc.FileName}.txt");
                if (File.Exists(path))
                    Process.Start("explorer", $"\"{path}\"");
                else
                    MessageBox.Show("Extracted text file not found.");
            }
        }

        private void OpenJson_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Document doc)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OcrData", $"ocr_{doc.Id}.json");
                if (File.Exists(path))
                    Process.Start("explorer", $"\"{path}\"");
                else
                    MessageBox.Show("OCR JSON file not found.");
            }
        }



    }

    

}
