using System;

namespace WpfApplication1
{
    public class WaybillPrint : WaybillList
    {
        public string PACK { set; get; }
        public string MATERIAL { set; get; }
        public string PDV { set; get; }
        public string SUMM { set; get; }

        public WaybillPrint(int value,float tradeprice)
        {
            SUMM = (value * tradeprice).ToString();
            PDV = (Math.Round((float.Parse(SUMM) * 0.2),2)).ToString();
        }
    }
}
