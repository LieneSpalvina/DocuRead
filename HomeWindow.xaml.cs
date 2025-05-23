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
    public partial class HomeWindow : Window
    {
        private readonly AuthService _authService;

        public HomeWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            DataContext = new HomeViewModel(authService);
        }

        private void OpenDocuments_Click(object sender, RoutedEventArgs e)
        {
            var documentsWindow = new DocumentsWindow(_authService);
            documentsWindow.Show();
            Application.Current.Windows[0]?.Close();
        }
    }

}
