using System.Windows;


namespace GestionePalestra
{
    /// <summary>
    /// Logica di interazione per WindowConnessione.xaml
    /// </summary>
    public partial class WindowConnessione : Window
    {
        public WindowConnessione()
        {
            InitializeComponent();

        }


        /// <summary>
        /// al caricamento della finestra applica i dati della connessione attuale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt_ip.Text = Database.DB_HOST;
            int_porta.Value = Database.DB_PORT;
            txt_db.Text = Database.DB_NAME;
            txt_uid.Text = Database.DB_USER;
            txt_pw.Password = Database.DB_PASSWORD;

        }


        /// <summary>
        /// esegue una connessione di test con la configurazione selezionata
        /// </summary>
        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            if (Database.TestConnection(txt_ip.Text, (int)int_porta.Value, txt_db.Text, txt_uid.Text, txt_pw.Password) == true)
                Message.Alert(AlertType.info, "Connessione stabilita correttamente", "test connessione");
            else
                Message.Alert(AlertType.warning, "Impossibile stabilire la connessione", "test connessione");

        }


        /// <summary>
        /// applica la connessione differenziando i due bottoni:
        /// OK: apre una nuova connessione senza modificare il file (valida quindi fino alla chiusura)
        /// APPLICA: apre una nuova connessione e salva le modifiche sul file
        /// </summary>
        private void connessione_click(object sender, RoutedEventArgs e)
        {
            if (Database.TestConnection(txt_ip.Text, (int)int_porta.Value, txt_db.Text, txt_uid.Text, txt_pw.Password) == true)
            {
                //scrive i paramentri
                Database.DB_HOST = txt_ip.Text;
                Database.DB_USER = txt_uid.Text;
                Database.DB_PORT = (int)int_porta.Value;
                Database.DB_PASSWORD = txt_pw.Password;
                Database.DB_NAME = txt_db.Text;

                //[inserire controllo se il salvataggio va a buon fine]
                Database.SaveCfg();

                //chiude la connessione corrente
                Database.Close();

                //inizializza e riapre la nuova
                Database.Open();

                Message.Alert(AlertType.info, "Impostazioni salvate correttamente", "connessione");
                this.Close();
            }
            else
            {
                Message.Alert(AlertType.warning, "Impossibile salvare le modifiche:\nLa configurazione non è valida", "test connessione");

            }
        }

       
    }
}
