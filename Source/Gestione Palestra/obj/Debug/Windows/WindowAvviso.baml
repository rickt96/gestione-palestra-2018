   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GestionePalestraGestionePalestra  5   	   #    GestionePalestra.WindowAvviso   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GestionePalestra     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5        $��594.501��5	   	   $	��450��6     � 
ResizeMode$  NoResize=�6=   .��5
      � .+�5   
   �� $��10,331,10,0q�6   $	��170��6(   $	��Top=�65   $��Destinatari��6M   ���	#FFD5DFE5   	BasicBlue#���   6z   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dg_istruttori_dest��5      $��10q�67   $��1q�6C      CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .  
�������   AutoGenerateColumns .  ���	#FFB9B9B9   HorizontalGridLinesBrush 6�   Z�    VerticalGridLinesBrush 6�   Z�    Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 #��61   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6<   �� *Selezionato+6^   0  *System.Windows.Controls.DataGridTextColumn  5      $	 
Istruttore��6-      
IsReadOnly . 
 6A   �� *Nome+6n     5      $	 Data lettura��6-    . 
 6C   �� *DataLettura+6w   5      5      5      �� 5   
   -   5         ��Name$ groupBox��5      $��10,10,10.2,0q�6%   $	��316��6;   $	��Top=�6H   $��Informazioni��6`   ���	#FFD5DFE5#���   6�   .��5      � .+�5      �� -   5         ��Name$ txt_descrizione��5      $��106,167,19.8,0q�63    �� TextWrapping$
 Wrap=�6K   $��4��6_   $��12(�6l   $��1q�6z    �� 	MaxLength$	 254��6�   $��Segoe UI)�6�   
���   �$��65��6�   $	��Top=�6�   $��asd
asd
asd��6�   .��6  ��5      �� 5       �� Color# � ��6*   .��5   +   5      �� 5      -   5         ��Name$ label_Copy10��5      $��Descrizione:��6.   $
��Left=�6E   $��
10,165,0,0q�6`   $	��Top=�6t   �� 5      -   5        $ label_Copy2��5      $��
Priorità:��6-   $
��Left=�6A   $��	10,72,0,0q�6\   $	��Top=�6o   $��85��6�   	� 5      -   5         	�Name$ rdb_priorita_0��5      $��Normale��66   $
��Left=�6H   $��
106,78,0,0q�6c   $	��Top=�6w   $��Center=�6�    	� 	GroupName$ priorita��6�    d� 	IsChecked$
 TrueY�6�   	� 5       -   5        $ rdb_priorita_1��5       $��Urgente��66   $
��Left=�6H   $��
187,78,0,0q�6c   $	��Top=�6w   $��Center=�6�   $ priorita��6�   �� 5!      -	   5        $ label_Copy12��5!      $��Data e ora:��6.   $
��Left=�6D   $��
10,103,0,0q�6_   $	��Top=�6s   $��85��6�   &   Xceed.Wpf.Toolkit.DateTimePicker  5"      -
   5          Name$ dtp_data��5"   &   $��106,106,19.8,0q�68   $��Center=�6P   $��4��6r   $��12(�6   $��1q�6�   $��Segoe UI)�6�   
���   �$��23��6�   $	��Top=�6�   ,  &Xceed.Wpf.Toolkit.Primitives.InputBase   
IsReadOnly .  6�   �� 5#      -   5        $ label_Copy1��5#      $��
Tipologia:��6-   $
��Left=�6B   $��	10,41,0,0q�6]   $	��Top=�6p   �� 5$      -   5         ��Name$ cmb_tipologia��5$      $��106,41,95.8,0q�62   $	��Top=�6I   $��26��6a   $��3��6m   $��Segoe UI)�6z   
���   �$��4,2,2,2q�6�   �� 5%      -   5        $ label_Copy3��5%      $��Titolo:��6-   $
��Left=�6?   $��	10,10,0,0q�6Z   $	��Top=�6m   $��26��6�   �� 5&      -   5        $ 
txt_titolo��5&      $��26��6.   $��106,10,95.8,0q�6:   $
 Wrap=�6Q   $	��Top=�6e   $��Center=�6}   $ 50��6�   �� 5'      -   5        $ label_Copy4��5'      $��Cliente:��6-   $
��Left=�6@   $��
10,134,0,0q�6[   $	��Top=�6o   �� 5(      -   5        $ cmb_cliente��5(      $��106,136,19.8,0q�60   $	��Top=�6H   $��20��6`   $��3��6l   $��Segoe UI)�6y   
���   �$��4,0,2,0q�6�    �� 
IsEditable . 6�   �� 5)      -   5        $ label_Copy5��5)      $��Visibilità:��6-   $
��Left=�6C   $��
10,240,0,0q�6^   $	��Top=�6r   $��85��6�   	� 5*      -   5        $  rdb_visibilita_0_personale��5*      $��	Personale��6B   $
��Left=�6V   $��106,246,0,0q�6q   $	��Top=�6�   $��Center=�6�   $ 
visibilita��6�   $
 TrueY�6  6  	� 5+      -   5        $ rdb_visibilita_1_pubblica��5+      $��Pubblica��6A   $
��Left=�6T   $��202,246,0,0q�6o   $	��Top=�6�   $��Center=�6�   $ 
visibilita��6�   5,      5-      � 5.   
   -   5         �Name$ grid_actions��5.        	BasicGray#��[ 6%   $��42��6M   $��Bottom=�6Y   .+�5/      �� -   5         ��Name$ 	btn_salva��5/        standardButton#��[ 6(    �� 	IsDefault . $��
0,10,10,10q�6a   $��Right=�6�   $��80��6�   .��50      ��  �� Orientation$ 
Horizontal=�6   .+�51      �� $@��:/Gestione Palestra;component/Resources/Icons/WhiteSave.png��6   $��14��6`   $��14��6l   �� 52      $��Salva��6    $��5,0,0,0q�6-   53      54      �� 55      -   5        $ btn_elimina��55      #��[ 6*    . $��0,10,100,10q�6}   $��Right=�6�   $��80��6�   .��56      �� $ 
Horizontal=�6   .+�57      �� $B��</Gestione Palestra;component/Resources/Icons/WhiteDelete.png��6   $��14��6b   $��14��6n   �� 58      $��Elimina��6    $��5,0,0,0q�6/   59      5:      5;      5=      5>      