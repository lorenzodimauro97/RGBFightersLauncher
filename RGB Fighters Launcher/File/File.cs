namespace RGB_Fighters_Launcher
{
    class File
    {
        public static bool CheckIfExists(string name) => System.IO.File.Exists(name);

        public static bool CheckIfDirectoryExists(string name) => System.IO.Directory.Exists(name);
    }
}
