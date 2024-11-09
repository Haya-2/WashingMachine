using System.ComponentModel;
namespace WashingMachine.ViewModels
{
    public class CombinedViewModel
    {
        public ManagerViewModel ManagerViewModel { get; }
        public LaundryViewModel LaundryViewModel { get; }

        public CombinedViewModel(ManagerViewModel managerViewModel, LaundryViewModel laundryViewModel)
        {
            ManagerViewModel = managerViewModel;
            LaundryViewModel = laundryViewModel;
            // Initialize asynchronously
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            await LaundryViewModel.InitializeAsync();
            await ManagerViewModel.InitializeAsync();

        }
    }
}
