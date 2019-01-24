using System; using GestionePalestra.MVC;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GestionePalestra.MVC;

namespace GestionePalestra
{
    /// <summary>
    /// Controllo custom per la visualizzazione sotto forma di card i dati di un avviso
    /// </summary>
    public partial class ControlAvviso : UserControl
    {
        string caption = "avviso";
        /// <summary>
        /// avviso di riferimento
        /// </summary>
        public Avviso a { get; set; }


        public ControlAvviso(Avviso a)
        {
            InitializeComponent();
            this.a = a;
            ShowData();
        }


        /// <summary>
        /// visualizza sul controllo i dati dell'avviso
        /// </summary>
        void ShowData()
        {
            //*********************************
            // 1.HEADER
            //*********************************

            //caricamento immagine profilo istruttore
            img_profilo.ImageSource = Common.BitmapFromByte(
                        IstruttoriController.GetByteImage(a.FKIstruttore),
                        "BlueLogin.png");

            //nome istruttore
            lbl_istruttore.Content = (a.FKIstruttore == Session.User.PKIstruttore)
                ? NomeIstruttore() + " (attivo)"
                : NomeIstruttore();
            //data
            txtb_tipologia_data.Text = string.Format("Aggiunto il {0} alle {1}",        //{0} •
                (a.DataInserimento.HasValue) ? a.DataInserimento.Value.ToString("yyyy/MM/dd") : "",
                (a.DataInserimento.HasValue) ? a.DataInserimento.Value.ToString("hh:mm") : "");

            //icona visibilita
            img_visibilita.Source = (a.isPersonal == true) ?
             new BitmapImage(new Uri(@"/Gestione Palestra;component//Gestione Palestra;component/Resources/Icons/WhiteSmallUser.png", UriKind.RelativeOrAbsolute)) :
             new BitmapImage(new Uri(@"/Gestione Palestra;component//Gestione Palestra;component/Resources/Icons/WhiteSmallGroup.png", UriKind.RelativeOrAbsolute));
            lbl_count_gruppo.Content = (a.Destinatari.Count > 0)? a.Destinatari.Count.ToString() : "";



            //*********************************
            // 2. INFORMAZIONI
            //*********************************

            //titolo descrizione
            txtb_titolo.Text = (a.Titolo != null) ? a.Titolo : "Nessun titolo";
            //descrizione
            txtb_descr.Text = (a.Descrizione != "") ? a.Descrizione : "Nessuna descrizione";
            //data
            if (a.Data.HasValue) txtb_data.Text = a.Data.Value.ToString("yyyy/MM/dd hh:mm");
            //priorita
            switch(a.Priorita) {
                case 0:
                    txtb_priorita.Foreground = Brushes.Green;
                    txtb_priorita.Text = "normale";
                    break;
                case 1:
                    txtb_priorita.Foreground = Brushes.Red;
                    txtb_priorita.Text = "urgente";
                    break;
                default:
                    txtb_priorita.Text = "----";
                    break;
            }
            //cliente selezionato
            SetCliente();
            //bottoni
            btn_modifica.Visibility = (a.FKIstruttore == Session.User.PKIstruttore)
                ? Visibility.Visible
                : Visibility.Collapsed;
            btn_elimina.Visibility = (a.FKIstruttore == Session.User.PKIstruttore)
                ? Visibility.Visible
                : Visibility.Collapsed;

        }


        string NomeIstruttore()
        {
            DataTable dt = IstruttoriController.SelezionaDT_old(a.FKIstruttore);
            string nome = dt.Rows[0][1] + " " + dt.Rows[0][2];
            return nome;
        }

        void SetCliente()
        {
            if (a.FKCliente.HasValue)
            {
                btn_apri_cliente.Visibility = Visibility.Visible;
                DataRow c = ClienteController.GetCliente(a.FKCliente.Value);
                if(c != null)
                    txtb_cliente_dest.Text = c[1]+" "+c[2];
                else
                    txtb_cliente_dest.Text = "Nessun cliente selezionato";
            }
            else
            {
                btn_apri_cliente.Visibility = Visibility.Collapsed;
            }
        }
        

        private void btn_apri_cliente_Click(object sender, RoutedEventArgs e)
        {
            if(a.FKCliente.HasValue)
                new WindowSchedaUtente(a.FKCliente.Value).Show();
        }   

        private void btn_modifica_Click(object sender, RoutedEventArgs e)
        {
            new WindowAvviso(a.PKAvviso).ShowDialog();
        }

        private void btn_visualizza_Click(object sender, RoutedEventArgs e)
        {
            new WindowMostraAvviso(a.PKAvviso).Show();
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if(Message.Confirm(DialogType.delete, caption))
                AvvisoController.Delete(a.PKAvviso);
        }
    }
            
}
