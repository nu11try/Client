   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5   	   $    DashBoardClient.TestFormChange   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DashBoardClient     5   	    "  VS2012WindowStyleWithoutButton#��[  5   
   $��384.291��5	      $	��294��6   $��384.291��6(   $	��294��6<     � 
ResizeMode$  NoResize=�6K    � WindowStartupLocation$ CenterScreen=�6a   ���	#FF444444 � 	Resources 5
       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value	 �� ScaleX
 �� ScaleY �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
	 0.85Q�6-   $

 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5       � ��# � ��6+   5       5!      .��5"      � ���	#FF45454F$��0,0,0,-4q�6"   .+�5#   
   �� -   5         ��Name$ TestID��5#      $��25��6"   $��
25,65,25,0q�6.   $	��Top=�6B   {�.  6Z   �� 5$   
   -   5         ��Name$ MethodSelect��5$      $��28��6)   $��25,147,25,0q�65   $	��Top=�6J   �� 5%   
   -   5         ��Name$ ActiveSelect��5%      $8��2Показывать ли в статистике?��6)   $
��Left=�6O   $��30��6j   $��
25,202,0,0q�6v   $	��Top=�6�   $	��238��6�   
�������6�   �� 5&   
   -   5         ��Name$ 
SubmitTest��5&      $��Подтвердить��6%   $
��Left=�6;   $��
25,257,0,0q�6V   $	��238��6j   $��28��6v   $	��Top=�6�   6�   �� 5'   
   -   5        $ 
CancelTest��5'      $��Отменить��6%   $
��Left=�68   $��
25,297,0,0q�6S   $	��238��6g   $��28��6s   $	��Top=�6   6�   �� 5(   
   $��ID��6   $
��Left=�6   $��	25,34,0,0q�68   $	��Top=�6K   
�������6c   �� 5)   
   $4��.Выберите тестовый случай��6   $
��Left=�63   $��
25,116,0,0q�6N   $	��Top=�6b   
�������6z   5+      5,      