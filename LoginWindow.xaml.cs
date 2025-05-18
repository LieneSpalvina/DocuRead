using System.Windows;
using DocumentReaderApp.ViewModels;
using DocumentReaderApp.Models;
using DocumentReaderApp.Services;
using System.Windows.Controls;

namespace DocumentReaderApp.Views
{
    public partial class LoginWindow : Window
    {

        public LoginWindow(AuthService authService)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(authService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

    }

    
}