using System; using GestionePalestra.MVC;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra di dialogo per la selezione di un cliente
    /// </summary>
    public partial class WindowSelezioneCliente : Window
    {
        public int id_cliente_selezionato { get; set; }
        DataTable _clientiDT;

        public WindowSelezioneCliente()
        {
            InitializeComponent();
            id_cliente_selezionato = -1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //caricamento dataset clienti
            _clientiDT = ClienteController.GetTableClienti("");

            //assegnazione al datagrid
            dg_clienti.ItemsSource = null;
            dg_clienti.ItemsSource = _clientiDT.DefaultView;
        }

        private void dg_clienti_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btn_seleziona_Click(sender, e);
        }

        private void btn_cerca_annulla_Click(object sender, RoutedEventArgs e)
        {
            //if (txt_cerca.Text == string.Empty) //annulla la ricerca
            //{
            //    dg_clienti.ItemsSource = null;
            //    dg_clienti.ItemsSource = FactoryCliente.GetListClientiBase("");
            //}
            //else //la esegue
            //{
            //    string wc = string.Format(@"WHERE (
            //    id_cliente = '{0}' OR 
            //    UPPER(nome) LIKE '%{0}%' OR 
            //    UPPER(cognome) LIKE '%{0}%'
            //    )", txt_cerca.Text.ToUpper());
            //    dg_clienti.ItemsSource = null;
            //    dg_clienti.ItemsSource = FactoryCliente.GetListClientiBase(wc);
            //    txt_cerca.SelectAll();
            //}
        }

        /// <summary>
        /// all'evento textchange cerca nel dataset
        /// </summary>
        private void txt_cerca_TextChanged(object sender, TextChangedEventArgs e)
        {
            _clientiDT.DefaultView.RowFilter = string.Format("nome LIKE UPPER('%{0}%') OR cognome LIKE UPPER('%{0}%')", txt_cerca.Text.ToUpper());
        }


        /// <summary>
        /// seleziona e restituisce l'id dell'elemento selezionato
        /// </summary>
        private void btn_seleziona_Click(object sender, RoutedEventArgs e)
        {
            if (dg_clienti.SelectedIndex == -1)
                return;

            int id_c = Convert.ToInt16((dg_clienti.SelectedItem as DataRowView).Row[0]);
            id_cliente_selezionato = id_c;
            this.Close(); 
        }
    }
}
