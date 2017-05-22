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
                DataBase.Query(new string[] { "@_name", "@_country", "@_city", "@_addr", "@_tel" }, new string[] { textBoxName.Text, textBoxCountry.Text, textBoxCity.Text, textBoxAddress.Text, textBoxTel.Text }, "INSERT INTO `manufacturer`(`M_NAME`,`M_COUNTRY`,`M_CITY`,`M_ADDR`,`M_TEL`)VALUES(@_name,@_country,@_city,@_addr,@_tel);");
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
