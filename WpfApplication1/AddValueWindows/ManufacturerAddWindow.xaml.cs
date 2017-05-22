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
        string idText;
        public bool flag = false;
        public Manufacturer obj = new Manufacturer();
        public ManufacturerAddWindow(string id)
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
            if (ErrorCheck.ManufacturerEnterCheck(textBoxName,textBoxCountry,textBoxCity,textBoxAddress,textBoxTel))
            {
                DataBase.Query(new string[] { "@_name", "@_country", "@_city", "@_addr", "@_tel" }, new string[] { textBoxName.Text, textBoxCountry.Text, textBoxCity.Text, textBoxAddress.Text, textBoxTel.Text }, "INSERT INTO `manufacturer`(`M_NAME`,`M_COUNTRY`,`M_CITY`,`M_ADDR`,`M_TEL`)VALUES(@_name,@_country,@_city,@_addr,@_tel);");
                DataBase.SetLog(idText, 1, 2, "Создание производителя,параметры:|название:" + textBoxName.Text + "|страна:" + textBoxCountry.Text + "|адрес:" + textBoxAddress.Text + "|");
                obj.ID = int.Parse(DataBase.QueryRetCell(null, null, "SELECT MAX(M_ID) FROM manufacturer;"));
                obj.NAME = textBoxName.Text;
                obj.COUNTRY = textBoxCountry.Text;
                obj.CITY = textBoxCity.Text;
                obj.ADDR = textBoxAddress.Text;
                obj.TEL = textBoxTel.Text;
                flag = true;
                this.Close();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch((sender as TextBox).Name)
            {
                case "textBoxName":
                case "textBoxCountry":
                    {
                        (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red);
                        break;
                    }
                default:
                    {
                        (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Yellow);
                        break;
                    }
            }
        }
    }
}
