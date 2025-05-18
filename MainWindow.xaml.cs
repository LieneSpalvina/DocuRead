using System;
using System.Windows;
using DocumentReaderApp.Data;
using DocumentReaderApp.Services;
using DocumentReaderApp.Views;


namespace DocumentReaderApp
{
    public partial class MainWindow : Window
    {
        private readonly AuthService _authService;

        public MainWindow() : this(new AuthService(new DatabaseContext()))
        {
        }

        public MainWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            new SignUpWindow(_authService).Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow(_authService).Show();
            this.Close();
        }
    }
}
