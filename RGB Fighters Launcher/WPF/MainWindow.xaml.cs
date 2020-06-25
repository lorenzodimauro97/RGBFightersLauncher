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
            Launcher launcher = new Launcher();
            launcher.CheckUpdates();
            _ = new ChangeLog(ChangelogBox);  //Inizializziamo il changelog
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetWindowButtons(false);
            StartClient();
            return;
        }

        public void StartClient()
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
