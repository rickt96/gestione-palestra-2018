using System; using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra per la visualizzazione e la modifica di un'anamnesi
    /// </summary>
    public partial class WindowAnamnesi : Window
    {
        string caption = "anamnesi";
        Anamnesi p;
        Cliente c;
        FormAction _action;



        /// <summary>
        /// crea o modifica una anamnesi
        /// </summary>
        /// <param name="action">insert o update</param>
        /// <param name="id">id cliente se insert | id anamnesi se update</param>
        public WindowAnamnesi(FormAction action, int id)
        {
            InitializeComponent();

            p = new Anamnesi();
            c = new Cliente();

            switch (action)
            {
                case FormAction.insert:
                    p.FKCliente = id;
                    c.PKCliente = id;
                    _action = FormAction.insert;
                    break;

                case FormAction.update:
                    p.PKAnamnesi = id;
                    _action = FormAction.update;
                    break;
            }
        }

        /// <summary>
        /// caricamento finestra
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool error = false;

            //
            // caricamento controlli
            //
            Common.PopulateComboBox(ref cmb_stile_vita, Common.Lifestyle, "Valore");
            Common.PopulateComboBox(ref cmb_obiettivo, Common.Obiettivi, "Valore");
            Common.PopulateComboBox(ref cmb_somatotipo, Common.Somatotipi, "Valore");
            Common.PopulateComboBox(ref cmb_istruttore, IstruttoriController.GetListIstruttori(), "NomeCompleto");
            nud_altezza.Value = (int)slide_altezza.Value;
            nud_peso.Value = slide_peso.Value;
            dp_data.SelectedDate = DateTime.Now;

            
            //
            // applicazione casi
            //
            switch(_action)
            {
                case FormAction.insert:
                    {
                        //carica il cliente
                        c = ClienteController.Seleziona(c.PKCliente);
                        if (c != null) {
                            //txt_nome_cliente.Text = c.Nome;
                            //txt_cognome_cliente.Text = c.Cognome;
                            //txt_sesso_cliente.Text = c.SessoToString;
                            //txt_data_nascita_cliente.Text = (c.DataNascita.HasValue) ? c.DataNascita.Value.ToShortDateString() : "";
                            //txt_eta_cliente.Text = c.Eta.Value + " anni";
                            //Common.SetGridImage(ref grid_img, c.Immagine);
                        } else {
                            error = true;
                        }   
                        
                        //layout
                        Title = "Nuova anamnesi";
                        btn_elimina.IsEnabled = false;
                    }
                    break;

                case FormAction.update:
                    {
                        //carica anamnesi
                        p = AnamnesiController.Select(p.PKAnamnesi);
                        if (p != null) {
                            //dg_patologie.ItemsSource = null;
                            //p.Patologie.Add(new ElementoAnamnesi() { Valore = "asd" });
                            //dg_patologie.ItemsSource = p.Patologie;
                            //dg_farmaci.ItemsSource = null;
                            //dg_farmaci.ItemsSource = p.Farmaci;
                            //txt_annotazioni.Text = p.Annotazioni;
                            //ckb_fumatore.IsChecked = p.Fumatore;
                            //ckb_bevitore.IsChecked = p.Bevitore;
                            //anamnesi cliente
                            //dp_data.SelectedDate = p.Data;
                            //foreach (Istruttore i in cmb_istruttore.Items)
                            //    if (i.PKIstruttore == p.FKIstruttore)
                            //        cmb_istruttore.SelectedItem = i;

                            ////anamnesi generale
                            //if (p.FKStileDiVita.HasValue)
                            //    foreach (Tipo t in cmb_stile_vita.Items)
                            //        if (t.PKTipo == p.FKStileDiVita)
                            //            cmb_stile_vita.SelectedItem = t;
                            //if (p.FKObiettivo.HasValue)
                            //    foreach (Tipo t in cmb_obiettivo.Items)
                            //        if (t.PKTipo == p.FKObiettivo)
                            //cmb_stile_vita.SelectedItem = t;
                            //nud_frequenza_settimanale.Value = p.FrequenzaSettimanale;
                            //tp_durata_seduta.Value = p.DurataSeduta;
                            //switch (p.Preferenze)
                            //{
                            //    case 0: rdb_0_aerobico.IsChecked = true; break;
                            //    case 1: rdb_1_isotonico.IsChecked = true; break;
                            //    case 2: ckb_2_entrambi.IsChecked = true; break;
                            //    default: ckb_2_entrambi.IsChecked = true; break;
                            //}
                            //anamnesi sportiva
                            //dg_attivita.ItemsSource = null;
                            //dg_attivita.ItemsSource = p.Attivita;
                            //cmb_somatotipo.SelectedIndex = (p.Somatotipo.HasValue) ? p.Somatotipo.Value : -1;
                            //slide_peso.Value = (p.Peso.HasValue) ? p.Peso.Value : 0;
                            //slide_altezza.Value = (p.Altezza.HasValue) ? p.Altezza.Value : 0;
                        }
                        else {
                            error = true;
                        } 

                        //layout
                        Title = "Modifica anamnesi";
                        dp_data.IsEnabled = false;
                    }
                    break;
            }

            if(error)
            {
                Message.Alert(AlertType.error, "Errore durante il caricamento", caption);
            }

            ///////////////////////////////////////////////////////////////////////////
            // caricamento cliente
            //
          


            //dati cliente
            if (c != null)
            {
                
            }

            //anamnesi clinica


            


            if (_action == FormAction.update)
            {
                p = AnamnesiController.Select(p.PKAnamnesi);
                if (p != null)
                {
                    //

                    //indici ad aggiornamento automatico..
                }
                else
                {
                    Message.Alert(AlertType.error, "Impossibile caricare l'errore", caption);
                    this.Close();
                }
            }
        }



        /// <summary>
        /// aggiornamento indici
        /// </summary>
        void AggiornaIndici()
        {
            if (this.IsLoaded != true)
                return;

            //frequenza cardiaca
            if(c.Eta.HasValue)
            {
                List<int> fc = Formule.FrequenzeCardiaca(c.Eta.Value);
                lbl_fc.Content = string.Format("FC massimo:\t{0} bpm\nFondo lento:\t{1} bpm\nFondo medio:\t{2} bpm\nFondo veloce:\t{3} bpm", fc[0], fc[1], fc[2], fc[3]);
            }

            //imc
            Tuple<double, string> bmi = Formule.IndiceMassaCorporea(slide_peso.Value, slide_altezza.Value / 100);
            //txt_imc.Text = bmi.Item1.ToString();
            //txt_imc_descrizione.Text = bmi.Item2.ToString();


            //met
            if (c.Eta.HasValue)
            {
                double met = Formule.CalcoloMetabolismoBasale(this.c.Sesso.Value, this.c.Eta.Value, slide_peso.Value, slide_altezza.Value / 100);
                //txt_met.Text = string.Format("{0} kcal", met);
            }
        }


       

        #region EVENTI CONTROLLI

        private void scorrimento_slider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsLoaded != true)
                return;

            Slider c = (sender as Slider);
            switch (c.Name)
            {
                case "slide_peso": nud_peso.Value = c.Value; break;
                case "slide_altezza": nud_altezza.Value = (int)c.Value; break;
            }

            AggiornaIndici();
        }

        private void ckb_2_entrambi_Checked(object sender, RoutedEventArgs e)
        {
            rdb_0_aerobico.IsChecked = false;
            rdb_0_aerobico.IsEnabled = false;
            rdb_1_isotonico.IsChecked = false;
            rdb_1_isotonico.IsEnabled = false;
        }

        private void ckb_2_entrambi_Unchecked(object sender, RoutedEventArgs e)
        {
            rdb_0_aerobico.IsChecked = true;
            rdb_0_aerobico.IsEnabled = true;
            rdb_1_isotonico.IsChecked = true;
            rdb_1_isotonico.IsEnabled = true;
        }

        private void nud_peso_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            slide_peso.Value = (double)nud_peso.Value;
        }

        private void nud_altezza_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            slide_altezza.Value = (double)nud_altezza.Value;
        }

        private void exp_avanzati_Collapsed(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded != true)
                return;

            this.Height = 728;
            //grb_valori_biometrici.Visibility = Visibility.Hidden;
        }

        private void exp_avanzati_Expanded(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded != true)
                return;

            this.Height = 857;
            //grb_valori_biometrici.Visibility = Visibility.Visible;
        }

        #endregion


        #region BOTTONI FUNZIONI

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            //i controlli perdono il focus per forzare la scrittura dei valori inseriti (altrimenti il timepicker non aggiorna il valore)
            Keyboard.ClearFocus();

            //aggiornamento proprieta dell'oggetto
            p.Peso = nud_peso.Value;
            p.Altezza = nud_altezza.Value;
            //p.FKStileDiVita = (cmb_stile_vita.SelectedIndex > -1) ? (cmb_stile_vita.SelectedItem as Tipo).PKTipo : (int?)null;
            //p.FKObiettivo = (cmb_obiettivo.SelectedIndex > -1) ? (cmb_obiettivo.SelectedItem as Tipo).PKTipo : (int?)null;
            //p.AttivitaPrecenti = txt_attivita_precedenti.Text;
            p.FrequenzaSettimanale = nud_frequenza_settimanale.Value;
            //p.DurataSeduta = TimeSpan.Parse(((DateTime)tp_durata_seduta.Value).ToShortTimeString());   //metodo burino ma serve per trattare a stringa il datetime del controllo e tradurlo in timespan
            //p.Annotazioni = txt_annotazioni.Text;
            //p.Postura = txt_atteggiamenti_posturali.Text;
            //p.Patologie = txt_patologie.Text;
            //p.Alterazioni = txt_alterazioni.Text;
            //p.Raccomandazioni = txt_raccomandazioni.Text;
            p.Data = (dp_data.SelectedDate.HasValue) ? dp_data.SelectedDate.Value : DateTime.Today;

            //double n;
            //if (double.TryParse(txt_girovita.Text, out n))
            //    p.Girovita = double.Parse(txt_girovita.Text);
            //if (double.TryParse(txt_fianchi.Text, out n))
            //    p.Fianchi = double.Parse(txt_fianchi.Text);
            //if (double.TryParse(txt_collo.Text, out n))
            //    p.Collo = double.Parse(txt_collo.Text);
            //if (double.TryParse(txt_coscia_dx.Text, out n))
            //    p.CosciaDx = double.Parse(txt_coscia_dx.Text);
            //if (double.TryParse(txt_coscia_sx.Text, out n))
            //    p.CosciaSx = double.Parse(txt_coscia_sx.Text);
            //if (double.TryParse(txt_massa_magra.Text, out n))
            //    p.MassaMagra = double.Parse(txt_massa_magra.Text);
            //if (double.TryParse(txt_massa_grassa.Text, out n))
            //    p.MassaGrassa = double.Parse(txt_massa_grassa.Text);
            //if (double.TryParse(txt_liquidi.Text, out n))
            //    p.Liquidi = double.Parse(txt_liquidi.Text);

            //p.FKCliente = c.PKCliente;
            p.FKIstruttore = (cmb_istruttore.SelectedItem as Istruttore).PKIstruttore;

            if (ckb_2_entrambi.IsChecked == true)
            {
                p.Preferenze = 2;
            }
            else
            {
                if (rdb_0_aerobico.IsChecked == true)
                    p.Preferenze = 0;
                else
                    p.Preferenze = 1;
            }


            //conferma
            if (Message.Confirm(DialogType.insert, caption) == false)
                return;

            //Scrittura modifiche
            int res = AnamnesiController.InsertUpdate(p);
            if (res > 0)
            {
                Message.Alert(DialogType.update, caption);
                this.Close();
            }
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (_action == FormAction.insert)
                return;

            if (Message.Confirm(DialogType.delete, caption))
            {
                int res = AnamnesiController.Delete(p.PKAnamnesi);
                if (res > 0)
                {
                    Message.Alert(DialogType.delete, caption);
                    this.Close();
                }
            }
        }

        #endregion

        
        private void cmb_somatotipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_somatotipo.SelectedIndex == -1)
                return;

            //txt_somatotipo_descrizione.Text = (cmb_somatotipo.SelectedItem as Tipo).Descrizione;
        }

        private void btn_aggiungi_elemento_lista(object sender, RoutedEventArgs e)
        {
            string val;
            switch((sender as Button).Name)
            {
                case "btn_aggiungi_farmaco":
                    val = Message.InputBox("aggiungi farmaco", "inserisci", "");
                    if (val != "") p.Farmaci.Add(val);
                    break;

                case "btn_aggiungi_patologie":
                    val = Message.InputBox("aggiungi patologia", "inserisci", "");
                    if (val != "") p.Patologie.Add(val);
                    break;

                case "btn_aggiungi_intolleranze":
                    val = Message.InputBox("aggiungi intolleranza", "inserisci", "");
                    if (val != "") p.Intolleranze.Add(val);
                    break;

                case "btn_aggiungi_alimento":
                    val = Message.InputBox("aggiungi alimento", "inserisci", "");
                    if (val != "") p.Alimenti.Add(val);
                    break;

                case "btn_aggiungi_integratore":
                    val = Message.InputBox("aggiungi integratore", "inserisci", "");
                    if (val != "") p.Integratori.Add(val);
                    break;

                case "btn_aggiungi_sport":
                    val = Message.InputBox("aggiungi sport", "inserisci", "");
                    if (val != "") p.Sport.Add(val);
                    break;
            }
        }


        private void btn_modifica_elemento_lista(object sender, RoutedEventArgs e)
        {
            //fare la modifica
            string val;
            switch ((sender as Button).Name)
            {
                case "btn_modifica_farmaco":

                    val = Message.InputBox("modifica farmaco", "inserisci", "");
                    if (val != "") p.Farmaci.Add(val);
                    break;

                case "btn_modifica_patologie":
                    val = Message.InputBox("modifica patologia", "inserisci", "");
                    if (val != "") p.Patologie.Add(val);
                    break;

                case "btn_modifica_intolleranze":
                    val = Message.InputBox("modifica intolleranza", "inserisci", "");
                    if (val != "") p.Intolleranze.Add(val);
                    break;

                case "btn_modifica_alimento":
                    val = Message.InputBox("modifica alimento", "inserisci", "");
                    if (val != "") p.Alimenti.Add(val);
                    break;

                case "btn_modifica_integratore":
                    val = Message.InputBox("modifica integratore", "inserisci", "");
                    if (val != "") p.Integratori.Add(val);
                    break;

                case "btn_modifica_sport":
                    val = Message.InputBox("modifica sport", "inserisci", "");
                    if (val != "") p.Sport.Add(val);
                    break;
            }
        }

    }
}