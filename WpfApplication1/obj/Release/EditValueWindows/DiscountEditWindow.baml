   M S B A M L   `   `   `  ����   Pharmacy DataBase(    "WpfApplication1.DiscountEditWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$-  'Редактирование акции��$	��300��$	��300�� � WindowStartupLocation$ CenterScreen=� � Icon$, &/Pharmacy DataBase;component/icone.ico��.���  � ColumnDefinitions �� $	��79* ��� $
��169* �.+��� -    ��Name$ comboBoxProduct��$��
75,12,24,0q�$��25��$	��Top=�$��2��$��14(� � ContextMenu �� .@�m� -   $$��Добавить новый...���� -    ��Name$ textBoxDescription��$��75,131,24,37q�$�� �� �� TextWrapping$
 Wrap=�$��2��$��14(�(  "System.Windows.Controls.DatePicker  -     Name$ datePickerBeginDate��$��
75,72,24,0q�$��25��$	��Top=�$��2��$��14(�  -   $ datePickerEndDate��$��75,101,24,0q�$��25��$	��Top=�$��2��$��14(��� $��
Товар��$��
10,10,13,0q�$	��Top=�$��26��$��14(��� $��Скидка��$��	10,39,1,0q�$	��Top=�$��26��$��14(��� $��Начало��$��10,69,168,0q�$	��Top=�$��26��$��2��$��14(��� $��
Конец��$��
10,100,8,0q�$	��Top=�$��26��$��14(��� $��Описание��$��0,130,167,0q�$	��Top=�$��26��$��2��$��14(��� -   	 ��Name$	 
buttonSave��$��Сохранить��$��	0,0,168,7q�$��25��$��Bottom=�$��Right=�$��92��$��14(�$��2��
 �� 	IsDefault
 . �� -   $	 
buttonBack��$��
Назад��$��0,0,10,7q�$��1��$��25��$��Bottom=�$��Right=�$��87��$��14(� �� IsCancel . �� $	��Top=�$��%��$
��Wrap=�$��
180,42,0,0q�$��25��$
��Left=�$��1��$��14(�%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ upDownPrice���  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$  ���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 0X�   	Increment$ 1X�   Maximum$	 100X�   Minimum$ 0X�$
��Left=�$	��193��$��25��$	��Top=�$��	75,42,0,0q�$��2��$��14(�