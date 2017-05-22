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
    public partial class WarningDelManufacturerBindsWindow : Window
    {
        string curId;
        public WarningDelManufacturerBindsWindow(string _curId)
        {
            InitializeComponent();
            curId = _curId;
        }

        private void checkBoxWarningSettings_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBoxWarningSettings.IsChecked == true)
            {
                Properties.Settings.Default.DelBindingToManufacturer = true;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonNotDelBinds_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonDelBinds_Click(object sender, RoutedEventArgs e)
        {
            string[] values = new string[1], valuesText = new string[1];
            valuesText[0] = "@_curid";
            values[0] = curId;
            DataBase.Query(valuesText, values, "DELETE FROM `product` WHERE M_ID=@_curid;");
            DataBase.Query(valuesText, values, "DELETE FROM `discount` WHERE M_ID=@_curid;");
        }
    }
}
