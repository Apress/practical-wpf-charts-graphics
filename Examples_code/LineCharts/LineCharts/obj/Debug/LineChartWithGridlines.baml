   M S B A M L   `   `   `  ����  
LineCharts'    !LineCharts.LineChartWithGridlines   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5        � Title$*  $Line Chart with Gridlines and Labels��5      $	��400��62   $	��400��6?   .��5      � -   5        $��grid1��5      $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      -   5         �Name$ column1��5	      $��* �6.    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      -   5        $
 row1��5      $��* �6(   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ tbTitle��5      $��2q�6   $��1��60   $��0��6@    J� RenderTransformOrigin$ 0.5,0.5&�5      $��14(�61   $
��Bold"�6?   $��Stretch=�5      $��Stretch=�6/    �� TextAlignment$ Center=�6K   $��Title��5      �� 5   
   -   5        $ tbXLabel��5      $��2q�6   $��1��61   $��2��6A   $ 0.5,0.5&�5      $ Center=�60   $��X Axis��5      �� 5   
   -   5        $��tbYLabel��5      $��2q�6   $��0��6/   $��1��6?   $ 0.5,0.5&�5      $ Center=�61   $��Y Axis��5       � LayoutTransform 5      �� 5       �� Angle$	 -90Q�6&   5      � 5!   
   -   5        	 �Name$	 	chartGrid��5!   !   $��0,0,0,0q�6   $��1��65   $��1��6E   }�.  
������ 5#      �� 5$   
   -   5        $��
textCanvas��5$      $��2q�6   $��1��6.   $��1��6>   }�. .+�5%      �� -	   5        $��chartCanvas��5%      }�. 6(   5&      5'      5(      