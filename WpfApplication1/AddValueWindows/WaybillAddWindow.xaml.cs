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
        List<NameIdList> employees = new List<NameIdList>();
        List<WaybillOutput> list = new List<WaybillOutput>();
        List<NameIdList> products = new List<NameIdList>();
        string[] tempMas;
        string idText;
        int selIndexEmployee = -1, countEmployee = 0;
        List<int[]> comboBoxSelIndex = new List<int[]>();
        public bool flag1 = false;
        public Waybill obj = new Waybill();
        public WaybillAddWindow(string id)
        {
            InitializeComponent();
            datePickerToday.Text = DataBase.QueryRetCell(null,null,"SELECT date(now());");
            ProductListUpdate();
            EmployeeListUpdate();
            countEmployee = comboBoxEployees.Items.Count;
            selIndexEmployee = comboBoxEployees.SelectedIndex;
            dataGridInfo.ItemsSource = list;
            idText = id;
        }
        
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if(ErrorCheck.WaybillEnterCheck(comboBoxEployees,textBoxAgent,datePickerToday) && products.Count > 0)
            {
                string lastId = DataBase.QueryRetCell(null, null, "SELECT IFNULL(MAX(W_ID)+1,1) FROM waybill;");
                obj.ID = int.Parse(lastId);
                obj.EMPLOYEE = employees[comboBoxEployees.SelectedIndex].NAME;
                obj.AGENT = textBoxAgent.Text;
                obj.DATE = datePickerToday.SelectedDate.Value;
                string[][] quantityData = new string[list.Count][];
                string queryString = "INSERT INTO `waybill_list`(`W_ID`, `P_ID`, `WL_VALUE`, `WL_TRADE_PRICE`, `WL_BDATE`, `WL_EDATE`)VALUES ";
                bool checkForErr = false;
                if (list.Count > 0 && comboBoxSelIndex.Count > 0 ) 
                {
                    for (int i = 0; i < comboBoxSelIndex.Count; i++)
                    {
                        if (list[i].ID == "NULL")
                        {
                            checkForErr = true;
                            i = list.Count;
                        }
                        else
                        {
                            if (list[i].VALUE == "")
                            {
                                checkForErr = true;
                                i = list.Count;
                            }
                            else
                            {
                                if (list[i].TRADEPRICE == "")
                                {
                                    checkForErr = true;
                                    i = list.Count;
                                }
                                else
                                {
                                    quantityData[i] = new string[] { list[i].ID, list[i].VALUE, list[i].TRADEPRICE, Converter.DateConvert(list[i].BDATE.ToShortDateString()) };
                                    list[i].TRADEPRICE = Converter.CurrencyConvert(list[i].TRADEPRICE);
                                    queryString += "(" + lastId + "," + list[i].ID + ",'" + list[i].VALUE + "','" + list[i].TRADEPRICE + "','" + Converter.DateConvert(list[i].BDATE.ToShortDateString()) + "','" + Converter.DateConvert(list[i].EDATE.ToShortDateString()) + "')";
                                    if (i != list.Count - 1)
                                    {
                                        queryString += ",";
                                    }
                                }
                            }
                        }
                    }
                    if (!checkForErr)
                    {
                        queryString += ";";
                        DataBase.Query(
                        new string[] { "@_id", "@_date", "@_employee", "@_agent" },
                        new string[] { lastId, Converter.DateConvert(datePickerToday.Text), employees[comboBoxEployees.SelectedIndex].ID.ToString(), textBoxAgent.Text },
                        "INSERT INTO `waybill`(W_ID,W_DATE,E_ID,W_AGENT_NAME)VALUES(@_id,@_date,@_employee,@_agent);");
                        DataBase.Query(null, null, queryString);
                        string[] waybillListIds = DataBase.QueryRetColumn(new string[] { "@_id" }, new string[] { lastId }, "SELECT WL_ID FROM waybill,waybill_list WHERE waybill.W_ID=waybill_list.W_ID AND waybill.W_ID=@_id;");
                        queryString = "(WL_ID)VALUES ";
                        for (int i = 0; i < waybillListIds.Length; i++)
                        {
                            queryString += "(" + waybillListIds[i] + ")";
                            if (i != waybillListIds.Length - 1)
                            {
                                queryString += ",";
                            }
                        }
                        queryString += ";";
                        DataBase.Query(null, null, "INSERT INTO product_sold" + queryString);
                        DataBase.Query(null, null, "INSERT INTO product_overdue" + queryString);
                        for (int i = 0; i < quantityData.Length; ++i)
                        {
                            DataBase.Query(new string[] { "@_id", "@_value" }, new string[] { quantityData[i][0], quantityData[i][1] }, "UPDATE product_quantity SET PQ_IN=PQ_IN+@_value WHERE P_ID=@_id;");
                            DataBase.Query(new string[] { "@_id", "@_price", "@_dat" }, new string[] { quantityData[i][0], Converter.FloatToCurrencyConvert((Converter.CurrencyToFloatConvert(quantityData[i][2]) * (1 - float.Parse(Properties.Settings.Default.PriceProcent) * 0.01)).ToString()), quantityData[i][3] }, "CALL insert_price(@_id,@_price,@_dat);");
                        }
                        DataBase.Query(null, null, "UPDATE product_overdue po,waybill_list wl SET po.PP_IS_OVERDUE=IF((wl.WL_EDATE-14)>DATE(NOW())-0,IF((SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=0,'Продано','Не просрочено'),IF((WL_EDATE)<DATE(NOW()),IF((SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=0,'Продано','Просрочено'),'Скоро истекает срок годности'))WHERE po.PP_IS_OVERDUE<>'Просрочено' AND po.PP_IS_OVERDUE<>'Продано' AND po.WL_ID=wl.WL_ID AND po.PP_ID>0 AND wl.WL_ID>0;");
                        DataBase.SetLog(idText, 1, 2, "Создание накладной,параметры:|код:" + lastId + "|дата:" + Converter.DateConvert(datePickerToday.Text) + "|");
                        flag1 = true;
                        this.Close();
                    }
                }
            }
        }

        private void comboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridInfo.SelectedIndex != -1 && (sender as ComboBox).SelectedIndex != -1 && (sender as ComboBox).SelectedIndex != (sender as ComboBox).Items.Count)
            {
                list[dataGridInfo.SelectedIndex].ID = products[(sender as ComboBox).SelectedIndex].ID.ToString();
            }
            for (int i = 0; i < comboBoxSelIndex.Count; i++)
            {
                if (comboBoxSelIndex[i][0] == dataGridInfo.SelectedIndex)
                {
                    comboBoxSelIndex[i][1] =
                        (sender as ComboBox).SelectedIndex;
                }
            }
        }

        private void EmployeeListUpdate()
        {
            employees.Clear();
            employees = DataBase.GetNameIdList(new string[] { "E_ID", "E_NAME" }, "SELECT E_ID,E_NAME FROM employee;");
            comboBoxEployees.Items.Clear();
            for (int i = 0; i < employees.Count; i++)
            {
                comboBoxEployees.Items.Add(employees[i].NAME + "(#" + employees[i].ID + ")");
            }
            if (comboBoxEployees.Items.Count > countEmployee)
            {
                comboBoxEployees.SelectedIndex = comboBoxEployees.Items.Count;
                countEmployee = comboBoxEployees.Items.Count;
            }
            else
            {
                comboBoxEployees.SelectedIndex = selIndexEmployee;
            }
        }

        private void ProductListUpdate()
        {
            products.Clear();
            products = DataBase.GetNameIdList(new string[] { "P_ID", "P_NAME" }, "SELECT P_ID,P_NAME FROM product;");
            tempMas = new string[products.Count];
            for (int i = 0; i < products.Count; i++)
            {
                tempMas[i] = products[i].NAME + "(#" + products[i].ID + ")";
            }
            dataGridInfo.DataContext = tempMas;
        }

        private void ProductAdd_Click(object sender, RoutedEventArgs e)
        {
            new ProductAddWindow(idText).ShowDialog();
            ProductListUpdate();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            selIndexEmployee = comboBoxEployees.SelectedIndex;
            new EmployeeAddWindow(idText).ShowDialog();
            EmployeeListUpdate();
        }

        private void comboBoxProduct_Loaded(object sender, RoutedEventArgs e)
        {
            bool flagTemp = false;
            int index = -1;
            for (int i = 0; i < comboBoxSelIndex.Count;i++ )
            {
                if(comboBoxSelIndex[i][0] == dataGridInfo.SelectedIndex)
                {
                    flagTemp = true;
                    index = i;
                }
            }
            if (!flagTemp)
            {
                comboBoxSelIndex.Add(new int[] { dataGridInfo.SelectedIndex, -1 });
            }
            else
            {
                if ((sender as ComboBox).Items.Count > 0)
                {
                    (sender as ComboBox).SelectedIndex = comboBoxSelIndex[index][1];
                }
            }
        }

        private void comboBoxEployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxEployees.BorderBrush = ErrorCheck.SelectionCheck(comboBoxEployees.SelectedIndex, comboBoxEployees.Items.Count);
        }

        private void textBoxAgent_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red);
        }
    }
}
