using RGB_Fighters_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;

namespace RGB_Fighters_Launcher
{
    class Hash
    {
        private string DownloadedHash { get; set; }

        public Hash()
        {
            DownloadedHash = string.Empty;
        }

        public bool CompareHash()
        {
            return DownloadedHash.Contains(CalculateMD5(Settings.Default.launcherZip));
        }

        public void Download(DownloadStringCompletedEventHandler handler) 
        {
            Downloader client = new Downloader("hash", null, null);
            client.DownloadString(new List<DownloadStringCompletedEventHandler>() { new DownloadStringCompletedEventHandler(SetHash), handler});
        }

        private void SetHash(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                DownloadedHash = e.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore! Impossibile verificare hash! " + ex.InnerException.Message);
                Application.Current.Shutdown();
            }
        }

        public bool CheckIfPresent()
        {
            return DownloadedHash != string.Empty;
        }

        public static string CalculateMD5(string filename)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
