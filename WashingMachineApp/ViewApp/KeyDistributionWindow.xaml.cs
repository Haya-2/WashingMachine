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
    /// Represents the link between washing machines and resident within the laundry system.
    /// This view allows managers to interact with the current status of the resident in the queue.
    /// They can give key to them, assignating a machine. They can also kick them out if they don't come.
    /// 
    /// <remarks>
    /// This class is part of the manager interface, and it is hosted within the main Manager window.
    /// It uses a combined view model (<see cref="CombinedViewModel"/>) that aggregates 
    /// <see cref="ManagerViewModel"/> and the global <see cref="App.LaundryViewModel"/>.
    /// </remarks>
    /// </summary>
    public partial class KeyDistributionWindow : UserControl
    {
        /// <summary>
        /// A reference to the parent Manager window that contains this control.
        /// This allows navigation back to the Manager interface.
        /// </summary>
        private readonly Manager _managerWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyDistributionWindow"/> class.
        /// Sets up the DataContext for data binding and stores a reference to the parent Manager window.
        /// </summary>
        /// <param name="manager">The parent Manager window that hosts this view.</param>
        public KeyDistributionWindow(Manager manager)
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
