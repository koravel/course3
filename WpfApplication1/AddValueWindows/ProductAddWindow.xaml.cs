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
    public partial class ProductAddWindow : Window
    {
        List<NameIdList> comboBoxValues = DataBase.GetNameIdList(new string [] { "M_ID", "M_NAME" }, "SELECT M_ID,M_NAME FROM manufacturer");
        public ProductAddWindow()
        {
            InitializeComponent();
            for (int i = 0; i < comboBoxValues.Count; i++)
            {
                comboBoxManufacturer.Items.Add(comboBoxValues[i].NAME+"(#"+comboBoxValues[i].ID+")");
            }
            datePickerToday.Text = DateTime.Today.ToString();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            if(textBoxName.Text == "")
            {
                MessageBox.Show("Введите название!");
            }
            else
            {
                dataCorrect++;
            }
            if(comboBoxManufacturer.SelectedItem == null)
            {
                MessageBox.Show("Укажите производителя!");
            }
            else
            {
                dataCorrect++;
            }
            if(comboBoxForm.SelectedItem == null)
            {
                MessageBox.Show("Укажите форму отпуска!");
            }
            else
            {
                dataCorrect++;
            }
            if (ErrorCheck.CheckPrice(textBoxPrice.Text))
            {
                dataCorrect++;
            }
            if(dataCorrect == 4)
            {
                string maxId = DataBase.QueryRetCell(null, null, "SELECT MAX(P_ID)+1 FROM product;");
                DataBase.Query(
                new string[] { "@_id", "@_name", "@_manufacturer", "@_group", "@_pack", "@_material", "@_form", "@_instr"},
                new string[] { maxId, textBoxName.Text, comboBoxValues[comboBoxManufacturer.SelectedIndex].ID.ToString(), comboBoxGroup.SelectedItem.ToString(), comboBoxPack.SelectedItem.ToString(), comboBoxMaterial.SelectedItem.ToString(), comboBoxForm.SelectedItem.ToString(), textBoxInstruction.Text},
                "INSERT INTO `product`(`P_ID`,`P_NAME`,`M_ID`,`P_GROUP`,`P_PACK`,`P_MATERIAL`,`P_FORM`,`P_INSTR`)VALUES(@_id,@_name,@_manufacturer,@_group,@_pack,@_material,@_form,@_instr);");

                DataBase.Query(
                new string[] { "@_id", "@_price", "@_date" },
                new string[] { maxId, textBoxPrice.Text, Converter.DateConvert(datePickerToday.Text) },
                "INSERT INTO `product_actual_price`(`P_ID`,`PAP_PRICE`,`PAP_DATE`)VALUES(@_id,@_price,@_date);");
                this.Close();
            }

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
    }
}
