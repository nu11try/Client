   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DashBoardClient.PropertiesDashBoardClient.Properties  5      1clr-namespace:DashBoardClientDashBoardClient  5          DashBoardClient.AddTests   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6p(clr-namespace:DashBoardClient.Properties     5      /localclr-namespace:DashBoardClient     5        N� Title$  AddTests��5       � 	Resources 5         
Background �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� ScaleX	 �� ScaleY
 �� 
TargetType ��Name��%'  &	        '�� !     ��5   
   	#FF45454F��6.   �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
 0.85Q�6-   $
	 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      "
 ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5!      �� 5"       � ��# � ��6+   5#      5$      .��5&      � .+�5'   
   �� -   5        $%��Добавление теста��5'      $��30��6,   $��315,10,325,0q�68   $	��Top=�6N   $��Center=�6f   $	��150��6�   ���	#FF2BAAC9
�������
�������6�   �� 5(   
   -   5         ��Name$ DocListView��5(   4   $��0,55,5,0q�6I   $��11(�6[   ���	#FF45454F J� 	AllowDrop . ��6   �� *	DocList+��5)      �� 5*       �� Color$ #45454f��6"   ��5,      �� 5-      " ��6   .�5.      ��  � �� 5/      �� 50      "
 ��6.   .R�51   "   �� $��Border��6)   
������ ���#E9E5E1$��0q�6f   $��1,0,1,1q�6q   ._�52   &   �� 53   #    �� Triggers 54   "   P� 55   &    �@J�IsMouseOver . . �56   *   ��  �� 
TargetName$ Border��6G    � �� �#5555616n   57   '   P� 58   &    �@��
IsSelected . . �59   *   �� $ Border��6G    � �� �#35353d6m   5:   '   5<      5>      5?       �� View 5A      �� 5B      .P�5C      ��  �� Width$
 auto��6%   $��
Тесты��62    �� CellTemplate 5D      �� 5E      .R�5F   "   ��  ��Name$ kpBtn��6)   -   5        $��30��5F   K   $��Center=�6�   $��30��6�   
 � Tag 68   �� *ID+��6W   Z� ��6n   Z� ��5G   &   �� 5H   *    �� ImageSource$0 */DashBoardClient;component/Images/open.png��65    j� Stretch$ Uniform=�6n   5J   #   5K      5M      �� 5N      $5��/Программа и методика (ПиМ)��6%    6I   �� )  #DashBoardClient.Properties.Settings   Default �� Source# Z  �� Path$ width� �� Mode$ Default=� 5O      �� 5P      .R�5Q   "   ��  ��Name$	 pim��6,   
�������$��NoWrap=�6a   $��Center=�6w   $
��Left=�6�   ��69   �� *Pim+6�   5R      5T      �� 5U      $��Добавлен��6%   $
 auto��67    5V      �� 5W      .R�5X   "   �� $
 date��6,   
�������$
��Wrap=�6c   $��Center=�6w   $
��Left=�6�   ��6:   �� *Date+6�   5Y      5[      5\      5^          DashBoardClient.WaiterForm  5_   
   -   5          Name$
 wait��5_      6,   5`      5b      