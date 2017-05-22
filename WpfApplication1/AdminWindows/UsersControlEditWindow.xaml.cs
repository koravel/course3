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
    public partial class UsersControlEditWindow : Window
    {
        string curtype;
        public UsersControlEditWindow(string _curtype)
        {
            InitializeComponent();
            string[] dataArray = new string[2];
            string[] typeText= new string[1];
            string[] typeValue= new string[1];
            typeText[0]="@_curtype"; typeValue[0]=_curtype;
            dataArray = DataBase.QueryRetRow(typeText, typeValue, "SELECT U_NAME,U_PASS FROM `user` WHERE U_TYPE=@_curtype;");
            textBoxLogin.Text=dataArray[0]; textBoxPassword.Text=dataArray[1];
            curtype = _curtype;
        }

        private void buttonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxLogin.Text!="")
            {
                if(textBoxPassword.Text.Length==32)
                {
                    string[] valueArray = new string[3];
                    string[] valueArrayText = new string[3];
                    valueArrayText[0] = "@_login";
                    valueArrayText[1] = "@_password";
                    valueArrayText[2] = "@_type";
                    valueArray[0] = textBoxLogin.Text;
                    valueArray[1] = textBoxPassword.Text;
                    valueArray[2] = curtype;
                    DataBase.Query(valueArrayText, valueArray, "UPDATE `user` SET `U_NAME`=@_login, `U_PASS`=@_password WHERE `U_TYPE`=@_type;");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("В поле пароля неверное количество символов!");
                }
            }
            else
            {
                MessageBox.Show("Поле псевдонима пустое!");
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
