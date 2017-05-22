using System.Text;
using System.IO;

namespace WpfApplication1
{
    public class Files
    {
        public static void SaveToArchive(string curId,string text,string type)
        {
            if(Directory.Exists(@"" + Properties.Settings.Default.SaveArchive))
            {
                StreamWriter w = new StreamWriter(File.Create(@"" + Properties.Settings.Default.SaveArchive + "\\" + type + curId + ".txt"), Encoding.GetEncoding(1251));
                w.WriteLine(text);
                w.Close();
            }
            else
            {
                Directory.CreateDirectory(@"" + Properties.Settings.Default.SaveArchive);
            }
        }
    }
}
