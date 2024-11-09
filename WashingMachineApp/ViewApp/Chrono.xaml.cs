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
    /// Interaction logic for Chrono.xaml
    /// </summary>
    public partial class Chrono : Window
    {
        public Chrono()
        {
            InitializeComponent();
            DataContext = App.LaundryViewModel;
        }
        private void ReturnToHome_Click(object sender, RoutedEventArgs e)
        {
            Home w = new Home();
            w.Show();
            this.Close();
        }
    }
}