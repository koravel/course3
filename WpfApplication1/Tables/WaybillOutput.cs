using System;
using System.Windows.Media;

namespace WpfApplication1
{
    public class WaybillOutput
    {
        public string ID { set; get; }
        public string NAME { set; get; }
        public string VALUE { set; get; }
        public string TRADEPRICE { set; get; }
        public DateTime BDATE { set; get; }
        public DateTime EDATE { set; get; }

        public WaybillOutput() 
        {
            ID = "NULL";
            VALUE = "0";
            TRADEPRICE = "0,00";
            BDATE = DateTime.Today;
            EDATE = DateTime.Today;
        }
    }
}
