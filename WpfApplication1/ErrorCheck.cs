using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class ErrorCheck
    {
        public static bool CheckPrice(string _currentString)
        {
            bool dotflag = false,valueFlag = true;
            int dotIndex=-1;
            if(_currentString!="")
            {
                int i = 0;
                bool breakFlag = false;
                while(i<_currentString.Length)
                {
                    if(i<=8)
                    {
                        if(_currentString[i]=='.')
                        {
                            if(dotflag == false)
                            {
                                if (i > 0 && i < _currentString.Length - 1)
                                {
                                    dotIndex=i;
                                    dotflag = true;
                                }
                                else
                                {
                                    i = _currentString.Length;
                                    valueFlag = false;
                                    breakFlag = true;
                                }
                            }
                        }
                        if (breakFlag == false && !Char.IsNumber(_currentString[i]) && i != dotIndex)
                        {
                            i = _currentString.Length;
                            valueFlag = false;
                        }
                    }
                    else
                    {
                        i = _currentString.Length;
                        valueFlag = false;
                    }
                    i++;
                }
                bool retFlag=false;
                if(dotflag == true && valueFlag == true)
                {
                    retFlag = true;
                }
                if(dotflag == false || valueFlag ==false)
                {
                    MessageBox.Show("Неверный формат! Формат: х.хх, не более 99999.99!");
                    retFlag = false;
                }
                return retFlag;
            }
            else
            {
                MessageBox.Show("Введите цену!");
                return false;
            }
        }

        public static bool CheckBeginEndDate(string _bdate,string _edate)
        {
            string[] bdate = new string[3];
            bdate=_bdate.Split('.');
            string[] edate = new string[3];
            edate = _edate.Split('.');
            if(Convert.ToInt32(bdate[2])<=Convert.ToInt32(edate[2]))
            {
                if(Convert.ToInt32(bdate[1])<=Convert.ToInt32(edate[1]))
                {
                    if(Convert.ToInt32(bdate[0])<=Convert.ToInt32(edate[0]))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Начальная дата не может быть больше конечной!");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Начальная дата не может быть больше конечной!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Начальная дата не может быть больше конечной!");
                return false;
            }
        }
    }
}
