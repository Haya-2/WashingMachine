using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using Washin.App.Services;
using System.Windows.Input;
using System.Windows;

/// <summary>
/// The LaundryViewModel class handles the business logic for managing laundry machines,
/// their states, and relevant statistics in the building. It fetches data from API services 
/// and provides functionalities for updating machine states and estimating waiting times.
/// </summary>
/// <remarks>
/// This class follows the MVVM pattern and uses the INotifyPropertyChanged interface 
/// to facilitate data binding with the UI. It supports asynchronous initialization 
/// and command-based machine state changes.
/// </remarks>
public class LaundryViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// API service for managing machine-related data.
    /// API service for fetching building-related data.
    /// </summary>
    private readonly MachineApiService _machineApiService;
    private readonly BuildingApiService _buildingApiService;
    private string _machineState;
    private int _nbMachine, _nbMachineWorking, _nbMachineNotW, _nbPeopleBeforeMeInQueue, _nbMachineAvailable;
    private double _waitingTimeEstimate;
    public ObservableCollection<Machine> Machines { get; set; } = new ObservableCollection<Machine>();

    private Machine _selectedMachine;
    public Machine SelectedMachine
    {
        get => _selectedMachine;
        set
        {
            _selectedMachine = value;
            OnPropertyChanged(nameof(SelectedMachine));
            CommandManager.InvalidateRequerySuggested();
        }
    }
    public string MachineState
    {
        get { return _machineState; }
        set
        {
            _machineState = value;
            OnPropertyChanged(nameof(MachineState));
        }
    }

    public int MachinesNb
    {
        get { return _nbMachine; }
        set
        {
            _nbMachine = value;
            OnPropertyChanged(nameof(MachinesNb));
        }
    }

    public int MachineWorking
    {
        get { return _nbMachineWorking; }
        set
        {
            _nbMachineWorking = value;
            OnPropertyChanged(nameof(MachineWorking));
        }
    }

    public int MachineNotWorking
    {
        get { return _nbMachineNotW; }
        set
        {
            _nbMachineNotW = value;
            OnPropertyChanged(nameof(MachineNotWorking));
        }
    }

    public int PeopleBeforeMeInQueue
    {
        get { return _nbPeopleBeforeMeInQueue; }
        set
        {
            _nbPeopleBeforeMeInQueue = value;
            OnPropertyChanged(nameof(PeopleBeforeMeInQueue));
        }
    }
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

    public double WaitingTimeEstimate
    {
        get { return _waitingTimeEstimate; }
        set
        {
            _waitingTimeEstimate = value;
            OnPropertyChanged(nameof(WaitingTimeEstimate));
        }
    }

    /* public int Machines
    {
        get { return _nbMachine; }
        set
        {
            _nbMachine = value;
            OnPropertyChanged(nameof(Machines));
        }
    }*/
    public ICommand ChangeMachineStateICommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LaundryViewModel"/> class.
    /// Sets up command bindings and subscribes to property change notifications.
    /// </summary>
    public LaundryViewModel()
    {
        _machineApiService = new MachineApiService();

        // Initialize commands
        var changeMachineStateCommand = new RelayCommand(
            () => ChangeMachineState(SelectedMachine),
            () => SelectedMachine != null
        );
        ChangeMachineStateICommand = changeMachineStateCommand;

        // Subscribe to property changes to update command state
        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(SelectedMachine))
            {
                changeMachineStateCommand.RaiseCanExecuteChanged();
            }
        };

    }

    /// <summary>
    /// Toggles the working state of a selected machine and updates the state via the API.
    /// </summary>
    /// <param name="machine">The machine whose state is to be changed.</param>
    /// <remarks>
    /// The method validates the input and ensures the API call succeeds, updating the machine's
    /// working state both locally and remotely.
    /// </remarks>
    private async void ChangeMachineState(Machine machine)
    {
        Console.WriteLine($"Machine {machine.Id}: IsWorking updated to {machine.IsWorking}");
        if (machine == null) return;

        try
        {
            machine.IsWorking = !machine.IsWorking;

            // Call the API to update the machine status
            await _machineApiService.UpdateMachineStatusAsync(machine.Id, machine.IsWorking);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing machine state: {ex.Message}");
        }
    }

    /// <summary>
    /// Asynchronously initializes the view model by loading building and machine data from the API.
    /// </summary>
    /// <remarks>
    /// This method fetches machine and queue data, calculates relevant statistics,
    /// and updates the UI state accordingly.
    /// </remarks>
    public async Task InitializeAsync()
    {
        await LoadBuildingDataAsync();
    }

    private async Task LoadBuildingDataAsync()
    {
        try
        {
            int buildingId = 1; // for now

            // Set a default machine state
            MachineState = (AvailableMachines > 0) ? "State: Washing machine available" : "State: No washing machines available"; // Placeholder
            // Estimate waiting time
            WaitingTimeEstimate = 20.0; // Placeholder

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

            // Assign var
            MachineWorking = 0; MachineNotWorking = 0;
            MachinesNb = 0;
            AvailableMachines = 0;
            foreach (var machine in Machines)
            {
                MachinesNb++;
                if (machine.IsWorking) MachineWorking++;
                if (machine.IsWorking == false ) MachineNotWorking++;
                if ((machine.UserId == null) && (machine.IsWorking)) AvailableMachines++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading: {e.Message}");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}

/// <summary>
/// Represents a washing machine entity with properties for its state, building ID, and user association.
/// </summary>
/// <remarks>
/// The Machine class implements the INotifyPropertyChanged interface for 
/// updating UI components in response to changes in its properties.
/// </remarks>
public class Machine
{
    public int Id { get; set; }
    private bool _isWorking;
    public int Id_Building { get; set; }

    /// Identifier of the user currently using the machine, if any.
    public int? UserId { get; set; }

    /// <summary>
    /// Indicates whether the machine is operational.
    /// </summary>
    public bool IsWorking
    {
        get => _isWorking;
        set
        {
            if (_isWorking != value)
            {
                _isWorking = value;
                OnPropertyChanged(nameof(IsWorking));
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
