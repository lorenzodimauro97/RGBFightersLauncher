using System.IO;

namespace RGB_Fighters_Launcher
{
    class File
    {
        public static bool CheckIfExists(string name) => System.IO.File.Exists(name);

        public static bool CheckIfDirectoryExists(string name) => System.IO.Directory.Exists(name);

        public static long Checklength(string name)
        {
            if (!CheckIfExists(name)) return 0;
            var file = new FileInfo(name);
            return file.Length;
        }
    }
}
