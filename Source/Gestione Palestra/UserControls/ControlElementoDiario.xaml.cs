using System; using GestionePalestra.MVC;
using System.Windows;
using System.Windows.Controls;


namespace GestionePalestra
{
    /// <summary>
    /// Controllo custom per la visualizzazione del promemoria
    /// </summary>
    public partial class ControlElementoDiario : UserControl
    {
        public Annotazione a;

        /// <summary>
        /// MODIFICA annotazione esistente
        /// </summary>
        public ControlElementoDiario(Annotazione a)
        {
            InitializeComponent();

            this.a = a;

            //assegnazione dati al form
            txt_titolo.Text = a.Titolo;
            txt_testo.Text = a.Testo;
            ckb_svolto.IsChecked = (bool)a.Svolto;
            lbl_data.Content = (a.Data.HasValue) ? a.Data.Value.ToString("yyyy/MM/dd hh:mm:ss") : "nessuna data";
        }

        /// <summary>
        /// creazione annotazione NUOVA
        /// </summary>
        public ControlElementoDiario()
        {
            InitializeComponent();

            //creazione oggetto
            a = new Annotazione() {
                PKAnnotazione = -1,
                FKIstruttore = Session.User.PKIstruttore,
                Titolo = "Titolo",
                Testo = "Descrizione",
                Data = DateTime.Now,
                Svolto = false
            };

            //assegnazione dati al form
            txt_titolo.Text = a.Titolo;
            txt_testo.Text = a.Testo;
            ckb_svolto.IsChecked = (bool)a.Svolto;
            lbl_data.Content = (a.Data.HasValue) ? a.Data.Value.ToLongDateString() + " " + a.Data.Value.ToShortTimeString() : "";
        }

        /// <summary>
        /// modifica o inserisci l'annotazione
        /// </summary>
        private void btn_salva_annotazione_Click(object sender, RoutedEventArgs e)
        {
            a.Titolo = txt_titolo.Text;
            a.Testo = txt_testo.Text;
            a.Svolto = (bool)ckb_svolto.IsChecked;

            //salvataggio
            int res = FactoryAnnotazioni.InsertUpdate(a);
            lbl_modifiche_sospese.Content = "";
        }


        private void control_lost_focus(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (a.Titolo != txt_titolo.Text)
                count++;
            if (a.Testo != txt_testo.Text)
                count++;
            if (a.Svolto != ckb_svolto.IsChecked)
                count++;

            if (count > 0)
                lbl_modifiche_sospese.Content = "modifiche non salvate";
            else
                lbl_modifiche_sospese.Content = "";
        }
    }
}
