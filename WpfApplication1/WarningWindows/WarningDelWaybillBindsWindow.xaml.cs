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
    public partial class WarningDelWaybillBindsWindow : Window
    {
        string curId,idText;
        public bool flag = false;
        public WarningDelWaybillBindsWindow(string id, string _curId)
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
            List<Waybill> tempobj = DataBase.GetWaybill("SELECT `waybill`.`W_ID`,`waybill`.`W_DATE`,`waybill`.`E_ID`,`waybill`.`W_AGENT_NAME` FROM `waybill` WHERE W_ID=@_id", new string[] { "@_id" }, new string[] { curId });
            if (tempobj.Count > 0)
            {
                Files.SaveToArchive(curId, "\nКод:" + tempobj[0].ID + "\nДата составления:" + tempobj[0].DATE + "\nПринимающий:" + tempobj[0].EMPLOYEE + "\nКонтрагент:" + tempobj[0].AGENT, "Waybill");
            }
            DataBase.Query(new string[] { "@_curid" }, new string[] { curId }, "DELETE FROM `waybill` WHERE W_ID=@_curid;");
            DataBase.SetLog(idText, 1, 3, "Удаление накладной,параметры:|код:" + curId + "|");
            flag = true;
            this.Close();
        }

        private void checkBoxWarningSettings_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxWarningSettings.IsChecked == true)
            {
                Properties.Settings.Default.DelBindingToWaybill = true;
                Properties.Settings.Default.Save();
            }
        }
    }
}
