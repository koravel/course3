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
    public partial class ManufacturerAddWindow : Window
    {
        public ManufacturerAddWindow()
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
            if(textBoxName.Text == "")
            {
                MessageBox.Show("Введите название!");
            }
            else
            {
                dataCorrect++;
            }
            if(textBoxCountry.Text == "")
            {
                MessageBox.Show("Введите страну!");
            }
            else
            {
                dataCorrect++;
            }
            if(textBoxCity.Text == "")
            {
                MessageBox.Show("Введите город!");
            }
            else
            {
                dataCorrect++;
            }
            if(dataCorrect == 3)
            {
                string[] value = new string[5];
                value[0] = textBoxName.Text;
                value[1] = textBoxCountry.Text;
                value[2] = textBoxCity.Text;
                value[3] = textBoxAddress.Text;
                value[4] = textBoxTel.Text;
                string[] valueText = new string[5];
                valueText[0] = "@_name";
                valueText[1] = "@_country";
                valueText[2] = "@_city";
                valueText[3] = "@_addr";
                valueText[4] = "@_tel";
                DataBase.Query(valueText, value, "INSERT INTO `manufacturer`(`M_NAME`,`M_COUNTRY`,`M_CITY`,`M_ADDR`,`M_TEL`)VALUES(@_name,@_country,@_city,@_addr,@_tel);");
                this.Close();
            }
        }
    }
}
