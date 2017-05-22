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

namespace WpfApplication1
{

    public partial class AdminWindow : Window
    {
        static string idText;
        List<CheckList> checkList = new List<CheckList> { };
        List<WaybillList> waybillList = new List<WaybillList> { };
        public AdminWindow(string _idText)
        {
            InitializeComponent();
            idText = _idText;
            dataGridCheckOut.ItemsSource = DataBase.GetCheck();
            dataGridDiscountOut.ItemsSource = DataBase.GetDiscount();
            dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer();
            dataGridProductOut.ItemsSource = DataBase.GetProduct();
            dataGridEmployeeOut.ItemsSource = DataBase.GetEmployee();
            dataGridWaybillOut.ItemsSource = DataBase.GetWaybill();
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

        private void adminLogout_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataBase.Query(new string[] { "@_id" }, new string[] { idText }, @"UPDATE `user` SET U_ONLINE='offline' WHERE U_ID=@_id;");
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch (selected)
            {
                case 0:
                    {
                        MessageBox.Show("Добавление запрещено.");
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
                        MessageBox.Show("Удаление запрещено.");
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            if (dataGridDiscountOut.SelectedIndex != -1)
                            {
                                DataBase.Query(new string[] { "@_curid" }, new string[] { Converter.DGCellToStringConvert(dataGridDiscountOut.SelectedIndex, 0, dataGridDiscountOut) }, "DELETE FROM `discounts` WHERE D_ID=@_curid;");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                        MessageBox.Show("Изменение запрещено.");
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
            int selected = tabControlSearch.SelectedIndex;
            switch(selected)
            {
                case 0:
                    {
                        string temp = null,
                        query = "SELECT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee` WHERE `check`.`E_ID`=`employee`.`E_ID` ",
                        redefineQuery = "SELECT DISTINCT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee`,check_list,product,product_actual_price WHERE `check`.`E_ID`=`employee`.`E_ID` AND `check`.C_ID=check_list.C_ID AND check_list.P_ID=product.P_ID AND check_list.P_ID=product_actual_price.P_ID ";
                        bool flag = false;
                        List<string> valuesText = new List<string>(), values = new List<string>();
                        if (checkBoxSearchEmployeeCheck.IsChecked == true && textBoxSearchEmployeeCheck.Text != "")
                        {
                            temp += "AND employee.E_NAME=@employee";
                            valuesText.Add("@employee");
                            values.Add(textBoxSearchEmployeeCheck.Text);
                        }
                        if (checkBoxSearchBDateCheck.IsChecked == true && checkBoxSearchEDateCheck.IsChecked == true && datePickerSearchBDateCheck.Text != "" && datePickerSearchEDateCheck.Text != "")
                        {
                            switch(comboBoxSearchDateRangeTypeCheck.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += " AND `check`.C_DATE BETWEEN @bdate AND @edate";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += " AND (`check`.C_DATE>@edate OR `check`.C_DATE<@bdate)";
                                        break;
                                    }
                            }
                            valuesText.Add("@bdate");
                            values.Add(Converter.DateConvert(datePickerSearchBDateCheck.Text));
                            valuesText.Add("@edate");
                            values.Add(Converter.DateConvert(datePickerSearchEDateCheck.Text));
                        }
                        else
                        {
                            if (checkBoxSearchBDateCheck.IsChecked == true && datePickerSearchBDateCheck.Text != "")
                            {
                                temp += "AND `check`.C_DATE>=@bdate";
                                valuesText.Add("@bdate");
                                values.Add(Converter.DateConvert(datePickerSearchBDateCheck.Text));
                            }
                            else
                            {
                                if (checkBoxSearchEDateCheck.IsChecked == true && datePickerSearchEDateCheck.Text != "")
                                {
                                    temp += "AND `check`.C_DATE<=@edate";
                                    valuesText.Add("@edate");
                                    values.Add(Converter.DateConvert(datePickerSearchEDateCheck.Text));
                                }
                            }
                        }
                        if (checkBoxSearchBHours.IsChecked == true && checkBoxSearchEHours.IsChecked == true && upDownSearchBHours.Text != "" && upDownSearchEHours.Text != "")
                        {
                            temp += " AND HOUR(`check`.C_DATE) BETWEEN @bhour AND @ehour";
                            valuesText.Add("@bhour");
                            values.Add(upDownSearchBHours.Text);
                            valuesText.Add("@ehour");
                            values.Add(upDownSearchEHours.Text);
                        }
                        else
                        {
                            if (checkBoxSearchBHours.IsChecked == true && upDownSearchBHours.Text != "")
                            {
                                temp += " AND HOUR(`check`.C_DATE)>=@bhour";
                                valuesText.Add("@bhour");
                                values.Add(upDownSearchBHours.Text);
                            }
                            else
                            {
                                if (checkBoxSearchEHours.IsChecked == true && upDownSearchEHours.Text != "")
                                {
                                    temp += " AND HOUR(`check`.C_DATE)<=@ehour";
                                    valuesText.Add("@ehour");
                                    values.Add(upDownSearchEHours.Text);
                                }
                            }
                        }
                        if (checkBoxSearchBMinutes.IsChecked == true && checkBoxSearchEMinutes.IsChecked == true && upDownSearchBMinutes.Text != "" && upDownSearchEMinutes.Text != "")
                        {
                            temp += " AND MINUTE(`check`.C_DATE) BETWEEN @bminute AND @eminute";
                            valuesText.Add("@bminute");
                            values.Add(upDownSearchBMinutes.Text);
                            valuesText.Add("@eminute");
                            values.Add(upDownSearchEMinutes.Text);
                        }
                        else
                        {
                            if (checkBoxSearchBMinutes.IsChecked == true && upDownSearchBMinutes.Text != "")
                            {
                                temp += " AND MINUTE(`check`.C_DATE)>=@bminute";
                                valuesText.Add("@bminute");
                                values.Add(upDownSearchBMinutes.Text);
                            }
                            else
                            {
                                if (checkBoxSearchEMinutes.IsChecked == true && upDownSearchEMinutes.Text != "")
                                {
                                    temp += " AND MINUTE(`check`.C_DATE)<=@eminute";
                                    valuesText.Add("@eminute");
                                    values.Add(upDownSearchEMinutes.Text);
                                }
                            }
                        }

                        if (checkBoxSearchBSeconds.IsChecked == true && checkBoxSearchESeconds.IsChecked == true && upDownSearchBSeconds.Text != "" && upDownSearchESeconds.Text != "")
                        {
                            temp += " AND SECOND(`check`.C_DATE) BETWEEN @bsecond AND @esecond";
                            valuesText.Add("@bsecond");
                            values.Add(upDownSearchBSeconds.Text);
                            valuesText.Add("@esecond");
                            values.Add(upDownSearchESeconds.Text);
                        }
                        else
                        {
                            if (checkBoxSearchBSeconds.IsChecked == true && upDownSearchBSeconds.Text != "")
                            {
                                temp += " AND SECOND(`check`.C_DATE)>=@bsecond";
                                valuesText.Add("@bsecond");
                                values.Add(upDownSearchBSeconds.Text);
                            }
                            else
                            {
                                if (checkBoxSearchESeconds.IsChecked == true && upDownSearchESeconds.Text != "")
                                {
                                    temp += " AND SECOND(`check`.C_DATE)<=@esecond";
                                    valuesText.Add("@esecond");
                                    values.Add(upDownSearchESeconds.Text);
                                }
                            }
                        }


                        if (checkBoxSearchPaytypeCash.IsChecked == true && checkBoxSearchPaytypeCard.IsChecked == true)
                        {

                        }
                        else
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
                        if (checkBoxSearchProductCheck.IsChecked == true && textBoxSearchProductCheck.Text != "")
                        {
                            temp += " AND product.P_NAME=@product";
                            valuesText.Add("@product");
                            values.Add(textBoxSearchProductCheck.Text);
                            flag = true;
                        }
                        if (checkBoxSearchProductIdCheck.IsChecked == true && textBoxSearchProductIdCheck.Text != "")
                        {
                            temp += " AND product.P_ID=@pid";
                            valuesText.Add("@pid");
                            values.Add(textBoxSearchProductIdCheck.Text);
                            flag = true;
                        }
                        if (checkBoxSearchPriceCheck.IsChecked == true && upDownSearchPriceCheck.Text != "")
                        {
                            temp += " AND product_actual_price.PAP_PRICE";
                            switch(comboBoxDirectionSearchPrice.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += Converter.CurrencyConvert(upDownSearchPriceCheck.Text);
                            flag = true;
                        }
                        if (checkBoxSearchValueCheck.IsChecked == true && upDownSearchValueCheck.Text != "")
                        {
                            temp += " AND check_list.CL_VALUE";
                            switch (comboBoxDirectionSearchPrice.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += "@value";
                            valuesText.Add("@value");
                            values.Add(upDownSearchValueCheck.Text);
                            flag = true;
                        }
                        if (checkBoxSearchCheckCode.IsChecked == true && textBoxSearchCheckCode.Text != "")
                        {

                            query = "SELECT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee` WHERE `check`.`E_ID`=`employee`.`E_ID` AND `check`.C_ID=@code;";
                            dataGridCheckOut.ItemsSource = DataBase.GetCheck(query, new string[] { "@code" }, new string[] { textBoxSearchCheckCode.Text });
                            valuesText.Clear();
                            values.Clear();
                        }
                        else
                        {
                            string[] valuesTextStr = new string[valuesText.Count], valuesStr = new string[values.Count];
                            for (int j = 0; j < valuesText.Count; j++)
                            {
                                valuesTextStr[j] = valuesText[j];
                                valuesStr[j] = values[j];
                            }
                            if(flag == true)
                            {
                                redefineQuery += temp + ";";
                                dataGridCheckOut.ItemsSource = DataBase.GetCheck(redefineQuery, valuesTextStr, valuesStr);
                            }
                            else
                            {
                                query += temp + ";";
                                dataGridCheckOut.ItemsSource = DataBase.GetCheck(query, valuesTextStr, valuesStr);
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        List<string> valuesText = new List<string>(), values = new List<string>();
                        string query = "SELECT discounts.D_ID,product.P_NAME,discounts.D_PRICE,discounts.D_BDATE,discounts.D_EDATE,discounts.D_TEXT FROM `discounts`,`product` WHERE `discounts`.`P_ID`=`product`.`P_ID`";
                        if (checkBoxSearchProductDiscount.IsChecked == true && textBoxSearchProductDiscount.Text != "")
                        {
                            query += " AND product.P_NAME=@_product";
                            valuesText.Add("@_product");
                            values.Add(textBoxSearchProductDiscount.Text);
                        }
                        else
                        {
                            if (checkBoxSearchProductIdDiscount.IsChecked == true && textBoxSearchProductIdDiscount.Text != "")
                            {
                                query += " AND discounts.P_ID=@_product";
                                valuesText.Add("@_product");
                                values.Add(textBoxSearchProductIdDiscount.Text);
                            }
                        }
                        if(checkBoxSearchProcDiscount.IsChecked == true && upDownSearchProcDiscount.Text != "")
                        {
                            query += " AND discounts.D_PRICE=@_proc";
                            valuesText.Add("@_proc");
                            values.Add(upDownSearchProcDiscount.Text);
                        }
                        if (checkBoxSearchBDateDiscount.IsChecked == true && datePickerSearchBDateDiscount.Text != "" && checkBoxSearchEDateDiscount.IsChecked == true && datePickerSearchEDateDiscount.Text != "")
                        {
                            switch(comboBoxSearchDateRangeType.SelectedIndex)
                            {
                                case 0:
                                    {
                                        query += " AND discounts.D_BDATE=@_bdate AND discounts.D_EDATE=@_edate";
                                        break;
                                    }
                                case 1:
                                    {
                                        query += " AND discounts.D_BDATE>=@_bdate AND discounts.D_EDATE<=@_edate";
                                        break;
                                    }
                                case 2:
                                    {
                                        query += " AND ((discounts.D_BDATE<=@_bdate AND discounts.D_EDATE<=@_bdate) OR (discounts.D_BDATE>=@_edate AND discounts.D_EDATE>=@_edate))";
                                        break;
                                    }
                                case 3:
                                    {
                                        query += " AND ((discounts.D_BDATE>=@_bdate AND discounts.D_EDATE>=@_edate AND discounts.D_BDATE<=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE>=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE<=@_edate AND discounts.D_EDATE>=@_bdate))";
                                        break;
                                    }
                                case 4:
                                    {
                                        query += " AND ((discounts.D_BDATE>=@_bdate AND discounts.D_EDATE>=@_edate AND discounts.D_BDATE<=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE>=@_edate) OR (discounts.D_BDATE<=@_bdate AND discounts.D_EDATE<=@_edate AND discounts.D_EDATE>=@_bdate) OR (discounts.D_BDATE>=@_bdate AND discounts.D_EDATE<=@_edate))";
                                        break;
                                    }
                            }
                            valuesText.Add("@_bdate");
                            values.Add(Converter.DateConvert(datePickerSearchBDateDiscount.Text));
                            valuesText.Add("@_edate");
                            values.Add(Converter.DateConvert(datePickerSearchEDateDiscount.Text));
                        }
                        else
                        {
                            if (checkBoxSearchBDateDiscount.IsChecked == true && datePickerSearchBDateDiscount.Text != "")
                            {
                                query += " AND discounts.D_BDATE>=@_bdate";
                                valuesText.Add("@_bdate");
                                values.Add(Converter.DateConvert(datePickerSearchBDateDiscount.Text));
                            }
                            else
                            {
                                if (checkBoxSearchEDateDiscount.IsChecked == true && datePickerSearchEDateDiscount.Text != "")
                                {
                                    query += " AND discounts.D_EDATE<=@_edate";
                                    valuesText.Add("@_edate");
                                    values.Add(Converter.DateConvert(datePickerSearchEDateDiscount.Text));
                                }
                            }
                        }
                        if(checkBoxSearchCodeDiscount.IsChecked == true && textBoxSearchCodeDiscount.Text != "")
                        {
                            query = "SELECT discounts.D_ID,product.P_NAME,discounts.D_PRICE,discounts.D_BDATE,discounts.D_EDATE,discounts.D_TEXT FROM `discounts`,`product` WHERE `discounts`.`P_ID`=`product`.`P_ID` AND discount.D_ID=@_id;";
                            dataGridDiscountOut.ItemsSource = DataBase.GetDiscount(query, new string[] { "@_id" }, new string[] { textBoxSearchCodeDiscount.Text });
                        }
                        else
                        {
                            string[] valuesTextStr = new string[valuesText.Count], valuesStr = new string[values.Count];
                            for (int j = 0; j < valuesText.Count; j++)
                            {
                                valuesTextStr[j] = valuesText[j];
                                valuesStr[j] = values[j];
                            }
                            dataGridDiscountOut.ItemsSource = DataBase.GetDiscount(query + ";", valuesTextStr, valuesStr);
                        }
                        break;
                    }
                case 2:
                    {
                        List<string> valuesText = new List<string>(), values = new List<string>();
                        string query = "SELECT E_ID,E_NAME,E_TEL,E_POSITION,E_CONTRACT FROM `employee` ";
                        bool flag = false;
                        if (checkBoxSearchNameEmployee.IsChecked == true && textBoxSearchNameEmployee.Text != "")
                        {
                            query += "WHERE E_NAME=@_name";
                            valuesText.Add("@_name");
                            values.Add(textBoxSearchNameEmployee.Text);
                            flag = true;
                        }
                        if(checkBoxSearchPosEmployee.IsChecked == true && comboBoxSearchPosEmployee.SelectedIndex != -1)
                        {
                            if(flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " E_POSITION=@_pos";
                            valuesText.Add("@_pos");
                            values.Add(comboBoxSearchPosEmployee.SelectedItem.ToString());
                        }
                        if(checkBoxSearchContractEmployee.IsChecked == true && textBoxSearchContractEmployee.Text != "")
                        {
                            if (flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " E_CONTRACT=@_contr";
                            valuesText.Add("@_contr");
                            values.Add(textBoxSearchContractEmployee.Text);
                        }
                        if(checkBoxSearchTelEmployee.IsChecked == true && textBoxSearchTelEmployee.Text != "")
                        {
                            if (flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " E_TEL=@_tel";
                            valuesText.Add("@_tel");
                            values.Add(textBoxSearchTelEmployee.Text);
                        }
                        if(checkBoxSearchCodeEmployee.IsChecked == true && textBoxSearchCodeEmployee.Text != "")
                        {
                            dataGridEmployeeOut.ItemsSource = DataBase.GetEmployee("SELECT E_ID,E_NAME,E_TEL,E_POSITION,E_CONTRACT FROM `employee` WHERE E_ID=@_id;", new string[] { "@_id" }, new string[] { textBoxSearchCodeEmployee.Text });
                        }
                        else
                        {
                            if(flag == true)
                            {
                                string[] valuesTextStr = new string[valuesText.Count], valuesStr = new string[values.Count];
                                for (int j = 0; j < valuesText.Count; j++)
                                {
                                    valuesTextStr[j] = valuesText[j];
                                    valuesStr[j] = values[j];
                                }
                                dataGridEmployeeOut.ItemsSource = DataBase.GetEmployee(query + ";",valuesTextStr,valuesStr);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        List<string> valuesText = new List<string>(), values = new List<string>();
                        string query = "SELECT M_ID,M_NAME,M_COUNTRY,M_CITY,M_ADDR,M_TEL FROM manufacturer ";
                        bool flag = false;
                        if (checkBoxSearchNameManufacturer.IsChecked == true && textBoxSearchNameManufacturer.Text != "")
                        {
                            query += "WHERE M_NAME=@_name";
                            valuesText.Add("@_name");
                            values.Add(textBoxSearchNameManufacturer.Text);
                            flag = true;
                        }
                        if (checkBoxSearchCountryManufacturer.IsChecked == true && textBoxSearchCountryManufacturer.Text != "")
                        {
                            if (flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " M_COUNTRY=@_country";
                            valuesText.Add("@_country");
                            values.Add(textBoxSearchCountryManufacturer.Text);
                        }
                        if (checkBoxSearchCityManufacturer.IsChecked == true && textBoxSearchCityManufacturer.Text != "")
                        {
                            if (flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " M_CITY=@_city";
                            valuesText.Add("@_city");
                            values.Add(textBoxSearchCityManufacturer.Text);
                        }
                        if (checkBoxSearchAddressManufacturer.IsChecked == true && textBoxSearchAddressManufacturer.Text != "")
                        {
                            if (flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " M_ADDR=@_address";
                            valuesText.Add("@_address");
                            values.Add(textBoxSearchAddressManufacturer.Text);
                        }
                        if (checkBoxSearchTelManufacturer.IsChecked == true && textBoxSearchTelManufacturer.Text != "")
                        {
                            if (flag == false)
                            {
                                flag = true;
                                query += "WHERE";
                            }
                            else
                            {
                                query += " AND";
                            }
                            query += " M_TEL=@_tel";
                            valuesText.Add("@_tel");
                            values.Add(textBoxSearchTelManufacturer.Text);
                        }
                        if (checkBoxSearchCodeManufacturer.IsChecked == true && textBoxSearchCodeManufacturer.Text != "")
                        {
                            dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer("SELECT M_ID,M_NAME,M_COUNTRY,M_CITY,M_ADDR,M_TEL FROM manufacturer WHERE M_ID=@_id;", new string[] { "@_id" }, new string[] { textBoxSearchCodeManufacturer.Text });
                        }
                        else
                        {
                            if(flag == true)
                            {
                            string[] valuesTextStr = new string[valuesText.Count], valuesStr = new string[values.Count];
                            for (int j = 0; j < valuesText.Count; j++)
                            {
                                valuesTextStr[j] = valuesText[j];
                                valuesStr[j] = values[j];
                            }
                            dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer(query + ";", valuesTextStr, valuesStr);
                            }
                            
                        }
                        break;
                    }
                case 4:
                    {
                        List<string> valuesText = new List<string>(), values = new List<string>();
                        string query = "SELECT p.P_ID,p.P_NAME,manufacturer.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,p.P_INSTR FROM `product` p,`manufacturer` WHERE `manufacturer`.`M_ID`=p.`M_ID` ",
                        redefineQuery = "SELECT p.P_ID,p.P_NAME,manufacturer.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,p.P_INSTR FROM `product` p,`manufacturer`,product_actual_price WHERE `manufacturer`.`M_ID`=p.`M_ID` AND p.P_ID=product_actual_price.P_ID ",
                        temp=null;
                        bool flag = false;
                        if (checkBoxSearchNameProduct.IsChecked == true && textBoxSearchNameProduct.Text != "")
                        {
                            temp += " AND p.P_NAME=@_name";
                            valuesText.Add("@_name");
                            values.Add(textBoxSearchNameProduct.Text);
                        }
                        if (checkBoxSearchManufacturerProduct.IsChecked == true && textBoxSearchManufacturerProduct.Text != "")
                        {
                            temp += " AND manufacturer.M_NAME=@_mname";
                            valuesText.Add("@_mname");
                            values.Add(textBoxSearchManufacturerProduct.Text);
                        }
                        if (checkBoxSearchGroupProduct.IsChecked == true && comboBoxSearchGroupProduct.SelectedIndex != -1)
                        {
                            temp += " AND p.P_GROUP=@_group";
                            valuesText.Add("@_group");
                            values.Add(comboBoxSearchGroupProduct.SelectedItem.ToString());
                        }
                        if (checkBoxSearchPackProduct.IsChecked == true && comboBoxSearchPackProduct.SelectedIndex != -1)
                        {
                            temp += " AND p.P_PACK=@_pack";
                            valuesText.Add("@_pack");
                            values.Add(comboBoxSearchPackProduct.SelectedItem.ToString());
                        }
                        if (checkBoxSearchMaterialProduct.IsChecked == true && comboBoxSearchMaterialProduct.SelectedIndex != -1)
                        {
                            temp += " AND p.P_MATERIAL=@_material";
                            valuesText.Add("@_material");
                            values.Add(comboBoxSearchMaterialProduct.SelectedItem.ToString());
                        }
                        if (checkBoxSearchFormProduct.IsChecked == true && comboBoxSearchFormProduct.SelectedIndex != -1)
                        {
                            temp += " AND p.P_FORM=@_form";
                            valuesText.Add("@_form");
                            values.Add(comboBoxSearchFormProduct.SelectedItem.ToString());
                        }
                        if (checkBoxSearchPriceProduct.IsChecked == true && upDownSearchPriceProduct.Text != "")
                        {
                            temp += " AND product_actual_price.PAP_PRICE";
                            switch (comboBoxSearchPriceRangeProduct.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += Converter.CurrencyConvert(upDownSearchPriceProduct.Text);
                            flag = true;
                        }
                        if (checkBoxSearchValueProduct.IsChecked == true && upDownSearchValueProduct.Text != "")
                        {
                            switch(comboBoxSearchTypeCountProduct.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += " AND (SELECT SUM(waybill_list.WL_VALUE-product_sold.PS_COUNT) FROM waybill_list,product_sold WHERE waybill_list.P_ID=p.P_ID AND waybill_list.WL_ID=product_sold.WL_ID)";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += " AND (SELECT waybill_list.WL_VALUE-product_sold.PS_COUNT FROM waybill_list,product_sold,product_overdue WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_sold.WL_ID=waybill_list.WL_ID AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=p.P_ID)";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += " AND (SELECT IF(COUNT(waybill_list.WL_VALUE-product_sold.PS_COUNT)>0,waybill_list.WL_VALUE-product_sold.PS_COUNT,0) FROM product_sold,product_overdue,waybill_list WHERE product_sold.WL_ID=product_overdue.WL_ID AND product_overdue.PP_IS_OVERDUE<>'Просрочено' AND product_overdue.PP_IS_OVERDUE<>'Продано' AND product_sold.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=p.P_ID)";
                                        break;
                                    }

                            }
                            switch (comboBoxDirectionSearchValueProduct.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += "@value";
                            valuesText.Add("@value");
                            values.Add(upDownSearchValueProduct.Text);
                            flag = true;
                        }
                        if (checkBoxSearchCodeProduct.IsChecked == true && textBoxSearchCodeProduct.Text != "")
                        {

                            dataGridProductOut.ItemsSource = DataBase.GetProduct(
                                "SELECT product.P_ID,product.P_NAME,manufacturer.M_NAME,product.P_GROUP,product.P_PACK,product.P_MATERIAL,product.P_FORM,product.P_INSTR FROM `product`,`manufacturer` WHERE `manufacturer`.`M_ID`=`product`.`M_ID AND product.P_ID=@_code`;",
                                new string[] { "@code" },
                                new string[] { textBoxSearchCodeProduct.Text });
                            valuesText.Clear();
                            values.Clear();
                        }
                        else
                        {
                            string[] valuesTextStr = new string[valuesText.Count], valuesStr = new string[values.Count];
                            for (int j = 0; j < valuesText.Count; j++)
                            {
                                valuesTextStr[j] = valuesText[j];
                                valuesStr[j] = values[j];
                            }
                            if (flag == true)
                            {
                                redefineQuery += temp + ";";
                                dataGridProductOut.ItemsSource = DataBase.GetProduct(redefineQuery, valuesTextStr, valuesStr);
                            }
                            else
                            {
                                query += temp + ";";
                                dataGridProductOut.ItemsSource = DataBase.GetProduct(query, valuesTextStr, valuesStr);
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        string temp = null,
                        query = "SELECT w.W_ID,w.W_DATE,e.E_NAME,w.W_AGENT_NAME FROM `waybill` w,`employee` e WHERE w.`E_ID`=e.`E_ID` ",
                        redefineQuery = "SELECT DISTINCT w.W_ID,w.W_DATE,e.E_NAME,w.W_AGENT_NAME FROM `waybill` w,`employee` e,waybill_list wl,product p WHERE w.`E_ID`=e.`E_ID` AND wl.W_ID=w.W_ID AND p.P_ID=wl.P_ID ";
                        bool flag = false;
                        List<string> valuesText = new List<string>(), values = new List<string>();
                        if (checkBoxSearchBDateWaybill.IsChecked == true && checkBoxSearchEDateWaybill.IsChecked == true && datePickerSearchBDateWaybill.Text != "" && datePickerSearchEDateWaybill.Text != "")
                        {
                            switch (comboBoxSearchDateRangeTypeWaybill.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += " AND w.W_DATE BETWEEN @bdate AND @edate";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += " AND (w.W_DATE>@edate OR w.W_DATE<@bdate)";
                                        break;
                                    }
                            }
                            valuesText.Add("@bdate");
                            values.Add(Converter.DateConvert(datePickerSearchBDateWaybill.Text));
                            valuesText.Add("@edate");
                            values.Add(Converter.DateConvert(datePickerSearchEDateWaybill.Text));
                        }
                        else
                        {
                            if (checkBoxSearchBDateWaybill.IsChecked == true && datePickerSearchBDateWaybill.Text != "")
                            {
                                temp += "AND w.W_DATE>=@bdate";
                                valuesText.Add("@bdate");
                                values.Add(Converter.DateConvert(datePickerSearchBDateWaybill.Text));
                            }
                            else
                            {
                                if (checkBoxSearchEDateWaybill.IsChecked == true && datePickerSearchEDateWaybill.Text != "")
                                {
                                    temp += "AND w.W_DATE<=@edate";
                                    valuesText.Add("@edate");
                                    values.Add(Converter.DateConvert(datePickerSearchEDateWaybill.Text));
                                }
                            }
                        }
                        if (checkBoxSearchEmployeeWaybill.IsChecked == true && textBoxSearchEmployeeWaybill.Text != "")
                        {
                            temp += " AND e.E_NAME=@_name";
                            valuesText.Add("@_name");
                            values.Add(textBoxSearchEmployeeWaybill.Text);
                        }
                        if (checkBoxSearchAgentWaybill.IsChecked == true && textBoxSearchAgentWaybill.Text != "")
                        {
                            temp += " AND w.W_AGENT_NAME=@_agent";
                            valuesText.Add("@_agent");
                            values.Add(textBoxSearchAgentWaybill.Text);
                        }
                        if (checkBoxSearchPriceWaybill.IsChecked == true && upDownSearchPriceWaybill.Text != "")
                        {
                            temp += " AND (SELECT SUM(waybill_list.WL_TRADE_PRICE*waybill_list.WL_VALUE) FROM waybill_list WHERE waybill_list.W_ID=w.W_ID)";
                            switch (comboBoxDirectionSearchPriceWaybill.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += Converter.CurrencyConvert(upDownSearchPriceWaybill.Text);
                            flag = true;
                        }
                        if (checkBoxSearchCodeWaybill.IsChecked == true && textBoxSearchCodeWaybill.Text != "")
                        {
                            temp += " AND w.W_ID=@_id";
                            valuesText.Add("@_id");
                            values.Add(textBoxSearchCodeWaybill.Text);
                        }
                        if (checkBoxSearchProductWaybill.IsChecked == true && textBoxSearchProductWaybill.Text != "")
                        {
                            temp += " AND p.P_NAME=@_pname";
                            valuesText.Add("@_pname");
                            values.Add(textBoxSearchProductWaybill.Text);
                            flag = true;
                        }
                        if (checkBoxSearchProductIdWaybill.IsChecked == true && textBoxSearchProductIdWaybill.Text != "")
                        {
                            temp += " AND wl.P_ID=@_id";
                            valuesText.Add("@_id");
                            values.Add(textBoxSearchProductIdWaybill.Text);
                        }
                        if (checkBoxSearchValueProductWaybill.IsChecked == true && upDownSearchValueProductWaybill.Text != "")
                        {
                            switch (comboBoxSearchTypeCountProductWaybill.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += " AND wl.WL_VALUE";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += " AND (SELECT COUNT(waybill_list.WL_ID) FROM product_overdue,waybill_list WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.WL_ID=wl.WL_ID)>0 AND wl.WL_VALUE";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += " AND (SELECT COUNT(waybill_list.WL_ID) FROM product_overdue,waybill_list where product_overdue.PP_IS_OVERDUE='Не просрочено' and product_overdue.WL_ID=waybill_list.WL_ID and waybill_list.WL_ID=wl.WL_ID)>0 AND (SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)";
                                        break;
                                    }

                            }
                            switch (comboBoxDirectionSearchValueProduct.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += "@value";
                            valuesText.Add("@value");
                            values.Add(comboBoxDirectionSearchValueProduct.Text);
                            flag = true;
                        }
                        if (checkBoxSearchPriceProductWaybill.IsChecked == true && upDownSearchPriceProductWaybill.Text != "")
                        {
                            temp += " AND wl.WL_TRADE_PRICE";
                            switch (comboBoxSearchPriceRangeProduct.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += "=";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += ">=";
                                        break;
                                    }
                                case 2:
                                    {
                                        temp += "<=";
                                        break;
                                    }
                                case 3:
                                    {
                                        temp += ">";
                                        break;
                                    }
                                case 4:
                                    {
                                        temp += "<";
                                        break;
                                    }

                            }
                            temp += Converter.CurrencyConvert(upDownSearchPriceProductWaybill.Text);
                            flag = true;
                        }
                        if (checkBoxSearchBDateProductWaybillOut.IsChecked == true && checkBoxSearchEDateProductWaybillOut.IsChecked == true && datePickerSearchBDateProductWaybillOut.Text != "" && datePickerSearchEDateProductWaybillOut.Text != "")
                        {
                            switch (comboBoxSearchDateRangeTypeProductWaybillOut.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += " AND wl.WL_BDATE BETWEEN @bbdate AND @bedate";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += " AND (wl.WL_BDATE>@bedate OR wl.WL_BDATE<@bbdate)";
                                        break;
                                    }
                            }
                            valuesText.Add("@bbdate");
                            values.Add(Converter.DateConvert(datePickerSearchBDateProductWaybillOut.Text));
                            valuesText.Add("@bedate");
                            values.Add(Converter.DateConvert(datePickerSearchEDateProductWaybillOut.Text));
                        }
                        else
                        {
                            if (checkBoxSearchBDateProductWaybillOut.IsChecked == true && datePickerSearchBDateProductWaybillOut.Text != "")
                            {
                                temp += "AND wl.WL_BDATE>=@bbdate";
                                valuesText.Add("@bbdate");
                                values.Add(Converter.DateConvert(datePickerSearchBDateProductWaybillOut.Text));
                            }
                            else
                            {
                                if (checkBoxSearchEDateProductWaybillOut.IsChecked == true && datePickerSearchEDateProductWaybillOut.Text != "")
                                {
                                    temp += "AND wl.WL_BDATE<=@bedate";
                                    valuesText.Add("@bedate");
                                    values.Add(Converter.DateConvert(datePickerSearchEDateProductWaybillOut.Text));
                                }
                            }
                        }
                        if (checkBoxSearchBDateProductWaybillIn.IsChecked == true && checkBoxSearchEDateProductWaybillIn.IsChecked == true && datePickerSearchBDateProductWaybillIn.Text != "" && datePickerSearchEDateProductWaybillIn.Text != "")
                        {
                            switch (comboBoxSearchDateRangeTypeProductWaybillIn.SelectedIndex)
                            {
                                case 0:
                                    {
                                        temp += " AND wl.WL_EDATE BETWEEN @ebdate AND @eedate";
                                        break;
                                    }
                                case 1:
                                    {
                                        temp += " AND (wl.WL_EDATE>@eedate OR wl.WL_EDATE<@ebdate)";
                                        break;
                                    }
                            }
                            valuesText.Add("@ebdate");
                            values.Add(Converter.DateConvert(datePickerSearchBDateProductWaybillIn.Text));
                            valuesText.Add("@eedate");
                            values.Add(Converter.DateConvert(datePickerSearchEDateProductWaybillIn.Text));
                        }
                        else
                        {
                            if (checkBoxSearchBDateProductWaybillOut.IsChecked == true && datePickerSearchBDateProductWaybillIn.Text != "")
                            {
                                temp += "AND wl.WL_EDATE>=@ebdate";
                                valuesText.Add("@ebdate");
                                values.Add(Converter.DateConvert(datePickerSearchBDateProductWaybillIn.Text));
                            }
                            else
                            {
                                if (checkBoxSearchEDateProductWaybillIn.IsChecked == true && datePickerSearchEDateProductWaybillIn.Text != "")
                                {
                                    temp += "AND wl.WL_EDATE<=@eedate";
                                    valuesText.Add("@eedate");
                                    values.Add(Converter.DateConvert(datePickerSearchEDateProductWaybillIn.Text));
                                }
                            }
                        }
                        break;
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

        private void checkBoxSearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEmployeeCheck.IsChecked == true)
            {
                textBoxSearchEmployeeCheck.IsEnabled = true;
            }
            else
            {
                textBoxSearchEmployeeCheck.Text = null;
                textBoxSearchEmployeeCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchBDate_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBDateCheck.IsChecked == true)
            {
                datePickerSearchBDateCheck.IsEnabled = true;
                if (checkBoxSearchEDateCheck.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeCheck.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchBDateCheck.Text = null;
                datePickerSearchBDateCheck.IsEnabled = false;
                comboBoxSearchDateRangeTypeCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchEDate_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEDateCheck.IsChecked == true)
            {
                datePickerSearchEDateCheck.IsEnabled = true;
                if (checkBoxSearchBDateCheck.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeCheck.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchEDateCheck.Text = null;
                datePickerSearchEDateCheck.IsEnabled = false;
                comboBoxSearchDateRangeTypeCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchBHours_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBHours.IsChecked == true)
            {
                upDownSearchBHours.IsEnabled = true;
            }
            else
            {
                upDownSearchBHours.Text = null;
                upDownSearchBHours.IsEnabled = false;
            }
        }

        private void checkBoxSearchBMinutes_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBMinutes.IsChecked == true)
            {
                upDownSearchBMinutes.IsEnabled = true;
            }
            else
            {
                upDownSearchBMinutes.Text = null;
                upDownSearchBMinutes.IsEnabled = false;
            }
        }

        private void checkBoxSearchBSeconds_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBSeconds.IsChecked == true)
            {
                upDownSearchBSeconds.IsEnabled = true;
            }
            else
            {
                upDownSearchBSeconds.Text = null;
                upDownSearchBSeconds.IsEnabled = false;
            }
        }

        private void checkBoxSearchEHours_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEHours.IsChecked == true)
            {
                upDownSearchEHours.IsEnabled = true;
            }
            else
            {
                upDownSearchEHours.Text = null;
                upDownSearchEHours.IsEnabled = false;
            }
        }

        private void checkBoxSearchEMinutes_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEMinutes.IsChecked == true)
            {
                upDownSearchEMinutes.IsEnabled = true;
            }
            else
            {
                upDownSearchEMinutes.Text = null;
                upDownSearchEMinutes.IsEnabled = false;
            }
        }

        private void checkBoxSearchESeconds_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchESeconds.IsChecked == true)
            {
                upDownSearchESeconds.IsEnabled = true;
            }
            else
            {
                upDownSearchESeconds.Text = null;
                upDownSearchESeconds.IsEnabled = false;
            }
        }

        private void checkBoxSearchCheckCode_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCheckCode.IsChecked == true)
            {
                textBoxSearchCheckCode.IsEnabled = true;
            }
            else
            {
                textBoxSearchCheckCode.Text = null;
                textBoxSearchCheckCode.IsEnabled = false;
            }
        }

        private void checkBoxSearchProductCheck_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchProductCheck.IsChecked == true)
            {
                textBoxSearchProductCheck.IsEnabled = true;
                textBoxSearchProductIdCheck.IsEnabled = false;
                checkBoxSearchProductIdCheck.IsEnabled = false;
            }
            else
            {
                checkBoxSearchProductIdCheck.IsEnabled = true;
                textBoxSearchProductCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchProductIdCheck_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchProductIdCheck.IsChecked == true)
            {
                textBoxSearchProductIdCheck.IsEnabled = true;
                textBoxSearchProductCheck.IsEnabled = false;
                checkBoxSearchProductCheck.IsEnabled = false;
            }
            else
            {
                checkBoxSearchProductCheck.IsEnabled = true;
                textBoxSearchProductIdCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchPriceCheck_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchPriceCheck.IsChecked == true)
            {
                upDownSearchPriceCheck.IsEnabled = true;
            }
            else
            {
                upDownSearchPriceCheck.Text = null;
                upDownSearchPriceCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchValueCheck_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchValueCheck.IsChecked == true)
            {
                upDownSearchValueCheck.IsEnabled = true;
            }
            else
            {
                upDownSearchValueCheck.Text = null;
                upDownSearchValueCheck.IsEnabled = false;
            }
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
            if (checkBoxSearchProcDiscount.IsChecked == true)
            {
                upDownSearchProcDiscount.IsEnabled = true;
            }
            else
            {
                upDownSearchProcDiscount.Text = null;
                upDownSearchProcDiscount.IsEnabled = false;
            }
        }

        private void checkBoxSearchBDateDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBDateDiscount.IsChecked == true)
            {
                datePickerSearchBDateDiscount.IsEnabled = true;
                if(checkBoxSearchEDateDiscount.IsChecked == true)
                {
                    comboBoxSearchDateRangeType.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchBDateDiscount.Text = null;
                datePickerSearchBDateDiscount.IsEnabled = false;
                comboBoxSearchDateRangeType.IsEnabled = false;
            }
        }

        private void checkBoxSearchEDateDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEDateDiscount.IsChecked == true)
            {
                datePickerSearchEDateDiscount.IsEnabled = true;
                if (checkBoxSearchBDateDiscount.IsChecked == true)
                {
                    comboBoxSearchDateRangeType.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchEDateDiscount.Text = null;
                datePickerSearchEDateDiscount.IsEnabled = false;
                comboBoxSearchDateRangeType.IsEnabled = false;
            }
        }

        private void checkBoxSearchCodeDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCodeDiscount.IsChecked == true)
            {
                textBoxSearchCodeDiscount.IsEnabled = true;
            }
            else
            {
                textBoxSearchCodeDiscount.Text = null;
                textBoxSearchCodeDiscount.IsEnabled = false;
            }
        }

        private void checkBoxSearchProductIdDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchProductIdDiscount.IsChecked == true)
            {
                textBoxSearchProductIdDiscount.IsEnabled = true;
                textBoxSearchProductDiscount.IsEnabled = false;
                checkBoxSearchProductDiscount.IsEnabled = false;
            }
            else
            {
                checkBoxSearchProductDiscount.IsEnabled = true;
                textBoxSearchProductIdDiscount.IsEnabled = false;
            }
        }

        private void checkBoxSearchProductDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchProductDiscount.IsChecked == true)
            {
                textBoxSearchProductDiscount.IsEnabled = true;
                textBoxSearchProductIdDiscount.IsEnabled = false;
                checkBoxSearchProductIdDiscount.IsEnabled = false;
            }
            else
            {
                checkBoxSearchProductIdDiscount.IsEnabled = true;
                textBoxSearchProductDiscount.IsEnabled = false;
            }
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
        }

        private void checkBoxSearchNameEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchNameEmployee.IsChecked == true)
            {
                textBoxSearchNameEmployee.IsEnabled = true;
            }
            else
            {
                textBoxSearchNameEmployee.Text = null;
                textBoxSearchNameEmployee.IsEnabled = false;
            }
        }

        private void checkBoxSearchPosEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchPosEmployee.IsChecked == true)
            {
                comboBoxSearchPosEmployee.IsEnabled = true;
            }
            else
            {
                comboBoxSearchPosEmployee.SelectedIndex = -1;
                comboBoxSearchPosEmployee.IsEnabled = false;
            }
        }

        private void checkBoxSearchContractEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchContractEmployee.IsChecked == true)
            {
                textBoxSearchContractEmployee.IsEnabled = true;
            }
            else
            {
                textBoxSearchContractEmployee.Text = null;
                textBoxSearchContractEmployee.IsEnabled = false;
            }
        }

        private void checkBoxSearchTelEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchTelEmployee.IsChecked == true)
            {
                textBoxSearchTelEmployee.IsEnabled = true;
            }
            else
            {
                textBoxSearchTelEmployee.Text = null;
                textBoxSearchTelEmployee.IsEnabled = false;
            }
        }

        private void checkBoxSearchCodeEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCodeEmployee.IsChecked == true)
            {
                textBoxSearchCodeEmployee.IsEnabled = true;
            }
            else
            {
                textBoxSearchCodeEmployee.Text = null;
                textBoxSearchCodeEmployee.IsEnabled = false;
            }
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
            if (checkBoxSearchNameManufacturer.IsChecked == true)
            {
                textBoxSearchNameManufacturer.IsEnabled = true;
            }
            else
            {
                textBoxSearchNameManufacturer.Text = null;
                textBoxSearchNameManufacturer.IsEnabled = false;
            }
        }

        private void checkBoxSearchCountryManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCountryManufacturer.IsChecked == true)
            {
                textBoxSearchCountryManufacturer.IsEnabled = true;
            }
            else
            {
                textBoxSearchCountryManufacturer.Text = null;
                textBoxSearchCountryManufacturer.IsEnabled = false;
            }
        }

        private void checkBoxSearchCityManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCityManufacturer.IsChecked == true)
            {
                textBoxSearchCityManufacturer.IsEnabled = true;
            }
            else
            {
                textBoxSearchCityManufacturer.Text = null;
                textBoxSearchCityManufacturer.IsEnabled = false;
            }
        }

        private void checkBoxSearchAddressManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchAddressManufacturer.IsChecked == true)
            {
                textBoxSearchAddressManufacturer.IsEnabled = true;
            }
            else
            {
                textBoxSearchAddressManufacturer.Text = null;
                textBoxSearchAddressManufacturer.IsEnabled = false;
            }
        }

        private void checkBoxSearchTelManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchTelManufacturer.IsChecked == true)
            {
                textBoxSearchTelManufacturer.IsEnabled = true;
            }
            else
            {
                textBoxSearchTelManufacturer.Text = null;
                textBoxSearchTelManufacturer.IsEnabled = false;
            }
        }

        private void checkBoxSearchCodeManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCodeManufacturer.IsChecked == true)
            {
                textBoxSearchCodeManufacturer.IsEnabled = true;
            }
            else
            {
                textBoxSearchCodeManufacturer.Text = null;
                textBoxSearchCodeManufacturer.IsEnabled = false;
            }
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
            if (checkBoxSearchNameProduct.IsChecked == true)
            {
                textBoxSearchNameProduct.IsEnabled = true;
            }
            else
            {
                textBoxSearchNameProduct.Text = null;
                textBoxSearchNameProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchManufacturerProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchManufacturerProduct.IsChecked == true)
            {
                textBoxSearchManufacturerProduct.IsEnabled = true;
            }
            else
            {
                textBoxSearchManufacturerProduct.Text = null;
                textBoxSearchManufacturerProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchGroupProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchGroupProduct.IsChecked == true)
            {
                comboBoxSearchGroupProduct.IsEnabled = true;
            }
            else
            {
                comboBoxSearchGroupProduct.SelectedIndex = -1;
                comboBoxSearchGroupProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchPackProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchPackProduct.IsChecked == true)
            {
                comboBoxSearchPackProduct.IsEnabled = true;
            }
            else
            {
                comboBoxSearchPackProduct.SelectedIndex = -1;
                comboBoxSearchPackProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchMaterialProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchMaterialProduct.IsChecked == true)
            {
                comboBoxSearchMaterialProduct.IsEnabled = true;
            }
            else
            {
                comboBoxSearchMaterialProduct.SelectedIndex = -1;
                comboBoxSearchMaterialProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchFormProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchFormProduct.IsChecked == true)
            {
                comboBoxSearchFormProduct.IsEnabled = true;
            }
            else
            {
                comboBoxSearchFormProduct.SelectedIndex = -1;
                comboBoxSearchFormProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchPriceProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchPriceProduct.IsChecked == true)
            {
                upDownSearchPriceProduct.IsEnabled = true;
            }
            else
            {
                upDownSearchPriceProduct.Text = null;
                upDownSearchPriceProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchCodeProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCodeProduct.IsChecked == true)
            {
                textBoxSearchCodeProduct.IsEnabled = true;
            }
            else
            {
                textBoxSearchCodeProduct.Text = null;
                textBoxSearchCodeProduct.IsEnabled = false;
            }
        }

        private void checkBoxSearchValueProduct_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchValueProduct.IsChecked == true)
            {
                upDownSearchValueProduct.IsEnabled = true;
            }
            else
            {
                upDownSearchValueProduct.Text = null;
                upDownSearchValueProduct.IsEnabled = false;
            }
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
            if (checkBoxSearchBDateWaybill.IsChecked == true)
            {
                datePickerSearchBDateWaybill.IsEnabled = true;
                if (checkBoxSearchEDateWaybill.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeWaybill.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchBDateWaybill.Text = null;
                datePickerSearchBDateWaybill.IsEnabled = false;
                comboBoxSearchDateRangeTypeWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchEDateWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEDateWaybill.IsChecked == true)
            {
                datePickerSearchEDateWaybill.IsEnabled = true;
                if (checkBoxSearchBDateWaybill.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeWaybill.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchEDateWaybill.Text = null;
                datePickerSearchEDateWaybill.IsEnabled = false;
                comboBoxSearchDateRangeTypeWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchEmployeeWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEmployeeWaybill.IsChecked == true)
            {
                textBoxSearchEmployeeWaybill.IsEnabled = true;
            }
            else
            {
                textBoxSearchEmployeeWaybill.Text = null;
                textBoxSearchEmployeeWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchAgentWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchAgentWaybill.IsChecked == true)
            {
                textBoxSearchAgentWaybill.IsEnabled = true;
            }
            else
            {
                textBoxSearchAgentWaybill.Text = null;
                textBoxSearchAgentWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchPriceWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchPriceWaybill.IsChecked == true)
            {
                upDownSearchPriceWaybill.IsEnabled = true;
            }
            else
            {
                upDownSearchPriceWaybill.Text = null;
                upDownSearchPriceWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchCodeWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchCodeWaybill.IsChecked == true)
            {
                textBoxSearchCodeWaybill.IsEnabled = true;
            }
            else
            {
                textBoxSearchCodeWaybill.Text = null;
                textBoxSearchCodeWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchBDateProductWaybillIn_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBDateProductWaybillIn.IsChecked == true)
            {
                datePickerSearchBDateProductWaybillIn.IsEnabled = true;
                if (checkBoxSearchEDateProductWaybillIn.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeProductWaybillIn.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchBDateProductWaybillIn.Text = null;
                datePickerSearchBDateProductWaybillIn.IsEnabled = false;
                comboBoxSearchDateRangeTypeProductWaybillIn.IsEnabled = false;
            }
        }

        private void checkBoxSearchEDateProductWaybillIn_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEDateProductWaybillIn.IsChecked == true)
            {
                datePickerSearchEDateProductWaybillIn.IsEnabled = true;
                if (checkBoxSearchBDateProductWaybillIn.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeProductWaybillIn.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchEDateProductWaybillIn.Text = null;
                datePickerSearchEDateProductWaybillIn.IsEnabled = false;
                comboBoxSearchDateRangeTypeProductWaybillIn.IsEnabled = false;
            }
        }

        private void checkBoxSearchProductWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchProductWaybill.IsChecked == true)
            {
                textBoxSearchProductWaybill.IsEnabled = true;
                textBoxSearchProductIdWaybill.IsEnabled = false;
                checkBoxSearchProductIdWaybill.IsEnabled = false;
            }
            else
            {
                checkBoxSearchProductIdWaybill.IsEnabled = true;
                textBoxSearchProductWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchProductIdWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchProductIdWaybill.IsChecked == true)
            {
                textBoxSearchProductIdWaybill.IsEnabled = true;
                textBoxSearchProductWaybill.IsEnabled = false;
                checkBoxSearchProductWaybill.IsEnabled = false;
            }
            else
            {
                checkBoxSearchProductWaybill.IsEnabled = true;
                textBoxSearchProductIdWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchValueProductWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchValueProductWaybill.IsChecked == true)
            {
                upDownSearchValueProductWaybill.IsEnabled = true;
            }
            else
            {
                upDownSearchValueProductWaybill.Text = null;
                upDownSearchValueProductWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchPriceProductWaybill_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchPriceProductWaybill.IsChecked == true)
            {
                upDownSearchPriceProductWaybill.IsEnabled = true;
            }
            else
            {
                upDownSearchPriceProductWaybill.Text = null;
                upDownSearchPriceProductWaybill.IsEnabled = false;
            }
        }

        private void checkBoxSearchBDateProductWaybillOut_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchBDateProductWaybillOut.IsChecked == true)
            {
                datePickerSearchBDateProductWaybillOut.IsEnabled = true;
                if (checkBoxSearchEDateProductWaybillOut.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeProductWaybillOut.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchBDateProductWaybillOut.Text = null;
                datePickerSearchBDateProductWaybillOut.IsEnabled = false;
                comboBoxSearchDateRangeTypeProductWaybillOut.IsEnabled = false;
            }
        }

        private void checkBoxSearchEDateProductWaybillOut_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEDateProductWaybillOut.IsChecked == true)
            {
                datePickerSearchEDateProductWaybillOut.IsEnabled = true;
                if (checkBoxSearchBDateProductWaybillOut.IsChecked == true)
                {
                    comboBoxSearchDateRangeTypeProductWaybillOut.IsEnabled = true;
                }
            }
            else
            {
                datePickerSearchEDateProductWaybillOut.Text = null;
                datePickerSearchEDateProductWaybillOut.IsEnabled = false;
                comboBoxSearchDateRangeTypeProductWaybillOut.IsEnabled = false;
            }
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