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
using System.IO;

namespace WpfApplication1
{
    public partial class DBSettingsWindow : Window
    {
        public DBSettingsWindow()
        {
            InitializeComponent();

            try
            {
                    textBox_Adress.Text = Properties.Settings.Default.Address;
                    textbox_DBName.Text = Properties.Settings.Default.Database;
                    textBox_Loign.Text = Properties.Settings.Default.Login;
                    textBox_Password.Text = Properties.Settings.Default.Password;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Address = textBox_Adress.Text;
                Properties.Settings.Default.Database = textbox_DBName.Text;
                Properties.Settings.Default.Login = textBox_Loign.Text;
                Properties.Settings.Default.Password = textBox_Password.Text;
                Properties.Settings.Default.Save();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToDefault_Click(object sender, RoutedEventArgs e)
        {
            textBox_Adress.Text = Properties.Settings.Default.Address_default;
            textbox_DBName.Text = Properties.Settings.Default.Database_default;
            textBox_Loign.Text = Properties.Settings.Default.Login_default;
            textBox_Password.Text = Properties.Settings.Default.Password_default;

        }

        private void CheckCon_Click(object sender, RoutedEventArgs e)
        {
            if (DataBase.ConCheck(new string[4] { textBox_Adress.Text, textbox_DBName.Text, textBox_Loign.Text, textBox_Password.Text }) == true)
            {
                MessageBox.Show("Соединение установлено.");
            }
            else
            {
                MessageBox.Show("Соединение отсутствует.");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
