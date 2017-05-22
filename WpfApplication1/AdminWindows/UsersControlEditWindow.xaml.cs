using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class UsersControlEditWindow : Window
    {
        string curtype,idText;
        public bool flag = false;
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
            if (ErrorCheck.UserEnterCheck(textBoxLogin, textBoxPassword))
            {
                textBoxPassword.BorderBrush = Brushes.Green;
                DataBase.Query(new string[] { "@_login", "@_password", "@_type" }, new string[] { textBoxLogin.Text, textBoxPassword.Text, curtype }, "UPDATE `user` SET `U_NAME`=@_login, `U_PASS`=md5(@_password) WHERE `U_TYPE`=@_type;");
                DataBase.SetLog(idText, 1, 1, "Изменение пользователя,параметры:|тип записи:" + curtype + "|логин:" + textBoxLogin.Text + "|");
                flag = true;
                this.Close();
            }
            else
            {
                textBoxPassword.BorderBrush = Brushes.Red;
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red);
        }
    }
}
