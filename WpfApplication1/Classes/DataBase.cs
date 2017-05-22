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

            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT U_TYPE FROM user WHERE U_NAME = @_userName AND U_PASS=@_userPassword", con);
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

        public static void Query(string[] _changeParametersText,string[] _changeParameters, string _queryString)
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

        public static string QueryRetCell(string[] _changeParametersText, string[] _changeParameters, string _queryString)
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

        public static string[] QueryRetColumn(string[] _changeParametersText, string[] _changeParameters, string _queryString)
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
                    List<string> retArr = new List<string> { };
                    while (dr.Read())
                    {
                            retArr.Add(dr.GetString(0));
                    }
                    con.Close();
                    string[] StrArrRet = new string[retArr.Count];
                    for (int i = 0; i < retArr.Count;i++ )
                    {
                        StrArrRet[i] = retArr[i];
                    }
                        return StrArrRet;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
                return null;
            }
        }

        public static string[] QueryRetRow(string[] _changeParametersText, string[] _changeParameters, string _queryString)
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
                        string[] StrArrRet = new string[dr.FieldCount];
                        dr.Read();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            StrArrRet[i] = dr.GetString(i);
                        }
                        con.Close();
                        return StrArrRet;
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

        //public static List<string> QueryGetColumn(string _columnName,string _tableName)
        //{
        //    List<string> rows = new List<string>();
        //    using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
        //    {
        //        try
        //        {

        //            con.Open();
        //            MySqlCommand com = new MySqlCommand("SELECT "+_columnName+" FROM "+_tableName+";", con);
        //            MySqlDataReader dr = com.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                rows.Add(dr.GetString(_columnName));
        //            }
        //            con.Close();
        //            return rows;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        con.Close();
        //        return null;
        //    }
        //}

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
                    MySqlCommand com = new MySqlCommand("SELECT E_ID,E_NAME,E_TEL,E_POSITION,E_CONTRACT,E_INN FROM `employee`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        employees.Add(new Employee() 
                        {
                            ID=dr.GetInt32("E_ID"),
                            NAME=dr.GetString("E_NAME"),
                            TEL=dr.GetString("E_TEL"),
                            POSITION=dr.GetString("E_POSITION"),
                            CONTRACT=dr.GetInt32("E_CONTRACT"),
                            INN = dr.GetInt32("E_INN")
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

        public static List<Employee> GetEmployee(string _queryString, string[] _valuesText, string[] _values)
        {
            List<Employee> employees = new List<Employee>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        employees.Add(new Employee()
                        {
                            ID = dr.GetInt32("E_ID"),
                            NAME = dr.GetString("E_NAME"),
                            TEL = dr.GetString("E_TEL"),
                            POSITION = dr.GetString("E_POSITION"),
                            CONTRACT = dr.GetInt32("E_CONTRACT"),
                            INN = dr.GetInt32("E_INN")
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
                            ID = dr.GetInt32("C_ID"),
                            DATE = dr.GetString("C_DATE"),
                            PAYTYPE = dr.GetString("C_PAYTYPE"),
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
   
        public static List<Check> GetCheck(string _queryString,string[] _valuesText,string[] _values)
        {
            List<Check> checks = new List<Check>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for(int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i],_values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        checks.Add(new Check()
                        {
                            ID = dr.GetInt32("C_ID"),
                            DATE = dr.GetString("C_DATE"),
                            PAYTYPE = dr.GetString("C_PAYTYPE"),
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
                    MySqlCommand com = new MySqlCommand("SELECT distinct product.P_ID,CONCAT( product.P_NAME,\"(#\",product.P_ID,\")\") AS 'P_NAME',cl.CL_VALUE,product_actual_price.PAP_PRICE FROM check_list cl,product,`check` c,product_actual_price WHERE cl.P_ID=product_actual_price.P_ID AND cl.P_ID=product.P_ID AND cl.C_ID=c.C_ID AND product_actual_price.PAP_PRICE=ifnull((select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID and pap.PAP_DATE<=c.C_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=cl.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1)) AND c.C_ID=@_curid;", con);
                    com.Parameters.AddWithValue("@_curid",_curid);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        checklists.Add(new CheckList()
                        {
                            ID = dr.GetInt32("P_ID"),
                            PRODUCT = dr.GetString("P_NAME"),
                            VALUE = dr.GetInt32("CL_VALUE"),
                            PRICE = dr.GetFloat("PAP_PRICE"),
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
                    MySqlCommand com = new MySqlCommand("SELECT discounts.D_ID,product.P_NAME,discounts.D_PRICE,discounts.D_BDATE,discounts.D_EDATE,discounts.D_TEXT FROM `discounts`,`product` WHERE `discounts`.`P_ID`=`product`.`P_ID`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        discounts.Add(new Discount()
                        {
                            ID = dr.GetInt32("D_ID"),
                            NAME = dr.GetString("P_NAME"),
                            PRICE = dr.GetFloat("D_PRICE"),
                            BDATE = dr.GetDateTime("D_BDATE"),
                            EDATE = dr.GetDateTime("D_EDATE"),
                            TEXT = dr.GetString("D_TEXT")
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

        public static List<Discount> GetDiscount(string _queryString, string[] _valuesText, string[] _values)
        {
            List<Discount> discounts = new List<Discount>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        discounts.Add(new Discount()
                        {
                            ID = dr.GetInt32("D_ID"),
                            NAME = dr.GetString("P_NAME"),
                            PRICE = dr.GetFloat("D_PRICE"),
                            BDATE = dr.GetDateTime("D_BDATE"),
                            EDATE = dr.GetDateTime("D_EDATE"),
                            TEXT = dr.GetString("D_TEXT")
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
                    MySqlCommand com = new MySqlCommand("SELECT M_ID,M_NAME,M_COUNTRY,M_CITY,M_ADDR,M_TEL FROM manufacturer;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        manufacturers.Add(new Manufacturer()
                        {
                            ID = dr.GetInt32("M_ID"),
                            NAME = dr.GetString("M_NAME"),
                            COUNTRY = dr.GetString("M_COUNTRY"),
                            CITY = dr.GetString("M_CITY"),
                            ADDR = dr.GetString("M_ADDR"),
                            TEL = dr.GetString("M_TEL")
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

        public static List<Manufacturer> GetManufacturer(string _queryString, string[] _valuesText, string[] _values)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        manufacturers.Add(new Manufacturer()
                        {
                            ID = dr.GetInt32("M_ID"),
                            NAME = dr.GetString("M_NAME"),
                            COUNTRY = dr.GetString("M_COUNTRY"),
                            CITY = dr.GetString("M_CITY"),
                            ADDR = dr.GetString("M_ADDR"),
                            TEL = dr.GetString("M_TEL")
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
                    MySqlCommand com = new MySqlCommand("SELECT product.P_ID,product.P_NAME,manufacturer.M_NAME,product.P_GROUP,product.P_PACK,product.P_MATERIAL,product.P_FORM,product.P_INSTR FROM `product`,`manufacturer` WHERE `manufacturer`.`M_ID`=`product`.`M_ID`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product()
                        {
                            ID = dr.GetInt32("P_ID"),
                            NAME = dr.GetString("P_NAME"),
                            MANUFACTURER = dr.GetString("M_NAME"),
                            GROUP = dr.GetString("P_GROUP"),
                            PACK = dr.GetString("P_PACK"),
                            MATERIAL=dr.GetString("P_MATERIAL"),
                            FORM = dr.GetString("P_FORM"),
                            INSTR = dr.GetString("P_INSTR")
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

        public static List<Product> GetProduct(string _queryString, string[] _valuesText, string[] _values)
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product()
                        {
                            ID = dr.GetInt32("P_ID"),
                            NAME = dr.GetString("P_NAME"),
                            MANUFACTURER = dr.GetString("M_NAME"),
                            GROUP = dr.GetString("P_GROUP"),
                            PACK = dr.GetString("P_PACK"),
                            MATERIAL = dr.GetString("P_MATERIAL"),
                            FORM = dr.GetString("P_FORM"),
                            INSTR = dr.GetString("P_INSTR")
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

        public static List<ProductActualPrice> GetProductActualPrice(string _curid)
        {
            List<ProductActualPrice> productActualPrices = new List<ProductActualPrice>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT product_actual_price.PAP_PRICE,product_actual_price.PAP_DATE FROM `product`,`product_actual_price` WHERE `product_actual_price`.`P_ID`=`product`.`P_ID` AND `product_actual_price`.`P_ID`=@_curid;", con);
                    com.Parameters.AddWithValue("@_curid", _curid);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        productActualPrices.Add(new ProductActualPrice()
                        {
                            PRICE = dr.GetString("PAP_PRICE"),
                            DATE = dr.GetDateTime("PAP_DATE"),
                           
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return productActualPrices;
            }
        }

        public static List<Waybill> GetWaybill()
        {
            List<Waybill> waybills = new List<Waybill>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT waybill.W_ID,waybill.W_DATE,employee.E_NAME,waybill.W_AGENT_NAME FROM `waybill`,`employee` WHERE `waybill`.`E_ID`=`employee`.`E_ID`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        waybills.Add(new Waybill()
                        {
                            ID = dr.GetInt32("W_ID"),
                            DATE = dr.GetDateTime("W_DATE"),
                            EMPLOYEE = dr.GetString("E_NAME"),
                            AGENT = dr.GetString("W_AGENT_NAME")
                        });

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return waybills;
            }
        }

        public static List<Waybill> GetWaybill(string _queryString, string[] _valuesText, string[] _values)
        {
            List<Waybill> waybills = new List<Waybill>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        waybills.Add(new Waybill()
                        {
                            ID = dr.GetInt32("W_ID"),
                            DATE = dr.GetDateTime("W_DATE"),
                            EMPLOYEE = dr.GetString("E_NAME"),
                            AGENT = dr.GetString("W_AGENT_NAME")
                        });

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return waybills;
            }
        }

        public static List<WaybillList> GetWaybillList(string _curid)
        {
            List<WaybillList> waybillLists = new List<WaybillList>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT product.P_ID,product.P_NAME,waybill_list.WL_VALUE,waybill_list.WL_TRADE_PRICE,waybill_list.WL_BDATE,waybill_list.WL_EDATE FROM `waybill`,`waybill_list`,`product` WHERE `waybill_list`.`W_ID`=`waybill`.`W_ID` AND `waybill_list`.`P_ID`=`product`.`P_ID` AND waybill_list.W_ID=@_curid;", con);
                    com.Parameters.AddWithValue("@_curid", _curid);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        waybillLists.Add(new WaybillList()
                        {
                            ID = dr.GetInt32("P_ID"),
                            PRODUCT = dr.GetString("P_NAME"),
                            VALUE = dr.GetInt32("WL_VALUE"),
                            TRADEPRICE = dr.GetFloat("WL_TRADE_PRICE"),
                            BDATE = dr.GetDateTime("WL_BDATE"),
                            EDATE = dr.GetDateTime("WL_EDATE")
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return waybillLists;
            }
        }

        public static List<User> GetUser()
        {
            List<User> users = new List<User>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT U_TYPE,U_NAME,U_PASS FROM `user`;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        users.Add(new User()
                        {
                            TYPE=dr.GetString("U_TYPE"),
                            LOGIN=dr.GetString("U_NAME"),
                            PASS=dr.GetString("U_PASS")
                        });

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return users;
            }
        }

        public static List<NameIdList> GetNameIdList(string[] _values, string _queryString)
        {
            List<NameIdList> products = new List<NameIdList>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new NameIdList()
                        {
                            ID = dr.GetInt32(_values[0]),
                            NAME = dr.GetString(_values[1])
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

        public static List<DiscountInfo> GetDiscountInfoList(string[] _valuesText,string[] _values, string _queryString)
        {
            List<DiscountInfo> discountInfo = new List<DiscountInfo>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        discountInfo.Add(new DiscountInfo()
                        {
                            NAME = dr.GetString("D_ID"),
                            PROCENT = dr.GetString("D_PRICE")
                        });

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return discountInfo;
            }
        }

        public static List<WaybillInfo> GetWaybillInfoList(string[] _valuesText, string[] _values, string _queryString)
        {
            List<WaybillInfo> waybillInfo = new List<WaybillInfo>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand(_queryString, con);
                    for (int i = 0; i < _values.Length; i++)
                    {
                        com.Parameters.AddWithValue(_valuesText[i], _values[i]);
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        waybillInfo.Add(new WaybillInfo()
                        {
                            OVERDUE = dr.GetString("OVERDUE"),
                            NOTOVERDUE = dr.GetString("NOTOVERDUE"),
                            SOLD = dr.GetString("PS_COUNT")
                        });

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                con.Close();
                return waybillInfo;
            }
        }

        public static void SetLog(string id,int actor, int type, string description)
        {
            string time = DataBase.QueryRetCell(null, null, "SELECT now();");
            string logOutput = time+"|",typeOut = null;
            switch (actor)
            {
                case 0:
                    {
                        logOutput += "System";
                        break;
                    }
                case 1:
                    {
                        logOutput += "User";
                        break;
                    }
            }
            logOutput += "|--ID=[" + id + "]:";
            switch(type)
            {
                case 0:
                    {
                        logOutput += "Select";
                        typeOut = "Select";
                        break;
                    }
                case 1:
                    {
                        logOutput += "Update";
                        typeOut = "Update";
                        break;
                    }
                case 2:
                    {
                        logOutput += "Insert";
                        typeOut = "Insert";
                        break;
                    }
                case 3:
                    {
                        logOutput += "Delete";
                        typeOut = "Delete";
                        break;
                    }
            }
            logOutput += "{" + description + "}";
            string[] dateTime= time.Split(' ');
            DataBase.Query(
                new string[] { "@_id", "@_type", "@_time", "@_text" },
                new string[] { id, typeOut, Converter.DateConvert(dateTime[0]) + " " + dateTime[1], logOutput }, "INSERT INTO user_action(U_ID,UA_TYPE,UA_DATETIME,UA_DESCRIPTION)VALUES(@_id,@_type,@_time,@_text)");
        }

        public static List<ProductInCheck> GetProductForSeller()
        {
            List<ProductInCheck> product = new List<ProductInCheck>();
            using (MySqlConnection con = new MySqlConnection(MSqlConB.ConnectionString))
            {
         
                    con.Open();
                    MySqlCommand com = new MySqlCommand("SELECT p.P_ID,p.P_NAME,ifnull((select pap.PAP_PRICE from product_actual_price pap,waybill w where pap.P_ID=p.P_ID and pap.PAP_DATE<=w.W_DATE  order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1),(select pap.PAP_PRICE from product_actual_price pap where pap.P_ID=p.P_ID order by pap.PAP_DATE desc,pap.PAP_PRICE desc limit 1)),wl.WL_TRADE_PRICE,(SELECT wl.WL_VALUE-ps.PS_COUNT FROM product_overdue WHERE WL_ID=wl.WL_ID AND PP_IS_OVERDUE='Не просрочено'),(SELECT if((SELECT COUNT(d.D_ID))>0,d.D_PRICE,0) FROM discounts d WHERE d.P_ID=p.P_ID AND d.D_BDATE>=NOW() AND d.D_EDATE<=NOW()),m.M_NAME,p.P_GROUP,p.P_PACK,p.P_MATERIAL,p.P_FORM,wl.WL_ID FROM product p,manufacturer m,product_sold ps,waybill_list wl WHERE wl.WL_ID=ps.WL_ID AND p.P_ID=wl.P_ID AND p.M_ID=m.M_ID AND (SELECT wl.WL_VALUE-ps.PS_COUNT FROM product_overdue WHERE WL_ID=wl.WL_ID AND PP_IS_OVERDUE='Не просрочено')>0;", con);
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        product.Add(new ProductInCheck(dr.GetString(2), dr.GetInt32(4))
                        {
                            ID = dr.GetInt32(0),
                            NAME = dr.GetString(1),
                            PRICE = dr.GetString(2),
                            TRADEPRICE = dr.GetString(3),
                            VALUE = dr.GetInt32(4),
                            DISCOUNT = dr.GetInt32(5),
                            MANUFACTURER = dr.GetString(6),
                            GROUP = dr.GetString(7),
                            PACK = dr.GetString(8),
                            MATERIAL = dr.GetString(9),
                            FORM = dr.GetString(10),
                            WAYBILLID = dr.GetInt32(11)
                        });

                    }
                    con.Close();
                    return product;
            }
        }
    }
}
