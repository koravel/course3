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
    public partial class UsersControlWindow : Window
    {
        public UsersControlWindow()
        {
            InitializeComponent();
            dataGridUserOut.ItemsSource = DataBase.GetUser();
        }

        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            new UsersControlAddWindow().ShowDialog();            
        }

        private void UserDelete_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                if (dataGridUserOut.SelectedIndex != -1)
                {
                    DataBase.Query(new string[] { "@_login", "@_password" }
                        , new string[2] { Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 1, dataGridUserOut), Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 2, dataGridUserOut) }
                        , "DELETE FROM `user` WHERE U_NAME=@_login AND U_PASS=@_password;");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUserEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridUserOut.SelectedIndex != -1)
                {
                    new UsersControlEditWindow(Converter.DGCellToStringConvert(dataGridUserOut.SelectedIndex, 0, dataGridUserOut)).ShowDialog();
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
        }

    }
}
