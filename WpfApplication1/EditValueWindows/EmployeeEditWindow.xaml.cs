using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class EmployeeEditWindow : Window
    {
        string curId,idText;
        public bool flag = false;
        public Employee obj;
        public EmployeeEditWindow(string id,string _curId,Employee tempObj)
        {
            InitializeComponent();
            curId = _curId;
            idText = id;
            obj = tempObj;
            string[] data = new string[5];
            data = DataBase.QueryRetRow(new string[] { "@curid" }, new string[] { _curId }, "SELECT E_NAME,E_TEL,E_POSITION,E_CONTRACT,E_INN FROM employee where E_ID=@curid;");
            comboBoxPos.SelectedIndex = comboBoxPos.Items.IndexOf(data[2]);
            textBoxName.Text = data[0];
            textBoxTel.Text = data[1];
            textBoxContract.Text = data[3];
            textBoxEmployeeINN.Text = data[4];
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            if (ErrorCheck.EmployeeEnterCheck(comboBoxPos, textBoxName, textBoxContract, textBoxEmployeeINN, textBoxTel,false))
            {
                DataBase.Query(
                    new string[] { "@_name", "@_tel", "@_pos", "@_contract", "@_curid", "@_inn" },
                    new string[] { textBoxName.Text, textBoxTel.Text, comboBoxPos.SelectedItem.ToString(), textBoxContract.Text, curId, textBoxEmployeeINN.Text },
                    "UPDATE employee SET E_NAME = @_name,E_TEL = @_tel,E_POSITION = @_pos,E_CONTRACT = @_contract,E_INN = @_inn WHERE E_ID = @_curid;");
                DataBase.SetLog(idText, 1, 1, "Изменение работники,параметры:|код:" + curId + "|");
                obj.NAME = textBoxName.Text;
                obj.TEL = textBoxTel.Text;
                obj.POSITION = comboBoxPos.SelectedItem.ToString();
                obj.CONTRACT = int.Parse(textBoxContract.Text);
                obj.INN = int.Parse(textBoxEmployeeINN.Text);
                flag = true;
                this.Close();
            }
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch ((sender as TextBox).Name)
            {
                case "textBoxTel":
                    {
                        (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 1, Brushes.Yellow);
                        break;
                    }
                case "textBoxName":
                    {
                        (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red);
                        break;
                    }
                default:
                    {
                        (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 1, Brushes.Red);
                        break;
                    }
            }
        }

    }
}
