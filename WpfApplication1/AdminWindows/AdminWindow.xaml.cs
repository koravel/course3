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
            checkBoxDelAll.IsChecked = false;
            idText = _idText;
            textBoxSearch.Visibility = Visibility.Hidden;
            comboBoxSelectiveSearch.Visibility = Visibility.Hidden;
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
                            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
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
            DataBase.Query(null, null, @"UPDATE `user` SET U_ONLINE='offline' WHERE U_ID='" + idText + "';");
            MainWindow openWindow = new MainWindow();
            openWindow.Show();
            this.Close();
        }

        private void adminLogout_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataBase.Query(null, null, @"UPDATE `user` SET U_ONLINE='offline' WHERE U_ID='" + idText + "';");
        }

        private void dataGridCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexTemp = dataGridCheckOut.SelectedIndex;
            if (indexTemp != -1)
            {
                var cellInfo = new DataGridCellInfo(dataGridCheckOut.Items[indexTemp], dataGridCheckOut.Columns[0]);
                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                dataGridCheckListOut.ItemsSource = DataBase.GetCheckList(content.Text);
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
            //string[] currentDate = DateTime.Today.ToString().Split(' ');
            //string[] currentSplit = currentDate[0].Split('.');
            //string currentDateInvert = currentSplit[2] + '-' + currentSplit[1] + '-' + currentSplit[0];
            //string[] value = new string[1]; value[0] = currentDateInvert;
            //string[] valueText = new string[1]; valueText[0] = "@_date";
            //string[] overdueProducts = DataBase.QueryRetRow(valueText,value,"SELECT P_ID FROM product_actual_price WHERE PAP_DATE<@_date;");
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
                case 0:
                    {
                        MessageBox.Show("Удаление запрещено.");
                        break;
                    }
                case 1:
                    {
                        string[] values = new string[1], valuesText = new string[1];
                        valuesText[0] = "@_curid";
                        try
                        {
                            int indexTemp = dataGridDiscountOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridDiscountOut.Items[indexTemp], dataGridDiscountOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                values[0] = content.Text;
                                DataBase.Query(valuesText, values, "DELETE FROM `discounts` WHERE D_ID=@_curid;");
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
                        string[] values = new string[1], valuesText = new string[1];
                        valuesText[0] = "@_curid";
                        try
                        {
                            int indexTemp = dataGridEmployeeOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridEmployeeOut.Items[indexTemp], dataGridEmployeeOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                values[0] = content.Text;
                                DataBase.Query(valuesText, values, "DELETE FROM `employee` WHERE E_ID=@_curid;");
                                if (checkBoxDelAll.IsChecked == true)
                                {
                                    if(Properties.Settings.Default.DelBindingToEmployee == false)
                                    {
                                        new WarningDelEmployeeBindsWindow(content.Text).ShowDialog();
                                    }
                                    else
                                    {
                                        valuesText[0] = "@_curid";
                                        values[0] = content.Text;
                                        DataBase.Query(valuesText, values, "DELETE FROM `waybill` WHERE E_ID=@_curid;");
                                        DataBase.Query(valuesText, values, "DELETE FROM `check` WHERE E_ID=@_curid;");
                                    }
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
                        string[] values = new string[1], valuesText = new string[1];
                        valuesText[0] = "@_curid";
                        try
                        {
                            int indexTemp = dataGridManufacturersOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridManufacturersOut.Items[indexTemp], dataGridManufacturersOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                values[0] = content.Text;
                                DataBase.Query(valuesText, values, "DELETE FROM `manufacturer` WHERE M_ID=@_curid;");
                                if (checkBoxDelAll.IsChecked == true)
                                {
                                    if (Properties.Settings.Default.DelBindingToManufacturer == false)
                                    {
                                        new WarningDelManufacturerBindsWindow(content.Text).ShowDialog();
                                    }
                                    else
                                    {
                                        valuesText[0] = "@_curid";
                                        values[0] = content.Text;
                                        DataBase.Query(valuesText, values, "DELETE FROM `product` WHERE M_ID=@_curid;");
                                        DataBase.Query(valuesText, values, "DELETE FROM `discount` WHERE M_ID=@_curid;");
                                    }
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
                        string[] values = new string[1], valuesText = new string[1];
                        valuesText[0] = "@_curid";
                        try
                        {
                            int indexTemp = dataGridProductOut.SelectedIndex;
                            if (indexTemp != -1)
                            {
                                var cellInfo = new DataGridCellInfo(dataGridProductOut.Items[indexTemp], dataGridProductOut.Columns[0]);
                                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                                values[0] = content.Text;
                                DataBase.Query(valuesText, values, "DELETE FROM `product` WHERE P_ID=@_curid;");
                                if (checkBoxDelAll.IsChecked == true)
                                {
                                    if (Properties.Settings.Default.DelBindingToProduct == false)
                                    {
                                        new WarningDelProductBindsWindow(content.Text).ShowDialog();
                                    }
                                    else
                                    {
                                        valuesText[0] = "@_curid";
                                        values[0] = content.Text;
                                        DataBase.Query(valuesText, values, "DELETE FROM `waybill` WHERE P_ID=@_curid;");
                                        DataBase.Query(valuesText, values, "DELETE FROM `discount` WHERE P_ID=@_curid;");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case 5:
                    {
                        MessageBox.Show("Удаление запрещено.");
                        break;
                    }
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}