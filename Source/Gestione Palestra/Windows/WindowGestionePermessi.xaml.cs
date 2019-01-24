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
    /// Logica di interazione per WindowGestionePermessi.xaml
    /// </summary>
    public partial class WindowGestionePermessi : Window
    {
        /*
         * per marcare la differenza tra modifica ed inserimento
         * per la window è indifferente,
         * quando premo su aggiungi (+) vienei istanziato _livello vuoto
         * e quando il metodo passa al factory capisce in base all'id se aggiornare o inserire
         */

        string caption = "livelli permessi";

        /// <summary>
        /// lista dei permessi
        /// </summary>
        List<LivelloPermesso> livelli = new List<LivelloPermesso>();

        /// <summary>
        /// oggetto temp per la modifica locale
        /// </summary>
        LivelloPermesso _livello;

        /// <summary>
        /// lista delle checkbox dei permessi
        /// questa è il set di elementi da assegnare alla listbox
        /// in caso di aggiunta di nuove voci accodarle qui
        /// </summary>
        List<CheckBox> checks = new List<CheckBox>()
        {
            new CheckBox(){ Content = "Crea, modifica ed elimina anamnesi", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Modifica ed elimina anamnesi generate da altri istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza gli elenchi delle anamnesi", HorizontalContentAlignment = HorizontalAlignment.Center},

            new CheckBox(){ Content = "Crea, modifica ed elimina avvisi", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Modifica ed elimina avvisi generati da altri istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza gli elenchi degli avvisi", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Imposta avvisi con priorità urgente", HorizontalContentAlignment = HorizontalAlignment.Center},

            new CheckBox(){ Content = "Crea, modifica ed elimina clienti", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Modifica ed elimina clienti inseriti da altri istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza gli elenchi degli clienti", HorizontalContentAlignment = HorizontalAlignment.Center},

            new CheckBox(){ Content = "Modifica ed elimina le credenziali di accesso", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Crea, Modifica ed elimina i profili di altri istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza gli elenchi degli istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza la finestra delle opzioni", HorizontalContentAlignment = HorizontalAlignment.Center},

            new CheckBox(){ Content = "Crea, modifica ed elimina schede di allenamento", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Modifica ed elimina schede create da altri istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza gli elenchi delle schede", HorizontalContentAlignment = HorizontalAlignment.Center},

            new CheckBox(){ Content = "Crea, modifica ed elimina test", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Modifica ed elimina test inseriti da altri istruttori", HorizontalContentAlignment = HorizontalAlignment.Center},
            new CheckBox(){ Content = "Visualizza gli elenchi dei test", HorizontalContentAlignment = HorizontalAlignment.Center}
        };

        

        public WindowGestionePermessi()
        {
            InitializeComponent();
        }


        /// <summary>
        /// carica le informazioni del livello selezionato nei controlli
        /// </summary>
        void ShowLivello()
        {
            txt_nome.Text = _livello.Nome;
            txt_descrizione.Text = _livello.Descrizione;

            checks[0].IsChecked = _livello.ANAMNESI_CUD_SELF;
            checks[1].IsChecked = _livello.ANAMNESI_UD_OTHER;
            checks[2].IsChecked = _livello.ANAMNESI_R;
            checks[3].IsChecked = _livello.AVVISI_CUD_SELF;
            checks[4].IsChecked = _livello.AVVISI_UD_OTHER;
            checks[5].IsChecked = _livello.AVVISI_R;
            checks[6].IsChecked = _livello.AVVISI_SET_URGENT;
            checks[7].IsChecked = _livello.CLIENTI_CUD_SELF;
            checks[8].IsChecked = _livello.CLIENTI_UD_OTHER;
            checks[9].IsChecked = _livello.CLIENTI_R;
            checks[10].IsChecked = _livello.ISTRUTTORI_UD_SELF;
            checks[11].IsChecked = _livello.ISTRUTTORI_CUD_OTHER;
            checks[12].IsChecked = _livello.ISTRUTTORI_R;
            checks[13].IsChecked = _livello.ISTRUTTORI_SHOW_SETTINGS;
            checks[14].IsChecked = _livello.SCHEDE_CUD_SELF;
            checks[15].IsChecked = _livello.SCHEDE_UD_OTHER;
            checks[16].IsChecked = _livello.SCHEDE_R;
            checks[17].IsChecked = _livello.TEST_CUD_SELF;
            checks[18].IsChecked = _livello.TEST_UD_OTHER;
            checks[19].IsChecked = _livello.TEST_R;
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //caricamento livelli permessi
            livelli = LivelliPermessiController.GetListPermessi();

            //listbox
            lbx_permessi.ItemsSource = checks;

            //popolamento combobox
            Common.PopulateComboBox(ref cmb_livelli, livelli, "Nome");
        }



        private void cmb_livelli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_livelli.SelectedIndex == -1)
                return;

            _livello = livelli[cmb_livelli.SelectedIndex];
            btn_applica.Content = "Modifica";
            ShowLivello();
        }

        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            cmb_livelli.SelectedIndex = -1;
            _livello = new LivelloPermesso() {
                PKLivelloPermesso = 0,
                Nome = "nome permesso",
                Descrizione = "descrizione permesso"
            };
            btn_applica.Content = "Inserisci";
            ShowLivello();
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_livelli.SelectedIndex == -1)
                return;

            int id = livelli[cmb_livelli.SelectedIndex].PKLivelloPermesso;
            if(LivelliPermessiController.Delete(id) > 0)
            {
                Message.Alert(DialogType.delete, caption);
                livelli = LivelliPermessiController.GetListPermessi();
                Common.PopulateComboBox(ref cmb_livelli, livelli, "Nome");
                cmb_livelli.SelectedIndex = -1;
            }
        }



        private void btn_applica_Click(object sender, RoutedEventArgs e)
        {
            if (_livello == null)
                return;


            if (Message.Question("Applicare le modifiche a " + txt_nome.Text + "?", caption) == false)
                return;

            //assegnazione campi oggetti
            _livello.Nome = txt_nome.Text;
            _livello.Descrizione = txt_descrizione.Text;

            _livello.ANAMNESI_CUD_SELF = (bool)checks[0].IsChecked;
            _livello.ANAMNESI_UD_OTHER = (bool)checks[1].IsChecked;
            _livello.ANAMNESI_R = (bool)checks[2].IsChecked;
            _livello.AVVISI_CUD_SELF = (bool)checks[3].IsChecked;
            _livello.AVVISI_UD_OTHER= (bool)checks[4].IsChecked;
            _livello.AVVISI_R= (bool)checks[5].IsChecked;
            _livello.AVVISI_SET_URGENT = (bool)checks[6].IsChecked;
            _livello.CLIENTI_CUD_SELF = (bool)checks[7].IsChecked;
            _livello.CLIENTI_UD_OTHER = (bool)checks[8].IsChecked;
            _livello.CLIENTI_R = (bool)checks[9].IsChecked;
            _livello.ISTRUTTORI_UD_SELF = (bool)checks[10].IsChecked;
            _livello.ISTRUTTORI_CUD_OTHER = (bool)checks[11].IsChecked;
            _livello.ISTRUTTORI_R = (bool)checks[12].IsChecked;
            _livello.ISTRUTTORI_SHOW_SETTINGS = (bool)checks[13].IsChecked;
            _livello.SCHEDE_CUD_SELF = (bool)checks[14].IsChecked;
            _livello.SCHEDE_UD_OTHER = (bool)checks[15].IsChecked;
            _livello.SCHEDE_R = (bool)checks[16].IsChecked;
            _livello.TEST_CUD_SELF = (bool)checks[17].IsChecked;
            _livello.TEST_UD_OTHER = (bool)checks[18].IsChecked;
            _livello.TEST_R = (bool)checks[19].IsChecked;

            //salvataggio db
            if(LivelliPermessiController.InsertUpdate(_livello) > 0)
            {
                Message.Alert(DialogType.update, caption);
                livelli = LivelliPermessiController.GetListPermessi();
                Common.PopulateComboBox(ref cmb_livelli, livelli, "Nome");
            }
                
        }
        private void btn_annulla_Click(object sender, RoutedEventArgs e)
        {
            ShowLivello();
        }
    }
}
