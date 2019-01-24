using System; using GestionePalestra.MVC;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra che mostra l'avviso, senza possibilita di modifica dei dati, se non quella di marcare la lettura
    /// </summary>
    public partial class WindowMostraAvviso : Window
    {
        DateTime? data_lettura = null;
        Avviso a;

        public WindowMostraAvviso(int PKAvviso)
        {
            InitializeComponent();
            a = new Avviso();
            a.PKAvviso = PKAvviso;
        }

        string NomeIstruttore()
        {
            Istruttore i = IstruttoriController.Seleziona(a.FKIstruttore);
            return i.NomeCompleto;
        }

        void SetCliente()
        {
            if (a.FKCliente > 0)
            {
                DataRow c = ClienteController.GetCliente(a.FKCliente.Value);
                if (c != null)
                    txtb_cliente_dest.Text = c[1] + " " + c[2];
                else
                    txtb_cliente_dest.Text = "Nessun cliente selezionato";
            }
        }

        /// <summary>
        /// esci
        /// </summary>
        private void img_indietro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// marca la lettura del messaggio
        /// </summary>

        /// <summary>
        /// caricamento informazioni avviso
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //*********************************
            // CARICAMENTO OGGETTO
            //*********************************
            a = AvvisoController.Select(a.PKAvviso);


            //*********************************
            // 1.HEADER
            //*********************************

            //caricamento immagine profilo istruttore
            Istruttore i = IstruttoriController.Seleziona(a.FKIstruttore); //[SOSTITUIRE GETIMAGE]
            img_profilo.ImageSource = Common.BitmapFromByte(i.Immagine, "BlueLogin.png");

            //nome istruttore
            lbl_istruttore.Content = (a.FKIstruttore == Session.User.PKIstruttore)
                ? NomeIstruttore() + " (istruttore attivo)"
                : NomeIstruttore();

            //data
            txtb_tipologia_data.Text = string.Format("Aggiunto il {0} alle {1}",
                a.DataInserimento.Value.ToString("yyyy/MM/dd"),
                a.DataInserimento.Value.ToString("hh:mm"));

            //icona visibilita
            img_visibilita.Source = (a.isPersonal == true) ?
             new BitmapImage(new Uri(@"/Gestione Palestra;component//Gestione Palestra;component/Resources/Icons/WhiteSmallUser.png", UriKind.RelativeOrAbsolute)) :
             new BitmapImage(new Uri(@"/Gestione Palestra;component//Gestione Palestra;component/Resources/Icons/WhiteSmallGroup.png", UriKind.RelativeOrAbsolute));
            lbl_count_gruppo.Content = (a.Destinatari.Count > 0) ? a.Destinatari.Count.ToString() : "";



            //*********************************
            // 2. INFORMAZIONI
            //*********************************

            //titolo descrizione
            txtb_titolo.Text = (a.Titolo != "") ? a.Titolo : "Nessun titolo";
            //descrizione
            txtb_descr.Text = (a.Descrizione != "") ? a.Descrizione : "Nessuna descrizione";
            //data
            txtb_data.Text = a.Data.Value.ToString("yyyy/MM/dd hh:mm");
            //priorita
            switch (a.Priorita)
            {
                case 0:
                    txtb_priorita.Foreground = Brushes.Green;
                    txtb_priorita.Text = "normale";
                    break;
                case 1:
                    txtb_priorita.Foreground = Brushes.Red;
                    txtb_priorita.Text = "urgente";
                    break;
            }
            //cliente selezionato
            SetCliente();


            //*********************************
            // 3. DESTINATARI
            //*********************************
            dg_stato_letture.ItemsSource = null;
            dg_stato_letture.ItemsSource = AvvisoController.GetStatiLetture(a.PKAvviso).DefaultView;


            //*********************************
            // 3. VERIFICA LETTURA
            //*********************************

            if(a.FKIstruttore != Session.User.PKIstruttore)//se l'avviso non di chi sta guardando
            {
                data_lettura = AvvisoController.GetStatoLettura(a.PKAvviso, Session.User.PKIstruttore);
                if (data_lettura.HasValue)//se è gia stato letto non serve riconfermare
                    btn_conferma_lettura.IsEnabled = false;
                else
                    btn_conferma_lettura.IsEnabled = true;
            }
            else
            {
                btn_conferma_lettura.Visibility = Visibility.Collapsed;
            }
            //if (a.FKIstruttore != Session.IstruttoreAttivo.PKIstruttore)
            //{
                
            //}
            //else
            //{
                
            //}  
            
        }

        /// <summary>
        /// in chiusura verifica lo stato della lettura
        /// e obbliga a confermare la lettura se il messaggio è urgente
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(a.Priorita == 1 && a.isPersonal == false && data_lettura != null) //priorita urgente
            {
                MessageBox.Show("E' necessario confermare la lettura per poter chiudere","Avviso",MessageBoxButton.OK,MessageBoxImage.Warning);
                e.Cancel = true;
            }
        }

        private void btn_conferma_lettura_Click(object sender, RoutedEventArgs e)
        {
            if(AvvisoController.ConfermaLetturaAvviso(a.PKAvviso, Session.User.PKIstruttore) == true)
            {
                data_lettura = DateTime.Now;
            }

            if (data_lettura != null)
                this.Close();
        }
    }
}
