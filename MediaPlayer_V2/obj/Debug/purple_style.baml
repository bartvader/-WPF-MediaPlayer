   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� Property �� Value �� EndPoint � MappingMode �� 
StartPoint � Color � Offset �� 
TargetType	 � 	Resources   Key
 �� TargetProperty �� 
TargetName8   2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value ��Name J� OpacityMask �� CornerRadius �� Triggers :� RoutedEvent  OnMouseLeave1 ��Name  OnMouseEnter1 J� SnapsToDevicePixels � RowDefinitions m�Name m� Fill m� 	Placement % !HorizontalSliderTrackNormalBorder ) %HorizontalSliderTrackNormalBackground ��Name ]�Name ]� DecreaseRepeatButton �� DecreaseLarge  SliderRepeatButtonStyle ]� IncreaseRepeatButton �� IncreaseLarge ]� Thumb  n�Name  HorizontalSliderThumbStyle! P� Property" P� Value# �� 
TargetName   HorizontalSliderUpThumbStyle " HorizontalSliderDownThumbStyle$ �� Triggers% � ColumnDefinitions #	 VerticalSliderTrackNormalBorder 
 VerticalSliderThumbStyle   VerticalSliderLeftThumbStyle ! VerticalSliderRightThumbStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�  '��       '��      7  7  '�� �    7  7  7  7  7  7  7  7 	 7  7  7  7 
 7  7  �� 5      "  ��6	   .�5      ��  � �� 5      �� 5      $ 0.5,1&�6   $ RelativeToBoundingBox=�6+   $ 0.5,0&�6O   .��5      � $ 	#FF020602��6   � 5	      $ 	#FF2F0A41��6   $ 1Q�6&   5
      5      5      �� 5      "  ��6	   .�5      ��  � �� 5      �� 5      " ��6   	 5      ���� 5      OnMouseLeave1  .��5   	   �� $N
 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6&   $ border��6�   .g�5   
      $ 0��6   $ 	#FFD38AE9��6*      5   
   $ 0:0:1��6   $ 	#FF9A24BE��6.   5   
   5   	   �� 5      OnMouseEnter1  .��5   	   �� $N
 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6&   $ border��6�   .g�5   
      $ 0��6   $ 	#FF9A24BE��6*      5   
   $ 0:0:1��6   $ 	#FFD38AE9��6.   5   
   5   	   5      .R�5       �� $ border��6    �	#FF6E3535$ 10��66   
�������$��2q�6\   ��5!      �� 5"   	   $ 0.5,1&�6   $ 0.5,0&�6.   .��5#   
   � $ Black��6   $ 0Q�6%   � 5$   
   $ 	#FF9A24BE��6   $ 1Q�6)   5%   
   ._�5'      �� #��z��6   $��Center=�63   $��Center=�6P   
�������6k   5(       5)      :� 5*      $ Mouse.MouseLeave��6   .[�5+   	   �� 8��  6   5,   	   :� 5-      $ Mouse.MouseEnter��6   .[�5.   	   �� $# OnMouseEnter1_BeginStoryboard��6   8�� 6@   5/   	   51      53      54      �� 55      "  ��6	   .�56      ��  �@��IsPressAndHoldEnabled .  63   �� 57       � ��
 ���� 6!   �� 58       � �� �	#FFC4C4C46!   �� 59       � �� 5:      �� 5;      " ��6   .R�5<      �� #��z��6   #��z��6:   #��z��6n    . ._�5=      �  5>   	   �� 5?   
   $
��Auto �6   �� 5@   
   $
��Auto �6   #��z��6&   �� 5A   
   $
��Auto �6   .+�5C   	   m� $ TopTick��6   # z��6"   $��4��6F   $	 Top=�6Q   $��0��6a   $y�	Collapsed=�6n   m� 5D   	   $ 
BottomTick��6   # z��6%   $��4��6I   $ Bottom=�6T   $��2��6g   $y�	Collapsed=�6t   �� 5E   	   $ TrackBackground��6   8��  $��1q�6j   8�� $ 1��6�   $	��4.0��6�   $	��5,0q�6�   $��1��6�   $��center=�6�   ._�5F   
   �� $��-6,-1q�6   .+�5G      �� $ PART_SelectionRange��6   #��� ��62   $	��4.0��6u   #��� ��6�   $	��1.0��6�   $y�Hidden=�6�   5H      5I   
   ]� 5J   	   $ 
PART_Track��6   $��1��6#    5K   
   �� 5L      #��Z 6   8�� 6B    5N   
   �� 5O      #��Z 6   8�� 
���  ��6s    5Q   
   n� 5R      $  Thumb��6   8�� 6    5T   
   5U   	   5V       5W      P� 5X      ! �@��TickPlacement$" TopLeft=�6)   . �5Y   	   �� $# TopTick��6&    � y�$ Visible=�6;   �� 5Z   	   $# Thumb��6!   8   � ��64   �� 5[   	   $# TrackBackground��6"    � ��$ 5,2,5,0q�6?   5\   	   P� 5]      ! �@��TickPlacement$" BottomRight=�6)   . �5^   	   �� $# 
BottomTick��6&    � y�$ Visible=�6>   �� 5_   	   $# Thumb��6!   8   � ��64   �� 5`   	   $# TrackBackground��6"    � ��$ 5,0,5,2q�6?   5a   	   P� 5b      ! �@��TickPlacement$
" Both=�6)   . �5c   	   �� $# TopTick��6&    � y�$ Visible=�6;   �� 5d   	   $# 
BottomTick��6&    � y�$ Visible=�6>   5e   	   P� 5f      ! �@��IsSelectionRangeEnabled" . . �5g   	   �� $# PART_SelectionRange��6&    � y�$ Visible=�6G   5h   	   P� 5i      ! �@J�IsKeyboardFocused" . . �5j   	   �� $# Thumb��6&    � ��
 ��  �69   5k   	   5m      5o      $ 5p      P� 5q      ! �@��Orientation$" Vertical=�6$   . �5r      ��  � �� 5s      �� 5t      " ��6   .R�5u   	   �� #��z��6   #��z��6<   #��z��6p    . ._�5v   
   � % 5w      �� 5x      $
��Auto �6   �� 5y      #��z��6   $
��Auto �6C   �� 5z      $
��Auto �6   .+�5|      m� $ TopTick��6   $��0��6$   # z��64   $
 Left=�6X   $y�	Collapsed=�6i   $��4��6�   m� 5}      $ 
BottomTick��6   $��2��6'   # z��67   $ Right=�6[   $y�	Collapsed=�6m   $��4��6�   �� 5~      $ TrackBackground��6   8�� $��1q�6j   8�� $��1��6�   $ 1��6�   $��center=�6�   $	��0,5q�6   $	��4.0��6  ._�5      �� $��-1,-6q�6   .+�5�      �� $ PART_SelectionRange��6   #��� ��64   #��� ��6w   $	��1.0��6�   $y�Hidden=�6�   $	��4.0��6�   5�      5�      ]� 5�      $ 
PART_Track��6   $��1��6%    5�      �� 5�      #��Z 6   8��	 6D    5�      �� 5�      #��Z 6   8��
 6D    5�      n� 5�      $  Thumb��6   8�� 6"   5�      5�      5�   
    5�   	   P� 5�   
   ! �@��TickPlacement$" TopLeft=�6+   . �5�      �� $# TopTick��6(    � y�$ Visible=�6=   �� 5�      $# Thumb��6#   8   � ��66   �� 5�      $# TrackBackground��6$    � ��$ 2,5,0,5q�6A   5�      P� 5�   
   ! �@��TickPlacement$" BottomRight=�6+   . �5�      �� $# 
BottomTick��6(    � y�$ Visible=�6@   �� 5�      $# Thumb��6#   8   � ��66   �� 5�      $# TrackBackground��6$    � ��$ 0,5,2,5q�6A   5�      P� 5�   
   ! �@��TickPlacement$
" Both=�6+   . �5�      �� $# TopTick��6(    � y�$ Visible=�6=   �� 5�      $# 
BottomTick��6(    � y�$ Visible=�6@   5�      P� 5�   
   ! �@��IsSelectionRangeEnabled" . . �5�      �� $# PART_SelectionRange��6(    � y�$ Visible=�6I   5�      P� 5�   
   ! �@J�IsKeyboardFocused" . . �5�      �� $# Thumb��6(    � ��
 ��  �6;   5�      5�   	   5�      5�      5�      5�      