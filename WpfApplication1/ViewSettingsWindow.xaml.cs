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

    public partial class ViewSettingsWindow : Window
    {
        public ViewSettingsWindow()
        {
            InitializeComponent();
            checkBoxShowCode.IsChecked = Properties.Settings.Default.ShowCode;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void checkBoxShowCode_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowCode = checkBoxShowCode.IsChecked.Value;
            Properties.Settings.Default.Save();
        }
    }
}
