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
    public partial class ManufacturerEditWindow : Window
    {
        string curId;
        public ManufacturerEditWindow(string _curId)
        {
            InitializeComponent();
            curId = _curId;
            string[] data = new string[5];
            data = DataBase.QueryRetRow(new string[]{ "@curid" },new string[]{ _curId },"SELECT M_NAME,M_COUNTRY,M_CITY,M_ADDR,M_TEL FROM manufacturer WHERE M_ID=@curid;");
            textBoxName.Text = data[0];
            textBoxCountry.Text = data[1];
            textBoxCity.Text = data[2];
            textBoxAddress.Text = data[3];
            textBoxTel.Text = data[4];
        }
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Введите название!");
            }
            else
            {
                dataCorrect++;
            }
            if (textBoxCountry.Text == "")
            {
                MessageBox.Show("Введите страну!");
            }
            else
            {
                dataCorrect++;
            }
            if (textBoxCity.Text == "")
            {
                MessageBox.Show("Введите город!");
            }
            else
            {
                dataCorrect++;
            }
            if (dataCorrect == 3)
            {
                DataBase.Query(
                    new string[] { "@_name", "@_country", "@_city", "@_addr", "@_tel", "@_curid" },
                    new string[] { textBoxName.Text, textBoxCountry.Text, textBoxCity.Text, textBoxAddress.Text, textBoxTel.Text, curId },
                    "UPDATE manufacturer SET M_NAME = @_name,M_COUNTRY = @_country,M_CITY = @_city,M_ADDR = @_addr,M_TEL = @_tel WHERE M_ID = @_curid;");
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
