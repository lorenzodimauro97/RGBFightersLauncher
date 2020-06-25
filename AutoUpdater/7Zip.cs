using AutoUpdater.Properties;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutoUpdater
{
    public class _7Zip
    {
        public static void Extract(string sourceArchive, string destination)
        {
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = Settings.Default.unzipperFile,
                    Arguments = $"x \"{sourceArchive}\" -aoa -o\"{destination}\""
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
