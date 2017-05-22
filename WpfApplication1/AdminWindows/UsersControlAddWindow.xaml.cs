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
                DataBase.Query(new string[] { "@_type", "@_login", "@_pass" }, new string[] { textBoxAddType.Text, textBoxAddLogin.Text, textBoxAddPassword.Text }, "INSERT INTO `user` (`U_ID`,`U_TYPE`,`U_NAME`,`U_PASS`,`U_ONLINE`) VALUES (null, @_type, @_login, md5(@_pass), 'offline');");
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
