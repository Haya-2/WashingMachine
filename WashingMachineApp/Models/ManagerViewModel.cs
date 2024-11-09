using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Washin.App.Services;


public class ManagerViewModel
{
    private readonly BuildingApiService _buildingApiService;
    private readonly MachineApiService _machineApiService;
    private readonly UserApiService _userApiService;
    public ObservableCollection<Resident> Residents { get; set; } = new ObservableCollection<Resident>();

    public int AvailableMachines { get; set; }

    public ICommand GiveKeyCommand { get; }
    public ICommand KickOutCommand { get; }

    public ManagerViewModel()
    {
        GiveKeyCommand = new RelayCommand<Resident>(GiveKey);
        KickOutCommand = new RelayCommand<Resident>(KickOut);

        _buildingApiService = new BuildingApiService();
        
    }

    private void GiveKey(Resident resident)
    {
        var result = MessageBox.Show($"Are you sure you want to give the key to {resident.Login}?",
                                     "Confirmation", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // Handle giving the key
            MessageBox.Show($"Key given to {resident.Login}.");
        }
    }

    private void KickOut(Resident resident)
    {
        var result = MessageBox.Show($"Are you sure you want to kick out {resident.Login}?",
                                     "Confirmation", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // Handle kicking out
            Residents.Remove(resident);
            MessageBox.Show($"{resident.Login} has been kicked out.");
        }
    }
    public async Task InitializeAsync()
    {
        await LoadBuildingDataAsync();
    }

    private async Task LoadBuildingDataAsync()
    {
        try
        {
            int buildingId = 1;

            // Fetch data from the API
            var apiResidents = await _buildingApiService.GetResidentsAsync(buildingId);
            Residents.Clear();
            foreach (var apiResident in apiResidents)
            {
                /*var resident = new Resident(
                    apiResident.Login,
                    apiResident.Password,
                    apiResident.Surname,
                    apiResident.Name,
                    apiResident.IsManager,
                    apiResident.Id_Building
                );*/
                Console.WriteLine(apiResident);

                //Residents.Add(resident);
            }

            // Fetch available machines for the building
            var machines = await _machineApiService.GetMachineAsync(buildingId);

        }
        catch (Exception e)
        {
            // Handle any exceptions gracefully
        }
    }
}
public class Resident
{
    public int Id { get; set; }   
    public string Login { get; set; }  
    public string Password { get; set; }  
    public string Surname { get; set; } 
    public string Name { get; set; }  
    public bool IsManager { get; set; }  
    public int Id_Building { get; set; } 

    // Constructor for convenience
    public Resident(string login, string password, string surname, string name, bool isManager, int idBuilding)
    {
        Login = login;
        Password = password;
        Surname = surname;
        Name = name;
        IsManager = isManager;
        Id_Building = idBuilding;
    }
}
