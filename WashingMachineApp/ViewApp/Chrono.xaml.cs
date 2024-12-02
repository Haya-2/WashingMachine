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

namespace WashingMachine.Views
{
    /// <summary>
    /// Interaction logic for Chrono.xaml
    /// </summary>
    public partial class Chrono : UserControl
    {
        private readonly Home _homeWindow;
        private readonly HomeWait _homeWaitControl;
        private readonly bool _isHomeWait;
        public Chrono(Home home, bool isHomeWait = false)
        {
            InitializeComponent();
            _homeWindow = home;
            _isHomeWait = isHomeWait;
        }

        public Chrono(HomeWait homeWait)
        {
            InitializeComponent();
            _homeWaitControl = homeWait;
            _isHomeWait = true;
        }

        private void ReturnToHome_Click(object sender, RoutedEventArgs e)
        {
            if (_isHomeWait && _homeWaitControl != null)
            {
                // Navigate back to the HomeWait UserControl
                _homeWaitControl._home.MainContentHome.Content = _homeWaitControl;
            }
            else if (_homeWindow != null)
            {
                // Navigate back to the Home window
                _homeWindow.MainContentHome.Content = null;
            }
        }
    }
}