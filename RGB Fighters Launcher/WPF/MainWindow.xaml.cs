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
            //UpdateProgress("Verifica aggiornamenti...");

            Client client = new Client(ProgressLabel, LauncherProgressBar);
            client.Start();
            /*WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateDownloadProgress);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);

            string hashFromFile = string.Empty;

            try { hashFromFile = client.DownloadString($"{Settings.Default.launcherUrl}/hash"); }

            catch (Exception ex)
            {
                ResetLauncher(true, $"Errore! {ex.Message}");
                return;
            }*/

            //client = new Downloader(Settings.Default.launcherUrl, "hash", ProgressLabel, LauncherProgressBar);
            //client.DownloadString();

            /*string clientHash = string.Empty;

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

                DownloadFile(client, new Uri($"{Settings.Default.launcherUrl}/RGBFighters.7z"), clientName);
            }*/
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

        /* public void DownloadFile(WebClient client, Uri url, string clientName)
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

             if (!result.Item1)
             {
                 ResetLauncher(true, result.Item2);
                 return;
             }
             StartGame();
         }

         public void ResetLauncher(bool isError, string errorMessage)
         {
             if (isError) MessageBox.Show(errorMessage);

             UpdateProgress(string.Empty);

             SetWindowButtons(true);

             return;
         }

         public void StartGame()
         {
             UpdateProgress("Avvio gioco...");

             string gamePath = @"Client\RGB Fighters.exe";

             try { Process.Start(gamePath); }

             catch (Exception Ex)
             {
                 ResetLauncher(true, $"Errore! {Ex.Message}");
                 return;
             }

             Dispatcher.Invoke(() => Application.Current.Shutdown());
         }

         private void UpdateProgress(string message) => Dispatcher.Invoke(() => ProgressLabel.Content = message);

         private void SetWindowButtons(bool value)
         {
             PlayButton.IsEnabled = SettingsButton.IsEnabled = value;
         }*/
    }
}
