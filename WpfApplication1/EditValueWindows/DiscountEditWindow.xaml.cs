using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApplication1
{
    public partial class DiscountEditWindow : Window
    {
        List<NameIdList> products = new List<NameIdList>();
        string curId,idText;
        int selectNum,count = 0;
        public bool flag = false;
        public Discount obj;
        public DiscountEditWindow(string id,string _curId,Discount tempObj)
        {
            InitializeComponent();
            ProductListUpdate();
            curId = _curId;
            idText = id;
            obj = tempObj;
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

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorCheck.DiscountEnterCheck(comboBoxProduct,datePickerBeginDate,upDownPrice,datePickerEndDate,"0"))
            {
                DataBase.Query(
                new string[] { "@_id", "@_price", "@_bdate", "@_edate", "@_text", "@_curid" },
                new string[] { products[comboBoxProduct.SelectedIndex].ID.ToString(), upDownPrice.Text, Converter.DateConvert(datePickerBeginDate.Text), Converter.DateConvert(datePickerEndDate.Text), textBoxDescription.Text, curId },
                "UPDATE discounts SET P_ID = @_id,D_PRICE = @_price,D_BDATE = @_bdate,D_EDATE = @_edate,D_TEXT = @_text WHERE D_ID = @_curid;");
                DataBase.SetLog(idText, 1, 1, "Изменение акции,параметры:|код:" + curId + "|");
                obj.NAME = products[comboBoxProduct.SelectedIndex].NAME.ToString();
                obj.PRICE = float.Parse(upDownPrice.Text);
                obj.BDATE = datePickerBeginDate.SelectedDate.Value;
                obj.EDATE = datePickerEndDate.SelectedDate.Value;
                obj.TEXT = textBoxDescription.Text;
                flag = true;
                this.Close();
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

        private void comboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBoxProduct.BorderBrush = ErrorCheck.SelectionCheck(comboBoxProduct.SelectedIndex, comboBoxProduct.Items.Count);
        }

        private void upDownPrice_KeyUp(object sender, KeyEventArgs e)
        {
            ErrorCheck.upDownDigitCheck(sender);
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ErrorCheck.BEDateCheck(datePickerBeginDate, datePickerEndDate);
        }
    }
}
