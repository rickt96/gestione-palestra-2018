   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GestioneClientiGestioneClienti  5      (    "GestioneClienti.PageGestioneSchede   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:GestioneClienti     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          N� Title$  PageGestioneSchede��5
      .��5      � 
�������.+�5   
   �� -   5         ��Name$ btn_modifica_categoria��5         standardButton#��[  61   $��Rinomina��6Y   $��
105,0,0,37q�6l   $��Bottom=�6�    � ToolTip$ modifica categoria��6�   $��Segoe UI Semibold)�6�   
�������$��26��6�   $
��Left=�6�   $��80��6  $��14(�6  6*  �� 5   
   -   5        $ btn_aggiungi_categoria��5      #��[  61   $��Aggiungi��6Y   $��	10,0,0,37q�6l   $��Bottom=�6   $ Crea una nuova categoria��6�   $��Segoe UI Semibold)�6�   $��26��6�   $
��Left=�6�   $��90��6  $��14(�6  6  �� 5   
   -   5        $ btn_elimina��5      #��[  6&   $��Elimina��6N   $��	0,0,10,37q�6`   $��Bottom=�6s   $% Elimina l'esercizio selezionato��6�   $��Segoe UI Semibold)�6�   
�������$��26��6�   $��Right=�6�   $��14(�6  $��90��6:  �� 5   
   -   5         ��Name$ lbx_categorie��5      $��1q�6)   $
��Left=�6=   $��
10,86,0,68q�6X   $	��270��6l   $��Disabled=�6x   #��� ��6�   �� 5   
   -   5        $ btn_modifica��5      #��[  6'   $��Modifica��6O   $��
0,0,105,37q�6b   $��Bottom=�6v   $ Modifica l'esercizio��6�   $��Segoe UI Semibold)�6�   
�������$��26��6�   $��Right=�6�   $��14(�6
  $��90��63  �� 5   
   -   5        $ btn_inserisci��5      #��[  6(   $��	Inserisci��6P   $��
0,0,200,37q�6d   $��Bottom=�6x   $! Aggiungi un nuovo esercizio��6�   $��Segoe UI Semibold)�6�   
�������$��26��6�   $��Right=�6�   $��14(�6  $��90��6=  �� 5   
   -   5        $ btn_elimina_categoria��5      #��[  60   $��Elimina��6X   $��
190,0,0,37q�6j   $��Bottom=�6~   $ elimina categoria��6�   $��Segoe UI Semibold)�6�   $��26��6�   $
��Left=�6�   $��90��6�   $��14(�6  6  �� 5   
   -	   5         ��Name$ 	sv_schede��5      $��285,55,10,68q�6*   
�������$��1q�6T   ��5      �� 5       �� Color# � ��6"   .��5      � -
   5         �Name$ 
wrp_schede��5      $	��338��6,   $	��305��69   $
��Left=�6E   $	��Top=�6`   5      (  "Xceed.Wpf.Toolkit.WatermarkTextBox  5   
   -   5          Name$ txt_cerca_esercizi��5       $��	10,55,0,0q�6<    �� TextWrapping$
 Wrap=�6O   	   	Watermark$	 cerca scheda...��6c   $	��3,0q�6   $
��Left=�6�   $	��270��6�   $��14(�6�   $��26��6�   $	��Top=�6  $��Center=�6  {�.  .��6M  ��5      �� 5      # � ��6"   .��5   1   5      �� 5   
     	BasicGray#���  6   $��25��6=   $��Bottom=�6I   $��0,1,0,0q�6d     	BasicBlue#���  6~   .@�5      �� -   5        
 ��Name$
 txt_cat_count��5      $��-- categorie��6/   
���   �6C   �� 5       $��1��6   $
��Left=�6"   #���  6=   #���  6g   �� 5!      -   5        $
 txt_es_count��5!      
���   ��� 6B   $	�-- ��6F   �� 6S   $�schede��6W   6h   5"      �� 5#   
   $!��Gestione schede allenamenti��6     HeaderLabel#��[ 66   $
��Left=�6[   $��	47,10,0,0q�6v   $	��Top=�6�   �� 5$   
   -   5        $ btn_indietro��5$      #��[  6'   $
��Left=�6O   $��	10,10,0,0q�6j   $	��Top=�6}   $��32��6�   $��32��6�   $��4q�6�   .��5%      �� $��Icone/WhiteBack2.png��6   5&      5'      5(      