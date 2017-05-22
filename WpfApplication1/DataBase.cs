using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Data;


namespace WpfApplication1
{
    class DataBase
    {
        static MySqlConnectionStringBuilder MSqlConB = new MySqlConnectionStringBuilder();

        public static void Connection(string[] tmpConStr)
        {
            MSqlConB.Server = tmpConStr[0];
            MSqlConB.Database = tmpConStr[1];
            MSqlConB.UserID = tmpConStr[2];
            MSqlConB.Password = tmpConStr[3];
        }

        public static void Connection()
        {
            MSqlConB.Server = Properties.Settings.Default.Address;
            MSqlConB.Database = Properties.Settings.Default.Database;
            MSqlConB.UserID = Properties.Settings.Default.Login;
            MSqlConB.Password = Properties.Settings.Default.Password;
        }

        public static string UserAuthorization(string _userName, string _userPassword)
        {
            string query = "SELECT U_TYPE FROM user WHERE U_NAME = @_userName AND U_PASS=@_userPassword";

            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(query, con);
                    com.Parameters.AddWithValue("@_userName", _userName);
                    com.Parameters.AddWithValue("@_userPassword", _userPassword);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        con.Close();
                        return dr.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
                return null;
            }
        }

        public static string computeMD5(string _userPassword)
        {
            string hash = string.Empty;

            byte[] bytes = Encoding.ASCII.GetBytes(_userPassword);
            MD5CryptoServiceProvider cps = new MD5CryptoServiceProvider();
            byte[] byteHash = cps.ComputeHash(bytes);

            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }

            return hash;
        }

        public static void TableOutput(string _queryString, DataGrid _dataGridOut)
        {
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    com.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter(_queryString, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    _dataGridOut.ItemsSource = dt.DefaultView;
                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }
        }

        public static void ExecuteQuery(string _queryString)
        {
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    com.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }
        }

        public static string CheckLogin(string _userName, string _userPassword)
        {
            string queryString = @"SELECT U_ONLINE FROM `user` WHERE U_NAME=@_userName AND U_PASS=@_userPassword;";
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(queryString, con);
                    com.Parameters.AddWithValue("@_userName", _userName);
                    com.Parameters.AddWithValue("@_userPassword", _userPassword);
                    MySqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        con.Close();
                        return dr.GetString(0);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
                return null;
            }
        }

        public static void FieldChange(string[] _changeParametersText,string[] _changeParameters, string _queryString)
        {
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    if (_changeParameters != null)
                    {
                        int n = _changeParameters.Length;
                        for (int i = 0; i < n; i++)
                        {
                            com.Parameters.AddWithValue(_changeParametersText[i], _changeParameters[i]);
                        }
                    }
                        com.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }

        }
        public static string FieldChangeWithResult(string[] _changeParametersText, string[] _changeParameters, string _queryString)
        {
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    if (_changeParameters != null)
                    {
                        int n = _changeParameters.Length;
                        for (int i = 0; i < n; i++)
                        {
                            com.Parameters.AddWithValue(_changeParametersText[i], _changeParameters[i]);
                        }
                    }
                        MySqlDataReader dr = com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            con.Close();
                            return dr.GetString(0);
                        }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
                return null;
            }

        }

        public static bool ConCheck(string[] database_settings)
        {
            DataBase.Connection(database_settings);
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    con.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                return true;
            }
        }

        public static List<Employee> GetEmployee()
        { 
            List<Employee> employees=new List<Employee>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT E_NAME,E_TEL,E_POSITION,E_CONTRACT FROM `employee`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        employees.Add(new Employee() 
                        {
                            NAME=dr.GetString("E_NAME"),
                            TEL=dr.GetString("E_TEL"),
                            POSITION=dr.GetString("E_POSITION"),
                            CONTRACT=dr.GetInt32("E_CONTRACT")
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return employees;
            }
        }

        public static List<Check> GetCheck()
        {
            List<Check> checks = new List<Check>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT `check`.C_ID,`check`.C_DATE,`check`.C_PAYTYPE,`employee`.E_NAME FROM `check`,`employee` WHERE `check`.`E_ID`=`employee`.`E_ID`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        checks.Add(new Check()
                        {
                            ID=dr.GetInt32("C_ID"),
                            DATE= dr.GetString("C_DATE"),
                            PAYTYPE=dr.GetString("C_PAYTYPE"),
                            NAME = dr.GetString("E_NAME")
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return checks;
            }
        }

        public static List<CheckList> GetCheckList(string _curid)
        {
            List<CheckList> checklists = new List<CheckList>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT product.P_NAME,check_list.CL_VALUE FROM `check_list`,`product`,`check` WHERE `check_list`.`P_ID`=`product`.`P_ID` AND check_list.C_ID=`check`.C_ID AND check_list.C_ID=@_curid;", con);
                    com.Parameters.AddWithValue("@_curid",_curid);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        checklists.Add(new CheckList()
                        {
                            PRODUCT=dr.GetString("P_NAME"),
                            VALUE = dr.GetInt32("CL_VALUE")
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return checklists;
            }
        }


        public static List<Discount> GetDiscount()
        {
            List<Discount> discounts = new List<Discount>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT product.P_NAME,discounts.D_PRICE,discounts.D_BDATE,discounts.D_EDATE,discounts.D_TEXT FROM `discounts`,`product` WHERE `discounts`.`P_ID`=`product`.`P_ID`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        discounts.Add(new Discount()
                        {
                            NAME=dr.GetString("P_NAME"),
                            PRICE=dr.GetFloat("D_PRICE"),
                            BDATE=dr.GetString("D_BDATE"),
                            EDATE=dr.GetString("D_EDATE"),
                            TEXT=dr.GetString("D_TEXT")
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return discounts;
            }
        }

        public static List<Manufacturer> GetManufacturer()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT manufacturer.M_NAME,manufacturer.M_COUNTRY,manufacturer.M_CITY,manufacturer.M_ADDR,manufacturer.M_TEL FROM `manufacturer`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        manufacturers.Add(new Manufacturer()
                        {
                            NAME=dr.GetString("M_NAME"),
                            COUNTRY=dr.GetString("M_COUNTRY"),
                            CITY=dr.GetString("M_CITY"),
                            ADDR=dr.GetString("M_ADDR"),
                            TEL=dr.GetString("M_TEL")
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return manufacturers;
            }
        }

        public static List<Product> GetProduct()
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT product.P_NAME,manufacturer.M_NAME,product.P_GROUP,product.P_PACK,product.P_MATERIAL,product.P_FORM,product.P_INSTR FROM `product`,`manufacturer` WHERE `manufacturer`.`M_ID`=`product`.`M_ID`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product()
                        {
                            NAME = dr.GetString("P_NAME"),
                            MANUFACTURER = dr.GetString("M_NAME"),
                            GROUP = dr.GetString("P_GROUP"),
                            PACK = dr.GetString("P_PACK"),
                            MATERIAL=dr.GetString("P_MATERIAL"),
                            FORM = dr.GetString("P_FORM"),
                            INSTR = dr.GetString("P_INSTR")
                            PRICE = dr.GetFloat()
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return products;
            }
        }

        //List<ProductActualPrice> GetProductActualPrice()
        //{
            //SELECT product_actual_price.PAP_PRICE,product_actual_price.PAP_DATE FROM `product`,`product_actual_price` WHERE `product_actual_price`.`P_ID`=`product`.`P_ID`;
        //}

        //public static List<Waybill> GetWaybill()
        //{

        //}

        ////public static List<User> GetUser()
        ////{

        ////}

    }
}
