using System.Configuration;
using System.Data;
using System.Windows;

namespace WashingMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LaundryViewModel LaundryViewModel { get; } = new LaundryViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //var mainWindow = new Login(); // Start with Home screen, un comment to start two instances
            //mainWindow.Show();
        }
    }

}
