   M S B A M L   `   `   `  ����   Pharmacy DataBase(    "WpfApplication1.UsersControlWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$7  1Управление пользователями��$��199.5��$	��660�� � WindowStartupLocation$ CenterScreen=� � Icon$, &/Pharmacy DataBase;component/icone.ico��.���  � ColumnDefinitions �� $
��173* ��� $
��479* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridUserOut��   GridLinesVisibility$ 
Horizontal=�   
IsReadOnly .    AutoGenerateColumns .  $��1,0,7,9q�$��1��$��14(�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Тип записи��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *TYPE+  $	 
Логин��
 �� *LOGIN+  $	 Пароль��
 �� *PASS+ � ContextMenu �� .@�m� -   $��Изменить��m� -    m�Name$ menuItemDel��$��Удалить��m� -   $��Добавить��m� -   $��Обновить���� -    ��Name$ buttonUserAdd��$#��Добавить запись��$��	10,38,4,0q�$��26��$	��Top=�$��14(��� -   $ buttonUserDelete��$!��Удалить запись��$��10,8,4,0q�$��26��$	��Top=�$��14(��� -   $ buttonUpdate��$��Обновить��$��	10,98,4,0q�$��26��$	��Top=�$��14(��� -	   $ 
buttonBack��$��
Назад��$��
10,128,4,0q�$��26��$	��Top=�$��14(� �� IsCancel . �� -
   $ buttonUserEdit��$#��Изменить запись��$��	10,68,4,0q�$��26��$	��Top=�$��14(�