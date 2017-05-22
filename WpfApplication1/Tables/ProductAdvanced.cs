using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class ProductAdvanced : Product
    {
        public string PRICE { get; set; }
        public string TRADEPRICE { get; set; }
        public int VALUE { get; set; }
        public int DISCOUNT { get; set; }
    }
}
