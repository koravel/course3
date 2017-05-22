using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApplication1
{
    public class Files
    {
        public static void SaveToArchive(string curId,string text,string type)
        {
            StreamWriter w = new StreamWriter(File.Create(@"" + Properties.Settings.Default.SaveArchive + "\\" + type + curId + ".txt"), Encoding.GetEncoding(1251));
                w.WriteLine(text);
                w.Close();
        }
    }
}
