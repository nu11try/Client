   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5   	       DashBoardClient.Settings   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DashBoardClient     5   	    "  VS2012WindowStyleWithoutButton#��[  5   	     � Title$   ��5	   	   $��353.747��6   $��435.273��6$   $��237.469��65   $	��258��6I   ���	#FF323439 � WindowStartupLocation$ CenterScreen=�6p    � 	Resources 5
       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value	 �� ScaleX
 �� ScaleY �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
	 0.85Q�6-   $

 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5       � ��# � ��6+   5       5!      .��5"      � $��0,0,0,-8q�6   .+�5#   
   � -   5         �Name$ projects��5#      $��10,101,0,55q�6!   �� 5$   
   -   5         ��Name$
 Exit��5$      $&�� Выйти из аккаунта��6   $��10,257,10,0q�6;   $	��Top=�6P   $��40��6h   ���#a5131d
����������	#FF2BAAC9$��1q�6�    J� Effect 5%      3  -System.Windows.Media.Effects.DropShadowEffect  5&         
BlurRadius$ 10Q�6#      	Direction$	 283Q�63      Opacity$	 0.6Q�6C      ShadowDepth$ 2Q�6Q   5(      �� 5)   
   $��IP сервера��6   $
��Left=�6%   $��	10,15,0,0q�6@   $	��Top=�6S   
�������6k   �� 5*   
   -   5         ��Name$ IPEdit��5*      $��23��6"   $��
10,42,10,0q�6.    �� TextWrapping$
 Wrap=�6B   $�� ��6V   $	��Top=�6^   �� 5+   
   -   5        $ IPSet��5+      $��Применить��6    $��10,212,10,0q�64   $	��Top=�6I   $��40��6a   ���	#FF2BAAC9
����������	#FF2BAAC9$��1q�6�    5,        5-      $ 10Q�6#   $	 283Q�63   $	 0.6Q�6C   $ 2Q�6Q   5/      �� 50   
   $Q��KОтображать запущенные тесты на проектах:��6   $
��Left=�6C   $��	10,70,0,0q�6^   $	��Top=�6q   
�������6�   51      52      