using System.Windows;
using System.Windows.Forms;

namespace WpfApplication1
{

    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            textBoxPath.Text = Properties.Settings.Default.SaveArchive;
            checkBoxDiscountsDel.IsChecked = !Properties.Settings.Default.DuplDiscount;
            checkBoxEmployeesDel.IsChecked = !Properties.Settings.Default.DelBindingToEmployee;
            checkBoxManufacturersDel.IsChecked = !Properties.Settings.Default.DelBindingToManufacturer;
            checkBoxProductsDel.IsChecked = !Properties.Settings.Default.DelBindingToProduct1;
            checkBoxWaybillsDel.IsChecked = !Properties.Settings.Default.DelBindingToWaybill;
        }

        private void buttonSettingsBack_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DuplDiscount = !checkBoxDiscountsDel.IsChecked.Value;
            Properties.Settings.Default.DelBindingToEmployee = !checkBoxEmployeesDel.IsChecked.Value;
            Properties.Settings.Default.DelBindingToManufacturer = !checkBoxManufacturersDel.IsChecked.Value;
            Properties.Settings.Default.DelBindingToProduct1 = !checkBoxProductsDel.IsChecked.Value;
            Properties.Settings.Default.DelBindingToWaybill = !checkBoxWaybillsDel.IsChecked.Value;
            Properties.Settings.Default.Save();
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
