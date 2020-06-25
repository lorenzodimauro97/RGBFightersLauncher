using RGB_Fighters_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RGB_Fighters_Launcher
{
    class Launcher
    {
        string downloadedLauncherHash;

        public void CheckUpdates()
        {
            Downloader client = new Downloader("launcherHash", null, null);
            client.DownloadString(new List<DownloadStringCompletedEventHandler>() { new DownloadStringCompletedEventHandler(SetHash) });
        }

        private void SetHash(object sender, DownloadStringCompletedEventArgs e)
        {

            try
            {
                downloadedLauncherHash = e.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore! {ex.InnerException.Message}");
                Application.Current.Shutdown();
            }

            if (CompareHash()) return;


            MessageBox.Show("Aggiornamento Launcher disponibile! Installazione obbligatoria.");
            DownloadLauncher();

        }

        private void DownloadLauncher()
        {
            var client = new Downloader(Settings.Default.launcherZip, null, null);
            client.DownloadFile(new AsyncCompletedEventHandler(LaunchAutoUpdater));
        }

        private bool CompareHash()
        {
            MessageBox.Show(downloadedLauncherHash + " " + Hash.CalculateMD5(Settings.Default.launcher));
            return downloadedLauncherHash.Contains(Hash.CalculateMD5(Settings.Default.launcher));
        }

        private void LaunchAutoUpdater(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
               Process.Start(Settings.Default.autoUpdater);
            }

            catch (Exception Ex)
            {
                MessageBox.Show($"Errore! {Ex.Message}");
            }
            Application.Current.Shutdown();
        }

    }
}
