using System.Text;
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
    /// Represents the login interface for the Washing Machine application.
    /// This window allows to connect to the app as :
    /// - a resident
    /// - a manager
    /// </summary>
    /// 
    /// <remarks>
    /// This class sets the DataContext to <see cref="LoginViewModel"/>
    /// This ViewModel was created especially for login.
    /// </remarks>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        /// <summary>
        /// DEBUG - Go to home without a login / password
        /// </summary>
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Home window
            Home homeWindow = new Home();

            homeWindow.Show();
            this.Close(); // Close the Login window
        }

        /// <summary>
        /// DEBUG - Go to manager window without a login / password
        /// </summary>
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