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
    public partial class CheckConWindow : Window
    {
        public bool flag;
        public CheckConWindow()
        {
            flag = false;
            InitializeComponent();
        }

        private void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
