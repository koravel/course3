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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    DataBase.Connection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\nСоединение не установлено!");
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
                        new AdminWindow(DataBase.QueryRetCell(new string[] { "@_login", "@_pass" }, new string[] { LoginField.Text, DataBase.computeMD5(PasswordField.Password) }, "SELECT U_ID FROM `user` WHERE U_NAME=@_login AND U_PASS=@_pass;")).Show();
                        this.Close();
                    }
                    else
                    {
                        if(loginType == "Менеджер")
                        {
                            new ManagerWindow().Show();
                            this.Close();
                        }
                        else
                        {
                            if (loginType == "Кассир")
                            {
                                new SellerWindow().Show();
                                this.Close();
                            }
                        }
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
    }
}
