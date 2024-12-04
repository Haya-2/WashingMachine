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
            MainContent.Content = new Views.KeyDistributionWindow(this);
        }

        private void ResidentsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.SeeResidents(this);
        }
        
        private void MachineStateButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.MachinesState(this);
        }
    }
}