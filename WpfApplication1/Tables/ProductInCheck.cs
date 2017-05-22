using System;

namespace WpfApplication1
{
    public class ProductInCheck : ProductAdvanced
    {
        public string CURPRICE { get; set; }
        public string SUMM { get; set; }
        public int WAYBILLID { get; set; }

        public ProductInCheck(string price,int value,int discount)
        {

            CURPRICE = Converter.CailingRound((float.Parse(price) * (100 - discount) * 0.01)).ToString();
            SUMM = Math.Round(decimal.Parse((float.Parse(CURPRICE) * value).ToString()), 2).ToString();
        }
    }
}
