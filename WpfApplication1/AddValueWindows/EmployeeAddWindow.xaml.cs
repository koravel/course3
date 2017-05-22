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
                DataBase.Query(new string[] { "@_name", "@_tel", "@_pos", "@_contract" }, new string[] { textBoxName.Text, textBoxTel.Text, selectedPosition.ToString(), textBoxContract.Text }, "INSERT INTO `employee`(`E_NAME`,`E_TEL`,`E_POSITION`,`E_CONTRACT`)VALUES(@_name,@_tel,@_pos,@_contract);");
                this.Close();
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
