   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GestioneClientiGestioneClienti  5      *    $GestioneClienti.PageGestioneEsercizi   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:GestioneClienti     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          N� Title$  PageGestioneEsercizi��5
      .��5      � 
�������.+�5   
   �� -   5         ��Name$ btn_modifica_categoria��5         standardButton#��[  61   $��Rinomina��6Y   $��
105,0,0,38q�6l   $��Bottom=�6�    � ToolTip$ modifica categoria��6�   $��Segoe UI Semibold)�6�   
�������$��26��6�   $
��Left=�6�   $��80��6  $��14(�6  6*  �� 5   
   -   5        $ btn_aggiungi_categoria��5      #��[  61   $��Aggiungi��6Y   $��	10,0,0,38q�6l   $��Bottom=�6   $ Crea una nuova categoria��6�   $��Segoe UI Semibold)�6�   $��26��6�   $
��Left=�6�   $��90��6  $��14(�6  6  �� 5   
   -   5        $ btn_elimina��5      #��[  6&   $��Elimina��6N   $��	0,0,12,38q�6`   $��Bottom=�6s   $% Elimina l'esercizio selezionato��6�   $��Segoe UI Semibold)�6�   
�������$��14(�6�   $��Right=�6  $	��100��6.  $��26��6:  �� 5   
   -   5         ��Name$ 
lbx_gruppi��5      $��1q�6&   $
��Left=�6:   $��
10,88,0,69q�6U   $	��270��6i   $��Disabled=�6u   ���	#FFABABAB6�   �� 5   
   -   5        $ btn_modifica��5      #��[  6'   $��Modifica��6O   $��
0,0,117,38q�6b   $��Bottom=�6v   $ Modifica l'esercizio��6�   $��Segoe UI Semibold)�6�   
�������$��14(�6�   $��Right=�6  $	��100��6'  $��26��63  �� 5   
   -   5        $ btn_inserisci��5      #��[  6(   $��	Inserisci��6P   $��
0,0,222,38q�6d   $��Bottom=�6x   $! Aggiungi un nuovo esercizio��6�   $��Segoe UI Semibold)�6�   
�������$��14(�6�   $��26��6  $��Right=�6!  $	��100��6=  �� 5   
   -   5        $ btn_elimina_categoria��5      #��[  60   $��Elimina��6X   $��
190,0,0,38q�6j   $��Bottom=�6~   $ elimina categoria��6�   $��Segoe UI Semibold)�6�   $��26��6�   $
��Left=�6�   $��90��6�   $��14(�6  6  �� 5   
   -	   5         ��Name$ sv��5      $��290,57,10,69q�6#   
�������$��1q�6M   ��5      �� 5       �� Color# � ��6"   .��5      � -
   5         �Name$ wrp_esercizi��5      $	��338��6.   $	��305��6;   $
��Left=�6G   $	��Top=�6b   5      (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5   
   -   5          Name$ txt_cerca_esercizi��5       $��	10,57,0,0q�6<    �� TextWrapping$
 Wrap=�6O   	   	Watermark$	 cerca esercizio..��6c   $	��3,0q�6�   $��14(�6�   $��26��6�   $	��Top=�6�   $
��Left=�6�   $	��270��6  $��Center=�6  .��6=  ��5      �� 5      # � ��6"   .��5   1   5      �� 5   
   -   5        $ btn_import_cartella��5      $��	0,10,10,0q�6.   #��[  6A   $4 .Importa gli esercizi da una cartella esistente��6i   $	��Top=�6�   $��28��6�   $��Right=�6�   $	��160��6  .��5      �� 
 �� Orientation$
 
Horizontal=�6   .+�5       �� $��Icone/WhiteImport.png��6   $��14��67   $��14��6C   �� 5!      $��Importa cartella��6   $��5,0,0,0q�64   $��14(�6E   5"      5#      �� 5$   
     	BasicGray#���  6   $��25��6=   $��Bottom=�6I   $��0,1,0,0q�6d     	BasicBlue#���  6~   $��2��6�   .@�5%      �� -   5         ��Name$ txt_cat_count��5%      $��-- categorie��6/   
���   �6C   �� 5&      $��1��6   $
��Left=�6"   #���  6=   �� 5'      -   5        $ txt_es_count��5'      $��-- esercizi��6.   
���   �6A   5(      �� 5)   
   $��Gestione esercizi��6     HeaderLabel#��[ 6,   $
��Left=�6Q   $��	47,10,0,0q�6l   $	��Top=�6   $	��5,0q�6�   �� 5*   
   -   5        $ btn_indietro��5*      #��[  6'   $
��Left=�6O   $��	10,10,0,0q�6j   $	��Top=�6}   $��32��6�   $��32��6�   $��4q�6�   .��5+      �� $��Icone/WhiteBack2.png��6   5,      5-      5.      