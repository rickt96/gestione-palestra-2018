   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GestionePalestraGestionePalestra  5      1    +GestionePalestra.Pages.PageGestionePalestra   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:GestionePalestra     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5        N� Title$  PageGestionePalestra��5	      .��5
      � 
������� � ColumnDefinitions 5   
   �� 5      �� 5      $	��150 �6   .+�5   
   �� $��0,1,0,0q�6   $��25��6.   $��Bottom=�6:   $��2��6U      	BasicBlue#���   6i   ��5      �� 5       �� Color# � ��6"   .@�5      �� -   5         ��Name$ lbl_tot_record��5      $��0 record trovati��6,   $
��Left=�6G   $��12(�6b   $	��5,0q�6p   �� 5      $��1��6   
�������$
��Left=�65   #���   6P   #���   6y   �� 5      -   5        $ lbl_display_record��5      $��0 record mostrati��60   $
��Left=�6L   $��12(�6g   $	��5,0q�6u   �� 5      $��1��6   
�������$
��Left=�65   #���   6P   �� 5      -   5        $ lbl_tot_sel��5      $��0 record selezionati��6)   $
��Left=�6H   $��12(�6c   $	��5,0q�6q   �� 5      $��1��6   
�������$
��Left=�65   $	��0,2q�6P   #���   6]   �� 5      -   5        $ lbl_last_update��5      $%��ultimo aggiornamento alle 00:00��6-   $
��Left=�6W   $��12(�6r   $	��5,0q�6�   5      �� 5   
   $	��Top=�6   $��
10,83,10,0q�6,   $��19��6@   $��1��6L   �� 6]   -   5         ��Name$ hl_avanzate��5   g   $��14(�6|   .�6�   �� $�Ricerca avanzata��6�   6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
dg_clienti��5         AutoGenerateColumns . ���	#FFB9B9B9   CanUserResizeRows .  
�������   CanUserAddRows .  ���	#FF042271$��1q�6�   	   HorizontalGridLinesBrush	 �	#FFAEB7B6
   VerticalGridLinesBrush
 �	#FFAEB7B6   
IsReadOnly .  �� AlternationCount$ 1��6P  $��10,57,12,36q�6�     GridLinesVisibility$	 All=�6�     AlternatingRowBackground 5      �� 5      # � ��6"   5       (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5!   
   -   5          Name$ 	txt_cerca��5!       $��26��63   $��
10,57,10,0q�6?    �� TextWrapping$
 Wrap=�6S   $	��Top=�6g      	Watermark$ ricerca rapida...��6�   $	��3,0q�6�   $��1��6�   $��Center=�6�   $��14(�6  �� 5"   
   -   5         ��Name$ btn_elimina��5"        standardButton#��[ 6&    �� 	IsDefault . $��10,143,10,0q�6y    � ToolTip$$ Elimina il cliente selezionato��6�   $��24��6�   $	��Top=�6�   $��1��6�   .��5#      ��  �� Orientation$ 
Horizontal=�6   .+�5$      �� $@��:/Gestione Palestra;component/Resources/Icons/WhiteSave.png��6   $��14��6\   $��14��6h   �� 5%      $��Elimina��6   $��5,0,0,0q�6+   5&      5'      �� 5(   
   -	   5        $ btn_apri_scheda��5(      #��[ 6*    . $��10,114,10,0q�6�   $, &Apri la scheda del cliente selezionato��6�   $��24��6�   $	��Top=�6�   $��1��6�   .��5)      �� $ 
Horizontal=�6   .+�5*      �� $@��:/Gestione Palestra;component/Resources/Icons/WhiteEdit.png��6   $��14��6\   $��14��6h   �� 5+      $��Modifica��6   $��5,0,0,0q�6,   5,      5-      �� 5.   
   -
   5        $ btn_nuovo_cliente��5.      #��[ 6,    . $��10,172,10,0q�6�   $ Aggiungi un nuovo cliente��6�   $��24��6�   $	��Top=�6�   $��1��6�   .��5/      �� $ 
Horizontal=�6   .+�50      �� $@��:/Gestione Palestra;component/Resources/Icons/WhiteAdd2.png��6   $��14��6\   $��14��6h   �� 51      $��	Inserisci��6   $��5,0,0,0q�6-   52      53      �� 54   
   #��[ 6   $��
10,0,10,65q�69   $��Bottom=�6M   $��24��6h   $��1��6t   {�.  .��55      �� $ 
Horizontal=�6   .+�56      �� $B��</Gestione Palestra;component/Resources/Icons/WhiteExport.png��6   $��14��6^   $��14��6j   �� 57      $��Esporta��6   $��5,0,0,0q�6+   58      59      �� 5:   
   #��[ 6   $��
10,0,10,94q�69   $��Bottom=�6M   $��24��6h   $��1��6t   {�.  .��5;      �� $ 
Horizontal=�6   .+�5<      �� $A��;/Gestione Palestra;component/Resources/Icons/WhitePrint.png��6   $��14��6]   $��14��6i   �� 5=      $��Stampa��6   $��5,0,0,0q�6*   5>      5?      �� 5@   
   -   5        $ btn_aggiorna��5@      #��[ 6'   $��
10,0,10,36q�6O   $��Bottom=�6c   $��24��6~   $��1��6�   .��5A      �� $ 
Horizontal=�6   .+�5B      �� $C��=/Gestione Palestra;component/Resources/Icons/WhiteRefresh.png��6   $��14��6_   $��14��6k   �� 5C      $��Aggiorna��6   $��5,0,0,0q�6,   5D      5E      �� 5F   
   $��Gestione clienti��6     HeaderLabel#��[ 6+   $
��Left=�6P   $��	47,10,0,0q�6k   $	��Top=�6~   �� 5G   
   -   5        $ btn_indietro��5G      #��[ 6'   $
��Left=�6P   $��	10,10,0,0q�6k   $	��Top=�6~   $��32��6�   $��32��6�   $��4q�6�   ��5H      �� 5I      $A��;/Gestione Palestra;component/Resources/Icons/WhiteBack2.png��6   5K      5M      5N      