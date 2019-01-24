using System.Windows;
using GestionePalestra.MVC;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra con i dati dell'istruttore
    /// </summary>
    public partial class WindowPannelloIstruttore : Window
    {
        string caption = "istruttore";
        

        public WindowPannelloIstruttore()
        {
            InitializeComponent();   
        }


        /// <summary>
        /// filling dei controlli,
        /// caricamento immagine,
        /// refresh liste
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //form filling
            cmb_sesso.ItemsSource = Common.Sesso;

            //controllo login
            if (Session.User == null)
            {
                MessageBox.Show("Nessun istruttore ha effettuato l'accesso","Errore",MessageBoxButton.OK,MessageBoxImage.Error);
                this.Close();
            }
            else
            {
                //dati istruttore
                txt_nome.Text = Session.User.Nome;
                txt_cognome.Text = Session.User.Cognome;
                cmb_sesso.SelectedIndex = (Session.User.Sesso.HasValue) ? Session.User.Sesso.Value : -1;
                txt_citta_nasc.Text = Session.User.Citta;
                dp_data_nasc.SelectedDate = Session.User.DataNascita;
                txt_tel.Text = Session.User.Telefono;
                txt_email.Text = Session.User.Email;

                //immagine
                Common.SetGridImage(ref grid_img, Session.User.Immagine);
            }

            
        }

        #region FUNZIONI HEADER

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            //aggiornamento dati
            Session.User.Nome = txt_nome.Text;
            Session.User.Cognome = txt_cognome.Text;
            Session.User.Sesso = cmb_sesso.SelectedIndex;
            Session.User.Citta = txt_citta_nasc.Text;
            Session.User.DataNascita = dp_data_nasc.SelectedDate;
            Session.User.Telefono = txt_tel.Text;
            Session.User.Email = txt_email.Text;

            //scrittura db
            if(IstruttoriController.Modifica(Session.User) > 0)
            {
                Message.Alert(DialogType.update, caption);
                this.Close();
            }
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion


        #region BOTTONI AVVISI
        //private void btn_promemoria_nuovo_Click(object sender, RoutedEventArgs e)
        //{
        //    new WindowAvviso( Session.IstruttoreAttivo.id_istruttore, Common.FunzioneFinestra.Inserimento).Show();
        //}
        //private void btn_promemoria_modifica_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dg_avvisi_personali.SelectedIndex == -1)
        //        return;

        //    DataRowView dataRow = (DataRowView)dg_avvisi_personali.SelectedItem;
        //    int id = Convert.ToInt16(dataRow["#"]);
        //    new WindowAvviso(id, Common.FunzioneFinestra.Modifica).ShowDialog();
        //    RefreshAvvisiPubblici();

        //}
        //private void btn_promemoria_elimina_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dg_avvisi_personali.SelectedIndex == -1)
        //        return;

        //    DataRowView dataRow = (DataRowView)dg_avvisi_personali.SelectedItem;
        //    int id = Convert.ToInt16(dataRow["#"]);

        //    if (FactoryAvviso.Elimina(id, true) == true)
        //        RefreshAvvisiPubblici();

        //}
        //private void btn_leggi_avviso_Click(object sender, RoutedEventArgs e)
        //{

        //}
        #endregion


        #region IMMAGINE
        
        private void btn_carica_immagine_Click(object sender, RoutedEventArgs e)
        {
            string file = Common.OpenFileDialogImage();
            if(file != "")
            {
                Session.User.Immagine = Common.ByteFromFile(file);
                Common.SetGridImage(ref grid_img, Session.User.Immagine);
            }
        }

        private void btn_rimuovi_immagine_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.delete, caption) == true)
            {
                Session.User.Immagine = null;
                Common.SetGridImage(ref grid_img, Session.User.Immagine);
            }     
        }

        #endregion


        #region ANAGRAFICA
        private void btn_credenziali_Click(object sender, RoutedEventArgs e)
        {
            new WindowCredenziali(Session.User.PKIstruttore).ShowDialog();
        }
        #endregion

        
    }
}
