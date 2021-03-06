﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class ProductEditWindow : Window
    {
        List<NameIdList> comboBoxValues = new List<NameIdList>();
        string curId,idText;
        int curMan,count;
        public Product objout;
        public bool flag = false;
        public ProductEditWindow(string id,string _curId,Product tempobj)
        {
            InitializeComponent();
            curId = _curId;
            idText = id;
            objout = tempobj;
            string[] date = DataBase.QueryRetRow(new string[] { "@_id" }, new string[] { objout.ID.ToString() }, "SELECT IF((SELECT DATE_ADD(pap.PAP_DATE, INTERVAL 7 DAY) FROM product_actual_price pap WHERE pap.PAP_ID=p.PAP_ID)>=now(),(DATE_ADD(p.PAP_DATE, INTERVAL 7 DAY)),0),now() FROM product_actual_price p WHERE p.PAP_ID=(SELECT MAX(PAP_ID) FROM product_actual_price WHERE P_ID=p.P_ID) AND p.P_ID=@_id;");
            if(date[0] == "0")
            {
                labelTimeToAdd.Content += "доступно";
                datePickerToday.IsEnabled = true;
                upDownPrice.IsEnabled = true;
            }
            else
            {
                string result = ((TimeSpan)(DateTime.Parse(date[0]) - DateTime.Parse(date[1]))).Days.ToString();
                labelTimeToAdd.Content += result;
                switch(int.Parse(result)%10)
                {
                    case 1:
                        {
                            labelTimeToAdd.Content += " день";
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                        {
                            labelTimeToAdd.Content += " дня";
                            break;
                        }
                    default:
                        {
                            labelTimeToAdd.Content += " дней";
                            break;
                        }
                }
            }
            ManufacturerListUpdate();
            string[] data = new string[9];
            curMan = -1;
            bool flag = false;
            data = DataBase.QueryRetRow(new string[] { "@curid" }, new string[] { _curId }, "SELECT product.P_NAME,manufacturer.M_NAME,product.P_GROUP,product.P_PACK,product.P_MATERIAL,product.P_FORM,product_actual_price.PAP_PRICE,product_actual_price.PAP_DATE,product.P_INSTR,product.P_CODE FROM product,manufacturer,product_actual_price WHERE product.M_ID=manufacturer.M_ID AND product.P_ID=product_actual_price.P_ID AND product.P_ID=@curid ORDER BY product_actual_price.PAP_DATE DESC LIMIT 1;");
            textBoxName.Text = data[0];
            for (int i = 0; i < comboBoxValues.Count; i++)
            {
                comboBoxManufacturer.Items.Add(comboBoxValues[i].NAME + "(#" + comboBoxValues[i].ID + ")");
                if(comboBoxValues[i].NAME == data[1] && flag == false)
                {
                    curMan = i;
                    flag = true;
                }
            }
            comboBoxManufacturer.SelectedIndex = curMan;
            comboBoxGroup.SelectedIndex = comboBoxGroup.Items.IndexOf(data[2]);
            comboBoxPack.SelectedIndex = comboBoxPack.Items.IndexOf(data[3]);
            comboBoxMaterial.SelectedIndex = comboBoxMaterial.Items.IndexOf(data[4]);
            comboBoxForm.SelectedIndex = comboBoxForm.Items.IndexOf(data[5]);
            upDownPrice.Text = data[6];
            datePickerToday.Text = data[7];
            textBoxInstruction.Text = data[8];
            textBoxCode.Text = data[9];
            count = comboBoxManufacturer.Items.Count;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorCheck.ProductEnterCheck(textBoxName, comboBoxManufacturer, comboBoxGroup, comboBoxPack, comboBoxMaterial, comboBoxForm, upDownPrice, datePickerToday, textBoxCode))
            {
                objout.NAME = textBoxName.Text;
                objout.MANUFACTURER = comboBoxValues[comboBoxManufacturer.SelectedIndex].NAME.ToString();
                objout.GROUP = comboBoxGroup.SelectedItem.ToString();
                objout.PACK = comboBoxPack.SelectedItem.ToString();
                objout.FORM = comboBoxForm.SelectedItem.ToString();
                objout.INSTR = textBoxInstruction.Text;
                objout.CODE = textBoxCode.Text;
                DataBase.Query(
                new string[] { "@_id", "@_name", "@_manufacturer", "@_group", "@_pack", "@_material", "@_form", "@_instr","@_code" },
                new string[] { curId, textBoxName.Text, comboBoxValues[comboBoxManufacturer.SelectedIndex].ID.ToString(), comboBoxGroup.SelectedItem.ToString(), comboBoxPack.SelectedItem.ToString(), comboBoxMaterial.SelectedItem.ToString(), comboBoxForm.SelectedItem.ToString(), textBoxInstruction.Text,textBoxCode.Text },
                "UPDATE product SET P_NAME  = @_name,M_ID = @_manufacturer,P_GROUP = @_group,P_PACK = @_pack,P_MATERIAL = @_material,P_FORM = @_form,P_INSTR = @_instr,P_CODE=@_code WHERE P_ID = @_id;");
                DataBase.SetLog(idText, 1, 1, "Изменение товара,параметры:|код:" + curId + "|название:" + textBoxName.Text + "|группа:" + comboBoxGroup.SelectedItem.ToString() + "|");
                DataBase.Query(
                new string[] { "@_id", "@_price", "@_date" },
                new string[] { curId, upDownPrice.Text.Replace(',','.'), Converter.DateConvert(datePickerToday.Text) },
                "INSERT INTO `product_actual_price`(`P_ID`,`PAP_PRICE`,`PAP_DATE`)VALUES(@_id,@_price,@_date);");
                DataBase.SetLog(idText, 1, 2, "Создание цены товара,параметры:|код товара:" + curId + "|дата изменения:" + Converter.DateConvert(datePickerToday.Text) + "|цена:" + Converter.DateConvert(datePickerToday.Text) + "|");
                flag = true;
                this.Close();
            }

        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ManufacturerAddWindow(idText).ShowDialog();
            ManufacturerListUpdate();
            for (int i = 0; i < comboBoxValues.Count; i++)
            {
                comboBoxManufacturer.Items.Add(comboBoxValues[i].NAME + "(#" + comboBoxValues[i].ID + ")");
            }
            if (comboBoxManufacturer.Items.Count > count)
            {
                comboBoxManufacturer.SelectedIndex = comboBoxManufacturer.Items.Count - 1;
            }
            else
            {
                comboBoxManufacturer.SelectedIndex = curMan;
            }
        }

        private void ManufacturerListUpdate()
        {
            comboBoxValues.Clear();
            comboBoxValues = DataBase.GetNameIdList(new string[] { "M_ID", "M_NAME" }, "SELECT M_ID,M_NAME FROM manufacturer");
            comboBoxManufacturer.Items.Clear();
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Red);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).Name)
            {
                case "comboBoxManufacturer":
                case "comboBoxForm":
                    {
                        (sender as ComboBox).BorderBrush = ErrorCheck.SelectionCheck((sender as ComboBox).SelectedIndex, (sender as ComboBox).Items.Count);
                        break;
                    }
                default:
                    {
                        (sender as ComboBox).BorderBrush = ErrorCheck.SelectionCheck((sender as ComboBox).SelectedIndex);
                        break;
                    }
            }

        }

        private void upDownPrice_KeyUp(object sender, KeyEventArgs e)
        {
            ErrorCheck.upDownDigitCheck(sender);
        }

        private void datePickerToday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as DatePicker).BorderBrush = ErrorCheck.TextCheck((sender as DatePicker).Text, 0, Brushes.Red);
        }

        private void textBoxCode_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxCode.BorderBrush = ErrorCheck.TextCheck(textBoxCode.Text, 1, Brushes.Red);
        }
    }
}
