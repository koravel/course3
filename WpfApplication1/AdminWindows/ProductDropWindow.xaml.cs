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
    public partial class ProductDropWindow : Window
    {
        List<ProductDrop> list = DataBase.GetProductDrop();
        string idText;
        public ProductDropWindow(string id)
        {
            InitializeComponent();
            dataGridProductDrop.ItemsSource = list;
            idText = id;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonDrop_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProductDrop.SelectedIndex != -1)
            {
                DataBase.Query(null,null,"UPDATE product_overdue SET PP_IS_OVERDUE='Продано';");
                DataBase.SetLog(idText,1,1,"Утилизация просроченного товара, код накладной="+((ProductDrop)dataGridProductDrop.SelectedItem).ID.ToString());
            }
        }


    }
}
