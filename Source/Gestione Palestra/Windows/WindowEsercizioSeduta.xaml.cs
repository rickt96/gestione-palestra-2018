using System; using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace GestionePalestra
{
    /// <summary>
    /// Logica di interazione per WindowEsercizioSeduta.xaml
    /// </summary>
    public partial class WindowEsercizioSeduta : Window
    {
        /*
         * [Note funzionamento]
         * qui viene definito solo l'esercizio singolo
         * ignorando l'unione con gli altri esercizi
         * quindi qui non importa il fatto che sia una super serie, circuito o simili
         * quello verrà definto in un'altra finestra
         */
        FormAction tipoFunzione;
        public EsercizioSeduta Esercizio { get; set; }
        public WindowEsercizioSeduta(EsercizioSeduta es)
        {
            InitializeComponent();
            tipoFunzione = FormAction.update;
            Esercizio = es;
            //lbl_tipo.Content = "Inserisci esercizio";
        }
        public WindowEsercizioSeduta()
        {
            InitializeComponent();
            tipoFunzione = FormAction.insert;
            Esercizio = new EsercizioSeduta();
            //lbl_tipo.Content = "Modifica esercizio";
        }


        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            Esercizio.Esercizio = (cmb_esercizi.SelectedIndex > -1) ? (cmb_esercizi.SelectedItem as Esercizio) : new Esercizio();
            Esercizio.Serie = nud_serie.Value;
            Esercizio.Ripetizioni = cmb_ripetizioni.Text;
            if(rdb_carico.IsChecked == true)
            {
                Esercizio.Carico = nud_carico.Value;
                Esercizio.Durata = null;
                Esercizio.ATempo = false;
            }
            else
            {
                Esercizio.Durata = tp_durata.Value;
                Esercizio.Carico = null;
                Esercizio.ATempo = true;
            }
            Esercizio.Recupero = tp_recupero.Value;
            Esercizio.Note = txt_note.Text;

            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //form filling
            Common.PopulateComboBox(ref cmb_esercizi, FactoryEsercizi.GetListEsercizi(), "Nome");
            cmb_ripetizioni.ItemsSource = FactoryEserciziSedute.GetListRipetizioniDinstict();

            //caricamento oggetto
            if(tipoFunzione == FormAction.update)
            {
                foreach (Esercizio es in cmb_esercizi.Items)
                    if (es.PKEsercizio == Esercizio.Esercizio.PKEsercizio)
                        cmb_esercizi.SelectedItem = es;

                nud_serie.Value = Esercizio.Serie;
                cmb_ripetizioni.Text = Esercizio.Ripetizioni;

                if (Esercizio.ATempo == false)
                {
                    rdb_carico.IsChecked = true;
                    nud_carico.Value = Esercizio.Carico;
                }
                else
                {
                    rdb_durata.IsChecked = true;
                    tp_durata.Value = Esercizio.Durata;
                }

                txt_note.Text = Esercizio.Note;
            }
        }

        private void cmb_esercizi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_esercizi.SelectedItem == null)
                return;

            Esercizio tmp = (Esercizio)cmb_esercizi.SelectedItem;
            Common.SetGridImage(ref grid_img, tmp.Immagine);
        }

        private void btn_seleziona_esercizio_Click(object sender, RoutedEventArgs e)
        {
            WindowSelezioneEsercizio ses = new WindowSelezioneEsercizio();
            ses.ShowDialog();
            if (ses.IdEsercizioSelezionato > -1)
            {
                foreach (Esercizio es in cmb_esercizi.Items)
                    if (es.PKEsercizio == ses.IdEsercizioSelezionato) {
                        cmb_esercizi.SelectedItem = es;
                        break;
                    }
                        
            }
        }
    }
}
