   M S B A M L   `   `   `  ���� M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WpfTuneForgePlayer.ViewsWpfTuneForgePlayer.Views  K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  '    !WpfTuneForgePlayer.Views.Settings   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:WpfTuneForgePlayer.Views     @	viewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  b ^ActiproSoftware.Shared.Wpf, Version=25.1.0.0, Culture=neutral, PublicKeyToken=36ff2196ab5654b9Ashared4http://schemas.actiprosoftware.com/winfx/xaml/shared  &   System.Windows.Media.TextOptions    TextRenderingMode$  	ClearType=�   TextFormattingMode$ Display=� � UseLayoutRounding .  J� SnapsToDevicePixels . .��� 
���   � � RowDefinitions �� $
��Auto ��� $��80 ��� $
��Auto �.+��� $��Settings��$��26(�
�������$
��Bold"�$��20q�$
��Left=��� -   $��0��$��47��
������ $��0q� � Cursor$
 Hand��$��	0,20,0,27q�$��Right=�.���� $6��0pack://siteoforigin:,,,/assets/sidebar/exit2.png��$��Uniform=�$��29��$��29���� $��1�� �� Orientation$ 
Horizontal=�$��Center=�$
��Left=�$��	150,0,0,0q�.+��� $��Current device:��
�������$��18(�$��Center=�$	��160���� $	��350��$��32�� �� 
IsEditable .   �� 
IsReadOnly . 	 J� IsHitTestVisible	 .  
���   �
���   �
�������$��16(����� *OutputDevices+
 �� SelectedItem
 �� *SelectedOutputDevice+ �� Mode$ TwoWay=��� $��1��$ 
Horizontal=�$	��Top=�$
��Left=�$	��661��$��2,78,0,0q�$��2��.+��� $��Sample rate:��
�������$��18(�$��Center=�$	��160��$��	153,0,0,0q��� -    ��Name$ SampleRateSelector�� �� SelectedIndex$ 0��	 .  $	��150��$��30��$��0,0,28,0q�.@��� $��44100 Hz���� $��48000 Hz���� $��96000 Hz���� $��3��$ 
Horizontal=�$��Center=�$��Center=�$	��661���� -   $��Center=�$��About��$��14(�$��2��$��Normal"�
������ 
�������
�������$
 Hand��$��Stretch=�$��0,235,397,0q�$	��104��