   M S B A M L   `   `   `  ���� M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WpfTuneForgePlayer.ViewsWpfTuneForgePlayer.Views  K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  ,    &WpfTuneForgePlayer.Views.FavoriteSongs   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:WpfTuneForgePlayer.Views     @	ViewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     &   System.Windows.Media.TextOptions    TextRenderingMode$  	ClearType=�   TextFormattingMode$ Display=� � UseLayoutRounding .  J� SnapsToDevicePixels .  N� 
Background �	#FF0A0A0A N� Title$ FavoriteSongs�� � 	Resources =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property	 �� Value��%�   '        �� "  .���  � ��
	 �������  � ��$
	 Bold"���  � ��	 �#333��  � ��	 �#555��  � ��$	 0,0,0,1q���  � ��$	 Center=�.��� $��25q�.+��� -   $��35��$��34��$��	50,5,25,0q�
������ 
������ 
������ $��Right=�
 K� ZIndex$	
 999��$	��Top=�.���� $5��/pack://siteoforigin:,,,/assets/sidebar/exit.png��$��Uniform=�$��32��$��34���� $��🎵 Favorite Songs��$��32(�$
��Bold"�
��� ���$��0,0,0,20q�$  	ClearType=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ White��   
BlurRadius$ 15Q�   ShadowDepth$ 5Q�   Opacity$	 0.6Q��� ���	#22FFFFFF �� CornerRadius$ 10�� �� Padding$ 15q�$��0,60,0,0q�._�&   System.Windows.Controls.DataGrid  -     Name$ FavoriteSongsGrid��   AutoGenerateColumns .     CanUserAddRows .     HeadersVisibility$ Column=�   RowBackground �	#22000000   AlternatingRowBackground �	#33000000
�������   GridLinesVisibility$
 None=�
������ {�.  $��0q�$��16(�   SelectionMode$ Single=�   SelectionUnit$ FullRow=���.    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
🎧 Title��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Title+  $ 👤 Artist��$ 2*  �� *Artist+  $ ⏱ Duration��$ *  �� *
Duration+