   M S B A M L   `   `   `  ����D  @GaussLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:GaussLink.Views.ItemExplorerGaussLink.Views.ItemExplorer  5   	   I)clr-namespace:GaussLink.Views.MainDisplayGaussLink.Views.MainDisplay  5   	   E'clr-namespace:GaussLink.Views.InspectorGaussLink.Views.Inspector  5   	   [2clr-namespace:GaussLink.ViewModels.WindowViewModel$GaussLink.ViewModels.WindowViewModel  5	   	       GaussLink.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :exp*clr-namespace:GaussLink.Views.ItemExplorer     5   	   =display)clr-namespace:GaussLink.Views.MainDisplay     5   	   8insp'clr-namespace:GaussLink.Views.Inspector     5   	   Dlocal2clr-namespace:GaussLink.ViewModels.WindowViewModel     5	   	     � Title$  	GaussLink��5   	   $	��450��6   $	��800��6(      CustomWindowStyle#���   5   	    � DataContext 5      :   4GaussLink.ViewModels.WindowViewModel.WindowViewModel  5   
    J� InputBindings 5      �� 5   
   
 �� Key$ O��6    �� 	Modifiers$
 Ctrlf�6    �� Command 6.   �� *OpenFileCommand+�� 5   
   $ S��6   $
 Ctrlf�6    6.   �� *SaveSelectedCommand+�� 5   
   $ S��6   $ 
Ctrl+Shiftf�6    65   �� *SaveAllCommand+ � 	Resources 5        NormalShadow F� ShadowDepth F� Softness	 F� Opacity��%D   &	       F� 5   
   $ 2Q�66   $	 0.2Q�6F   $
	 0.64Q�6U   5      .��5      � 
 � RowDefinitions
 5   
   �� 5      $��1* �6   �� 5      $��5 �6   �� 5      $	��16* �6   .+�5   
   � $��0��6   .+�5      o� $��Center=�6   .@�5       m� $
��File��6   .@�5!      m� $
��Open��6    m� InputGestureText$ Ctrl+O��6Q     SingleDropDownMenuItem#���  6k    m� Command 6-   �� *OpenFileCommand+6k   �� 5"      m� 5#      $��Save Selected��6   $ Ctrl+S��66   #���  6P   m� 5$      $��Save All��6   $ Ctrl+Shift+S��61   #���  6Q   5%      m� 5&      $��Settings��6   .@�5'      m� $��Avaliable Themes Below��6   #���  6?   m� 5(      -   5        $��Light��5(      #���  6.   
 J� Uid$ 0��6v    m� Icon 5)      �� 5*      ���	#FF848484���	#FFE8E8E8$��2��6L    �� StrokeLineJoin$ Round=�6`    J� BitmapEffect# [ 6w   5,      m� 5-      -   5        $��Colourful Light��5-      #���  68   $ 1��6�    5.      �� 5/      ���	#FF2C7FF5���	#FFE8E8E8$��2��6L   $ Round=�6`   # [ 6w   51      m� 52      -   5        $
��Dark��52      #���  6-   $ 2��6u    53      �� 54      ���	#FF404040���	#FF1B1B1B$��2��6L   $ Round=�6`   # [ 6w   56      m� 57      -   5        $��Colourful Dark��57      #���  67   $ 3��6    58      �� 59      ���	#FF2C7FF5���	#FF1B1B1B$��2��6L   $ Round=�6`   # [ 6w   5;      5<      5=      5>      �� 5?   
   $��1��6   $��0,0,0,5q�6    
 Simple#���  68   � 5@   
   $��2��6   .+�5A      �  � ColumnDefinitions 5B      �� 5C      $��3* �6'   $��50��62   �� 5D      $��5 �6'   �� 5E      $��8* �6'   $	��100��62   �� 5F      $��5 �6'   �� 5G      $��3* �6'   $��50��62   .+�5I      � $��0��6   .+�5J      � -   5        $��ExplorerParentPanel��5J      
 5K      �� 5L      $
��auto �6,   �� 5M      $��* �6,   .+�5O      �� $��0��6    $��Item Explorer��6-   $
��Bold"�6E   $��Center=�6W   3   -GaussLink.Views.ItemExplorer.ItemExplorerView  5P      $��1��6/   5Q      5R      �� 5S      $��1��6   $��Stretch=�6/   � 5T      $��2��6   .+�5U      -   'GaussLink.Views.MainDisplay.DisplayView  5V      �� 5W      $��3��6   $��Stretch=�6/   � 5X      $��4��6   
 5Y      �� 5Z      $
��auto �6(   �� 5[      $��* �6(   .+�5]      �� $��0��6   $��	Inspector��6)   $
��Bold"�6=   $��Center=�6O   -   'GaussLink.Views.Inspector.InspectorView  5^      $��1��6)   5_      5`      5a      5d      5e      