   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5          DashBoardClient.Doc   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:DashBoardClient     5        N� Title$	  Doc��5	       � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� ScaleX	 �� ScaleY
 �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
 0.85Q�6-   $
	 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      "
 ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5        � ��# � ��6+   5!      5"      .��5$      � .+�5%   
   �� -   5        $'��!Добавить документ��5%      $��30��6-   $��330,10,320,0q�69   $	��Top=�6O   $��Center=�6g   $	��150��6�   ���	#FF2BAAC9
�������
�������6�   �� 5&   
   -   5         ��Name$ DocListView��5&   3   $��0,55,5,0q�6H   $��11(�6Z   ���	#FF45454F J� 	AllowDrop . ��6   �� *	DocList+��5'      �� 5(       �� Color$ #45454f��6!   ��5*      �� 5+      " ��6   .�5-      ��  � �� 5.      �� 5/      "
 ��6.   .R�50   "   �� $��Border��6)   
������ ���#E9E5E1$��0q�6f   $��1,0,1,1q�6q   ._�51   &   �� 52   #    �� Triggers 53   "   P� 54   &    �@J�IsMouseOver . . �55   *   ��  �� 
TargetName$ Border��6G    � �� �#5555616n   56   '   P� 57   &    �@��
IsSelected . . �58   *   �� $ Border��6G    � �� �#35353d6m   59   '   5;      5=      5>       �� View 5@      �� 5A      .P�5B      ��  �� Width$
 auto��6%   $��КП.��62    �� CellTemplate 5C      �� 5D      .R�5E   "   ��  ��Name$ kpBtn��6)   -   5        $��30��5E   K   $��Center=�6�   $��30��6�   
 � Tag 68   �� *ID+��6W   Z� ��6n   Z� ��5F   &   �� 5G   *    �� ImageSource$0 */DashBoardClient;component/Images/open.png��65    j� Stretch$ Uniform=�6n   5I   #   5J      5L      �� 5M      $
 auto��6%   $��Ред. ��62    5N      �� 5O      .R�5P   "   �� $ redBtn��6)   -   5        $��30��5P   L   $��Center=�6�   $��30��6�    69   �� *ID+��6X   Z� ��6o   Z� ��5Q   &   �� 5R   *   $/ )/DashBoardClient;component/Images/set.png��65   $ Uniform=�6m   5T   #   5U      5W      �� 5X      $5��/Программа и методика (ПиМ)��6%   $	 400��6I    5Y      �� 5Z      .R�5[   "   ��  ��Name$	 pim��6,   
�������$��NoWrap=�6a   $��Center=�6w   $
��Left=�6�   ��69   �� *Pim+6�   5\      5^      �� 5_      $��Добавлен��6%   $
 auto��67    5`      �� 5a      .R�5b   "   �� $
 date��6,   
�������$
��Wrap=�6c   $��Center=�6w   $
��Left=�6�   ��6:   �� *Date+6�   5c      5e      5f      5h          DashBoardClient.WaiterForm  5i   
   -   5          Name$
 wait��5i      6,   5j      5k      