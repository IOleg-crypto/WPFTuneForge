   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WpfTuneForgePlayer.ViewsWpfTuneForgePlayer.Views  5      K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  5      '    !WpfTuneForgePlayer.Views.Settings   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:WpfTuneForgePlayer.Views     5      @	viewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      b ^ActiproSoftware.Shared.Wpf, Version=25.1.0.0, Culture=neutral, PublicKeyToken=36ff2196ab5654b9Ashared4http://schemas.actiprosoftware.com/winfx/xaml/shared  5	      &   System.Windows.Media.TextOptions    TextRenderingMode$  	ClearType=�5         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels .  N� 
Background
 �   �.��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   $��	0,10,0,20q�6!   .+�5      �� $��Settings��6   $��28(�5      
�������$
��Bold"�5      $
��Left=�5      �� 5!      -   5        $��40��5!      $��40��6    $��Right=�5"      
������ 
������  � ToolTip$
 Back��5&       � Cursor$
 Hand��5'      $��0,0,10,0q�5(      .��5)      �� $6��0pack://siteoforigin:,,,/assets/sidebar/exit2.png��6   $��Uniform=�6R   5*      5+      �� 5.   
   $��1��6   ���	#22FFFFFF �� CornerRadius$ 12��50      	 �� Padding$	 20q�51      ���#555$��1q�53      ._�54      �� .+�56      �� 
 �� Orientation$
 
Horizontal=�6   $��Center=�66   .+�57      �� $��Current device:��6   
�������$��16(�59      $	��140��5:      $��Center=�5;      �� 5=      $	��300��6   $��32��5>      ���#111
���   ����#444$��14(�5D      ��5?      �� *OutputDevices+ �� SelectedItem 5@      �� *SelectedOutputDevice+ �� Mode$ TwoWay=���5E      �� 5F      .R�5G   "   �� ��6,   �� *Name+5H      5J      5K      �� 5M      $
 
Horizontal=�6   $��Center=�66   .+�5N      �� $��Sample Rate:��6   
�������$��16(�5P      $	��140��5Q      �� 5R      $��14(�5S       
�������$��Center=�5U       ��5R       �� *!SelectedOutputDevice.SampleRate+ �� StringFormat$ {0} Hz��5U       5V      �� 5X      $
 
Horizontal=�6   $��Center=�66   .+�5Y      �� $��	Channels:��6   
�������$��16(�5[      $	��140��5\      �� 5]      $��14(�5^       
�������$��Center=�5`       ��5]       �� *SelectedOutputDevice.Channels+5`       5a      �� 5b      $
 
Horizontal=�6   $��Center=�66   .+�5c      �� $��Automatic playback music��6   $��14(�5d      
�������$	��182��5e   .   $��3,0,0,0q�6:   3  -ActiproSoftware.Windows.Controls.ToggleSwitch  5f      $��ToggleSwitch��6*   $	��134��6A      ContentPlacement$ Track=�6�    d� 	IsChecked 6M   �� *IsAutomaticPlayback+$ TwoWay=�6�   5g      �� 5h      $��50��6   $��0,20,0,0q�6*   $��Center=�6=   .+�5i      �� $/��)More settings could be added in future...��6   $��Center=�6P   
�������6�   5j      5k      5m      �� 5o   
   -   5        $��About��5o      $��2��5p      $	��120��5q      $��35��5r      $��0,20,0,0q�5s      $��Center=�5t      ���#333
�������$
��Bold"�5x      ���#555$
 Hand��5z      5|      5}      