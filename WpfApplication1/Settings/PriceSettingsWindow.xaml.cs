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

namespace WpfApplication1
{

    public partial class PriceSettingsWindow : Window
    {
        public PriceSettingsWindow()
        {
            InitializeComponent();
            upDownPriceProc.Text = Properties.Settings.Default.PriceProcent;
        }

        private void buttonPriceSettingsBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPriceSettingsSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.PriceProcent = upDownPriceProc.Text;
        }
    }
}
