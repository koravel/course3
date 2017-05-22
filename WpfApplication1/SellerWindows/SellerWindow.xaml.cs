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
    public partial class SellerWindow : Window
    {
        List<ProductInCheck> list = new List<ProductInCheck>();
        List<ProductInCheck> listToCheck = new List<ProductInCheck>();
        public SellerWindow()
        {
            InitializeComponent();
            list = DataBase.GetProductForSeller();
            dataGridProductOut.ItemsSource = list;
            dataGridProductToCheckOut.ItemsSource = listToCheck;
        }

        private void buttonSell_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProductOut.SelectedIndex != -1)
            {
                listToCheck.Add(list[dataGridProductOut.SelectedIndex]);
                dataGridProductToCheckOut.Items.Refresh();
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //DataBase.SetLog(idText, 1, 0, "Выход из системы...");
            new MainWindow().Show();
        }
    }
}
