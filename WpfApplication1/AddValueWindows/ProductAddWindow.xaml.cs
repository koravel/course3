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
    /// <summary>
    /// Логика взаимодействия для ProductAddWindow.xaml
    /// </summary>
    public partial class ProductAddWindow : Window
    {
        public ProductAddWindow()
        {
            InitializeComponent();
            List<Row> comboBoxValues = DataBase.QueryGetColumn("M_NAME", "manufacturer");
            for (int i = 0; i < comboBoxValues.Count; i++)
            {
                comboBoxManufacturer.Items.Add(comboBoxValues[i].ROW);
            }
            comboBoxGroup.Items.Add("Без группы");
            comboBoxGroup.Items.Add("Антисептики");
            comboBoxGroup.Items.Add("Слабительные");
            comboBoxGroup.Items.Add("Противовирусные");
            comboBoxGroup.Items.Add("Антибакериальные");
            comboBoxGroup.Items.Add("Отхаркивающие и рвотные");
            comboBoxGroup.Items.Add("Антидепрессанты");
            comboBoxGroup.Items.Add("Снотворные");
            comboBoxGroup.Items.Add("Анальгетики");
            comboBoxGroup.Items.Add("Противогрибковые");
            comboBoxGroup.Items.Add("Противопаразитные");
            comboBoxGroup.Items.Add("Противовоспалительные");
            comboBoxGroup.Items.Add("Противозачаточные");
            comboBoxGroup.Items.Add("Противоалергические");
            comboBoxGroup.Items.Add("Гастроэнтероголические");
            comboBoxGroup.Items.Add("Противорвотные");
            comboBoxGroup.Items.Add("Противодиарейные");
            comboBoxGroup.Items.Add("Ингибиторы");
            comboBoxGroup.Items.Add("Бронхолитические");
            comboBoxGroup.Items.Add("Муколитические");
            comboBoxGroup.Items.Add("Противокашлевые");
            comboBoxGroup.Items.Add("Иммуномодуляторы");
            comboBoxGroup.Items.Add("Иммуноглобулины");
            comboBoxGroup.Items.Add("Витаминные");
            comboBoxGroup.Items.Add("Минеральные");
            comboBoxGroup.Items.Add("Поливитаминные");
            comboBoxGroup.Items.Add("Питательные смеси");
            comboBoxGroup.Items.Add("Жаропонижающие");
            comboBoxGroup.Items.Add("Антианемические");
            comboBoxGroup.Items.Add("Фитопрепараты");
            comboBoxGroup.Items.Add("Средства для контрацепции");
            comboBoxGroup.Items.Add("Сосудосуживающие");
            comboBoxGroup.Items.Add("Антиглаукомные");
            comboBoxGroup.Items.Add("Офтальмологические");
            comboBoxGroup.Items.Add("Проктологические");
            comboBoxGroup.Items.Add("Урологические");
            comboBoxGroup.Items.Add("Нефрологические");
            comboBoxGroup.Items.Add("Дерматологические");
            comboBoxGroup.Items.Add("Диагностические");
            comboBoxGroup.Items.Add("Перевязочные");
            comboBoxGroup.Items.Add("Токсикологические");
            comboBoxGroup.Items.Add("Антидоты");
            comboBoxGroup.Items.Add("Гомеопатические");
            comboBoxGroup.Items.Add("Эфирные масла");
            comboBoxGroup.Items.Add("Средства личной гигиены");
            comboBoxPack.Items.Add("Без упаковки");
            comboBoxPack.Items.Add("Ампула");
            comboBoxPack.Items.Add("Картонная коробка");
            comboBoxPack.Items.Add("Банка");
            comboBoxPack.Items.Add("Пакет");
            comboBoxPack.Items.Add("Полимерная");
            comboBoxPack.Items.Add("Баллон");
            comboBoxPack.Items.Add("Туба");
            comboBoxPack.Items.Add("Флакон");
            comboBoxPack.Items.Add("Бумага");
            comboBoxMaterial.Items.Add("Неизвестно");
            comboBoxMaterial.Items.Add("Жидкость");
            comboBoxMaterial.Items.Add("Сироп");
            comboBoxMaterial.Items.Add("Таблетки");
            comboBoxMaterial.Items.Add("Капсулы");
            comboBoxMaterial.Items.Add("Шприц-тюбики");
            comboBoxMaterial.Items.Add("Порошок");
            comboBoxMaterial.Items.Add("Гранулы");
            comboBoxMaterial.Items.Add("Растительные волокна");
            comboBoxMaterial.Items.Add("Аэрозоль");
            comboBoxMaterial.Items.Add("Суспензия");
            comboBoxMaterial.Items.Add("Мазь");
            comboBoxMaterial.Items.Add("Паста");
            comboBoxForm.Items.Add("с рецептом");
            comboBoxForm.Items.Add("без рецепта");
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
                string[] values = new string[8];
                values[0] = DataBase.QueryRetCell(null, null, "SELECT MAX(P_ID)+1 FROM product;");
                values[1] = textBoxName.Text;
                object selectedItem = comboBoxManufacturer.SelectedItem;
                values[2] = selectedItem.ToString();
                selectedItem = comboBoxGroup.SelectedItem;
                values[3] = selectedItem.ToString();
                selectedItem = comboBoxPack.SelectedItem;
                values[4] = selectedItem.ToString();
                selectedItem = comboBoxMaterial.SelectedItem;
                values[5] = selectedItem.ToString();
                selectedItem = comboBoxForm.SelectedItem;
                values[6] = selectedItem.ToString();
                values[7] = textBoxPrice.Text;
                
                string[] valuesText = new string[8];
                valuesText[0] = "@_id";
                valuesText[1] = "@_name";
                valuesText[2] = "@_manufacturer";
                valuesText[3] = "@_group";
                valuesText[4] = "@_pack";
                valuesText[5] = "@_material";
                valuesText[6] = "@_form";
                valuesText[7] = "@_instr";
                
                DataBase.Query(valuesText, values, "INSERT INTO `product`(`P_ID`,`P_NAME`,`M_ID`,`P_GROUP`,`P_PACK`,`P_MATERIAL`,`P_FORM`,`P_INSTR`)VALUES(@_id,@_name,(SELECT M_ID FROM manufacturer WHERE M_NAME=@_manufacturer),@_group,@_pack,@_material,@_form,@_instr);");
                string[] values2 = new string[3];
                values2[0] = values[0];
                values2[1] = textBoxPrice.Text;
        
                string[] date = datePickerToday.Text.Split('.');
                values2[2] = date[2] + "." + date[1] + "." + date[0];
                string[] valuesText2 = new string[3];
                valuesText2[0] = "@_id";
                valuesText2[1] = "@_price";
                valuesText2[2] = "@_date";
                DataBase.Query(valuesText2, values2, "INSERT INTO `product_actual_price`(`P_ID`,`PAP_PRICE`,`PAP_DATE`)VALUES(@_id,@_price,@_date);");
                this.Close();
            }

        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
