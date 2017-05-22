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
    public partial class DuplicateDiscountWindow : Window
    {
        public bool flag = false;
        public DuplicateDiscountWindow()
        {
            InitializeComponent();
        }

        private void checkBoxWarningSettings_Click(object sender, RoutedEventArgs e)
        {
            if((sender as CheckBox).IsChecked == true)
            {
                Properties.Settings.Default.DuplDiscount = true;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            this.Close();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
