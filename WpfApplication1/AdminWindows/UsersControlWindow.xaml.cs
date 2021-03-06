﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
    public partial class UsersControlWindow : Window
    {
        string idText;
        public UsersControlWindow(string id)
        {
            InitializeComponent();
            dataGridUserOut.ItemsSource = DataBase.GetUser();
            DataBase.SetLog(id, 0, 0, "Заполнение таблицы пользователей...");
            idText = id;
        }

        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            UsersControlAddWindow window = new UsersControlAddWindow(idText);
            window.ShowDialog();
            if (window.flag)
            {
                dataGridUserOut.ItemsSource = DataBase.GetUser();
            }
        }

        private void UserDelete_Click(object sender, RoutedEventArgs e)
        {

            if (dataGridUserOut.SelectedIndex != -1 && Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 0, dataGridUserOut) != "Администратор")
                {
                    string login = Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 1, dataGridUserOut);
                    DataBase.Query(new string[] { "@_login", "@_password" }
                        , new string[2] { login, Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 2, dataGridUserOut) }
                        , "DELETE FROM `user` WHERE U_NAME=@_login AND U_PASS=@_password;");
                    dataGridUserOut.ItemsSource = DataBase.GetUser();
                    DataBase.SetLog(idText, 1, 3, "Удаление пользователя,параметры:|логин:" + login + "|");
                }
        }

        private void buttonUserEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridUserOut.SelectedIndex != -1)
                {
                    UsersControlEditWindow window = new UsersControlEditWindow(idText,Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 0, dataGridUserOut));
                    window.ShowDialog();
                    if(window.flag)
                    {
                        dataGridUserOut.ItemsSource = DataBase.GetUser();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            dataGridUserOut.ItemsSource = DataBase.GetUser();
            DataBase.SetLog(idText, 1, 0, "Обновление таблицы пользователей...");
        }

        private void dataGridUserOut_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex != -1)
            {
                switch (e.Key)
                {
                    case Key.Delete:
                        {
                            UserDelete_Click(null, null);
                            break;
                        }
                    case Key.Insert:
                        {
                            UserAdd_Click(null, null);
                            break;
                        }
                    case Key.F12:
                        {
                            buttonUserEdit_Click(null, null);
                            break;
                        }
                    case Key.F11:
                        {
                            buttonUpdate_Click(null, null);
                            break;
                        }
                }
            }
        }
    }
}
