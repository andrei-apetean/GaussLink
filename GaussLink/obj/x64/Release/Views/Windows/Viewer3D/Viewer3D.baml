   M S B A M L   `   `   `  ���� D  @GaussLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:GaussLink.Views.WindowsGaussLink.Views.Windows  &     GaussLink.Views.Windows.Viewer3D   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:GaussLink.Views.Windows       � Title$  GaussLink - 3D Viewer��$	��500��$	��800��   CustomWindowStyle#���   .��� .+�� -   $��animGrid��.+�A� $	��155��$	��155��$��
-150,0,0,0q�  ControlDefaultForeground#���   �� StrokeDashArray$	 0.4R�$��Center=�$��Center=�$��8�� J� RenderTransformOrigin$ 0.5,0.5&�z���  � Triggers :�  :� RoutedEvent$ Loaded��.[��� .���� .��W�  �� TargetProperty$7 1(Ellipse.RenderTransform).(RotateTransform.Angle)� W� From$ 0X�	 W� To$
 -360X� g� Duration$ 0:0:2D�	 g� RepeatBehavior$	 Forever��A� $	��140��$	��140��$��
-150,0,0,0q�#���  $��Center=�$��Center=�$��5��$ 0.5,0.5&�A� $	��120��$	��120��$��
-150,0,0,0q�#���  $	 0.2R�$��Center=�$��Center=�$��8��$ 0.5,0.5&�z���  :� $ Loaded��.[��� .���� .��W� $7 1(Ellipse.RenderTransform).(RotateTransform.Angle)�$
 -360X�$ 0X�$ 0:0:2D�$	 Forever��� #���  $��2��
 � Points
 8�  8� X$	 400Q� 8� Y$	 200Q�8� $	 450Q�$	 150Q�8� $	 600Q�$	 150Q��� $��Creating molecule...��$	��Top=�$��Right=�$��180,150q�$��16(� :� $ Loaded��.[��� .���� .��W� $ Opacity�$ 1X�$
 0.25X�$ 0:0:0.25D� g� AutoReverse . � -   $��
parentGrid�� � ColumnDefinitions �� $��4* ��� $��5 ��� $��1* �.+�� -   $��	viewPanel��$��0��.+��� -   $��overlay��.+��� -    ��Name$ panelToggleBtn��$��20��$��20��$��Stretch=�
 �� Top$ 5�� �� Right$ 5��.���� -   $��tglText��$	��❯��$��20��$��Center=�'   !GaussLink.Views.Windows.Overlay3D  -     Name$ 	overlay3D��$	��100��$	��100�� �� Left$ 5��$ 5���� $��1��$��Stretch=�� -   $��propertiesPanel��$��2��.+��� $��Disabled=�$
��Auto=�.���� .+�9� $��
Properties�� 9� 
IsExpanded . .���� $��10,0,0,0q�.+��� -	   $
��atm1��$��No selection���� -
   $
��atm2��$��No selection���� -   $	��dst��$	��N/A��9� -   $��animationExpander��$��	Animation�� . .���� $��10,0,0,0q�.+��� -   $��vmLbl��$��Vibration Mode:��$��5,5,5,2q�� $��5,0,5,5q�.+�� $��20�� �� $��* ��� $��20 �.+��� -   $��
vibeModeTB��$��20��$��0��$��Bottom=�$��1��� $��1�� � RowDefinitions �� $��10 ��� $��10 �.+��� -   $��incrementBtn��$��0��$��1q�.���� $��˄��$��10(�$��Center=�$��Center=��� -   $��decrementBtn��$��1��$��1q�.���� $��˅��$��10(�$��Center=�$��Center=��� -   $ 
animButton��$��5q�$��Play Animation���� -   $��animSpeedLbl��$��Animation Speed: 1���� -    ��Name$ animSpeedSlider��$��5q� � Value$ 1Q� � Minimum$ 0Q� � Maximum$ 2Q� � SmallChange$	 0.1Q� � LargeChange$
 0.25Q� �� TickFrequency$	 0.1Q� �� IsSnapToTickEnabled . �� $��0,0,0,2q� 
 Simple#���  9� $��Viewport�� . .���� $��10,0,0,0q�.+��� $��Background Color:���� -   $ bgSlider��$��5q�$ 0Q�$	 255Q���  �� Orientation$ 
Horizontal=�$��5q�.+��� -   $��
gizmoCheck��$��20��$��20��.���� -   $��gizmoChkText��$	��✓���� $	��5,0q�$��Show Transform Gizmo���� $ 
Horizontal=�$��5q�.+��� -   $��	axisCheck��$��20��$��20��.���� -   $��axisChkText��$�� ���� $	��5,0q�$��Show Carthesian Axis��