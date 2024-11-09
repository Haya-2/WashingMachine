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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
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
    }
}