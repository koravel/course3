using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class SellerWindow : Window
    {
        List<ProductInCheck> list = new List<ProductInCheck>();
        List<ProductInCheck> listToCheck = new List<ProductInCheck>();
        List<ProductInCheck> listSearch = new List<ProductInCheck>();
        List<int> indexes = new List<int>(),buffList = new List<int>();
        List<NameIdList> employees = new List<NameIdList>();
        string idText;
        float summ = 0,prepayment = 0;
        public SellerWindow(string id)
        {
            InitializeComponent();
            DataBase.SetLog(id, 1, 0, "Вход в систему...");
            idText = id;
            EmployeesUpdate();
            list = DataBase.GetProductForSeller();
            dataGridProductOut.ItemsSource = list;
            dataGridProductToCheckOut.ItemsSource = listToCheck;
        }

        private void buttonSell_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProductOut.SelectedIndex != -1)
            {
                if (upDownValue.Text != "" && upDownValue.Value.Value <= ((ProductInCheck)(dataGridProductOut.Items[dataGridProductOut.SelectedIndex])).VALUE)
                {
                    if(dataGridProductOut.ItemsSource == list)
                    {
                        ProductToCheckAdd(list, dataGridProductOut.SelectedIndex);
                    }
                    else
                    {
                        ProductToCheckAdd(listSearch, buffList[dataGridProductOut.SelectedIndex]);
                    }
                }
            }
        }

        private void ProductToCheckAdd(List<ProductInCheck> curList,int curIndex)
        {
            bool flag = true;
            int index = -1;
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (listToCheck[i].WAYBILLID == curList[dataGridProductOut.SelectedIndex].WAYBILLID)
                {
                    index = i;
                    i = listToCheck.Count;
                    flag = false;
                }
            }
            if (flag)
            {
                indexes.Add(curIndex);
                listToCheck.Add(new ProductInCheck(list[curIndex].PRICE, upDownValue.Value.Value, list[curIndex].DISCOUNT)
                {
                    ID = list[curIndex].ID,
                    NAME = list[curIndex].NAME,
                    PRICE = list[curIndex].PRICE,
                    TRADEPRICE = list[curIndex].TRADEPRICE,
                    VALUE = upDownValue.Value.Value,
                    DISCOUNT = list[curIndex].DISCOUNT,
                    MANUFACTURER = list[curIndex].MANUFACTURER,
                    GROUP = list[curIndex].GROUP,
                    PACK = list[curIndex].PACK,
                    MATERIAL = list[curIndex].MATERIAL,
                    FORM = list[curIndex].FORM,
                    WAYBILLID = list[curIndex].WAYBILLID,
                    CODE = list[curIndex].CODE
                });
                summ += float.Parse(listToCheck[listToCheck.Count - 1].SUMM);
                textBlockSumm.Text = "сумма:" + summ.ToString();
            }
            else
            {
                float tempSumm = float.Parse(listToCheck[index].SUMM);
                listToCheck[index].VALUE += upDownValue.Value.Value;
                listToCheck[index].CURPRICE = Converter.CailingRound((float.Parse(listToCheck[index].PRICE) * (100 - listToCheck[index].DISCOUNT) * 0.01)).ToString();
                listToCheck[index].SUMM = Math.Round(decimal.Parse((float.Parse(listToCheck[index].CURPRICE) * listToCheck[index].VALUE).ToString()), 2).ToString();
                summ += (float.Parse(listToCheck[index].SUMM) - tempSumm);
                textBlockSumm.Text = "сумма:" + Math.Round(summ, 2).ToString();
            }

            list[curIndex].VALUE -= upDownValue.Value.Value;
            dataGridProductOut.Items.Refresh();
            dataGridProductToCheckOut.Items.Refresh();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataBase.SetLog(idText, 1, 0, "Выход из системы...");
            new MainWindow().Show();
        }

        private void dataGridProductToCheckOut_KeyUp(object sender, KeyEventArgs e)
        {
            if(dataGridProductToCheckOut.SelectedIndex != -1 && e.Key == Key.Delete)
            {
                SecurityPassWindow window = new SecurityPassWindow();
                window.ShowDialog();
                if (window.flag)
                {
                    BackupAddtion(dataGridProductToCheckOut.SelectedIndex);
                    dataGridProductToCheckOut.Items.Refresh();
                    dataGridProductOut.Items.Refresh();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case(Key.F2):
                    {
                        buttonPrepayment_Click(null, null);
                        break;
                    }
                case (Key.F3):
                    {
                        buttonSellToCheck_Click(null, null);
                        break;
                    }
                case (Key.F4):
                    {
                        buttonCancel_Click(null, null);
                        break;
                    }
                case (Key.F5):
                    {
                        break;
                    }
                case (Key.F11):
                    {
                        radioButtonCash.IsChecked = true;
                        break;
                    }
                case (Key.F12):
                    {
                        radioButtonCard.IsChecked = true;
                        break;
                    }
            }
        }

        private void buttonPrepayment_Click(object sender, RoutedEventArgs e)
        {
            AddPrepaymentWindow window = new AddPrepaymentWindow(summ);
            window.ShowDialog();
            if(window.flag)
            {
                textBlockPrePayment.Text = "аванс:" + window.prePayment.ToString();
                prepayment = window.prePayment;
                textBlockDelivery.Text = "сдача:" + Math.Round((window.prePayment - summ),2).ToString();
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProductToCheckOut.Items.Count > 0)
            {
                SecurityPassWindow window = new SecurityPassWindow();
                window.ShowDialog();
                if (window.flag)
                {
                    while (listToCheck.Count > 0)
                    {
                        BackupAddtion(0);
                    }
                }
            }
        }

        private void BackupAddtion(int index)
        {
            list[indexes[index]].VALUE += listToCheck[index].VALUE;
            summ -= float.Parse(listToCheck[index].SUMM);
            textBlockSumm.Text = "сумма:" + Math.Round(summ,2).ToString();
            listToCheck.RemoveAt(index);
            indexes.RemoveAt(index);
            dataGridProductToCheckOut.Items.Refresh();
            dataGridProductOut.Items.Refresh();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string temp = null;
            List<string> param = new List<string>(), paramText = new List<string>();
            List<string> searchResult = new List<string>();
            if (e.Key == Key.Enter)
            {
                switch (comboBoxSearch.SelectedIndex)
                {
                    case 0:
                        {
                            ParamsAdd(param, paramText, "@_barcode", textBoxSearch.Text, " AND p.P_CODE=@_barcode", ref temp, false);
                            break;
                        }
                    case 1:
                        {
                            ParamsAdd(param, paramText, "@_name", textBoxSearch.Text, " AND p.P_NAME=@_name", ref temp, false);
                            break;
                        }
                    case 2:
                        {
                            ParamsAdd(param, paramText, "@_price", textBoxSearch.Text, " AND round(ifnull((select pap.PAP_PRICE from product_actual_price pap,waybill w where pap.P_ID=p.P_ID and pap.PAP_DATE<=w.W_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=p.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1)),2)", ref temp, true);
                            break;
                        }
                    case 3:
                        {
                            ParamsAdd(param, paramText, "@_tradeprice", textBoxSearch.Text, " AND wl.WL_TRADE_PRICE", ref temp, true);
                            break;
                        }
                    case 4:
                        {
                            listSearch.Clear();
                            buffList.Clear();
                            switch(comboBoxDirectionSearchValue.SelectedIndex)
                            {
                                case 0:
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            if (list[i].VALUE.ToString() == textBoxSearch.Text)
                                            {
                                                listSearch.Add(list[i]);
                                                buffList.Add(i);
                                            }
                                        }
                                        break;
                                    }
                                case 1:
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            if (list[i].VALUE >= int.Parse(textBoxSearch.Text))
                                            {
                                                listSearch.Add(list[i]);
                                                buffList.Add(i);
                                            }
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            if (list[i].VALUE <= int.Parse(textBoxSearch.Text))
                                            {
                                                listSearch.Add(list[i]);
                                                buffList.Add(i);
                                            }
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            if (list[i].VALUE > int.Parse(textBoxSearch.Text))
                                            {
                                                listSearch.Add(list[i]);
                                                buffList.Add(i);
                                            }
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            if (list[i].VALUE < int.Parse(textBoxSearch.Text))
                                            {
                                                listSearch.Add(list[i]);
                                                buffList.Add(i);
                                            }
                                        }
                                        break;
                                    }
                            }
                            
                            break;
                        }
                    case 5:
                        {
                            ParamsAdd(param, paramText, "@_discount", textBoxSearch.Text, " AND (SELECT if((SELECT COUNT(d.D_ID))>0,d.D_PRICE,0) FROM discounts d WHERE d.P_ID=p.P_ID AND d.D_BDATE<=NOW() AND d.D_EDATE>=NOW())", ref temp, true);
                            break;
                        }
                    case 6:
                        {
                            ParamsAdd(param, paramText, "@_manufacturer", textBoxSearch.Text, " AND m.M_NAME=@_manufacturer", ref temp, false);
                            break;
                        }
                    case 7:
                        {
                            ParamsAdd(param, paramText, "@_group", textBoxSearch.Text, " AND p.P_GROUP=@_group", ref temp, false);
                            break;
                        }
                    case 8:
                        {
                            ParamsAdd(param, paramText, "@_pack", textBoxSearch.Text, " AND p.P_PACK=@_pack", ref temp, false);
                            break;
                        }
                    case 9:
                        {
                            ParamsAdd(param, paramText, "@_material", textBoxSearch.Text, " AND p.P_MATERIAL=@_material", ref temp, false);
                            break;
                        }
                    case 10:
                        {
                            ParamsAdd(param, paramText, "@_form", textBoxSearch.Text, " AND p.P_FORM=@_form", ref temp,false);
                            break;
                        }
                }
                if (temp != null)
                {
                    string[] paramStr = new string[param.Count], paramTextStr = new string[param.Count];
                    for (int i = 0; i < param.Count; i++)
                    {
                        paramStr[i] = param[i];
                        paramTextStr[i] = paramText[i];
                    }
                    listSearch.Clear();
                    buffList.Clear();
                    searchResult = DataBase.GetProductForSeller(paramTextStr, paramStr, temp);
                    if(searchResult.Count < list.Count)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            for (int j = 0; j < searchResult.Count; j++)
                            {
                                if (list[i].WAYBILLID.ToString() == searchResult[j])
                                {
                                    listSearch.Add(list[i]);
                                    buffList.Add(i);
                                    searchResult.RemoveAt(j);
                                    j = searchResult.Count + 1;
                                }
                            }
                        }
                        dataGridProductOut.ItemsSource = null;
                        dataGridProductOut.ItemsSource = listSearch;
                    }
                }
                else
                {
                    if (listSearch.Count > 0 && searchResult.Count < list.Count)
                    {
                        dataGridProductOut.ItemsSource = null;
                        dataGridProductOut.ItemsSource = listSearch;
                    }
                }
            }
        }

        private void ParamsAdd(List<string> values,List<string> Textvalues,string text,string value,string queryPart,ref string query,bool mode)
        {
            query += queryPart;
            if(mode)
            {
                query += CompareType(comboBoxDirectionSearchValue.SelectedIndex) + text;
            }
            values.Add(value);
            Textvalues.Add(text);
        }

        private void buttonSearchEscape_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProductOut.ItemsSource != list)
            {
                dataGridProductOut.ItemsSource = null;
                dataGridProductOut.ItemsSource = list;
            }
            textBoxSearch.Text = null;
        }

        private string CompareType(int mode)
        {
            switch (mode)
            {
                case 0:
                    {
                        return "=";
                    }
                case 1:
                    {
                        return ">=";
                    }
                case 2:
                    {
                        return "<=";
                    }
                case 3:
                    {
                        return ">";

                    }
                case 4:
                    {
                        return "<";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProductToCheckOut.SelectedIndex != -1)
            {
                BackupAddtion(dataGridProductToCheckOut.SelectedIndex);
                dataGridProductToCheckOut.Items.Refresh();
                dataGridProductOut.Items.Refresh();
            }
        }

        private void buttonSellToCheck_Click(object sender, RoutedEventArgs e)
        {
            if(comboBoxEmployeeName.SelectedIndex != -1 && listToCheck.Count > 0)
            {
                if(summ <= prepayment)
                {
                    comboBoxEmployeeName.BorderBrush = Brushes.Gray;
                    TransactionConfirm window = new TransactionConfirm();
                    window.ShowDialog();
                    if (window.flag)
                    {
                        string paytype;
                        if (radioButtonCard.IsChecked.Value)
                        {
                            paytype = "Карточка";
                        }
                        else
                        {
                            paytype = "Наличные";
                        }
                        string maxId = DataBase.QueryRetCell(null, null, "SELECT IFNULL(MAX(C_ID)+1,1) FROM `check`;");
                        DataBase.Query(new string[] { "@_id", "@_eid", "@_paytype", "@_summ", "@_prepayment" }, new string[] { maxId, employees[comboBoxEmployeeName.SelectedIndex].ID.ToString(), paytype, Converter.FloatToCurrencyConvert(summ.ToString()), Converter.FloatToCurrencyConvert(prepayment.ToString()) }, "INSERT INTO `check`(C_ID,E_ID,C_DATE,C_PAYTYPE,C_SUM,C_PREPAYMENT)VALUES(@_id,@_eid,now(),@_paytype,@_summ,@_prepayment);");
                        List<string> productId = new List<string>();
                        List<string> productValue = new List<string>();
                        for (int i = 0; i < dataGridProductToCheckOut.Items.Count;i++ )
                        {
                            DataBase.Query(new string[] { "@_wlid", "@_value" }, new string[] { listToCheck[i].WAYBILLID.ToString(),listToCheck[i].VALUE.ToString() }, "UPDATE product_sold SET PS_COUNT=PS_COUNT+@_value WHERE WL_ID=@_wlid;");
                            DataBase.Query(new string[] { "@_wlid" }, new string[] { listToCheck[i].WAYBILLID.ToString() }, "UPDATE product_overdue po,waybill_list wl SET po.PP_IS_OVERDUE=IF((SELECT product_sold.PS_COUNT FROM product_sold WHERE product_sold.WL_ID=wl.WL_ID)=wl.WL_VALUE,'Продано','Не просрочено') WHERE po.WL_ID=wl.WL_ID AND wl.WL_ID=@_wlid;");
                            DataBase.Query(new string[] { "@_cid", "@_pid", "@_clvalue" }, new string[] { maxId, listToCheck[i].ID.ToString(), listToCheck[i].VALUE.ToString() }, "INSERT INTO check_list(C_ID,P_ID,CL_VALUE)VALUES(@_cid,@_pid,@_clvalue)");
                            for (int j = 0; j < productId.Count; j++)
                            {
                                if(productId[i] != listToCheck[i].ID.ToString())
                                {
                                    productId.Add(listToCheck[i].ID.ToString());
                                    productValue.Add(listToCheck[i].VALUE.ToString());
                                    i = productId.Count;
                                }
                                else
                                {
                                    productValue[j] = (int.Parse(productValue[j])+listToCheck[i].VALUE).ToString();
                                }
                            }
                        }
                        for (int i = 0; i < productId.Count;i++ )
                        {
                            DataBase.Query(new string[] { "@_pid", "@_value" }, new string[] { productId[i],productValue[i] }, "UPDATE product_quantity SET PQ_OUT=PQ_OUT+@_value WHERE P_ID=@_pid");
                        }
                    }
                    listToCheck.Clear();
                    summ = 0;
                    prepayment = 0;
                    textBlockDelivery.Text = "Сдача:0";
                    textBlockPrePayment.Text = "Аванс:0";
                    textBlockSumm.Text = "Сумма:0";
                    dataGridProductToCheckOut.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Аванс не может быть меньше суммы!");
                }
               
            }
            else
            {
                comboBoxEmployeeName.BorderBrush = Brushes.Red;
            }
        }

        private void menuItemEmployeeUpdate_Click(object sender, RoutedEventArgs e)
        {
            EmployeesUpdate();
        }

        private void EmployeesUpdate()
        {
            comboBoxEmployeeName.Items.Clear();
            employees = DataBase.GetNameIdList(new string[] { "E_ID", "E_NAME" }, "SELECT E_ID,E_NAME FROM employee;");
            for (int i = 0; i < employees.Count; i++)
            {
                comboBoxEmployeeName.Items.Add(employees[i].NAME + "(#" + employees[i].ID + ")");
            }
        }


    }
}
