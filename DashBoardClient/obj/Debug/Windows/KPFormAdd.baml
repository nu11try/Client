   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       DashBoardClient.KPFormAdd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DashBoardClient     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	    "  VS2012WindowStyleWithoutButton#��[  5	   	   $	��286��5
      $	��294��6   $	��286��6"   $	��294��62     � 
ResizeMode$  NoResize=�6A    � WindowStartupLocation$ CenterScreen=�6W   ���	#FF444444 � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value	 �� ScaleX
 �� ScaleY �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
	 0.85Q�6-   $

 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5        � ��# � ��6+   5!      5"      .��5#      � ���	#FF45454F$��0,0,0,-4q�6"   .+�5$   
   �� -   5         ��Name$ NameKP��5$      $��30��6"   $��
25,65,25,0q�6.   $	��Top=�6B   (  "System.Windows.Controls.DatePicker  5%   
   -   5          Name$ 	DateBlock��5%      $��30��6(   $��22,110,28,0q�64   $	��Top=�6I      SelectedDateFormat$ Short=�6a      Text 6|   ��  �� Today �� Source# Z  �� StringFormat$ {0:dddd, MMMM dd, yyyy}���� 5'   
   -   5         ��Name$ SubmitKP��5'      $��Подтвердить��6#   $
��Left=�69   $��
22,157,0,0q�6T   $	��238��6h   $��28��6t   $	��Top=�6�   6�   �� 5(   
   -   5        $ CancelKP��5(      $��Отменить��6#   $
��Left=�66   $��
22,197,0,0q�6Q   $	��238��6e   $��28��6q   $	��Top=�6}   6�   �� 5)   
   $��Название КП��6   $
��Left=�6&   $��	25,34,0,0q�6A   $	��Top=�6T   
�������6l   5+      5,      