using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class AddPrepaymentWindow : Window
    {
        float summ;
        public float prePayment;
        public bool flag = false;
        public AddPrepaymentWindow(float summTemp)
        {
            summ = summTemp;
            InitializeComponent();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            (sender as TextBox).BorderBrush = ErrorCheck.TextCheck((sender as TextBox).Text, 1, Brushes.Red);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int dataCorrect = 0;
            textBoxSumm.BorderBrush = ErrorCheck.TextCheck(textBoxSumm.Text,ref dataCorrect,1,Brushes.Red);
            if(dataCorrect == 1)
            {
                if (summ <= float.Parse(textBoxSumm.Text))
                {
                    prePayment = float.Parse(textBoxSumm.Text);
                    flag = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Аванс не может быть меньше стоимости!");
                    textBoxSumm.BorderBrush = Brushes.Red;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
