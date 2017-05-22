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
using MySql.Data.MySqlClient;
using System.Data;
using Xceed.Wpf.Toolkit;
using System.ComponentModel;

namespace WpfApplication1
{

    public partial class AdminWindow : Window
    {
        static string idText,temp;
        bool flag;
        List<CheckList> checkList = new List<CheckList> { };
        List<WaybillList> waybillList = new List<WaybillList> { };
        List<string> valuesText = new List<string>(), values = new List<string>();
        private delegate List<T> GetTableDelegate<T>(string query, string[] values, string[] valuesText);
        public AdminWindow(string id)
        {
            idText = id;
            InitializeComponent();
            dataGridCheckOut.ItemsSource = DataBase.GetCheck();
            DataBase.SetLog(idText, 0, 0, DateTime.Now, "Заполнение таблицы чеков...");
            dataGridDiscountOut.ItemsSource = DataBase.GetDiscount();
            DataBase.SetLog(idText, 0, 0, DateTime.Now, "Заполнение таблицы скидок...");
            dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer();
            DataBase.SetLog(idText, 0, 0, DateTime.Now, "Заполнение таблицы производителей...");
            dataGridProductOut.ItemsSource = DataBase.GetProduct();
            DataBase.SetLog(idText, 0, 0, DateTime.Now, "Заполнение таблицы товаров...");
            dataGridEmployeeOut.ItemsSource = DataBase.GetEmployee();
            DataBase.SetLog(idText, 0, 0, DateTime.Now, "Заполнение таблицы работников...");
            dataGridWaybillOut.ItemsSource = DataBase.GetWaybill();
            DataBase.SetLog(idText, 0, 0, DateTime.Now, "Заполнение таблицы накладных...");
            valuesText.Clear();
            values.Clear();
        }

        private void UsersControl_Click(object sender, RoutedEventArgs e)
        {
            new UsersControlWindow().ShowDialog();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch(selected)
            {
                case 0:
                    {
                        dataGridCheckOut.ItemsSource = DataBase.GetCheck();
                        if (dataGridCheckOut.SelectedIndex != -1)
                        {
                            checkList = DataBase.GetCheckList(Converter.DGCellToStringConvert(dataGridCheckOut.SelectedIndex, 0, dataGridCheckOut));
                            dataGridCheckListOut.ItemsSource = checkList;
                        }
                        break;
                    }
                case 1:
                    {
                        dataGridDiscountOut.ItemsSource = DataBase.GetDiscount();
                        break;
                    }
                case 2:
                    {
                        dataGridEmployeeOut.ItemsSource = DataBase.GetEmployee();
                        break;
                    }
                case 3:
                    {
                        dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer();
                        break;
                    }
                case 4:
                    {
                        dataGridProductOut.ItemsSource = DataBase.GetProduct();
                        if (dataGridProductOut.SelectedIndex != -1)
                        {
                            dataGridProductActPriceOut.ItemsSource = DataBase.GetProductActualPrice(Converter.DGCellToStringConvert(dataGridProductOut.SelectedIndex, 0, dataGridProductOut));
                        }
                        break;
                    }
                case 5:
                    {
                        dataGridWaybillOut.ItemsSource = DataBase.GetWaybill();
                        if (dataGridWaybillOut.SelectedIndex != -1)
                        {
                            dataGridWaybillListOut.ItemsSource = DataBase.GetWaybillList(Converter.DGCellToStringConvert(dataGridWaybillOut.SelectedIndex, 0, dataGridWaybillOut));
                        }
                        break;
                    }
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void adminLogout_Closing(object sender, CancelEventArgs e)
        {
            new MainWindow().Show();
        }

        private void dataGridCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCheckOut.SelectedIndex != -1)
            {
                string checkId = Converter.DGCellToStringConvert(dataGridCheckOut.SelectedIndex, 0, dataGridCheckOut);
                checkList = DataBase.GetCheckList(checkId);
                dataGridCheckListOut.ItemsSource = checkList;
                //string totalPrice = DataBase.QueryRetCell(new string[] { "@_curid" }, new string[] { content.Text }, "SELECT IF((SELECT COUNT(discounts.D_ID) FROM discounts,check_list cl,`check` c WHERE cl.C_ID=@_curid AND discounts.P_ID=cl.P_ID AND c.C_ID=cl.C_ID AND c.C_DATE>=discounts.D_BDATE AND c.C_DATE<=discounts.D_EDATE)>0,(SELECT (SELECT SUM(pap.PAP_PRICE*cl.CL_VALUE*(1-d.D_PRICE*0.01)) FROM product_actual_price pap,check_list cl WHERE pap.P_ID=cl.P_ID AND d.P_ID=cl.P_ID AND cl.C_ID=c.C_ID AND pap.PAP_DATE=(SELECT PAP_DATE FROM product_actual_price WHERE product_actual_price.P_ID=cl.P_ID ORDER BY 1 DESC LIMIT 1)) FROM `check` c,discounts d WHERE c.C_DATE>=d.D_BDATE AND c.C_DATE<=d.D_EDATE AND c.C_ID=@_curid),(SELECT (SELECT SUM(cl.CL_VALUE*product_actual_price.PAP_PRICE) FROM check_list cl,product_actual_price WHERE cl.C_ID=c.C_ID AND cl.P_ID=product_actual_price.P_ID AND product_actual_price.PAP_ID=(SELECT PAP_ID FROM product_actual_price WHERE P_ID=cl.P_ID ORDER BY PAP_DATE DESC LIMIT 1)) FROM `check` c WHERE c.C_ID=@_curid));");
                string totalPrice = DataBase.QueryRetCell(new string[] { "@_curid" }, new string[] { checkId }, "SELECT C_SUM FROM `check` where C_ID=@_curid;");
                totalPriceTextBlock.Text = "Общая цена:";
                if(totalPrice == null)
                {
                    totalPriceTextBlock.Text += "0";
                }
                else
                {
                    totalPriceTextBlock.Text += totalPrice;
                }
            }
        }

        private void dataGridProductOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridProductOut.SelectedIndex != -1)
                {
                    string productId = Converter.DGCellToStringConvert(dataGridProductOut.SelectedIndex, 0, dataGridProductOut);
                    dataGridProductActPriceOut.ItemsSource = DataBase.GetProductActualPrice(productId);
                    DataBase.Query(new string[] { "@_id" }, new string[] { productId }, "UPDATE product_overdue po,waybill_list wl SET po.PP_IS_OVERDUE=IF((wl.WL_EDATE-14)>DATE(NOW())-0,IF((SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=0,'Продано','Не просрочено'),IF((WL_EDATE)<DATE(NOW()),IF((SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=0,'Продано','Просрочено'),'Скоро истекает срок годности'))WHERE po.PP_IS_OVERDUE<>'Просрочено' AND po.PP_IS_OVERDUE<>'Продано' AND po.WL_ID=wl.WL_ID AND wl.P_ID=@_id AND wl.WL_ID>0;");
                    string quantity = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { productId }, "SELECT SUM(waybill_list.WL_VALUE-product_sold.PS_COUNT) FROM product_sold,waybill_list WHERE product_sold.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=@_id;"),
                    overdueValue = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { productId }, "SELECT waybill_list.WL_VALUE-product_sold.PS_COUNT FROM waybill_list,product_sold,product_overdue WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_sold.WL_ID=waybill_list.WL_ID AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=@_id;"),
                    actualValue = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { productId }, "SELECT IF(COUNT(waybill_list.WL_VALUE-product_sold.PS_COUNT)>0,waybill_list.WL_VALUE-product_sold.PS_COUNT,0) FROM product_sold,product_overdue,waybill_list WHERE product_sold.WL_ID=product_overdue.WL_ID AND product_overdue.PP_IS_OVERDUE<>'Просрочено' AND product_overdue.PP_IS_OVERDUE<>'Продано' AND product_sold.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=@_id;");
                    textBlockProductCount.Text = "Всего на складе:";
                    if(quantity == null)
                    {
                        textBlockProductCount.Text += "0";
                    }
                    else
                    {
                        textBlockProductCount.Text += quantity;
                    }
                    textBlockProductCount.Text += "\nПросрочено:";
                    if (overdueValue == null)
                    {
                        textBlockProductCount.Text += "0";
                    }
                    else
                    {
                        textBlockProductCount.Text += overdueValue;
                    }
                    textBlockProductCount.Text += "\nНе просрочено:";
                    if (actualValue == null)
                    {
                        textBlockProductCount.Text += "0";
                    }
                    else
                    {
                        textBlockProductCount.Text += actualValue;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void dataGridWaybillOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridWaybillOut.SelectedIndex != -1)
                {
                    string waybillId = Converter.DGCellToStringConvert(dataGridWaybillOut.SelectedIndex, 0, dataGridWaybillOut);
                    waybillList = DataBase.GetWaybillList(waybillId);
                    dataGridWaybillListOut.ItemsSource = waybillList;
                    string totalTradePrice = DataBase.QueryRetCell(new string[] { "@_curid" }, new string[] { waybillId }, "SELECT SUM(waybill_list.WL_TRADE_PRICE*waybill_list.WL_VALUE) FROM waybill_list WHERE waybill_list.W_ID=@_curid;");
                    totalWaybillPriceTextBlock.Text = "Общая цена:";
                    if(totalTradePrice == null)
                    {
                        totalWaybillPriceTextBlock.Text += "0";
                    }
                    else
                    {
                        totalWaybillPriceTextBlock.Text += totalTradePrice;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch (selected)
            {
                case 0:
                    {
                        System.Windows.MessageBox.Show("Добавление запрещено.");
                        break;
                    }
                case 1:
                    {
                        new DiscountAddWindow().ShowDialog();
                        break;
                    }
                case 2:
                    {
                        new EmployeeAddWindow().ShowDialog();
                        break;
                    }
                case 3:
                    {
                        new ManufacturerAddWindow().ShowDialog();
                        break;
                    }
                case 4:
                    {
                        new ProductAddWindow().ShowDialog();
                        break;
                    }
                case 5:
                    {
                        new WaybillAddWindow().ShowDialog();
                        break;
                    }
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch (selected)
            {
                case 0: case 5:
                    {
                        System.Windows.MessageBox.Show("Удаление запрещено.");
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            if (dataGridDiscountOut.SelectedIndex != -1)
                            {
                                string discountId = Converter.DGCellToStringConvert(dataGridDiscountOut.SelectedIndex, 0, dataGridDiscountOut);
                                DataBase.Query(new string[] { "@_curid" }, new string[] { discountId }, "DELETE FROM `discounts` WHERE D_ID=@_curid;");
                                DataBase.SetLog(idText, 1, 3, DateTime.Now, "Удаление акции,код=" + discountId);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            if (dataGridEmployeeOut.SelectedIndex != -1)
                            {
                                string employeeId = Converter.DGCellToStringConvert(dataGridEmployeeOut.SelectedIndex, 0, dataGridEmployeeOut);
                                if(Properties.Settings.Default.DelBindingToEmployee == false)
                                {
                                    new WarningDelEmployeeBindsWindow(employeeId).ShowDialog();
                                }
                                else
                                {
                                    DataBase.Query(new string[] { "@_curid" }, new string[] { employeeId }, "DELETE FROM `employee` WHERE E_ID=@_curid;");
                                    DataBase.SetLog(idText, 1, 3, DateTime.Now, "Удаление работника,код=" + employeeId);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 3:
                    {
                        try
                        {
                            if (dataGridManufacturersOut.SelectedIndex != -1)
                            {
                                string maufacturerId = Converter.DGCellToStringConvert(dataGridManufacturersOut.SelectedIndex, 0, dataGridManufacturersOut);
                                if (Properties.Settings.Default.DelBindingToManufacturer == false)
                                {
                                    new WarningDelManufacturerBindsWindow(maufacturerId).ShowDialog();
                                }
                                else
                                {
                                    DataBase.Query(new string[] { "@_curid" }, new string[] { maufacturerId }, "DELETE FROM `manufacturer` WHERE M_ID=@_curid;");
                                    DataBase.SetLog(idText, 1, 3, DateTime.Now, "Удаление производителя,код=" + maufacturerId);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 4:
                    {
                        try
                        {
                            if (dataGridProductOut.SelectedIndex != -1)
                            {
                                string productId = Converter.DGCellToStringConvert(dataGridProductOut.SelectedIndex, 0, dataGridProductOut);
                                if (Properties.Settings.Default.DelBindingToProduct == false)
                                {
                                    new WarningDelProductBindsWindow(productId).ShowDialog();
                                }
                                else
                                {
                                    DataBase.Query(new string[] { "@_curid" }, new string[] { productId }, "DELETE FROM `product` WHERE P_ID=@_curid;");
                                    DataBase.SetLog(idText, 1, 3, DateTime.Now, "Удаление товара,код=" + productId);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                        break;
                    }
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch(selected)
            {
                case 0: case 5:
                    {
                        System.Windows.MessageBox.Show("Изменение запрещено.");
                        break;
                    }
                case 1:
                    {
                        if (dataGridDiscountOut.SelectedIndex != -1)
                        {
                            new DiscountEditWindow(Converter.DGCellToStringConvert(dataGridDiscountOut.SelectedIndex, 0, dataGridDiscountOut)).ShowDialog();
                        }
                        break;
                    }
                case 2:
                    {
                        if (dataGridEmployeeOut.SelectedIndex != -1)
                        {
                            new EmployeeEditWindow(Converter.DGCellToStringConvert(dataGridEmployeeOut.SelectedIndex, 0, dataGridEmployeeOut)).ShowDialog();
                        }
                        break;
                    }
                case 3:
                    {
                        if (dataGridManufacturersOut.SelectedIndex != -1)
                        {
                            new ManufacturerEditWindow(Converter.DGCellToStringConvert(dataGridManufacturersOut.SelectedIndex, 0, dataGridManufacturersOut)).ShowDialog();
                        }
                        break;
                    }
                case 4:
                    {
                        if (dataGridProductOut.SelectedIndex != -1)
                        {
                            new ProductEditWindow(Converter.DGCellToStringConvert(dataGridProductOut.SelectedIndex, 0, dataGridProductOut)).ShowDialog();
                        }
                        break;
                    }
            }
        }

        private void reportsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ReportsWindow().ShowDialog();
        }

        private void dataGridEmployeeOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGridEmployeeOut.SelectedIndex != -1)
            {
                string professionName = Converter.DGCellToStringConvert(dataGridEmployeeOut.SelectedIndex, 3, dataGridEmployeeOut);
                if (professionName != "Уборщик")
                {
                    textBlockTypeCount.Text = professionName + "ы:";
                }
                else
                {
                    textBlockTypeCount.Text = professionName + "и:";
                }
                textBlockTypeCount.Text += DataBase.QueryRetCell(new string[] { "@_pos" }, new string[] { professionName }, "SELECT COUNT(E_ID) FROM employee WHERE E_POSITION=@_pos;");
            }
        }

        private void menuItemViewSettings_Click(object sender, RoutedEventArgs e)
        {
            new ViewSettingsWindow().ShowDialog();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            temp = null;
            flag = false;
            switch(tabControlSearch.SelectedIndex)
            {
                case 0:
                    {
                          SQLParameterAdd(checkBoxSearchEmployeeCheck.IsChecked.Value, new string[] { " AND employee.E_NAME=@_employee", "@_employee", textBoxSearchEmployeeCheck.Text }, ref temp, ref flag, new bool[] { false, false });

                        RangeComparsionGet(new bool[] { checkBoxSearchBDateCheck.IsChecked.Value, checkBoxSearchEDateCheck.IsChecked.Value }, comboBoxSearchDateRangeTypeCheck.SelectedIndex, new string[] { 
                                "AND `check`.C_DATE>=@_bdate","AND `check`.C_DATE<=@_edate"," AND `check`.C_DATE BETWEEN @_bdate AND @_edate"," AND (`check`.C_DATE>@_edate OR `check`.C_DATE<@_bdate)"},
                            new string[] { Converter.DateConvert(datePickerSearchBDateCheck.Text), Converter.DateConvert(datePickerSearchEDateCheck.Text) }, new string[] { "@_bdate", "@_edate" }, ref temp, ref flag);

                        RangeComparsionGet(new bool[] { checkBoxSearchBHours.IsChecked.Value, checkBoxSearchEHours.IsChecked.Value }, 0, new string[] { 
                                " AND HOUR(`check`.C_DATE)>=@_bhour"," AND HOUR(`check`.C_DATE)<=@_ehour"," AND HOUR(`check`.C_DATE) BETWEEN @_bhour AND @_ehour"},
                            new string[] { upDownSearchBHours.Text, upDownSearchEHours.Text }, new string[] { "@_bhour", "@_ehour" }, ref temp, ref flag);

                        RangeComparsionGet(new bool[] { checkBoxSearchBMinutes.IsChecked.Value, checkBoxSearchEMinutes.IsChecked.Value }, 0, new string[] { 
                                " AND MINUTE(`check`.C_DATE)>=@_bminute"," AND MINUTE(`check`.C_DATE)<=@_eminute"," AND MINUTE(`check`.C_DATE) BETWEEN @_bminute AND @_eminute"},
                            new string[] { upDownSearchBMinutes.Text, upDownSearchEMinutes.Text }, new string[] { "@_bminute", "@_eminute" }, ref temp, ref flag);

                        RangeComparsionGet(new bool[] { checkBoxSearchBSeconds.IsChecked.Value, checkBoxSearchESeconds.IsChecked.Value }, 0, new string[] { 
                                " AND SECOND(`check`.C_DATE)>=@_bsecond"," AND SECOND(`check`.C_DATE)<=@_esecond"," AND SECOND(`check`.C_DATE) BETWEEN @_bsecond AND @_esecond"},
                            new string[] { upDownSearchBSeconds.Text, upDownSearchESeconds.Text }, new string[] { "@_bsecond", "@_esecond" }, ref temp, ref flag);

                        if (!(checkBoxSearchPaytypeCash.IsChecked == true && checkBoxSearchPaytypeCard.IsChecked == true))
                        {
                            if (checkBoxSearchPaytypeCash.IsChecked == true)
                            {
                                temp += " AND `check`.C_PAYTYPE='Наличные'";
                            }
                            else
                            {
                                if (checkBoxSearchPaytypeCard.IsChecked == true)
                                {
                                    temp += " AND `check`.C_PAYTYPE='Карточка'";
                                }
                            }
                        }

                        SQLParameterAdd(checkBoxSearchProductCheck.IsChecked.Value, new string[] { " AND product.P_NAME=@_product", "@_product", textBoxSearchProductCheck.Text }, ref temp, ref flag, new bool[] { true, false });

                        SQLParameterAdd(checkBoxSearchProductIdCheck.IsChecked.Value, new string[] { " AND product.P_ID=@_pid", "@_pid", textBoxSearchProductIdCheck.Text }, ref temp, ref flag, new bool[] { true, false });

                        ComparsionGet(checkBoxSearchPriceCheck.IsChecked.Value, new int[] { comboBoxDirectionSearchPrice.SelectedIndex, 0 },
                            new string[] { " AND product_actual_price.PAP_PRICE" },
                            new string[] { null, Converter.CurrencyConvert(upDownSearchPriceCheck.Text) }, ref temp, ref flag);

                        ComparsionGet(checkBoxSearchValueCheck.IsChecked.Value, new int[] { comboBoxDirectionSearchValue.SelectedIndex, 0 },
                             new string[] { " AND check_list.CL_VALUE" },
                             new string[] { "@_value", upDownSearchValueCheck.Text }, ref temp, ref flag);

                        SQLParameterAdd(checkBoxSearchCheckCode.IsChecked.Value, new string[] { " AND `check`.C_ID=@_id", "@_id", textBoxSearchCheckCode.Text }, ref temp, ref flag, new bool[] { false, false });

                        MakeSearch(dataGridCheckOut, DataBase.GetCheck, new string[] { 
                            "SELECT DISTINCT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee`,check_list,product,product_actual_price WHERE `check`.`E_ID`=`employee`.`E_ID` AND `check`.C_ID=check_list.C_ID AND check_list.P_ID=product.P_ID AND check_list.P_ID=product_actual_price.P_ID ",
                            "SELECT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee` WHERE `check`.`E_ID`=`employee`.`E_ID` " }, temp, flag);
                        break;
                    }
                case 1:
                    {
                        SQLParameterAdd(checkBoxSearchProductDiscount.IsChecked.Value, new string[] { " AND discounts.P_ID=@_product", "@_product", textBoxSearchProductDiscount.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchProductIdDiscount.IsChecked.Value, new string[] { " AND discounts.P_ID=@_product", "@_product", textBoxSearchProductIdDiscount.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchProcDiscount.IsChecked.Value, new string[] { " AND discounts.D_PRICE=@_proc", "@_proc", upDownSearchProcDiscount.Text }, ref temp, ref flag, new bool[] { false, false });

                        RangeComparsionGet(new bool[] { checkBoxSearchBDateDiscount.IsChecked.Value, checkBoxSearchEDateDiscount.IsChecked.Value }, comboBoxSearchDateRangeType.SelectedIndex, new string[] { 
                                " AND discounts.D_BDATE>=@_bdate", " AND discounts.D_EDATE<=@_edate", 
                                " AND discounts.D_BDATE=@_bdate AND discounts.D_EDATE=@_edate", " AND discounts.D_BDATE>=@_bdate AND discounts.D_EDATE<=@_edate", 
                                " AND ((discounts.D_BDATE<=@_bdate AND discounts.D_EDATE<=@_bdate) OR (discounts.D_BDATE>=@_edate AND discounts.D_EDATE>=@_edate))", 
                                " AND ((discounts.D_BDATE>=@_bdate AND discounts.D_EDATE>=@_edate AND discounts.D_BDATE<=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE>=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE<=@_edate AND discounts.D_EDATE>=@_bdate))",
                                " AND ((discounts.D_BDATE>=@_bdate AND discounts.D_EDATE>=@_edate AND discounts.D_BDATE<=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE>=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE<=@_edate AND discounts.D_EDATE>=@_bdate) OR (discounts.D_BDATE>=@_bdate AND discounts.D_EDATE<=@_edate))" },
                            new string[] { Converter.DateConvert(datePickerSearchBDateDiscount.Text), Converter.DateConvert(datePickerSearchEDateDiscount.Text) }, new string[] { "@_bdate", "@_edate" }, ref temp, ref flag);

                        SQLParameterAdd(checkBoxSearchCodeDiscount.IsChecked.Value, new string[] { " AND discounts.D_ID=@_id", "@_id", textBoxSearchCodeDiscount.Text }, ref temp, ref flag, new bool[] { false, false });

                        MakeSearch(dataGridDiscountOut, DataBase.GetDiscount, new string[] { "SELECT discounts.D_ID,product.P_NAME,discounts.D_PRICE,discounts.D_BDATE,discounts.D_EDATE,discounts.D_TEXT FROM `discounts`,`product` WHERE `discounts`.`P_ID`=`product`.`P_ID`", null }, temp, true);
                        break;
                    }
                case 2:
                    {
                        SQLParameterAdd(checkBoxSearchPosEmployee.IsChecked.Value, new string[] { "WHERE E_NAME=@_name", "@_name", textBoxSearchNameEmployee.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchPosEmployee.IsChecked.Value, new string[] { " E_POSITION=@_pos", "@_pos", comboBoxSearchPosEmployee.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchContractEmployee.IsChecked.Value, new string[] { " E_CONTRACT=@_contr", "@_contr", textBoxSearchContractEmployee.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchTelEmployee.IsChecked.Value, new string[] { " E_TEL=@_tel", "@_tel", textBoxSearchTelEmployee.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchCodeEmployee.IsChecked.Value, new string[] { " E_ID=@_code", "@_code", textBoxSearchCodeEmployee.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchINNEmployee.IsChecked.Value, new string[] { " E_INN=@_inncode", "@_inncode", textBoxSearchINNEmployee.Text }, ref temp, ref flag, new bool[] { false, true });

                        MakeSearch(dataGridEmployeeOut, DataBase.GetEmployee, new string[] { "SELECT E_ID,E_NAME,E_TEL,E_POSITION,E_CONTRACT,E_INN FROM `employee` ", null }, temp, true);
                        break;
                    }
                case 3:
                    {
                        SQLParameterAdd(checkBoxSearchNameManufacturer.IsChecked.Value, new string[] { "WHERE M_NAME=@_name", "@_name", textBoxSearchNameManufacturer.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchCountryManufacturer.IsChecked.Value, new string[] { " M_COUNTRY=@_country", "@_country", textBoxSearchCountryManufacturer.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchAddressManufacturer.IsChecked.Value, new string[] { " M_CITY=@_city", "@_city", textBoxSearchCityManufacturer.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchAddressManufacturer.IsChecked.Value, new string[] { " M_ADDR=@_address", "@_address", textBoxSearchAddressManufacturer.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchTelManufacturer.IsChecked.Value, new string[] { " M_TEL=@_tel", "@_tel", textBoxSearchTelManufacturer.Text }, ref temp, ref flag, new bool[] { false, true });

                        SQLParameterAdd(checkBoxSearchCodeManufacturer.IsChecked.Value, new string[] { " M_ID=@_code", "@_code", textBoxSearchCodeManufacturer.Text }, ref temp, ref flag, new bool[] { false, true });

                        MakeSearch(dataGridManufacturersOut, DataBase.GetManufacturer, new string[] { "SELECT M_ID,M_NAME,M_COUNTRY,M_CITY,M_ADDR,M_TEL FROM manufacturer ", null }, temp, true);
                    break;
                    }
                case 4:
                    {
                        SQLParameterAdd(checkBoxSearchNameProduct.IsChecked.Value, new string[] { " AND p.P_NAME=@_name", "@_name", textBoxSearchNameProduct.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchManufacturerProduct.IsChecked.Value, new string[] { " AND manufacturer.M_NAME=@_mname", "@_mname", textBoxSearchManufacturerProduct.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchGroupProduct.IsChecked.Value, new string[] { " AND p.P_GROUP=@_group", "@_group", comboBoxSearchGroupProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchPackProduct.IsChecked.Value, new string[] { " AND p.P_PACK=@_pack", "@_pack", comboBoxSearchPackProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchMaterialProduct.IsChecked.Value, new string[] { " AND p.P_MATERIAL=@_material", "@_material", comboBoxSearchMaterialProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchFormProduct.IsChecked.Value, new string[] { " AND p.P_FORM=@_form", "@_form", comboBoxSearchFormProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });

                        ComparsionGet(checkBoxSearchPriceProduct.IsChecked.Value, new int[] { comboBoxSearchPriceRangeProduct.SelectedIndex, 0 },
                              new string[] { " AND product_actual_price.PAP_PRICE" },
                              new string[] { null, Converter.CurrencyConvert(upDownSearchPriceProduct.Text) }, ref temp, ref flag);

                        ComparsionGet(checkBoxSearchValueProduct.IsChecked.Value, new int[] { comboBoxDirectionSearchValueProduct.SelectedIndex, comboBoxSearchTypeCountProduct.SelectedIndex },
                            new string[] { " AND (SELECT SUM(waybill_list.WL_VALUE-product_sold.PS_COUNT) FROM waybill_list,product_sold WHERE waybill_list.P_ID=p.P_ID AND waybill_list.WL_ID=product_sold.WL_ID)",
                            " AND (SELECT waybill_list.WL_VALUE-product_sold.PS_COUNT FROM waybill_list,product_sold,product_overdue WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_sold.WL_ID=waybill_list.WL_ID AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=p.P_ID)",
                            " AND (SELECT IF(COUNT(waybill_list.WL_VALUE-product_sold.PS_COUNT)>0,waybill_list.WL_VALUE-product_sold.PS_COUNT,0) FROM product_sold,product_overdue,waybill_list WHERE product_sold.WL_ID=product_overdue.WL_ID AND product_overdue.PP_IS_OVERDUE<>'Просрочено' AND product_overdue.PP_IS_OVERDUE<>'Продано' AND product_sold.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=p.P_ID)" },
                            new string[] { "@valueq", upDownSearchValueProduct.Text }, ref temp, ref flag);

                        SQLParameterAdd(checkBoxSearchCodeProduct.IsChecked.Value, new string[] { " AND product.P_ID=@_code", "@_code", textBoxSearchCodeProduct.Text }, ref temp, ref flag, new bool[] { false, false });

                        MakeSearch(
                            dataGridProductOut, DataBase.GetProduct, 
                            new string[] { "SELECT p.P_ID,p.P_NAME,manufacturer.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,p.P_INSTR FROM `product` p,`manufacturer`,product_actual_price WHERE `manufacturer`.`M_ID`=p.`M_ID` AND p.P_ID=product_actual_price.P_ID ",
                                "SELECT p.P_ID,p.P_NAME,manufacturer.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,p.P_INSTR FROM `product` p,`manufacturer` WHERE `manufacturer`.`M_ID`=p.`M_ID` " },
                                temp, flag);
                        break;
                    }
                case 5:
                    {
                        RangeComparsionGet(new bool[] { checkBoxSearchBDateWaybill.IsChecked.Value, checkBoxSearchEDateWaybill.IsChecked.Value },
                            comboBoxSearchDateRangeTypeWaybill.SelectedIndex,
                            new string[] { " AND w.W_DATE>=@bdate", " AND w.W_DATE<=@edate", " AND w.W_DATE BETWEEN @bdate AND @edate", " AND (w.W_DATE>@edate OR w.W_DATE<@bdate)" },
                            new string[] { Converter.DateConvert(datePickerSearchBDateWaybill.Text), Converter.DateConvert(datePickerSearchEDateWaybill.Text) },
                            new string[] { "@bdate", "@edate" }, ref temp, ref flag);

                        SQLParameterAdd(checkBoxSearchEmployeeWaybill.IsChecked.Value, new string[] { " AND e.E_NAME=@_name", "@_name", textBoxSearchEmployeeWaybill.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchAgentWaybill.IsChecked.Value, new string[] { " AND w.W_AGENT_NAME=@_agent", "@_agent", textBoxSearchAgentWaybill.Text }, ref temp, ref flag, new bool[] { false, false });

                        ComparsionGet(checkBoxSearchPriceWaybill.IsChecked.Value, new int[] { comboBoxDirectionSearchPriceWaybill.SelectedIndex, 0 },
                            new string[] { " AND (SELECT SUM(waybill_list.WL_TRADE_PRICE*waybill_list.WL_VALUE) FROM waybill_list WHERE waybill_list.W_ID=w.W_ID)" },
                            new string[] { null, Converter.CurrencyConvert(upDownSearchPriceWaybill.Text) }, ref temp, ref flag);

                        SQLParameterAdd(checkBoxSearchCodeWaybill.IsChecked.Value, new string[] { " AND w.W_ID=@_id", "@_id", textBoxSearchCodeWaybill.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchProductWaybill.IsChecked.Value, new string[] { " AND p.P_NAME=@_pname", "@_pname", textBoxSearchProductWaybill.Text }, ref temp, ref flag, new bool[] { true, false });

                        SQLParameterAdd(checkBoxSearchProductIdWaybill.IsChecked.Value, new string[] { " AND wl.P_ID=@_id", "@_id", textBoxSearchProductIdWaybill.Text }, ref temp, ref flag, new bool[] { true, false });

                        ComparsionGet(checkBoxSearchValueProductWaybill.IsChecked.Value, new int[] { comboBoxDirectionSearchValueProductWaybill.SelectedIndex, comboBoxSearchTypeCountProductWaybill.SelectedIndex },
                            new string[] { " AND wl.WL_VALUE", " AND (SELECT COUNT(waybill_list.WL_ID) FROM product_overdue,waybill_list WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.WL_ID=wl.WL_ID)>0 AND (SELECT wl.WL_VALUE-PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)",
                                " AND (SELECT COUNT(waybill_list.WL_ID) FROM product_overdue,waybill_list where product_overdue.PP_IS_OVERDUE='Не просрочено' and product_overdue.WL_ID=waybill_list.WL_ID and waybill_list.WL_ID=wl.WL_ID)>0 AND (SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)"," AND (SELECT PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)" },
                            new string[] { "@valueq", upDownSearchValueProductWaybill.Text }, ref temp, ref flag);

                        ComparsionGet(checkBoxSearchPriceProductWaybill.IsChecked.Value, new int[] { comboBoxDirectionSearchPriceProductWaybill.SelectedIndex, 0 },
                              new string[] { " AND wl.WL_TRADE_PRICE" },
                              new string[] { null, Converter.CurrencyConvert(upDownSearchPriceProductWaybill.Text) }, ref temp, ref flag);
                       
                        RangeComparsionGet(new bool[] { checkBoxSearchBDateProductWaybillOut.IsChecked.Value, checkBoxSearchEDateProductWaybillOut.IsChecked.Value },
                            comboBoxSearchDateRangeTypeProductWaybillOut.SelectedIndex,
                            new string[] { " AND wl.WL_EDATE>=@bbdate", " AND wl.WL_EDATE<=@bedate", " AND wl.WL_EDATE BETWEEN @bbdate AND @bedate", " AND (wl.WL_EDATE>@bedate OR wl.WL_EDATE<@bbdate)" },
                            new string[] { Converter.DateConvert(datePickerSearchBDateProductWaybillOut.Text), Converter.DateConvert(datePickerSearchEDateProductWaybillOut.Text) },
                            new string[] { "@bbdate", "@bedate" }, ref temp, ref flag);

                        RangeComparsionGet(new bool[] { checkBoxSearchBDateProductWaybillIn.IsChecked.Value, checkBoxSearchEDateProductWaybillIn.IsChecked.Value },
                            comboBoxSearchDateRangeTypeProductWaybillIn.SelectedIndex,
                            new string[] { " AND wl.WL_BDATE>=@ebdate", " AND wl.WL_BDATE<=@eedate", " AND wl.WL_BDATE BETWEEN @ebdate AND @eedate", " AND (wl.WL_BDATE>@eedate OR wl.WL_BDATE<@ebdate)" },
                            new string[] { Converter.DateConvert(datePickerSearchBDateProductWaybillIn.Text), Converter.DateConvert(datePickerSearchEDateProductWaybillIn.Text) },
                            new string[] { "@ebdate", "@eedate" }, ref temp,ref flag);

                        MakeSearch(dataGridWaybillOut, DataBase.GetWaybill, 
                            new string[] { "SELECT DISTINCT w.W_ID,w.W_DATE,e.E_NAME,w.W_AGENT_NAME FROM `waybill` w,`employee` e,waybill_list wl,product p WHERE w.`E_ID`=e.`E_ID` AND wl.W_ID=w.W_ID AND p.P_ID=wl.P_ID ",
                                "SELECT w.W_ID,w.W_DATE,e.E_NAME,w.W_AGENT_NAME FROM `waybill` w,`employee` e WHERE w.`E_ID`=e.`E_ID` " }
                                , temp, flag);
                        break;
                    }
            }
            valuesText.Clear();
            values.Clear();
        }

        private void MakeSearch<T>(DataGrid dataGrid, GetTableDelegate<T> data, string[] queryPart, string sqlParams, bool flag)
        {
            if(sqlParams != null)
            {
                string[] valuesTextStr = new string[valuesText.Count], valuesStr = new string[values.Count];
                string SQLQuery = null;
                for (int j = 0; j < valuesText.Count; j++)
                {
                    valuesTextStr[j] = valuesText[j];
                    valuesStr[j] = values[j];
                }
                if (flag == true)
                {
                    SQLQuery += queryPart[0] + sqlParams + ";";
                }
                else
                {
                    SQLQuery += queryPart[1] + sqlParams + ";";
                }
                dataGrid.ItemsSource = data(SQLQuery, valuesTextStr, valuesStr);
            }
        }

        private void ComparsionGet(bool toggleValue, int[] numberToggleValue, string[] sqlQueryPart, string[] sqlAddParam, ref string query, ref bool flag)
        {
            if (toggleValue == true && sqlAddParam[1] != "")
            {
                switch (numberToggleValue[1])
                {
                    case 0:
                        {
                            query += sqlQueryPart[0];
                            break;
                        }
                    case 1:
                        {
                            query += sqlQueryPart[1];
                            break;
                        }
                    case 2:
                        {
                            query += sqlQueryPart[2];
                            break;
                        }

                }
                switch (numberToggleValue[0])
                {
                    case 0:
                        {
                            query += "=";
                            break;
                        }
                    case 1:
                        {
                            query += ">=";
                            break;
                        }
                    case 2:
                        {
                            query += "<=";
                            break;
                        }
                    case 3:
                        {
                            query += ">";
                            break;
                        }
                    case 4:
                        {
                            query += "<";
                            break;
                        }
                }
                if (sqlAddParam[0] != null)
                {
                    query += sqlAddParam[0];
                    valuesText.Add(sqlAddParam[0]);
                    values.Add(sqlAddParam[1]);
                }
                else
                {
                    query += sqlAddParam[1];
                }
                flag = true;
            }
        }

        private void RangeComparsionGet(bool[] toggleValue,int rangeType, string[] sqlQueryPart, string[] sqlValue, string[] sqlAddParam, ref string query,ref bool flag)
        {
            if (toggleValue[0] == true && toggleValue[1] == true && sqlValue[0] != "" && sqlValue[1] != "")
            {

                switch (rangeType)
                {
                    case 0:
                        {
                            query += sqlQueryPart[2];
                            break;
                        }
                    case 1:
                        {
                            query += sqlQueryPart[3];
                            break;
                        }
                    case 2:
                        {
                            query += sqlQueryPart[4];
                            break;
                        }
                    case 3:
                        {
                            query += sqlQueryPart[5];
                            break;
                        }
                    case 4:
                        {
                            query += sqlQueryPart[6];
                            break;
                        }

                }
                valuesText.Add(sqlAddParam[0]);
                values.Add(sqlValue[0]);
                valuesText.Add(sqlAddParam[1]);
                values.Add(sqlValue[1]);
                flag = true;
            }
            else
            {
                if (toggleValue[0] == true && sqlValue[0] != "")
                {
                    query += sqlQueryPart[0];
                    valuesText.Add(sqlAddParam[0]);
                    values.Add(sqlValue[0]);
                    flag = true;
                }
                else
                {
                    if (toggleValue[1] == true && sqlValue[1] != "")
                    {
                        query += sqlQueryPart[1];
                        valuesText.Add(sqlAddParam[1]);
                        values.Add(sqlValue[1]);
                        flag = true;
                    }
                }
            }
        }

        private void SQLParameterAdd(bool toggleValue,string[] addParam,ref string query,ref bool flag,bool[] modeKey)
        {
            if(toggleValue == true && addParam[2] != "")
            {
                if (modeKey[1] == true)
                {
                    if (query == null)
                    {
                        query = "WHERE";
                    }
                    else
                    {
                        query += " AND";
                    }
                }
                query += addParam[0];
                valuesText.Add(addParam[1]);
                values.Add(addParam[2]);
                if (modeKey[0] == true)
                {
                    flag = true;
                }
            }
        }

        private void menuItemPriceSettings_Click(object sender, RoutedEventArgs e)
        {
            new PriceSettingsWindow().ShowDialog();
        }

        private void dataGridProductDiscountsDisplay_Initialized(object sender, EventArgs e)
        {
            if (dataGridCheckListOut.SelectedIndex != -1)
            {
                List<DiscountInfo> dataList = DataBase.GetDiscountInfoList(
                    new string[] { "@_id", "@_cid" },
                    new string[] { checkList[dataGridCheckListOut.SelectedIndex].ID.ToString(), Converter.DGCellToStringConvert(dataGridCheckOut.SelectedIndex, 0, dataGridCheckOut) },
                    "SELECT CONCAT('#',D_ID) AS 'D_ID',CONCAT(D_PRICE,'%') AS 'D_PRICE' FROM discounts,check_list,`check` WHERE discounts.P_ID=check_list.P_ID AND `check`.C_ID=@_cid AND check_list.P_ID=@_id AND check_list.C_ID=`check`.C_ID AND `check`.C_DATE>=discounts.D_BDATE AND `check`.C_DATE<=discounts.D_EDATE;");
                if (dataList.Count != 0)
                {
                    (sender as DataGrid).ItemsSource = dataList;
                }
                else
                {
                    (sender as DataGrid).Visibility = Visibility.Collapsed;
                }
            }
        }

        private void dataGridWaybillOverdueDisplay_Initialized(object sender, EventArgs e)
        {
            if (dataGridWaybillListOut.SelectedIndex != -1)
            {
                List<WaybillInfo> dataList = DataBase.GetWaybillInfoList(
                    new string[] { "@_id", "@_wid" },
                    new string[] { waybillList[dataGridWaybillListOut.SelectedIndex].ID.ToString(), Converter.DGCellToStringConvert(dataGridWaybillOut.SelectedIndex, 0, dataGridWaybillOut) },
                    "SELECT IF((SELECT COUNT(wl.WL_ID) WHERE po.PP_IS_OVERDUE='Просрочено')>0,wl.WL_VALUE-ps.PS_COUNT,0) AS 'OVERDUE',IF((SELECT COUNT(wl.WL_ID) WHERE po.PP_IS_OVERDUE='Не просрочено')>0,wl.WL_VALUE-ps.PS_COUNT,0) AS 'NOTOVERDUE',ps.PS_COUNT FROM waybill_list wl,product_sold ps,product_overdue po WHERE ps.WL_ID=wl.WL_ID AND po.WL_ID=wl.WL_ID AND wl.W_ID=@_wid AND wl.P_ID=@_id;");
                if (dataList.Count != 0)
                {
                    (sender as DataGrid).ItemsSource = dataList;
                }
                else
                {
                    (sender as DataGrid).Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SearchSingleToggle(CheckBox checkBox, object obj)
        {
            switch (obj.GetType().ToString())
            {
                case "System.Windows.Controls.TextBox":
                    {
                        if (checkBox.IsChecked == true)
                        {
                            (obj as TextBox).IsEnabled = true;
                        }
                        else
                        {
                            (obj as TextBox).Text = null;
                            (obj as TextBox).IsEnabled = false;
                        }
                        break;
                    }
                case "Xceed.Wpf.Toolkit.IntegerUpDown":
                    {
                        if (checkBox.IsChecked == true)
                        {
                            (obj as IntegerUpDown).IsEnabled = true;
                        }
                        else
                        {
                            (obj as IntegerUpDown).Text = null;
                            (obj as IntegerUpDown).IsEnabled = false;
                        }
                        break;
                    }
                case "Xceed.Wpf.Toolkit.DecimalUpDown":
                    {
                        if (checkBox.IsChecked == true)
                        {
                            (obj as DecimalUpDown).IsEnabled = true;
                        }
                        else
                        {
                            (obj as DecimalUpDown).Text = null;
                            (obj as DecimalUpDown).IsEnabled = false;
                        }
                        break;
                    }
                case "System.Windows.Controls.DatePicker":
                    {
                        if (checkBox.IsChecked == true)
                        {
                            (obj as DatePicker).IsEnabled = true;
                        }
                        else
                        {
                            (obj as DatePicker).Text = null;
                            (obj as DatePicker).IsEnabled = false;
                        }
                        break;
                    }
                case "System.Windows.Controls.ComboBox":
                    {
                        if (checkBox.IsChecked == true)
                        {
                            (obj as ComboBox).IsEnabled = true;
                            (obj as ComboBox).SelectedIndex = 0;
                        }
                        else
                        {
                            (obj as ComboBox).SelectedIndex = -1;
                            (obj as ComboBox).IsEnabled = false;
                        }
                        break;
                    }
            }
        }

        private void SearchDateRangeToggle(CheckBox checkBox1, CheckBox checkBox2, ComboBox comboBox, DatePicker datePicker)
        {
            if (checkBox1.IsChecked == true)
            {
                datePicker.IsEnabled = true;
                if (checkBox2.IsChecked == true)
                {
                    comboBox.IsEnabled = true;
                }
            }
            else
            {
                datePicker.Text = null;
                datePicker.IsEnabled = false;
                comboBox.IsEnabled = false;
            }
        }

        private void SearchIdNameToggle(CheckBox checkBox1,CheckBox checkbox2,TextBox textBox1,TextBox textBox2)
        {
            if (checkBox1.IsChecked == true)
            {
                textBox1.IsEnabled = true;
                textBox2.IsEnabled = false;
                textBox2.Text = null;
                checkbox2.IsEnabled = false;
            }
            else
            {
                checkbox2.IsEnabled = true;
                textBox1.IsEnabled = false;
                textBox1.Text = null;
            }
        }

        private void checkBoxSearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchEmployeeCheck, textBoxSearchEmployeeCheck);
        }

        private void checkBoxSearchBDate_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchBDateCheck, checkBoxSearchEDateCheck, comboBoxSearchDateRangeTypeCheck, datePickerSearchBDateCheck);
        }

        private void checkBoxSearchEDate_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchEDateCheck, checkBoxSearchBDateCheck, comboBoxSearchDateRangeTypeCheck, datePickerSearchEDateCheck);
        }

        private void checkBoxSearchBHours_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchBHours, upDownSearchBHours);
        }

        private void checkBoxSearchBMinutes_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchBMinutes, upDownSearchBMinutes);
        }

        private void checkBoxSearchBSeconds_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchBSeconds, upDownSearchBSeconds);
        }

        private void checkBoxSearchEHours_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchEHours, upDownSearchEHours);
        }

        private void checkBoxSearchEMinutes_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchEMinutes, upDownSearchEMinutes);
        }

        private void checkBoxSearchESeconds_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchESeconds, upDownSearchESeconds);
        }

        private void checkBoxSearchCheckCode_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCheckCode, textBoxSearchCheckCode);
        }

        private void checkBoxSearchProductCheck_Click(object sender, RoutedEventArgs e)
        {
            
            SearchIdNameToggle(checkBoxSearchProductCheck, checkBoxSearchProductIdCheck, textBoxSearchProductCheck, textBoxSearchProductIdCheck);
        }

        private void checkBoxSearchProductIdCheck_Click(object sender, RoutedEventArgs e)
        {
            
            SearchIdNameToggle(checkBoxSearchProductIdCheck, checkBoxSearchProductCheck, textBoxSearchProductIdCheck, textBoxSearchProductCheck);
        }

        private void checkBoxSearchPriceCheck_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchPriceCheck, upDownSearchPriceCheck);
        }

        private void checkBoxSearchValueCheck_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchValueCheck, upDownSearchValueCheck);
        }

        private void buttonResetSearchCheck_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchEmployeeCheck.IsChecked = false;
            textBoxSearchEmployeeCheck.Text = null;
            textBoxSearchEmployeeCheck.IsEnabled = false;
            checkBoxSearchBDateCheck.IsChecked = false;
            datePickerSearchBDateCheck.Text = null;
            datePickerSearchBDateCheck.IsEnabled = false;
            checkBoxSearchEDateCheck.IsChecked = false;
            datePickerSearchEDateCheck.Text = null;
            datePickerSearchEDateCheck.IsEnabled = false;
            checkBoxSearchBHours.IsChecked = false;
            upDownSearchBHours.Text = null;
            upDownSearchBHours.IsEnabled = false;
            checkBoxSearchEHours.IsChecked = false;
            upDownSearchEHours.Text = null;
            upDownSearchEHours.IsEnabled = false;
            checkBoxSearchBMinutes.IsChecked = false;
            upDownSearchBMinutes.Text = null;
            upDownSearchBMinutes.IsEnabled = false;
            checkBoxSearchEMinutes.IsChecked = false;
            upDownSearchEMinutes.Text = null;
            upDownSearchEMinutes.IsEnabled = false;
            checkBoxSearchBSeconds.IsChecked = false;
            upDownSearchBSeconds.Text = null;
            upDownSearchBSeconds.IsEnabled = false;
            checkBoxSearchESeconds.IsChecked = false;
            upDownSearchESeconds.Text = null;
            upDownSearchESeconds.IsEnabled = false;
            checkBoxSearchCheckCode.IsChecked = false;
            textBoxSearchCheckCode.Text = null;
            textBoxSearchCheckCode.IsEnabled = false;
            checkBoxSearchProductCheck.IsChecked = false;
            textBoxSearchProductCheck.Text = null;
            textBoxSearchProductCheck.IsEnabled = false;
            checkBoxSearchPriceCheck.IsChecked = false;
            upDownSearchPriceCheck.Text = null;
            upDownSearchPriceCheck.IsEnabled = false;
            checkBoxSearchValueCheck.IsChecked = false;
            upDownSearchValueCheck.Text = null;
            upDownSearchValueCheck.IsEnabled = false;
            checkBoxSearchPaytypeCard.IsChecked = false;
            checkBoxSearchPaytypeCash.IsChecked = false;
            comboBoxSearchDateRangeTypeCheck.IsEnabled = false;
            checkBoxSearchProductIdCheck.IsChecked = false;
            checkBoxSearchProductIdCheck.IsEnabled = true;
            textBoxSearchProductIdCheck.Text = null;
            textBoxSearchProductIdCheck.IsEnabled = false;
            checkBoxSearchProductCheck.IsEnabled = true;
            comboBoxSearchDateRangeTypeCheck.SelectedIndex = 0;
            comboBoxDirectionSearchPrice.SelectedIndex = 0;
            comboBoxDirectionSearchValue.SelectedIndex = 0;

        }

        private void buttonResetSearchDiscount_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchProductDiscount.IsChecked = false;
            checkBoxSearchProductDiscount.IsEnabled = true;
            textBoxSearchProductDiscount.Text = null;
            textBoxSearchProductDiscount.IsEnabled = false;
            checkBoxSearchProcDiscount.IsChecked = false;
            upDownSearchProcDiscount.Text = null;
            upDownSearchProcDiscount.IsEnabled = false;
            checkBoxSearchBDateDiscount.IsChecked = false;
            datePickerSearchBDateDiscount.Text = null;
            datePickerSearchBDateDiscount.IsEnabled = false;
            checkBoxSearchEDateDiscount.IsChecked = false;
            datePickerSearchEDateDiscount.Text = null;
            datePickerSearchEDateDiscount.IsEnabled = false;
            checkBoxSearchCodeDiscount.IsChecked = false;
            textBoxSearchCodeDiscount.Text = null;
            textBoxSearchCodeDiscount.IsEnabled = false;
            checkBoxSearchProductIdDiscount.IsChecked = false;
            checkBoxSearchProductIdDiscount.IsEnabled = true;
            textBoxSearchProductIdDiscount.Text = null;
            textBoxSearchProductIdDiscount.IsEnabled = false;
            comboBoxSearchDateRangeType.IsEnabled = false;
            comboBoxSearchDateRangeType.SelectedIndex = 0;
        }

        private void checkBoxSearchProcDiscount_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchProcDiscount, upDownSearchProcDiscount);
        }

        private void checkBoxSearchBDateDiscount_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchBDateDiscount, checkBoxSearchEDateDiscount, comboBoxSearchDateRangeType, datePickerSearchBDateDiscount);
            
        }

        private void checkBoxSearchEDateDiscount_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchEDateDiscount, checkBoxSearchBDateDiscount, comboBoxSearchDateRangeType, datePickerSearchEDateDiscount);
        }

        private void checkBoxSearchCodeDiscount_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCodeDiscount, textBoxSearchCodeDiscount);
        }

        private void checkBoxSearchProductIdDiscount_Click(object sender, RoutedEventArgs e)
        {
            SearchIdNameToggle(checkBoxSearchProductIdDiscount, checkBoxSearchProductDiscount, textBoxSearchProductIdDiscount, textBoxSearchProductDiscount);
           
        }

        private void checkBoxSearchProductDiscount_Click(object sender, RoutedEventArgs e)
        {
            SearchIdNameToggle(checkBoxSearchProductDiscount, checkBoxSearchProductIdDiscount, textBoxSearchProductDiscount, textBoxSearchProductIdDiscount);
            
        }

        private void buttonResetSearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchNameEmployee.IsChecked = false;
            textBoxSearchNameEmployee.Text = null;
            textBoxSearchNameEmployee.IsEnabled = false;
            checkBoxSearchPosEmployee.IsChecked = false;
            comboBoxSearchPosEmployee.SelectedIndex = -1;
            comboBoxSearchPosEmployee.IsEnabled = false;
            checkBoxSearchContractEmployee.IsChecked = false;
            textBoxSearchContractEmployee.Text = null;
            textBoxSearchContractEmployee.IsEnabled = false;
            checkBoxSearchTelEmployee.IsChecked = false;
            textBoxSearchTelEmployee.Text = null;
            textBoxSearchTelEmployee.IsEnabled = false;
            checkBoxSearchCodeEmployee.IsChecked = false;
            textBoxSearchCodeEmployee.Text = null;
            textBoxSearchCodeEmployee.IsEnabled = false;
            checkBoxSearchINNEmployee.IsChecked = false;
            textBoxSearchINNEmployee.Text = null;
            textBoxSearchINNEmployee.IsEnabled = false;
        }

        private void checkBoxSearchNameEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchNameEmployee, textBoxSearchNameEmployee);
        }

        private void checkBoxSearchPosEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchPosEmployee, comboBoxSearchPosEmployee);
        }

        private void checkBoxSearchContractEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchContractEmployee, textBoxSearchContractEmployee);
        }

        private void checkBoxSearchTelEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchTelEmployee, textBoxSearchTelEmployee);
        }

        private void checkBoxSearchCodeEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCodeEmployee, textBoxSearchCodeEmployee);
        }

        private void checkBoxSearchINNEmployee_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchINNEmployee, textBoxSearchINNEmployee);
        }

        private void buttonResetSearchManufacturer_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchNameManufacturer.IsChecked = false;
            textBoxSearchNameManufacturer.Text = null;
            textBoxSearchNameManufacturer.IsEnabled = false;
            checkBoxSearchCountryManufacturer.IsChecked = false;
            textBoxSearchCountryManufacturer.Text = null;
            textBoxSearchCountryManufacturer.IsEnabled = false;
            checkBoxSearchCityManufacturer.IsChecked = false;
            textBoxSearchCityManufacturer.Text = null;
            textBoxSearchCityManufacturer.IsEnabled = false;
            checkBoxSearchAddressManufacturer.IsChecked = false;
            textBoxSearchAddressManufacturer.Text = null;
            textBoxSearchAddressManufacturer.IsEnabled = false;
            checkBoxSearchTelManufacturer.IsChecked = false;
            textBoxSearchTelManufacturer.Text = null;
            textBoxSearchTelManufacturer.IsEnabled = false;
            checkBoxSearchCodeManufacturer.IsChecked = false;
            textBoxSearchCodeManufacturer.Text = null;
            textBoxSearchCodeManufacturer.IsEnabled = false;
            checkBoxSearchCodeManufacturer.IsChecked = false;
            textBoxSearchCodeManufacturer.Text = null;
            textBoxSearchCodeManufacturer.IsEnabled = false;
        }

        private void checkBoxSearchNameManufacturer_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchNameManufacturer, textBoxSearchNameManufacturer);
        }

        private void checkBoxSearchCountryManufacturer_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCountryManufacturer, textBoxSearchCountryManufacturer);
        }

        private void checkBoxSearchCityManufacturer_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCityManufacturer, textBoxSearchCityManufacturer);
        }

        private void checkBoxSearchAddressManufacturer_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchAddressManufacturer, textBoxSearchAddressManufacturer);
        }

        private void checkBoxSearchTelManufacturer_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchTelManufacturer, textBoxSearchTelManufacturer);
        }

        private void checkBoxSearchCodeManufacturer_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCodeManufacturer, textBoxSearchCodeManufacturer);
        }

        private void buttonResetSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchNameProduct.IsChecked = false;
            textBoxSearchNameProduct.Text = null;
            textBoxSearchNameProduct.IsEnabled = false;
            checkBoxSearchManufacturerProduct.IsChecked = false;
            textBoxSearchManufacturerProduct.Text = null;
            textBoxSearchManufacturerProduct.IsEnabled = false;
            checkBoxSearchGroupProduct.IsChecked = false;
            comboBoxSearchGroupProduct.SelectedIndex = -1;
            comboBoxSearchGroupProduct.IsEnabled = false;
            checkBoxSearchPackProduct.IsChecked = false;
            comboBoxSearchPackProduct.SelectedIndex = -1;
            comboBoxSearchPackProduct.IsEnabled = false;
            checkBoxSearchMaterialProduct.IsChecked = false;
            comboBoxSearchMaterialProduct.SelectedIndex = -1;
            comboBoxSearchMaterialProduct.IsEnabled = false;
            checkBoxSearchFormProduct.IsChecked = false;
            comboBoxSearchFormProduct.SelectedIndex = -1;
            comboBoxSearchFormProduct.IsEnabled = false;
            checkBoxSearchPriceProduct.IsChecked = false;
            upDownSearchPriceProduct.Text = null;
            upDownSearchPriceProduct.IsEnabled = false;
            checkBoxSearchCodeProduct.IsChecked = false;
            textBoxSearchCodeProduct.Text = null;
            textBoxSearchCodeProduct.IsEnabled = false;
            checkBoxSearchValueProduct.IsChecked = false;
            upDownSearchValueProduct.Text = null;
            upDownSearchValueProduct.IsEnabled = false;
            comboBoxSearchPriceRangeProduct.SelectedIndex = 0;
            comboBoxSearchTypeCountProduct.SelectedIndex = 0;
            comboBoxDirectionSearchValueProduct.SelectedIndex = 0;
        }

        private void checkBoxSearchNameProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchNameProduct, textBoxSearchNameProduct);
        }

        private void checkBoxSearchManufacturerProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchManufacturerProduct, textBoxSearchManufacturerProduct);
        }

        private void checkBoxSearchGroupProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchGroupProduct, comboBoxSearchGroupProduct);
        }

        private void checkBoxSearchPackProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchPackProduct, comboBoxSearchPackProduct);
        }

        private void checkBoxSearchMaterialProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchMaterialProduct, comboBoxSearchMaterialProduct);
        }

        private void checkBoxSearchFormProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchFormProduct, comboBoxSearchFormProduct);
        }

        private void checkBoxSearchPriceProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchPriceProduct, upDownSearchPriceProduct);
        }

        private void checkBoxSearchCodeProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCodeProduct, textBoxSearchCodeProduct);
        }

        private void checkBoxSearchValueProduct_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchValueProduct, upDownSearchValueProduct);
        }

        private void buttonResetSearchWaybill_Click(object sender, RoutedEventArgs e)
        {
            checkBoxSearchBDateWaybill.IsChecked = false;
            datePickerSearchBDateWaybill.Text = null;
            datePickerSearchBDateWaybill.IsEnabled = false;
            comboBoxSearchDateRangeTypeWaybill.SelectedIndex = 0;
            comboBoxSearchDateRangeTypeWaybill.IsEnabled = false;
            checkBoxSearchEDateWaybill.IsChecked = false;
            datePickerSearchEDateWaybill.Text = null;
            datePickerSearchEDateWaybill.IsEnabled = false;
            checkBoxSearchEmployeeWaybill.IsChecked = false;
            textBoxSearchEmployeeWaybill.Text = null;
            textBoxSearchEmployeeWaybill.IsEnabled = false;
            checkBoxSearchAgentWaybill.IsChecked = false;
            textBoxSearchAgentWaybill.Text = null;
            textBoxSearchAgentWaybill.IsEnabled = false;
            checkBoxSearchPriceWaybill.IsChecked = false;
            upDownSearchPriceWaybill.Text = null;
            upDownSearchPriceWaybill.IsEnabled = false;
            comboBoxDirectionSearchPriceWaybill.SelectedIndex = 0;
            checkBoxSearchCodeWaybill.IsChecked = false;
            textBoxSearchCodeWaybill.Text = null;
            textBoxSearchCodeWaybill.IsEnabled = false;
            checkBoxSearchBDateProductWaybillIn.IsChecked = false;
            datePickerSearchBDateProductWaybillIn.Text = null;
            datePickerSearchBDateProductWaybillIn.IsEnabled = false;
            comboBoxSearchDateRangeTypeProductWaybillIn.SelectedIndex = 0;
            comboBoxSearchDateRangeTypeProductWaybillIn.IsEnabled = false;
            checkBoxSearchEDateProductWaybillIn.IsChecked = false;
            datePickerSearchEDateProductWaybillIn.Text = null;
            datePickerSearchEDateProductWaybillIn.IsEnabled = false;
            checkBoxSearchProductWaybill.IsChecked = false;
            checkBoxSearchProductWaybill.IsEnabled = true;
            textBoxSearchProductWaybill.Text = null;
            textBoxSearchProductWaybill.IsEnabled = false;
            checkBoxSearchProductIdWaybill.IsChecked = false;
            checkBoxSearchProductIdWaybill.IsEnabled = true;
            textBoxSearchProductIdWaybill.Text = null;
            textBoxSearchProductIdWaybill.IsEnabled = false;
            checkBoxSearchValueProductWaybill.IsChecked = false;
            upDownSearchValueProductWaybill.Text = null;
            upDownSearchValueProductWaybill.IsEnabled = false;
            comboBoxDirectionSearchValueProductWaybill.SelectedIndex = 0;
            comboBoxSearchTypeCountProductWaybill.SelectedIndex = 0;
            checkBoxSearchPriceProductWaybill.IsChecked = false;
            upDownSearchPriceProductWaybill.Text = null;
            upDownSearchPriceProductWaybill.IsEnabled = false;
            comboBoxDirectionSearchPriceProductWaybill.SelectedIndex = 0;
            checkBoxSearchBDateProductWaybillOut.IsChecked = false;
            datePickerSearchBDateProductWaybillOut.Text = null;
            datePickerSearchBDateProductWaybillOut.IsEnabled = false;
            comboBoxSearchDateRangeTypeProductWaybillOut.SelectedIndex = 0;
            comboBoxSearchDateRangeTypeProductWaybillOut.IsEnabled = false;
            checkBoxSearchEDateProductWaybillOut.IsChecked = false;
            datePickerSearchEDateProductWaybillOut.Text = null;
            datePickerSearchEDateProductWaybillOut.IsEnabled = false;
        }

        private void checkBoxSearchBDateWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchBDateWaybill, checkBoxSearchEDateWaybill, comboBoxSearchDateRangeTypeWaybill, datePickerSearchBDateWaybill);
        }

        private void checkBoxSearchEDateWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchEDateWaybill, checkBoxSearchBDateWaybill, comboBoxSearchDateRangeTypeWaybill, datePickerSearchEDateWaybill);
        }

        private void checkBoxSearchEmployeeWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchEmployeeWaybill, textBoxSearchEmployeeWaybill);
        }

        private void checkBoxSearchAgentWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchAgentWaybill, textBoxSearchAgentWaybill);
        }

        private void checkBoxSearchPriceWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchPriceWaybill, upDownSearchPriceWaybill);
        }

        private void checkBoxSearchCodeWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchCodeWaybill, textBoxSearchCodeWaybill);
        }

        private void checkBoxSearchBDateProductWaybillIn_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchBDateProductWaybillIn, checkBoxSearchEDateProductWaybillIn, comboBoxSearchDateRangeTypeProductWaybillIn, datePickerSearchBDateProductWaybillIn);
        }

        private void checkBoxSearchEDateProductWaybillIn_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchEDateProductWaybillIn, checkBoxSearchBDateProductWaybillIn, comboBoxSearchDateRangeTypeProductWaybillIn, datePickerSearchEDateProductWaybillIn);
        }

        private void checkBoxSearchProductWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchIdNameToggle(checkBoxSearchProductWaybill, checkBoxSearchProductIdWaybill, textBoxSearchProductWaybill, textBoxSearchProductIdWaybill);
        }

        private void checkBoxSearchProductIdWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchIdNameToggle(checkBoxSearchProductIdWaybill, checkBoxSearchProductWaybill, textBoxSearchProductIdWaybill, textBoxSearchProductWaybill);
        }

        private void checkBoxSearchValueProductWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchValueProductWaybill, upDownSearchValueProductWaybill);
        }

        private void checkBoxSearchPriceProductWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchPriceProductWaybill, upDownSearchPriceProductWaybill);
        }

        private void checkBoxSearchBDateProductWaybillOut_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchBDateProductWaybillOut, checkBoxSearchEDateProductWaybillOut, comboBoxSearchDateRangeTypeProductWaybillOut, datePickerSearchBDateProductWaybillOut);
        }

        private void checkBoxSearchEDateProductWaybillOut_Click(object sender, RoutedEventArgs e)
        {
            SearchDateRangeToggle(checkBoxSearchEDateProductWaybillOut, checkBoxSearchBDateProductWaybillOut, comboBoxSearchDateRangeTypeProductWaybillOut, datePickerSearchEDateProductWaybillOut);
        }

        private void buttonResetSearch_Click(object sender, RoutedEventArgs e)
        {
            buttonResetSearchCheck_Click(null,null);
            buttonResetSearchDiscount_Click(null, null);
            buttonResetSearchEmployee_Click(null, null);
            buttonResetSearchManufacturer_Click(null, null);
            buttonResetSearchProduct_Click(null, null);
            buttonResetSearchWaybill_Click(null, null);
        }
    }
}