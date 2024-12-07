﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Washin.App.Services;
using System.Reflection.PortableExecutable;

public class ManagerViewModel
{
    private readonly BuildingApiService _buildingApiService;
    private readonly MachineApiService _machineApiService;
    private readonly UserApiService _userApiService;
    public ObservableCollection<Resident> Residents { get; set; } = new ObservableCollection<Resident>();
    public ObservableCollection<Resident> Queue { get; set; } = new ObservableCollection<Resident>();

    public int AvailableMachines { get; set; }

    public RelayCommandUser<Resident> GiveKeyCommand { get; private set; }
    public RelayCommandUser<Resident> KickOutCommand { get; private set; }

    private Resident _selecteResident;
    public Resident SelectedResident
    {
        get => _selecteResident;
        set
        {
            _selecteResident = value;
            OnPropertyChanged(nameof(SelectedResident));
            // Notify that ChangeMachineStateCommand might need reevaluation
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public ManagerViewModel()
    {

    _buildingApiService = new BuildingApiService();
        _userApiService = new UserApiService();
        _machineApiService = new MachineApiService();

        // Initialize commands
        GiveKeyCommand = new RelayCommandUser<Resident>(
            async _selecteResident => await GiveKey(_selecteResident),
            _selecteResident => _selecteResident != null // Enable the command only if a resident is selected
        );

        KickOutCommand = new RelayCommandUser<Resident>(
            async _selecteResident => await KickOut(_selecteResident),
            _selecteResident => _selecteResident != null // Enable the command only if a resident is selected
        );
    }

    private async Task GiveKey(Resident resident)
    {
        if (resident == null) { return; }
        try
        {
            // Call API to add the resident to the queue
            await _userApiService.RemoveFromQueueAsync(resident.Id);
            Residents.Remove(resident); // Remove the resident from the UI list

            // Optional: Update the resident list or machine state
            MessageBox.Show($"Key successfully given to {resident.Login}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error while giving the key: {ex.Message}");
        }
    }

    private async Task KickOut(Resident resident)
    {
        if (resident == null) { return; }
        try
        {
            // Call API to remove the resident from the queue
            await _userApiService.RemoveFromQueueAsync(resident.Id);

            // Optional: Update the resident list or machine state
            Residents.Remove(resident); // Remove the resident from the UI list
            MessageBox.Show($"Resident {resident.Login} has been kicked out.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error while kicking out: {ex.Message}");
        }
    }

    public async Task InitializeAsync()
    {
        await LoadBuildingDataAsync();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    private async Task LoadBuildingDataAsync()
    {
        try
        {
            int buildingId = 1;

            // Fetch data from the API
            var apiResidents = await _userApiService.GetResidentsAsync(buildingId);
            Residents.Clear();
            foreach (var apiResident in apiResidents)
            {
                var resident = new Resident();
                resident.Login = apiResident.Login;
                resident.Password = apiResident.Password;
                resident.Surname = apiResident.Surname;
                resident.Name = apiResident.Name;
                resident.IsManager = apiResident.IsManager;
                resident.Id_Building = apiResident.Id_Building;

                Console.WriteLine(resident);
                Residents.Add(resident);
            }

            var apiQueue = await _buildingApiService.GetQueueAsync(buildingId);
            Queue.Clear();
            foreach (var apiResident in apiQueue)
            {
                var resident = new Resident();
                resident.Login = apiResident.Login;
                resident.Password = apiResident.Password;
                resident.Surname = apiResident.Surname;
                resident.Name = apiResident.Name;
                resident.IsManager = apiResident.IsManager;
                resident.Id_Building = apiResident.Id_Building;

                Console.WriteLine(resident);
                Queue.Add(resident);
            }

            // Fetch available machines for the building
            //var machines = await _machineApiService.GetMachineAsync(buildingId);

        }
        catch (Exception e)
        {
            // Handle any exceptions gracefully
            Console.WriteLine("hello");
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
//    public Resident(int id, string login, string password, string surname, string name, bool isManager, int idBuilding)
//    {
//        this.id = id;
//        this.login = login;
//        this.password = password;
//        this.surname = surname;
//        this.name = name;
//        this.isManager = isManager;
//        this.id_Building = idBuilding;
//    }
}
