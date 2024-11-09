using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {

        public Manager()
        {
            InitializeComponent();
            DataContext = App.LaundryViewModel;
        }
        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Key window
            KeyDistributionWindow keyWindow = new KeyDistributionWindow();
            keyWindow.Show();
            this.Close(); // Close the Manager window
        }
    }
}