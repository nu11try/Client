   M S B A M L   `   `   `  ����J  FDashBoardClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DashBoardClientDashBoardClient  5   	       DashBoardClient.Auth   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:DashBoardClient     5   	    "  VS2012WindowStyleWithoutButton#��[  5   	     � Title$   ��5	   	   $��249.544��6   $	��258��6$   $��237.469��61   $	��258��6E   $��237.469��6T   $	��258��6h   ���	#FF323439 � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5
       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value	 �� ScaleX
 �� ScaleY �� 
TargetType ��Name��%�  '��       �� 5   
   " ��6    5      P� 5       � �� . . �5      ��  � z� 5      �� 5      $
	 0.85Q�6-   $

 0.85Q�6;   5      5      .�5      ��  �@J�RenderTransformOrigin$ 0.5, 0.5&�66   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
 brdr��6!   #��z��6/   #��z��6c   #��z��6�   #��z��6�   ._�5      �� #��z��6/   #��z��6r   #��z��6�   5      5      5      �� 5       � ��# � ��6+   5       5!      .��5"      � .+�5#   
   �� $��Авторизация��6   $��	85,36,0,0q�6&   $
��Left=�69   $��82��6T   
�������$��32��6r   $	��Top=�6~   �� 5$   
   -   5         ��Name$ 	loginAuth��5$      $
��Left=�6%   $��22��6@   $��	66,73,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $��
Логин��6s   $	��Top=�6�   $	��120��6�   ���	#FFACACAC.��6�    J� Effect 5%      3  -System.Windows.Media.Effects.DropShadowEffect  5&         
BlurRadius$ 10Q�6#      	Direction$	 283Q�63      Opacity$	 0.6Q�6C      ShadowDepth$ 2Q�6Q   .��5'      5(      G� 5)   
   -   5         G�Name$ passAuth��5)      $
��Left=�6(   $��
66,109,0,0q�6C   $	��Top=�6W   $	��120��6o   $��21��6{    G� Password$ Password��6�   ���	#FFACACAC 5*        5+      $ 10Q�6#   $	 283Q�63   $	 0.6Q�6C   $ 2Q�6Q   5-      �� 5.   
   -   5         ��Name$ btnAuth��5.      $��Вход��6"   $��85,151,82,20q�61   ���	#FF2BAAC9
����������	#FF2BAAC9$��1q�6�    5/        50      $ 10Q�6#   $	 283Q�63   $	 0.6Q�6C   $ 2Q�6Q   52      �� 53   
   -   5        $��25��53      $��25��6   $��1��6'   $��200,10,17,154q�6;   $��1��6R    � Cursor$
 Hand��6c   $0��*/DashBoardClient;component/Images/gear.png��6�    J� OpacityMask 54      �� 55       �� ImageSource$0 */DashBoardClient;component/Images/gear.png��6   57      59      5:      