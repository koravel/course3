﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
                string type = "";
                textBoxAddPassword.BorderBrush = Brushes.Green;
                switch(comboBoxType.SelectedItem.ToString())
                {
                    case "Администратор":
                        {
                            type = "3";
                            break;
                        }
                    case "Кассир":
                        {
                            type = "1";
                            break;
                        }
                }
                DataBase.Query(
                            new string[] { "@_type", "@_login", "@_pass" },
                            new string[] { type, textBoxAddLogin.Text, textBoxAddPassword.Text },
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
