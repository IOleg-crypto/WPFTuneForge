   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfTuneForgePlayerWpfTuneForgePlayer  5      #    WpfTuneForgePlayer.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ?wpfTuneForgePlayer clr-namespace:WpfTuneForgePlayer     5        � Title$  	TuneForge��5      $	��960��5      $	��500��6    � WindowStyle$
 None=�5       � AllowsTransparency . 
������ &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels .  � Icon$2 ,pack://siteoforigin:,,,/assets/icon/icon.png��5      .��5      �� 
���   � �� CornerRadius$ 16��5      ���#333$��1q�5       . 	 J� Effect	 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5      
   
BlurRadius$
 15Q�6      ShadowDepth$ 2Q�6/      Opacity$	 0.3Q�6?   ._�5   
   �  � RowDefinitions 5      �� 5      $��48 �6    �� 5      $��* �6    .+�5#      � -   5        $��0��5#      ���#1A1A1A � ColumnDefinitions 5$      �� 5%      $��* �6'   �� 5&      $
��Auto �6'   .+�5)      �� $��	TuneForge��6   
�������$��18(�5+      $��SemiBold"�5,      $��Center=�5-      $
��16,0q�5.      �� 50      $��1��6    �� Orientation$ 
Horizontal=�6-   $��Center=�6F   $��0,0,10,0q�6a   .+�51      �� -   5        $��36��51      $��28��6(   
������ $��0q�6M    � Cursor$
 Hand��6x   .��52      �� $	��─��6$   $��16(�6-   
�������$��Center=�6N   $��Center=�6i   53      �� 54      -   5        $��36��54      $��28��6(   
������ $��0q�6M   $
 Hand��6u   .��55      �� $	��✕��6$   $��16(�6-   
�������$��Center=�6N   $��Center=�6i   56      57      58      � 5;      $��1��6    5<      �� 5=      $	��142 �6'   �� 5>      $��42 �6'   �� 5?      $��* �6'   .+�5B          WpfTuneForgePlayer.Sidebar  -   5          Name$ Sidebar��5B   -   $��0��5C   -   $
��Left=�5D   -   $��3��5E   -    � 5G      -   5          �Name$ MainContentFrame��5G      $��2��5H        � NavigationUIVisibility$ Hidden=�5I      
���   �5J      5L      5M      5N      5O      