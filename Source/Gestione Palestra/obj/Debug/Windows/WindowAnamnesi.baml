   M S B A M L   `   `   `  ����L  HGestione Palestra, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GestionePalestraGestionePalestra  5   	   # System.Windows.Controls.ToolkitjNclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.ToolkitSystem.Windows.Controls 5   	   %    GestionePalestra.WindowAnamnesi   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GestionePalestra     5   	   cControlsNclr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Toolkit     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f43xtk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5        $��761.2��5   	   $��1389.2��6     � 
ResizeMode$  NoResize=�6'   
�������$��14(�6g   .��5      � .+�5   
   �    	BasicGray#��[  6   $��42��67   $��Bottom=�6C   .+�5      �� -   5         ��Name$ 	btn_salva��5        standardButton#��[ 6(   $��
0,10,10,10q�6h   $��Right=�6|   $	��100��6�   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $@��:/Gestione Palestra;component/Resources/Icons/WhiteSave.png��6   $��14��6`   $��14��6l   �� 5      $��Salva��6    $��5,0,0,0q�6-   5      5      �� 5      -   5        $ btn_elimina��5      #��[ 6*   $��0,10,115,10q�6l   $��Right=�6�   $	��100��6�   .��5      �� $ 
Horizontal=�6   .+�5      �� $B��</Gestione Palestra;component/Resources/Icons/WhiteDelete.png��6   $��14��6b   $��14��6n   �� 5      $��Elimina��6    $��5,0,0,0q�6/   5      5      5      �� 5   
   $��Anamnesi generale��6   $
��Left=�6.   $��
348,10,0,0q�6I   $	��Top=�6]   $	��333��6u   $	��222��6�     	BasicBlue#���  6�   .��5      � .+�5      �� -   5         ��Name$ label_Copy3��5      $��
Lifestyle:��6-   $
��Left=�6B   $��	10,10,0,0q�6]   $	��Top=�6p   �� 5      -   5         ��Name$ cmb_stile_vita��5      $��
125,13,0,0q�63   $	��Top=�6G   $��20��6_   $��3��6k   $��Segoe UI)�6x   
���   �$��4,2,2,2q�6�   $
��Left=�6�   $	��152��6�   �� 5      -   5        $ 
label_Copy��5      $��
obiettivo:��6,   $
��Left=�6A   $��	10,41,0,0q�6\   $	��Top=�6o   �� 5       -   5        $ cmb_obiettivo��5       $��
125,43,0,0q�62   $	��Top=�6F   $��20��6^   $��4��6j   $��Segoe UI)�6w   
���   �$��4,2,2,2q�6�   $
��Left=�6�   $	��152��6�   �� 5!      -   5        $ label_Copy2��5!      $��Preferenze:��6-   $
��Left=�6C   $��
10,128,0,0q�6^   $	��Top=�6r   $��85��6�   	� 5"      -	   5         	�Name$ rdb_0_aerobico��5"      $��Aerobico��66   $
��Left=�6I   $��125,157,0,0q�6d   $	��Top=�6y   $��Center=�6�   $��6��6�   	� 5#      -
   5        $ rdb_1_isotonico��5#      $��	isotonico��67   $
��Left=�6K   $��125,180,0,0q�6f   $	��Top=�6{   $��Center=�6�   $��7��6�   �� 5$      -   5         ��Name$ ckb_2_entrambi��5$      $��Entrambi��63   $
��Left=�6F   $��125,134,0,0q�6a   $	��Top=�6v   $��Center=�6�   6�   �� 5%      -   5        $ label_Copy11��5%      $��
Frequenza:��6.   $
��Left=�6C   $��	10,70,0,0q�6^   $	��Top=�6q   $��88��6�   �� 5&      -   5        $ label_Copy12��5&      $��Durata seduta:��6.   $
��Left=�6G   $��	10,99,0,0q�6b   $	��Top=�6u   "  Xceed.Wpf.Toolkit.TimePicker  5'      -   5          Name$ tp_durata_seduta��5'   !   $
��Left=�6;   $��125,103,0,0q�6V   $	��118��6k   $��Center=�6w   $��9��6�   $��12(�6�   $��1q�6�   $��Segoe UI)�6�   
���   �$��20��6�   $	��Top=�6�   ,  &Xceed.Wpf.Toolkit.Primitives.InputBase   
IsReadOnly .  6  %  Xceed.Wpf.Toolkit.IntegerUpDown  5(      -   5        	  Name$	 nud_frequenza_settimanale��5(   $   $
��Left=�6G   $��
126,73,0,0q�6b   $��55��6v   $��Center=�6�   $��8��6�   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Value$
 3X�6�   $��12(�6�   $��1q�6�   $��Segoe UI)�6�   
���   �$��20��6  $	��Top=�6  $��0q�6)  5)      5*      �� 5+   
   $��Dati sessione��6   $��	10,10,0,0q�6*   $	��Top=�6=   $	��280��6U   $
��Left=�6b   $	��333��6}   #���  6�   .��5,      � 
�������.+�5-      �� -   5        $ label_Copy14��5-      $��Istruttore:��6.   $
��Left=�6D   $��
10,121,0,0q�6_   $	��Top=�6s   �� 5.      -   5        $ cmb_istruttore��5.      $��115,123,10,0q�63   $	��Top=�6I   $��20��6a    �� SelectedIndex$ 0��6m   $��11��6   $��Segoe UI)�6�   
���   �$��4,2,2,2q�6�   �� 5/      -   5        $ label4��5/      $��Data:��6(   $
��Left=�68   $��
10,150,0,0q�6S   $	��Top=�6g   (  "System.Windows.Controls.DatePicker  50      -   5          Name$ dp_data��50      $��115,151,0,0q�6.   $	��Top=�6C   
�������$��12��6n   $
��Left=�6|   $	��165��6�   ��51      �� 52       �� Color# � ��6*   54      �� 55      -   5        $ label_Copy10��55      $��Note:��6.   $
��Left=�6>   $��
10,179,0,0q�6Y   $	��Top=�6m   �� 56      -   5         ��Name$ txt_annotazioni��56      $��61��63   $��115,183,10,0q�6?    �� TextWrapping$
 Wrap=�6U   $��TextBox
Asd
Asd��6i   $	��Top=�6�   � 57      -   5         �Name$ grid_img��57      $
��Left=�6)   $	��100��6D   $��	10,10,0,0q�6Q   $	��Top=�6d   $	��100��6|   #���   6�   �� 58      $>��8Nome:		---
Cognome:	---
Sesso:		---
Data nascita:	---��6   $��115,10,10,0q�6�   $	��Top=�6�   $	��100��6�   59      5:      �� 5;   
   $��Anamnesi clinica��6   $��348,237,0,0q�6-   $	��Top=�6B   $	��429��6Z   $
��Left=�6g   $	��333��6�   #���  6�   .��5<      � .+�5=      �� -   5         ��Name$ 	textBlock��5=      $
��Left=�6/   $��	10,10,0,0q�6J   $
��Wrap=�6]   $	��Top=�6q   
���   �$��Patologie riscontrate:��6�   �� 5>      -   5        $ textBlock_Copy��5>      $
��Left=�64   $��
10,162,0,0q�6O   $
��Wrap=�6c   $��Farmaci assunti:��6w   $	��Top=�6�   
���   �$	��275��6�   �� 5?      -   5        $ btn_elimina_patologia��5?      $��Elimina��68   $��Right=�6J   $��0,134,12.8,0q�6f   $	��Top=�6|   $��75��6�   �� 5@      -   5        $ btn_aggiungi_patologia��5@      $��Aggiungi��69   $
��Left=�6L   $��
10,134,0,0q�6g   $	��Top=�6{   $��75��6�   6�   �� 5A      -   5        $ btn_elimina_farmaco��5A      $��Elimina��66   $��Right=�6H   $��0,295,12.8,0q�6d   $	��Top=�6z   $��75��6�   �� 5B      -   5        $ btn_aggiungi_farmaco��5B      $��Aggiungi��67   $
��Left=�6J   $��
10,295,0,0q�6e   $	��Top=�6y   $��75��6�   6�   �� 5C      -   5        $ ckb_bevitore��5C      $��bevitore��61   $
��Left=�6D   $��
10,341,0,0q�6_   $	��Top=�6s   $��5,0,0,0q�6�   $��Center=�6�   �� 5D      -   5        $ ckb_fumatore��5D      $��fumatore��61   $
��Left=�6D   $��
10,365,0,0q�6_   $	��Top=�6s   $��5,0,0,0q�6�   $��Center=�6�   �� 5E      -   5        $ btn_modifica_patologia��5E      $��Modifica��69   $
��Left=�6L   $��
90,134,0,0q�6g   $	��Top=�6{   $��75��6�   �� 5F      -    5        $ btn_modifica_farmaco��5F      $��Modifica��67   $
��Left=�6J   $��
90,295,0,0q�6e   $	��Top=�6y   $��75��6�   �� 5G      -!   5         ��Name$ lbx_patologie��5G      $	��100��61   $��10,29,12.8,0q�6>   $	��Top=�6T   �� 5H      -"   5        $ lbx_farmaci��5H      $	��109��6/   $��10,181,12.8,0q�6<   $	��Top=�6S   5I      5J      �� 5K   
   $��Anamnesi fisiologica��6   $
��Left=�61   $	��371��6L   $��
10,295,0,0q�6Y   $	��Top=�6m   $	��333��6�   #���  6�   .��5L      � .+�5M      �� -#   5        $ label_Copy1��5M      $��	Peso(kg):��6-   $
��Left=�6A   $��	10,39,0,0q�6\   $	��Top=�6o   �� 5N      -$   5         ��Name$ 
slide_peso��5N      $��198,42,10,0q�6-   $	��Top=�6B    � Maximum$	 130Q�6�    � Minimum$ 40Q�6�    � Value$ 70Q�6�    �� IsSnapToTickEnabled . $��20��6�   $��2��6�    �� TickFrequency$	 0.1Q�6   �� TickPlacement$ BottomRight=�6-   � LargeChange$	 0.1Q�6I  ��6Z   Z� ��6p   Z� ��6�   Z� 6I  $  Xceed.Wpf.Toolkit.DoubleUpDown  5O      -%   5          Name$ nud_peso��5O   #   $
��Left=�65   $��
126,42,0,0q�6P   $��55��6d   $��Center=�6o   $��1��6�   $��12(�6�   $��1q�6�   $��Segoe UI)�6�   
���   �$��20��6�   $	��Top=�6�   $��0q�6  6  �� 5P      -&   5        $ label_Copy9��5P      $��Altezza(cm):��6-   $
��Left=�6D   $��	10,68,0,0q�6_   $	��Top=�6r   �� 5Q      -'   5        $ slide_altezza��5Q      $��198,71,10,0q�60   $	��Top=�6E   $	 220Q�6�   $	 100Q�6�   $	 170Q�6�    . $��20��6�   $��2��6�   $ BottomRight=�6   � SmallChange$ 1Q�6:  ��6]   Z� ��6s   Z� ��6�   Z� 6:    5R      -(   5        $	 nud_altezza��5R   $   $
��Left=�69   $��
125,71,0,0q�6T   $��55��6h   $��Center=�6s   $��2��6�   $��12(�6�   $��1q�6�   $��Segoe UI)�6�   
���   �$��20��6�   $	��Top=�6�   $��0q�6  6  �� 5S      -)   5        $ label_Copy16��5S      $��Somatotipo:��6.   $
��Left=�6D   $��	10,10,0,0q�6_   $	��Top=�6r   �� 5T      -*   5        $ cmb_somatotipo��5T      $��125,13,10,0q�63   $	��Top=�6H   $��20��6`   $��3��6l   $��Segoe UI)�6y   
���   �$��4,2,2,2q�6�   �� 5U      -+   5        $ lbl_bmi��5U      $$��Valore:	--
Stato:	------------��6)   $
��Left=�6^   $��
10,126,0,0q�6y   $	��Top=�6�   �� 5V      -,   5        $ label1_Copy2��5V      $��Indice di massa corporea��6.   $
��Left=�6Q   $��	10,97,0,0q�6l   $	��Top=�6   $
��Bold"�6�   �� 5W      --   5        $ label1_Copy3��5W      $ ��Calcolo metabolismo basale��6.   $
��Left=�6S   $��
10,173,0,0q�6n   $	��Top=�6�   $
��Bold"�6�   �� 5X      -.   5        $ lbl_met��5X      $��Valore:	----��6)   $
��Left=�6D   $��
10,202,0,0q�6_   $��26��6s   $	��Top=�6   �� 5Y      -/   5        $ label1_Copy4��5Y      $ ��Frequenza cardiaca massima��6.   $
��Left=�6S   $��10,228,0,-4q�6n   $	��Top=�6�   $
��Bold"�6�   �� 5Z      -0   5        $ lbl_fc��5Z      $I��CFC massima:	---
Fondo lento:	---
Fondo medio:	---
Fondo veloce:	---��6(   $
��Left=�6�   $��10,257,0,-88q�6�   $	��Top=�6�   5[      5\      �� 5]   
   $��Anamnesi sportiva��6   $��1023,10,0,0q�6.   $	��Top=�6C   $	��222��6[   $
��Left=�6h   $	��332��6�   #���  6�   .��5^      � .+�5_      �� -1   5        $ textBlock_Copy1��5_      $
��Left=�65   $��	10,10,0,0q�6P   $
��Wrap=�6c   $#��Attività sportive praticate:��6w   $	��Top=�6�   
���   �6�   �� 5`      -2   5        $ btn_elimina_sport��5`      $��Elimina��64   $��Right=�6F   $��0,164,10.2,0q�6b   $	��Top=�6x   $��75��6�   �� 5a      -3   5        $ btn_aggiungi_sport��5a      $��Aggiungi��65   $
��Left=�6H   $��
10,164,0,0q�6c   $	��Top=�6w   $��75��6�   6�   �� 5b      -4   5        $ btn_modifica_sport��5b      $��Modifica��65   $
��Left=�6H   $��
90,164,0,0q�6c   $	��Top=�6w   $��75��6�   �� 5c      -5   5        $ 	lbx_sport��5c      $	��130��6-   $��10,29,10.2,0q�6:   $	��Top=�6P   5d      5e      �� 5f   
   $��Anamnesi alimentare��6   $��
686,10,0,0q�60   $	��Top=�6D   $	��554��6\   $
��Left=�6i   $	��332��6�   #���  6�   .��5g      � .+�5h      �� -6   5        $ textBlock_Copy2��5h      $
��Left=�65   $��	10,10,0,0q�6P   $
��Wrap=�6c   $��Alimenti comuni��6w   $	��Top=�6�   
���   �6�   �� 5i      -7   5        $ btn_elimina_alimento��5i      $��Elimina��67   $��Right=�6I   $��0,143,10.4,0q�6e   $	��Top=�6{   $��75��6�   �� 5j      -8   5        $ btn_aggiungi_alimento��5j      $��Aggiungi��68   $
��Left=�6K   $��
10,143,0,0q�6f   $	��Top=�6z   $��75��6�   6�   �� 5k      -9   5        $ btn_modifica_alimento��5k      $��Modifica��68   $
��Left=�6K   $��
90,143,0,0q�6f   $	��Top=�6z   $��75��6�   �� 5l      -:   5        $ lbx_alimenti��5l      $	��109��60   $��10,29,10.4,0q�6=   $	��Top=�6S   �� 5m      -;   5        $ textBlock_Copy3��5m      $
��Left=�65   $��
10,186,0,0q�6P   $
��Wrap=�6d   $	��Top=�6x   
���   ��� 6�   $�Intolleranze e��6�   �� 6�   $� ��6�   �� 6�   $�allergie��6�   6�   �� 5n      -<   5        $ btn_elimina_intolleranza��5n      $��Elimina��6;   $��Right=�6M   $��0,319,10.2,0q�6i   $	��Top=�6   $��75��6�   �� 5o      -=   5        $ btn_aggiungi_intolleranza��5o      $��Aggiungi��6<   $
��Left=�6O   $��
10,319,0,0q�6j   $	��Top=�6~   $��75��6�   6�   �� 5p      ->   5        $ btn_modifica_intolleranza��5p      $��Modifica��6<   $
��Left=�6O   $��
90,319,0,0q�6j   $	��Top=�6~   $��75��6�   �� 5q      -?   5        $ lbx_intolleranze��5q      $	��109��64   $��10,205,10.2,0q�6A   $	��Top=�6X   �� 5r      -@   5        $ textBlock_Copy4��5r      $
��Left=�65   $��
10,362,0,0q�6P   $
��Wrap=�6d   $	��Top=�6x   
���   �$��Integratori e supplementi��6�   �� 5s      -A   5        $ btn_elimina_integratore��5s      $��Elimina��6:   $��Right=�6L   $��0,495,10.2,0q�6h   $	��Top=�6~   $��75��6�   �� 5t      -B   5        $ btn_aggiungi_integratore��5t      $��Aggiungi��6;   $
��Left=�6N   $��
10,495,0,0q�6i   $	��Top=�6}   $��75��6�   6�   �� 5u      -C   5        $ btn_modifica_integratore��5u      $��Modifica��6;   $
��Left=�6N   $��
90,495,0,0q�6i   $	��Top=�6}   $��75��6�   �� 5v      -D   5        $ lbx_integratori��5v      $	��109��63   $��10,381,10.2,0q�6@   $	��Top=�6W   5w      5x      5z      5{      