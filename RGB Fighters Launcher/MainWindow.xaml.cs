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
        public string address = @"http://rgbfighters.videogamezone.eu";

        public MainWindow(){ 
            InitializeComponent(); 
            ChangelogBox.Text = new ChangeLog(address).Content;
        }

    private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.IsEnabled = false;
            CheckClientUpdates(@"client.7z");
            return;
        }

        public void CheckClientUpdates(string clientName)
        {
            UpdateProgress("Verifica aggiornamenti...");
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateDownloadProgress);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);

            string hashFromFile = string.Empty;

            try {hashFromFile = client.DownloadString($"{address}/hash"); }

            catch(Exception ex)
            {
                ResetLauncher(true, $"Errore! {ex.Message}");
                return;
            }

            string clientHash = string.Empty;

            if (File.CheckIfExists(clientName)) clientHash = MD5.Calculate(clientName);

            if (Directory.Exists("Client") && hashFromFile.Contains(clientHash)) StartGame();

            else
            {
                if (!hashFromFile.Contains(clientHash)) MessageBox.Show($"Aggiornamento disponibile!\nHash Client:{clientHash}\nHash Server:{hashFromFile}");

                var preClearResult = Client.PreClear();

                if (!preClearResult.Item1)
                {
                    ResetLauncher(true, preClearResult.Item2);
                    return;
                }

                DownloadFile(client, new Uri($"{address}/RGBFighters.7z"), clientName);
            }
        }

        public void DownloadFile(WebClient client, Uri url, string clientName)
        {
            UpdateProgress("Download in corso...");
            LauncherProgressBar.Visibility = Visibility.Visible;
            client.DownloadFileAsync(url, clientName);
        }

        public void UpdateDownloadProgress(object sender, DownloadProgressChangedEventArgs e) => LauncherProgressBar.Value = e.ProgressPercentage;

        public void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            UpdateProgress("Download completato!");
            LauncherProgressBar.Visibility = Visibility.Hidden;
            Task.Run(() => ExtractFile(@"client.7z", "Client"));
        }

            public void ExtractFile(string sourceArchive, string destination)
        {
            var result = _7Zip.Extract(sourceArchive, destination);

            if(!result.Item1)
            {
                ResetLauncher(true, result.Item2);
                return;
            }
            StartGame();
        }

        public void ResetLauncher(bool isError, string errorMessage)
        {
            if(isError) MessageBox.Show(errorMessage);
            ProgressLabel.Content = string.Empty;
            PlayButton.IsEnabled = true;
            return;
        }

        public void StartGame()
        {
            UpdateProgress("Avvio gioco...");

            string gamePath = @"Client\RGB Fighters.exe";

            try {Process.Start(gamePath); }

            catch (Exception Ex)
            {
                ResetLauncher(true, $"Errore! {Ex.Message}");
                return;
            }
            Dispatcher.Invoke(() => Application.Current.Shutdown());
        }

        public void UpdateAddress(object sender, RoutedEventArgs e)
        {
            AddressInput input = new AddressInput(this);
            input.Show();
        }

        private void UpdateProgress(string message) => Dispatcher.Invoke(() => ProgressLabel.Content = message);
    }
}
