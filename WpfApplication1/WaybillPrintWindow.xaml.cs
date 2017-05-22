using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace WpfApplication1
{
    public partial class WaybillPrintWindow : Window
    {

        public WaybillPrintWindow(string text, string employeetemp, string agenttemp,string summtemp, List<WaybillPrint> list)
        {
            InitializeComponent();
            LabelText.Content = text;
            dataGridWaybillPrint.ItemsSource = list;
            LabelEmployee.Content += employeetemp;
            LabelAgent.Content += agenttemp;
            LabelSumm.Content += summtemp;
        }


        public static void SaveToPNG(FrameworkElement frameworkElement, Size size, string fileName)
        {
            using (FileStream stream = new FileStream(string.Format("{0}.png", fileName), FileMode.Create))
            {
                SaveToPNG(frameworkElement, size, stream);
                stream.Close();
            }
            
        }

        public static void SaveToPNG(FrameworkElement frameworkElement, Size size, Stream stream)
        {
            Transform transform = frameworkElement.LayoutTransform;
            frameworkElement.LayoutTransform = null;
            Thickness margin = frameworkElement.Margin;
            frameworkElement.Margin = new Thickness(0, 0, margin.Right - margin.Left, margin.Bottom - margin.Top);
            frameworkElement.Measure(size);
            frameworkElement.Arrange(new Rect(size));
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(frameworkElement);
            frameworkElement.LayoutTransform = transform;
            frameworkElement.Margin = margin;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Interlace = PngInterlaceOption.On;
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                System.Drawing.Bitmap bitmap;
                SaveToPNG(this, new Size(Width = this.Width, Height = this.Height), "imagetemp");
                bitmap = new System.Drawing.Bitmap("imagetemp.png");
                System.Windows.Forms.PrintPreviewDialog printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                printDocument.PrintPage += (sender1, args) =>
                {
                    args.Graphics.DrawImage(bitmap, 0, 0);

                };
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.PrintPreviewControl.Zoom = 1;
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Close();
                bitmap.Dispose();
                File.Delete("imagetemp.png");
            }
            else
            {
                if(e.Key == Key.Escape)
                {
                    this.Close();
                }
            }
        }
    }
}
