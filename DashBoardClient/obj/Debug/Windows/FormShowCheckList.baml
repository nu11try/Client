   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5   	   '    !DashBoardClient.FormShowCheckList   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DashBoardClient     5   	    "  VS2012WindowStyleWithoutButton#��[  5   	   $	��450��5	   	   $	��800��6   $	��450��6"   $	��800��62     � 
ResizeMode$  NoResize=�6A    � WindowStartupLocation$ CenterScreen=�6W   ���	#FF444444 � 	Resources 5
       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value	 �� ScaleX
 �� ScaleY �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
	 0.85Q�6-   $

 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5       � ��# � ��6+   5       5!      .��5"      � .+�5(   
   �� -   5         ��Name$ CommentsList��5(      $��0,43,5,0q�6)   $��11(�6;   ���	#FF45454F��5)      �� 5*       �� Color$ #45454f��6"   ��5,      �� 5.      " ��6   .�5/      ��  � ��$	 Top=�6A   �� 50       � �� 51      �� 52      " ��6.   .R�53   "   �� $��Border��6)   
������ ���#E9E5E1$��0q�6f   $��1,0,1,1q�6q   ._�54   &   �� 55   #    �� Triggers 56   "   P� 57   &    �@J�IsMouseOver . . �58   *   ��  �� 
TargetName$ Border��6G    � �� �#5555616n   59   '   P� 5:   &    �@��
IsSelected . . �5;   *   �� $ Border��6G    � �� �#35353d6m   5<   '   5?      5A      5B       �� View 5E      �� 5F      .P�5G      �� $��
Номер��6%    �� Width$ 50��64    �� CellTemplate 5H      �� 5I      .R�5J   "   ��  ��Name$ Number��6,   $
��Wrap=�6o   $	��Top=�6�   $
��left=�6�   ��6<   �� *step+��6R   �� *Color+6�   5K      5M      �� 5N      $��Шаг��6%   $	 740��62    5O      �� 5P      .R�5Q   "   �� $
 Step��6,   $
��Wrap=�6p   $	��Top=�6�   $
��left=�6�   ��6:   �� *	comment+��6S   �� *Color+6�   5R      5T      5U      5W          DashBoardClient.WaiterForm  5X   
   -   5          Name$
 wait��5X      6,   5Y      5Z      