   M S B A M L   `   `   `  ����M  IWpfTuneForgePlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:WpfTuneForgePlayer.ViewModelWpfTuneForgePlayer.ViewModel  5      '    !WpfTuneForgePlayer.MusicDirectory   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @	ViewModel*clr-namespace:WpfTuneForgePlayer.ViewModel     5        N� 
Background
  �   �&   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5	         TextFormattingMode$ Display=�5
       � UseLayoutRounding .  J� SnapsToDevicePixels . .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   
���   � �� CornerRadius$ 6��61    �� Padding$ 8q�6B   $��0,0,0,10q�6N   ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� -   5        $��0��5      $��32��5      $��32��6$   
������ $��0q�5!      	 � Cursor$
	 Hand��5"      .��5$      �� $6��0pack://siteoforigin:,,,/assets/sidebar/exit2.png��6   $��Uniform=�6V   5%      �� 5)      $��1��6   $��	TuneForge��5*      
�������$��20(�5,      $��Center=�5-      $��Center=�5.      �� 51      $��2��6   
 �� Orientation$
 
Horizontal=�52      $��Center=�53      .+�55      �� $��28��6   $��28��6(   
������ $��0q�57      $
	 Hand��58      ��6'   �� *Commands.ReloadMusicPage+.��59      �� $7��1pack://siteoforigin:,,,/assets/menu/newrepeat.png��6    $��Uniform=�6[   $��0,0,5,0q�6m   5:      5;      �� 5<      $��Choose folder��6    
�������$��Center=�5>       �� 5?      -   5        $��28��5?      $��28��6(   
������ $��0q�5A      $
	 Hand��5B      .��5C      �� $:��4pack://siteoforigin:,,,/assets/menu/folderchoose.png��6    $��Uniform=�6^   5D      5E      5F      5G      �� 5J   
   -   5         ��Name$ InfoInDirectory��5J      $��1��5K      $'��!Please choose a folder with music��5L      
�������$��18(�5N      $��SemiBold"�5O      $��Center=�5P      $��Center=�5Q       �� TextAlignment$ Center=�5R      $y�	Collapsed=�5S      $��0,0,60,4q�6+   �� 5V   
   $��1��6   $
��Auto=�5W      .��5X      �� ��6   �� *Songs+��5Y      �� 5Z      .R�5[      � 5\      ��5_      �� 5`      .R�5a      �� $	��160��6!   $	��240��6-   ���#1F1F22$ 8��6O   $��4q�6`   $
	 Hand��6k   z�5b      �� 5c   "    �� ScaleX$ 1Q�61    �� ScaleY$ 1Q�6<    J� RenderTransformOrigin$ 0.5,0.5&�5e   a   ��5g      �� 5h   "    �� 
TargetType" ��6(    �� Triggers 5i   &   P� 5j   *    P� Property �@J�IsMouseOver P� Value .  N� EnterActions 5k   .   �� 5l   2   .��5m   6   �� .��5n   :   W�  �� TargetProperty$- 'RenderTransform.(ScaleTransform.ScaleX)�6J   	 W� To$
 1.05X�5o   6    g� Duration$ 0:0:0.15D�6@   W� 5p   :   $- 'RenderTransform.(ScaleTransform.ScaleY)�6J   $
 1.05X�5q   6   $ 0:0:0.15D�6@   5r   7   5s   3    N� ExitActions 5u   .   �� 5v   2   .��5w   6   �� .��5x   :   W� $- 'RenderTransform.(ScaleTransform.ScaleX)�6J   $ 1X�5y   6   $ 0:0:0.15D�6=   W� 5z   :   $- 'RenderTransform.(ScaleTransform.ScaleY)�6J   $ 1X�5{   6   $ 0:0:0.15D�6=   5|   7   5}   3   5   +   5�   #   ._�5�      �� .+�5�   "   �� $	��140��6D   $��Uniform=�6Q   $��0,0,0,4q�6c   ��6(   �� *
AlbumArt+6c   �� 5�   "   $	��4,0q�6,   ���#44469   �� 5�   "   ���#DDD$��SemiBold"�6V   $��Center=�5�      $��6,4,6,0q�65   $��CharacterEllipsis=�6F   ��5�   ,   �� *Artist+5�   F   �� 5�   "   
�������$
��Wrap=�6V   $ Center=�5�      $��13(�6/   $��6,0,6,0q�6=   ��5�   ,   �� *Title+5�   =   �� 5�   "   ���#888$��12(�6X   $��Center=�5�      $��0,6,0,0q�65   ��5�   ,   �� *
Duration+5�   5   5�       J� InputBindings 5�      d� 5�   "    d� Gesture$ 	LeftClicka�6/    �� Command 5�      �� *.,DataContext.Commands.PlaySelectedSongCommand+ �� RelativeSource ��  �� AncestorType$
 PageL� �� CommandParameter 5�      �� 5�      5�      5�      5�      5�      5�      