   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:WpfTuneForgePlayer.ViewsWpfTuneForgePlayer.Views  5      K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  5      ,    &WpfTuneForgePlayer.Views.FavoriteSongs   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:WpfTuneForgePlayer.Views     5      @	ViewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     5      &   System.Windows.Media.TextOptions    TextRenderingMode$  	ClearType=�5
         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels .  N� 
Background �	#FF0A0A0A N� Title$ FavoriteSongs��5       � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property	 �� Value��%  '        �� 5   
   "  6   .�5      ��  � ��
	 �����6+   �� 5       � ��$
	 Bold"�6+   �� 5       � ��	 �#3336+   �� 5       � ��	 �#5556,   �� 5       � ��$	 0,0,0,1q�60   �� 5       � ��$	 Center=�6;   5      5      .��5      � $��25q�6   .+�5   
   �� -   5        $��35��5   (   $��34��63   $��	50,5,25,0q�6?   
������ 
������ 
������ $��Right=�6�   
 K� ZIndex$	
 999��6�   $	��Top=�6�   .��5       �� $5��/pack://siteoforigin:,,,/assets/sidebar/exit.png��6   $��Uniform=�5!      $��32��5"      $��34��5#      5$      �� 5'   
   $��🎵 Favorite Songs��6   $��32(�5(      $
��Bold"�5)      
��� ���$��0,0,0,20q�5+      $  	ClearType=�5,       J� Effect 5.      3  -System.Windows.Media.Effects.DropShadowEffect  5/         Color$ White��6#      
BlurRadius$ 15Q�61      ShadowDepth$ 5Q�6A      Opacity$	 0.6Q�6Q   51      �� 55   
   ���	#22FFFFFF �� CornerRadius$ 10��56       �� Padding$ 15q�57      $��0,60,0,0q�58      ._�5;      &   System.Windows.Controls.DataGrid  -   5          Name$ FavoriteSongsGrid��5;         AutoGenerateColumns .     CanUserAddRows .    HeadersVisibility$ Column=�5>         RowBackground �	#22000000   AlternatingRowBackground �	#33000000
�������   GridLinesVisibility$ 
Horizontal=�5B      
������ {�.    
IsReadOnly . $��0q�5F      $��16(�5G         SelectionMode$ Single=�5H         SelectionUnit$ FullRow=�5I      ��.    Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      ,  &System.Windows.Controls.DataGridColumn   Header$ 👤 Artist��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Artist+6W     5N      $ 
🎧 Title��6)   $ 2* 6U    6;   �� *Title+6U     5O      $ ⏱ Duration��6)   $ 2* 6Z    6=   �� *
Duration+6Z   5Q      5R      5T      5U      