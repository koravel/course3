   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Pharmacy DataBase!   WpfApplication1.AdminWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  3System&clr-namespace:System;assembly=mscorlib  -     �Name$  adminWindow�� � Title$  Администратор��$��513.677��$��1391.536��$��0q�$
��1100�� � Icon$, &/Pharmacy DataBase;component/icone.ico��.���  � ColumnDefinitions �� .+�o� $��29��$	��Top=�$��14(�.@�m� -    m�Name$ userControlMenuiItem��$1��+Управление уч. записями��m� -   $ menuItemSettings��$��Настройки��m� -   $ menuItemPrint��$��Печать��m� -   $ exitMenuItem��$��
Выход���� -    ��Name$ buttonUpdate��$��Обновить��$��0,0,5,9q�$��25��$��Bottom=�$��Right=�$��74��$��14(��� -   $ 	buttonAdd��$��Добавить��$��0,0,85,9q�$��25��$��Bottom=�$��Right=�$��74��$��14(��� -   $ buttonDelete��$��Удалить��$��	0,0,165,9q�$��25��$��Bottom=�$��Right=�$��74��$��14(��� -	   $ buttonSearch��$��	0,0,324,9q�$��Найти...��$��25��$��Bottom=�$��Right=�$��74��$��14(��� -
    ��Name$ tabControlTables��$��597,22,5,44q�$��14(�.@��� -    ��Name$ tabItemCheck��$��Чеки��$��
-1,0,-22,0q�.��� -    �Name$ 	gridCheck�����	#FFE5E5E5.+�� 	 � RowDefinitions	 �� �� $��48 � �� $
��256* ��� $
��128* �.+�&   System.Windows.Controls.DataGrid  -   
  Name$
 dataGridCheckOut��   AutoGenerateColumns .     
IsReadOnly . $��2��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Время��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *DATE+  $ Кассир�� �� *NAME+  $ Оплата�� �� *	PAYTYPE+  $ Код�� �� *ID+ � ContextMenu �� .@�m� -   $��Изменить��m� -   $��Удалить��m� -   $��Добавить��m� -   $��Обновить��  -   $
 dataGridCheckListOut�� .   . $��1��   $ 
Товар�� �� *	PRODUCT+  $ Количество�� �� *VALUE+  $ Цена�� �� *PRICE+   RowDetailsTemplate �� .R�  $%
 dataGridProductDiscountsDisplay��-    .  $   HorizontalScrollBarVisibility$
 Auto=�   CanUserDeleteRows .     CanUserAddRows .     CanUserResizeColumns .    CanUserReorderColumns .     CanUserResizeRows .  J� 	AllowDrop .   .    ClipboardCopyMode$ ExcludeHeader=�   SelectionMode$ Single=�   $ 
Акция�� �� *NAME+  $ Скидка�� �� *	PROCENT+�� -    ��Name$ totalPriceTextBlock��$
��Wrap=�$��1��$��1��$��0,2,0,0q���  � Language$ ru-ru�$�Общая цена : ���� -   $ tabItemDiscount��$��
Акции��$��
21,0,-21,0q�.��� ���	#FFE5E5E5.+�  -   $
 dataGridDiscountOut�� .   . $��0,0,0,-6q�   $ Код�� �� *ID+  $ 
Товар�� �� *NAME+  $ Скидка�� �� *PRICE+  $ Начало�� �� *BDATE+ �� StringFormat$ {0:dd.MM.yy}��  $ 
Конец�� �� *EDATE+$ {0:dd.MM.yy}��  $ Описание�� �� *TEXT+ �� .@�m� -   $��Изменить��m� -   $��Удалить��m� -   $��Добавить��m� -   $��Обновить���� -   $ tabItemEmployee��$��Работники��$��
20,0,-20,0q�.��� ���	#FFE5E5E5	 �� $
��286* ��� $��50 � �� .+�  -   $
 dataGridEmployeeOut�� .   . $��0,0,0,1q�   $ 	Ф.И.О.�� �� *NAME+  $ Телефон�� �� *TEL+  $ Должность�� �� *
POSITION+  $ № контракта�� �� *
CONTRACT+  $ ИНН�� �� *INN+  $ Код�� �� *ID+ �� .@�m� -   $��Изменить��m� -   $��Удалить��m� -   $��Добавить��m� -    $��Обновить���� -!   $ textBlockTypeCount��$
��Wrap=�$��1��$��27��$��Bottom=�$
��Left=�$	��153���� -"   $ tabItemManufacturer��$ ��Производители��$��
18,0,-18,0q�.��� ���	#FFE5E5E5.+�  -#   $
 dataGridManufacturersOut�� .   . $��0,0,0,-6q�   $ Название�� �� *NAME+  $ Страна�� �� *	COUNTRY+  $ 
Город�� �� *CITY+  $ 
Адрес�� �� *ADDR+  $ Телефон�� �� *TEL+  $ Код�� �� *ID+ �� .@�m� -$   $��Изменить��m� -%   $��Удалить��m� -&   $��Добавить��m� -'   $��Обновить���� -(   $ tabItemProduct��$��Товары��$��
16,0,-16,0q�.��� ���	#FFE5E5E5.+�� $��0,0,0,-6q�	 �� $
��286* ��� $��50 � �� $
��256* ��� $
��128* �.+�  -)   $
 dataGridProductOut�� .   .    $ Штрих-код�� �� *CODE+  $ Наименование�� �� *NAME+  $  Производитель�� �� *MANUFACTURER+  $ Группа�� �� *GROUP+  $ Упаковка�� �� *PACK+  $ Материал�� �� *
MATERIAL+  $ Отпуск�� �� *FORM+  $ Инструкция�� �� *INSTR+  $ Код�� �� *ID+ �� .@�m� -*   $��Изменить��m� -+   $��Удалить��m� -,   $��Добавить��m� --   $��Обновить��  -.   $ 
 dataGridProductActPriceOut�� .   . $��1��   $ Дата�� �� *DATE+$ {0:dd.MM.yy}��  $ Цена�� �� *PRICE+�� -/   $ textBlockProductCount��$
��Wrap=�$��1���� -0   $ buttonDropProduct��$��Утилизация��$��1��$
��Left=�$��1��$	��Top=�$	��257��$��50���� -1   $ tabItemWaybill��$��Накладные��$��
15,0,-15,0q�.��� ���	#FFE5E5E5.+�� $��0,0,0,-6q�	 �� $
��286* ��� $��35 � �� $
��215* ��� $
��171* �.+�  -2   $
 dataGridWaybillOut�� .   . $��2��   $ Дата�� �� *DATE+$ {0:dd.MM.yy}��  $ Принял�� �� *
EMPLOYEE+  $ Контрагент�� �� *AGENT+  $ Код�� �� *ID+ �� .@�m� -3   $��Изменить��m� -4   $��Удалить��m� -5   $��Добавить��m� -6   $��Обновить��  -7   $
 dataGridWaybillListOut�� .   . $��1��   $ 
Товар�� �� *	PRODUCT+  $ Количество�� �� *VALUE+  $ Опт.цена�� �� *
TRADEPRICE+  $% Дата поступления�� �� *BDATE+$ {0:dd.MM.yy}��  $ Годен до�� �� *EDATE+$ {0:dd.MM.yy}�� �� .R�  $#
 dataGridWaybillOverdueDisplay��-8    .  $
 Auto=� .   .   .  .   .  .   . $ ExcludeHeader=�$ Extended=�   $ Просрочено�� �� *	OVERDUE+  $ Не просрочено�� �� *
NOTOVERDUE+  $ Продано�� �� *SOLD+  $  Утилизировано�� �� *UTIL+�� -9   $  totalWaybillPriceTextBlock��$
��Wrap=�$��1��$��1��$��0,0,0,1q��� $ ru-ru�$�Общая цена : ���� -:   $ 
buttonEdit��$��Изменить��$��	0,0,244,9q�$��25��$��Bottom=�$��Right=�$��75��$��14(��� -;   $ tabControlSearch��$��	10,22,0,0q�$
��Left=�$	��587��$	��418��$	��Top=� �� SelectedIndex$ 0��.@��� -<   $  tabItemCheckSearchSettings��$��Чеки��$��14(�.��� ���	#FFE5E5E5$��0,0,0,-4q�.+��� -=    ��Name$! checkBoxSearchEmployeeCheck��$��Кассир��$��Right=�$��0,0,505,353q�$��Bottom=�$��22��$��14(��� ->     ��Name$   textBoxSearchEmployeeCheck��$��Right=�$��0,0,359,352q�$��Bottom=�$	��141��{�.  $��14(��� -?   $  textBoxSearchCheckCode��$��Right=�$��23��$��
0,0,336,59q�! �� TextWrapping$
! Wrap=�$��Bottom=�$	��164��{�.  $��14(��� -@   $ checkBoxSearchCheckCode��$��Код��$��Right=�$��
0,0,521,66q�$��Bottom=�$��14(��� $��Дата��$��Right=�$��0,0,543,316q�$��Bottom=�$��24��{�.  $��14(�(  "System.Windows.Controls.DatePicker  -A   "  Name$ " datePickerSearchBDateCheck��$��Right=�$��0,0,359,318q�$��Bottom=�$	��136��{�.  $��14(�%  Xceed.Wpf.Toolkit.IntegerUpDown  -B   #  Name$# upDownSearchBHours���  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$   	Increment$$ 1X��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]%   Maximum$% 24X�&   Minimum$& 0X�$��24��$��Bottom=�$��Right=�$��42��$��0,0,453,222q�{�.  $��14(�  -C   $# upDownSearchBSeconds��$$ 1X�$% 60X�$& 0X�$��24��$��Bottom=�$��Right=�$��42��$��0,0,359,222q�{�.  $��14(�  -D   $# upDownSearchBMinutes��$$ 1X�$% 60X�$& 0X�$��24��$��Bottom=�$��Right=�$��42��$��0,0,406,222q�{�.  $��14(��� $��Тип оплаты��$��Right=�$��0,0,495,129q�$��Bottom=�{�.  $��14(��� -E   $ checkBoxSearchPaytypeCash��$��Наличные��$��Right=�$��0,0,427,108q�$��Bottom=�$��14(��� -F   $ checkBoxSearchPaytypeCard��$��
Карта��$��Right=�$��
0,0,457,87q�$��Bottom=�$��14(��� -G   $ checkBoxSearchTime��$��
Время��$��Right=�$��0,0,532,216q�$��Bottom=�{�.  $��14(��� -H   $ checkBoxSearchBHours��$�� ��$��Right=�$��0,0,465,201q�$��Bottom=�$��14(��� -I   $ checkBoxSearchBMinutes��$�� ��$��Right=�$��0,0,420,201q�$��Bottom=�$��14(��� -J   $ checkBoxSearchBSeconds��$�� ��$��Right=�$��0,0,372,201q�$��Bottom=�$��14(��� -K   $ buttonResetSearchCheck��$%��Сбросить фильтры��$��	0,0,51,14q�$��Right=�$	��131��$��27��$��Bottom=�$��14(��� -L   $ checkBoxSearchBDateCheck��$
��от��$��Right=�$��0,0,502,322q�$��Bottom=�$��14(��� -M   $ checkBoxSearchEDateCheck��$
��до��$��Right=�$��0,0,500,260q�$��Bottom=�$��14(�  -N   $ " datePickerSearchEDateCheck��$��Right=�$��0,0,359,256q�$��Bottom=�$	��136��{�.  $��14(�  -O   $# upDownSearchEHours��$$ 1X�$% 24X�$& 0X�$��24��$��Bottom=�$��Right=�$��42��$��0,0,453,170q�{�.  $��14(�  -P   $# upDownSearchESeconds��$$ 1X�$% 60X�$& 0X�$��24��$��Bottom=�$��Right=�$��42��$��0,0,359,170q�{�.  $��14(�  -Q   $# upDownSearchEMinutes��$$ 1X�$% 60X�$& 0X�$��24��$��Bottom=�$��Right=�$��42��$��0,0,406,170q�{�.  $��14(��� -R   $ checkBoxSearchEHours��$�� ��$��Right=�$��0,0,465,149q�$��Bottom=�$��14(��� -S   $ checkBoxSearchEMinutes��$�� ��$��Right=�$��0,0,420,149q�$��Bottom=�$��14(��� -T   $ checkBoxSearchESeconds��$�� ��$��Right=�$��0,0,372,149q�$��Bottom=�$��14(��� $
��от��$��Right=�$��0,0,511,222q�$��Bottom=�{�.  $��14(��� $
��до��$��Right=�$��0,0,509,178q�$��Bottom=�{�.  $��14(��� -U   $  checkBoxSearchProductCheck��$��Название��$��Right=�$��0,0,206,316q�$��Bottom=�$��14(��� -V   $ checkBoxSearchPriceCheck��$��Цена��$��Right=�$��0,0,248,242q�$��Bottom=�$��14(��� -W   $ checkBoxSearchValueCheck��$��Количество��$��Right=�$��0,0,206,174q�$��Bottom=�$��14(��� -X   $  textBoxSearchProductCheck��$��Right=�$��23��$��
0,0,51,312q�$
! Wrap=�$��Bottom=�$	��150��{�.  $��14(�%	  Xceed.Wpf.Toolkit.DecimalUpDown	  -Y   ' 	 Name$' upDownSearchPriceCheck���
  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]( 
  FormatString$( C2��) 
  	Increment$	) .01X��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]*   Maximum$* 99999.99X�+   Minimum$
+ 0.01X�$��25��$��Bottom=�$��Right=�$	��150��$��
0,0,51,236q�{�.  $��14(�  -Z   $# upDownSearchValueCheck��$$ 1X�$% 99999X�$& 1X�$��24��$��Bottom=�$��Right=�$	��150��$��
0,0,51,170q�{�.  $��14(��� -[   , ��Name$", comboBoxDirectionSearchValue��$
��Left=�$��376,163,0,0q�$	��Top=�$	��150��$ 0��$��14(�.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -\   $", comboBoxDirectionSearchPrice��$
��Left=�$��
376,96,0,0q�$	��Top=�$	��150��$ 0��$��14(�.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -]   $&,  comboBoxSearchDateRangeTypeCheck��$
��Left=�$��	82,74,0,0q�$	��Top=�$	��136��$ 0��{�.  $��14(�.@���в диапазоне����не в диапазоне���� $��
Товар��$
��Left=�$��
241,15,0,0q�$	��Top=�$��14(��� -^   $" checkBoxSearchProductIdCheck��$��Код��$
��Left=�$��
293,15,0,0q�$	��Top=�$��14(��� -_   $!  textBoxSearchProductIdCheck��$
��Left=�$��23��$��
376,15,0,0q�$
! Wrap=�$	��Top=�$	��150��{�.  $��14(��� -`   $ checkBoxSearchSummCheck��$��
Сумма��$
��Left=�$��279,318,0,0q�$	��Top=��� -a   $# checkBoxSearchPrepaymentCheck��$��
Аванс��$
��Left=�$��279,260,0,0q�$	��Top=�	  -b   $!' upDownSearchPrepaymentCheck��$( C2��$	) .01X�$* 99999.99X�$
+ 0.01X�$��23��$��Bottom=�$��Right=�$	��150��$��
0,0,51,104q�{�.  $��14(��� -c   $,, &comboBoxDirectionSearchPrepaymentCheck��$
��Left=�$��376,230,0,0q�$	��Top=�$	��150��$ 0��$��14(�.@���
равно����"больше или равно����"меньше или равно����больше����меньше��	  -d   $' upDownSearchSummCheck��$( C2��$	) .01X�$* 99999.99X�$
+ 0.01X�$��23��$��Bottom=�$��Right=�$	��150��$��	0,0,51,46q�{�.  $��14(��� -e   $&,  comboBoxDirectionSearchSummCheck��$
��Left=�$��376,288,0,0q�$	��Top=�$	��150��$ 0��$��14(�.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -f   $# tabItemDiscountSearchSettings��$��
Акции��$��14(�.��� ���	#FFE5E5E5$��0,0,0,-4q�.+��� -g   $# checkBoxSearchProductDiscount��$��Название��$
��Left=�$��
282,12,0,0q�$	��Top=��� $��
Товар��$
��Left=�$��
239,13,0,0q�$	��Top=�  -h   $# upDownSearchProcDiscount��$$ 1X�$	% 100X�$& 0X�$
��Left=�$	��152��$��20��$	��Top=�$��
368,78,0,0q�{�.  �� -i   $"  textBoxSearchProductDiscount��$
��Left=�$��23��$��	368,9,0,0q�$
! Wrap=�$	��Top=�$	��152��{�.  �� -j   $  checkBoxSearchProcDiscount��$��Скидка��$
��Left=�$��
282,78,0,0q�$	��Top=�  -k   $#" datePickerSearchBDateDiscount��$
��Left=�$��368,116,0,0q�$	��Top=�$	��152��{�.    -l   $#" datePickerSearchEDateDiscount��$
��Left=�$��368,179,0,0q�$	��Top=�$	��152��{�.  �� -m   $! checkBoxSearchBDateDiscount��$
��От��$
��Left=�$��282,119,0,0q�$	��Top=�$��24���� -n   $! checkBoxSearchEDateDiscount��$
��До��$
��Left=�$��282,175,0,0q�$	��Top=�$��24��$��58���� -o   $  checkBoxSearchCodeDiscount��$��Код��$
��Left=�$��282,218,0,0q�$	��Top=��� -p   $  textBoxSearchCodeDiscount��$
��Left=�$��23��$��368,218,0,0q�$
! Wrap=�$	��Top=�$	��152��{�.  �� $
��Left=�$��
525,78,0,0q�$
��Wrap=�$	��Top=�$��20��{�.  �� $ ru-ru�$�%��- �� 	IsEnabled- .  �� -q   $ buttonResetSearchDiscount��$%��Сбросить фильтры��$
��Left=�$��384,256,0,0q�$	��Top=�$	��136���� -r   $% checkBoxSearchProductIdDiscount��$��Код��$
��Left=�$��
282,43,0,0q�$	��Top=��� -s   $$  textBoxSearchProductIdDiscount��$
��Left=�$��23��$��
368,43,0,0q�$
! Wrap=�$	��Top=�$	��152��{�.  �� -t   $!, comboBoxSearchDateRangeType��$
��Left=�$��331,149,0,0q�$	��Top=�$	��189��$ 0��{�.  .@���
равно����в диапазоне����не в диапазоне����частично����.*частично или полностью���� $��Работники��$��-2,0,2,0q�$��14(�.��� ���	#FFE5E5E5.+��� -u   $  checkBoxSearchNameEmployee��$��	Ф.И.О.��$
��Left=�$��
303,15,0,0q�$	��Top=��� -v   $$ checkBoxSearchContractEmployee��$��№ контракта��$
��Left=�$��303,108,0,0q�$	��Top=��� -w   $ checkBoxSearchPosEmployee��$��Должность��$
��Left=�$��
303,59,0,0q�$	��Top=��� -x   $ checkBoxSearchTelEmployee��$��Телефон��$
��Left=�$��303,155,0,0q�$	��Top=��� -y   $  checkBoxSearchCodeEmployee��$��Код��$
��Left=�$��303,201,0,0q�$	��Top=��� -z   $  textBoxSearchNameEmployee��$
��Left=�$��23��$��
410,12,0,0q�$
! Wrap=�$	��Top=�$	��136��{�.  �� -{   $#  textBoxSearchContractEmployee��$
��Left=�$��23��$��410,105,0,0q�$
! Wrap=�$	��Top=�$	��136��{�.  �� -|   $, comboBoxSearchPosEmployee��$
��Left=�$��
410,59,0,0q�$	��Top=�$	��136��{�.  .@���Кассир����Уборщик����Менеджер����Администратор����Фармацевт���� -}   $  textBoxSearchTelEmployee��$
��Left=�$��23��$��410,152,0,0q�$
! Wrap=�$	��Top=�$	��136��{�.  �� -~   $  textBoxSearchCodeEmployee��$
��Left=�$��23��$��410,198,0,0q�$
! Wrap=�$	��Top=�$	��136��{�.  �� -   $ buttonResetSearchEmployee��$%��Сбросить фильтры��$
��Left=�$��410,284,0,0q�$	��Top=�$	��136���� -�   $ checkBoxSearchINNEmployee��$��ИНН��$
��Left=�$��303,245,0,0q�$	��Top=��� -�   $  textBoxSearchINNEmployee��$
��Left=�$��23��$��410,245,0,0q�$
! Wrap=�$	��Top=�$	��136��{�.  �� $ ��Производители��$��-2,0,2,0q�$��14(�.��� ���	#FFE5E5E5.+��� -�   $#  textBoxSearchNameManufacturer��$
��Left=�$��23��$��
409,10,0,0q�$
! Wrap=�$	��Top=�$	��144��{�.  �� -�   $&   textBoxSearchCountryManufacturer��$
��Left=�$��23��$��
409,57,0,0q�$
! Wrap=�$	��Top=�$	��144��{�.  �� -�   $#  textBoxSearchCityManufacturer��$
��Left=�$��23��$��409,103,0,0q�$
! Wrap=�$	��Top=�$	��144��{�.  �� -�   $&   textBoxSearchAddressManufacturer��$
��Left=�$��23��$��409,147,0,0q�$
! Wrap=�$	��Top=�$	��144��{�.  �� -�   $"  textBoxSearchTelManufacturer��$
��Left=�$��23��$��409,189,0,0q�$
! Wrap=�$	��Top=�$	��144��{�.  �� -�   $#  textBoxSearchCodeManufacturer��$
��Left=�$��23��$��409,228,0,0q�$
! Wrap=�$	��Top=�$	��144��{�.  �� -�   $$ checkBoxSearchNameManufacturer��$��Название��$
��Left=�$��
322,13,0,0q�$	��Top=��� -�   $' !checkBoxSearchCountryManufacturer��$��Страна��$
��Left=�$��
322,60,0,0q�$	��Top=��� -�   $$ checkBoxSearchCityManufacturer��$��
Город��$
��Left=�$��322,103,0,0q�$	��Top=��� -�   $# checkBoxSearchTelManufacturer��$��Телефон��$
��Left=�$��322,192,0,0q�$	��Top=��� -�   $' !checkBoxSearchAddressManufacturer��$��
Адрес��$
��Left=�$��322,150,0,0q�$	��Top=��� -�   $$ checkBoxSearchCodeManufacturer��$��Код��$
��Left=�$��322,231,0,0q�$	��Top=��� -�   $# buttonResetSearchManufacturer��$%��Сбросить фильтры��$
��Left=�$��409,266,0,0q�$	��Top=�$	��144���� $��Товары��$��-2,0,2,0q�$��14(�.��� ���	#FFE5E5E5.+��� -�   $  textBoxSearchNameProduct��$
��Left=�$��23��$��	129,7,0,0q�$
! Wrap=�$	��Top=�$	��153��{�.  �� -�   $&   textBoxSearchManufacturerProduct��$
��Left=�$��23��$��
129,51,0,0q�$
! Wrap=�$	��Top=�$	��153��{�.  �� -�   $ , comboBoxSearchGroupProduct��$
��Left=�$��
129,94,0,0q�$	��Top=�$	��153��{�.  .@���Без группы����Антисептики����Слабительные����"Противовирусные����$ Антибакериальные����0,Отхаркивающие и рвотные����"Антидепрессанты����Снотворные����Анальгетики����$ Противогрибковые����&"Противопаразитные����.*Противовоспалительные����&"Противозачаточные����*&Противоалергические����0,Гастроэнтероголические���� Противорвотные����$ Противодиарейные����Ингибиторы����$ Бронхолитические���� Муколитические����"Противокашлевые����$ Иммуномодуляторы����"Иммуноглобулины����Витаминные����Минеральные���� Поливитаминные����%!Питательные смеси���� Жаропонижающие����"Антианемические����Фитопрепараты����40Средства для контрацепции����$ Сосудосуживающие���� Антиглаукомные����($Офтальмологические����$ Проктологические����Урологические����"Нефрологические����&"Дерматологические����"Диагностические����Перевязочные����&"Токсикологические����Антидоты����"Гомеопатические����Эфирные масла����0,Средства личной гигиены����Косметические���� -�   $, comboBoxSearchPackProduct��$
��Left=�$��	397,5,0,0q�$	��Top=�$	��158��{�.  .@���Без упаковки����Ампула����%!Картонная коробка����
Банка����
Пакет����Полимерная����Баллон����Туба����Флакон����Бумага���� -�   $#, comboBoxSearchMaterialProduct��$
��Left=�$��
397,45,0,0q�$	��Top=�$	��158��{�.  .@���Неизвестно����Жидкость����
Сироп����Таблетки����Капсулы����Шприц-тюбики����Порошок����Гранулы����+'Растительные волокна����Аэрозоль����Суспензия����Мазь����
Паста���� -�   $, comboBoxSearchFormProduct��$
��Left=�$��
397,87,0,0q�$	��Top=�$	��158��{�.  .@���с рецептом����без рецепта����строгого учета��	  -�   $' upDownSearchPriceProduct��$( C2��$	) .01X�$* 99999.99X�$
+ 0.01X�$��23��$��Bottom=�$��Right=�$	��153��$��0,0,295,183q�{�.  �� -�   $ checkBoxSearchNameProduct��$��Название��$
��Left=�$��7,14,0,0q�$	��Top=��� -�   $' !checkBoxSearchManufacturerProduct��$ ��Производитель��$
��Left=�$��7,51,0,0q�$	��Top=��� -�   $  checkBoxSearchGroupProduct��$��Группа��$
��Left=�$��7,94,0,0q�$	��Top=��� -�   $ checkBoxSearchPackProduct��$��Упаковка��$
��Left=�$��	305,5,0,0q�$	��Top=��� -�   $# checkBoxSearchMaterialProduct��$��Материал��$
��Left=�$��
305,45,0,0q�$	��Top=��� -�   $ checkBoxSearchFormProduct��$��Отпуск��$
��Left=�$��
305,87,0,0q�$	��Top=��� -�   $  checkBoxSearchPriceProduct��$��Цена��$
��Left=�$��
10,177,0,0q�$	��Top=��� -�   $ buttonResetSearchProduct��$%��Сбросить фильтры��$
��Left=�$��420,252,0,0q�$	��Top=�$	��135���� -�   $%, comboBoxSearchPriceRangeProduct��$
��Left=�$��129,138,0,0q�$	��Top=�$	��153��$ 0��.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -�   $ checkBoxSearchCodeProduct��$��Код��$
��Left=�$��
10,215,0,0q�$	��Top=��� -�   $  textBoxSearchCodeProduct��$
��Left=�$��23��$��129,215,0,0q�$
! Wrap=�$	��Top=�$	��153��{�.  �� -�   $  checkBoxSearchValueProduct��$��Количество��$��Right=�$��0,0,195,183q�$��Bottom=�  -�   $# upDownSearchValueProduct��$$ 1X�$% 99999X�$& 1X�$��23��$��Bottom=�$��Right=�$	��158��$��
0,0,22,179q�{�.  �� -�   $), #comboBoxDirectionSearchValueProduct��$
��Left=�$��397,151,0,0q�$	��Top=�$	��158��$ 0��.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -�   $$, comboBoxSearchTypeCountProduct��$
��Left=�$��397,120,0,0q�$	��Top=�$	��158��$ 0��.@���
всего����просрочено����не просрочено���� -�   $ checkBoxSearchBarcode��$��Штрих-код��$
��Left=�$��290,215,0,0q�$	��Top=��� -�   $  textBoxSearchBarcode��$
��Left=�$��23��$��397,215,0,0q�$
! Wrap=�$�� ��$	��Top=�$	��158��{�.  �� -�   $!, comboBoxSearchProductStatus��$
��Left=�$��132,253,0,0q�$	��Top=�$	��153��{�.  .@���
любое����не просрочено����истекает с.г.����просрочено����продано���� -�   $! checkBoxSearchProductStatus��$��Состояние��$
��Left=�$��
10,255,0,0q�$	��Top=��� $��Накладные��$��-5,0,6,0q�$��14(�.��� ���	#FFE5E5E5.+��� -�   $"  textBoxSearchEmployeeWaybill��$
��Left=�$��23��$��
131,97,0,0q�$
! Wrap=�$	��Top=�$	��147��{�.  �� -�   $  textBoxSearchAgentWaybill��$
��Left=�$��23��$��131,125,0,0q�$
! Wrap=�$	��Top=�$	��147��{�.  �� -�   $  textBoxSearchCodeWaybill��$
��Left=�$��23��$��131,216,0,0q�$
! Wrap=�$	��Top=�$	��147��{�.  �� -�   $# checkBoxSearchEmployeeWaybill��$��Принимающий��$
��Left=�$��	10,99,0,0q�$	��Top=��� -�   $  checkBoxSearchAgentWaybill��$��Контрагент��$
��Left=�$��
10,126,0,0q�$	��Top=��� -�   $ checkBoxSearchCodeWaybill��$��Код��$
��Left=�$��
10,220,0,0q�$	��Top=��� -�   $ buttonResetSearchWaybill��$%��Сбросить фильтры��$
��Left=�$��418,330,0,0q�$	��Top=�$	��135��  -�   $," &datePickerSearchBDateProductWaybillOut��$
��Left=�$��409,209,0,0q�$	��Top=�$	��146��{�.  .   DisplayDate$. 
2016-04-27��  -�   $," &datePickerSearchEDateProductWaybillOut��$
��Left=�$��409,268,0,0q�$	��Top=�$	��144��{�.  �� -�   $* $checkBoxSearchBDateProductWaybillOut��$
��от��$
��Left=�$��371,213,0,0q�$	��Top=�$��24���� -�   $* $checkBoxSearchEDateProductWaybillOut��$
��до��$
��Left=�$��371,271,0,0q�$	��Top=�$��24��$��70���� -�   $2, ,comboBoxSearchDateRangeTypeProductWaybillOut��$
��Left=�$��409,241,0,0q�$	��Top=�$	��144��$ 0��{�.  .@���в диапазоне����не в диапазоне���� -�   $' !checkBoxSearchPriceProductWaybill��$��Опт.цена��$��Right=�$��0,0,200,191q�$��Bottom=�$��67��	  -�   $%' upDownSearchPriceProductWaybill��$( C2��$	) .01X�$* 99999.99X�$
+ 0.01X�$��23��$��Bottom=�$��Right=�$	��146��$��
0,0,22,187q�{�.  �� -�   $0, *comboBoxDirectionSearchPriceProductWaybill��$
��Left=�$��409,143,0,0q�$	��Top=�$	��146��$ 0��.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -�   $' !checkBoxSearchValueProductWaybill��$��Количество��$��Right=�$��0,0,175,245q�$��Bottom=�  -�   $%# upDownSearchValueProductWaybill��$$ 1X�$% 99999X�$& 1X�$��19��$��Bottom=�$��Right=�$	��146��$��
0,0,22,245q�{�.  �� -�   $0, *comboBoxDirectionSearchValueProductWaybill��$
��Left=�$��
409,89,0,0q�$	��Top=�$	��146��$ 2��.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -�   $" checkBoxSearchProductWaybill��$��Название��$
��Left=�$��	342,5,0,0q�$	��Top=��� $��
Товар��$
��Left=�$��
299,20,0,0q�$	��Top=�{�.  �� -�   $!  textBoxSearchProductWaybill��$
��Left=�$��23��$��	425,4,0,0q�$
! Wrap=�$	��Top=�$	��130��{�.  �� -�   $$ checkBoxSearchProductIdWaybill��$��Код��$
��Left=�$��
342,36,0,0q�$	��Top=��� -�   $#  textBoxSearchProductIdWaybill��$
��Left=�$��23��$��
425,32,0,0q�$
! Wrap=�$	��Top=�$	��130��{�.    -�   $"" datePickerSearchBDateWaybill��$
��Left=�$��	131,4,0,0q�$	��Top=�$	��147��{�.    -�   $"" datePickerSearchEDateWaybill��$
��Left=�$��
131,65,0,0q�$	��Top=�$	��147��{�.  �� -�   $  checkBoxSearchBDateWaybill��$
��От��$
��Left=�$��68,8,0,0q�$	��Top=�$��24���� -�   $  checkBoxSearchEDateWaybill��$
��До��$
��Left=�$��	68,69,0,0q�$	��Top=�$��24��$��58���� -�   $(, "comboBoxSearchDateRangeTypeWaybill��$
��Left=�$��
131,35,0,0q�$	��Top=�$	��147��$ 0��{�.  .@���в диапазоне����не в диапазоне���� $
��Left=�$��	-3,29,0,0q�$
��Wrap=�$	��Top=�{�.  �� $ ru-ru�$�       Дата���� �� $ ru-ru�$� составления���� -�   $+, %comboBoxSearchTypeCountProductWaybill��$
��Left=�$��
436,60,0,0q�$	��Top=�$	��119��$ 0��.@���всего принято����просрочено����не просрочено����продано���� -�   $  checkBoxSearchPriceWaybill��$��Цена��$��Right=�$��0,0,517,179q�$��Bottom=�	  -�   $' upDownSearchPriceWaybill��$( C2��$	) .01X�$* 99999.99X�$
+ 0.01X�$��23��$��Bottom=�$��Right=�$	��147��$��0,0,299,175q�{�.  �� -�   $), #comboBoxDirectionSearchPriceWaybill��$
��Left=�$��131,155,0,0q�$	��Top=�$	��147��$ 0��.@���
равно����"больше или равно����"меньше или равно����больше����меньше��  -�   $+" %datePickerSearchBDateProductWaybillIn��$
��Left=�$��131,253,0,0q�$	��Top=�$	��147��{�.    -�   $+" %datePickerSearchEDateProductWaybillIn��$
��Left=�$��131,315,0,0q�$	��Top=�$	��147��{�.  �� -�   $) #checkBoxSearchBDateProductWaybillIn��$
��от��$
��Left=�$��
95,256,0,0q�$	��Top=�$��24���� -�   $) #checkBoxSearchEDateProductWaybillIn��$
��до��$
��Left=�$��
95,319,0,0q�$	��Top=�$��24��$��70���� -�   $1, +comboBoxSearchDateRangeTypeProductWaybillIn��$
��Left=�$��131,285,0,0q�$	��Top=�$	��147��$ 0��{�.  .@���в диапазоне����не в диапазоне���� $
��Left=�$��
10,280,0,0q�$
��Wrap=�$	��Top=�{�.  �� $ ru-ru�$�       ���� $�Дата���� �� $� поступления���� $
��Left=�$��310,244,0,0q�$
��Wrap=�$��Годен до��$	��Top=�{�.  �� -�   $(, "comboBoxSearchProductStatusWaybill��$
��Left=�$��402,300,0,0q�$	��Top=�$	��153��{�.  .@���
любое����не просрочено����истекает с.г.����просрочено����продано���� -�   $( "checkBoxSearchProductStatusWaybill��$��Состояние��$
��Left=�$��299,306,0,0q�$	��Top=��� -�   $ buttonResetSearch��$��Общий сброс��$
��Left=�$��487,450,0,0q�$	��Top=�$	��110��$��14(�