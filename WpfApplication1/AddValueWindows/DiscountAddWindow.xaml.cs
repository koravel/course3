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
            int dataCorrect = 0;
            if (comboBoxProduct.SelectedItem == null)
            {
                comboBoxProduct.BorderBrush = Brushes.Red;
                MessageBox.Show("Выберите товар!");
            }
            else
            {
                dataCorrect++;
            }
            if(datePickerBeginDate.Text=="")
            {
                datePickerBeginDate.BorderBrush = Brushes.Red;
                MessageBox.Show("Введите дату начала!");
            }
            else
            {
                dataCorrect++;
            }
            if (datePickerEndDate.Text == "")
            {
                datePickerEndDate.BorderBrush = Brushes.Red;
                MessageBox.Show("Введите дату конца!");
            }
            else
            {
                dataCorrect++;
            }
            if(datePickerBeginDate.SelectedDate != null && datePickerEndDate.SelectedDate != null)
            {
                if (ErrorCheck.CheckBeginEndDate(datePickerBeginDate.SelectedDate.Value, datePickerEndDate.SelectedDate.Value))
                {
                    if (dataCorrect == 3)
                    {
                        DataBase.Query(
                        new string[] { "@_id", "@_price", "@_bdate", "@_edate", "@_text" },
                        new string[] { products[comboBoxProduct.SelectedIndex].ID.ToString(), upDownPrice.Text, Converter.DateConvert(datePickerBeginDate.Text), Converter.DateConvert(datePickerEndDate.Text), textBoxDescription.Text },
                        "INSERT INTO `discounts`(`P_ID`,`D_PRICE`,`D_BDATE`,`D_EDATE`,`D_TEXT`)VALUES(@_id,@_price,@_bdate,@_edate,@_text);");
                        DataBase.SetLog(idText, 1, 2, "Создание акции,параметры:|код товара:" + products[comboBoxProduct.SelectedIndex].ID.ToString() + "|сроки:" + Converter.DateConvert(datePickerBeginDate.Text) + "-" + Converter.DateConvert(datePickerEndDate.Text) + "|");
                        this.Close();
                    }
                }
                else
                {
                    datePickerBeginDate.BorderBrush = Brushes.Red;
                    datePickerEndDate.BorderBrush = Brushes.Red;
                    MessageBox.Show("Дата конца не может быть меньше даты начала!");
                }
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
            if(datePickerBeginDate.Text != "" && datePickerEndDate.Text !="")
            {
                if (DateTime.Parse(datePickerBeginDate.Text) > DateTime.Parse(datePickerEndDate.Text))
                {
                    datePickerBeginDate.BorderBrush = Brushes.Red;
                    datePickerEndDate.BorderBrush = Brushes.Red;
                }
                else
                {
                    datePickerBeginDate.BorderBrush = Brushes.PowderBlue;
                    datePickerEndDate.BorderBrush = Brushes.PowderBlue;
                }
            }
        }

        private void upDownPrice_KeyDown(object sender, KeyEventArgs e)
        {
            string s = (new KeyConverter()).ConvertToString(e.Key);
            int i = 0;
            if (s.Contains("NumPad"))
            {
                i = 6;
            }
            if(s[i] < '0' || s[i] > '9')
            {
                upDownPrice.BorderBrush = Brushes.Red;
            }
            else
            {
                upDownPrice.BorderBrush = Brushes.Gray;
            }
        }

        private void comboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxProduct.SelectedIndex != -1)
            {
                comboBoxProduct.BorderBrush = Brushes.Gray;
            }
            else
            {
                if(comboBoxProduct.Items.Count == 0)
                {
                    MessageBox.Show("Товары отсутствут, внесите данные.");
                }
            }
        }
    }
}
