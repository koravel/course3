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
    public partial class DiscountAddWindow : Window
    {
        public DiscountAddWindow()
        {
            InitializeComponent();
            List<Row> products=DataBase.QueryGetColumn("P_NAME","product");
            for (int i = 0; i < products.Count; i++)
            {
                comboBoxProduct.Items.Add(products[i].ROW);
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            object selectedProduct = comboBoxProduct.SelectedItem;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите товар!");
            }
            else
            {
                dataCorrect++;
            }
            if(ErrorCheck.CheckPrice(textBoxPrice.Text))
            {
                dataCorrect++;
            }
            if(datePickerBeginDate.Text=="")
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
            if(dataCorrect==4)
            {
                if(ErrorCheck.CheckBeginEndDate(datePickerBeginDate.Text,datePickerEndDate.Text))
                    {
                        string[] values = new string[5];
                        values[0] = selectedProduct.ToString();
                        values[1] = textBoxPrice.Text;
                        values[2] = datePickerBeginDate.Text;
                        values[3] = datePickerEndDate.Text;
                        values[4] = textBoxDescription.Text;
                        string[] valuesText = new string[5];
                        valuesText[0] = "@_product";
                        valuesText[1] = "@_price";
                        valuesText[2] = "@_bdate";
                        valuesText[3] = "@_edate";
                        valuesText[4] = "@_text";
                        DataBase.Query(valuesText, values, "INSERT INTO `discounts`(`P_ID`,`D_PRICE`,`D_BDATE`,`D_EDATE`,`D_TEXT`)VALUES((SELECT `product`.`P_ID` FROM `product` WHERE `product`.`P_NAME`=@_product),@_price,@_bdate,@_edate,@_text)");
                        this.Close();
                    }
            }
        }
    }
}
