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
            if (ErrorCheck.UserEnterCheck(comboBoxType,textBoxAddLogin,textBoxAddPassword))
            {
                textBoxAddPassword.BorderBrush = Brushes.Green;
                DataBase.Query(
                            new string[] { "@_type", "@_login", "@_pass" },
                            new string[] { comboBoxType.SelectedItem.ToString(), textBoxAddLogin.Text, textBoxAddPassword.Text },
                            "INSERT INTO `user` (`U_TYPE`,`U_NAME`,`U_PASS`) VALUES (@_type, @_login, md5(@_pass));");
                DataBase.SetLog(idText, 1, 2, "Создание пользователя,параметры:|тип записи:" + comboBoxType.SelectedItem.ToString() + "|логин:" + textBoxAddLogin.Text + "|");
                flag = true;
                this.Close();
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red);
        }

        private void comboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxType.BorderBrush = ErrorCheck.SelectionCheck(comboBoxType.SelectedIndex, comboBoxType.Items.Count);
        }
    }
}
