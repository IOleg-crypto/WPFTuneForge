   M S B A M L   `   `   `  ���� M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfTuneForgePlayerWpfTuneForgePlayer  #    WpfTuneForgePlayer.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ?wpfTuneForgePlayer clr-namespace:WpfTuneForgePlayer       � Title$  	TuneForge��$	��960��$	��500�� � WindowStyle$
 None=� � AllowsTransparency . 
������ &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�   TextFormattingMode$ Display=� � UseLayoutRounding .  J� SnapsToDevicePixels .  � Icon$2 ,pack://siteoforigin:,,,/assets/icon/icon.png��.���� 
���   � �� CornerRadius$ 16�����#333$��1q� . 	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   
BlurRadius$
 15Q�   ShadowDepth$ 2Q�   Opacity$	 0.3Q�._��  � RowDefinitions �� $��48 ��� $��* �.+�� -   $��0�����#1A1A1A � ColumnDefinitions �� $��* ��� $
��Auto �.+��� $��	TuneForge��
�������$��18(�$��SemiBold"�$��Center=�$
��16,0q��� $��1�� �� Orientation$ 
Horizontal=�$��Center=�$��0,0,10,0q�.+��� -   $��36��$��28��
������ $��0q� � Cursor$
 Hand��.���� $	��─��$��16(�
�������$��Center=�$��Center=��� -   $��36��$��28��
������ $��0q�$
 Hand��.���� $	��✕��$��16(�
�������$��Center=�$��Center=�� $��1�� �� $	��142 ��� $��42 ��� $��* �.+�    WpfTuneForgePlayer.Sidebar  -     Name$ Sidebar��$��0��$
��Left=�$��3�� � -     �Name$ MainContentFrame��$��2��  � NavigationUIVisibility$ Hidden=�
���   �