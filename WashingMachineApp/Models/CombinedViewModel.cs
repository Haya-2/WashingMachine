using System.ComponentModel;
namespace WashingMachine.ViewModels
{
    /// <summary>
    /// ViewModel that combines both Manager and Laundry ViewModels.
    /// This class facilitates interaction with both Manager and Laundry systems, allowing 
    /// for async initialization of both when the CombinedViewModel is instantiated.
    /// </summary>
    public class CombinedViewModel
    {
        public ManagerViewModel ManagerViewModel { get; }
        public LaundryViewModel LaundryViewModel { get; }

        /// <summary>
        /// Initializes a new instance of the CombinedViewModel, accepting
        /// the individual ManagerViewModel and LaundryViewModel instances as parameters.
        /// Calls InitializeAsync() to asynchronously initialize both ViewModels.
        /// </summary>
        /// <param name="managerViewModel">The ManagerViewModel instance to be associated with this CombinedViewModel.</param>
        /// <param name="laundryViewModel">The LaundryViewModel instance to be associated with this CombinedViewModel.</param>
        public CombinedViewModel(ManagerViewModel managerViewModel, LaundryViewModel laundryViewModel)
        {
            ManagerViewModel = managerViewModel;
            LaundryViewModel = laundryViewModel;
            // Initialize asynchronously
            InitializeAsync();
        }

        /// <summary>
        /// Asynchronously initializes the ManagerViewModel and LaundryViewModel.
        /// This method ensures that both ViewModels are fully initialized before 
        /// they are used in the CombinedViewModel context.
        /// </summary>
        private async void InitializeAsync()
        {
            await LaundryViewModel.InitializeAsync();
            await ManagerViewModel.InitializeAsync();

        }
    }
}
