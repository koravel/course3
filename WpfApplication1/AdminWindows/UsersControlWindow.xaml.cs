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
                int indexTemp = dataGridUserOut.SelectedIndex;
                if (indexTemp != -1)
                {
                    var cellInfo = new DataGridCellInfo(dataGridUserOut.Items[indexTemp], dataGridUserOut.Columns[1]);
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                    string[] values = new string[2];
                    values[0] = content.Text;
                    cellInfo=new DataGridCellInfo(dataGridUserOut.Items[indexTemp], dataGridUserOut.Columns[2]);
                    content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                    values[1]=content.Text;
                    DataBase.Query(new string[] { "@_login", "@_password" }, values, "DELETE FROM `user` WHERE U_NAME=@_login AND U_PASS=@_password;");
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
                int indexTemp = dataGridUserOut.SelectedIndex;
                if (indexTemp != -1)
                {
                    var cellInfo = new DataGridCellInfo(dataGridUserOut.Items[indexTemp], dataGridUserOut.Columns[0]);
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                    new UsersControlEditWindow(content.Text).ShowDialog();
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
