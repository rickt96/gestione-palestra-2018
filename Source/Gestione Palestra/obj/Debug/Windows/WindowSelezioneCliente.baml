   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GestionePalestraGestionePalestra  5   	   -    'GestionePalestra.WindowSelezioneCliente   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GestionePalestra     5   	   -   5        $	��300��5   	   $��354.721��6     � 
ResizeMode$  NoResize=�6=   .��5	      � .+�5
   
   �� -   5         ��Name$ 	txt_cerca��5
      $��20��6%   $��
10,10,10,0q�61    �� TextWrapping$
 Wrap=�6E   $	��Top=�6Y   6q   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
dg_clienti��5      $��0,41,0,45.4q�6'      GridLinesVisibility$ 
Horizontal=�6<   
�������   
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .  $��14(�6�   	   AutoGenerateColumns	 .  
   HorizontalGridLinesBrush
 5      �� 5       �� Color# � ��6"      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ #��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 60   �� *#+  5      $
 Nome��6%    63   �� *nome+  5      $ Cognome��6%    66   �� *	cognome+5      � 5   
      	BasicGray#��[  6   $��45��67   $��Bottom=�6C   $��	0,0,0,0.4q�6^   .+�5      �� -   5         ��Name$ btn_seleziona��5      $��	SELEZIONA��6,   $��Right=�6@   $��
0,10,10,10q�6\   $
��15,6q�6p   6   5      5      5      