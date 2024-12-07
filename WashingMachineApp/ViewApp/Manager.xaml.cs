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
    /// Represents the main manager interface for the Washing Machine application.
    /// This window allows managers to navigate through various functionalities, such as:
    /// - Viewing and managing key distribution.
    /// - Monitoring residents and their washing machine usage.
    /// - Checking the state of washing machines in the laundry room.
    /// 
    /// <remarks>
    /// This class sets the DataContext to a CombinedViewModel that aggregates
    /// <see cref="ManagerViewModel"/> and the application's global <see cref="App.LaundryViewModel"/>.
    /// This facilitates seamless data binding for the manager's tasks and machine state updates.
    /// </remarks>
    /// </summary>
    public partial class Manager : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// Sets up the DataContext and prepares the view for manager interactions.
        /// </summary>
        public Manager()
        {
            InitializeComponent();
            DataContext = new CombinedViewModel(new ManagerViewModel(), App.LaundryViewModel);
            //DataContext = App.LaundryViewModel;
        }

        /// <summary>
        /// Event handler for the Key Distribution button.
        /// Opens the Key Distribution view within the MainContent frame.
        /// </summary>
        /// <param name="sender">The source of the button click event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.KeyDistributionWindow(this);
        }

        /// <summary>
        /// Event handler for the Residents button.
        /// Opens the See Residents view within the MainContent frame.
        /// </summary>
        /// <param name="sender">The source of the button click event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        private void ResidentsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.SeeResidents(this);
        }

        /// <summary>
        /// Event handler for the Machine State button.
        /// Opens the Machines State view within the MainContent frame.
        /// </summary>
        /// <param name="sender">The source of the button click event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        private void MachineStateButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.MachinesState(this);
        }
    }
}