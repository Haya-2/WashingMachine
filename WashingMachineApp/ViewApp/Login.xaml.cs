﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WashingMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Home window
            Home homeWindow = new Home();
            homeWindow.Show();
            this.Close(); // Close the Login window
        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Manager window
            Manager managerWindow = new Manager();
            managerWindow.Show();
            this.Close(); // Close the Login window
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Set the password to the ViewModel
                var viewModel = DataContext as LoginViewModel;
                if (viewModel != null)
                {
                    viewModel.Password = passwordBox.Password;
                    //Console.WriteLine(viewModel.Password, passwordBox.Password, "TEST");
                }
            }
        }
    }
}