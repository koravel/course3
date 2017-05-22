using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    class Converter
    {
        
        public static string DateConvert(string value)
        {
            string[] date = value.Split('.');
            string q = date[2] + "-" + date[1] + "-" + date[0];
            return q;
        }
        public static string CurrencyConvert(string value)
        {
            value = value.Remove(value.Length - 2);
            value = value.Replace(",", ".");
            return value;
        }
    }
}
