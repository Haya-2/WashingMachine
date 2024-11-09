﻿using System.Text;
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

namespace WashingMachine
{
    /// <summary>
    /// Interaction logic for SeeResidents.xaml
    /// </summary>
    public partial class SeeResidents : Window
    {
        public SeeResidents()
        {
            InitializeComponent();
            DataContext = new CombinedViewModel(new ManagerViewModel(), App.LaundryViewModel);
        }

        private void ReturnToManager_Click(object sender, RoutedEventArgs e)
        {
            Manager W = new Manager();
            W.Show();
            this.Close(); // Close the KeyDistributionWindow window
        }
    }
}
