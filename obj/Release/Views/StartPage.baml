   M S B A M L   `   `   `  ���� M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfTuneForgePlayerWpfTuneForgePlayer  K*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  "    WpfTuneForgePlayer.StartPage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:WpfTuneForgePlayer     @	viewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  2
syncfusion!http://schemas.syncfusion.com/wpf  b ^ActiproSoftware.Shared.Wpf, Version=25.1.0.0, Culture=neutral, PublicKeyToken=36ff2196ab5654b9Ashared4http://schemas.actiprosoftware.com/winfx/xaml/shared    N� Title$  	StartPage��&   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�   TextFormattingMode$ Display=� � UseLayoutRounding .  J� SnapsToDevicePixels . .��� 
���   �.+�(�  (� Stretch$ Uniform=�.��� $	��800��$	��450��
���   � � RowDefinitions �� $��3* ��� $	��43* �.+� � -     �Name$ MainContentFrame��$��2��  � NavigationUIVisibility$ Hidden=�� $	��Top=�$	��120��$��	0,280,0,0q�$��1��	 � ColumnDefinitions	 �� $
��267* ��� $
��267* ��� $
��134* ��� $��4 ��� $	��42* ��� $	��86* �.+�� $��1��.+��� 
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�$��	68,48,0,0q�$��80��.+���    ImageButtonStyle#��[  
������ $	��5,0q��� #��[  
������  � Cursor$
 Hand��$	��5,0q����� *Commands.PlayCommand+.���� -    ��Name$ 
PlayButton��$��97��$��88������ *PlayPauseButton+�� #��[  ���� *Commands.StartMusic+.���� $6��0pack://siteoforigin:,,,/assets/menu/previous.png��$��0,31,188,-20q��� #��[  ���� *Commands.EndMusic+.���� $2��,pack://siteoforigin:,,,/assets/menu/next.png��$��195,56,-6,6q� J� RenderTransformOrigin$ 0.5,0.5&��� -    ��Name$ StartMusicLabel��
�������$��14(�$
��Left=�$	��Top=�$��	51,20,0,0q�$��0������ *CurrentTime+�� -   $ EndMusicLabel��
�������$��14(�$��4��$��
11,20,0,70q�$
��Left=�$��2������ *	EndTime+�� -    ��Name$ MusicTrackBar�� � Maximum$
 1000Q� � Minimum$ 0Q�$��38,10,39,77q�$��6��$��0�� � Value �� *TrackPosition+ �� Mode$ TwoWay=�{��� *GetStatusOnSlider+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ PreviewMouseLeftButtonUp��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *Commands.ChangeMusicTime+�� #��[  
������ $	��5,0q����� *Commands.SelectFavoriteSong+.���� -   $ FavoriteSong��$��664,-18,-430,119q�$��2��$��3������ *FavoriteSong+�� #��[  
������ ���� *Commands.RepeatCommand+.���� $��724,-19,-500,120q�$8��2pack://siteoforigin:,,,/assets/menu/repeatSong.png��$��5���� #��[  
������ $��5,0,62,0q�$��4��$��2������ * Commands.SelectChaoticallySong+.���� $@��:pack://siteoforigin:,,,/assets/menu/songHaoticPlaylist.png��$
��Left=�$��26��$
 Hand��$ -1.834,0.31&�$��27,-18,0,121q��� -   $ 	MusicLogo��$��300,10,300,0q�$��1��$	��164��$	��Top=�$	��200������ *
AlbumArt+ J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  z�V� .~��� ��  �� AngleY$ 0Q��� T�  T� Y$ 0Q��� -   $ NameSong��$��Center=�$��Center=�
�������$��	0,185,0,0q�$��1��$	��Top=�$��14(�$	��800��$ 0.5,0.5&�$��Right=����� *Artist+z�V� .~��� �� $ 0.055Q��� T� $ 0.205Q��� -	   $ 
NameArtist��$��Center=�$��Center=�
�������$��
2,232,-2,0q�$��1��$	��Top=�$ 0.5,0.5&�$��12(����� *Title+z�V� .~��� �� ��  �� Angle$ -0.165Q�T� �� -
   $ NameProgram��$��	TuneForge��$
��Left=�
�������$��35��$��702,-10,0,0q�$	��Top=�$��88��$��16(�$��2���� #��[  
������ $��109,79,0,60q�$
��Left=�$	��509������ *Commands.ToggleAudio+.���� -   $ SoundButton��$��1��$��-77,208,562,80q�$ 0.5,0.5&����� *SoundStatus+z�V� .~��� �� �� $ -0.441Q�T� 