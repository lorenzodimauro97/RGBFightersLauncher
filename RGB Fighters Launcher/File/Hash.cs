using RGB_Fighters_Launcher.Properties;
using System;
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
            Download();
        }

        public bool CompareHash()
        {
            return DownloadedHash.Contains(CalculateMD5(Settings.Default.launcherZip));
        }

        private void Download() {
            Downloader client = new Downloader("hash", null, null);
            client.DownloadString(new DownloadStringCompletedEventHandler(SetHash));
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
