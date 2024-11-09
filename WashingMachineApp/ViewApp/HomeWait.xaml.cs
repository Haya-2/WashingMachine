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
using WashingMachine.ViewModels;

namespace WashingMachine
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomeWait : Window
    {
        public HomeWait()
        {
            InitializeComponent();
            DataContext = App.LaundryViewModel;
        }
        private void Button_Chrono(object sender, RoutedEventArgs e)
        {
            // Open the Key window
            Chrono w = new Chrono();
            w.Show();
            this.Close(); // Close the Manager window
        }

        private void Button_QuitQueue(object sender, RoutedEventArgs e)
        {
            // Open the Key window
            HomeWait w = new HomeWait();
            w.Show();
            this.Close();
        }
    }
}