   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5          DashBoardClient.Autostart   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:DashBoardClient     5        N� Title$  	Autostart��5	       � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� ScaleX	 �� ScaleY
 �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
 0.85Q�6-   $
	 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      "
 ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5        � ��# � ��6+   5!      5"      .��5$      � .+�5%   
   �� -   5        $-��'Добавление автостарт��5%      $��30��60   $��330,10,295,0q�6<   $	��Top=�6R   $��Center=�6j   $	��175��6�   ���	#FF2BAAC9
�������
�������6�   �� 5&   
   -   5         ��Name$ AutoListView��5&   +   $��0,55,5,0q�6A   $��11(�6S   ���	#FF45454F��6   �� ��5'      �� 5(       �� Color$ #45454f��6!   ��5*      �� 5+      " ��6   .�5-      ��  � �� 5.      �� 5/      "
 ��6.   .R�50   "   �� $��Border��6)   
������ ���#E9E5E1$��0q�6f   $��1,0,1,1q�6q   ._�51   &   �� 52   #    �� Triggers 53   "   P� 54   &    �@J�IsMouseOver . . �55   *   ��  �� 
TargetName$ Border��6G    � �� �#5555616n   56   '   P� 57   &    �@��
IsSelected . . �58   *   �� $ Border��6G    � �� �#35353d6m   59   '   5;      5=      5>       �� View 5@      �� 5A      .P�5B      ��  �� Width$
 auto��6%    �� CellTemplate 5C      �� 5D      .R�5E   "   ��  ��Name$ 	changeBtn��6)   -   5        $��30��5E   O   $��Center=�6�   $��30��6�   
 � Tag 6<   �� *ID+��6[   Z� ��6r   Z� ��5F   &   �� 5G   *    �� ImageSource$/ )/DashBoardClient;component/Images/set.png��65    j� Stretch$ Uniform=�6m   5I   #   5J      5L      �� 5M      $��Имя��6%   $
 auto��62    5N      �� 5O      .R�5P   "   ��  ��Name$
 Name��6,   
�������$
��Wrap=�6c   $��Center=�6w   $
��Left=�6�   ��6:   �� *Name+6�   5Q      5S      �� 5T      $��Тип��6%   $
 auto��62    5U      �� 5V      .R�5W   "   �� $
 Type��6,   
�������$
��Wrap=�6c   $��Center=�6w   $��Center=�6�   ��6:   �� *Type+6�   5X      5Z      �� 5[      $��Наборы��6%   $
 auto��65    5\      �� 5]      .R�5^   "   �� $ Packs��6,   
�������$
��Wrap=�6d   $��Center=�6x   $��Center=�6�   ��6;   �� *Pack+6�   5_      5a      �� 5b      $��Дни��6%   $
 auto��62    5c      �� 5d      .R�5e   "   �� $	 Day��6,   
�������$
��Wrap=�6a   $��Center=�6u   $��Center=�6�   ��69   �� *Day+6�   5f      5h      �� 5i      $��
Время��6%   $
 auto��64    5j      �� 5k      .R�5l   "   �� $
 Time��6,   
�������$
��Wrap=�6c   $��Center=�6w   $��Center=�6�   ��6:   �� *Time+6�   5m      5o      5�      5�          DashBoardClient.WaiterForm  5�   
   -   5          Name$
 wait��5�      6,   5�      5�      