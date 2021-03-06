﻿using System;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace WpfApplication1
{
    class ErrorCheck
    {

        public static void BEDateCheck(object begin,object end)
        {
            if ((begin as DatePicker).Text != "" && (end as DatePicker).Text != "")
            {
                if (DateTime.Parse((begin as DatePicker).Text) > DateTime.Parse((end as DatePicker).Text))
                {
                    (begin as DatePicker).BorderBrush = Brushes.Red;
                    (end as DatePicker).BorderBrush = Brushes.Red;
                }
                else
                {
                    (begin as DatePicker).BorderBrush = Brushes.PowderBlue;
                    (end as DatePicker).BorderBrush = Brushes.PowderBlue;
                }
            }
        }

        public static void upDownDigitCheck(object sender)
        {
            bool flag = false;
            switch(sender.GetType().ToString())
            {
                case "Xceed.Wpf.Toolkit.IntegerUpDown":
                    {
                        for (int i = 0; i < (sender as IntegerUpDown).Text.Length; i++) 
                        {
                            if((sender as IntegerUpDown).Text[i] < '0' || (sender as IntegerUpDown).Text[i] > '9')
                            {
                                flag = true;
                                i = (sender as IntegerUpDown).Text.Length;
                            }
                        }
                        if (flag)
                        {
                            (sender as IntegerUpDown).BorderBrush = Brushes.Red;
                        }
                        else
                        {
                            (sender as IntegerUpDown).BorderBrush = Brushes.Gray;
                        }
                        break;
                    }
                case "Xceed.Wpf.Toolkit.DecimalUpDown":
                    {
                        for (int i = 0; i < (sender as DecimalUpDown).Text.Length; i++)
                        {
                            if ((sender as DecimalUpDown).Text[i] < '0' || (sender as DecimalUpDown).Text[i] > '9')
                            {
                                if ((sender as DecimalUpDown).Text[i] != ',')
                                {
                                    flag = true;
                                    i = (sender as DecimalUpDown).Text.Length;
                                }
                            }
                        }
                        if (flag)
                        {
                            (sender as DecimalUpDown).BorderBrush = Brushes.Red;
                        }
                        else
                        {
                            (sender as DecimalUpDown).BorderBrush = Brushes.Gray;
                        }
                        break;
                    }
            }
        }

        public static SolidColorBrush TextCheck(string text,ref int flag,int mode,SolidColorBrush customColor)
        {
            if (text == "")
            {
                return customColor;
            }
            else
            {
                if (mode == 0)
                {
                    flag++;
                    return Brushes.Gray;
                }
                else
                {
                    if(mode == 1 || mode == 2)
                    {
                        bool checkSymbols = true;
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (!Char.IsDigit(text[i]))
                            {
                                if(mode == 2)
                                {
                                    if(text[i] != ',')
                                    {
                                        checkSymbols = false;
                                        i = text.Length;
                                    }
                                }
                                else
                                {
                                    checkSymbols = false;
                                    i = text.Length;
                                }
                            }
                        }
                        if (checkSymbols)
                        {
                            flag++;
                            return Brushes.Gray;
                        }
                        else
                        {
                            return customColor;
                        }
                    }
                    else
                    {
                        return customColor;
                    }
                }
            }
        }

        public static SolidColorBrush TextCheck(string text, int mode, SolidColorBrush customColor)
        {
            if (text == "")
            {
                return customColor;
            }
            else
            {
                if (mode == 0)
                {
                    return Brushes.Gray;
                }
                else
                {
                    if (mode == 1)
                    {
                        bool checkSymbols = true;
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (!Char.IsDigit(text[i]))
                            {
                                if (mode == 2)
                                {
                                    if (text[i] != ',')
                                    {
                                        checkSymbols = false;
                                        i = text.Length;
                                    }
                                }
                                else
                                {
                                    checkSymbols = false;
                                    i = text.Length;
                                }
                            }
                        }
                        if (checkSymbols)
                        {
                            return Brushes.Gray;
                        }
                        else
                        {
                            return customColor;
                        }
                    }
                    else
                    {
                        return customColor;
                    }
                }
            }
        }

        public static bool TextCheck(string text, int mode)
        {
            if (text == "")
            {
                return false;
            }
            else
            {
                if (mode == 0)
                {
                    return true;
                }
                else
                {
                    if (mode == 1)
                    {
                        bool checkSymbols = true;
                        for (int i = 0; i < text.Length; i++)
                        {
                            if (!Char.IsDigit(text[i]))
                            {
                                if (mode == 2)
                                {
                                    if (text[i] != ',')
                                    {
                                        checkSymbols = false;
                                        i = text.Length;
                                    }
                                }
                                else
                                {
                                    checkSymbols = false;
                                    i = text.Length;
                                }
                            }
                        }
                        if (checkSymbols)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static SolidColorBrush SelectionCheck(int index,int itemsCount, ref int flag)
        {
            if (index != -1)
            {
                flag++;
                return Brushes.Gray;
            }
            else
            {
                if (itemsCount == 0)
                {
                    System.Windows.MessageBox.Show("Данные отсутствуют, внесите новые.");
                    return Brushes.Blue;
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }

        public static SolidColorBrush SelectionCheck(int index)
        {
            if (index != -1)
            {
                return Brushes.Gray;
            }
            else
            {
                return Brushes.Yellow;
            }
        }

        public static SolidColorBrush SelectionCheck(int index, int itemsCount)
        {
            if (index != -1)
            {
                return Brushes.Gray;
            }
            else
            {
                if (itemsCount == 0)
                {
                    System.Windows.MessageBox.Show("Данные отсутствуют, внесите новые.");
                    return Brushes.Yellow;
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }

        public static bool DiscountEnterCheck(ComboBox product,DatePicker begin,IntegerUpDown price,DatePicker end,string pid)
        {
            int dataCorrect = 0;
            product.BorderBrush = SelectionCheck(product.SelectedIndex,product.Items.Count,ref dataCorrect);
            begin.BorderBrush = TextCheck(begin.Text, ref dataCorrect, 0, Brushes.Red);
            price.BorderBrush = TextCheck(price.Text, ref dataCorrect, 1, Brushes.Red);
            end.BorderBrush = TextCheck(end.Text, ref dataCorrect, 0, Brushes.Red);
            if (begin.SelectedDate != null && end.SelectedDate != null)
            {
                if (begin.SelectedDate > end.SelectedDate)
                {
                    begin.BorderBrush = Brushes.Red;
                    end.BorderBrush = Brushes.Red;
                    System.Windows.MessageBox.Show("Дата конца не может быть меньше даты начала!");
                    return false;
                }
                else
                {
                    if (dataCorrect == 4)
                    {
                        if (int.Parse(DataBase.QueryRetCell(new string[] { "@_bdate", "@_edate", "@_pid" }, new string[] { Converter.DateConvert(begin.Text), Converter.DateConvert(end.Text), pid }, "SELECT COUNT(D_ID) FROM discounts WHERE D_BDATE=@_bdate AND D_EDATE=@_edate AND P_ID=@_pid;")) > 0)
                        {
                            if(Properties.Settings.Default.DuplDiscount)
                            {
                                return true;
                            }
                            else
                            {
                                DuplicateDiscountWindow window = new DuplicateDiscountWindow();
                                window.ShowDialog();
                                if (window.flag)
                                {
                                    if (pid != "-1")
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                begin.BorderBrush = Brushes.Red;
                end.BorderBrush = Brushes.Red;
                return false;
            }
        }

        public static bool EmployeeEnterCheck(ComboBox position,TextBox name,TextBox contract,TextBox inn,TextBox tel,bool mode)
        {
            int dataCorrect = 0;
            name.BorderBrush = TextCheck(name.Text, ref dataCorrect, 0, Brushes.Red);
            position.BorderBrush = SelectionCheck(position.SelectedIndex, position.Items.Count, ref dataCorrect);
            contract.BorderBrush = TextCheck(contract.Text, ref dataCorrect, 1, Brushes.Red);
            inn.BorderBrush = TextCheck(inn.Text, ref dataCorrect, 1, Brushes.Red);
            tel.BorderBrush = TextCheck(tel.Text, 1, Brushes.Yellow);
            if(dataCorrect == 4)
            {
                if(mode)
                {
                    if (int.Parse(DataBase.QueryRetCell(new string[] { "@_contract", "@_inn" }, new string[] { contract.Text, inn.Text }, "SELECT COUNT(E_ID) FROM employee WHERE E_CONTRACT=@_contract OR E_INN=@_inn;")) > 0)
                    {
                        System.Windows.MessageBox.Show("Такой ИНН или контракт уже существуют!");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool ManufacturerEnterCheck(TextBox name,TextBox country,TextBox city,TextBox address,TextBox tel)
        {
            int dataCorrect = 0;
            name.BorderBrush = TextCheck(name.Text, ref dataCorrect, 0, Brushes.Red);
            country.BorderBrush = TextCheck(country.Text, ref dataCorrect, 0, Brushes.Red);
            city.BorderBrush = TextCheck(city.Text, 0, Brushes.Yellow);
            address.BorderBrush = TextCheck(address.Text, 0, Brushes.Yellow);
            tel.BorderBrush = TextCheck(tel.Text, 0, Brushes.Yellow);
            if(dataCorrect == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ProductEnterCheck(TextBox name,ComboBox manufacturer,ComboBox group,ComboBox pack,ComboBox material,ComboBox form,DecimalUpDown price,DatePicker pricedate,TextBox code)
        {
            int dataCorrect = 0;
            name.BorderBrush = TextCheck(name.Text, ref dataCorrect, 0, Brushes.Red);
            manufacturer.BorderBrush = SelectionCheck(manufacturer.SelectedIndex, manufacturer.Items.Count, ref dataCorrect);
            form.BorderBrush = SelectionCheck(form.SelectedIndex, form.Items.Count, ref dataCorrect);
            price.BorderBrush = TextCheck(price.Text, ref dataCorrect, 2, Brushes.Red);
            pricedate.BorderBrush = TextCheck(pricedate.Text, ref dataCorrect, 0, Brushes.Red);
            code.BorderBrush = TextCheck(code.Text, ref dataCorrect, 1, Brushes.Red);
            group.BorderBrush = SelectionCheck(group.SelectedIndex);
            pack.BorderBrush = SelectionCheck(pack.SelectedIndex);
            material.BorderBrush = SelectionCheck(material.SelectedIndex);
            if (dataCorrect == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WaybillEnterCheck(ComboBox employee,TextBox agent,DatePicker date)
        {
            int dataCorrect = 0;
            employee.BorderBrush = SelectionCheck(employee.SelectedIndex, employee.Items.Count, ref dataCorrect);
            agent.BorderBrush = TextCheck(agent.Text, ref dataCorrect, 0, Brushes.Red);
            date.BorderBrush = TextCheck(date.Text, ref dataCorrect, 0, Brushes.Red);
            if(dataCorrect == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UserEnterCheck(ComboBox type, TextBox login, TextBox pass)
        {
            int dataCorrect = 0;
            type.BorderBrush = SelectionCheck(type.SelectedIndex, type.Items.Count, ref dataCorrect);
            if (DataBase.QueryRetCell(new string[] { "@_type" }, new string[] { type.SelectedItem.ToString() }, "SELECT COUNT(U_TYPE) from `user` WHERE U_TYPE=@_type;") != "0")
            {
                System.Windows.MessageBox.Show("Типы записей уникальны!");
            }
            else
            {
                dataCorrect++;
            }
            login.BorderBrush = TextCheck(login.Text, ref dataCorrect, 0, Brushes.Red);
            pass.BorderBrush = TextCheck(pass.Text, ref dataCorrect, 0, Brushes.Red);
            if (dataCorrect == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UserEnterCheck(TextBox login, TextBox pass)
        {
            int dataCorrect = 0;
            login.BorderBrush = TextCheck(login.Text, ref dataCorrect, 0, Brushes.Red);
            pass.BorderBrush = TextCheck(pass.Text, ref dataCorrect, 0, Brushes.Red);
            if (dataCorrect == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
