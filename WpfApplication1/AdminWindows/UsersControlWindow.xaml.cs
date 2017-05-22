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
            string queryString = @"SELECT U_TYPE AS 'Тип записи',U_NAME AS 'Логин',U_PASS AS 'Пароль',U_ONLINE AS 'Статус' FROM `user`;";
            DataBase.TableOutput(queryString, dataGridUserOut);
        }

        private void UserAdd_Click(object sender, RoutedEventArgs e)
        {
            new UsersControlAddWindow().ShowDialog();            
        }

        private void UserDelete_Click(object sender, RoutedEventArgs e)
        {
            string queryString = @"DELETE FROM `user` WHERE U_NAME=@_login AND U_PASS=@_password;";
            string[] values=new string[2],valuesText=new string[2];
            //valuesText[0]="@_login";
            //valuesText[1]="@_password";
            //values[0] = dataGridUserOut;
            //values[1] = "";
            //DataBase.FieldChange(valuesText,values,queryString);
        }



        private void UserEdit_Click(object sender, RoutedEventArgs e)
        {
//            string queryString=@"UPDATE ";
        }

        private void dataGridUserOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            //MessageBox.Show("   You selected " + lbi.Content.ToString() + ".");
        }
    }
}
