   M S B A M L   `   `   `  ����   Pharmacy DataBase(    "WpfApplication1.WaybillPrintWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Печать��$��1009.429��$
��20cm�� � 
ResizeMode$ NoResize=�$��14(� � WindowStartupLocation$ CenterScreen=� � Icon$, &/Pharmacy DataBase;component/icone.ico��.��� .+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridWaybillPrint��$
��Left=�$��
0,110,0,85q�$	��748��   
IsReadOnly .     AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Товар��1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *	PRODUCT+  $ Количество��	 �� *VALUE+  $ Упаковка��	 �� *PACK+  $ Материал��	 �� *
MATERIAL+  $ Опт.цена��	 �� *
TRADEPRICE+  $ ПДВ��	 �� *PDV+  $ 
Сумма��	 �� *SUMM+�� -   
 ��Name$
 	LabelText��$�� ��$
��Left=�$��	10,10,0,0q�$	��Top=��� -   $
 	LabelSumm��$��Общая сумма:��$
��Left=�$��
516,0,0,51q�$��29��$��Bottom=��� -   $
 
LabelAgent��$"��Товар отпустил:��$
��Left=�$��	10,0,0,43q�$��29��$��Bottom=��� -   $
 LabelEmployee��$��Товар принял:��$
��Left=�$��	10,0,0,17q�$��29��$��Bottom=�