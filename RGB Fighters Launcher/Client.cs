using RGB_Fighters_Launcher.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
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
            hash.Download(new DownloadStringCompletedEventHandler(CheckHash));
        }

        public void Start()
        {
            progressLabel.Content = "Checking content...";

            if(hash.CheckIfPresent()) CheckHash(null, null);
        }

        private void Download()
        {
            var client = new Downloader(Settings.Default.clientZip, progressLabel, downloadBar);
            client.DownloadFile(new AsyncCompletedEventHandler(Extract));
        }

        private void CheckHash(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!hash.CheckIfPresent()) return;
            progressLabel.Content = "Comparing hashes...";

            if (hash.CompareHash()) StartGame();
            else
            {
                MessageBox.Show("Nuovo aggiornamento disponibile!");
                Download();
            }
        }

        private void Extract(object sender, AsyncCompletedEventArgs e)
        {
            progressLabel.Content = "Extracting update...";
            _7Zip.Extract(Settings.Default.clientZip, Settings.Default.clientFolderName);
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
    }
}
