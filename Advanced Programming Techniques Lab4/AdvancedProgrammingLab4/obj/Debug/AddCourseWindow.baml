   M S B A M L   `   `   `  ����R  NAdvancedProgrammingLab4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdvancedProgrammingLab4AdvancedProgrammingLab4  5   	   -    'AdvancedProgrammingLab4.AddCourseWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AdvancedProgrammingLab4     5   	     � Title$  
Add Course��5   	   $	��450��6   $	��600��6)    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $
��Name��6   $��0��6   $��0��6,   $��Center=�6<   �� 5   
   -   5         ��Name$ CourseNameTextBox��5      $��0��6-   $��1��6:   $��2��6J   $��5q�6^   �� 5   
   $��Teacher��6   $��1��6"   $��0��6/   $��Center=�6?   �� 5   
   -   5         ��Name$ TeacherComboBox��5      $��1��6,   $��1��69   $��2��6I   $��5q�6]    �� DisplayMemberPath$ FullName��5      �� 5   
   $��Students��6   $��2��6#   $��0��60   $��3��6@   $
��Bold"�6T   $��0,10,0,5q�6f   �� 5    
   -   5         ��Name$ AvailableStudentsListView��5       $��3��66   $��0��6C   $��2��6S   $��5,0,5,5q�6g    �� View 5"      �� 5#      .P�5$      �� $��Available Students��6%   	 �� DisplayMemberBinding	 6A   �� *
FullName+5%      5'      �� 5)   
   -   5        $ SelectedStudentsListView��5)      $��3��65   $��2��6B   $��5,0,5,5q�6R    5*      �� 5+      .P�5,      �� $��Selected Students��6%   
 �� CellTemplate
 5-      �� 5.      .R�5/   "   �  50   &   �� 51   *   $��* �6;   �� 52   *   $
��Auto �6;   .+�54   &   �� $��0��6J   ��60   �� *
FullName+6J   �� 55   &   -   5        $��Remove��55   -   $��1��6>   $��5,0,0,0q�6N   $��70��56   n   ��6Q   �� 6n   57   #   58      5:      5;      5=      �� 5?   
   -   5         ��Name$ ActionCourseButton��5?      $	��Add��6-   $��4��6;   $��0��6H   $��3��6X   $��Right=�5@      $	��100��6-   $��0,10,5,5q�69   5A      5B      5C      