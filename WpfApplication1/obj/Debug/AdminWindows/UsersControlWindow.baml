   M S B A M L   `   `   `  ����  WpfApplication1(    "WpfApplication1.UsersControlWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  UsersControlWindow��5   	   $��179.5��6$   $	��660��63   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��173* �6   �� 5      $
��479* �6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridUserOut��5
         GridLinesVisibility$ 
Horizontal=�6,      
IsReadOnly .    AutoGenerateColumns .  $��1,0,7,9q�6{   $��1��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Тип записи��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *TYPE+  5      $ 
Логин��6%    64   �� *LOGIN+  5      $ Пароль��6%    65   �� *PASS+  5      $ Статус��6%    65   �� *ONLINE+5      �� 5   
   -   5        	 ��Name$	 buttonUserAdd��5      $#��Добавить запись��6(   $��	10,32,4,0q�6X   $��22��6k   $	��Top=�6w   �� 5   
   -   5        $	 buttonUserDelete��5      $!��Удалить запись��6+   $��10,5,4,0q�6D   $��22��6o   $	��Top=�6{   �� 5   
   -   5        $	 buttonUpdate��5      $��Обновить��6'   $��	10,86,4,0q�6:   $��22��6h   $	��Top=�6t   �� 5   
   -   5        $	 
buttonBack��5      $��
Назад��6%   $��
10,113,4,0q�65   $��22��6b   $	��Top=�6n   �� 5   
   -   5        $	 buttonUserEdit��5      $#��Изменить запись��6)   $��	10,59,4,0q�6C   $��22��6s   $	��Top=�6   5      5      