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
using System.Data;
using Xceed.Wpf.Toolkit;

namespace WpfApplication1
{
    public partial class WaybillAddWindow : Window
    {
        List<NameIdList> employees = DataBase.GetNameIdList(new string[] { "E_ID", "E_NAME" }, "SELECT E_ID,E_NAME FROM employee;");
        List<WaybillOutput> list = new List<WaybillOutput>();
        List<NameIdList> products = DataBase.GetNameIdList(new string[] { "P_ID", "P_NAME" }, "SELECT P_ID,P_NAME FROM product;");
        public WaybillAddWindow()
        {
            InitializeComponent();
            datePickerToday.Text = DateTime.Today.ToString();
            string[] tempMas = new string[products.Count];
            for (int i = 0; i < employees.Count; i++)
            {
                comboBoxEployees.Items.Add(employees[i].NAME+"(#"+employees[i].ID+")");
            }
            for (int i = 0; i < products.Count; i++ )
            {
                tempMas[i] = products[i].NAME + "(#" + products[i].ID + ")";
            }
                dataGridInfo.ItemsSource = list;
            dataGridInfo.DataContext = tempMas;
        }
        
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            if(comboBoxEployees.SelectedIndex != -1)
            {
                flag = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите принимающего!");
            }
            if(textBoxAgent.Text != "")
            {
                flag = true;
            }
            else
            {
                flag = false;
                System.Windows.MessageBox.Show("Имя контрагента пусто!");
            }
            if(flag == true)
            {
                string lastId = DataBase.QueryRetCell(null, null, "SELECT MAX(W_ID)+1 FROM waybill;");
                DataBase.Query(
                new string[] { "@_id", "@_date", "@_employee", "@_agent" },
                new string[] { lastId, Converter.DateConvert(datePickerToday.Text), employees[comboBoxEployees.SelectedIndex].ID.ToString(), textBoxAgent.Text },
                "INSERT INTO `waybill`(W_ID,W_DATE,E_ID,W_AGENT_NAME)VALUES(@_id,@_date,@_employee,@_agent);");
                string queryString = "INSERT INTO `waybill_list`(`W_ID`, `P_ID`, `WL_VALUE`, `WL_TRADE_PRICE`, `WL_BDATE`, `WL_EDATE`)VALUES ";
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].TRADEPRICE = Converter.CurrencyConvert(list[i].TRADEPRICE);
                    queryString += "(" + lastId + "," + list[i].ID + ",'" + list[i].VALUE + "','" + list[i].TRADEPRICE + "','" + Converter.DateConvert(list[i].BDATE.ToShortDateString()) + "','" + Converter.DateConvert(list[i].EDATE.ToShortDateString()) + "')";
                    if(i != list.Count-1)
                    {
                        queryString += ",";
                    }
                }
                queryString += ";";
                DataBase.Query(null, null, queryString);
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

        private void comboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox a = (ComboBox)sender;
            if(dataGridInfo.SelectedIndex != -1 && a.SelectedIndex != -1)
            {
                list[dataGridInfo.SelectedIndex].ID = products[a.SelectedIndex].ID.ToString();
            }
        }
    }
}
