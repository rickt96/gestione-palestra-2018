using System; using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Data;
using System.Windows;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra per la modifica e l'inserimento di avvisi personali o pubblici
    /// </summary>
    public partial class WindowAvviso : Window
    {
        /*
         * Note di funzionamento:
         * la finestra unifica le funzioni delle due modalita di avvisi: personali e comunicazioni
         * nel "costruttore" viene deciso se è una modifica o un inserimento
         * in "Window_Loaded" vengono applicati i dati (form filling e assegnazione info)
         * 
         * Gestione selezione istruttori destinatari:
         * parte un po' merdosa in verità
         * nell'ordine:
         * 1. in Window_Loaded, nella parte 1 viene richiamata la lista degli istruttori, e viene aggiunto un campo per la checkbox
         * 2. se è una modifica carica gli istruttori impostando il check e la dataLettura se sono presenti nei destinatari dell avviso (a.Destinatari)
         * 3. dopo le operazioni assegna l'itemSource al datagrid dei destinatari
         * 4. nel salvataggio viene passato in foreach il datagrid, i row true vengono aggiunti ai destinatari (a.Destinatari)
         * 
         * se è cecked il personale viene disabilitata la grid in basso per i destinatari
         * altrimenti se è una comunicazione viene abilitata e la lista caricata
         */

        Avviso a;
        FormAction _funzione;
        DataTable DtIstruttori;

        /// <summary>
        /// inserimento nuovo avviso
        /// </summary>
        /// <param name="isPersonal">imposta la visibilità dell'avviso</param>
        public WindowAvviso(bool isPersonal)
        {
            InitializeComponent();
            _funzione = FormAction.insert;
            a = new Avviso();
            a.isPersonal = isPersonal;
        }


        /// <summary>
        /// modifica avviso esistente
        /// </summary>
        /// <param name="id">id dell'avviso</param>
        public WindowAvviso(int id)
        {
            InitializeComponent();
            _funzione = FormAction.update;
            a = new Avviso();
            a.PKAvviso = id;
        }


        /// <summary>
        /// caricamento finestra: filling dei controlli, caricamneto oggetto
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //**************************
            //1. fill controlli 
            //**************************

            //tipologia
            Common.PopulateComboBox(ref cmb_tipologia, TipiController.GetTipi(TipiStati.TipologieAvvisi), "Valore");

            //data
            dtp_data.Value = DateTime.Now;

            //elenco clienti
            Common.PopulateComboBox(ref cmb_cliente, ClienteController.GetListClientiBase(""), "NomeCompleto");

            //elenco istruttori - aggiunta colonna per gestire la selezione
            DtIstruttori = IstruttoriController.GetTableIstruttori(Session.User.PKIstruttore);
            DtIstruttori.Columns.Add("Selezionato", typeof(bool));
            DtIstruttori.Columns.Add("DataLettura", typeof(string));
            DtIstruttori.Columns["DataLettura"].AllowDBNull = true;
            DtIstruttori.Columns["Selezionato"].DefaultValue = false;
            foreach (DataRow dr in DtIstruttori.Rows)
                dr["Selezionato"] = false;


            //**************************
            //2. se non è admin non puo dare avvisi urgenti
            //**************************
            //if (Session.IstruttoreAttivo.FKLivelliPermessi != 0)
            //    rdb_priorita_1.IsEnabled = false;


            //**************************
            //3. applicazione grafica e caricamento oggetto
            //**************************
            switch (_funzione)
            {
                case FormAction.insert:
                    Title = "Nuovo avviso";
                    btn_salva.ToolTip = "Inserisci avviso";
                    btn_elimina.IsEnabled = false;
                    break;

                case FormAction.update:
                    Title = "Modifica avviso";
                    btn_salva.ToolTip = "Salva modifica";
                    a = AvvisoController.Select(a.PKAvviso);
                    if (a != null)
                    {
                        //dati base
                        txt_titolo.Text = a.Titolo;
                        foreach (Tipo t in cmb_tipologia.Items)
                            if (t.PKTipo == a.FKTipo)
                                cmb_tipologia.SelectedItem = t;                 
                        dtp_data.Value = a.Data;
                        txt_descrizione.Text = a.Descrizione;

                        //cliente
                        foreach (ClienteBase cli in cmb_cliente.Items){
                            if (cli.PKCliente == a.FKCliente){
                                cmb_cliente.SelectedItem = cli;
                                break;
                            }
                        }

                        //priorita
                        if (a.Priorita == 0)
                            rdb_priorita_0.IsChecked = true;
                        if (a.Priorita == 1)
                            rdb_priorita_1.IsChecked = true;

                        //impostazione destinatari avviso
                        if (a.Destinatari.Count > 0 && _funzione == FormAction.update)
                        {
                            foreach (DataRow dr in DtIstruttori.Rows){
                                foreach (AvvisoIstruttore ai in a.Destinatari){
                                    if (Convert.ToInt16(dr["#"]) == ai.FKIstruttoreDestinatario){
                                        dr["Selezionato"] = true;
                                        dr["DataLettura"] = (ai.DataLettura.HasValue)? ai.DataLettura.Value.ToString("yyyy/MM/dd hh:mm:ss") : string.Empty;//(ai.DataLettura.HasValue) ? ai.DataLettura.Value : DBNull.Value;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            //asegnazione datagrid (solo dopo aver fatto le modifiche)
            dg_istruttori_dest.ItemsSource = null;
            dg_istruttori_dest.ItemsSource = DtIstruttori.AsDataView();

            //visibilita
            if (a.isPersonal == true)
                rdb_visibilita_0_personale.IsChecked = true;
            else
                rdb_visibilita_1_pubblica.IsChecked = true;


            //4. permessi accesso
            if (_funzione == FormAction.update && a.FKIstruttore != Session.User.PKIstruttore)
            {
                MessageBox.Show("Non è possibile eseguire operazioni sugli avvisi generati da altri istruttori", "Avviso", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }


        #region HEADER

       
        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion


        #region CONTROLLI FORM

        private void rdb_visibilita_0_personale_Checked(object sender, RoutedEventArgs e)
        {
            if(this.IsLoaded)
            {
                dg_istruttori_dest.IsEnabled = false;
                dg_istruttori_dest.ItemsSource = null;
            }
                
        }
        private void rdb_visibilita_0_personale_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                dg_istruttori_dest.IsEnabled = true;
                dg_istruttori_dest.ItemsSource = DtIstruttori.AsDataView();
            }
        }


        #endregion

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            //controllo errori
            string error_caption = "Impossibile salvare le modifiche:";
            int start_count = error_caption.Length;
            if (dtp_data.Value == null)
                error_caption += "\n-La data non è valida";
            if (txt_descrizione.Text == "")
                error_caption += "\n-Descrizione del messaggio non presente";
            if (start_count < error_caption.Length)
            {
                MessageBox.Show(error_caption, "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            //*****************************
            //aggiornamento oggetto
            //*****************************

            //1. dati
            a.FKTipo = (cmb_tipologia.SelectedIndex > -1) ? (cmb_tipologia.SelectedItem as Tipo).PKTipo : (int?)null;
            a.Data = (DateTime)dtp_data.Value;
            a.Titolo = txt_titolo.Text;
            a.Descrizione = txt_descrizione.Text;
            a.FKIstruttore = Session.User.PKIstruttore;

            //cliente
            a.FKCliente = (cmb_cliente.SelectedIndex > -1) ? (cmb_cliente.SelectedItem as ClienteBase).PKCliente : (int?)null;

            //visibilita
            a.isPersonal = (rdb_visibilita_0_personale.IsChecked == true)
                ? true
                : false;

            //priorita
            if (rdb_priorita_0.IsChecked == true)
                a.Priorita = 0;
            if (rdb_priorita_1.IsChecked == true)
                a.Priorita = 1;

            //inserimento destinatari
            a.Destinatari.Clear();
            if (a.isPersonal == false)
            {
                //aggiunge ai destinatari dell'avviso quelli selezionati dal datagrid
                foreach (DataRow dr in DtIstruttori.Rows)
                {
                    if (Convert.ToBoolean(dr["Selezionato"]) == true)
                        a.Destinatari.Add(new AvvisoIstruttore()
                        {
                            FKAvviso = a.PKAvviso,
                            FKIstruttoreDestinatario = Convert.ToInt32(dr["#"]),
                            DataLettura = (dr["DataLettura"].ToString() != string.Empty) ? Convert.ToDateTime(dr["DataLettura"]) : (DateTime?)null
                        });
                }
            }


            //scrittura db
            int result = -1;
            switch (_funzione)
            {
                case FormAction.insert:
                    result = AvvisoController.insert(a);
                    break;

                case FormAction.update:
                    result = AvvisoController.Update(a);
                    break;
            }

            if (result >= 0)
                this.Close();
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (_funzione != FormAction.update)
                return;

            int res = AvvisoController.Delete(a.PKAvviso);
            if (res > 0)
                this.Close();
        }
    }
}
