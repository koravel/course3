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
        List<Check> check = new List<Check> { };
        List<Discount> discount = new List<Discount> { };
        List<Employee> employee = new List<Employee> { };
        List<Manufacturer> manufacturer = new List<Manufacturer> { };
        List<Product> product = new List<Product> { };
        List<Waybill> waybill = new List<Waybill> { };
        static string idText,temp;
        bool flag,intitalizeFlag = false;
        List<CheckList> checkList = new List<CheckList> { };
        List<WaybillList> waybillList = new List<WaybillList> { };
        List<string> valuesText = new List<string>(), values = new List<string>();
        public delegate List<T> GetTableDelegate<T>(string query, string[] values, string[] valuesText);
        public AdminWindow(string id)
        {
            string type = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { id }, "SELECT U_TYPE FROM `user` WHERE U_ID=@_id;");
            if (type != "Администратор" && type != "Директор")
            {
                System.Windows.MessageBox.Show("Вход запрещен!");
                this.Close();
            }
            idText = id;
            InitializeComponent();
            DataBase.SetLog(idText, 1, 0, "Вход в систему...");
            check = DataBase.GetCheck();
            if(check.Count > 0)
            {
                dataGridCheckOut.ItemsSource = check;
                GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
                DataBase.SetLog(idText, 0, 0, "Заполнение таблицы чеков...");
            }
            discount = DataBase.GetDiscount();
            if(discount.Count > 0)
            {
                dataGridDiscountOut.ItemsSource = discount;
                DataBase.SetLog(idText, 0, 0, "Заполнение таблицы скидок...");
            }
            manufacturer = DataBase.GetManufacturer();
            if(manufacturer.Count > 0)
            {
                dataGridManufacturersOut.ItemsSource = manufacturer;
                DataBase.SetLog(idText, 0, 0, "Заполнение таблицы производителей...");
            }
            product = DataBase.GetProduct();
            if(product.Count > 0)
            {
                dataGridProductOut.ItemsSource = product;
                GetSubTable<ProductActualPrice>(dataGridProductActPriceOut, dataGridProductOut);
                DataBase.SetLog(idText, 0, 0, "Заполнение таблицы товаров...");

            }
            employee = DataBase.GetEmployee();
            if(employee.Count > 0)
            {
                dataGridEmployeeOut.ItemsSource = employee;
                if (dataGridEmployeeOut.Items.Count > 0)
                {
                    dataGridEmployeeOut.SelectedIndex = 0;
                    CalculateEmployeesCount(0);
                }
                DataBase.SetLog(idText, 0, 0, "Заполнение таблицы работников...");
            }
            waybill = DataBase.GetWaybill();
            if(waybill.Count > 0)
            {
                dataGridWaybillOut.ItemsSource = waybill;
                GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
                DataBase.SetLog(idText, 0, 0, "Заполнение таблицы накладных...");
            }
            valuesText.Clear();
            values.Clear();
            intitalizeFlag = true;
        }

        private void CalculateTotalPrice(string curid)
        {
            string totalPrice = DataBase.QueryRetCell(new string[] { "@_curid" }, new string[] { curid }, "SELECT C_SUM FROM `check` where C_ID=@_curid;");
            totalPriceTextBlock.Text = "Общая цена:";
            if (totalPrice == null)
            {
                totalPriceTextBlock.Text += "0";
            }
            else
            {
                totalPriceTextBlock.Text += totalPrice;
            }
        }

        private void CalculateProductCount(string curid)
        {
            DataBase.Query(new string[] { "@_id" }, new string[] { curid }, "UPDATE product_overdue po,waybill_list wl SET po.PP_IS_OVERDUE=IF((wl.WL_EDATE-14)>DATE(NOW())-0,IF((SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=0,'Продано','Не просрочено'),IF((WL_EDATE)<DATE(NOW()),IF((SELECT wl.WL_VALUE-product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=0,'Продано','Просрочено'),'Скоро истекает срок годности'))WHERE po.PP_IS_OVERDUE<>'Просрочено' AND po.PP_IS_OVERDUE<>'Продано' AND po.WL_ID=wl.WL_ID AND wl.P_ID=@_id AND wl.WL_ID>0;");
            string quantity = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { curid }, "SELECT IFNULL(SUM(waybill_list.WL_VALUE-product_sold.PS_COUNT),0) FROM product_sold,waybill_list WHERE product_sold.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=@_id;"),
            overdueValue = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { curid }, "SELECT IFNULL(sum(waybill_list.WL_VALUE-product_sold.PS_COUNT),0) FROM waybill_list,product_sold,product_overdue WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_sold.WL_ID=waybill_list.WL_ID AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=@_id;"),
            actualValue = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { curid }, "SELECT IFNULL(sum(waybill_list.WL_VALUE-product_sold.PS_COUNT),0) FROM waybill_list,product_sold,product_overdue WHERE product_overdue.PP_IS_OVERDUE='Не просрочено' AND product_sold.WL_ID=waybill_list.WL_ID AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=@_id;");
            textBlockProductCount.Text = "Всего на складе:";
            if (quantity == null)
            {
                textBlockProductCount.Text += "0";
            }
            else
            {
                textBlockProductCount.Text += quantity;
            }
            textBlockProductCount.Text += "   Просрочено:";
            if (overdueValue == null)
            {
                textBlockProductCount.Text += "0";
            }
            else
            {
                textBlockProductCount.Text += overdueValue;
            }
            textBlockProductCount.Text += "   Не просрочено:";
            if (actualValue == null)
            {
                textBlockProductCount.Text += "0";
            }
            else
            {
                textBlockProductCount.Text += actualValue;
            }
        }

        private void CalculateWaybillCount(string curid)
        {
            string totalTradePrice = DataBase.QueryRetCell(new string[] { "@_curid" }, new string[] { curid }, "SELECT SUM(waybill_list.WL_TRADE_PRICE*waybill_list.WL_VALUE) FROM waybill_list WHERE waybill_list.W_ID=@_curid;");
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

        private void CalculateEmployeesCount(int curid)
        {
            if (dataGridEmployeeOut.Items.Count > 0)
            {
                if (dataGridEmployeeOut.SelectedIndex != -1)
                {
                    string professionName = ((Employee)dataGridEmployeeOut.Items[curid]).POSITION;
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
            else
            {
                textBlockTypeCount.Text = null;
            }
        }

        private void GetSubTable<T>(DataGrid subDataGrid, DataGrid dataGrid)
        {

            if (dataGrid.Items.IsEmpty == false)
            {
                dataGrid.SelectedIndex = 0;
                string id = null,temp = typeof(T).ToString();
                string[] type = temp.Split('.');
                switch (type[1])
                {
                    case "CheckList":
                        {
                            id = ((Check)dataGrid.Items[0]).ID.ToString();
                            checkList = DataBase.GetCheckList("SELECT distinct product.P_ID,CONCAT( product.P_NAME,\"(#\",product.P_ID,\")\") AS 'P_NAME',cl.CL_VALUE,product_actual_price.PAP_PRICE FROM check_list cl,product,`check` c,product_actual_price WHERE cl.P_ID=product_actual_price.P_ID AND cl.P_ID=product.P_ID AND cl.C_ID=c.C_ID AND product_actual_price.PAP_PRICE=ifnull((select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID and pap.PAP_DATE<=c.C_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1)) AND c.C_ID=@_curid;", new string[] { "@_curid" }, new string[] { id });
                            if(checkList.Count > 0)
                            {
                                subDataGrid.ItemsSource = checkList;
                                CalculateTotalPrice(id);
                            }
                            break;
                        }
                    case "WaybillList":
                        {
                            id = ((Waybill)dataGrid.Items[0]).ID.ToString();
                            waybillList = DataBase.GetWaybillList(new string[]{"@_curid"},new string[]{ id },"SELECT product.P_ID,product.P_NAME,waybill_list.WL_VALUE,waybill_list.WL_TRADE_PRICE,waybill_list.WL_BDATE,waybill_list.WL_EDATE FROM `waybill`,`waybill_list`,`product` WHERE `waybill_list`.`W_ID`=`waybill`.`W_ID` AND `waybill_list`.`P_ID`=`product`.`P_ID` AND waybill_list.W_ID=@_curid;");
                            if(waybillList.Count > 0)
                            {
                                subDataGrid.ItemsSource = waybillList;
                                CalculateWaybillCount(id);
                            }
                            break;
                        }
                    case "ProductActualPrice":
                        {
                            id = ((Product)dataGrid.Items[0]).ID.ToString();
                            List<ProductActualPrice> tempList = DataBase.GetProductActualPrice(id);
                            if(tempList.Count > 0)
                            {
                                subDataGrid.ItemsSource = tempList;
                                CalculateProductCount(id);
                            }
                            break;
                        }
                }
            }
            else
            {
                subDataGrid.ItemsSource = null;
            }
        }

        private void UsersControl_Click(object sender, RoutedEventArgs e)
        {
            new UsersControlWindow(idText).ShowDialog();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch(selected)
            {
                case 0:
                    {
                        check = DataBase.GetCheck();
                        if(check.Count > 0)
                        {
                            dataGridCheckOut.ItemsSource = check;
                            GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
                        }
                        break;
                    }
                case 1:
                    {
                        discount = DataBase.GetDiscount();
                        if(discount.Count > 0)
                        {
                            dataGridDiscountOut.ItemsSource = discount;
                        }
                        break;
                    }
                case 2:
                    {
                        employee = DataBase.GetEmployee();
                        if(employee.Count > 0)
                        {
                            dataGridEmployeeOut.ItemsSource = employee;
                            if (dataGridEmployeeOut.Items.Count > 0)
                            {
                                dataGridEmployeeOut.SelectedIndex = 0;
                                CalculateEmployeesCount(0);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        manufacturer = DataBase.GetManufacturer();
                        if (manufacturer.Count > 0)
                        {
                            dataGridManufacturersOut.ItemsSource = manufacturer;
                        }
                        break;
                    }
                case 4:
                    {
                        product = DataBase.GetProduct();
                        if (product.Count > 0)
                        {
                            dataGridProductOut.ItemsSource = product;
                            GetSubTable<ProductActualPrice>(dataGridProductActPriceOut, dataGridProductOut);
                        }
                        break;
                    }
                case 5:
                    {
                        waybill = DataBase.GetWaybill();
                        if(waybill.Count > 0)
                        {
                            dataGridWaybillOut.ItemsSource = waybill;
                            GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
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
            DataBase.SetLog(idText, 1, 0, "Выход из системы...");
            new MainWindow().Show();
        }

        private void dataGridCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridCheckOut.SelectedIndex != -1 && dataGridCheckOut.Items.Count >0)
            {
                string checkId = Converter.DGCellToStringConvert(dataGridCheckOut.SelectedIndex, 3, dataGridCheckOut);
                if(checkId != null)
                {
                    checkList = DataBase.GetCheckList("SELECT distinct product.P_ID,CONCAT( product.P_NAME,\"(#\",product.P_ID,\")\") AS 'P_NAME',cl.CL_VALUE,product_actual_price.PAP_PRICE FROM check_list cl,product,`check` c,product_actual_price WHERE cl.P_ID=product_actual_price.P_ID AND cl.P_ID=product.P_ID AND cl.C_ID=c.C_ID AND product_actual_price.PAP_PRICE=ifnull((select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID and pap.PAP_DATE<=c.C_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1)) AND c.C_ID=@_curid;", new string[] { "@_curid" }, new string[] { checkId });
                    dataGridCheckListOut.ItemsSource = checkList;
                    CalculateTotalPrice(checkId);
                }
            }
        }

        private void dataGridProductOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridProductOut.SelectedIndex != -1 && dataGridProductOut.Items.Count > 0)
                {
                    string productId = Converter.DGCellToStringConvert(dataGridProductOut.SelectedIndex, 8, dataGridProductOut);
                    List<ProductActualPrice> productActualPrice = DataBase.GetProductActualPrice(productId);

                    if (productActualPrice.Count > 0)
                    {
                        dataGridProductActPriceOut.ItemsSource = productActualPrice; 
                        CalculateProductCount(productId);
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
                if (dataGridWaybillOut.SelectedIndex != -1 && dataGridWaybillOut.Items.Count > 0)
                {
                    string waybillId = Converter.DGCellToStringConvert(dataGridWaybillOut.SelectedIndex, 3, dataGridWaybillOut);
                    waybillList = DataBase.GetWaybillList(new string[]{"@_curid"},new string[]{ waybillId },"SELECT product.P_ID,product.P_NAME,waybill_list.WL_VALUE,waybill_list.WL_TRADE_PRICE,waybill_list.WL_BDATE,waybill_list.WL_EDATE FROM `waybill`,`waybill_list`,`product` WHERE `waybill_list`.`W_ID`=`waybill`.`W_ID` AND `waybill_list`.`P_ID`=`product`.`P_ID` AND waybill_list.W_ID=@_curid;");
                    if (waybillList.Count > 0)
                    {
                        dataGridWaybillListOut.ItemsSource = waybillList;
                        CalculateWaybillCount(waybillId);
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
            switch (tabControlTables.SelectedIndex)
            {
                case 0:
                    {
                        System.Windows.MessageBox.Show("Добавление запрещено.");
                        break;
                    }
                case 1:
                    {
                        DiscountAddWindow window = new DiscountAddWindow(idText);
                        window.ShowDialog();
                        if (window.flag == true)
                        {
                            discount.Add(window.obj);
                            dataGridDiscountOut.Items.Refresh();
                        }
                        break;
                    }
                case 2:
                    {
                        EmployeeAddWindow window = new EmployeeAddWindow(idText);
                        window.ShowDialog();
                        if (window.flag == true)
                        {
                            employee.Add(window.obj);
                            dataGridEmployeeOut.Items.Refresh();
                        }
                        break;
                    }
                case 3:
                    {
                        ManufacturerAddWindow window = new ManufacturerAddWindow(idText);
                        window.ShowDialog();
                        if (window.flag == true)
                        {
                            manufacturer.Add(window.obj);
                            dataGridManufacturersOut.Items.Refresh();
                        }
                        break;
                    }
                case 4:
                    {
                        ProductAddWindow window = new ProductAddWindow(idText);
                        window.ShowDialog();
                        if (window.flag == true)
                        {
                            product.Add(window.obj);
                            dataGridProductOut.Items.Refresh();
                        }
                        break;
                    }
                case 5:
                    {
                        WaybillAddWindow window = new WaybillAddWindow(idText);
                        window.ShowDialog();
                        if (window.flag1 == true)
                        {
                            waybill.Add(window.obj);
                            dataGridWaybillOut.Items.Refresh();
                        }
                        break;
                    }
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            int selected = tabControlTables.SelectedIndex;
            switch (selected)
            {
                case 0:
                    {
                        DirectorPassWindow window = new DirectorPassWindow();
                        window.ShowDialog();
                        if(window.flag)
                        {
                            BackupBinds(0, ((Check)(dataGridCheckOut.SelectedItem)).ID);
                            Check tempobj = DeleteRow<Check>(dataGridCheckOut, true, check, -1, "DELETE FROM `check` WHERE C_ID", "Удаление чека,код=", 0, 3, "SELECT `check`.`C_ID`,`check`.`E_ID`,`check`.`C_DATE`,`check`.`C_PAYTYPE`,`check`.`C_SUM`,`check`.`C_PREPAYMENT` FROM `check` WHERE C_ID=@_id;", DataBase.GetFullCheck);
                            if (tempobj != null)
                            {
                                Files.SaveToArchive(tempobj.ID.ToString(), "\nКод:" + tempobj.ID + "\nКассир:" + tempobj.NAME + "\nДата:" + tempobj.DATE + "\nТип оплаты:" + tempobj.PAYTYPE + "\nСумма:" + tempobj.SUM + "\nАванс:" + tempobj.PREPAYMENT,"Check");
                            }
                            GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
                        }
                        break;
                    }
                case 1:
                    {
                        Discount tempobj = DeleteRow<Discount>(dataGridDiscountOut, true, discount, -1, "DELETE FROM `discounts` WHERE D_ID", "Удаление акции,код=", 0, 0, "SELECT `discounts`.`D_ID`,`discounts`.`P_ID`,`discounts`.`D_PRICE`,`discounts`.`D_BDATE`,`discounts`.`D_EDATE`,`discounts`.`D_TEXT` FROM `discounts` WHERE D_ID=@_id;", DataBase.GetDiscount);
                        if (tempobj != null)
                        {
                            Files.SaveToArchive(tempobj.ID.ToString(), "\nКод:" + tempobj.ID + "\nТовар:" + tempobj.NAME + "\nЦена:" + tempobj.PRICE + "\nНачало:" + tempobj.BDATE + "\nКонец:" + tempobj.EDATE + "\nОписание:" + tempobj.TEXT,"Discount");
                        }
                        break;
                    }
                case 2:
                    {
                        BackupBinds(2, ((Employee)(dataGridEmployeeOut.SelectedItem)).ID);
                        Employee tempobj = DeleteRow<Employee>(dataGridEmployeeOut, Properties.Settings.Default.DelBindingToEmployee, employee, 0, "DELETE FROM `employee` WHERE E_ID", "Удаление работника,код=", 1, 5, "SELECT `employee`.`E_ID`,`employee`.`E_NAME`,`employee`.`E_TEL`,`employee`.`E_POSITION`,`employee`.`E_CONTRACT`,`employee`.`E_INN` FROM `employee` WHERE E_ID=@_id;", DataBase.GetEmployee);
                        if (tempobj != null)
                        {
                            Files.SaveToArchive(tempobj.ID.ToString(), "\nКод:" + tempobj.ID + "\nФ.И.О." + tempobj.NAME + "\nДолжость:" + tempobj.POSITION + "\nНомер контракта:" + tempobj.CONTRACT + "\nИНН:" + tempobj.INN + "\nТелефон:" + tempobj.TEL,"Employee");
                        }
                        textBlockTypeCount.Text = "";
                        check = DataBase.GetCheck();
                        if(check.Count > 0)
                        {
                            dataGridCheckOut.ItemsSource = check;
                            GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
                        }
                        waybill = DataBase.GetWaybill();
                        if(waybill.Count > 0)
                        {
                            dataGridWaybillOut.ItemsSource = waybill;
                            GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
                        }
                        break;
                    }
                case 3:
                    {
                        BackupBinds(1, ((Manufacturer)(dataGridManufacturersOut.SelectedItem)).ID);
                        Manufacturer tempobj = DeleteRow<Manufacturer>(dataGridManufacturersOut, Properties.Settings.Default.DelBindingToManufacturer, manufacturer, 1, "DELETE FROM `manufacturer` WHERE M_ID", "Удаление производителя,код=", 1, 5, "SELECT `manufacturer`.`M_ID`,`manufacturer`.`M_NAME`,`manufacturer`.`M_COUNTRY`,`manufacturer`.`M_CITY`,`manufacturer`.`M_ADDR`,`manufacturer`.`M_TEL` FROM `manufacturer` WHERE M_ID=@_id;", DataBase.GetManufacturer);
                        if (tempobj != null)
                        {
                            Files.SaveToArchive(tempobj.ID.ToString(), "\nКод:" + tempobj.ID + "\nНазвание:" + tempobj.NAME + "\nСтрана:" + tempobj.COUNTRY + "\nГород:" + tempobj.CITY + "\nАдрес:" + tempobj.ADDR + "\nТелефон:" + tempobj.TEL,"Manufacturer");
                        }
                        check = DataBase.GetCheck();
                        if (check.Count > 0)
                        {
                            dataGridCheckOut.ItemsSource = check;
                            GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
                        }
                        discount = DataBase.GetDiscount();
                        if(discount.Count > 0)
                        {
                            dataGridDiscountOut.ItemsSource = discount;
                        }
                        product = DataBase.GetProduct();
                        if(product.Count > 0)
                        {
                            dataGridProductOut.ItemsSource = product;
                            GetSubTable<ProductActualPrice>(dataGridProductActPriceOut, dataGridProductOut);
                        }
                        waybill = DataBase.GetWaybill();
                        if (waybill.Count > 0)
                        {
                            dataGridWaybillOut.ItemsSource = waybill;
                            GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
                        }
                        break;
                    }
                case 4:
                    {
                        BackupBinds(3, ((Product)(dataGridProductOut.SelectedItem)).ID);
                        Product tempobj = DeleteRow<Product>(dataGridProductOut, Properties.Settings.Default.DelBindingToProduct1, product, 2, "DELETE FROM `product` WHERE P_ID", "Удаление товара,код=", 1, 8, "SELECT `product`.`P_ID`,`product`.`P_NAME`,`product`.`M_ID`,`product`.`P_GROUP`,`product`.`P_PACK`,`product`.`P_MATERIAL`,`product`.`P_FORM`,`product`.`P_INSTR`,`product`.`P_CODE` FROM `product` WHERE P_ID=@_id;", DataBase.GetProduct);
                        if (tempobj != null)
                        {
                            string text = "";
                            string[] tempProductQuantity = DataBase.QueryRetRow(new string[] { "@_id" }, new string[] { tempobj.ID.ToString() }, "SELECT PQ_IN,PQ_OUT FROM product_quantity WHERE P_ID=@_id;");
                            if (tempProductQuantity.Length > 0)
                            {
                                text = "\n\tВсего пришло:" + tempProductQuantity[0] + "\n\tВсего продано:" + tempProductQuantity[1];
                            }
                            Files.SaveToArchive(tempobj.ID.ToString(), "\nКод:" + tempobj.ID + "\nНазвание:" + tempobj.NAME + "\nПроизводитель:" + tempobj.MANUFACTURER + "\nФорма отпуска:" + tempobj.FORM + "\nГруппа:" + tempobj.GROUP + "\nМатериал:" + tempobj.MATERIAL + "\nУпаковка" + tempobj.PACK + "\nИнструкция" + tempobj.INSTR+text,"Product");
                        }
                        dataGridProductActPriceOut.ItemsSource = null;
                        GetSubTable<ProductActualPrice>(dataGridProductActPriceOut, dataGridProductOut);
                        check = DataBase.GetCheck();
                        if (check.Count > 0)
                        {
                            dataGridCheckOut.ItemsSource = check;
                            GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
                        }
                        discount = DataBase.GetDiscount();
                        if (discount.Count > 0)
                        {
                            dataGridDiscountOut.ItemsSource = discount;
                        }
                        waybill = DataBase.GetWaybill();
                        if (waybill.Count > 0)
                        {
                            dataGridWaybillOut.ItemsSource = waybill;
                            GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
                        }
                        break;
                    }
                case 5:
                    {
                        DirectorPassWindow window = new DirectorPassWindow();
                        window.ShowDialog();
                        if (window.flag)
                        {
                            BackupBinds(4, ((Waybill)(dataGridWaybillOut.SelectedItem)).ID);
                            Waybill tempobj = DeleteRow<Waybill>(dataGridWaybillOut, Properties.Settings.Default.DelBindingToWaybill, waybill, 3, "DELETE FROM `waybill` WHERE W_ID", "Удаление накладной,код=", 1, 3, "SELECT `waybill`.`W_ID`,`waybill`.`W_DATE`,`waybill`.`E_ID`,`waybill`.`W_AGENT_NAME` FROM `waybill`;", DataBase.GetWaybill);
                            if (tempobj != null)
                            {
                                Files.SaveToArchive(tempobj.ID.ToString(), "\nКод:" + tempobj.ID + "\nДата составления:" + tempobj.DATE + "\nПринимающий:" + tempobj.EMPLOYEE + "\nКонтрагент:" + tempobj.AGENT,"Waybill");
                            }
                            GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
                        }
                        break;
                    }
            }
        }

        public void BackupBinds(int table,int index)
        {
            switch(table)
            {
                case 0:
                    {
                        CycleBackupCheclList(index,"c.C_ID");
                        break;
                    }
                case 1:
                    {
                        List<Product> tempProductList = DataBase.GetProduct("SELECT `product`.`P_ID`,`product`.`P_NAME`,`product`.`M_ID`,`product`.`P_GROUP`,`product`.`P_PACK`,`product`.`P_MATERIAL`,`product`.`P_FORM`,`product`.`P_INSTR`,`product`.`P_CODE` FROM `product` WHERE M_ID=@_id;", new string[] { "@_id" }, new string[] { index.ToString() });
                        if (tempProductList.Count > 0)
                        {
                            for (int i = 0; i < tempProductList.Count; i++)
                            {
                                System.IO.StreamWriter w = new System.IO.StreamWriter(System.IO.File.Create(@"" + Properties.Settings.Default.SaveArchive + "\\" + "Product" + index.ToString() + ".txt"), Encoding.GetEncoding(1251));
                                w.WriteLine("\nКод:" + tempProductList[i].ID + "\nНазвание:" + tempProductList[i].NAME + "\nПроизводитель:" + tempProductList[i].MANUFACTURER + "\nФорма отпуска:" + tempProductList[i].FORM + "\nГруппа:" + tempProductList[i].GROUP + "\nМатериал:" + tempProductList[i].MATERIAL + "\nпаковка" + tempProductList[i].PACK + "\nИнструкция" + tempProductList[i].INSTR);
                                string[] tempProductQuantity = DataBase.QueryRetRow(new string[]{ "@_id" }, new string[]{ tempProductList[i].ID.ToString() }, "SELECT PQ_IN,PQ_OUT FROM product_quantity WHERE P_ID=@_id");
                                if(tempProductQuantity.Length > 0)
                                {
                                    w.WriteLine("\n\tВсего пришло:"+tempProductQuantity[0]+"\n\tВсего продано:"+tempProductQuantity[1]);
                                }
                                CycleBackupPrice(tempProductList[i].ID);
                                CycleBackupDiscount(tempProductList[i].ID);
                                CycleBackupWaybillList(tempProductList[i].ID,"product.P_ID");
                                w.Close();
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        List<Check> tempCheck = DataBase.GetCheck("SELECT `check`.`C_ID`,`check`.`C_DATE`,`check`.`C_PAYTYPE`,`check`.`E_ID`,`check`.`C_SUM`,`check`.`C_PREPAYMENT` FROM `check` WHERE E_ID=@_id", new string[] { "@_id" }, new string[] { index.ToString() });
                        if (tempCheck.Count > 0)
                        {
                            for (int i = 0; i < tempCheck.Count;i++ )
                            {
                                Files.SaveToArchive(tempCheck[i].ID.ToString(), "\nКод:" + tempCheck[i].ID + "\nКассир:" + tempCheck[i].NAME + "\nДата:" + tempCheck[i].DATE + "\nТип оплаты:" + tempCheck[i].PAYTYPE + "\nСумма:" + tempCheck[i].SUM + "\nАванс:" + tempCheck[i].PREPAYMENT, "Check");
                                CycleBackupCheclList(tempCheck[i].ID,"c.C_ID");
                            }
                        }
                        List<Waybill> tempWaybill = DataBase.GetWaybill("SELECT `waybill`.`W_ID`,`waybill`.`W_DATE`,`waybill`.`E_ID`,`waybill`.`W_AGENT_NAME` FROM `waybill` WHERE E_ID=@_id", new string[] { "@_id" }, new string[] { index.ToString() });
                        if(tempWaybill.Count > 0)
                        {
                            for (int i = 0; i < tempWaybill.Count; i++)
                            {
                                Files.SaveToArchive(tempWaybill[i].ID.ToString(), "\nКод:" + tempWaybill[i].ID + "\nДата составления:" + tempWaybill[i].DATE + "\nПринимающий:" + tempWaybill[i].EMPLOYEE + "\nКонтрагент:" + tempWaybill[i].AGENT, "Waybill");
                                CycleBackupWaybillList(tempWaybill[i].ID,"waybill.W_ID");
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        CycleBackupPrice(index);
                        CycleBackupDiscount(index);
                        CycleBackupWaybillList(index,"product.P_ID");
                        CycleBackupCheclList(index, "cl.P_ID");
                        break;
                    }
                case 4:
                    {
                        CycleBackupWaybillList(index,"waybill.W_ID");
                        break;
                    }
            }
        }

        private void CycleBackupCheclList(int id,string queryPart)
        {
            List<CheckList> tempList = DataBase.GetCheckList("SELECT distinct product.P_ID,CONCAT( product.P_NAME,\"(#\",product.P_ID,\")\") AS 'P_NAME',cl.CL_VALUE,product_actual_price.PAP_PRICE FROM check_list cl,product,`check` c,product_actual_price WHERE cl.P_ID=product_actual_price.P_ID AND cl.P_ID=product.P_ID AND cl.C_ID=c.C_ID AND product_actual_price.PAP_PRICE=ifnull((select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID and pap.PAP_DATE<=c.C_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1)) AND " + queryPart + "=@_curid;", new string[] { "@_curid" }, new string[] { id.ToString() });
            if (tempList.Count > 0)
            {
                string text = null;
                for (int i = 0; i < tempList.Count; i++)
                {
                    text = "\nКод чека:" + id + "\nКод продукта:" + tempList[i].ID + "\nНазвание продукта:" + tempList[i].PRODUCT + "\nЦена:" + tempList[i].PRICE + "\nКоличество:" + tempList[i].VALUE;
                    Files.SaveToArchive(id.ToString() + i.ToString(), text, "CheckList");
                }

            }
        }

        private void CycleBackupPrice(int id)
        {
            List<ProductActualPrice> tempPriceList = DataBase.GetProductActualPrice(id.ToString());
            if (tempPriceList.Count > 0)
            {
                for (int j = 0; j < tempPriceList.Count; j++)
                {
                    Files.SaveToArchive(id.ToString()+tempPriceList[j].ID.ToString(),"\nДата:" + tempPriceList[j].DATE + "\nЦена:" + tempPriceList[j].PRICE,"ProductPrice");
                }
            }
        }

        private void CycleBackupWaybillList(int id,string queryPart)
        {
            List<WaybillList> tempWaybillList = DataBase.GetWaybillList(new string[] { "@_curid" }, new string[] { id.ToString() }, "SELECT waybill_list.WL_ID,product.P_NAME,waybill_list.WL_VALUE,waybill_list.WL_TRADE_PRICE,waybill_list.WL_BDATE,waybill_list.WL_EDATE,waybill.W_ID FROM `waybill`,`waybill_list`,`product` WHERE `waybill_list`.`W_ID`=`waybill`.`W_ID` AND `waybill_list`.`P_ID`=`product`.`P_ID` AND "+queryPart+"=@_curid;");
            if (tempWaybillList.Count > 0)
            {
                for (int j = 0; j < tempWaybillList.Count; j++)
                {
                    string[] dataPlus = DataBase.QueryRetRow(new string[] { "@_id" }, new string[] { tempWaybillList[j].ID.ToString() }, "SELECT po.PP_IS_OVERDUE,ps.PS_COUNT FROM product_overdue po,product_sold ps,waybill_list wl WHERE po.WL_ID=wl.WL_ID AND ps.WL_ID=wl.WL_ID AND wl.WL_ID=@_id");
                    Files.SaveToArchive(id.ToString() + tempWaybillList[j].ID.ToString(), "\nКод:" + tempWaybillList[j].ID.ToString() + "\nТовар:" + tempWaybillList[j].PRODUCT + "\nКоличество:" + tempWaybillList[j].VALUE + "\nСостояние:"+dataPlus[0]+"\nПродано:"+dataPlus[1]+"\nОпт.цена:" + tempWaybillList[j].TRADEPRICE + "\nДата поступления:" + tempWaybillList[j].BDATE + "\nГоден до:" + tempWaybillList[j].EDATE, "WaybillList");
                }
            }
        }

        private void CycleBackupDiscount(int id)
        {
            List<Discount> tempDiscountList = DataBase.GetDiscount("SELECT * FROM discounts WHERE P_ID=@_id", new string[] { "@_id" }, new string[] { id.ToString() });
            if (tempDiscountList.Count > 0)
            {
                for (int j = 0; j < tempDiscountList.Count; j++)
                {
                    Files.SaveToArchive(tempDiscountList[j].ID.ToString(), "\nКод:" + tempDiscountList[j].ID + "\nТовар:" + tempDiscountList[j].NAME + "\nЦена:" + tempDiscountList[j].PRICE + "\nНачало:" + tempDiscountList[j].BDATE + "\nКонец:" + tempDiscountList[j].EDATE + "\nОписание:" + tempDiscountList[j].TEXT, "Discount");
                }
            }
        }

        public T DeleteRow<T>(DataGrid dataGrid,bool settings,List<T> list,int type,string sqlQuery,string logText,int mode,int columnId,string getObjQuery,GetTableDelegate<T> getObj)
        {
                if (dataGrid.SelectedIndex != -1)
                {
                    string objId = Converter.DGCellToStringConvert(dataGrid.SelectedIndex, columnId, dataGrid);
                    if (mode == 1)
                    {
                        if (settings == false)
                        {
                            bool flag = false;
                            switch(type)
                            {
                                case 0:
                                    {
                                        WarningDelEmployeeBindsWindow window = new WarningDelEmployeeBindsWindow(idText, objId);
                                        window.ShowDialog();
                                        flag = window.flag;
                                        break;
                                    }
                                case 1:
                                    {
                                        WarningDelManufacturerBindsWindow window = new WarningDelManufacturerBindsWindow(idText, objId);
                                        window.ShowDialog();
                                        flag = window.flag;
                                        break;
                                    }
                                case 2:
                                    {
                                        WarningDelProductBindsWindow window = new WarningDelProductBindsWindow(idText, objId);
                                        window.ShowDialog();
                                        flag = window.flag;
                                        break;
                                    }
                                case 3:
                                    {
                                        WarningDelWaybillBindsWindow window = new WarningDelWaybillBindsWindow(idText, objId);
                                        window.ShowDialog();
                                        flag = window.flag;
                                        break;
                                    }
                            }
                            if (flag == true)
                            {
                                list.RemoveAt(dataGrid.SelectedIndex);
                                dataGrid.Items.Refresh();
                                return default(T);
                            }
                            else
                            {
                                return default(T);
                            }
                        }
                        else
                        {
                            var tempobj = getObj(getObjQuery, new string[] { "@_id" }, new string[] { objId });
                            DataBase.Query(new string[] { "@_curid" }, new string[] { objId }, sqlQuery + "=@_curid;");
                            list.RemoveAt(dataGrid.SelectedIndex);
                            dataGrid.Items.Refresh();
                            DataBase.SetLog(idText, 1, 3, logText + objId);
                            if (tempobj.Count > 0)
                            {
                                return tempobj[0];
                            }
                            else
                            {
                                return default(T);
                            }
                        }
                    }
                    else
                    {
                        if(mode == 0)
                        {
                            var tempobj = getObj(getObjQuery, new string[] { "@_id" }, new string[] { objId });
                            DataBase.Query(new string[] { "@_curid" }, new string[] { objId }, sqlQuery + "=@_curid;");
                            list.RemoveAt(dataGrid.SelectedIndex);
                            dataGrid.Items.Refresh();
                            DataBase.SetLog(idText, 1, 3, logText + objId);
                            if(tempobj.Count > 0)
                            {
                                return tempobj[0];
                            }
                            else
                            {
                                return default(T);
                            }
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
                else
                {
                    return default(T);
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
                            DiscountEditWindow window = new DiscountEditWindow(idText, Converter.DGCellToStringConvert(dataGridDiscountOut.SelectedIndex, 0, dataGridDiscountOut), ((Discount)(dataGridDiscountOut.SelectedItem)));
                            window.ShowDialog();
                            if (window.flag)
                            {
                                discount[dataGridDiscountOut.SelectedIndex] = window.obj;
                                dataGridDiscountOut.Items.Refresh();
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        if (dataGridEmployeeOut.SelectedIndex != -1)
                        {
                            EmployeeEditWindow window = new EmployeeEditWindow(idText, Converter.DGCellToStringConvert(dataGridEmployeeOut.SelectedIndex, 0, dataGridEmployeeOut), ((Employee)(dataGridEmployeeOut.SelectedItem)));
                            window.ShowDialog();
                            if (window.flag)
                            {
                                employee[dataGridEmployeeOut.SelectedIndex] = window.obj;
                                dataGridEmployeeOut.Items.Refresh();
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (dataGridManufacturersOut.SelectedIndex != -1)
                        {
                            ManufacturerEditWindow window = new ManufacturerEditWindow(idText, Converter.DGCellToStringConvert(dataGridManufacturersOut.SelectedIndex, 0, dataGridManufacturersOut),((Manufacturer)(dataGridManufacturersOut.SelectedItem)));
                            window.ShowDialog();
                            if (window.flag)
                            {
                                manufacturer[dataGridManufacturersOut.SelectedIndex] = window.obj;
                                dataGridManufacturersOut.Items.Refresh();
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        if (dataGridProductOut.SelectedIndex != -1)
                        {
                            ProductEditWindow window = new ProductEditWindow(idText, Converter.DGCellToStringConvert(dataGridProductOut.SelectedIndex, 0, dataGridProductOut),((Product)(dataGridProductOut.SelectedItem)));
                            window.ShowDialog();
                            if(window.flag)
                            {
                                product[dataGridProductOut.SelectedIndex] = window.objout;
                                dataGridProductOut.Items.Refresh();
                            }
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
            CalculateEmployeesCount((sender as DataGrid).SelectedIndex);
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
                            "SELECT DISTINCT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME,`check`.C_SUM,`check`.C_PREPAYMENT FROM `check`,`employee`,check_list,product,product_actual_price WHERE `check`.`E_ID`=`employee`.`E_ID` AND `check`.C_ID=check_list.C_ID AND check_list.P_ID=product.P_ID AND check_list.P_ID=product_actual_price.P_ID ",
                            "SELECT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee` WHERE `check`.`E_ID`=`employee`.`E_ID` " }, temp, flag);
                        GetSubTable<CheckList>(dataGridCheckListOut, dataGridCheckOut);
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

                        if (comboBoxSearchGroupProduct.SelectedItem != null)
                        {
                            SQLParameterAdd(checkBoxSearchGroupProduct.IsChecked.Value, new string[] { " AND p.P_GROUP=@_group", "@_group", comboBoxSearchGroupProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });
                        }
                        if (comboBoxSearchPackProduct.SelectedItem != null)
                        {
                            SQLParameterAdd(checkBoxSearchPackProduct.IsChecked.Value, new string[] { " AND p.P_PACK=@_pack", "@_pack", comboBoxSearchPackProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });
                        }
                        if (comboBoxSearchMaterialProduct.SelectedItem != null)
                        {
                            SQLParameterAdd(checkBoxSearchMaterialProduct.IsChecked.Value, new string[] { " AND p.P_MATERIAL=@_material", "@_material", comboBoxSearchMaterialProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });
                        }
                        if (comboBoxSearchFormProduct.SelectedItem != null)
                        {
                            SQLParameterAdd(checkBoxSearchFormProduct.IsChecked.Value, new string[] { " AND p.P_FORM=@_form", "@_form", comboBoxSearchFormProduct.SelectedItem.ToString() }, ref temp, ref flag, new bool[] { false, false });
                        }
                        if (comboBoxSearchProductStatus.SelectedItem != null && comboBoxSearchProductStatus.SelectedIndex != 0)
                        {
                            SQLParameterAdd(checkBoxSearchProductStatus.IsChecked.Value, new string[] { " AND po.PP_IS_OVERDUE=@_status", "@_status", ProductStatus(comboBoxSearchProductStatus.SelectedIndex) }, ref temp, ref flag, new bool[] { true, false });
                        }
                        
                        ComparsionGet(checkBoxSearchPriceProduct.IsChecked.Value, new int[] { comboBoxSearchPriceRangeProduct.SelectedIndex, 0 },
                              new string[] { " AND product_actual_price.PAP_PRICE" },
                              new string[] { null, Converter.CurrencyConvert(upDownSearchPriceProduct.Text) }, ref temp, ref flag);

                        ComparsionGet(checkBoxSearchValueProduct.IsChecked.Value, new int[] { comboBoxDirectionSearchValueProduct.SelectedIndex, comboBoxSearchTypeCountProduct.SelectedIndex },
                            new string[] { " AND (SELECT SUM(waybill_list.WL_VALUE-product_sold.PS_COUNT) FROM waybill_list,product_sold WHERE waybill_list.P_ID=p.P_ID AND waybill_list.WL_ID=product_sold.WL_ID)",
                            " AND (SELECT waybill_list.WL_VALUE-product_sold.PS_COUNT FROM waybill_list,product_sold,product_overdue WHERE product_overdue.PP_IS_OVERDUE='Просрочено' AND product_sold.WL_ID=waybill_list.WL_ID AND product_overdue.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=p.P_ID)",
                            " AND (SELECT IF(COUNT(waybill_list.WL_VALUE-product_sold.PS_COUNT)>0,waybill_list.WL_VALUE-product_sold.PS_COUNT,0) FROM product_sold,product_overdue,waybill_list WHERE product_sold.WL_ID=product_overdue.WL_ID AND product_overdue.PP_IS_OVERDUE<>'Просрочено' AND product_overdue.PP_IS_OVERDUE<>'Продано' AND product_sold.WL_ID=waybill_list.WL_ID AND waybill_list.P_ID=p.P_ID)" },
                            new string[] { "@valueq", upDownSearchValueProduct.Text }, ref temp, ref flag);

                        SQLParameterAdd(checkBoxSearchCodeProduct.IsChecked.Value, new string[] { " AND p.P_ID=@_code", "@_code", textBoxSearchCodeProduct.Text }, ref temp, ref flag, new bool[] { false, false });

                        SQLParameterAdd(checkBoxSearchBarcode.IsChecked.Value, new string[] { " AND p.P_CODE=@_barcode", "@_barcode", textBoxSearchBarcode.Text }, ref temp, ref flag, new bool[] { false, false });

                        MakeSearch(
                            dataGridProductOut, DataBase.GetProduct, 
                            new string[] { "SELECT p.P_ID,p.P_NAME,manufacturer.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,p.P_INSTR,p.P_CODE FROM `product` p,`manufacturer`,product_actual_price,waybill_list wl,product_overdue po WHERE `manufacturer`.`M_ID`=p.`M_ID` AND p.P_ID=product_actual_price.P_ID AND wl.P_ID=p.P_ID AND wl.WL_ID=po.WL_ID ",
                                "SELECT p.P_ID,p.P_NAME,manufacturer.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,p.P_INSTR,p.P_CODE FROM `product` p,`manufacturer` WHERE `manufacturer`.`M_ID`=p.`M_ID` " },
                                temp, flag);
                        GetSubTable<ProductActualPrice>(dataGridProductActPriceOut, dataGridProductOut);
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

                        if (comboBoxSearchProductStatusWaybill.SelectedItem != null && comboBoxSearchProductStatusWaybill.SelectedIndex != 0)
                        {
                            SQLParameterAdd(checkBoxSearchProductStatusWaybill.IsChecked.Value, new string[] { " AND po.PP_IS_OVERDUE=@_status", "@_status", ProductStatus(comboBoxSearchProductStatusWaybill.SelectedIndex) }, ref temp, ref flag, new bool[] { true, false });
                        }

                        MakeSearch(dataGridWaybillOut, DataBase.GetWaybill, 
                            new string[] { "SELECT DISTINCT w.W_ID,w.W_DATE,e.E_NAME,w.W_AGENT_NAME FROM `waybill` w,`employee` e,waybill_list wl,product p,product_overdue po WHERE w.`E_ID`=e.`E_ID` AND wl.W_ID=w.W_ID AND p.P_ID=wl.P_ID AND po.WL_ID=wl.WL_ID ",
                                "SELECT w.W_ID,w.W_DATE,e.E_NAME,w.W_AGENT_NAME FROM `waybill` w,`employee` e WHERE w.`E_ID`=e.`E_ID` " }
                                , temp, flag);
                        GetSubTable<WaybillList>(dataGridWaybillListOut, dataGridWaybillOut);
                        break;
                    }
            }
            valuesText.Clear();
            values.Clear();
        }

        private string ProductStatus(int selectedVal)
        {
            switch(selectedVal)
            {
                case 1:
                    {
                        return "Не просрочено";
                    }
                case 2:
                    {
                        return "Скоро истекает срок годности";
                    }
                case 3:
                    {
                        return "Просрочено";
                    }
                case 4:
                    {
                        return "Продано";
                    }
                default:
                    {
                        return "";
                    }
            }
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
            if (intitalizeFlag == true)
            {
                if (dataGridCheckListOut.SelectedIndex != -1)
                {
                    List<DiscountInfo> dataList = DataBase.GetDiscountInfoList(
                        new string[] { "@_id", "@_cid" },
                        new string[] { checkList[dataGridCheckListOut.SelectedIndex].ID.ToString(), Converter.DGCellToStringConvert(dataGridCheckOut.SelectedIndex, 3, dataGridCheckOut) },
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
        }

        private void dataGridWaybillOverdueDisplay_Initialized(object sender, EventArgs e)
        {
            if (intitalizeFlag == true)
            {
                if (dataGridWaybillListOut.SelectedIndex != -1)
                {
                    List<WaybillInfo> dataList = DataBase.GetWaybillInfoList(
                        new string[] { "@_id", "@_wid" },
                        new string[] { waybillList[dataGridWaybillListOut.SelectedIndex].ID.ToString(), Converter.DGCellToStringConvert(dataGridWaybillOut.SelectedIndex, 3, dataGridWaybillOut) },
                        "SELECT IF((SELECT COUNT(wl.WL_ID) WHERE po.PP_IS_OVERDUE='Просрочено')>0,wl.WL_VALUE-ps.PS_COUNT,0),IF((SELECT COUNT(wl.WL_ID) WHERE po.PP_IS_OVERDUE='Не просрочено')>0,wl.WL_VALUE-ps.PS_COUNT,0),ps.PS_COUNT FROM waybill_list wl,product_sold ps,product_overdue po WHERE ps.WL_ID=wl.WL_ID AND po.WL_ID=wl.WL_ID AND wl.W_ID=@_wid AND wl.P_ID=@_id;");
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
            checkBoxSearchBarcode.IsChecked = false;
            textBoxSearchBarcode.Text = null;
            textBoxSearchBarcode.IsEnabled = false;
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
            comboBoxSearchProductStatus.SelectedIndex = 0;
            comboBoxSearchProductStatus.IsEnabled = false;
            checkBoxSearchProductStatus.IsChecked = false;
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
        private void checkBoxSearchBarcode_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchBarcode, textBoxSearchBarcode);
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
            checkBoxSearchProductStatusWaybill.IsChecked = false;
            comboBoxSearchProductStatusWaybill.SelectedIndex = 0;
            comboBoxSearchProductStatusWaybill.IsEnabled = false;
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

        private void dataGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if((sender as DataGrid).SelectedIndex != -1)
            {
                switch(e.Key)
                {
                    case Key.Delete:
                        {
                            buttonDelete_Click(null,null);
                            break;
                        }
                    case Key.Insert:
                        {
                            buttonAdd_Click(null,null);
                            break;
                        }
                    case Key.F12:
                        {
                            buttonEdit_Click(null,null);
                            break;
                        }
                    case Key.F11:
                        {
                            UpdateData_Click(null,null);
                            break;
                        }
                }
            }
        }

        private void checkBoxSearchProductStatus_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchProductStatus, comboBoxSearchProductStatus);
        }

        private void checkBoxSearchProductStatusWaybill_Click(object sender, RoutedEventArgs e)
        {
            SearchSingleToggle(checkBoxSearchProductStatusWaybill, comboBoxSearchProductStatusWaybill);
        }
    }
}