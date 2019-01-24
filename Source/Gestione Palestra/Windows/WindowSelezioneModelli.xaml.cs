using System; using GestionePalestra.MVC;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace GestionePalestra
{
    /// <summary>
    /// seleziona una scheda tra i modelli disponibili e la restitusice alla finestra padre
    /// </summary>
    public partial class WindowSelezioneModelli : Window
    {
        public Scheda SchedaSelezionata;


        private Scheda _tmp;

        //scheda selezionata dalla combobox
        private Scheda _sel;

        public WindowSelezioneModelli()
        {
            InitializeComponent();
            SchedaSelezionata = null;
            caricaModelli();
        }

        /// <summary>
        /// carica tutti i modelli nella combobox
        /// </summary>
        void caricaModelli()
        {
            cmb_modelli.ItemsSource = null;
            cmb_modelli.ItemsSource = SchedeController.GetTableSchedeModelli().Rows;
            cmb_modelli.DisplayMemberPath = ".[nome]";
        }

        /// <summary>
        /// seleziona l'id e chiude la finestra
        /// </summary>
        private void btn_seleziona_modello_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_modelli.SelectedIndex == -1 || _sel == null)
                return;

            //carica dati
            if(ckb_dati.IsChecked == true)
            {
                SchedaSelezionata = _sel;
            }

            //sedute
            SchedaSelezionata.Sedute.Clear();
            if(ckb_sedute.IsChecked == true)
            {
                for(int i = 0; i <= _sel.Sedute.Count -1; i++)
                {
                    if ((lbx_sedute.Items[i] as CheckBox).IsChecked == true)
                        SchedaSelezionata.Sedute.Add(_sel.Sedute[i]);
                }
            }
            
            
            this.Close();
        }

        private void cmb_modelli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = Convert.ToInt16(((DataRow)cmb_modelli.SelectedItem)[0]);
            _sel = SchedeController.SelezionaSchedaCompleta(id);

            lbx_sedute.Items.Clear();
            foreach(Seduta sed in _sel.Sedute)
            {
                lbx_sedute.Items.Add(
                    new CheckBox() {
                        Content = sed.Nome,
                        IsChecked = true
                    }
                );
            }
            
        }

        private void ckb_sedute_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded != true)
                return;

            lbx_sedute.IsEnabled = true;
            foreach (object c in lbx_sedute.Items)
                if (c is CheckBox)
                    (c as CheckBox).IsChecked = true;
        }

        private void ckb_sedute_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded != true)
                return;

            lbx_sedute.IsEnabled = false;
            foreach (object c in lbx_sedute.Items)
                if (c is CheckBox)
                    (c as CheckBox).IsChecked = false;
        }
    }
}


/*
tv_esercizi.Items.Clear();
int id = Convert.ToInt16(((DataRow)cmb_modelli.SelectedItem)[0]);
Scheda s = FactorySchede.SelezionaSchedaCompleta(id);

string dati = string.Format(
    "Nome:\t\t{0}\n" +
    "Difficoltà:\t{1}\n" +
    "N°sedute:\t{2}\n\n",
    s.Nome,
    (s.Difficolta.HasValue) ? String.Concat(Enumerable.Repeat("★", s.Difficolta.Value)) : "",
    s.Sedute.Count);
int c = 1;
foreach(Seduta sed in s.Sedute)
{
    TreeViewItem ti = new TreeViewItem();
    ti.Header = (sed.Nome != "")? sed.Nome: "Nessun nome";
    foreach(EsercizioSeduta es in sed.Esercizi)
    {
        ti.Items.Add(es.FKEsercizio);
    }
    tv_esercizi.Items.Add(ti);
    //dati += string.Format("{0}. {1} ({2} esercizi)\n", c, (sed.Nome != "")?sed.Nome: "nessun nome", sed.Esercizi.Count);
    //c++;
}

lbl_dati.Content = dati;
 */
