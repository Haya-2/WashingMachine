using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

public class ManagerViewModel
{
    public ObservableCollection<Resident> Residents { get; set; }
    public int AvailableMachines { get; set; }

    public ICommand GiveKeyCommand { get; }
    public ICommand KickOutCommand { get; }

    public ManagerViewModel()
    {
        // Sample data
        Residents = new ObservableCollection<Resident>
        {
            new Resident { FullName = "John Doe" },
            new Resident { FullName = "Jane Smith" }
        };
        AvailableMachines = 3;

        GiveKeyCommand = new RelayCommand<Resident>(GiveKey);
        KickOutCommand = new RelayCommand<Resident>(KickOut);
    }

    private void GiveKey(Resident resident)
    {
        var result = MessageBox.Show($"Are you sure you want to give the key to {resident.FullName}?",
                                     "Confirmation", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // Handle giving the key
            MessageBox.Show($"Key given to {resident.FullName}.");
        }
    }

    private void KickOut(Resident resident)
    {
        var result = MessageBox.Show($"Are you sure you want to kick out {resident.FullName}?",
                                     "Confirmation", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // Handle kicking out
            Residents.Remove(resident);
            MessageBox.Show($"{resident.FullName} has been kicked out.");
        }
    }
}

public class Resident
{
    public string FullName { get; set; }
}
