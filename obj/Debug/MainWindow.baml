   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfTuneForgePlayerWpfTuneForgePlayer  5      #    WpfTuneForgePlayer.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ?wpfTuneForgePlayer clr-namespace:WpfTuneForgePlayer     5        � Title$  	TuneForge��5      $	��960��5      $	��480��6    � WindowStyle$
 None=�5       � AllowsTransparency . 
������  � 
ResizeMode$ CanMinimize=�5      &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels .  � Icon$2 ,pack://siteoforigin:,,,/assets/icon/icon.png��5      .��5      �� 
���   �	 �� CornerRadius$	 16��5      ���#333$��1q�5       . 
 J� Effect
 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6      ShadowDepth$ 0Q�6/      Opacity$	 0.3Q�6?   ._�5   
   �  � RowDefinitions 5      �� 5      $��48 �6    �� 5       $��* �6    .+�5$      � -   5        $��0��5$      ���#1A1A1A � ColumnDefinitions 5%      �� 5&      $��* �6'   �� 5'      $
��Auto �6'   .+�5*      �� $��	TuneForge��6   
�������$��18(�5,      $��SemiBold"�5-      $��Center=�5.      $
��16,0q�5/      �� 51      $��1��6    �� Orientation$ 
Horizontal=�6-   $��Center=�6F   $��0,0,10,0q�6a   .+�52      �� -   5        $��36��52      $��28��6(   
������ $��0q�6M    � Cursor$
 Hand��6x   .��53      �� $	��─��6$   $��16(�6-   
�������$��Center=�6N   $��Center=�6i   54      �� 55      -   5        $��36��55      $��28��6(   
������ $��0q�6M   $
 Hand��6u   .��56      �� $	��✕��6$   $��16(�6-   
�������$��Center=�6N   $��Center=�6i   57      58      59      � 5<      $��1��6    5=      �� 5>      $	��142 �6'   �� 5?      $��42 �6'   �� 5@      $��* �6'   .+�5C          WpfTuneForgePlayer.Sidebar  -   5          Name$ Sidebar��5C   -   $��0��5D   -   $
��Left=�5E   -   $��3��5F   -    � 5H      -   5          �Name$ MainContentFrame��5H      $��2��5I        � NavigationUIVisibility$ Hidden=�5J      
���   �5K      5M      5N      5O      5P      