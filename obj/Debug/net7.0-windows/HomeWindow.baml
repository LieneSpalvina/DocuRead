   M S B A M L   `   `   `  ����  DocumentReaderApp(    "DocumentReaderApp.Views.HomeWindow   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$
  Home��5      $
��1024��5      $	��768��5      $	��800��5      $	��600��5	       � WindowStartupLocation$ CenterScreen=�5
      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��5      $	��200��5      $��30��5      $��	0,10,0,10q�5      $��Center=�5      ��5      �� *AvailableLanguages+ �� SelectedItem 5      �� *SelectedLanguage+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5!   
   $��1��5"      $	��200��5#      $��30��5$      $��	0,10,0,10q�5%      $��Center=�5&      $��Browse File��5(      ��5'      �� *BrowseFileCommand+5(      �� 5+   
   $��2��5,      $	��400��5-      $��	0,10,0,10q�5.      $��Center=�5/      $
��Wrap=�51      ��50      �� *SelectedFilePath+$ OneWay=�51      �� 54   
   $��3��55      $	��200��56      $��30��57      $��	0,10,0,10q�58      $��Center=�59      $��Extract Text��5;      ��5:      �� *ExtractTextCommand+5;      �� 5>   
   $��4��5?      $	��600��5@      $	��300��5A      $��	0,10,0,10q�5B      $��Center=�5C       �� AcceptsReturn$
 True��5D      " �� VerticalScrollBarVisibility$
 Auto=�5F      ��5E      �� *ExtractedText+$ TwoWay=�$ PropertyChanged=�5F      �� 5I   
   $��5��5J      $��0,10,0,0q�5K      $��Center=�5L       �� Orientation$ 
Horizontal=�5M      .+�5O      �� $	��150��5P      $��30��5Q      $��5q�5R      $��	Save Text��5T      ��5S      �� *SaveTextCommand+5T      �� 5W      -   5        $	��150��5X      $��30��5Y      $��5q�5Z      $��	Documents��5\      �� 5_      $	��100��5`      $��30��5a      $��5q�5b      $��Log Out��5d      ��5c      �� *SignOutCommand+5d      5e      5g      5h      