   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfTuneForgePlayerWpfTuneForgePlayer  5      K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  5      "    WpfTuneForgePlayer.StartPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:WpfTuneForgePlayer     5      @	viewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      ` \Syncfusion.Tools.Wpf, Version=30.1462.41.0, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89a ]Syncfusion.Shared.WPF, Version=30.1462.41.0, Culture=neutral, PublicKeyToken=3d67ed1f87d44c894
syncfusion!http://schemas.syncfusion.com/wpf   5	      b ^ActiproSoftware.Shared.Wpf, Version=25.1.0.0, Culture=neutral, PublicKeyToken=36ff2196ab5654b9Ashared4http://schemas.actiprosoftware.com/winfx/xaml/shared  5
        N� Title$  	StartPage��5      &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels . .��5      � 
���   � � RowDefinitions 5   
   �� 5      $
��133* �6   �� 5      $
��113* �6   .+�5   
   (�  (� Stretch$ Uniform=�6   $��2��6$   .��5      � $	��800��6   $	��450��6   
���   � 5      �� 5      $��3* �6$   �� 5      $	��43* �6$   .+�5!       � -   5          �Name$ MainContentFrame��5!      $��2��62     � NavigationUIVisibility$ Hidden=�6B   � 5#      $	��Top=�6   $	��120��6/   $��	0,280,0,0q�6<   $��1��6O   	 � ColumnDefinitions	 5$      �� 5%      $
��267* �6+   �� 5&      $
��267* �6+   �� 5'      $
��134* �6+   �� 5(      $��4 �6+   �� 5)      $	��42* �6+   �� 5*      $	��86* �6+   .+�5-      �� 
 �� Orientation$
 
Horizontal=�6!   $
��Left=�6:   $	��Top=�6U   $��
27,-25,0,0q�6m    K� ZIndex$ 1��6�   .+�5.      ��    ImageButtonStyle#��[  6!   $��24��6w   $��24��6�   $��4,0,4,0q�6�   ��6K   �� *Commands.DecreaseVolume+.��5/      �� $=��7pack://siteoforigin:,,,/assets/menu/decrease-volume.png��6$   $��24��6e   $��24��6p   50      �� 52      #��[  6!   $��24��6t   $��24��6   $��4,0,4,0q�6�   ��6K   �� *Commands.ToggleAudio+.��53      �� $��24��6C   $��24��6N   ��6$   �� *SoundStatus+6N   54      �� 56      #��[  6!   $��24��6w   $��24��6�   $��4,0,4,0q�6�   ��6K   �� *Commands.IncreaseVolume+.��57      �� $6��0pack://siteoforigin:,,,/assets/menu/volumeup.png��6$   $��24��6^   $��24��6i   58      59      � 5;      $��1��6   .+�5<      �� $
 
Horizontal=�6%   $
��Left=�5=   $   $	��Top=�5>   $   $��	68,48,0,0q�5?   $   $��80��5@   $   .+�5A      �� #��[  6%   
������ $	��5,0q�6i   �� 5C      #��[  6%   
������  � Cursor$
 Hand��5D   ?   $	��5,0q�6M   ��5C   O   �� *Commands.PlayCommand+.��5E   "   �� -   5         ��Name$ 
PlayButton��5E   (   $	��110��6`   $��88��6l   $��0,-5,8,0q�6x   ��6<   �� *PlayPauseButton+5F   #   5G      5H      �� 5I      #��[  6!   ��6K   �� *Commands.StartMusic+.��5J      �� $6��0pack://siteoforigin:,,,/assets/menu/previous.png��6$   $��0,31,188,-20q�6^   5K      �� 5M      #��[  6!   ��6K   �� *Commands.EndMusic+.��5N      �� $2��,pack://siteoforigin:,,,/assets/menu/next.png��6$   $��195,56,-6,6q�6Z    J� RenderTransformOrigin$ 0.5,0.5&�6o   5O      5P      �� 5R      -   5         ��Name$ StartMusicLabel��5R      
�������$��14(�6h   $
��Left=�5S      $	��Top=�67   $��	51,20,0,0q�6O   $��0��6b   ��5R   5   �� *CurrentTime+5S   b   �� 5T      -   5        $ EndMusicLabel��5T      
�������$��14(�6b   $��4��5U      $��
11,20,0,70q�6,   $
��Left=�6@   $��2��6[   ��5T   3   �� *	EndTime+5U   [   �� 5W      -   5         ��Name$ MusicTrackBar��5W       � Maximum$
 1000Q�5X       � Minimum$ 0Q�6,   $��38,10,39,77q�68   $��6��6M   $��0��6a    � Value 5W   4   �� *TrackPosition+ �� Mode$ TwoWay=�{�6a   �� *GetStatusOnSlider+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5Y      +  %Microsoft.Xaml.Behaviors.EventTrigger  5Z         	EventName$ PreviewMouseLeftButtonUp��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5[   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *Commands.ChangeMusicTime+5\      5^      �� 5`      #��[  6   
������ $	��5,0q�6�   ��6a   �� *Commands.SelectFavoriteSong+.��5a      �� -   5        $ FavoriteSong��5a       $��664,-18,-430,119q�66   $��2��6p   $��3��6�   ��6P   �� *FavoriteSong+6�   5b      �� 5d      #��[  6   
������ ��6G   �� *Commands.RepeatCommand+.��5e      �� $��724,-19,-500,120q�6    $8��2pack://siteoforigin:,,,/assets/menu/repeatSong.png��6:   $��5��6v   5f      �� 5h      #��[  6   
������ $��5,0,62,0q�6�   $��4��6�   $��2��6�   ��6G   �� * Commands.SelectChaoticallySong+.��5i      �� $@��:pack://siteoforigin:,,,/assets/menu/songHaoticPlaylist.png��6    $
��Left=�5j      $��26��5k      $
 Hand��5l      $ -1.834,0.31&�5m      $��27,-18,0,121q�5n      5o      5p      �� 5r      -   5        $ 	MusicLogo��5r      $��300,10,300,0q�6+   $��1��6]   $	��164��6j   $	��Top=�6w   $	��200��6�   ��6A   �� *
AlbumArt+ J� Effect 5s      3  -System.Windows.Media.Effects.DropShadowEffect  5t      z�5v      V� 5w      .~�5x      �� �� 5y       �� AngleY$ 0Q�6,   �� 5z      T� 5{       T� Y$ 0Q�61   5|      5~      �� 5�      -   5        $ NameSong��5�      $��Center=�6E   $��Center=�6g   
�������$��	0,185,0,0q�5�   +   $��1��6>   $	��Top=�6K   $��14(�6c   $	��800��6q   $ 0.5,0.5&�5�      $��Right=�68   ��5�   *   �� *Artist+z�5�      V� 5�      .~�5�      �� �� 5�      $ 0.055Q�6,   �� 5�      T� 5�      $ 0.205Q�61   5�      5�      �� 5�      -	   5        $ 
NameArtist��5�      $��Center=�6F   $��Center=�6h   
�������$��
2,232,-2,0q�5�   +   $��1��6?   $	��Top=�6L   $ 0.5,0.5&�6d   $��12(�6�   ��5�   ,   �� *Title+z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�       �� Angle$ -0.165Q�6.   T� 5�      5�      5�      �� 5�      -
   5        $ NameProgram��5�      $��	TuneForge��6-   $
��Left=�6A   
�������$��35��6o   $��702,-10,0,0q�5�      $	��Top=�6-   $��88��6E   $��16(�6P   $��2��6^   5�      5�      5�      5�      