using System.Windows;

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
            new MainWindow().Show();
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

        private void buttonDirectorSettings_Click(object sender, RoutedEventArgs e)
        {
            new DirectorSettingsWindow().ShowDialog();
        }

        private void buttonDirectorLogs_Click(object sender, RoutedEventArgs e)
        {
            new LogWindow().Show();
        }
    }
}
