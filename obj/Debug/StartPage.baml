   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfTuneForgePlayerWpfTuneForgePlayer  5      K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  5      "    WpfTuneForgePlayer.StartPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:WpfTuneForgePlayer     5      @	viewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5        N� Title$  	StartPage��5      &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels . .��5      � 
���   �.+�5   
   (�  (� Stretch$ Uniform=�6   .��5      � $	��800��6   $	��450��6   
���   � � RowDefinitions 5      �� 5      $��3* �6$   �� 5      $	��43* �6$   .+�5       � -   5          �Name$ MainContentFrame��5      $��2��62     � NavigationUIVisibility$ Hidden=�6B   � 5      $	��Top=�6   $	��120��6/   $��	0,280,0,0q�6<   $��1��6O   	 � ColumnDefinitions	 5      �� 5      $
��267* �6+   �� 5      $
��267* �6+   �� 5       $
��134* �6+   �� 5!      $��4 �6+   �� 5"      $	��42* �6+   �� 5#      $	��86* �6+   .+�5&      � $��1��6   .+�5'      �� 
 �� Orientation$
 
Horizontal=�6%   $
��Left=�5(   $   $	��Top=�5)   $   $��	68,48,0,0q�5*   $   $��80��5+   $   .+�5,      ��    ImageButtonStyle#��[  6%   
������ $	��5,0q�6i   �� 5.      #��[  6%   
������  � Cursor$
 Hand��5/   ?   $	��5,0q�6M   ��5.   O   �� *PlayCommand+.��50   "   �� $3��-pack://siteoforigin:,,,/assets/menu/pause.png��6(   $��97��6_   $��88��6j   51      52      �� 53      #��[  6!   ��6K   �� *_startMusic+.��54      �� $6��0pack://siteoforigin:,,,/assets/menu/previous.png��6$   $��0,31,188,-20q�6^   55      �� 57      #��[  6!   ��6K   �� *	_endMusic+.��58      �� $2��,pack://siteoforigin:,,,/assets/menu/next.png��6$   $��195,56,-6,6q�6Z    J� RenderTransformOrigin$ 0.5,0.5&�6o   59      5:      �� 5<      -   5         ��Name$ StartMusicLabel��5<      
�������$��14(�6h   $
��Left=�5=      $	��Top=�67   $��	51,20,0,0q�6O   $��0��6b   ��5<   5   �� *CurrentTime+5=   b   �� 5>      -   5        $ EndMusicLabel��5>      
�������$��14(�6b   $��4��5?      $��
11,20,0,70q�6,   $
��Left=�6@   $��2��6[   ��5>   3   �� *	EndTime+5?   [   �� 5A      -   5         ��Name$ MusicTrackBar��5A       � Maximum$
 1000Q�5B       � Minimum$ 0Q�6,   }�. $��38,10,39,77q�6L   $��6��6a   $��0��6u    � Value 5A   4   �� *TrackPosition+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5C      +  %Microsoft.Xaml.Behaviors.EventTrigger  5D         	EventName$ PreviewMouseLeftButtonUp��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5E   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *changeMusicTime+5F      5H      �� 5J      #��[  6   
������ $	��5,0q�6�   ��6a   �� *SelectFavoriteSong+.��5K      �� -   5         ��Name$ FavoriteSong��5K       $��647,-18,-430,119q�66   $��2��6p   $��3��6�   ��6P   �� *FavoriteSong+6�   5L      �� 5N      #��[  6   
������ ��6G   �� *RepeatCommand+.��5O      �� $��724,-19,-500,120q�6    $8��2pack://siteoforigin:,,,/assets/menu/repeatSong.png��6:   $��5��6v   5P      �� 5R      $@��:pack://siteoforigin:,,,/assets/menu/songHaoticPlaylist.png��6   $
��Left=�6`   $��26��6{   $ -1.834,0.31&�6�   $��29,-18,0,121q�6�   $��4��6�   $��2��6�   5S      �� 5U      -   5        $ 	MusicLogo��5U      $��300,10,300,0q�6+   $��1��6]   $	��164��6j   $	��Top=�6w   $	��200��6�   ��6A   �� *
AlbumArt+z�5V      V� 5W      .~�5X      �� �� 5Y       �� AngleY$ 0Q�6,   �� 5Z      T� 5[       T� Y$ 0Q�61   5\      5^      �� 5`      -   5        $ NameSong��5`      $��Center=�6E   $��Center=�6g   
�������$��	0,185,0,0q�5a   +   $��1��6>   $	��Top=�6K   $��14(�6c   $	��800��6q   $ 0.5,0.5&�5b      $��Right=�68   ��5`   *   �� *Artist+z�5c      V� 5d      .~�5e      �� �� 5f      $ 0.055Q�6,   �� 5g      T� 5h      $ 0.205Q�61   5i      5k      �� 5m      -   5        $ 
NameArtist��5m      $��Center=�6F   $��Center=�6h   
�������$��
2,232,-2,0q�5n   +   $��1��6?   $	��Top=�6L   $ 0.5,0.5&�6d   $��12(�6�   ��5m   ,   �� *Title+z�5o      V� 5p      .~�5q      �� �� 5r      �� 5s       �� Angle$ -0.165Q�6.   T� 5t      5u      5w      �� 5y      -	   5        $ NameProgram��5y      $��	TuneForge��6-   $
��Left=�6A   
�������$��35��6o   $��702,-10,0,0q�5z      $	��Top=�6-   $��88��6E   $��16(�6P   $��2��6^   5{      5|      �� 5~   
   #��[  6   
������ $��109,79,0,60q�5      $
��Left=�6&   $	��509��6A   ��5~   ;   �� *toggleAudio+.��5�      �� -
   5        $ SoundButton��5�      $��1��6)   $��-84,192,554,77q�66   $ 0.5,0.5&�6m   ��6N   �� *SoundStatus+z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      $ -0.441Q�6*   T� 5�      5�      5�      5�      5�      5�      