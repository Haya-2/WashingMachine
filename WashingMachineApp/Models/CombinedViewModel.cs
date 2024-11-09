using System.ComponentModel;

public class CombinedViewModel
{
    public ManagerViewModel ManagerViewModel { get; }
    public LaundryViewModel LaundryViewModel { get; }

    public CombinedViewModel(ManagerViewModel managerViewModel, LaundryViewModel laundryViewModel)
    {
        ManagerViewModel = managerViewModel;
        LaundryViewModel = laundryViewModel;
    }
}
