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
        string idText;
        public bool flag = false;
        public Employee obj = new Employee();
        public EmployeeAddWindow(string id)
        {
            InitializeComponent();
            idText = id;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if(ErrorCheck.EmployeeEnterCheck(comboBoxPos,textBoxName,textBoxContract,textBoxEmployeeINN,textBoxTel))
            {
                DataBase.Query(new string[] { "@_name", "@_tel", "@_pos", "@_contract","@_inn" }, new string[] { textBoxName.Text, textBoxTel.Text, comboBoxPos.SelectedItem.ToString(), textBoxContract.Text,textBoxEmployeeINN.Text }, "INSERT INTO `employee`(`E_NAME`,`E_TEL`,`E_POSITION`,`E_CONTRACT`,E_INN)VALUES(@_name,@_tel,@_pos,@_contract,@_inn);");
                DataBase.SetLog(idText, 1, 2, "Создание работника,параметры:|имя:" + textBoxName.Text + "|должность:" + comboBoxPos.SelectedItem.ToString() + "|");
                obj.ID = int.Parse(DataBase.QueryRetCell(null, null, "SELECT MAX(E_ID) FROM employee;"));
                obj.NAME = textBoxName.Text;
                obj.TEL = textBoxTel.Text;
                obj.POSITION = comboBoxPos.SelectedItem.ToString();
                obj.CONTRACT = int.Parse(textBoxContract.Text);
                obj.INN = int.Parse(textBoxEmployeeINN.Text);
                flag = true;
                this.Close();
            }
        }

  

        private void comboBoxPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxPos.BorderBrush = ErrorCheck.SelectionCheck(comboBoxPos.SelectedIndex, comboBoxPos.Items.Count);
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
