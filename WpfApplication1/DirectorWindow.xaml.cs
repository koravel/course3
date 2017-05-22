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
    public partial class DirectorWindow : Window
    {
        string idText;
        public DirectorWindow(string id)
        {
            InitializeComponent();
            idText = id;
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonLoginAsSeller_Click(object sender, RoutedEventArgs e)
        {
            new SellerWindow(idText).Show();
        }

        private void buttonLoginAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow(idText).Show();
        }
    }
}
