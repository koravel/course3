   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Pharmacy DataBase"   WpfApplication1.SellerWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  3System&clr-namespace:System;assembly=mscorlib  -     � Title$  Кассир��$	��426��$
��1082��$��565.5��$��1368.5�� � Icon$, &/Pharmacy DataBase;component/icone.ico��.���  � RowDefinitions �� $	��47* ��� $	��14* ��� $	��21* ��� $	��53* ��� $	��44* � � ColumnDefinitions �� $	��58* ��� $
��641* ��� $	��69* ��� $
��593* �����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFCBCBCB�� � Offset$ 0.346Q�� $ White��.+�	� -    	�Name$ radioButtonCash��$��наличные(F11)��$��0,4,11,0q�	 	� 	GroupName$	 Paytype_Group��
 d� 	IsChecked$

 TrueY�$��14(�$��Right=�$	��110��$��19��$	��Top=�$��3��$��4��	� -   $ radioButtonCard��$��карта(F12)��$��Right=�$��	0,26,39,0q�$	��Top=�$	 Paytype_Group��$��14(�$��3��$��4��&   System.Windows.Controls.DataGrid  -     Name$ dataGridProductOut��   AutoGenerateColumns .     
IsReadOnly . $��5,40,5,2q�$��14(�$��4��$��2��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Штрих-код��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CODE+  $ Наименование�� �� *NAME+  $ Цена�� �� *PRICE+  $ Опт.цена�� �� *
TRADEPRICE+  $ Количество�� �� *VALUE+  $ Скидка�� �� *
DISCOUNT+  $  Производитель�� �� *MANUFACTURER+  $ Группа�� �� *GROUP+  $ Упаковка�� �� *PACK+  $ Материал�� �� *
MATERIAL+  $ Отпуск�� �� *FORM+�� -    ��Name$ buttonSellToCheck��$��Продать(F3)��$��Right=�$��
0,0,112,13q�$��Bottom=�$��90��$��14(�$��3��$��4���� -   $ buttonCancel��$��Отмена(F4)��$��Right=�$��	0,0,11,13q�$��Bottom=�$��83��$��14(�$��3��$��4���� -   $ 
buttonBack��$��
Выйти��$
��Left=�$��	10,10,0,0q�$	��Top=�$��75��$��14(�$��2���� -    ��Name$ textBoxSearch��$��25��$��6.5,7,270,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��14(�$��4��$��1���� -	   $ 
buttonSell��$��>>��$��0,1,1,0q�$��36(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�$��2��$��60��$	��Top=�$��2��  -
   $ dataGridProductToCheckOut�� .   . $��	4,40,10,2q�$��14(�$��3��$��4��   $ Штрих-код�� �� *CODE+  $ Наименование�� �� *NAME+  $ Цена�� �� *
CURPRICE+  $ Количество�� �� *VALUE+  $ 
Сумма�� �� *SUMM+  $ Скидка�� �� *
DISCOUNT+ � ContextMenu �� .@�m� -   $��удалить���� -    ��Name$ comboBoxSearch��$��Right=�$��0,7,90,0q�$	��Top=�$	��150��$��14(� �� SelectedIndex$ 0��$��4��$��1��.@���штрих-код����название����цена����опт.цена����количество����скидка����производитель����группа����упаковка����материал����форма отпуска��%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ upDownValue���  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$  ���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 1X�   	Increment$ 1X�   Maximum$ 99999X�   Minimum$ 1X�$��0,0,1,1q�$��14(�$��2��$��38��$��Bottom=�$��1���� -   $ buttonPrepayment��$!��Внести аванс(F2)��$��Right=�$��
0,0,220,14q�$��Bottom=�$	��116��$��14(�$��24��$��3��$��4��� $
��Left=�$��8,7,0,0q�$	��Top=�$��67��$	��449��$��3��$��4������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFB9B9B9��$ 0Q�� $ 	#FFEAEAEA��$
 0.38Q�.+��� -    ��Name$ textBlockPrePayment��$
��Wrap=�$	��Top=�$��34��$��20(�$��
10,0,300,0q���  � Language$ ru-ru�$�аванс:0���� -   $ textBlockSumm��$
��Wrap=�$	��Top=�$��34��$��20(�$��149,0,152,0q��� $ ru-ru�$�сумма:0���� -   $ textBlockDelivery��$
��Wrap=�$	��Top=�$��34��$��20(�$��	302,0,0,0q��� $ ru-ru�$�сдача:0���� -   $" comboBoxDirectionSearchValue��$��Right=�$��	0,49,90,0q�$	��Top=�$	��150��$ 0��$��14(�$��4��$��1��.@���
равно����"больше или равно����"меньше или равно����больше����меньше���� -   $ buttonSearchEscape��$��X��$��Right=�$��0,7,41,0q�$	��Top=�$��44��$��25��$��14(�$
��Bold"�$��4��$��1���� $��5,7,0,0q�$��4��$7��1/Pharmacy DataBase;component/Resources/search.png��$
��Fill=�$
��Left=�$��53��$��52��$	��Top=��� -   $ comboBoxEmployeeName��$��3��$
��Left=�$��4,13,0,0q�$	��Top=�$	��216��  J� RenderTransformOrigin$  0.349,0.624&� �� .@�m� -   ! m�Name$! menuItemEmployeeUpdate��$��обновить��