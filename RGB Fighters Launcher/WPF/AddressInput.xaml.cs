using RGB_Fighters_Launcher.Properties;
using System.Windows;

namespace RGB_Fighters_Launcher
{
    /// <summary>
    /// Interaction logic for AddressInput.xaml
    /// </summary>
    public partial class AddressInput : Window
    {
        private MainWindow window;

        public AddressInput(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            AddressBox.Text = Settings.Default.launcherUrl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.launcherUrl = AddressBox.Text;
            Settings.Default.Save();
            this.Close();
        }
    }
}
