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
        List<NameIdList> products = DataBase.GetNameIdList(new string[] { "P_ID", "P_NAME" },"SELECT P_ID,P_NAME FROM product;");
        public DiscountAddWindow()
        {
            InitializeComponent();
            for (int i = 0; i < products.Count; i++ )
            {
                comboBoxProduct.Items.Add(products[i].NAME + "(#"+products[i].ID+")");
            }
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
                MessageBox.Show("Выберите товар!");
            }
            else
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
            if(dataCorrect==3)
            {
                if(ErrorCheck.CheckBeginEndDate(datePickerBeginDate.Text,datePickerEndDate.Text))
                    {
                        DataBase.Query(
                        new string[] { "@_id", "@_price", "@_bdate", "@_edate", "@_text"},
                        new string[] { products[comboBoxProduct.SelectedIndex].ID.ToString(),upDownPrice.Text, Converter.DateConvert(datePickerBeginDate.Text), Converter.DateConvert(datePickerEndDate.Text), textBoxDescription.Text },
                        "INSERT INTO `discounts`(`P_ID`,`D_PRICE`,`D_BDATE`,`D_EDATE`,`D_TEXT`)VALUES(@_id,@_price,@_bdate,@_edate,@_text);");
                        //DataBase.SetLog(idText, 1, 3, DateTime.Now, "Создание акции,параметры: ");
                        this.Close();
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
    }
}
