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
        public SellerWindow()
        {
            InitializeComponent();
            dataGridProductOut.ItemsSource = DataBase.GetProductForSeller();
        }

        private void buttonSell_Click(object sender, RoutedEventArgs e)
        {
            //dataGridProductToCheckOut.ItemsSource = DataBase.GetProductForSeller();
        }
    }
}
