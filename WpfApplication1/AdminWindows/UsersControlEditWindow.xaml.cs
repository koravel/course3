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
        string curtype,idText;
        public UsersControlEditWindow(string id,string _curtype)
        {
            InitializeComponent();
            string[] dataArray = new string[2];
            dataArray = DataBase.QueryRetRow(new string[] { "@_curtype" }, new string[] { _curtype }, "SELECT U_NAME,U_PASS FROM `user` WHERE U_TYPE=@_curtype;");
            DataBase.SetLog(id, 0, 0, "Выборка пользователя для редактирования,параметры:|логин:" + dataArray[0] + "|тип записи:" + _curtype + "|");
            textBoxLogin.Text=dataArray[0]; textBoxPassword.Text=dataArray[1];
            curtype = _curtype;
            idText = id;
        }

        private void buttonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxLogin.Text!="")
            {
                if(textBoxPassword.Text.Length==32)
                {
                    DataBase.Query(new string[] { "@_login", "@_password", "@_type" }, new string[] { textBoxLogin.Text, textBoxPassword.Text, curtype }, "UPDATE `user` SET `U_NAME`=@_login, `U_PASS`=@_password WHERE `U_TYPE`=@_type;");
                    DataBase.SetLog(idText, 1, 1, "Изменение пользователя,параметры:|тип записи:" + curtype + "|логин:" + textBoxLogin.Text + "|");
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
