using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            AddressBox.Text = this.window.address;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.address = AddressBox.Text;
            this.Close();
        }
    }
}
