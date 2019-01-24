using Microsoft.Win32;
using System; using GestionePalestra.MVC;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data;
using System.Collections.Generic;

using Microsoft.VisualBasic;

namespace GestionePalestra
{
    /// <summary>
    /// Visualizza la scheda con tutti i dati del cliente ed i collegamenti a schede, anamnesi, test ed attività
    /// </summary>
    public partial class WindowSchedaUtente : Window
    {
        string caption = "profilo cliente";

        //oggetto del cliente
        Cliente c;

        //dati
        DataTable _anamnesiDT;
        DataTable _testDT;
        DataTable _schedeDT;
        List<DocumentoCliente> _documentiLST;


        /// <summary>
        /// carica il profilo completo del cliente specificato
        /// </summary>
        /// <param name="id_cliente">id del cliente da caricare</param>
        public WindowSchedaUtente(int id_cliente)
        {
            InitializeComponent();
            c = new Cliente();
            c.PKCliente = id_cliente;
        }


        
        void RefreshAnamnesi()
        {
            _anamnesiDT = AnamnesiController.GetTableAnamnesiCliente(c.PKCliente);
            dg_anamnesi.ItemsSource = null;
            dg_anamnesi.ItemsSource = _anamnesiDT.DefaultView;
        }

        void RefreshTest()
        {
            _testDT = TestController.GetTableTestCliente(c.PKCliente);
            dg_test.ItemsSource = null;
            dg_test.ItemsSource = _testDT.DefaultView;
        }

        void RefreshSchede()
        {
            _schedeDT = SchedeController.GetTableSchedeCliente(c.PKCliente);
            dg_schede.ItemsSource = null;
            dg_schede.ItemsSource = _schedeDT.DefaultView;
        }

        void RefreshDocumenti()
        {
            _documentiLST = DocumentiController.GetListDocumentiCliente(c.PKCliente);
            dg_documenti.ItemsSource = null;
            dg_documenti.ItemsSource = _documentiLST;
        }


        /// <summary>
        /// caricamento della finestra: esegue tutte le operazioni di form filling
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //****************************
            //form filling 
            //****************************

            //sesso          
            cmb_sesso.ItemsSource = Common.Sesso;

            //istruttore
            Common.PopulateComboBox(ref cmb_istruttore, IstruttoriController.GetListIstruttori(), "NomeCompleto");

            //stati cliente
            Common.PopulateComboBox(ref cmb_stato_cliente, TipiController.GetTipi(TipiStati.StatiCliente), "Valore");


            //caricamento cliente
            c = ClienteController.Seleziona(c.PKCliente);

            //carica immagine
            Common.SetGridImage(ref grid_img, this.c.Immagine);

            //applicazione campi
            if (c != null)
            {
                //verifica permessi: solo se il profilo è stato caricato bene
                if (c.FKIstruttore != Session.User.PKIstruttore/* && Session.IstruttoreAttivo.Permessi != 0*/)
                {
                    //disabilitazione blocchi funzioni
                    //tab_funzioni.IsEnabled = false;
                    //group_anagrafica.IsEnabled = false;
                    //group_iscrizione.IsEnabled = false;
                    //group_iscrizione.IsEnabled = false;
                    //foreach (UIElement el in stp_funzioni.Children)
                    //    if (el is Image) (el as Image).Visibility = Visibility.Collapsed;

                    //immagine avviso blocco
                    //BitmapImage b = new BitmapImage();
                    //b.BeginInit();
                    //b.CacheOption = BitmapCacheOption.OnLoad;
                    //b.UriSource = new Uri(@"pack://application:,,,/Gestione Palestra;component//Gestione Palestra;component/Resources/Icons/WhiteWarning.png", UriKind.Absolute);
                    //b.EndInit();
                    Image i = new Image();
                    i.Source = Common.BitmapFromIcon("WhiteWarning.png");
                    i.ToolTip = "Non puoi apportare modifiche su un cliente appartente ad un altro istruttore";
                    //stp_funzioni.Children.Add(i);
                }

                //anagrafica
                txt_nome.Text = c.Nome;
                txt_cognome.Text = c.Cognome;
                cmb_sesso.SelectedIndex = (c.Sesso.HasValue) ? c.Sesso.Value : -1;
                txt_cf.Text = c.CodiceFiscale;
                txt_citta_nasc.Text = c.CittaNascita;
                txt_note.Text = c.Note;
                dp_data_nasc.SelectedDate = (DateTime)c.DataNascita;

                //contatti
                txt_tel.Text = c.Telefono;
                txt_email.Text = c.Email;

                //iscrizione
                dp_iscr.SelectedDate = (DateTime)c.DataIscrizione;
                foreach(Tipo t in cmb_stato_cliente.Items)
                    if(t.PKTipo == c.FKStato) {
                        cmb_stato_cliente.SelectedItem = t;
                        break;
                    }

                //annotazioni
                txt_note.Text = c.Note;

                //istruttore
                foreach (Istruttore i in cmb_istruttore.Items)
                    if (i.PKIstruttore == c.FKIstruttore)
                        cmb_istruttore.SelectedItem = i;
                

                //campi tabcontrol
                RefreshAnamnesi();
                RefreshTest();
                RefreshSchede();
                RefreshDocumenti();
            }
            else
            {
                MessageBox.Show("Impossibile caricare il profilo", "Clienti", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }



        #region ANAGRAFICA

        private void ControlloInserimentoNumeri(object sender, TextCompositionEventArgs e)
        {
            if (e.Text[0] >= 48 && e.Text[0] <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btn_calcola_cv_Click(object sender, RoutedEventArgs e)
        {
            //string sesso = "";
            //if (cmb_sesso.SelectedIndex == 0)
            //    sesso = "M";
            //else
            //    sesso = "F";

            //txt_cf.Text = CodiceFiscale.GetCodiceFiscale(
            //    txt_cognome.Text,
            //    txt_nome.Text,
            //    sesso,
            //    (DateTime)dp_data_nasc.SelectedDate,
            //    txt_citta_nasc.Text
            //    );
        }

        #endregion



        #region IMMAGINE

        private void btn_carica_immagine_Click(object sender, RoutedEventArgs e)
        {
            string file_path = Common.OpenFileDialogImage();
            if (file_path != "")
            {
                c.Immagine = Common.ByteFromFile(file_path);
                Common.SetGridImage(ref grid_img, c.Immagine);
            }
        }

        private void btn_rimuovi_immagine_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.delete, caption) == true)
            {
                c.Immagine = null;
                Common.SetGridImage(ref grid_img, c.Immagine);
            }
        }

        #endregion



        #region HEADER

        private void img_salva_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void img_indietro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void img_elimina_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
                
        }

        #endregion



        #region ANAMNESI

        private void cmb_sort_anamnesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmb_sort_anamnesi.SelectedIndex < 1)
            //    return;

            //switch (cmb_sort_anamnesi.SelectedIndex)
            //{
            //    case 1:
            //        _anamnesiDT.DefaultView.Sort = "data ASC";
            //        break;
            //    case 2: break;
            //}

        }

        private void btn_nuova_anamnesi_Click(object sender, RoutedEventArgs e)
        {
            Window wa = new WindowAnamnesi(FormAction.insert, c.PKCliente);
            wa.ShowDialog();

            RefreshAnamnesi();
            RefreshTest();               
        }

        private void btn_modifica_anamnesi_Click(object sender, RoutedEventArgs e)
        {
            if (dg_anamnesi.SelectedIndex == -1)
                return;

            DataRowView dataRow = (DataRowView)dg_anamnesi.SelectedItem;
            int id = Convert.ToInt16(dataRow[0]);

            Window wa = new WindowAnamnesi(FormAction.update, id);
            wa.ShowDialog();

            RefreshAnamnesi();
            RefreshTest();
        }

        private void btn_elimina_anamnesi_Click(object sender, RoutedEventArgs e)
        {
            if (dg_anamnesi.SelectedIndex == -1)
                return;

            DataRowView dataRow = (DataRowView)dg_anamnesi.SelectedItem;
            int id = Convert.ToInt16(dataRow[0]);

            if (AnamnesiController.Delete(id) > 0)
            {
                //FactoryStoricoEventi.Inserisci(c.PKCliente, "Anamnesi eliminata");
                RefreshAnamnesi();
                RefreshTest();
            }
                
        }

        private void btn_refresh_anamnesi_Click(object sender, RoutedEventArgs e)
        {
            RefreshAnamnesi();
        }

        #endregion



        #region SCHEDE

        private void cmb_sort_schede_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_apri_scheda_Click(object sender, RoutedEventArgs e)
        {
            if (dg_schede.SelectedIndex == -1)
                return;

            DataRowView dataRow = (DataRowView)dg_schede.SelectedItem;
            int id = Convert.ToInt16(dataRow[0]);
            new WindowSchedaAllenamento(id).ShowDialog();
            RefreshSchede();
        }

        private void btn_annulla_assegnazione_scheda_Click(object sender, RoutedEventArgs e)
        {
            if (dg_schede.SelectedIndex == -1)
                return;

            //todo
        }

        private void btn_stampa_scheda_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)dg_schede.SelectedItem;
            int id = Convert.ToInt16(dataRow[0]);
            Scheda s = SchedeController.SelezionaSchedaCompleta(id);
            new WindowStampa(s).ShowDialog();
        }

        #endregion



        #region TEST

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            if (dg_test.SelectedIndex == -1)
                return;

            DataRowView dataRow = (DataRowView)dg_test.SelectedItem;
            int id = Convert.ToInt16(dataRow[0]);
            new WindowTest(FormAction.insert, c.PKCliente).ShowDialog();
            RefreshTest();
        }

        private void dg_test_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btn_test_Click(sender, e);
        }

        

        private void btn_aggiorna_test_Click(object sender, RoutedEventArgs e)
        {
            RefreshTest();
            
        }

        #endregion



        #region FUNZIONI

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            //impostazione campi oggetto
            c.Nome = txt_nome.Text;
            c.Cognome = txt_cognome.Text;
            c.CodiceFiscale = txt_cf.Text;
            c.Sesso = cmb_sesso.SelectedIndex;
            c.DataNascita = (DateTime)dp_data_nasc.SelectedDate;
            c.CittaNascita = txt_citta_nasc.Text;
            c.Telefono = txt_tel.Text;
            c.Email = txt_email.Text;
            c.FKStato = (cmb_stato_cliente.SelectedIndex > -1) ? (cmb_stato_cliente.SelectedItem as Tipo).PKTipo : (int?)null;
            c.DataIscrizione = dp_iscr.SelectedDate;
            c.Note = txt_note.Text;
            c.FKIstruttore = (cmb_istruttore.SelectedItem as Istruttore).PKIstruttore;

            //Scrittura
            if (ClienteController.Modifica(c) > 0)
            {
                //FactoryStoricoEventi.Inserisci(c.PKCliente, "Modifica profilo cliente");
                Common.LastUpdateClienti = DateTime.Now;
                Message.Alert(DialogType.update, caption);
            }
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (ClienteController.Elimina(c.PKCliente) > 0)
            {
                //FactoryStoricoEventi.Inserisci((int?)null, "Rimosso profilo cliente");
                Common.LastUpdateClienti = DateTime.Now;
                this.Close();
            }
        }




        #endregion



        private void cmb_sort_documenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_carica_file_Click(object sender, RoutedEventArgs e)
        {
            //string file = "";
            OpenFileDialog of = new OpenFileDialog();
            if(of.ShowDialog() == true)
            {
                //file = of.FileName;
                FileInfo fi = new FileInfo(of.FileName);
                DocumentoCliente dc = new DocumentoCliente();
                dc.File = Common.ByteFromFile(of.FileName);
                dc.NomeFile = Path.GetFileNameWithoutExtension(of.FileName);
                dc.EstensioneFile = Path.GetExtension(of.FileName);
                dc.Dimensioni = fi.Length;
                dc.DataCreazione = fi.CreationTime;
                dc.DataInserimento = DateTime.Now;
                dc.FKIstruttore = Session.User.PKIstruttore;
                dc.FKCliente = c.PKCliente;

                if (DocumentiController.Insert(dc) > 0)
                {
                    RefreshDocumenti();
                    //_documentiLST.Add(dc);
                    Message.Alert(DialogType.insert, "Documento");
                }
                    

            }
        }

        private void btn_apri_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("da fare");
        }

        private void btn_scarica_Click(object sender, RoutedEventArgs e)
        {
            if (dg_documenti.SelectedIndex == -1)
                return;

            DocumentoCliente dc = (DocumentoCliente)dg_documenti.SelectedItem;
            Common.SaveFileFromByte(dc.File, dc.NomeCompleto);
            MessageBox.Show("asd");
        }

        private void btn_elimina_file_Click(object sender, RoutedEventArgs e)
        {
            if (dg_documenti.SelectedIndex == -1)
                return;

            if (Message.Confirm(DialogType.delete, caption) == true)
            {
                int id = (dg_documenti.SelectedItem as DocumentoCliente).PKDocumento;
                if (DocumentiController.Delete(id) > 0)
                    RefreshDocumenti();
            }

            
        }

        private void btn_aggiorna_file_Click(object sender, RoutedEventArgs e)
        {
            RefreshDocumenti();
        }

        private void btn_inserisci_test_Click(object sender, RoutedEventArgs e)
        {
            new WindowTest( FormAction.insert, c.PKCliente).ShowDialog();
            RefreshTest();
        }

        private void btn_modifica_test_Click(object sender, RoutedEventArgs e)
        {
            if (dg_test.SelectedIndex == -1)
                return;

            DataRowView dataRow = (DataRowView)dg_test.SelectedItem;
            int id = Convert.ToInt16(dataRow[0]);

            new WindowTest(FormAction.update, id).ShowDialog();
            RefreshTest();
        }

        private void btn_elimina_test_Click(object sender, RoutedEventArgs e)
        {
            if (dg_test.SelectedIndex == -1)
                return;

            if (Message.Confirm(DialogType.delete, "test") == true)
            {
                DataRowView dataRow = (DataRowView)dg_test.SelectedItem;
                int id = Convert.ToInt16(dataRow[0]);

                if (TestController.Delete(id) > 0)
                    RefreshTest();
            }
        }

        private void btn_rinomina_file_Click(object sender, RoutedEventArgs e)
        {
            if (dg_documenti.SelectedIndex == -1)
                return;

            DocumentoCliente c = (DocumentoCliente)dg_documenti.SelectedItem;
            string newname = Interaction.InputBox("Rinomina il documento", "Modifica", c.NomeFile);
            if (newname != "" && newname != c.NomeFile)
            {
                c.NomeFile = newname;
                if (DocumentiController.Rename(c) > 0)
                    RefreshDocumenti();
            }
        }

        private void btn_aggiungi_scheda_Click(object sender, RoutedEventArgs e)
        {
            new WindowSchedaAllenamento().ShowDialog();
            RefreshSchede();
        }
    }
}