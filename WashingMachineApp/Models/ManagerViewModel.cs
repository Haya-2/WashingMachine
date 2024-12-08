using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Washin.App.Services;
using System.Reflection.PortableExecutable;

/// <summary>
/// The ManagerViewModel class provides the logic and data binding for managing the state of residents, queues,
/// and washing machines in the laundry management system.
/// </summary>
/// <remarks>
/// This class interacts with API services to load data, update machine and resident states, and manage commands 
/// for key distribution and queue handling. It also uses the MVVM pattern to bind data to the manager interface.
/// </remarks>
/// </summary>
public class ManagerViewModel
{
    /// <summary>
    /// API service for building-related operations such as queue management.
    /// API service for machine-related operations such as updating machine states.
    /// API service for user-related operations such as fetching residents.
    /// </summary>
    private readonly BuildingApiService _buildingApiService;
    private readonly MachineApiService _machineApiService;
    private readonly UserApiService _userApiService;

    private int _nbPeopleInQueue;
    /// <summary>
    /// Observable collection of residents in the building.
    /// Observable collection of residents currently in the queue.
    /// Observable collection of all machines in the building.
    /// </summary>
    public ObservableCollection<Resident> Residents { get; set; } = new ObservableCollection<Resident>();
    public ObservableCollection<Resident> Queue { get; set; } = new ObservableCollection<Resident>(); 
    public ObservableCollection<Machine> Machines { get; set; }


    /// <summary>
    /// The count of available washing machines.
    /// </summary>
    private int _availableMachines;
    public int AvailableMachines
    {
        get => _availableMachines;
        set
        {
            if (_availableMachines != value)
            {
                _availableMachines = value;
                OnPropertyChanged(nameof(AvailableMachines));
            }
        }
    }

    /// <summary>
    /// The total number of people currently in the queue.
    /// </summary>
    /// 
    public int PeopleInQueue
    {
        get { return _nbPeopleInQueue; }
        set
        {
            _nbPeopleInQueue = value;
            OnPropertyChanged(nameof(PeopleInQueue));
        }
    }

    /// <summary>
    /// The currently selected resident.
    /// </summary>
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

    /// <summary>
    /// Command to assign a key to a resident for machine use.
    /// </summary>
    public RelayCommandUser<Resident> GiveKeyCommand { get; private set; }

    /// <summary>
    /// Command to remove a resident from the queue.
    /// </summary>
    public RelayCommandUser<Resident> KickOutCommand { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagerViewModel"/> class.
    /// Sets up the API services, initializes data collections, and configures commands.
    /// </summary>
    public ManagerViewModel()
    {

        _buildingApiService = new BuildingApiService();
        _userApiService = new UserApiService();
        _machineApiService = new MachineApiService();

        Machines = new ObservableCollection<Machine>();

        // Initialize commands
        GiveKeyCommand = new RelayCommandUser<Resident>(
            async _selecteResident => await GiveKey(_selecteResident),
            _selecteResident => _selecteResident != null // Enable the command only if a resident is selected
        );
        KickOutCommand = new RelayCommandUser<Resident>(
            async _selecteResident => await KickOut(_selecteResident),
            _selecteResident => _selecteResident != null 
        );
    }

    /// <summary>
    /// Assigns a washing machine to a selected resident.
    /// </summary>
    /// <param name="resident">The resident to assign the machine to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task GiveKey(Resident resident)
    {
        // If the resident is not found or is null.
        if (resident == null) { return; }
        try
        {
            int buildingId = 1; // For now
            if (AvailableMachines <= 0)
            {
                MessageBox.Show("No available machines at the moment. Please wait.");
                return;
            }
            // For each machine, if there is no user assign and the machine is working
            foreach (var machineToAssign in Machines)
            {
                if ((machineToAssign.UserId == null) && (machineToAssign.IsWorking == true))
                {
                    // Assign user to machine
                    machineToAssign.UserId = resident.Id;
                    // Call API to give the machine to a resident
                    await _machineApiService.UpdateMachineKeysAsync(machineToAssign.Id, resident.Id);
                    // Call API to remove the resident from the queue
                    await _buildingApiService.RemoveFromQueueAsync(buildingId, resident.Id);
                    Queue.Remove(resident); // Remove from Queue 
                    ReloadQueue();

                    // Decrease queue count
                    PeopleInQueue--;
                    // Decrease available machines count
                    AvailableMachines--;
                    break;
                }
                else
                {
                    Console.WriteLine(machineToAssign.Id); // debug / breakpoint
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error while giving the key: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes a resident from the queue.
    /// </summary>
    /// <param name="resident">The resident to remove from the queue.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task KickOut(Resident resident)
    {
        if (resident == null) { return; }
        try
        {
            int buildingId = 1; // For now
            // Call API to remove the resident from the queue
            await _buildingApiService.RemoveFromQueueAsync(buildingId, resident.Id);
            Queue.Remove(resident); // Remove from Queue
            ReloadQueue();
            // Decrease queue count
            PeopleInQueue--;
            //MessageBox.Show($"Resident {resident.Login} has been kicked out.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error while kicking out: {ex.Message}");
        }
    }

    /// <summary>
    /// Initializes the view model by loading building, resident, queue, and machine data from the APIs.
    /// </summary>
    /// <returns>A task representing the asynchronous initialization process.</returns>
    public async Task InitializeAsync()
    {
        await LoadBuildingDataAsync();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>
    /// Loads building data, including residents, queues, and machines, from the APIs.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task LoadBuildingDataAsync()
    {
        try
        {
            int buildingId = 1; // For now
            PeopleInQueue = 0; // var to 0.
            AvailableMachines = 0; // var to 0.

            // Fetch data from the API - All residents
            var apiResidents = await _userApiService.GetResidentsAsync(buildingId);
            Residents.Clear();
            foreach (var apiResident in apiResidents)
            {
                var resident = new Resident();
                resident.Id = apiResident.Id;
                resident.Login = apiResident.Login;
                resident.Password = apiResident.Password;
                resident.Surname = apiResident.Surname;
                resident.Name = apiResident.Name;
                resident.IsManager = apiResident.IsManager;
                resident.Id_Building = apiResident.Id_Building;

                //Console.WriteLine(resident);
                Residents.Add(resident);
            }

            // Fetch data from the API - Queue residents
            var apiQueue = await _buildingApiService.GetQueueAsync(buildingId);
            Queue.Clear();
            foreach (var apiResident in apiQueue)
            {
                var resident = new Resident();
                resident.Id = apiResident.Id;
                resident.Login = apiResident.Login;
                resident.Password = apiResident.Password;
                resident.Surname = apiResident.Surname;
                resident.Name = apiResident.Name;
                resident.IsManager = apiResident.IsManager;
                resident.Id_Building = apiResident.Id_Building;
                PeopleInQueue++; // var ++

                //Console.WriteLine(resident);
                Queue.Add(resident);
            }

            // Fetch data from the API - Machines
            var apiMachines = await _machineApiService.GetMachineAsync(buildingId);
            Machines.Clear();
            foreach (var apiMachine in apiMachines)
            {
                var machine = new Machine();
                machine.Id = apiMachine.Id;
                machine.IsWorking = apiMachine.IsWorking;
                machine.Id_Building = apiMachine.Id_Building;
                machine.UserId = apiMachine.UserId;

                //Console.WriteLine(machine);
                Machines.Add(machine);
                //Console.WriteLine($"Machine {machine.Id}: IsWorking updated to {machine.IsWorking}");
            }

            foreach (var machine in Machines)
            {
                if ((machine.UserId == null) && (machine.IsWorking == true)) AvailableMachines++; // var ++
            }

        }
        catch (Exception e)
        {
            // Handle any exceptions gracefully
            Console.WriteLine("hello");
        }
    }

    /// <summary>
    /// Reloads the queue by fetching the latest data from the API.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task ReloadQueue()
    {
        try
        {
            int buildingId = 1;
            var updatedQueue = await _buildingApiService.GetQueueAsync(buildingId);
            Queue.Clear();
            foreach (var resident in updatedQueue)
            {
                Queue.Add(resident);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error reloading queue: {ex.Message}");
        }
    }

}

/// <summary>
/// Represents a resident in the building.
/// </summary>
public class Resident
{
    public int Id { get; set; }   
    public string Login { get; set; }  
    public string Password { get; set; }  
    public string Surname { get; set; } 
    public string Name { get; set; }  
    public bool IsManager { get; set; }  
    public int Id_Building { get; set; } 
}
