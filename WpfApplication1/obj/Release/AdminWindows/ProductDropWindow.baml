   M S B A M L   `   `   `  ����   Pharmacy DataBase'    !WpfApplication1.ProductDropWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Утилизация��$	��600��$	��600��$��14(�.���  � ColumnDefinitions �� $
��173* ��� $
��419* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridProductDrop��$��2,0,0,0q�$��1��   
IsReadOnly .    AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Накладная №��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+  $ 
Товар�� �� *	PRODUCT+  $ Количество�� �� *VALUE+�� -    ��Name$ 
buttonDrop��$��Утилзировать��$
��Left=�$��	26,28,0,0q�$	��Top=�$	��122���� -   $ 
buttonBack��$��
Назад��$
��Left=�$��	26,70,0,0q�$	��Top=�$	��122��