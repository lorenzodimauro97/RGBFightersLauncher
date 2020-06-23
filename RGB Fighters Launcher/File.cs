namespace RGB_Fighters_Launcher
{
    class File
    {
        public static bool CheckIfExists(string name)
        {
            return System.IO.File.Exists(name);
        }
    }
}
