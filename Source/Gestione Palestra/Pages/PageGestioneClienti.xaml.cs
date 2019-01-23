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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;
using System.Data;

namespace GestionePalestra.Pages
{
    /// <summary>
    /// Interaction logic for PageGestionePalestra.xaml
    /// </summary>
    public partial class PageGestionePalestra : Page
    {
        /*
         * [note funzionamento update]
         * ListnerUpdate viene avviato col metodo update()
         * ogni X secondi lui controlla su common l'ultima data di modifica
         * e la confronta con la sua locale
         * se risulta che la data locale è minore aggiorna la sorgente
         * e marca la data locale al timestamp
         * cosi nel controllo successivo non viene ricaricato nulla
         * 
         * [note datagrid]
         * il primo caricamento è nell'evento Loaded del DataGrid
         */

        DateTime LastUpdateLocal;
        string caption = "cliente";
        Thread ListnerUpdate;
        DataTable _clientiDT;


        public PageGestionePalestra()
        {
            InitializeComponent();

            //listner per aggiornamento tabella
            ListnerUpdate = new Thread(update);
            ListnerUpdate.IsBackground = true;
            ListnerUpdate.Start();
        }


        /// <summary>
        /// ricarica dal database la lista dei clienti
        /// </summary>
        void RefreshClienti()
        {
            _clientiDT = FactoryCliente.GetTableClienti("");
            _clientiDT.CaseSensitive = false;

            dg_clienti.ItemsSource = null;
            dg_clienti.ItemsSource = _clientiDT.DefaultView;

            LastUpdateLocal = DateTime.Now;

            SetStatusBarInfo();
            txt_cerca.Text = "";
        }



        /// <summary>
        /// imposta i valori nella statusbar
        /// </summary>
        void SetStatusBarInfo()
        {
            lbl_tot_record.Content = _clientiDT.Rows.Count + " record trovati";
            lbl_display_record.Content = dg_clienti.Items.Count + " record mostrati";
            lbl_tot_sel.Content = dg_clienti.SelectedItems.Count + " record selezionati";
            lbl_last_update.Content = "ultimo aggiornamento alle " + LastUpdateLocal.ToShortTimeString();
        }


        /// <summary>
        /// mostra la scheda utente
        /// </summary>
        void ApriSchedaUtente()
        {
            if (dg_clienti.SelectedIndex == -1)
                return;

            DataRowView DataRowView = (DataRowView)dg_clienti.SelectedItem;
            int id = Convert.ToInt16(DataRowView[0]);
            new WindowSchedaUtente(id).Show();
        }


        /// <summary>
        /// elimina l'utente selezionato
        /// </summary>
        void EliminaUtente()
        {
            if (dg_clienti.SelectedIndex == -1)
                return;

            DataRowView DataRowView = (DataRowView)dg_clienti.SelectedItem;
            int id = Convert.ToInt16(DataRowView["#"]);

            if (Message.Confirm(DialogType.delete, caption) == false)
                return;

            if (FactoryCliente.Elimina(id) > 0)
            {
                //FactoryStoricoEventi.Inserisci((int?)null, "Rimosso profilo cliente");
            }

            RefreshClienti();
        }

        void update()
        {
            while (true)
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    if (Common.LastUpdateClienti > LastUpdateLocal)
                    {
                        RefreshClienti();
                    }
                });
                Thread.Sleep(2000);
            }
        }


        #region RICERCA

        private void txt_clienti_cerca_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //cerca
            if (e.Key == Key.Enter)
            {
                btn_cerca_Click(sender, e);
                txt_cerca.SelectAll();
            }

            //pulisci
            if (e.Key == Key.Delete)
            {
                txt_cerca.Text = "";
                btn_cerca_Click(sender, e);
            }
        }

        private void btn_ricerca_avanzata_Click(object sender, RoutedEventArgs e)
        {
            (new WindowRicercaAvanzata(ref dg_clienti)).Show();
        }

        private void btn_cerca_Click(object sender, RoutedEventArgs e)
        {
            _clientiDT.DefaultView.RowFilter = string.Format("nome LIKE '%{0}%' OR cognome LIKE '%{0}%'", txt_cerca.Text);
        }

        #endregion


        #region EVENTI DATAGRID

        private void dg_clienti_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshClienti();
        }

        private void dg_clienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetStatusBarInfo();
        }

        private void mouse_doubleclick_datagrid(object sender, MouseButtonEventArgs e)
        {
            ApriSchedaUtente();
        }

        private void controlli_tastiera_datagrid(object sender, KeyEventArgs e)
        {
            //blocca cancellazione con delete
            if (e.Key == Key.Delete)
                EliminaUtente();

            //funzione al tasto enter
            if (e.Key == Key.Enter)
                ApriSchedaUtente();
        }

        #endregion


        #region EVENTI FINESTRA

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ListnerUpdate.Abort();
        }

        #endregion


        #region BARRA FUNZIONI

        private void btn_apri_scheda_Click(object sender, RoutedEventArgs e)
        {
            ApriSchedaUtente();
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            EliminaUtente();
        }

        private void btn_nuovo_cliente_Click(object sender, RoutedEventArgs e)
        {
            new WindowInserisciUtente().Show();
        }

        private void btn_aggiorna_Click(object sender, RoutedEventArgs e)
        {
            RefreshClienti();
            MessageBox.Show("Lista aggiornata", caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("PageHome.xaml", UriKind.Relative));
            Paging.BackHome(this);
        }
    }
}
