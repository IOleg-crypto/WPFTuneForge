   M S B A M L   `   `   `  ����  WpfTuneForgePlayer'    !WpfTuneForgePlayer.MusicDirectory   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� 
Background
  �   �&   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�5         TextFormattingMode$ Display=�5       � UseLayoutRounding .  J� SnapsToDevicePixels . .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ���#1A1A1A �� CornerRadius$ 6��63    �� Padding$ 8q�6D   $��0,0,0,10q�6P   ._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� -   5        $��0��5      $��32��5      $��32��6$   
������ $��0q�5      	 � Cursor$
	 Hand��5      .��5      �� $6��0pack://siteoforigin:,,,/assets/sidebar/exit2.png��6   $��Uniform=�6V   5       �� 5"      $��1��6   $��	TuneForge��5#      
�������$��20(�5%      $��Center=�5&      $��Center=�5'      �� 5*      $��2��6   
 �� Orientation$
 
Horizontal=�5+      $��Center=�5,      .+�5-      �� $��Choose folder��6    
�������$��Center=�5/       �� 50      -   5        $��28��50      $��28��6(   
������ $��0q�52      $
	 Hand��53      .��54      �� $:��4pack://siteoforigin:,,,/assets/menu/folderchoose.png��6    $��Uniform=�6^   55      56      57      58      �� 5;   
   -   5         ��Name$ InfoInDirectory��5;      $��1��5<      $'��!Please choose a folder with music��5=      
�������$��18(�5?      $��SemiBold"�5@      $��Center=�5A      $��Center=�5B       �� TextAlignment$ Center=�5C      $y�	Collapsed=�5D      $��0,0,60,4q�6+   �� 5G   
   $��1��6   $
��Auto=�5H      .��5I      �� ��6   �� *Songs+��5J      �� 5K      .R�5L      � 5M      ��5P      �� 5Q      .R�5R      �� $	��160��6!   $	��240��6-   ���#222$ 8��6L   $��4q�6]   $
	 Hand��6h   z�5S      �� 5T   "    �� ScaleX$ 1Q�61    �� ScaleY$ 1Q�6<    J� RenderTransformOrigin$ 0.5,0.5&�5V   a   ��5X      �� 5Y   "    �� 
TargetType" ��6(    �� Triggers 5Z   &   P� 5[   *    P� Property �@J�IsMouseOver P� Value .  N� EnterActions 5\   .   �� 5]   2   .��5^   6   �� .��5_   :   W�  �� TargetProperty$- 'RenderTransform.(ScaleTransform.ScaleX)�6J   	 W� To$
 1.05X�5`   6    g� Duration$ 0:0:0.15D�6@   W� 5a   :   $- 'RenderTransform.(ScaleTransform.ScaleY)�6J   $
 1.05X�5b   6   $ 0:0:0.15D�6@   5c   7   5d   3    N� ExitActions 5f   .   �� 5g   2   .��5h   6   �� .��5i   :   W� $- 'RenderTransform.(ScaleTransform.ScaleX)�6J   $ 1X�5j   6   $ 0:0:0.15D�6=   W� 5k   :   $- 'RenderTransform.(ScaleTransform.ScaleY)�6J   $ 1X�5l   6   $ 0:0:0.15D�6=   5m   7   5n   3   5p   +   5r   #   ._�5v      �� .+�5w   "   �� $	��140��6D   $��Uniform=�6Q   $��0,0,0,4q�6c   ��6(   �� *
AlbumArt+6c   �� 5x   "   $	��4,0q�6,   ���#44469   �� 5y   "   ���#DDD$��SemiBold"�6V   $��Center=�5z      $��6,4,6,0q�65   $��CharacterEllipsis=�6F   ��5y   ,   �� *Artist+5z   F   �� 5{   "   
�������$
��Wrap=�6V   $ Center=�5|      $��13(�6/   $��6,0,6,0q�6=   ��5{   ,   �� *Title+5|   =   �� 5}   "   ���#888$��12(�6X   $��Center=�5~      $��0,6,0,0q�65   ��5}   ,   �� *
Duration+5~   5   5       J� InputBindings 5�      d� 5�   "    d� Gesture$ 	LeftClicka�6/    �� Command 5�      �� *%#DataContext.PlaySelectedSongCommand+ �� RelativeSource ��  �� AncestorType$
 PageL� �� CommandParameter 5�      �� 5�      5�      5�      5�      5�      5�      