﻿using System;
using System.Windows.Controls;

namespace WpfApplication1
{
    class Converter
    {
        
        public static string DateConvert(string value)
        {
            if(value != null)
            {
                string[] date = value.Split('.');
                if(date.Length>2)
                {
                    string result = date[2] + "-" + date[1] + "-" + date[0];
                    return result;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        public static string CurrencyConvert(string value)
        {
            if(value != null)
            {
                value = value.Remove(value.Length - 2);
                value = value.Replace(",", ".");
                return value;
            }
            else
            {
                return "0";
            }
        }

        public static float CurrencyToFloatConvert(string value)
        {
            if (value != null)
            {
                if (value.Contains("р."))
                {
                    value = value.Remove(value.Length - 2);
                }
                value.Replace('.',',');
                return float.Parse(value);
            }
            else
            {
                return 0;
            }
        }

        public static string FloatToCurrencyConvert(string value)
        {
            if (value != null)
            {
                value = value.Replace(",", ".");
                return value;
            }
            else
            {
                return "0";
            }
        }

        public static string DGCellToStringConvert(int row,int cloumn,DataGrid dataGrid)
        {
            var cellInfo = new DataGridCellInfo(dataGrid.Items[row], dataGrid.Columns[cloumn]);
            var content = cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock;
            if(content != null)
            {
                return content.Text;
            }
            else
            {
                return null;
            }
        }

        public static double CailingRound(double value)
        {
            value *= 100;
            double intvalue = Math.Truncate(value);
            value = value - intvalue;
            if(value > 0)
            {
                value = intvalue + 1;
            }
            return value * 0.01;
        }
    }
}
