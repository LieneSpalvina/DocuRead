   M S B A M L   `   `   `  ����  DocumentReaderApp)    #DocumentReaderApp.Views.LoginWindow   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  Login��5      $
��1024��5      $	��768��5      $	��800��5      $	��600��5	       � WindowStartupLocation$ CenterScreen=�5
      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��5      $	��100��5      $��30��5      $��10q�5      $
��Left=�5      $	��Top=�5       $
��Back��5"      $��14(�5#      ��5!      �� *BackCommand+5#      �� 5&   
   $��1��5'      $��	0,30,0,10q�5(      $��Center=�5)      .+�5*      �� $��0,0,0,5q�5+      $��18(�5,      $��	Username:��5-       �� TextAlignment$ Center=�5.      �� 5/      -   5        $��UsernameTextBox��50      $	��250��51      $��30��52      ��53      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�54      �� 57   
   $��2��58      $��	0,10,0,10q�59      $��Center=�5:      .+�5;      �� $��0,0,0,5q�5<      $��18(�5=      $��	Password:��5>      $ Center=�5?      G� 5@      -   5        $��PasswordBox��5A      $	��250��5B      $��30��5C      5D      5E      �� 5H   
   $��3��5I      $	��200��5J      $��40��5K      $��	0,20,0,10q�5L      $��Center=�5M      $��Log In��5O      $��18(�5P      ��5N      �� *LoginCommand+5P      5R      5S      