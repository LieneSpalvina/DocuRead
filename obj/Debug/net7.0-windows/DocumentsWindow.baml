   M S B A M L   `   `   `  ����  DocumentReaderApp-    'DocumentReaderApp.Views.DocumentsWindow   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  Saved Documents��5      $
��1024��5      $	��768��5      $	��800��5      $	��600��5	       � WindowStartupLocation$ CenterScreen=�5
      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0,0,0,10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6)   .+�5      �� $	��200��5       $��0,0,10,0q�5!      ��5"      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5$      $	��150��5%      $��0,0,10,0q�5&      ��5'      �� *AvailableLanguages+ �� SelectedItem 5(      �� *SelectedLanguage+$ PropertyChanged=��� 5*      $��Clear��6A   ��6   �� *ClearFiltersCommand+6A   5+      �� 5.      $��1��6   $��20,0,0,0q�6)   .+�5/      �� $��16(�50      $
��Bold"�51      $��📊 Summary��52      �� 53      ��6   �� *TotalDocuments+ �� StringFormat$ Total Documents: {0}���� 54      ��6   �� *MostRecentScan+$ Most Recent Scan: {0}���� 56      ��6   �� *LanguageCounts+��57      �� 58      .R�59      �� �� 5:   "   �6&   ��  �� Path$	 Key�	 �� Mode$	 OneWay=� �� 5;   "   $�: ��6&    �� 5<   "   �6&   �� $ Value�$	 OneWay=�5=      5>      5@      5A      5B      &   System.Windows.Controls.DataGrid  5E   
   $��1��5F      
   AutoGenerateColumns$
 False��5G         CanUserAddRows$ False��5H         CanUserDeleteRows$ False��5I         
IsReadOnly$
 True��5J      ��5K      �� *	Documents+   Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 5N         Header$ ID��5P      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5O      �� *Id+5P        5Q      $ * 5R      $ 	File Name��5T       5S      �� *
FileName+5T        5U      $	 100 5V      $ Language��5X       5W      �� *
Language+5X        5Y      $	 150 5Z      $ Date Extracted��5\       5[      �� *ExtractionDate+5\      4  .System.Windows.Controls.DataGridTemplateColumn  5^      $	 100 6)   $ Original File��65      CellTemplate 5_      �� 5`      .R�5a      �� -   5        $
��Open��5a   @   5b      5d        5f      $	 100 6)   $ 	Text File��65    5g      �� 5h      .R�5i      �� -   5        $
��Open��5i   <   5j      5l        5n      $	 100 6)   $ OCR JSON��65    5o      �� 5p      .R�5q      �� -   5        $
��Open��5q   <   5r      5t        5u      $	 100 6)   $ Delete��65    5v      �� 5w      .R�5x      �� $��Delete��5{   !   ��5y   !   �� *#!DataContext.DeleteDocumentCommand+ �� RelativeSource ��  �� AncestorType
 Window��5z   !   �� 5{   !   5|      5~      5�      �� 5�   
   $��2��5�      $��0,10,0,0q�5�      $��Right=�5�      $ 
Horizontal=�5�      .+�5�      �� $	��100��5�      $��30��5�      $��0,0,10,0q�5�      $
��Back��5�      $��14(�5�      ��5�      �� *BackCommand+5�      �� 5�      $	��100��5�      $��30��5�      $��Log out��5�      $��14(�5�      ��5�      �� *LogOutCommand+5�      5�      5�      5�      