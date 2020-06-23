using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB_Fighters_Launcher
{
    class Client
    {
        public static (bool, string) PreClear()
        {
            if (!Directory.Exists("Client")) return (true, "");

            try { Directory.Delete("Client", true); }

            catch (IOException ex)
            {
                return (false, $"Errore! {ex.Message}, chiudere il gioco!");
            }

            return (true, "");

        }
    }
}
