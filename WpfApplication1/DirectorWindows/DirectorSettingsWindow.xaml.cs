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
    public partial class DirectorSettingsWindow : Window
    {
        public DirectorSettingsWindow()
        {
            InitializeComponent();
            upDownPriceProc.Text = Properties.Settings.Default.PriceProcent;

        }

        private void buttonSavePriceProcent_Click(object sender, RoutedEventArgs e)
        {
            if(ErrorCheck.TextCheck(upDownPriceProc.Text,1))
            {
                Properties.Settings.Default.PriceProcent = upDownPriceProc.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonSaveDirPass_Click(object sender, RoutedEventArgs e)
        {
            PasswordCange(textBoxOldPass, textBoxNewPass, textBoxNewPassConfirm, "SELECT PW_PASS FROM passwords WHERE PW_TYPE='1';", "UPDATE passwords SET PW_PASS=@_pass WHERE PW_TYPE='1'");
        }

        private void PasswordCange(PasswordBox old, PasswordBox newpass, PasswordBox confirm, string query,string outquery)
        {
            if (DataBase.computeMD5(old.Password) == DataBase.QueryRetCell(null, null, query))
            {
                old.BorderBrush = Brushes.Gray;
                if (newpass.Password == confirm.Password && newpass.Password != "")
                {
                    newpass.BorderBrush = Brushes.Gray;
                    confirm.BorderBrush = Brushes.Gray;
                    DataBase.Query(new string[] { "@_pass" }, new string[] { DataBase.computeMD5(confirm.Password) }, outquery);
                }
                else
                {
                    newpass.BorderBrush = Brushes.Red;
                    confirm.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                old.BorderBrush = Brushes.Red;
            }
        }

        private void buttonSaveSecurPass_Click(object sender, RoutedEventArgs e)
        {
            PasswordCange(textBoxOldSecurityPass, textBoxNewSecurityPass, textBoxConfirmSecurityPass, "SELECT PW_PASS FROM passwords WHERE PW_TYPE='2';", "UPDATE passwords SET PW_PASS=@_pass WHERE PW_TYPE='2'");
        }

        private void buttonSaveDirPassAcc_Click(object sender, RoutedEventArgs e)
        {
            PasswordCange(textBoxOldPassAcc, textBoxNewPassAcc, textBoxNewPassConfirmAcc, "SELECT U_PASS FROM `user` WHERE U_TYPE='Директор';", "UPDATE `user` SET U_PASS=@_pass WHERE U_TYPE='Директор'");
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
