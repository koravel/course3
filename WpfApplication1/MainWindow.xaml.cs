﻿using System;
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
using MySql.Data.MySqlClient;
using System.Windows.Media.Animation;
using System.IO;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    DataBase.Connection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (LoginField.Text != "" && PasswordField.Password != "")
            {
                string loginType = DataBase.UserAuthorization(LoginField.Text, DataBase.computeMD5(PasswordField.Password));
                if (loginType == null)
                {
                    MessageBox.Show("Таких пользователей не существует!");
                }
                else
                {
                    if (loginType == "Администратор")
                    {
                        string onlineCheck=DataBase.CheckLogin(LoginField.Text, DataBase.computeMD5(PasswordField.Password));

                        if (onlineCheck == "offline")
                            {
                                string query = @"UPDATE `user` SET U_ONLINE='online' WHERE U_NAME='" + LoginField.Text + "' AND U_PASS='" +DataBase.computeMD5(PasswordField.Password)+ "';";
                                DataBase.FieldChange(null,null,query);
                                query = @"SELECT U_ID FROM `user` WHERE U_NAME='" + LoginField.Text + "' AND U_PASS='" + DataBase.computeMD5(PasswordField.Password) + "';";
                                AdminWindow openWindow = new AdminWindow(DataBase.FieldChangeWithResult(null,null,query));
                                openWindow.Show();
                            }
                            else
                            {
                                MessageBox.Show("Пользователь уже онлайн!");
                                return;
                            }
                    }
                    else if(loginType == "Менеджер")
                    {
                        ManagerWindow openWindow = new ManagerWindow();
                        openWindow.Show();
                    }
                    else if (loginType == "Кассир")
                    {
                        SellerWindow openWindow = new SellerWindow();
                        openWindow.Show();
                    }
                    this.Close();
                }
            }

            LoginField.BorderBrush = Brushes.Red;
            PasswordField.BorderBrush = Brushes.Red;
            LoginField.BorderBrush = Brushes.Red;
            LoginField.Background = Brushes.Pink;
            PasswordField.Background = Brushes.Pink;
        }

         private void LoginExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

         private void DBSettings_Click(object sender, RoutedEventArgs e)
         {
             new DBSettingsWindow().ShowDialog();
         }

         private void EnterCatch_KeyDown(object sender, KeyEventArgs e)
         {
             if (e.Key == Key.Enter)
             {
                 Login_Click(null,null);
             }
         }
    }
}
