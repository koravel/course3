using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class Converter
    {
        
        public static string DateConvert(string value)
        {
            string[] date = value.Split('.');
            return date[2] + "." + date[1] + "." + date[0];
        }
    }
}
