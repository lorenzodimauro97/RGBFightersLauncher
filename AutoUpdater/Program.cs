using AutoUpdater.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace AutoUpdater
{
    class Program
    {
        private static void Main(string[] args)
        {
            Thread.Sleep(1000);
            _7Zip.Extract(Settings.Default.launcherZip);
            File.Delete(Settings.Default.launcherZip);

            try { Process.Start(Settings.Default.launcherFile); }

            catch (Exception)
            {
                return;
            }
        }
    }
}
