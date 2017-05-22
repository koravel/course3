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
    public partial class ReportsWindow : Window
    {
        List<NameIdList> employees = new List<NameIdList>();
        List<NameIdList> products = new List<NameIdList>();
        public ReportsWindow()
        {
            InitializeComponent();
            ListUpdate(comboBoxEmployees, employees, new string[] { "E_ID", "E_NAME" }, "SELECT E_ID,E_NAME FROM employee;");
            ListUpdate(comboBoxProduct, products, new string[] { "P_ID", "P_NAME" }, "SELECT P_ID,P_NAME FROM product;");
        }

        private void ListUpdate(ComboBox comboBox,List<NameIdList> list,string[] param,string query)
        {
            products.Clear();
            list = DataBase.GetNameIdList(param, query);
            comboBox.Items.Clear();
            comboBox.Items.Add("все");
            for (int i = 0; i < list.Count; i++)
            {
                comboBox.Items.Add(list[i].NAME + "(#" + list[i].ID + ")");
            }
        }
    }
}
