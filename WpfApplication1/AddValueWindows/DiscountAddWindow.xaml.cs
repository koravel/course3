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
        List<NameIdList> products = new List<NameIdList>();
        string idText;
        public bool flag = false;
        public Discount obj = new Discount();

        public DiscountAddWindow(string id)
        {
            InitializeComponent();
            ProductListUpdate();
            idText = id;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorCheck.DiscountEnterCheck(comboBoxProduct,datePickerBeginDate,upDownPrice,datePickerEndDate,products[comboBoxProduct.SelectedIndex].ID.ToString()))
                {
                    DataBase.Query(
                    new string[] { "@_id", "@_price", "@_bdate", "@_edate", "@_text" },
                    new string[] { products[comboBoxProduct.SelectedIndex].ID.ToString(), upDownPrice.Text, Converter.DateConvert(datePickerBeginDate.Text), Converter.DateConvert(datePickerEndDate.Text), textBoxDescription.Text },
                    "INSERT INTO `discounts`(`P_ID`,`D_PRICE`,`D_BDATE`,`D_EDATE`,`D_TEXT`)VALUES(@_id,@_price,@_bdate,@_edate,@_text);");
                    DataBase.SetLog(idText, 1, 2, "Создание акции,параметры:|код товара:" + products[comboBoxProduct.SelectedIndex].ID.ToString() + "|сроки:" + Converter.DateConvert(datePickerBeginDate.Text) + "-" + Converter.DateConvert(datePickerEndDate.Text) + "|");
                    obj.ID = int.Parse(DataBase.QueryRetCell(null, null, "SELECT MAX(D_ID) FROM discounts;"));
                    obj.NAME = products[comboBoxProduct.SelectedIndex].NAME.ToString();
                    obj.PRICE = float.Parse(upDownPrice.Text);
                    obj.BDATE = datePickerBeginDate.SelectedDate.Value;
                    obj.EDATE = datePickerEndDate.SelectedDate.Value;
                    obj.TEXT = textBoxDescription.Text;
                    flag = true;
                    this.Close();
                }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ProductAddWindow(idText).ShowDialog();
            ProductListUpdate();
        }

        private void ProductListUpdate()
        {
            products.Clear();
            products = DataBase.GetNameIdList(new string[] { "P_ID", "P_NAME" }, "SELECT P_ID,P_NAME FROM product;");
            comboBoxProduct.Items.Clear();
            for (int i = 0; i < products.Count; i++)
            {
                comboBoxProduct.Items.Add(products[i].NAME + "(#" + products[i].ID + ")");
            }
        }

        private void DateCheck_Click(object sender, SelectionChangedEventArgs e)
        {
            ErrorCheck.BEDateCheck(datePickerBeginDate,datePickerEndDate);
        }

        private void upDownPrice_KeyUp(object sender, KeyEventArgs e)
        {
            ErrorCheck.upDownDigitCheck(sender);
        }

        private void comboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxProduct.BorderBrush = ErrorCheck.SelectionCheck(comboBoxProduct.SelectedIndex, comboBoxProduct.Items.Count);
        }
    }
}
