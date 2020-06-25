using System;
using System.Diagnostics;

namespace RGB_Fighters_Launcher
{
    class _7Zip
    {
        public static (bool, string) Extract(string sourceArchive, string destination)
        {
            if (!File.CheckIfExists(sourceArchive)) return (false, "Errore! la cartella compressa è inesistente!");

            string zPath = "7za.exe"; //add to proj and set CopyToOuputDir

            try
            {
                ProcessStartInfo pro = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = zPath,
                    Arguments = $"x \"{sourceArchive}\" -y -o\"{destination}\""
                };

                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (Exception Ex)
            {
                return (false, $"Errore! {Ex.Message}");
            }
            return (true, "");
        }
    }




}
