using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Washin.App.Services;
using WashingMachine;

public class LoginViewModel
{
    private readonly UserApiService _userApiService;

    public LoginViewModel()
    {
        _userApiService = new UserApiService();
        LoginCommand = new RelayCommand(ExecuteLogin);
    }

    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand LoginCommand { get; }

    private async void ExecuteLogin()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            MessageBox.Show("Please enter both login and password.");
            return;
        }

        try
        {
            var user = await _userApiService.GetLoginAsync(Username, Password);

            if (user == null)
            {
                MessageBox.Show("Invalid login or password.");
                return;
            }

            // Check if the user is a manager or not
            if (user.IsManager)
            {
                // Redirect to Manager Page
                Manager managerWindow = new Manager();
                managerWindow.Show();
            }
            else
            {
                // Redirect to Home Page
                Home homeWindow = new Home();
                homeWindow.Show();
            }

            // Close the current login window
            Application.Current.MainWindow.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }
    }
}
