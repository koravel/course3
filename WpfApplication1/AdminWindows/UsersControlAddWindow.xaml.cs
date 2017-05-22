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
        string idText;
        public bool flag = false;

        public UsersControlAddWindow(string id)
        {
            InitializeComponent();
            idText = id;
        }

        private void AddToDB_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxAddLogin.Text != "")
            {
                textBoxAddLogin.BorderBrush = Brushes.Green;
            }
            else
            {
                textBoxAddLogin.BorderBrush = Brushes.Red;
            }
            if (textBoxAddPassword.Text != "")
            {
                textBoxAddPassword.BorderBrush = Brushes.Green;
                DataBase.Query(
                            new string[] { "@_type", "@_login", "@_pass" },
                            new string[] { textBoxAddType.Text, textBoxAddLogin.Text, textBoxAddPassword.Text },
                            "INSERT INTO `user` (`U_TYPE`,`U_NAME`,`U_PASS`) VALUES (@_type, @_login, md5(@_pass));");
                DataBase.SetLog(idText, 1, 2, "Создание пользователя,параметры:|тип записи:" + textBoxAddType.Text + "|логин:" + textBoxAddLogin.Text + "|");
                flag = true;
                this.Close();
               
            }
            else
            {
                textBoxAddLogin.BorderBrush = Brushes.Red;
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
