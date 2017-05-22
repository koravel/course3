using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace WpfApplication1
{
    public partial class WindowCheckPrint : Window
    {
        string cid;
        public WindowCheckPrint(string id)
        {
            InitializeComponent();
            cid = id;
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            Check obj = (DataBase.GetCheck("SELECT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee` WHERE `check`.`E_ID`=`employee`.`E_ID` AND `check`.C_ID=@_id;", new string[] { "@_id" }, new string[] { cid }))[0]; 
            string[] tempMas = DataBase.QueryRetRow(new string[] { "@_id" }, new string[] { obj.ID.ToString() }, "SELECT C_PREPAYMENT,C_SUM FROM `check` WHERE C_ID=@_id");
            List<CheckPrint> tempList = DataBase.GetCheckPrint("SELECT distinct product.P_ID,product.P_NAME,cl.CL_VALUE,round(ifnull((select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID and pap.PAP_DATE<=c.C_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1))*cl.CL_VALUE*(select if((select count(d.D_ID))>0,exp(sum(log(1-d.D_PRICE*0.01))),1) from discounts d where d.P_ID=cl.P_ID and d.D_BDATE<=c.C_DATE and d.D_EDATE>=c.C_DATE),2),(SELECT if((SELECT COUNT(d.D_ID))>0,d.D_PRICE,0) from discounts d where d.P_ID=cl.P_ID) FROM check_list cl,product,`check` c,product_actual_price WHERE cl.P_ID=product_actual_price.P_ID AND cl.P_ID=product.P_ID AND cl.C_ID=c.C_ID AND product_actual_price.PAP_PRICE=ifnull((select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID and pap.PAP_DATE<=c.C_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1))  AND c.C_ID=@_curid;", new string[] { "@_curid" }, new string[] { obj.ID.ToString() });
            System.Windows.Forms.PrintPreviewDialog printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            string text = "\tЧП “АптекаТрейд”\n\tг. Харьков, ул.Сумская, 25\n\tтел. (057)741-56-89\nКАССИР	" + obj.NAME +
                "\n-------------------------------------------------------\n";
            if (tempList.Count > 0)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    text += tempList[i].PRODUCT + "\t" + tempList[i].VALUE + "*" + tempList[i].PRICE + " А\n\t\t\t=" + tempList[i].VALUE * tempList[i].PRICE + "\n-------------------------------------------------------\n";
                }
            }
            text += "СУММА\t\t\t" + tempMas[1] + "\nПДВ А = 20,00%\n-------------------------------------------------------\nАВАНС[" + obj.PAYTYPE + "]\t\t" + tempMas[0] + "\n" +
                "СДАЧА\t\t\t" + (Math.Round(float.Parse(tempMas[0]) - float.Parse(tempMas[1]), 2)).ToString() + "\n\t" + obj.DATE + "\n\tБлагодарим за покупку!\n\tФИСКАЛЬНЫЙ ЧЕК";
            printDocument.PrintPage += (sender1, args) =>
            {
                args.Graphics.DrawString(text, new System.Drawing.Font("Times New Roman", 14), new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.PointF(15.0F, 15.0F));

            };
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.PrintPreviewControl.Zoom = 1;
            printPreviewDialog.Show();
            this.Close();
        }
    }
}
