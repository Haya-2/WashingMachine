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
    /// Interaction logic for KeyDistributionWindow.xaml
    /// </summary>
    public partial class KeyDistributionWindow : Window
    {
        public KeyDistributionWindow()
        {
            InitializeComponent();
            DataContext = new CombinedViewModel(new ManagerViewModel(), App.LaundryViewModel);
        }

        private void ReturnToManager_Click(object sender, RoutedEventArgs e)
        {
            Manager W = new Manager();
            W.Show();
            this.Close(); // Close the KeyDistributionWindow window
        }
    }
}
