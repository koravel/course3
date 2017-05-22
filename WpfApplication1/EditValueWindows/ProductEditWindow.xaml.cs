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
    public partial class ProductEditWindow : Window
    {
        List<NameIdList> comboBoxValues = DataBase.GetNameIdList(new string[] { "M_ID", "M_NAME" }, "SELECT M_ID,M_NAME FROM manufacturer");
        string curId;
        public ProductEditWindow(string _curId)
        {
            InitializeComponent();
            curId = _curId;
            string[] data = new string[9];
            int curMan = -1;
            bool flag = false;
            data = DataBase.QueryRetRow(new string[] { "@curid" }, new string[] { _curId }, "SELECT product.P_NAME,manufacturer.M_NAME,product.P_GROUP,product.P_PACK,product.P_MATERIAL,product.P_FORM,product_actual_price.PAP_PRICE,product_actual_price.PAP_DATE,product.P_INSTR FROM product,manufacturer,product_actual_price WHERE product.M_ID=manufacturer.M_ID AND product.P_ID=product_actual_price.P_ID AND product.P_ID=@curid ORDER BY product_actual_price.PAP_DATE DESC LIMIT 1;");
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
            textBoxPrice.Text = data[6].Replace(",", ".");
            datePickerToday.Text = data[7];
            textBoxInstruction.Text = data[8];
            comboBoxManufacturer.Items.Add("Добавить нового...");
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Введите название!");
            }
            else
            {
                dataCorrect++;
            }
            if (comboBoxManufacturer.SelectedItem == null)
            {
                MessageBox.Show("Укажите производителя!");
            }
            else
            {
                dataCorrect++;
            }
            if (comboBoxForm.SelectedItem == null)
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
            if (dataCorrect == 4)
            {
                DataBase.Query(
                new string[] { "@_id", "@_name", "@_manufacturer", "@_group", "@_pack", "@_material", "@_form", "@_instr" },
                new string[] { curId, textBoxName.Text, comboBoxValues[comboBoxManufacturer.SelectedIndex].ID.ToString(), comboBoxGroup.SelectedItem.ToString(), comboBoxPack.SelectedItem.ToString(), comboBoxMaterial.SelectedItem.ToString(), comboBoxForm.SelectedItem.ToString(), textBoxInstruction.Text },
                "UPDATE product SET P_NAME  = @_name,M_ID = @_manufacturer,P_GROUP = @_group,P_PACK = @_pack,P_MATERIAL = @_material,P_FORM = @_form,P_INSTR = @_instr WHERE P_ID = @_id;");
                if(checkBoxNewPrice.IsChecked == true)
                {
                    DataBase.Query(
                    new string[] { "@_id", "@_price", "@_date" },
                    new string[] { curId, textBoxPrice.Text, Converter.DateConvert(datePickerToday.Text) },
                    "INSERT INTO `product_actual_price`(`P_ID`,`PAP_PRICE`,`PAP_DATE`)VALUES(@_id,@_price,@_date);");
                }
                else
                {
                    DataBase.Query(
                    new string[] { "@_id", "@_price", "@_date","@_curid" },
                    new string[] { curId, textBoxPrice.Text, Converter.DateConvert(datePickerToday.Text), DataBase.QueryRetCell(new string[] { "@_tempid" }, new string[] { curId }, "SELECT PAP_ID from product_actual_price WHERE P_ID=@_tempid ORDER BY PAP_DATE DESC LIMIT 1;") },
                    "UPDATE product_actual_price SET P_ID = @_id,PAP_PRICE = @_price,PAP_DATE = @_date WHERE PAP_ID = @_curid;");
                }
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

        private void comboBoxManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxManufacturer.SelectedIndex == comboBoxManufacturer.Items.Count)
            {
                new ManufacturerAddWindow().ShowDialog();
            }
        }
    }
}
