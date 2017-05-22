using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //string adminId = DataBase.QueryRetCell(null, null, "SELECT U_ID FROM `user` WHERE U_TYPE='Администратор';");
        //public AdminWindow admin = new AdminWindow(null);

        //private void Application_Exit(object sender, ExitEventArgs e)
        //{
        //    if (DataBase.QueryRetCell(new string[] { "@_id" }, new string[] { adminId }, "SELECT U_ONLINE FROM `user` WHERE U_ID=@_id;") == "online")
        //    {
        //        DataBase.Connection();
        //        DataBase.Query(null, null, "UPDATE `pharmacy_db`.`user` SET `U_ONLINE`='offline' WHERE `U_ID`='1';");
        //    }
        //}
    }
}
