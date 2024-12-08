using System;
using System.Windows.Input;

/// <summary>
/// RelayCommand for the LOGIN.
/// </summary>
public class RelayCommandLogin : ICommand
    {
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommandLogin(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object parameter)
    {
        _execute();
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
