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

        public static bool CheckBeginEndDate(DateTime _bdate, DateTime _edate)
        {
            if(_bdate != null && _edate != null)
            {
                if (_bdate > _edate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
