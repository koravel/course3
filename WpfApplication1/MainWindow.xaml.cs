using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            bool flag = true;
            CheckConWindow checkWindow = null;
            DBSettingsWindow setWindow = null;
            InitializeComponent();
            while (!DataBase.ConCheck(new string[] { Properties.Settings.Default.Address, Properties.Settings.Default.Database, Properties.Settings.Default.Login, Properties.Settings.Default.Password }) && flag)
            {
                checkWindow = new CheckConWindow();
                setWindow = new DBSettingsWindow();
                checkWindow.ShowDialog();
                if(checkWindow.flag)
                {
                    setWindow.ShowDialog();
                }
                else
                {
                    flag = false;
                    this.Close();
                }
                checkWindow.Close();
                setWindow.Close();
            }
            if (DataBase.ConCheck(new string[] { Properties.Settings.Default.Address, Properties.Settings.Default.Database, Properties.Settings.Default.Login, Properties.Settings.Default.Password }))
            {
                if (DataBase.QueryRetCell(null, null, "SELECT COUNT(PW_ID) FROM passwords WHERE PW_TYPE='1';") == "0")
                {
                    this.IsEnabled = false;
                    new FirstWindow().ShowDialog();
                }
                if (DataBase.QueryRetCell(null, null, "SELECT COUNT(PW_ID) FROM passwords WHERE PW_TYPE='1';") == "0" || DataBase.QueryRetCell(null, null, "SELECT COUNT(PW_ID) FROM passwords WHERE PW_TYPE='2';") == "0")
                {
                    this.Close();
                }
                else
                {
                    this.IsEnabled = true;
                    if (DataBase.QueryRetCell(null, null, "SELECT IFNULL(COUNT(U_ID),0) FROM `user` WHERE U_TYPE=3;") == "0")
                    {
                        MessageBox.Show("Теперь добавьте администратора. Для добавления нажмите любую клавишу.");
                        new UsersControlWindow("-1").ShowDialog();
                    }
                }
            }
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
                DataBase.Query(null, null, "USE " + Properties.Settings.Default.Database + ";");
                string loginType = DataBase.UserAuthorization(LoginField.Text, DataBase.computeMD5(PasswordField.Password));
                if (loginType == null)
                {
                    MessageBox.Show("Таких пользователей не существует!");
                }
                else
                {
                    string idText = DataBase.QueryRetCell(new string[] { "@_login", "@_pass" }, new string[] { LoginField.Text, DataBase.computeMD5(PasswordField.Password) }, "SELECT U_ID FROM `user` WHERE U_NAME=@_login AND U_PASS=@_pass;");
                    if (loginType == "Директор")
                    {
                        new DirectorWindow(idText).Show();
                        this.Close();
                    }
                    if (loginType == "Администратор")
                    {
                        new AdminWindow(idText).Show();
                        this.Close();
                    }
                    else
                    {
                        if (loginType == "Кассир")
                        {
                            new SellerWindow(idText).Show();
                            this.Close();
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
