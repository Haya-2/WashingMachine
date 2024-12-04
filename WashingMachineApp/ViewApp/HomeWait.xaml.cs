﻿using System.ComponentModel;
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

namespace WashingMachine.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class HomeWait : UserControl
    {
        public readonly Home _home;
        public HomeWait(Home home)
        {
            InitializeComponent();
            _home = home;
            DataContext = App.LaundryViewModel;
        }
        private void Button_Chrono(object sender, RoutedEventArgs e)
        {
            Chrono chronoPage = new Chrono(this); // Pass HomeWait to Chrono
            _home.MainContentHome.Content = chronoPage;
        }

        private void Button_QuitQueue(object sender, RoutedEventArgs e)
        {
            _home.MainContentHome.Content = null;
        }
    }
}