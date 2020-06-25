using RGB_Fighters_Launcher.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace RGB_Fighters_Launcher
{
    public partial class MainWindow : Window
    {
        public bool canLaunch = false;

        public Downloader client;

        public MainWindow()
        {
            InitializeComponent();
            _ = new ChangeLog(ChangelogBox);  //Inizializziamo il changelog
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetWindowButtons(false);
            CheckClientUpdates(@"client.7z");
            return;
        }

        public void CheckClientUpdates(string clientName)
        {
            Client client = new Client(ProgressLabel, LauncherProgressBar);
            client.Start();
        }

        public void UpdateAddress(object sender, RoutedEventArgs e)
        {
            AddressInput input = new AddressInput(this);
            input.Show();
        }

        private void SetWindowButtons(bool value)
        {
            PlayButton.IsEnabled = SettingsButton.IsEnabled = value;
        }
    }
}
