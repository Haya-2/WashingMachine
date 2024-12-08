using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WashingMachine.ViewModels;
using WashingMachine.Views;

namespace WashingMachine
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new LaundryViewModel(CurrentUser.UserId);
        }

        private void Button_Chrono(object sender, RoutedEventArgs e)
        {
            Chrono chronoPage = new Chrono(this, false); // Indicate it's the main Home window
            MainContentHome.Content = chronoPage;
        }

        private async void Button_Queue(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LaundryViewModel;

            await viewModel.ExecuteGetInQueue();

            HomeWait homeWaitPage = new HomeWait(this,viewModel); // Pass `this` as the parent Home window
            MainContentHome.Content = homeWaitPage;
        }

    }
}