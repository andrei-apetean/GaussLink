   M S B A M L   `   `   `  ���� D  @GaussLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:GaussLink.Views.ItemExplorerGaussLink.Views.ItemExplorer  I)clr-namespace:GaussLink.Views.MainDisplayGaussLink.Views.MainDisplay  E'clr-namespace:GaussLink.Views.InspectorGaussLink.Views.Inspector  [2clr-namespace:GaussLink.ViewModels.WindowViewModel$GaussLink.ViewModels.WindowViewModel      GaussLink.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :exp*clr-namespace:GaussLink.Views.ItemExplorer     =display)clr-namespace:GaussLink.Views.MainDisplay     8insp'clr-namespace:GaussLink.Views.Inspector     Dlocal2clr-namespace:GaussLink.ViewModels.WindowViewModel       � Title$  	GaussLink��$	��450��$	��800��   CustomWindowStyle#���    � DataContext :   4GaussLink.ViewModels.WindowViewModel.WindowViewModel   J� InputBindings �� 
 �� Key$ O�� �� 	Modifiers$
 Ctrlf� �� Command �� *OpenFileCommand+�� $ S��$
 Ctrlf� �� *SaveSelectedCommand+�� $ S��$ 
Ctrl+Shiftf� �� *SaveAllCommand+ � 	Resources   NormalShadow F� ShadowDepth F� Softness	 F� Opacity��%,   &	       F� $ 2Q�$	 0.2Q�$
	 0.64Q�.��� 
 � RowDefinitions
 �� $��1* ��� $��5 ��� $	��19* �.+�� $��0��.+�o� $��Center=�.@�m� $
��File��.@�m� $
��Open�� m� InputGestureText$ Ctrl+O��  SingleDropDownMenuItem#���   m� Command �� *OpenFileCommand+�� m� $��Save Selected��$ Ctrl+S��#���  m� $��Save All��$ Ctrl+Shift+S��#���  m� $��Settings��.@�m� $��Avaliable Themes Below��#���  m� -   $��Light��#���  
 J� Uid$ 0�� m� Icon �� ���	#FF848484���	#FFE8E8E8$��2�� �� StrokeLineJoin$ Round=� J� BitmapEffect# [ m� -   $��Colourful Light��#���  $ 1�� �� ���	#FF2C7FF5���	#FFE8E8E8$��2��$ Round=�# [ m� -   $
��Dark��#���  $ 2�� �� ���	#FF404040���	#FF1B1B1B$��2��$ Round=�# [ m� -   $��Colourful Dark��#���  $ 3�� �� ���	#FF2C7FF5���	#FF1B1B1B$��2��$ Round=�# [ �� $��1��$��0,0,0,5q� 
 Simple#���  � $��2��.+��  � ColumnDefinitions �� $��3* �$��50���� $��5 ��� $��8* �$	��100���� $��5 ��� $��3* �$��50��.+�� $��0��.+�� -   $��ExplorerParentPanel��
 �� $
��auto ��� $��* �.+��� $��0��$��Item Explorer��$
��Bold"�$��Center=�3   -GaussLink.Views.ItemExplorer.ItemExplorerView  $��1���� $��1��$��Stretch=�� $��2��.+�-   'GaussLink.Views.MainDisplay.DisplayView  �� $��3��$��Stretch=�� $��4��
 �� $
��auto ��� $��* �.+��� $��0��$��	Inspector��$
��Bold"�$��Center=�-   'GaussLink.Views.Inspector.InspectorView  $��1��