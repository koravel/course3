   M S B A M L   `   `   `  ����   Pharmacy DataBase'    !WpfApplication1.DiscountAddWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$%  Добавление акции��$	��300��$	��300�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$, &/Pharmacy DataBase;component/icone.ico�� � 	Resources  
  Color1��%   &	        ��	#FF617584��.���  � ColumnDefinitions �� $	��79* ��� $
��169* �.+��� -    ��Name$ comboBoxProduct��$��
75,12,24,0q�$��22��$	��Top=�$��2��$��14(� � ContextMenu �� .@�m� -   $$��Добавить новый...���� -    ��Name$ textBoxDescription��$��75,131,24,37q�$�� ��	 �� TextWrapping$
	 Wrap=�$��2��$��14(�(  "System.Windows.Controls.DatePicker  -   
  Name$
 datePickerBeginDate��$��
75,72,24,0q�$��24��$	��Top=�$��2��$��14(�  -   $
 datePickerEndDate��$��74,101,25,0q�$��24��$	��Top=�$��2��$��14(��� $��
Товар��$��
10,10,13,0q�$	��Top=�$��32��$��14(��� $��Скидка��$��	10,39,1,0q�$	��Top=�$��26��$��14(��� $��Начало��$��10,69,168,0q�$	��Top=�$��26��$��2��$��14(��� $��
Конец��$��
10,100,8,0q�$	��Top=�$��26��$��14(��� $��Описание��$��0,130,167,0q�$	��Top=�$��26��$��2��$��14(��� -    ��Name$ 
buttonSave��$��Сохранить��$��
0,0,139,10q�$��25��$��Bottom=�$��Right=�$��80��$��2��$��14(� �� 	IsDefault . �� -   $ 
buttonBack��$��
Назад��$��	0,0,26,10q�$��1��$��25��$��Bottom=�$��Right=�$��76��$��14(� �� IsCancel . �� $��1��$
��Left=�$��
179,42,0,0q�$
��Wrap=�$��%��$	��Top=�$��22��$��14(�%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ upDownPrice���  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$  ���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 0X�   	Increment$ 1X�   Maximum$	 100X�   Minimum$ 0X�$
��Left=�$	��193��$��23��$	��Top=�$��	75,42,0,0q�$��2��$��14(�