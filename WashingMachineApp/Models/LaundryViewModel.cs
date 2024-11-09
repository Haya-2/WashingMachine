using System.ComponentModel;
using Washin.App.Services;

public class LaundryViewModel : INotifyPropertyChanged
{
    private readonly BuildingApiService _buildingApiService;
    private string _machineState;
    private int _nbPeopleInQueue, _nbMachine, _nbMachineWorking, _nbMachineNotW, _nbPeopleBeforeMeInQueue, _nbMachineAvailable;
    private double _waitingTimeEstimate;

    public string MachineState
    {
        get { return _machineState; }
        set
        {
            _machineState = value;
            OnPropertyChanged(nameof(MachineState));
        }
    }

    public int PeopleInQueue
    {
        get { return _nbPeopleInQueue; }
        set
        {
            _nbPeopleInQueue = value;
            OnPropertyChanged(nameof(PeopleInQueue));
        }
    }

    public int Machines
    {
        get { return _nbMachine; }
        set
        {
            _nbMachine = value;
            OnPropertyChanged(nameof(Machines));
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

    public int MachineAvailable
    {
        get { return _nbMachineAvailable; }
        set
        {
            _nbMachineAvailable = value;
            OnPropertyChanged(nameof(MachineAvailable));
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
    public LaundryViewModel()
    {
        _buildingApiService = new BuildingApiService();
    }

    // Asynchronous initialization method
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
            var machinesData = await _buildingApiService.GetMachinesAsync(buildingId);
            var queueData = await _buildingApiService.GetQueueAsync(buildingId);

            // Parse and update data (replace these placeholders with actual data)
            MachineAvailable = 5; 
            MachineWorking = 5; 
            PeopleInQueue = 5; 

            // Set a default machine state
            MachineState = (MachineAvailable > 0) ? "State: Washing machine available" : "State: No washing machines available";

            // Estimate waiting time
            WaitingTimeEstimate = 20.0; // Placeholder
        }
        catch (Exception e)
        {
            // Handle any exceptions gracefully
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
