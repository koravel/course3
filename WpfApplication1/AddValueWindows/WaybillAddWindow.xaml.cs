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
        public WaybillAddWindow()
        {
            InitializeComponent();
            datePickerToday.Text = DateTime.Today.ToString();
            for (int i = 0; i < employees.Count; i++)
            {
                comboBoxEployees.Items.Add(employees[i].NAME+"(#"+employees[i].ID+")");
            }
            dataGridInfo.ItemsSource = new List<WaybillOutput> { new WaybillOutput() };
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string lastId = DataBase.QueryRetCell(null, null, "SELECT MAX(W_ID)+1 FROM waybill;");
            DataBase.Query(
            new string[] { "@_id", "@_date", "@_employee", "@_agent" },
            new string[] { lastId, Converter.DateConvert(datePickerToday.Text), employees[comboBoxEployees.SelectedIndex].ID.ToString(), textBoxAgent.Text },
            "INSERT INTO `waybill`(W_ID,W_DATE,E_ID,W_AGENT_NAME)VALUES(@_id,@_date,@_employee,@_agent);");
            List<WaybillOutput> productsOutput = new List<WaybillOutput>();
            for (int i = 0; i < dataGridInfo.Items.Count;i++ )
            {
                productsOutput[i] = (WaybillOutput)dataGridInfo.Items[dataGridInfo.SelectedIndex];
            }
                //DataBase.Query(
                //new string[] { },
                //new string[] { },
                //"INSERT INTO `waybill_list`()VALUES()");
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
