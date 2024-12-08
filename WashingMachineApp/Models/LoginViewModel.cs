using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Washin.App.Services;
using WashingMachine;

/// <summary>
/// The LoginViewModel class manages the login functionality, allowing users to authenticate
/// and redirecting them to appropriate pages based on their roles (manager or resident).
/// </summary>
/// <remarks>
/// This class uses the MVVM pattern to bind login inputs and the login command to the UI. 
/// It interacts with the UserApiService to verify credentials.
/// </remarks>
public class LoginViewModel
{
    /// <summary>
    /// API service for user-related operations such as login verification.
    /// </summary>
    private readonly UserApiService _userApiService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
    /// Configures the login command and initializes the API service.
    /// </summary>
    public LoginViewModel()
    {
        _userApiService = new UserApiService();
        LoginCommand = new RelayCommandLogin(ExecuteLogin);
    }

    /// <summary>
    /// The username entered by the user.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// The password entered by the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Command to execute the login functionality.
    /// </summary>
    public ICommand LoginCommand { get; }

    /// <summary>
    /// Executes the login functionality, validating credentials and redirecting to the appropriate page.
    /// </summary>
    /// <remarks>
    /// This method verifies if the entered credentials are correct by calling the API service. If the user is a manager,
    /// it redirects to the manager page; otherwise, it redirects to the home page.
    /// </remarks>
    /// <example>
    /// Given the following credentials:
    /// Username: "admin"
    /// Password: "admin123"
    /// 
    /// If the user is a manager, the application will open the `Manager` window.
    /// If the user is a resident, it will open the `Home` window.
    /// </example>
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

            CurrentUser.UserId = user.Id;

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
