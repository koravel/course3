using System.Windows;

namespace WpfApplication1
{
    public partial class TransactionConfirm : Window
    {
        public bool flag = false;
        public TransactionConfirm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            this.Close();
        }
    }
}
