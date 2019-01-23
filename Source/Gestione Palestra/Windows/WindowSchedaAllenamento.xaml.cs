using Microsoft.VisualBasic;
using System; using GestionePalestra.MVC;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GestionePalestra
{
    /// <summary>
    /// Creazione, modifica, eliminazione, assegnamento e stampa di una scheda di allenamento
    /// </summary>
    public partial class WindowSchedaAllenamento : Window
    {
        /*
         * ordine esecuzione:
         * 1 costruttore 1 (per una scheda nuova):
         *   - valorizza le variabili interne
         *   - istanzia l'oggetto scheda della finestra
         *   
         *   costruttore 2 (modifica scheda esistente):
         *   - valorizza le variabili interne
         *   - instanzia l'oggetto e assegna id scheda passato al costruttore
         *   
         * 2 window_loaded:
         *   - form filling
         *   - caricamento oggetto (se è una modifica)
         *   
         * 
         * Note salvataggio:
         * le operazioni sulle sudute e sulla scheda vengono fatte solo al salvataggio
         * viene letto l'oggetto e apportate le modifiche al DB
         * 
         * Note creazione scheda:
         * visto che non è possibile aggiungere esercizi ad una scheda inesistente l'inserimento funziona cosi:
         * se la finestra è in modalita inserimento viene mostrato solo il groupbox per i dati della scheda
         * quando viene salvata, quindi creata nel db
         * la finestra entra in modalita modifica,
         * viene mostrato il groupbox delle sedute_esercizi e quello del cliente
         * nel caso la scheda sia un modello nono è possibile assegnarla ad un cliente (da vedere se è un sistema valido)
         * 
         * Note etichette schede:
         * vedere "seduta 1", "seduta 2" ecc ecc è lezzo
         * quindi posso convertire da ascii a char partendo da 65
         * es: Convert.ToChar(65) ==> 'A'   
         */

        public bool SchedaInserita = false;               //indica se è stata inserita una scheda in questa sessione, potrei toglierla tra poco
        FormAction _TipoFunzione;
        Scheda s;
        int index_seduta_sel;                             //indice seduta selezionata dalla listbox
        int max_sedute;                                   //numero massimo delle sedute
        int progr_count_gruppi = 0;                       //contatore incrementale per i gruppi di aggrregazione (circuito, superset)

        /// <summary>
        /// Inserimento di una scheda nuova
        /// </summary>
        public WindowSchedaAllenamento()
        {
            InitializeComponent();
            s = new Scheda();
            _TipoFunzione = FormAction.insert;
            Title = "Crezione scheda";
            btn_elimina.IsEnabled = false;
            btn_stampa.IsEnabled = false;

        }


        /// <summary>
        /// modifica di una scheda esistente
        /// </summary>
        /// <param name="id_scheda">id scheda da modificare</param>
        public WindowSchedaAllenamento(int id_scheda)
        {
            InitializeComponent();
            s = new Scheda();
            s.PKScheda = id_scheda;
            _TipoFunzione = FormAction.update;
            Title = "Modifica scheda";
        }


        /// <summary>
        /// caricamento finestra, subito dopo costruttore
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //****************************************
            // form filling
            //****************************************

            //clienti
            Common.PopulateComboBox(ref cmb_cliente, FactoryCliente.GetListClientiBase(""), "NomeCompleto");

            //categorie
            Common.PopulateComboBox(ref cmb_categoria_scheda, FactoryCategorieSchede.GetCategorieSchede(), "Nome");

            //frequenza settimanale
            cmb_freq_sett.ItemsSource = Enumerable.Range(1, 7);

            //numero sedute
            cmb_n_sedute_totale.ItemsSource = Enumerable.Range(1, 31);

            //istruttori
            Common.PopulateComboBox(ref cmb_istruttore, FactoryIstruttore.GetListIstruttori(), "NomeCompleto");

            //impostazione numero massimo sedute
            max_sedute = cmb_freq_sett.Items.Count-1;


            //****************************************
            // caricamento dati
            //****************************************

            //modifica
            if (_TipoFunzione == FormAction.update)
            {
                //caricamento oggetto
                s = FactorySchede.SelezionaSchedaCompleta(s.PKScheda);
                if (s != null)
                {               
                    CaricaDatiBase();       //assegnazione dati base           
                    CaricaSedute();         //assegnazione sedute
                }
                else
                {
                    Message.Alert(AlertType.error, "Nessuna scheda disponibile", "scheda");
                    this.Close();
                }

                //blocco modifica dell'istruttore creatore
                cmb_istruttore.IsEnabled = false;
            }

            //permessi (tenere il codice in fondo altrimenti la scheda non è caricata)
            if (_TipoFunzione == FormAction.update && Session.User.PKIstruttore != s.FKIstruttore)
            {
                MessageBox.Show("Non è possibile modificare una scheda creata da un altro istruttore", "Permessi", MessageBoxButton.OK, MessageBoxImage.Warning);
                foreach(object o in grid_actions.Children)
                {
                    if (o is Button)
                        if ((o as Button).Name != "btn_stampa")
                            (o as Button).IsEnabled = false;
                }
            }
        }


        /******************************************
        * METODI 
        ******************************************/


        /// <summary>
        /// legge le sedute della scheda e crea i controlli in dg_sedute.
        /// imposta anche in maniera progressiva (A,B,C ecc ecc) il nome delle sedute nel caso il nome vero sia vuoto
        /// </summary>
        void CaricaSedute()
        {
            dg_sedute.ItemsSource = null;
            dg_esercizi.ItemsSource = null;
            dg_sedute.ItemsSource = s.Sedute;

            if (dg_sedute.Items.Count > 0)
            {
                if (index_seduta_sel > dg_sedute.Items.Count - 1)
                    dg_sedute.SelectedIndex = dg_sedute.Items.Count - 1;      //se cancello un elemento selezionato in fondo seleziono in automatico l'ultimo elemento presente della lista
                else
                    dg_sedute.SelectedIndex = index_seduta_sel;                //altrimenti seleziona l'ultimo inserito
            }       
        }


        /// <summary>
        /// carica sulla listbox gli esercizi letti dalla seduta dentro la scheda, 
        /// ed applica i counter ai controlli mostrati (parte tosta)
        /// </summary>
        /// <param name="i">indice della seduta in scheda</param>
        void CaricaEserciziSeduta(int index_sed)
        {
            //assegna etichetta scheda
            //lbl_esercizi_seduta.Content = "Esercizi "+s.Sedute[index_sed].Nome;

            //pulizia listbox
            dg_esercizi.ItemsSource = null;
            //ListCollectionView lcv = new ListCollectionView(s.Sedute[index_sed].Esercizi);
            //lcv.GroupDescriptions.Add(new PropertyGroupDescription("Gruppo"));
            dg_esercizi.ItemsSource = s.Sedute[index_sed].Esercizi;

            dg_sedute.Items.Refresh();


        }


        /// <summary>
        /// mostra i dati base della scheda letti dall'oggetto s
        /// </summary>
        void CaricaDatiBase()
        {
            txt_nome.Text = s.Nome;                                         //nome
            txt_obiettivo.Text = s.Obiettivo;                               //obiettivo
            sld_difficolta.Value = (s.Difficolta.HasValue) ? s.Difficolta.Value : 1;                            //difficolta
            cmb_freq_sett.SelectedItem = (s.FrequenzaSettimanale.HasValue) ? s.FrequenzaSettimanale : 1;
            cmb_n_sedute_totale.SelectedItem = (s.NumeroSedute.HasValue) ? s.NumeroSedute : 1;              //numero sedute
            ckb_is_model.IsChecked = s.IsModel;                             //modello
            txt_dettagli.Text = s.Dettagli;


            foreach (Istruttore i in cmb_istruttore.Items)                  //istruttore
                if (i.PKIstruttore == s.FKIstruttore)
                    cmb_istruttore.SelectedItem = i;


            foreach (CategoriaScheda cs in cmb_categoria_scheda.Items)              //categoria scheda
                if (s.FKCategoriaScheda == cs.PKCategoria)
                    cmb_categoria_scheda.SelectedItem = cs;


            dp_data_inizio.SelectedDate = s.DataInizio;                     //data inizio
            dp_data_fine.SelectedDate = s.DataFine;                         //data fine

            for (int i = 0; i < cmb_cliente.Items.Count; i++){
                if (s.FKCliente == (cmb_cliente.Items[i] as ClienteBase).PKCliente){
                    cmb_cliente.SelectedIndex = i;
                    break;
                }
            }
        }


        #region HEADER

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.update, "scheda") == false)
                return;

            //aggiornamento campi oggetto scheda
            s.Nome = txt_nome.Text;
            s.Obiettivo = txt_obiettivo.Text;
            s.Difficolta = (int)sld_difficolta.Value;
            s.FrequenzaSettimanale = (int)cmb_freq_sett.SelectedItem;
            s.NumeroSedute = (int)cmb_n_sedute_totale.SelectedItem;
            s.FKIstruttore = ((Istruttore)cmb_istruttore.SelectedItem).PKIstruttore;
            s.IsModel = (bool)ckb_is_model.IsChecked;
            s.Dettagli = txt_dettagli.Text;
            s.FKCategoriaScheda = (cmb_categoria_scheda.SelectedIndex > -1) ? (cmb_categoria_scheda.SelectedItem as CategoriaScheda).PKCategoria: (int?)null;
            s.DataInizio = dp_data_inizio.SelectedDate;
            s.DataFine = dp_data_fine.SelectedDate;
            s.FKCliente = (cmb_cliente.SelectedIndex > -1) ? Convert.ToInt16(((ClienteBase)cmb_cliente.SelectedItem).PKCliente) : (int?)null;
            //*i dati delle sudute e degli allenamenti sono gia impostati runtime

            //salvataggio
            switch (_TipoFunzione)
            {
                case FormAction.insert:
                    int last_id = FactorySchede.CreaSchedaCompleta(s);
                    if (last_id > 0)
                    {
                        s.PKScheda = last_id;
                        //passa a modalita modifica
                        _TipoFunzione = FormAction.update;
                        btn_elimina.IsEnabled = true;
                        btn_stampa.IsEnabled = true;
                        SchedaInserita = true;
                    }
                    break;

                case FormAction.update:
                    FactorySchede.ModificaSchedaCompleta(s);
                    break;
            }
            Message.Alert(DialogType.update, "scheda");
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.delete, "scheda") == false)
                return;

            if (FactorySchede.Elimina(s.PKScheda) > 0)
                this.Close();
        }

        private void btn_stampa_Click(object sender, RoutedEventArgs e)
        {
            if (s.FKCliente == null)
            {
                MessageBox.Show("E' necessario assegnare la scheda ad un cliente per poterla stampare", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            new WindowStampa(s).ShowDialog();
        }

        private void btn_importa_modello_Click(object sender, RoutedEventArgs e)
        {
            WindowSelezioneModelli sm = new WindowSelezioneModelli();
            sm.ShowDialog();
            if (sm.SchedaSelezionata != null)
            {
                //applicazione modello
                s = sm.SchedaSelezionata;

                /*
                 * [IMPORTANTE] visto che il metodo FactorySchede.Modifica non ricrea indinstintamente le sedute e gli esercizi
                 * è necessario porre a 0 gli id che arrivano dalla selezione dei modelli
                 * altrimenti nelle modifiche rischierei di sovrascrivere sedute ed esercizi appartenenti alla scheda del modello selezionato
                 */

                s.PKScheda = 0;
                foreach (Seduta s in s.Sedute)
                {
                    s.PKSeduta = 0;
                    foreach (EsercizioSeduta es in s.Esercizi)
                        es.PKEsercizioSeduta = 0;
                }

                //applicazione oggetto nuovo
                CaricaDatiBase();
                CaricaSedute();
                MessageBox.Show("Modello importato correttamente", "Modello", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion


        #region DATI SCHEDA

        private void ckb_is_model_Checked(object sender, RoutedEventArgs e)
        {
            cmb_cliente.SelectedIndex = -1;
            dp_data_inizio.IsEnabled = false;
            dp_data_fine.IsEnabled = false;
            btn_stampa.IsEnabled = false;
        }

        private void ckb_is_model_Unchecked(object sender, RoutedEventArgs e)
        {
            cmb_cliente.SelectedIndex = -1;
            dp_data_inizio.IsEnabled = false;
            dp_data_fine.IsEnabled = false;
            btn_stampa.IsEnabled = true;
        }

        private void btn_rimuovi_cliente_Click(object sender, RoutedEventArgs e)
        {
            cmb_cliente.SelectedIndex = -1;
            dp_data_inizio.SelectedDate = null;
            dp_data_fine.SelectedDate = null;
        }

        #endregion


        #region GROUP SEDUTE ESERCIZI

        private void btn_aggiungi_seduta_Click(object sender, RoutedEventArgs e)
        {
            if (s.Sedute.Count > max_sedute)
                return;

            s.Sedute.Add(new Seduta() { Nome="nuova seduta"});
            CaricaSedute();
        }

        private void btn_rinomina_seduta_Click(object sender, RoutedEventArgs e)
        {
            if (dg_sedute.SelectedIndex == -1)
                return;

            //ottiene indice elemento e nome iniziale
            int i = dg_sedute.SelectedIndex;
            string old = s.Sedute[i].Nome;

            //inputbox VB per la modifica del nome
            string newname = Interaction.InputBox("Modifica il nome della seduta", "Modifica", old);
            if (newname != "" && newname != s.Sedute[i].Nome)
            {
                //se è valida aggiorna la proprieta nome della seduta e aggiorna il controllo
                s.Sedute[i].Nome = newname;
            }
        }

        private void btn_elimina_seduta_Click(object sender, RoutedEventArgs e)
        {
            if (dg_sedute.SelectedIndex == -1)
                return;

            int i = dg_sedute.SelectedIndex;
            if(Message.Confirm(DialogType.delete, "scheda"))
            {
                s.Sedute.RemoveAt(i);
                dg_sedute.Items.Refresh();
            }
        }

        private void dg_sedute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_sedute.SelectedIndex == -1)
                return;

            index_seduta_sel = dg_sedute.SelectedIndex;
            CaricaEserciziSeduta(index_seduta_sel);
        }

        private void dg_sedute_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btn_rinomina_seduta_Click(sender, e);
        }

        private void btn_nuovo_esercizio_Click(object sender, RoutedEventArgs e) 
        {
            if (dg_sedute.SelectedIndex == -1)
                return;

            int index_es_sel = dg_esercizi.SelectedIndex;

            //crea nuova finestra per l'aggiunta
            WindowEsercizioSeduta wes = new WindowEsercizioSeduta();
            wes.ShowDialog();

            if (wes.DialogResult == true)
            {
                if(index_es_sel > -1)
                {
                    s.Sedute[index_seduta_sel].Esercizi.Insert(index_es_sel+1, wes.Esercizio);
                }
                else
                {
                    s.Sedute[index_seduta_sel].Esercizi.Add(wes.Esercizio);
                }

                dg_esercizi.Items.Refresh();
                dg_sedute.Items.Refresh();
            }
        }

        private void btn_modifica_esercizio_Click(object sender, RoutedEventArgs e)
        {
            if (dg_esercizi.SelectedIndex == -1)
                return;

            //seleziona l'indice dalla listbox
            int es_i = dg_esercizi.SelectedIndex;

            //creo finestra per la modifica degli eserici (nel secondo parametro dichiaro in line l'array e ci assegno l'eserczio selezionato)
            WindowEsercizioSeduta wes = new WindowEsercizioSeduta(s.Sedute[index_seduta_sel].Esercizi[es_i]);
            wes.ShowDialog();

            if(wes.DialogResult == true)
            {
                s.Sedute[index_seduta_sel].Esercizi[es_i] = wes.Esercizio;
                dg_esercizi.Items.Refresh();
            }
        }

        private void btn_rimuovi_esercizio_Click(object sender, RoutedEventArgs e)
        {
            if (dg_esercizi.SelectedIndex == -1)
                return;

            int i = dg_esercizi.SelectedIndex;
            s.Sedute[index_seduta_sel].Esercizi.RemoveAt(i);

            dg_esercizi.Items.Refresh();
            dg_sedute.Items.Refresh();
        }

        private void btn_unisci_esercizi_Click(object sender, RoutedEventArgs e)
        {
            if (dg_esercizi.SelectedItems.Count <= 1)
                return;

            int tipo = -1;
            if (rdb_1_serie.IsChecked == true)
                tipo = 1;
            if (rdb_2_circuito.IsChecked == true)
                tipo = 2;


            int c = 0;
            switch (tipo)
            {
                case 1: //serie:riferimento a serie del primo es, mantiene per ognuno solo i carichi
                    foreach (EsercizioSeduta es in dg_esercizi.SelectedItems)
                    {
                        es.Gruppo = progr_count_gruppi;
                        es.Metodo = tipo;
                        if (c > 0)
                        {
                            es.Serie = null;
                            es.Ripetizioni = null;
                            es.Recupero = null;
                        }
                        c++;
                    }
                    progr_count_gruppi++;
                    break;

                case 2: //circuito
                    foreach(EsercizioSeduta es in dg_esercizi.SelectedItems)
                    {
                        es.Gruppo = progr_count_gruppi;
                        es.Metodo = tipo;
                        if (c > 0)
                        {
                            es.Serie = null;
                            es.Recupero = null;
                        }
                        c++;
                    }
                    progr_count_gruppi++;
                    break;
            }
            dg_esercizi.Items.Refresh();
        }

        private void btn_move_up_Click(object sender, RoutedEventArgs e)
        {
            if (dg_esercizi.SelectedIndex < 1)
                return;

            EsercizioSeduta tmp = s.Sedute[index_seduta_sel].Esercizi[dg_esercizi.SelectedIndex];
            s.Sedute[index_seduta_sel].Esercizi.RemoveAt(dg_esercizi.SelectedIndex);
            s.Sedute[index_seduta_sel].Esercizi.Insert(dg_esercizi.SelectedIndex - 1, tmp);

            dg_esercizi.Items.Refresh();

        }

        private void btn_move_down_Click(object sender, RoutedEventArgs e)
        {
            if (dg_esercizi.SelectedIndex == -1 || dg_esercizi.SelectedIndex >= dg_esercizi.Items.Count - 1)
                return;

            EsercizioSeduta tmp = s.Sedute[index_seduta_sel].Esercizi[dg_esercizi.SelectedIndex];
            s.Sedute[index_seduta_sel].Esercizi.RemoveAt(dg_esercizi.SelectedIndex);
            s.Sedute[index_seduta_sel].Esercizi.Insert(dg_esercizi.SelectedIndex + 1, tmp);

            dg_esercizi.Items.Refresh();
        }

        private void btn_svuota_esercizi_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.delete, "esercizi seduta") == false)
                return;

            s.Sedute[index_seduta_sel].Esercizi.Clear();
            dg_esercizi.Items.Refresh();
        }

        #endregion

    }
}


/*
controllare se l'input nella casella è numerico
private void txt_n_sedute_PreviewTextInput(object sender, TextCompositionEventArgs e)
{
    //controllo se l'inserimento è solo numerico
    Regex regex = new Regex("[^0-9]+");
    e.Handled = regex.IsMatch(e.Text);
}
 */
