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
    public partial class WarningDelProductBindsWindow : Window
    {
        string curId,idText;
        public bool flag = false;
        public WarningDelProductBindsWindow(string id,string _curId)
        {
            InitializeComponent();
            curId = _curId;
            idText = id;
        }

        private void buttonNotDelBinds_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonDelBinds_Click(object sender, RoutedEventArgs e)
        {
            List<Product> tempobj = DataBase.GetProduct("SELECT * FROM product WHERE P_ID=@_id", new string[] { "@_id" }, new string[] { curId });
            if (tempobj.Count > 0)
            {
                Files.SaveToArchive(curId, "\nКод:" + tempobj[0].ID + "\nНазвание:" + tempobj[0].NAME + "\nПроизводитель:" + tempobj[0].MANUFACTURER + "\nФорма отпуска:" + tempobj[0].FORM + "\nГруппа:" + tempobj[0].GROUP + "\nМатериал:" + tempobj[0].MATERIAL + "\nУпаковка" + tempobj[0].PACK + "\nИнструкция" + tempobj[0].INSTR, "Product");
            }
            DataBase.Query(new string[] { "@_curid" }, new string[] { curId }, "DELETE FROM `product` WHERE P_ID=@_curid;");
            DataBase.SetLog(idText, 1, 3, "Удаление товара,параметры:|код:" + curId + "|");
            flag = true;
            this.Close();
        }

        private void checkBoxWarningSettings_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxWarningSettings.IsChecked == true)
            {
                Properties.Settings.Default.DelBindingToProduct1 = true;
                Properties.Settings.Default.Save();
            }
        }
    }
}
