using System.Windows;
using System.Windows.Controls;

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
