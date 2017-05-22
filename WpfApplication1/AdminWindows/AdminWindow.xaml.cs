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
        public AdminWindow(string _idText)
        {
            InitializeComponent();
            idText = _idText;
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
                        int indexTemp = dataGridCheckOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridCheckOut.Items[indexTemp], dataGridCheckOut.Columns[0]);
                            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock);
                            dataGridCheckListOut.ItemsSource = DataBase.GetCheckList(content.Text);
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
                        int indexTemp = dataGridProductOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridProductOut.Items[indexTemp], dataGridProductOut.Columns[0]);
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                            dataGridProductActPriceOut.ItemsSource = DataBase.GetProductActualPrice(content.Text);
                        }
                        break;
                    }
                case 5:
                    {
                        dataGridWaybillOut.ItemsSource = DataBase.GetWaybill();
                        int indexTemp = dataGridWaybillOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridWaybillOut.Items[indexTemp], dataGridWaybillOut.Columns[0]);
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                            dataGridWaybillListOut.ItemsSource = DataBase.GetWaybillList(content.Text);
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
            int indexTemp = dataGridCheckOut.SelectedIndex;
            if (indexTemp != -1)
            {
                var cellInfo = new DataGridCellInfo(dataGridCheckOut.Items[indexTemp], dataGridCheckOut.Columns[0]);
                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                dataGridCheckListOut.ItemsSource = DataBase.GetCheckList(content.Text);
                string totalPrice = DataBase.QueryRetCell(new string[] { "@_curid" }, new string[] { content.Text }, "SELECT IF((SELECT COUNT(discounts.D_ID) FROM discounts,check_list cl,`check` c WHERE cl.C_ID=@_curid AND discounts.P_ID=cl.P_ID AND c.C_ID=cl.C_ID AND c.C_DATE>=discounts.D_BDATE AND c.C_DATE<=discounts.D_EDATE)>0,(SELECT (SELECT SUM(pap.PAP_PRICE*cl.CL_VALUE*(1-d.D_PRICE*0.01)) FROM product_actual_price pap,check_list cl WHERE pap.P_ID=cl.P_ID AND d.P_ID=cl.P_ID AND cl.C_ID=c.C_ID AND pap.PAP_DATE=(SELECT PAP_DATE FROM product_actual_price WHERE product_actual_price.P_ID=cl.P_ID ORDER BY 1 DESC LIMIT 1)) FROM `check` c,discounts d WHERE c.C_DATE>=d.D_BDATE AND c.C_DATE<=d.D_EDATE AND c.C_ID=@_curid),(SELECT (SELECT SUM(cl.CL_VALUE*product_actual_price.PAP_PRICE) FROM check_list cl,product_actual_price WHERE cl.C_ID=c.C_ID AND cl.P_ID=product_actual_price.P_ID AND product_actual_price.PAP_ID=(SELECT PAP_ID FROM product_actual_price WHERE P_ID=cl.P_ID ORDER BY PAP_DATE DESC LIMIT 1)) FROM `check` c WHERE c.C_ID=@_curid));");
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

        private void tabItemCheck_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridCheckOut.ItemsSource = DataBase.GetCheck();
        }

        private void tabItemDiscount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridDiscountOut.ItemsSource = DataBase.GetDiscount();
        }

        private void tabItemManufacturer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer();
        }

        private void tabItemProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridProductOut.ItemsSource = DataBase.GetProduct();
        }

        private void dataGridProductOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int indexTemp = dataGridProductOut.SelectedIndex;
                if (indexTemp != -1)
                {
                    var cellInfo = new DataGridCellInfo(dataGridProductOut.Items[indexTemp], dataGridProductOut.Columns[0]);
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                    dataGridProductActPriceOut.ItemsSource = DataBase.GetProductActualPrice(content.Text);
                    string quantity = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { content.Text }, "SELECT PQ_QUANTITY FROM product_quantity WHERE P_ID=@_id;"),
                    overdueValue = DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { content.Text }, "select waybill_list.WL_VALUE-product_sold.PS_COUNT from waybill_list,product_sold,product_overdue where product_overdue.PP_IS_OVERDUE='Просрочено' and product_sold.WL_ID=waybill_list.WL_ID and product_overdue.WL_ID=waybill_list.WL_ID and waybill_list.P_ID=@_id;");
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tabItemEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridEmployeeOut.ItemsSource = DataBase.GetEmployee();
        }

        private void tabItemWaybill_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGridWaybillOut.ItemsSource = DataBase.GetWaybill();
        }

        private void dataGridWaybillOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int indexTemp = dataGridWaybillOut.SelectedIndex;
                if (indexTemp != -1)
                {
                    var cellInfo = new DataGridCellInfo(dataGridWaybillOut.Items[indexTemp], dataGridWaybillOut.Columns[0]);
                    var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                    dataGridWaybillListOut.ItemsSource = DataBase.GetWaybillList(content.Text);
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
                            int indexTemp = dataGridDiscountOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridDiscountOut.Items[indexTemp], dataGridDiscountOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                DataBase.Query(new string[] { "@_curid" }, new string[] { content.Text }, "DELETE FROM `discounts` WHERE D_ID=@_curid;");
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
                            int indexTemp = dataGridEmployeeOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridEmployeeOut.Items[indexTemp], dataGridEmployeeOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                if(Properties.Settings.Default.DelBindingToEmployee == false)
                                {
                                    new WarningDelEmployeeBindsWindow(content.Text).ShowDialog();
                                }
                                else
                                {
                                    DataBase.Query(new string[] { "@_curid" }, new string[] { content.Text }, "DELETE FROM `employee` WHERE E_ID=@_curid;");
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
                            int indexTemp = dataGridManufacturersOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridManufacturersOut.Items[indexTemp], dataGridManufacturersOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                if (Properties.Settings.Default.DelBindingToManufacturer == false)
                                {
                                    new WarningDelManufacturerBindsWindow(content.Text).ShowDialog();
                                }
                                else
                                {
                                    DataBase.Query(new string[] { "@_curid" }, new string[] { content.Text }, "DELETE FROM `manufacturer` WHERE M_ID=@_curid;");
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
                            int indexTemp = dataGridProductOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridProductOut.Items[indexTemp], dataGridProductOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                if (Properties.Settings.Default.DelBindingToProduct == false)
                                {
                                    new WarningDelProductBindsWindow(content.Text).ShowDialog();
                                }
                                else
                                {
                                    DataBase.Query(new string[] { "@_curid" }, new string[] { content.Text }, "DELETE FROM `product` WHERE P_ID=@_curid;");
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
                        int indexTemp = dataGridDiscountOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridDiscountOut.Items[indexTemp], dataGridDiscountOut.Columns[0]);
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                            new DiscountEditWindow(content.Text).ShowDialog();
                        }
                        break;
                    }
                case 2:
                    {

                        int indexTemp = dataGridEmployeeOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridEmployeeOut.Items[indexTemp], dataGridEmployeeOut.Columns[0]);
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                            new EmployeeEditWindow(content.Text).ShowDialog();
                        }
                        break;
                    }
                case 3:
                    {
                        int indexTemp = dataGridManufacturersOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridManufacturersOut.Items[indexTemp], dataGridManufacturersOut.Columns[0]);
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                            new ManufacturerEditWindow(content.Text).ShowDialog();
                        }
                        break;
                    }
                case 4:
                    {
                        int indexTemp = dataGridProductOut.SelectedIndex;
                        if (indexTemp != -1)
                        {
                            var cellInfo = new DataGridCellInfo(dataGridProductOut.Items[indexTemp], dataGridProductOut.Columns[0]);
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                            new ProductEditWindow(content.Text).ShowDialog();
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
                var cellInfo = new DataGridCellInfo(dataGridEmployeeOut.Items[dataGridEmployeeOut.SelectedIndex], dataGridEmployeeOut.Columns[3]);
                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                if(content.Text != "Уборщик")
                {
                    textBlockTypeCount.Text = content.Text + "ы:";
                }
                else
                {
                    textBlockTypeCount.Text = content.Text + "и:";
                }
                textBlockTypeCount.Text += DataBase.QueryRetCell(new string[] { "@_pos" }, new string[] { content.Text }, "SELECT COUNT(E_ID) FROM employee WHERE E_POSITION=@_pos;");
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
                        if (checkBoxSearchEmployeeCheck.IsChecked == true)
                        {
                            dataGridCheckOut.ItemsSource = DataBase.GetCheck("employee.E_NAME", textBoxSearchEmployeeCheck.Text);
                        }
                        if (checkBoxSearchBDateCheck.IsChecked == true)
                        {
                            //datePickerSearchBDateCheck.Text;
                        }
                        if (checkBoxSearchEDateCheck.IsChecked == true)
                        {
                            //datePickerSearchEDateCheck.Text;
                        }
                        if(checkBoxSearchBHours.IsChecked == true)
                        {
                            //upDownSearchBHours.Text;
                        }
                        if (checkBoxSearchBMinutes.IsChecked == true)
                        {
                            //upDownSearchBMinutes.Text;
                        }
                        if (checkBoxSearchBSeconds.IsChecked == true)
                        {
                            //upDownSearchBSeconds.Text;
                        }
                        if (checkBoxSearchEHours.IsChecked == true)
                        {
                            //upDownSearchEHours.Text;
                        }
                        if (checkBoxSearchEMinutes.IsChecked == true)
                        {
                            //upDownSearchEMinutes.Text;
                        }
                        if (checkBoxSearchESeconds.IsChecked == true)
                        {
                            //upDownSearchESeconds.Text;
                        }
                        if (checkBoxSearchCheckCode.IsChecked == true)
                        {
                            //textBoxSearchCheckCode.Text;
                        }
                        if (checkBoxSearchProductCheck.IsChecked == true)
                        {
                            //textBoxSearchProductCheck.Text;
                        }
                        if (checkBoxSearchPriceCheck.IsChecked == true)
                        {
                            //upDownSearchPriceCheck.Text;
                        }
                        if (checkBoxSearchValueCheck.IsChecked == true)
                        {
                            //upDownSearchValueCheck.Text;
                        }
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                case 5:
                    {
                        break;
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
            }
            else
            {
                datePickerSearchBDateCheck.Text = null;
                datePickerSearchBDateCheck.IsEnabled = false;
            }
        }

        private void checkBoxSearchEDate_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxSearchEDateCheck.IsChecked == true)
            {
                datePickerSearchEDateCheck.IsEnabled = true;
            }
            else
            {
                datePickerSearchEDateCheck.Text = null;
                datePickerSearchEDateCheck.IsEnabled = false;
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
            }
            else
            {
                textBoxSearchProductCheck.Text = null;
                textBoxSearchProductCheck.IsEnabled = false;
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
        }

    }
}