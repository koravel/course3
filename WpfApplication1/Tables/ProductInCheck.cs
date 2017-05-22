using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class ProductInCheck : ProductAdvanced
    {
        public string CURPRICE { get; set; }
        public string SUMM { get; set; }

        public ProductInCheck(string price,int value)
        {
            CURPRICE = (float.Parse(price) * (100 - DISCOUNT) * 0.01).ToString();
            SUMM = (float.Parse(CURPRICE) * value).ToString();
        }
    }
}
