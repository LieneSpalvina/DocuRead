using System;
using System.Windows;
using DocumentReaderApp.Services;
using DocumentReaderApp.Models;
using DocumentReaderApp.ViewModels;
using BCrypt.Net;
using DocumentReaderApp.Views;


namespace DocumentReaderApp
{
    public partial class SignUpWindow : Window
    {
        private readonly AuthService _authService;

        public SignUpWindow(AuthService authService) // Inject AuthService
        {
            InitializeComponent();
            _authService = authService;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(_authService).Show();
            this.Close();
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Hash password before saving
            User newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            bool isRegistered = await _authService.RegisterUserAsync(newUser);

            if (isRegistered)
            {
                MessageBox.Show("Account created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                new LoginWindow(_authService).Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or Email already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
