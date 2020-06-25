using RGB_Fighters_Launcher.Properties;
using System;
using System.Diagnostics;
using System.Windows;

namespace RGB_Fighters_Launcher
{
    class _7Zip
    {
        public static void Extract(string sourceArchive, string destination)
        {
            if (!File.CheckIfExists(sourceArchive)) {
                MessageBox.Show("Are you trying to extract something that doesn't exist?");
                Application.Current.Shutdown();
            }

            try
            {
                ProcessStartInfo pro = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = Settings.Default.unzipperFile,
                    Arguments = $"x \"{sourceArchive}\" -y -o\"{destination}\""
                };

                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Errore! {Ex.Message}");
                Application.Current.Shutdown();
            }
        }
    }




}
