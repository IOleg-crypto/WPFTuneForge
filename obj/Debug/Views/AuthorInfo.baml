   M S B A M L   `   `   `  ����  WpfTuneForgePlayer)    #WpfTuneForgePlayer.Views.AuthorInfo   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   
������   � WindowStyle$
  None=�5   	    � AllowsTransparency .  � 
ResizeMode$ CanMinimize=�5
   	   &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5   	      TextFormattingMode$ Display=�5   	    � UseLayoutRounding .  J� SnapsToDevicePixels .  � Title$ 
AuthorInfo��5   	   $	��300��5   	   $	��600��6   .��5      �� 
���   � �� CornerRadius$ 16��5      ���#333$��1q�5       . 	 J� Effect	 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 15Q�6      ShadowDepth$ 2Q�6/      Opacity$	 0.3Q�6?   ._�5   
   �  � RowDefinitions 5      �� 5      $��48 �6    �� 5      $��* �6    .+�5"      � -   5        $��0��5"      ���#1A1A1A � ColumnDefinitions 5#      �� 5$      $��* �6'   �� 5%      $
��Auto �6'   .+�5(      �� $��	TuneForge��6   
�������$��18(�5*      $��SemiBold"�5+      $��Center=�5,      $
��16,0q�5-      �� 50      -   5        $	��✕��50      $��32��51      $��32��6$   $��14(�52      
������ 
�������
������  � Cursor$
 Hand��56       � ToolTip$ Close��57      $��Right=�59      5:      � 5=      $��1��6   $��20,10,20,20q�6     5>      �� 5?      $
��Auto �6'   �� 5@      $��* �6'   .+�5D      �� $��0��6   $��0,0,20,0q�6)   ._�5E      �� $8��2pack://siteoforigin:,,,/assets/menu/logogithub.jpg��6   $	��150��5F      $	��120��6(   $��UniformToFill=�5G      5H      �� 5K      $��1��6   $��Center=�6-   .+�5L      �� $��GitHub profile:��6    $��20(�5M       $
��Bold"�5N       
�������5O       �� 5Q      $��16(�6    
����ح��� 5R      -   5        $%��https://github.com/IOleg-crypto?�5R   $   .�5S   P   !https://github.com/IOleg-crypto5U      5V      �� 5X      $[��UThis application is a music player developed using WPF, MVVM, and the NAudio library.��6    $��16(�5Y       
�������$
��Wrap=�5[        �� TextAlignment$
 Left=�5\       5]      5^      5_      5`      5a      