   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GestioneClientiGestioneClienti  5      &     GestioneClienti.PageImpostazioni   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:GestioneClienti     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          N� Title$  PageImpostazioni��5      .��5      � 
�������.+�5   
   �� $��Impostazioni��6      HeaderLabel#��[  6'   $
��Left=�6L   $��	47,10,0,0q�6g   $	��Top=�6z   $	��5,0q�6�   �� 5   
   -   5         ��Name$ btn_indietro��5        standardButton#��[ 6'   $
��Left=�6O   $��	10,10,0,0q�6j   $	��Top=�6}   $��32��6�   $��32��6�   $��4q�6�   .��5      �� $��Icone/WhiteBack2.png��6   5      �� 5   
   -   5         ��Name$ tab_main��5      $��10,57,10.2,45.2q�6'   ��6@   Z� .@�5      �� $��Generali��6   $��35��6(   $	��6,2q�64   
�������.��5      � 
�������.+�5      � -   5         �Name$ grid_img_logo��5      $��94��62   $��	0,23,30,0q�6>   $	��Top=�6Q   $��Right=�6i   $��94��6�   ��5      �� 5       �� Color# � ��6.   5      �� 5      -   5         ��Name$ 
textBlock2��5       $
��Left=�64   $��	30,22,0,0q�6O   $
��Wrap=�6b   $	��Top=�6v   $��Segoe UI Semibold)�6�   �� 6�   $�Logo di sistema��6�   �� 6�   $�:��6�   6�   �� 5      -   5        $ 
textBlock3��5       $
��Left=�64   $��	30,44,0,0q�6O   $
��Wrap=�6b   $	��Top=�6v   $	��418��6�   �� 6�   $�	Seleziona��6�   �� 6�   $
� un'��6�   �� 6�   $� i��6�   �� 6�   $"�mmagine che verrà utilizzat��6�   �� 6�   $�a come logo del sistema��6  �� 6"  $R�L. Essa sarà visualizzata nella schermata home e nelle stampe personalizzate��6&  �� 6{  �� 6�  6�  �� 5      -   5        $ btn_carica_logo��5      #��[ 66   $��Carica logo..��6^   $
��Left=�6v   $��	30,97,0,0q�6�   $	��Top=�6�   $��90��6�   $��20��6�   6�   �� 5      $��33��6    $��30,122,30,0q�6,   $	��Top=�6A   �� 5      -   5        $ textBlock2_Copy��5       $
��Left=�69   $��
30,160,0,0q�6T   $
��Wrap=�6h   $	��Top=�6|   $��Segoe UI Semibold)�6�   $#��Nome del centro/proprietario:��6�   �� 5       -	   5         ��Name$ txt_nome_attivita��5       $
��Left=�69   $��20��6T   $��
30,201,0,0q�6`    �� TextWrapping$
 Wrap=�6t   $	��Top=�6�   $	��222��6�   $��Center=�6�   �� 5!      -
   5        $ 
textBlock4��5!       $
��Left=�64   $��
30,181,0,0q�6O   $
��Wrap=�6c   $	��Top=�6w   $	��418��6�   $E��?Il nome del centro verrà utilizzato nelle stampe dei documenti��6�   �� 5"      $��33��6    $��30,232,30,0q�6,   $	��Top=�6A   �� 5#      -   5        $ textBlock2_Copy1��5#       $
��Left=�6:   $��
30,270,0,0q�6U   $
��Wrap=�6i   $	��Top=�6}   $��Segoe UI Semibold)�6�   $��Data di avvio:��6�   (  "System.Windows.Controls.DatePicker  5$      -   5          Name$ dp_data_avvio��5$   !   $
��Left=�68   $��
30,312,0,0q�6S   $	��Top=�6g   ���	#FFABADB3
�������$	��178��6�   �� 5%      -   5        $ textBlock4_Copy��5%       $
��Left=�69   $��
30,291,0,0q�6T   $
��Wrap=�6h   $	��Top=�6|   $	��418��6�   �� 6�   $G�ALa data di avvio determina il riferimento per l'avvio del sistema��6�   6�   5&      5'      �� 5(      $��Backup��6   $��35��6&   $	��6,2q�62   .��5)      � 
�������.+�5*      �� -   5        $ txt_ultimo_backup��5*      $��25��69   $��
30,350,0,0q�6E   $
 Wrap=�6Y   $��0000/00/00 00:00:00��6m   $	��Top=�6�   	 �� 
IsReadOnly	 . $��Center=�6�   $
��Left=�6�   $	��153��6�   �� 5+      -   5        $ btn_crea_backup��5+      #��[ 66   $��Crea ora��6^   $
��Left=�6q   $	��127��6�   $��12(�6�   $��1q�6�   $��
30,290,0,0q�6�   $��21��6�   $	��Top=�6�   6�   �� 5,      -   5        
 ��Name$
 lbx_tipo_backup��5,      $��1q�67   $	��207��6K   $��	30,36,0,0q�6X   $	��Top=�6k   $
��Left=�6�   $	��363��6�   
�������@�5-      �� 5.      $#��backup automatico disattivato��6*   �� 5/      $��ogni giorno��6*   �� 50      $��ogni 3 giorni��6*   �� 51      $��ogni 5 giorni��6*   �� 52      $��ogni settimana��6*   �� 53      $��ogni 2 settimane��6*   �� 54      $��	ogni mese��6*   56      �� 57      -   5         ��Name$ label_Copy1��57      $"��Frequenza backup automatici:��61   $
��Left=�6X   $��	30,10,0,0q�6s   $	��Top=�6�   �� 58      -   5        $ label_Copy2��58      $��Creazione backup manuale:��61   $
��Left=�6U   $��
30,264,0,0q�6p   $	��Top=�6�   �� 59      -   5        $ label_Copy3��59      $��Data ultimo backup:��61   $
��Left=�6O   $��
30,325,0,0q�6j   $	��Top=�6~   5:      5;      �� 5<      $��
Ripristino��6   $	��6,2q�6*   .��5=      � 
�������$��0q�6*   .+�5>      �� -   5        $ btn_avvio_ripristino��5>      #��[ 6;   $��Avvia��6c   $
��Left=�6s   $	��Top=�6�   $	��100��6�   $��21��6�   $��12(�6�   $��1q�6�   $��
30,376,0,0q�6�   6�   �� 5?      -   5        $ lbl_selected_file_backup��5?      $��Seleziona un file..��6>   $��149,374,0,0q�6\   $	��Top=�6q   $
��Left=�6�   $	��191��6�   �� 5@      -   5        $
 
lbx_backup��5@      $	��121��62   $��	30,36,0,0q�6?   $	��Top=�6R   $��1q�6�   $
��Left=�6�   $	��310��6�   
�������6�   �� 5A      -   5        $ txt_ultimo_ripristino��5A      $��
30,451,0,0q�6=   $
 Wrap=�6Q   $��0000/00/00 00:00:00��6e   	 . $��20��6�   $	��Top=�6�   $��Center=�6�   $
��Left=�6�   $	��151��6�   �� 5B      -   5        $
 listBox��5B      $��1q�6/   $��96��6C   $��
30,225,0,0q�6O   $	��Top=�6c   {�.  $
��Left=�6�   $	��310��6�   
�������.@�5C      �� $��tutte le tabelle��6#    d� 	IsChecked$
 TrueY�6>   {�.  6O   5D      �� 5E      -   5        $ btn_elimina_ripristino��5E      #��[ 6=   $��	Elimina..��6e   $
��Left=�6y   $	��Top=�6�   $	��100��6�   $��21��6�   $��12(�6�   $��1q�6�   $��240,162,0,0q�6�   6�   �� 5F      -   5        $ btn_esporta_ripristino��5F      #��[ 6=   $��Esporta��6e   $
��Left=�6w   $	��Top=�6�   $	��100��6�   $��21��6�   $��12(�6�   $��1q�6�   $��135,162,0,0q�6�   6�   �� 5G      -   5        $ btn_dettagli_backup��5G      #��[ 6:   $��Dettagli��6b   $
��Left=�6u   $	��Top=�6�   $	��100��6�   $��21��6�   $��12(�6�   $��1q�6�   $��
30,162,0,0q�6�   6�   �� 5H      -   5        $ label_Copy4��5H      $��File da ripristinare:��61   $
��Left=�6Q   $��	30,10,0,0q�6l   $	��Top=�6   �� 5I      -   5        $ label_Copy6��5I      $(��"Seleziona i campi da ripristinare:��61   $
��Left=�6^   $��
30,199,0,0q�6y   $	��Top=�6�   �� 5J      -   5        $ label_Copy8��5J      $��Avvia ripristino:��61   $
��Left=�6M   $��
30,350,0,0q�6h   $	��Top=�6|   �� 5K      -   5        $ label_Copy9��5K      $��Data ultimo ripristino:��61   $
��Left=�6S   $��
30,426,0,0q�6n   $	��Top=�6�   5L      5M      �� 5N      $��Utenti��6   $
��Left=�6&   $	��Top=�6A   $��35��6Y   $	��6,2q�6e   .��5O      � 
�������.+�5P      &   System.Windows.Controls.DataGrid  -    5          Name$ 	dg_utenti��5P         
IsReadOnly . ���	#FF688CAF$��1q�6\    �� AlternationCount$ 2��6p      HorizontalGridLinesBrush �	#FFAEB7B6   VerticalGridLinesBrush �	#FFAEB7B6$��Center=�6�   
����������	#FF042271$��30,41,0,76.2q�6  $
��Left=�6/  $	��508��6J     AlternatingRowBackground 5Q      �� 5R      # � ��6.   5T      �� 5U      -!   5        $ btn_inserisci_istruttore��5U      #��[ 6?   $��	Inserisci��6g   $
��Left=�6{   $��30,0,0,49.2q�6�   $��Bottom=�6�   $��76��6�    � ToolTip$# Inserisci un nuovo istruttore��6�   $��22��6   �� 5V      -"   5        $) #btn_modifica_credenziali_istruttore��5V      #��[ 6J   $��Modifica credenziali��6r   $
��Left=�6�   $��192,0,0,49.2q�6�   $��Bottom=�6�   $	��135��6�   $- 'Modifica le credenziali dell'iatruttore��6�   $��22��6M  �� 5W      -#   5        $ btn_elimina_istruttore��5W      #��[ 6=   $��Elimina��6e   $
��Left=�6w   $��111,0,0,49.2q�6�   $��Bottom=�6�   $��76��6�   $&  Elimina l'istruttore selezionato��6�   $��22��6  �� 5X      -$   5        $ btn_livelli_permessi��5X      #��[ 6;   $��Gestisci livelli permessi��6c   $
��Left=�6�   $��368,0,0,49.2q�6�   $��Bottom=�6�   $	��170��6�   $&  Elimina l'istruttore selezionato��6�   $��22��6-  �� 5Y      -%   5        $ label_Copy10��5Y      $-��'Elenco degli istruttori e dei permessi:��62   $
��Left=�6d   $��	30,10,0,0q�6   $	��Top=�6�   5Z      5[      �� 5\      $��Connessione��6   $
��Left=�6+   $	��Top=�6F   $��35��6^   $	��6,2q�6j   .��5]      � 
�������.+�5^      �� -&   5        $ label_Copy7��5^      $/��)Parametri per la connessione al database:��61   $
��Left=�6e   $��	30,10,0,0q�6�   $	��Top=�6�   �� 5_      -'   5        $ btn_modifica_connessione��5_      $��Modifica connessione��6f   $
��Left=�6�   $��
30,157,0,0q�6�   $	��Top=�6�   $	��160��6�   �� 5`      -(   5        $ lbl_connessione��5`      $o��iServer:			127.0.0.1
Utente:			asd
password:		asdasd
Porta servizio:		asdasdas
Nome database:		dadsasd��65   $
��Left=�6�   $��	30,41,0,0q�6  $	��Top=�6'  $	��250��6?  $	��111��6K  $��15,5,5,5q�6X  5a      5b      �� 5c      $��Percorsi��6   $
��Left=�6(   $	��Top=�6C   $��35��6[   $	��6,2q�6g   .��5d      � 
�������.+�5e      �� -)   5        $ textBlock2_Copy2��5e       $
��Left=�6:   $��	30,10,0,0q�6U   $
��Wrap=�6h   $	��Top=�6|   $��Segoe UI Semibold)�6�   $��Percorso Google Chrome:��6�   �� 5f      -*   5        $ textBlock4_Copy1��5f       $
��Left=�6:   $��	30,31,0,0q�6U   $
��Wrap=�6h   $}��wPer poter accedere alle funzionalità di stampa è necessario specificare il percorso di installazione di Google Chrome��6|   $	��Top=�6�   $	��313��6  �� 5g      -+   5        $ btn_sel_chrome��5g      #��[ 65   $��	Seleziona��6]   $��Right=�6q   $��0,68,179.8,0q�6�   $	��Top=�6�   $��82��6�   $��26��6�   6�   �� 5h      -,   5        $ txt_chrome_path��5h      $��30,69,266.8,0q�67   $	��Top=�6N   $��20��6f   �� 5i      $��33��6    $��30,100,179.8,0q�6,   $	��Top=�6D   �� 5j      --   5        $ textBlock2_Copy3��5j       $
��Left=�6:   $��
30,138,0,0q�6U   $
��Wrap=�6i   $	��Top=�6}   $��Segoe UI Semibold)�6�   $��Cartella dei backup:��6�   �� 5k      -.   5        $ btn_sel_chrome_Copy��5k      #��[ 6:   $��	Seleziona��6b   $��Right=�6v   $��0,158,179.8,0q�6�   $	��Top=�6�   $��82��6�   $��26��6�   6�   �� 5l      -/   5        $ txt_chrome_path_Copy��5l      $��30,159,266.8,0q�6<   $	��Top=�6T   $��20��6l   �� 5m      $��33��6    $��30,190,179.8,0q�6,   $	��Top=�6D   �� 5n      -0   5        $ textBlock2_Copy4��5n       $
��Left=�6:   $��
30,228,0,0q�6U   $
��Wrap=�6i   $	��Top=�6}   $��Segoe UI Semibold)�6�   $��Cartella dei documenti:��6�   �� 5o      -1   5        $ btn_sel_chrome_Copy1��5o      #��[ 6;   $��	Seleziona��6c   $��Right=�6w   $��0,248,179.8,0q�6�   $	��Top=�6�   $��82��6�   $��26��6�   6�   �� 5p      -2   5        $ txt_chrome_path_Copy1��5p      $��30,249,266.8,0q�6=   $	��Top=�6U   $��20��6m   5q      5r      5s      �� 5t   
   -3   5        $ 	btn_salva��5t      $��Salva modifiche��6$   $��Right=�6>   $��0,0,10.2,10.8q�6Z   $��Bottom=�6q   $	��150��6�   6�   5v      5w      