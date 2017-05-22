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

namespace WpfApplication1
{
    public partial class SellerWindow : Window
    {
        List<ProductInCheck> list = new List<ProductInCheck>();
        List<ProductInCheck> listToCheck = new List<ProductInCheck>();
        List<ProductInCheck> listSearch = new List<ProductInCheck>();
        List<int> indexes = new List<int>(),buffList = new List<int>();
        string idText;
        float summ = 0;
        public SellerWindow(string id)
        {
            InitializeComponent();
            DataBase.SetLog(id, 1, 0, "Вход в систему...");
            idText = id;
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
                        bool flag = true;
                        int index = -1;
                        for (int i = 0; i < listToCheck.Count; i++)
                        {
                            if (listToCheck[i].WAYBILLID == list[dataGridProductOut.SelectedIndex].WAYBILLID)
                            {
                                index = i;
                                i = listToCheck.Count;
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            indexes.Add(dataGridProductOut.SelectedIndex);
                            listToCheck.Add(new ProductInCheck(list[dataGridProductOut.SelectedIndex].PRICE, upDownValue.Value.Value, list[dataGridProductOut.SelectedIndex].DISCOUNT)
                            {
                                ID = list[dataGridProductOut.SelectedIndex].ID,
                                NAME = list[dataGridProductOut.SelectedIndex].NAME,
                                PRICE = list[dataGridProductOut.SelectedIndex].PRICE,
                                TRADEPRICE = list[dataGridProductOut.SelectedIndex].TRADEPRICE,
                                VALUE = upDownValue.Value.Value,
                                DISCOUNT = list[dataGridProductOut.SelectedIndex].DISCOUNT,
                                MANUFACTURER = list[dataGridProductOut.SelectedIndex].MANUFACTURER,
                                GROUP = list[dataGridProductOut.SelectedIndex].GROUP,
                                PACK = list[dataGridProductOut.SelectedIndex].PACK,
                                MATERIAL = list[dataGridProductOut.SelectedIndex].MATERIAL,
                                FORM = list[dataGridProductOut.SelectedIndex].FORM,
                                WAYBILLID = list[dataGridProductOut.SelectedIndex].WAYBILLID,
                                CODE = list[dataGridProductOut.SelectedIndex].CODE
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
                            textBlockSumm.Text = "сумма:" + Math.Round(summ,2).ToString();
                        }

                        list[dataGridProductOut.SelectedIndex].VALUE -= upDownValue.Value.Value;
                        dataGridProductOut.Items.Refresh();
                        dataGridProductToCheckOut.Items.Refresh();
                    }
                    else
                    {
                        bool flag = true;
                        int index = -1;
                        for (int i = 0; i < listToCheck.Count; i++)
                        {
                            if (listToCheck[i].WAYBILLID == listSearch[dataGridProductOut.SelectedIndex].WAYBILLID)
                            {
                                index = i;
                                i = listToCheck.Count;
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            indexes.Add(buffList[dataGridProductOut.SelectedIndex]);
                            listToCheck.Add(new ProductInCheck(list[buffList[dataGridProductOut.SelectedIndex]].PRICE, upDownValue.Value.Value, list[buffList[dataGridProductOut.SelectedIndex]].DISCOUNT)
                            {
                                ID = list[buffList[dataGridProductOut.SelectedIndex]].ID,
                                NAME = list[buffList[dataGridProductOut.SelectedIndex]].NAME,
                                PRICE = list[buffList[dataGridProductOut.SelectedIndex]].PRICE,
                                TRADEPRICE = list[buffList[dataGridProductOut.SelectedIndex]].TRADEPRICE,
                                VALUE = upDownValue.Value.Value,
                                DISCOUNT = list[buffList[dataGridProductOut.SelectedIndex]].DISCOUNT,
                                MANUFACTURER = list[buffList[dataGridProductOut.SelectedIndex]].MANUFACTURER,
                                GROUP = list[buffList[dataGridProductOut.SelectedIndex]].GROUP,
                                PACK = list[buffList[dataGridProductOut.SelectedIndex]].PACK,
                                MATERIAL = list[buffList[dataGridProductOut.SelectedIndex]].MATERIAL,
                                FORM = list[buffList[dataGridProductOut.SelectedIndex]].FORM,
                                WAYBILLID = list[buffList[dataGridProductOut.SelectedIndex]].WAYBILLID,
                                CODE = list[buffList[dataGridProductOut.SelectedIndex]].CODE
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
                        list[buffList[dataGridProductOut.SelectedIndex]].VALUE -= upDownValue.Value.Value;
                        dataGridProductOut.Items.Refresh();
                        dataGridProductToCheckOut.Items.Refresh();
                    }
                }
            }
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
                BackupAddtion(dataGridProductToCheckOut.SelectedIndex);
                dataGridProductToCheckOut.Items.Refresh();
                dataGridProductOut.Items.Refresh();
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
                textBlockDelivery.Text = "сдача:" + Math.Round((window.prePayment - summ),2).ToString();
            }
        }

        private void buttonSellToCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            
            while(listToCheck.Count > 0)
            {
                BackupAddtion(0);
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
            if (dataGridProductOut.ItemsSource != list)
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
    }
}
