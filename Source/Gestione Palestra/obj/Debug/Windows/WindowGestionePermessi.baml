   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GestionePalestraGestionePalestra  5   	   -    'GestionePalestra.WindowGestionePermessi   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GestionePalestra     5   	   -   5          � Title$  Gestione livelli permessi��5   	   $��441.208��6+   $	��500��6<   .��5	      � .+�5
   
   �� -   5         ��Name$ btn_aggiungi��5
      $��Right=�6'   $��	0,65,47,0q�6C   $	��Top=�6V   $��22��6n      standardButton#���   6y   $��22��6�   .��5      �� $?��9/Gestione Palestra;component/Resources/Icons/WhiteAdd.png��6   $��2q�6W   5      �� 5   
   -   5        $ btn_applica��5      $��Salva��6&   $��Right=�66   $��	0,0,20,20q�6R   $��Bottom=�6e   $��75��6�   #���   6�   $��20��6�   6�   �� 5   
   -   5        $ btn_annulla��5      $��Annulla��6&   $��Right=�68   $��
0,0,100,20q�6T   $��Bottom=�6h   $��75��6�   #���   6�   $��20��6�   6�   �� 5   
   $����Gestisci e configura i livelli di permesi da assegnare agli istruttori.
Seleziona un livello per visualizzarlo e modificarlo, clicca sul bottone + per crearne uno nuovo.��6   $��
10,10,10,0q�6�   $	��Top=�6�   $��11(�6�   �� 5   
   -   5         ��Name$ cmb_livelli��5      $��102,65,74,0q�6(   $	��Top=�6=   6U   �� 5   
   -   5        $ btn_elimina��5      $��Right=�6&   $��	0,65,20,0q�6B   $	��Top=�6U   $��22��6m   #���   6x   $��22��6�   .��5      �� $B��</Gestione Palestra;component/Resources/Icons/WhiteDelete.png��6   $��2q�6Z   5      �� 5   
   $
��Left=�6   $��	20,93,0,0q�6/   $
��Wrap=�6B   $��Nome:��6V   $	��Top=�6c   $��77��6{   �� 5   
   $
��Left=�6   $��
20,118,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��77��6o   �� 6{   $�
Descrizion��6   �� 6�   $�e��6�   �� 6�   $�:��6�   6�   �� 5   
   $
��Left=�6   $��
20,147,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��	Permessi:��6o   $��77��6�   �� 5   
   -   5         ��Name$ txt_nome��5      $��20��6$   $��102,92,20,0q�60    �� TextWrapping$
 Wrap=�6E   $	��Top=�6Y   �� 5   
   -   5        $ txt_descrizione��5      $��20��6+   $��102,117,20,0q�67   $
 Wrap=�6M   $	��Top=�6a   �� 5   
   -	   5         ��Name$ lbx_permessi��5      $��102,147,20,55q�6(   �� 5   
   $
��Left=�6   $��	20,68,0,0q�6/   $
��Wrap=�6B   $��
Seleziona:��6V   $	��Top=�6h   $��77��6�   5      5      