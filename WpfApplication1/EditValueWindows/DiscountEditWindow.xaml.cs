﻿using System;
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
    public partial class DiscountEditWindow : Window
    {
        List<NameIdList> products = new List<NameIdList>();
        string curId,idText;
        int selectNum,count = 0;
        public DiscountEditWindow(string id,string _curId)
        {
            InitializeComponent();
            ProductListUpdate();
            curId = _curId;
            idText = id;
            string[] data = new string[5];
            selectNum = -1;
            data = DataBase.QueryRetRow(new string[] { "@curid" }, new string[] { _curId }, "SELECT discounts.P_ID,discounts.D_PRICE,discounts.D_BDATE,discounts.D_EDATE,discounts.D_TEXT FROM `discounts`,`product` WHERE `discounts`.`D_ID`=@curid AND discounts.P_ID=product.P_ID;");
            for (int i = 0; i < products.Count; i++)
            {
                comboBoxProduct.Items.Add(products[i].NAME + "(#" + products[i].ID + ")");
                if(products[i].ID.ToString() == data[0])
                {
                    selectNum = i;
                }
            }
            comboBoxProduct.SelectedIndex = selectNum;
            count = comboBoxProduct.Items.Count;
            upDownPrice.Text = data[1].Replace(",", ".");
            datePickerBeginDate.Text = data[2];
            datePickerEndDate.Text = data[3];
            textBoxDescription.Text = data[4];
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            if (comboBoxProduct.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар!");
            }
            else
            {
                dataCorrect++;
            }
            if (datePickerBeginDate.Text == "")
            {
                MessageBox.Show("Введите дату начала!");
            }
            else
            {
                dataCorrect++;
            }
            if (datePickerEndDate.Text == "")
            {
                MessageBox.Show("Введите дату конца!");
            }
            else
            {
                dataCorrect++;
            }
            if (dataCorrect == 3)
            {
                if (ErrorCheck.CheckBeginEndDate(datePickerBeginDate.SelectedDate.Value, datePickerEndDate.SelectedDate.Value))
                {
                    DataBase.Query(
                    new string[] { "@_id", "@_price", "@_bdate", "@_edate", "@_text", "@_curid" },
                    new string[] { products[comboBoxProduct.SelectedIndex].ID.ToString(), upDownPrice.Text, Converter.DateConvert(datePickerBeginDate.Text), Converter.DateConvert(datePickerEndDate.Text), textBoxDescription.Text, curId },
                    "UPDATE discounts SET P_ID = @_id,D_PRICE = @_price,D_BDATE = @_bdate,D_EDATE = @_edate,D_TEXT = @_text WHERE D_ID = @_curid;");
                    DataBase.SetLog(idText, 1, 1, "Изменение акции,параметры:|код:" + curId + "|");
                    this.Close();
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ProductAddWindow(idText).ShowDialog();
            ProductListUpdate();
            for (int i = 0; i < products.Count; i++)
            {
                comboBoxProduct.Items.Add(products[i].NAME + "(#" + products[i].ID + ")");
            }
            if (comboBoxProduct.Items.Count > count)
            {
                comboBoxProduct.SelectedIndex = comboBoxProduct.Items.Count - 1;
            }
            else
            {
                comboBoxProduct.SelectedIndex = selectNum;
            }
        }

        private void ProductListUpdate()
        {
            products.Clear();
            products = DataBase.GetNameIdList(new string[] { "P_ID", "P_NAME" }, "SELECT P_ID,P_NAME FROM product;");
            comboBoxProduct.Items.Clear();
        }
    }
}
