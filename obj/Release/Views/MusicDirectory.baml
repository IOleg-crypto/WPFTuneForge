   M S B A M L   `   `   `  ���� M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  '    !WpfTuneForgePlayer.MusicDirectory   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @	ViewModel*clr-namespace:WpfTuneForgePlayer.ViewModel       N� 
Background
  �   �&   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�   TextFormattingMode$ Display=� � UseLayoutRounding .  J� SnapsToDevicePixels . .���  � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��
���   � �� CornerRadius$ 6�� �� Padding$ 8q�$��0,0,0,10q�._��  � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -   $��0��$��32��$��32��
������ $��0q�	 � Cursor$
	 Hand��.���� $6��0pack://siteoforigin:,,,/assets/sidebar/exit2.png��$��Uniform=��� $��1��$��	TuneForge��
�������$��20(�$��Center=�$��Center=��� $��2��
 �� Orientation$
 
Horizontal=�$��Center=�.+��� $��28��$��28��
������ $��0q�$
	 Hand������ *Commands.ReloadMusicPage+.���� $7��1pack://siteoforigin:,,,/assets/menu/newrepeat.png��$��Uniform=�$��0,0,5,0q��� $��Choose folder��
�������$��Center=��� -   $��28��$��28��
������ $��0q�$
	 Hand��.���� $:��4pack://siteoforigin:,,,/assets/menu/folderchoose.png��$��Uniform=��� -    ��Name$ InfoInDirectory��$��1��$'��!Please choose a folder with music��
�������$��18(�$��SemiBold"�$��Center=�$��Center=� �� TextAlignment$ Center=�$y�	Collapsed=�$��0,0,60,4q��� $��1��$
��Auto=�.���� ���� *Songs+���� .R�� ���� .R��� $	��160��$	��240�����#1F1F22$ 8��$��4q�$
	 Hand��z���  �� ScaleX$ 1Q� �� ScaleY$ 1Q� J� RenderTransformOrigin$ 0.5,0.5&�����  �� 
TargetType" �� �� Triggers P�  P� Property �@J�IsMouseOver P� Value .  N� EnterActions �� .���� .��W�  �� TargetProperty$- 'RenderTransform.(ScaleTransform.ScaleX)�	 W� To$
 1.05X� g� Duration$ 0:0:0.15D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)�$
 1.05X�$ 0:0:0.15D� N� ExitActions �� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)�$ 1X�$ 0:0:0.15D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)�$ 1X�$ 0:0:0.15D�._��� .+��� $	��140��$��Uniform=�$��0,0,0,4q����� *
AlbumArt+�� $	��4,0q����#444�� ���#DDD$��SemiBold"�$��Center=�$��6,4,6,0q�$��CharacterEllipsis=����� *Artist+�� 
�������$
��Wrap=�$ Center=�$��13(�$��6,0,6,0q����� *Title+�� ���#888$��12(�$��Center=�$��0,6,0,0q����� *
Duration+ J� InputBindings d�  d� Gesture$ 	LeftClicka� �� Command �� *.,DataContext.Commands.PlaySelectedSongCommand+ �� RelativeSource ��  �� AncestorType$
 PageL� �� CommandParameter �� 