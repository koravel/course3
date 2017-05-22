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
        string curId,idText;
        public bool flag = false;
        public Manufacturer obj;
        public ManufacturerEditWindow(string id,string _curId,Manufacturer tempObj)
        {
            InitializeComponent();
            curId = _curId;
            idText = id;
            obj = tempObj;
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
            if (ErrorCheck.ManufacturerEnterCheck(textBoxName, textBoxCountry, textBoxCity, textBoxAddress, textBoxTel))
            {
                DataBase.Query(
                    new string[] { "@_name", "@_country", "@_city", "@_addr", "@_tel", "@_curid" },
                    new string[] { textBoxName.Text, textBoxCountry.Text, textBoxCity.Text, textBoxAddress.Text, textBoxTel.Text, curId },
                    "UPDATE manufacturer SET M_NAME = @_name,M_COUNTRY = @_country,M_CITY = @_city,M_ADDR = @_addr,M_TEL = @_tel WHERE M_ID = @_curid;");
                DataBase.SetLog(idText, 1, 1, "Изменение производителя,параметры:|код:" + curId + "|");
                obj.NAME = textBoxName.Text;
                obj.COUNTRY = textBoxCountry.Text;
                obj.CITY = textBoxCity.Text;
                obj.ADDR = textBoxAddress.Text;
                obj.TEL = textBoxTel.Text;
                flag = true;
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
        
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch ((sender as TextBox).Name)
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
