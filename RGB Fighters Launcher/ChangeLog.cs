using System;
using System.Net;
using System.Windows;

namespace RGB_Fighters_Launcher
{
    class ChangeLog
    {
        public string Content { get; private set; }

        private readonly string address;

        public ChangeLog(string address)
        {
            this.address = address;
            Download();
        }

        private void Download()
        {
            WebClient client = new WebClient();

            try { Content = client.DownloadString($"{address}/changelog"); }

            catch(Exception ex)
            {
                Content = ex.Message;
            }
        }

    }
}
