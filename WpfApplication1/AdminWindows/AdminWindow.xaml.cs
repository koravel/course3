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

        private void TablesListVisible_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Employees_Selected(object sender, RoutedEventArgs e)
        {
            //dataGridEmployeesOut.ItemsSource = DataBase.GetEmployee();
        }

        private void Waybill_Lists_Selected(object sender, RoutedEventArgs e)
        {
            //string query = @"SELECT waybill.W_ID AS 'Номер наклдной',product.P_NAME AS 'Товар',waybill_list.WL_VALUE AS 'Количество',waybill_list.WL_TRADE_PRICE AS 'Опт.цена',waybill_list.WL_BDATE AS 'Дата поступления',waybill_list.WL_EDATE AS 'Годен до'FROM `waybill`,`waybill_list`,`product` WHERE `waybill_list`.`W_ID`=`waybill`.`W_ID` AND `waybill_list`.`P_ID`=`product`.`P_ID`;";
            //DataBase.TableOutput(query, dataGridOut);
        }

        private void Waybills_Selected(object sender, RoutedEventArgs e)
        {
            //string query = @"SELECT waybill.W_ID AS 'Номер накладной',waybill.W_DATE AS 'Дата',waybill.W_AGENT_NAME AS 'Контрагент',employee.E_NAME AS 'Принял' FROM `waybill`,`employee` WHERE `waybill`.`E_ID`=`employee`.`E_ID`;";
            //DataBase.TableOutput(query, dataGridOut);
        }

        private void Produts_Actual_Price_Selected(object sender, RoutedEventArgs e)
        {
            //string query = @"SELECT product.P_NAME AS 'Товар',product_actual_price.PAP_PRICE AS 'Акт.цена',product_actual_price.PAP_DATE AS 'Срок действия' FROM `product`,`product_actual_price` WHERE `product_actual_price`.`P_ID`=`product`.`P_ID`;";
            //DataBase.TableOutput(query, dataGridOut);
        }

        private void Products_Selected(object sender, RoutedEventArgs e)
        {
            //string query = @"SELECT product.P_NAME AS 'Товар',manufacturer.M_NAME AS 'Название',product.P_GROUP AS 'Группа',product.P_PACK AS 'Упаковка',product.P_MATERIAL AS 'Форма',product.P_FORM AS 'Отпуск',product.P_INSTR AS 'Инструкция' FROM `product`,`manufacturer` WHERE `manufacturer`.`M_ID`=`product`.`M_ID`;";
            //DataBase.TableOutput(query, dataGridOut);
        }

        private void UsersControl_Click(object sender, RoutedEventArgs e)
        {
            new UsersControlWindow().ShowDialog();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
        //    if (tableList.SelectedIndex != -1)
        //    {
        //        int i = tableList.SelectedIndex;
        //        switch (i)
        //        {
        //            case 0:
        //                {
        //                    break;
        //                }
        //            case 1:
        //                {
        //                    queryString = @"SELECT check.C_ID AS 'Код чека',product.P_NAME AS 'Название товара',check_list.CL_VALUE AS 'Количество' FROM `check_list`,`product`,`check` WHERE `check_list`.`P_ID`=`product`.`P_ID`;";
        //                    break;
        //                }
        //            case 2:
        //                {
        //                    queryString = @"SELECT product.P_NAME AS 'Товар',discounts.D_PRICE AS 'Скидка',discounts.D_BDATE AS 'Начало',discounts.D_EDATE AS 'Конец',discounts.D_TEXT AS 'Описание' FROM `discounts`,`product` WHERE `discounts`.`P_ID`=`product`.`P_ID`;";
        //                    break;
        //                }
        //            case 3:
        //                {
        //                    queryString = @"SELECT E_NAME AS 'ФИО',E_TEL AS 'Телефон',E_POSITION AS 'Дожлность',E_CONTRACT AS 'Контракт' FROM `employee`;";
        //                    break;
        //                }
        //            case 4:
        //                {
        //                    queryString = @"SELECT manufacturer.M_NAME AS 'Название',manufacturer.M_COUNTRY AS 'Страна',manufacturer.M_CITY AS 'Город',manufacturer.M_ADDR AS 'Адрес',manufacturer.M_TEL AS 'Телефон' FROM `manufacturer`;";
        //                    break;
        //                }
        //            case 5:
        //                {
        //                    queryString = @"SELECT product.P_NAME AS 'Товар',manufacturer.M_NAME AS 'Название',product.P_GROUP AS 'Группа',product.P_PACK AS 'Упаковка',product.P_MATERIAL AS 'Форма',product.P_FORM AS 'Отпуск',product.P_INSTR AS 'Инструкция' FROM `product`,`manufacturer` WHERE `manufacturer`.`M_ID`=`product`.`M_ID`;";
        //                    break;
        //                }
        //            case 6:
        //                {
        //                    queryString = @"SELECT product.P_NAME AS 'Товар',product_actual_price.PAP_PRICE AS 'Акт.цена',product_actual_price.PAP_DATE AS 'Срок действия' FROM `product`,`product_actual_price` WHERE `product_actual_price`.`P_ID`=`product`.`P_ID`;";
        //                    break;
        //                }
        //            case 7:
        //                {
        //                    queryString = @"SELECT waybill.W_ID AS 'Номер накладной',waybill.W_DATE AS 'Дата',waybill.W_AGENT_NAME AS 'Контрагент',employee.E_NAME AS 'Принял' FROM `waybill`,`employee` WHERE `waybill`.`E_ID`=`employee`.`E_ID`;";
        //                    break;
        //                }
        //            case 8:
        //                {
        //                    queryString = @"SELECT waybill.W_ID AS 'Номер наклдной',product.P_NAME AS 'Товар',waybill_list.WL_VALUE AS 'Количество',waybill_list.WL_TRADE_PRICE AS 'Опт.цена',waybill_list.WL_BDATE AS 'Дата поступления',waybill_list.WL_EDATE AS 'Годен до'FROM `waybill`,`waybill_list`,`product` WHERE `waybill_list`.`W_ID`=`waybill`.`W_ID` AND `waybill_list`.`P_ID`=`product`.`P_ID`;";
        //                    break;
        //                }
        //        }
        //    }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            DataBase.FieldChange(null, null, @"UPDATE `user` SET U_ONLINE='offline' WHERE U_ID='"+idText+"';");
            MainWindow openWindow = new MainWindow();
            openWindow.Show();
            this.Close();
        }

        private void adminLogout_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataBase.FieldChange(null, null, @"UPDATE `user` SET U_ONLINE='offline' WHERE U_ID='" + idText + "';");
        }

        private void dataGridCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexTemp=dataGridCheckOut.SelectedIndex;
            if (indexTemp != -1)
            {
                var cellInfo = new DataGridCellInfo(dataGridCheckOut.Items[indexTemp], dataGridCheckOut.Columns[0]);
                var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
                dataGridCheckListOut.ItemsSource = DataBase.GetCheckList(content.Text);
            }
        }

        private void tabItemCheck_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGridCheckOut.ItemsSource = DataBase.GetCheck();
        }

        private void tabItemDiscount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGridDiscountOut.ItemsSource = DataBase.GetDiscount();
        }

        private void tabItemManufacturer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dataGridManufacturersOut.ItemsSource = DataBase.GetManufacturer();
        }
      
    }
}