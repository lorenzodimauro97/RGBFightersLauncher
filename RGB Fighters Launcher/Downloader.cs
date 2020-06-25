using RGB_Fighters_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace RGB_Fighters_Launcher
{
    public class Downloader
    {
        private string FileName { get; set; }

        public string DownloadStringResult { get; private set; }

        private readonly Label progressLabel;
        private readonly ProgressBar downloadBar;
        private WebClient client;

        public Downloader(string fileName, Label progressLabel, ProgressBar downloadBar)
        {
            FileName = fileName;
            this.progressLabel = progressLabel;
            this.downloadBar = downloadBar;
        }

        public void DownloadString(List<DownloadStringCompletedEventHandler> handler)
        {
            UpdateProgress($"Downloading {Settings.Default.clientUrl}/{FileName}");

            SetupStringAsync(handler);

            ChangeDownloadBarVisibility(Visibility.Visible);

            client.DownloadStringAsync(new Uri($"{Settings.Default.clientUrl}/{FileName}"));

        }

        public void DownloadFile(AsyncCompletedEventHandler handler)
        {
            UpdateProgress($"Downloading {FileName}");

            SetupFileAsync(handler);

            ChangeDownloadBarVisibility(Visibility.Visible);

            client.DownloadFileAsync(new Uri($"{Settings.Default.clientUrl}/{FileName}"), FileName);
        }

        private void SetupStringAsync(List<DownloadStringCompletedEventHandler> handler)
        {
            client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateDownloadProgress);

            foreach (var h in handler) client.DownloadStringCompleted += h;

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ClearClient);
        }

        private void SetupFileAsync(AsyncCompletedEventHandler handler)
        {
            client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateDownloadProgress);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(ClearClient) + handler;
        }

        public void UpdateDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            if (downloadBar != null)
                downloadBar.Value = e.ProgressPercentage;
        }

        public void ClearClient(object sender, AsyncCompletedEventArgs e)
        {
            UpdateProgress("Download completato!");
            ChangeDownloadBarVisibility(Visibility.Hidden);
            client.Dispose();
        }

        public void ChangeDownloadBarVisibility(Visibility visibility)
        {
            if (downloadBar != null)
                downloadBar.Visibility = visibility;
        }

        public void UpdateProgress(string message)
        {
            if (progressLabel != null)
                progressLabel.Content = message;
        }

    }
}
