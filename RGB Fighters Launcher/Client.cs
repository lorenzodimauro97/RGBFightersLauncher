using RGB_Fighters_Launcher.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RGB_Fighters_Launcher
{
    class Client
    {

        private readonly Label progressLabel;
        private readonly ProgressBar downloadBar;

        private readonly Hash hash;

        public Client(Label progressLabel, ProgressBar downloadBar)
        {
            this.progressLabel = progressLabel;
            this.downloadBar = downloadBar;

            hash = new Hash();
        }

        public void Start()
        {
            progressLabel.Content = "Checking content...";
            if (!File.CheckIfDirectoryExists(Settings.Default.clientFolderName) || !File.CheckIfExists(Settings.Default.launcherZip)) {
                PreClear();
                Download();
                return;
            }

            CheckHash();
        }

        private void Download()
        {
            var client = new Downloader(Settings.Default.launcherZip, progressLabel, downloadBar);
            client.DownloadFile(new AsyncCompletedEventHandler(Extract));
        }

        private void CheckHash()
        {
            progressLabel.Content = "Comparing hashes...";
            bool hashResult = hash.CompareHash();

            if (hashResult) StartGame();
            else
            {
                MessageBox.Show("Nuovo aggiornamento disponibile!");
                Download();
            }
        }

        private void Extract(object sender, AsyncCompletedEventArgs e)
        {
            progressLabel.Content = "Extracting update...";
            _7Zip.Extract(Settings.Default.launcherZip, Settings.Default.clientFolderName);
            Start();
        }

        private void StartGame()
        {
            try { Process.Start(Settings.Default.gamePath); }

            catch (Exception)
            {
                return;
            }
            Application.Current.Shutdown();
        }

        public static (bool, string) PreClear()
        {
            if (!Directory.Exists("Client")) return (true, "");

            try { Directory.Delete("Client", true); }

            catch (IOException ex)
            {
                return (false, $"Errore! {ex.Message}, chiudere il gioco!");
            }

            return (true, "");

        }
    }
}
