using AutoUpdater.Properties;
using System.IO;
using System.Threading;

namespace AutoUpdater
{
    class Program
    {
        private static void Main(string[] args)
        {
            Thread.Sleep(2500);
            _7Zip.Extract(Settings.Default.launcherZip, "");

            File.Delete(Settings.Default.launcherZip);
        }
    }
}
