using System; using GestionePalestra.MVC;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra di dialogo per la selezione più agile di un esercizio
    /// </summary>
    public partial class WindowSelezioneEsercizio : Window
    {
        public int IdEsercizioSelezionato { get; set; }

        DataTable _eserciziDT;

        /// <summary>
        /// costruttore di base
        /// </summary>
        public WindowSelezioneEsercizio()
        {
            InitializeComponent();
            IdEsercizioSelezionato = -1;
        }

        
        /// <summary>
        /// recupero id dall'elemento selezionato dalla lista
        /// </summary>
        private void btn_seleziona_Click(object sender, RoutedEventArgs e)
        {
            if (dg_esercizi.SelectedIndex == -1)
                return;

            IdEsercizioSelezionato = Convert.ToInt16(((DataRowView)dg_esercizi.SelectedItem)[0]);
            MessageBox.Show(IdEsercizioSelezionato.ToString());
            this.Close(); 
        }

        
        /// <summary>
        /// caricamento elementi table
        /// </summary>
        private void dg_esercizi_Loaded(object sender, RoutedEventArgs e)
        {
            _eserciziDT = EserciziController.GetTableEsercizi();
            dg_esercizi.ItemsSource = null;
            dg_esercizi.ItemsSource = _eserciziDT.DefaultView;
        }

        private void txt_ricerca_TextChanged(object sender, TextChangedEventArgs e)
        {
            _eserciziDT.DefaultView.RowFilter = string.Format("nome LIKE '%{0}%' OR categoria LIKE '%{0}%'", txt_ricerca.Text);
        }

        private void dg_esercizi_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btn_seleziona_Click(sender, e);
        }
    }
}