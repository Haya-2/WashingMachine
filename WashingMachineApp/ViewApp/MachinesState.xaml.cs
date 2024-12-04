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
    /// Interaction logic for MachinesState.xaml
    /// </summary>
    public partial class MachinesState : UserControl
    {
        private readonly Manager _managerWindow;
        public MachinesState(Manager manager)
        {
            InitializeComponent();
            _managerWindow = manager;
            DataContext = new CombinedViewModel(new ManagerViewModel(), App.LaundryViewModel);
        }

        private void ReturnToManager_Click(object sender, RoutedEventArgs e)
        {
            _managerWindow.MainContent.Content = null;
        }
    }
}
