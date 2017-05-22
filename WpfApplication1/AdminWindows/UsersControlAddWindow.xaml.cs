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

    public partial class UsersControlAddWindow : Window
    {
        public UsersControlAddWindow()
        {
            InitializeComponent();
        }

        private void AddToDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string queryString = @"INSERT INTO `user` (`U_ID`,`U_TYPE`,`U_NAME`,`U_PASS`,`U_ONLINE`) VALUES (null, @_type, @_login, md5(@_pass), 'offline');";
                string[] obj = new string[3];
                string[] objVal = new string[3];
                obj[0] = "@_type";
                obj[1] = "@_login";
                obj[2] = "@_pass";
                objVal[0] = textBoxAddType.Text;
                objVal[1] = textBoxAddLogin.Text;
                objVal[2] = textBoxAddPassword.Text;
                DataBase.Query(obj, objVal, queryString);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
