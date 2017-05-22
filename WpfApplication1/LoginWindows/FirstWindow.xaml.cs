using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            textBoxDirectorLogin.BorderBrush = ErrorCheck.TextCheck(textBoxDirector.Text, ref flag, 0, Brushes.Red); 
            textBoxSecurity.BorderBrush = ErrorCheck.TextCheck(textBoxSecurity.Text, ref flag, 0, Brushes.Red);
            if(flag == 3)
            {
                DataBase.Query(new string[]{ "@_dpass"},new string[]{DataBase.computeMD5(textBoxDirector.Text)}, "INSERT INTO passwords(PW_PASS,PW_TYPE)VALUES(@_dpass,1);");
                DataBase.Query(new string[] { "@_spass" }, new string[] { DataBase.computeMD5(textBoxSecurity.Text) }, "INSERT INTO passwords(PW_PASS,PW_TYPE)VALUES(@_spass,2);");
                DataBase.Query(new string[] { "@_login", "@_pass" }, new string[] { textBoxDirectorLogin.Text,DataBase.computeMD5(textBoxDirector.Text) }, "INSERT INTO `user`(U_NAME,U_PASS,U_TYPE)VALUES(@_login,@_pass,'Директор');");
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
