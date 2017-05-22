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
    public partial class FirstWindow : Window
    {
        public FirstWindow()
        {
            InitializeComponent();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {

            int flag = 0;
            textBoxDirector.BorderBrush = ErrorCheck.TextCheck(textBoxDirector.Text, ref flag, 0, Brushes.Red); 
            textBoxSecurity.BorderBrush = ErrorCheck.TextCheck(textBoxSecurity.Text, ref flag, 0, Brushes.Red);
            if(flag == 2)
            {
                Properties.Settings.Default.Directors_password = DataBase.computeMD5(textBoxDirector.Text);
                Properties.Settings.Default.Security_password = DataBase.computeMD5(textBoxSecurity.Text);
                Properties.Settings.Default.Save();
                this.Close();
            }
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red); 
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
