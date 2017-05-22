
namespace WpfApplication1
{
    public class Check
    {
        public int ID {set; get; }
        public string NAME{set; get; }
        public string DATE { set; get; }
        public string PAYTYPE { set; get; }
        public string SUM { set; get; }
        public string PREPAYMENT { set; get; }
        public Check()
        {
            SUM = "";
            PREPAYMENT = "";
        }
    }
}
