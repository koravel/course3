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
        string curId,idText;
        public bool flag = false;
        public WarningDelManufacturerBindsWindow(string id,string _curId)
        {
            InitializeComponent();
            curId = _curId;
            idText = id;
        }

        private void checkBoxWarningSettings_Click(object sender, RoutedEventArgs e)
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
            DataBase.Query(new string[] { "@_curid" }, new string[] { curId }, "DELETE FROM `manufacturer` WHERE M_ID=@_curid;");
            DataBase.SetLog(idText, 1, 3, "Удаление производителя,параметры:|код:" + curId + "|");
            flag = true;
            this.Close();
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
