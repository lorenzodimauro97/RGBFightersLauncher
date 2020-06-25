﻿using RGB_Fighters_Launcher.Properties;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Controls;

namespace RGB_Fighters_Launcher
{
    class ChangeLog
    {
        public string Content { get; private set; }

        public TextBlock textBlock;

        public ChangeLog(TextBlock textBlock)
        {
            this.textBlock = textBlock;
            Download();
        }

        private void Download()
        {
            Downloader client = new Downloader("changelog", null, null);
            client.DownloadString(new DownloadStringCompletedEventHandler(SetContent));
        }

        private void SetContent(object sender, DownloadStringCompletedEventArgs e){

            try
            {
                Content = e.Result;
            }
            catch(Exception ex)
            {
                Content = ex.InnerException.Message;
            }

                textBlock.Text = Content;
        }

    }
}
