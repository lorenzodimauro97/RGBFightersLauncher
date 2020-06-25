using AutoUpdater.Properties;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutoUpdater
{
    public class _7Zip
    {
        public static void Extract(string sourceArchive)
        {
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = Settings.Default.unzipperFile,
                    Arguments = $"e \"{sourceArchive}\" -aoa"
                };

                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Error! {Ex.InnerException.Message}");
            }
        }
    }




}
