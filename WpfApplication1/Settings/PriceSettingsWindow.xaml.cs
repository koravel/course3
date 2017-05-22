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
using System.Windows.Forms;

namespace WpfApplication1
{

    public partial class PriceSettingsWindow : Window
    {
        public PriceSettingsWindow()
        {
            InitializeComponent();

            textBoxPath.Text = Properties.Settings.Default.SaveArchive;
        }

        private void buttonPriceSettingsBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void buttonSearchArchiveDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            folderBrowserDialog.Description = "Поиск папки";
            string folderName = "";
            folderName = folderBrowserDialog.SelectedPath;
            if(folderName != "")
            {
                Properties.Settings.Default.SaveArchive = folderName;
                textBoxPath.Text = folderName;
                Properties.Settings.Default.Save();
            }
        }
    }
}
