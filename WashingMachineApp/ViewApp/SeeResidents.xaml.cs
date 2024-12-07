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
    /// Interaction logic for SeeResidents.xaml
    /// </summary>
    public partial class SeeResidents : UserControl
    {
        /// <summary>
        /// A reference to the parent Manager window that contains this control.
        /// This allows navigation back to the Manager interface.
        /// </summary>
        private readonly Manager _managerWindow;
        public SeeResidents(Manager manager)
        {
            InitializeComponent();
            _managerWindow = manager;
            DataContext = new CombinedViewModel(new ManagerViewModel(), App.LaundryViewModel);
        }

        /// <summary>
        /// Event handler for the "Return to Manager" button.
        /// Clears the content of the Manager's MainContent frame, effectively returning to the main Manager interface.
        /// </summary>
        /// <param name="sender">The source of the button click event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        private void ReturnToManager_Click(object sender, RoutedEventArgs e)
        {
            _managerWindow.MainContent.Content = null;
        }
    }
}
