   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GestioneClientiGestioneClienti  5   	   -    'GestioneClienti.WindowSchedaAllenamento   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:GestioneClienti     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5        $��569.734��5
   	   $	��580��6   $	��795��6*   $	��785��67   $	��810��6F     � 
ResizeMode$  NoResize=�6U   .��5      � .+�5   
   � -   5         �Name$ grid_actions��5         	BasicGray#��[  6%   $��44��6M   $��Bottom=�6Y   .+�5      �� -   5         ��Name$ 
btn_stampa��5      $��
10,10,0,10q�6B     standardButton#���  6V    � ToolTip$ stampa la scheda corrente��6   $
��Left=�6�   $	��120��6�   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Icone/WhitePrint.png��6   $��14��6:   $��14��6F   �� 5      $��STAMPA��6    $��5,0,0,0q�6.   5      5      �� 5      -   5        $ btn_importa_modello��5      $��140,10,0,10q�6T   #���  6i   $" importa un modello esistente��6�   $
��Left=�6�   $	��180��6�   .��5      �� $ 
Horizontal=�6   .+�5      �� $��Icone/WhiteModel.png��6   $��14��6:   $��14��6F   �� 5      $��IMPORTA MODELLO��6    $��5,0,0,0q�67   5      5      �� 5      -   5        $ 	btn_salva��5      $��
0,10,10,10q�6@   #���  6T   $ salva le modifiche��6}   $��Right=�6�   $	��170��6�   .��5      �� $ 
Horizontal=�6   .+�5      �� $��Icone/WhiteSave.png��6   $��14��69   $��14��6E   �� 5      $��5,0,0,0q�6    SALVA MODIFICHE6B   5      5      �� 5      -   5        $ btn_elimina��5      $��0,10,190,10q�6D   #���  6Y   $  elimina la scheda corrente��6�   $��Right=�6�   $	��130��6�   .��5       �� $ 
Horizontal=�6   .+�5!      �� $��Icone/WhiteDelete.png��6   $��14��6;   $��14��6G   �� 5"      $��ELIMINA��6    $��5,0,0,0q�6/   5#      5$      5%      � 5&   
   
�������$��10,10,255.8,0q�6"   $	��155��69   $	��Top=�6F   .+�5'      �� -   5         ��Name$ txt_nome��5'      $
��Left=�6(   $��24��6C   $��	10,26,0,0q�6O    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $	��165��6�   $��Center=�6�   �� 5(      -   5         ��Name$ cmb_freq_sett��5(      $
��Left=�6.   $��
380,26,0,0q�6I   $	��Top=�6]   $	��127��6u   $��24��6�    �� SelectedIndex$ 0��6�   �� 5)      -	   5        $ cmb_istruttore��5)      $
��Left=�6/   $��195,116,0,0q�6J   $	��165��6_   $��24��6k   $	��Top=�6w   $��6,0,5,0q�6�   $ 0��6�   �� 5*      -
   5        $ txt_dettagli��5*      $��
195,26,0,0q�6,   $
 Wrap=�6@   $
��Left=�6T   $	��165��6o   $��24��6{   $	��Top=�6�   �� 5+      -   5        	 ��Name$	 ckb_is_model��5+      $��Salva come modello��6-   $
��Left=�6J   $��380,116,0,0q�6e   $	��Top=�6z   $f `Attenzione: se una scheda viene salvata come modello non sarà possbile assegnarla ad un cliente��6�   $��24��6>  $��Center=�6J  �� 5,      -   5        $ txt_obiettivo��5,      $
��Left=�6-   $��	10,71,0,0q�6H   $	��Top=�6[   $	��165��6s   $��24��6   �� 5-      -   5        
 ��Name$
 	textBlock��5-      $
��Left=�6+   $��	10,10,0,0q�6F   $
��Wrap=�6Y   $��Nome scheda:��6m   $	��Top=�6�   �� 5.      -   5        $
 textBlock_Copy��5.      $
��Left=�60   $��
380,10,0,0q�6K   $
��Wrap=�6_   $��Frequenza settimanale:��6s   $	��Top=�6�   �� 5/      -   5        $
 textBlock_Copy1��5/      $
��Left=�61   $��
380,55,0,0q�6L   $
��Wrap=�6`   $	��Top=�6t   �� 6�   $�N��6�   �� 6�   $�umero ��6�   �� 6�   $�sedute��6�   �� 6�   $� totali��6�   �� 6�   $�:��6�   6�   �� 50      -   5        $ cmb_n_sedute_totale��50      $
��Left=�64   $��
380,71,0,0q�6O   $	��Top=�6c   $	��127��6{   $��24��6�   $ 0��6�   �� 51      -   5        $
 textBlock_Copy2��51      $
��Left=�61   $��	10,55,0,0q�6L   $
��Wrap=�6_   $	��Top=�6s   �� 6�   $�	Obiettivo��6�   �� 6�   $�:��6�   6�   �� 52      -   5        $
 textBlock_Copy3��52      $
��Left=�61   $��
10,100,0,0q�6L   $
��Wrap=�6`   $��Difficoltà:��6t   $	��Top=�6�   �� 53      -   5        $
 textBlock_Copy4��53      $
��Left=�61   $��195,100,0,0q�6L   $
��Wrap=�6a   $	��Top=�6u   $��Istruttore:��6�   �� 54      -   5        $
 textBlock_Copy7��54      $
��Left=�61   $��380,100,0,0q�6L   $
��Wrap=�6a   $	��Top=�6u   �� 6�   $�Modello��6�   �� 6�   $�:��6�   6�   �� 55      -   5        $
 textBlock_Copy8��55      $
��Left=�61   $��
195,10,0,0q�6L   $
��Wrap=�6`   $��	Dettagli:��6t   $	��Top=�6�   �� 56      -   5         ��Name$ sld_difficolta��56      $��
10,116,0,0q�6-    � Maximum$ 5Q�6A    � Minimum$ 1Q�6M    � Value$ 1Q�6Y    �� AutoToolTipPlacement$ TopLeft=�6c    � SmallChange$ 1Q�6�   $
��Left=�6�   $	��165��6�   $��1q�6�   $��24��6�   $	��Top=�6  $��Center=�6  $��0q�6@  ��6�   Z� ��6�   Z� 6@  �� 57      -   5        $ cmb_categoria_scheda��57      $
��Left=�65   $��
195,71,0,0q�6P   $	��Top=�6d   $	��165��6|   $��24��6�   $ 0��6�   $	��3,1q�6�   �� 58      -   5        $
 textBlock_Copy5��58      $
��Left=�61   $��
195,55,0,0q�6L   $
��Wrap=�6`   $��
Categoria:��6t   $	��Top=�6�   59      � 5:   
   
�������$��
549,10,0,0q�6"   $
��Left=�66   $	��230��6Q   $	��155��6]   $	��Top=�6j   .+�5;      �� -   5        $ cmb_cliente��5;      $
��Left=�6,   $��	10,26,0,0q�6G   $	��Top=�6Z   $	��144��6r   $��24��6~   $��6,0,5,0q�6�    �� 
IsEditable . 6�   �� 5<      -   5        $
 textBlock_Copy6��5<      $
��Left=�61   $��	10,10,0,0q�6L   $
��Wrap=�6_   $��Cliente:��6s   $	��Top=�6�   �� 5=      -   5        $
 textBlock_Copy9��5=      $
��Left=�61   $��	10,55,0,0q�6L   $
��Wrap=�6_   $��Data inizio:��6s   $	��Top=�6�   (  "System.Windows.Controls.DatePicker  5>      -   5          Name$ dp_data_inizio��5>      $��	10,71,0,0q�61   ���	#FFACACAC
�������$��24��6o   $	��Top=�6{   $
��Left=�6�   $	��144��6�   �� 5?      -   5        $
 textBlock_Copy10��5?      $
��Left=�62   $��
10,100,0,0q�6M   $
��Wrap=�6a   $��
Data fine:��6u   $	��Top=�6�     5@      -   5        $ dp_data_fine��5@      $��
10,116,0,0q�6/   ���	#FFACACAC
�������$��24��6n   $	��Top=�6z   $
��Left=�6�   $	��144��6�   �� 5A      -   5        $ btn_rimuovi_cliente��5A      $��Rimuovi��62   $��159,26,10,0q�6D   $	��Top=�6Y   $��26��6q   6}   5B      � 5C   
   
�������$��
10,176,0,0q�6"   $
��Left=�66   $	��769��6Q   $	��317��6]   $	��Top=�6j    � ColumnDefinitions 5D      �� 5E      �� 5F      $	��100 �6#   .+�5H      �� -    5        $ btn_elimina_seduta��5H      $��Elimina��61   $��	95,0,0,10q�6C   $��Bottom=�6V   $ elimina la seduta��6q   $
��Left=�6�   $��80��6�   $��30��6�   �� 5I      -!   5        $ btn_aggiungi_seduta��5I      $��Aggiungi��62   $��	10,0,0,10q�6E   $��Bottom=�6X   $ aggiungi una nuova seduta��6s   $
��Left=�6�   $��18��6�   $��80��6�   &   System.Windows.Controls.DataGrid  5J      -"   5          Name$ 	dg_sedute��5J      $��
10,10,0,45q�6*   $��1q�6>      CanUserResizeColumns .     CanUserSortColumns .     CanUserReorderColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .  
�������   AutoGenerateColumns .     
IsReadOnly .  $
��Left=�6c  $	��165��6~  ���	#FFB9B9B9   VerticalGridLinesBrush 5K      �� 5L       �� Color# � ��6&      HorizontalGridLinesBrush 5N      �� 5O      # � ��6&       Columns  5Q      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn!   Header$! Seduta��6)   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 69   �� *Nome+  5S      $! N°Esercizi��6)   #   
IsReadOnly# . " 6=   �� *NumeroEsercizi+6`   5U      �� 5V      -#   5        $ btn_modifica_esercizio��5V      $��Modifica��65   $��
10,40,10,0q�6H   $	��Top=�6\   $ modifica categoria��6t   $��1��6�   $��25��6�   �� 5W      -$   5        $ btn_nuovo_esercizio��5W      $��	Inserisci��62   $��
10,10,10,0q�6F   $	��Top=�6Z   $ aggiungi categoria��6r   $��1��6�   $��25��6�   �� 5X      -%   5        $ btn_rimuovi_esercizio��5X      $��Rimuovi��64   $��
10,70,10,0q�6F   $	��Top=�6Z   $ elimina categoria��6r   $��1��6�   $��25��6�     5Y      -&   5        $ dg_esercizi��5Y      $��200,10,0,10q�6,   $��1q�6A   
������� .   .  .   .  ���	#FFB9B9B9 5Z      �� 5[      # � ��6&    5]      �� 5^      # � ��6&     5`        5a      $! 	Esercizio��6)   " 6<   �� *NomeEsercizio+  5b      $! 	Categoria��6)   " 6<   �� *NomeCategoria+  5c      $! Serie��6)   " 68   �� *Serie+  5d      $! Serie��6)   " 68   �� *Ripetizioni+  5e      $
! Rec.��6)   " 67   �� *
Recupero+$ �� StringFormat$$ mm\:ss��  5f      $! Carico��6)   " 69   �� *Carico+  5g      $! Durata��6)   " 69   �� *Durata+$$ mm\:ss��  5h      $! Gruppo��6)   " 69   �� *Gruppo+  5i      $! Metodo��6)   " 69   �� *Metodo+5k      �� 5l      -'   5        $ btn_unisci_esercizi��5l      $��Unisci��62   $��
10,0,10,79q�6C   $��Bottom=�6W   $A ;Unisci gli esercizi selezionati con la metodologia definita��6r   $��1��6�   $��17��6�   �� 5m      -(   5        $ btn_move_up��5m      $��	Sposta su��6*   $��1��6>   $��10,142,10,0q�6N   $	��Top=�6c   $��24��6�   �� 5n      -)   5        $ btn_move_down��5n      $��Sposta giù��6,   $��1��6A   $��10,172,10,0q�6Q   $	��Top=�6f   $��26��6�   �� 5o      -*   5        $ btn_svuota_esercizi��5o      $��Svuota��62   $��1��6C   $��10,100,10,0q�6S   $	��Top=�6h   $��25��6�   	� 5p      -+   5        % 	�Name$% rdb_0_standard��5p      $��Standard��62   $��1��6E   $��10.4,243,9.6,0q�6U   ���	#FFB9B9B9$��18��6�   $	��Top=�6�   	� 5q      -,   5        $% rdb_1_serie��5q      $��Serie��6/   $��1��6?   $��10.4,266,9.6,0q�6O   ���	#FFB9B9B9$��18��6   $	��Top=�6�   	� 5r      --   5        $% rdb_2_circuito��5r      $��Circuito��62   $��1��6E   $��10.4,289,9.6,0q�6U   ���	#FFB9B9B9$��18��6�   $	��Top=�6�   5s      5u      5v      