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
    public partial class EmployeeAddWindow : Window
    {
        public EmployeeAddWindow()
        {
            InitializeComponent();
            comboBoxPos.Items.Add("Кассир");
            comboBoxPos.Items.Add("Уборщик");
            comboBoxPos.Items.Add("Фармацевт");
            comboBoxPos.Items.Add("Менеджер");
            comboBoxPos.Items.Add("Администратор");
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            object selectedPosition = comboBoxPos.SelectedItem;
            if(textBoxName.Text == "")
            {
                MessageBox.Show("Отсутствует Ф.И.О.!");
            }
            else
            {
                dataCorrect++;
            }
            if(selectedPosition == null)
            {
                MessageBox.Show("Отсутствует должность!");
            }
            else
            {
                dataCorrect++;
            }
            if(textBoxContract.Text == "")
            {
                MessageBox.Show("Отсутствует номер контракта!");
            }
            else
            {
                bool checkSymbols = true;
                for (int i = 0; i < textBoxContract.Text.Length;i++ )
                {
                    if(!Char.IsDigit(textBoxContract.Text[i]))
                    {
                        checkSymbols = false;
                        i = textBoxContract.Text.Length;
                    }
                }
                if(checkSymbols)
                {
                    dataCorrect++;
                }
                else
                {
                    MessageBox.Show("В номере контракта есть буквы!");
                }
            }
            if(dataCorrect == 3)
            {
                string[] value=new string[4];
                value[0] = textBoxName.Text;
                value[1] = textBoxTel.Text;
                value[2] = selectedPosition.ToString();
                value[3] = textBoxContract.Text;
                string[] valueText=new string[4];
                valueText[0] = "@_name";
                valueText[1] = "@_tel";
                valueText[2] = "@_pos";
                valueText[3] = "@_contract";
                DataBase.Query(valueText, value, "INSERT INTO `employee`(`E_NAME`,`E_TEL`,`E_POSITION`,`E_CONTRACT`)VALUES(@_name,@_tel,@_pos,@_contract);");
                this.Close();
            }
        }
    }
}
