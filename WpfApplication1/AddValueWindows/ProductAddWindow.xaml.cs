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
    public partial class ProductAddWindow : Window
    {
        List<NameIdList> comboBoxValues = new List<NameIdList> { };
        string idText;
        public bool flag = false;
        public Product obj = new Product();
        public ProductAddWindow(string id)
        {
            InitializeComponent();
            ManufacturerListUpdate();
            datePickerToday.Text = DateTime.Today.ToString();
            idText = id;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if(ErrorCheck.ProductEnterCheck(textBoxName,comboBoxManufacturer,comboBoxGroup,comboBoxPack,comboBoxMaterial,comboBoxForm,upDownPrice,datePickerToday,textBoxCode))
            {
                if(comboBoxGroup.SelectedIndex == -1)
                {
                    comboBoxGroup.SelectedIndex = 0;
                }
                if (comboBoxPack.SelectedIndex == -1)
                {
                    comboBoxPack.SelectedIndex = 0;
                }
                if (comboBoxMaterial.SelectedIndex == -1)
                {
                    comboBoxMaterial.SelectedIndex = 0;
                }
                string maxId = DataBase.QueryRetCell(null, null, "SELECT IFNULL(MAX(P_ID)+1,1) FROM product;");
                DataBase.Query(
                new string[] { "@_id", "@_name", "@_manufacturer", "@_group", "@_pack", "@_material", "@_form", "@_instr"},
                new string[] { maxId, textBoxName.Text, comboBoxValues[comboBoxManufacturer.SelectedIndex].ID.ToString(), comboBoxGroup.SelectedItem.ToString(), comboBoxPack.SelectedItem.ToString(), comboBoxMaterial.SelectedItem.ToString(), comboBoxForm.SelectedItem.ToString(), textBoxInstruction.Text},
                "INSERT INTO `product`(`P_ID`,`P_NAME`,`M_ID`,`P_GROUP`,`P_PACK`,`P_MATERIAL`,`P_FORM`,`P_INSTR`)VALUES(@_id,@_name,@_manufacturer,@_group,@_pack,@_material,@_form,@_instr);");
                DataBase.Query(
                new string[] { "@_id", "@_price", "@_date" },
                new string[] { maxId, upDownPrice.Text.Replace(',', '.'), Converter.DateConvert(datePickerToday.Text) },
                "INSERT INTO `product_actual_price`(`P_ID`,`PAP_PRICE`,`PAP_DATE`)VALUES(@_id,@_price,@_date);");
                DataBase.Query(new string[] { "@_id" }, new string[] { maxId }, "INSERT INTO product_quantity(P_ID)VALUES(@_id)");
                DataBase.SetLog(idText, 1, 2, "Создание товара,параметры:|код:" + maxId + "|название:" + textBoxName.Text + "|производитель код:" + comboBoxValues[comboBoxManufacturer.SelectedIndex].ID.ToString() + "|цена:" + upDownPrice.Text + "|");
                obj.ID = int.Parse(maxId);
                obj.NAME = textBoxName.Text;
                obj.MANUFACTURER = comboBoxValues[comboBoxManufacturer.SelectedIndex].ID.ToString();
                obj.GROUP = comboBoxGroup.SelectedItem.ToString();
                obj.PACK = comboBoxPack.SelectedItem.ToString();
                obj.MATERIAL = comboBoxMaterial.SelectedItem.ToString();
                obj.FORM = comboBoxForm.SelectedItem.ToString();
                obj.INSTR = textBoxInstruction.Text;
                flag = true;
                this.Close();
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ManufacturerListUpdate()
        {
            comboBoxValues.Clear();
            comboBoxValues = DataBase.GetNameIdList(new string[] { "M_ID", "M_NAME" }, "SELECT M_ID,M_NAME FROM manufacturer");
            comboBoxManufacturer.Items.Clear();
            for (int i = 0; i < comboBoxValues.Count; i++)
            {
                comboBoxManufacturer.Items.Add(comboBoxValues[i].NAME + "(#" + comboBoxValues[i].ID + ")");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ManufacturerAddWindow(idText).ShowDialog();
            ManufacturerListUpdate();
        }

        private void upDownPrice_KeyUp(object sender, KeyEventArgs e)
        {
            ErrorCheck.upDownDigitCheck(sender);
        }

        private void textBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 0, Brushes.Yellow);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch((sender as ComboBox).Name)
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

        private void textBoxCode_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxCode.BorderBrush = ErrorCheck.TextCheck(textBoxCode.Text, 1, Brushes.Red);
        }

    }
}
