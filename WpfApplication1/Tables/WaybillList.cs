using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class WaybillList
    {
        public int ID { set; get; }
        public string PRODUCT { set; get; }
        public int VALUE { set; get; }
        public float TRADEPRICE { set; get; }
        public DateTime BDATE { set; get; }
        public DateTime EDATE { set; get; }
    }
}
