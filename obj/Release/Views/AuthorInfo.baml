   M S B A M L   `   `   `  ����   WpfTuneForgePlayer)    #WpfTuneForgePlayer.Views.AuthorInfo   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     
������   � WindowStyle$
  None=� � AllowsTransparency .  � 
ResizeMode$ CanMinimize=�&   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�   TextFormattingMode$ Display=� � UseLayoutRounding .  J� SnapsToDevicePixels .  � Title$ 
AuthorInfo��$	��300��$	��600��.���� 
���   � �� CornerRadius$ 16�����#333$��1q� . 	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   
BlurRadius$
 15Q�   ShadowDepth$ 2Q�   Opacity$	 0.3Q�._��  � RowDefinitions �� $��48 ��� $��* �.+�� -   $��0�����#1A1A1A � ColumnDefinitions �� $��* ��� $
��Auto �.+��� $��	TuneForge��
�������$��18(�$��SemiBold"�$��Center=�$
��16,0q��� -   $	��✕��$��32��$��32��$��14(�
������ 
�������
������  � Cursor$
 Hand�� � ToolTip$ Close��$��Right=�� $��1��$��20,10,20,20q� �� $
��Auto ��� $��* �.+��� $��0��$��0,0,20,0q�._��� $8��2pack://siteoforigin:,,,/assets/menu/logogithub.jpg��$	��150��$	��120��$��UniformToFill=��� $��1��$��Center=�.+��� $��GitHub profile:��$��20(�$
��Bold"�
��������� $��16(�
����ح��� -   $%��https://github.com/IOleg-crypto?�.�!https://github.com/IOleg-crypto�� $[��UThis application is a music player developed using WPF, MVVM, and the NAudio library.��$��16(�
�������$
��Wrap=� �� TextAlignment$
 Left=�