using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using Washin.App.Services;
using System.Windows.Input;
using System.Windows;


public class LaundryViewModel : INotifyPropertyChanged
{
    private readonly MachineApiService _machineApiService;
    private readonly BuildingApiService _buildingApiService;
    private string _machineState;
    private int _nbPeopleInQueue, _nbMachine, _nbMachineWorking, _nbMachineNotW, _nbPeopleBeforeMeInQueue, _nbMachineAvailable;
    private double _waitingTimeEstimate;
    public ObservableCollection<Machine> Machines { get; set; } = new ObservableCollection<Machine>();

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
    public RelayCommand ChangeMachineStateCommand { get; }

    public LaundryViewModel()
    {
        _machineApiService = new MachineApiService();

        // Initialize commands
        ChangeMachineStateCommand = new RelayCommand(() => ChangeMachineState());
    }

    private async void ChangeMachineState(Machine machine)
    {
        if (machine == null) return;

        try
        {
            // Toggle the IsWorking state 
            machine.IsWorking = !machine.IsWorking;

            // Call the API to update the machine status
            await _machineApiService.UpdateMachineStatusAsync(machine.Id, machine.UserId);

            // Notify UI of property changes (if needed)
            OnPropertyChanged(nameof(Machines));
        }
        catch (Exception ex)
        {
            // Handle exceptions (log, show error message, etc.)
            Console.WriteLine($"Error changing machine state: {ex.Message}");
        }
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
            //var machinesData = await _buildingApiService.GetMachinesAsync(buildingId);
            //var queueData = await _buildingApiService.GetQueueAsync(buildingId);

            // Parse and update data (replace these placeholders with actual data)
            MachineAvailable = 999;
            MachineWorking = 999;
            PeopleInQueue = 999;

            // Set a default machine state
            MachineState = (MachineAvailable > 0) ? "State: Washing machine available" : "State: No washing machines available";

            // Estimate waiting time
            WaitingTimeEstimate = 20.0; // Placeholder

            // Fetch data from the API

            var apiMachines = await _machineApiService.GetMachineAsync(buildingId);
            Machines.Clear();
            
            foreach (var apiMachine in apiMachines)
            {
                var machine = new Machine();
                machine.Id = apiMachine.Id;
                machine.IsWorking = apiMachine.IsWorking;
                machine.Id_Building = apiMachine.Id_Building;
                machine.UserId = apiMachine.UserId;

                machine.Id = 99;
                machine.IsWorking = true;
                machine.Id_Building = 55;
                machine.UserId = 99;

                Console.WriteLine(machine);
                Machines.Add(machine);
            }
            Machines.Add(new Machine { Id = 99, Id_Building = 101, IsWorking = true, UserId = 1 });
            Machines.Add(new Machine { Id = 98, Id_Building = 102, IsWorking = false, UserId = 2 }); 

            // Fetch available machines for the building
            //var machines = await _machineApiService.GetMachineAsync(buildingId);

        }
        catch (Exception e)
        {
            // Handle any exceptions gracefully
            Console.WriteLine("hello");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    private async void ChangeMachineState()
    {
        try
        {
            // Example: Change the state of the first machine in the list for simplicity
            if (Machines.Count == 0)
            {
                MessageBox.Show("No machines available to update.");
                return;
            }

            // For demonstration, pick the first machine or modify this logic as needed
            var machine = Machines.FirstOrDefault();

            if (machine != null)
            {
                // Toggle the machine state (e.g., if IsWorking is true, make it false, and vice versa)
                machine.IsWorking = !machine.IsWorking;

                // Call the API to update the machine's state
                await _machineApiService.UpdateMachineStatusAsync(machine.Id, machine.IsWorking ? null : machine.UserId);

                // Notify UI to refresh bindings
                OnPropertyChanged(nameof(Machines));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error changing machine state: {ex.Message}");
        }
    }

}


public class Machine
{
    public int Id { get; set; }
    public bool IsWorking { get; set; }
    public int Id_Building { get; set; }
    public int? UserId { get; set; } 
}
