using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public partial class DirectorPassWindow : Window
    {
        public bool flag = false;
        public DirectorPassWindow()
        {
            InitializeComponent();
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBoxPass.BorderBrush = ErrorCheck.TextCheck(textBoxPass.Text, 0, Brushes.Red);
            if (DataBase.computeMD5(textBoxPass.Text) == DataBase.QueryRetCell(null, null, "SELECT PW_PASS FROM passwords WHERE PW_TYPE='1';"))
            {
                flag = true;
                this.Close();
            }
        }
    }
}
