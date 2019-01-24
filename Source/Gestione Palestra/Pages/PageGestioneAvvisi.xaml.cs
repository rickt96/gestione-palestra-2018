using System;
using GestionePalestra.MVC;
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
using System.Data;

namespace GestionePalestra
{
    /// <summary>
    /// Interaction logic for PageGestioneAvvisi.xaml
    /// </summary>
    public partial class PageGestioneAvvisi : Page
    {
        /*
         * [note uso eventi controlli custom]
         * l'evento click sui bottoni dei controlli custom
         * viene ridefinito anche in questa finestra
         * viene eseguito prima il click definito nel controllo
         * poi quello impostato nella finestra
         * (vedi i dettagli nella region HEADER)
         */


        //0: agenda personale | 1: comunicazioni
        int _tipoVisualizzazione = 0;
        DataTable sourceDT;

        //stringa sql per il filtro delle date: viene accodata quando si leggono gli avvisi e le comunicazioni
        string where_clause = "";

        public PageGestioneAvvisi()
        {
            InitializeComponent();
        }

        /// <summary>
        /// aggiorna l'elenco degli avvisi in base alla tipologia
        /// </summary>
        void RefreshAgenda()
        {
            lbx_avvisi.Items.Clear();
            DataTable dt = new DataTable();
            switch (_tipoVisualizzazione)
            {
                case 0:
                    //grp_gruppo.Header = "Messaggi personali";
                    txb_descr.Text = "Elenco dei messaggi personali, visibili solo all'istruttore attivo";
                    dt = AvvisoController.GetAgenda(Session.User.PKIstruttore, where_clause);
                    break;

                case 1:
                    //grp_gruppo.Header = "Cominicazioni pubbliche";
                    txb_descr.Text = "Elenco degli avvisi pubblici, visibili a chi è destinatario";
                    dt = AvvisoController.GetComunicazioni(Session.User.PKIstruttore, where_clause);
                    break;
            }

            Avviso avv;
            foreach (DataRow dr in dt.Rows)
            {
                avv = AvvisoController.Select(Convert.ToInt16(dr[0]));
                ControlAvviso ca = new ControlAvviso(avv);
                ca.btn_modifica.Click += control_avviso_update_Click;
                ca.btn_elimina.Click += control_avviso_update_Click;
                lbx_avvisi.Items.Add(ca);
            }

            if (dt.Rows.Count == 0)
            {
                lbx_avvisi.Items.Add(new Label()
                {
                    Content = "Nessun avviso disponibile",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    FontSize = 14,
                    IsEnabled = false
                });
            }
        }




        /// <summary>
        /// caricamento finestra
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAgenda();
        }

        /// <summary>
        /// selezione del tipo di visualizzazione: agenda personale o comunicazioni
        /// </summary>
        private void rdb_visualizzazione_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded != true)
                return;

            if (rdb_agenda.IsChecked == true)
                _tipoVisualizzazione = 0;
            if (rdb_comunicazioni.IsChecked == true)
                _tipoVisualizzazione = 1;

            RefreshAgenda();
            where_clause = "";
            //rdb_data_0_tutti.IsChecked = true;
        }


        #region CRUD AVVISI

        /*
         * gli eventi modifica ed elimina definiti nel ControlAvviso vengono eseguiti prima di control_avviso_update_Click
         * quindi qui in questa window l'evento di click esegue solo il refresh dopo la modifica hostata nel controllo detto sopra:
         * praticamente il controllo contiene i bottoni che fanno modifica ed eliminazione
         * dopo il loro evento di click parte control_avviso_update_Click che fa solo il refresh
         */

        private void control_avviso_update_Click(object sender, RoutedEventArgs e)
        {
            RefreshAgenda();
        }



        #endregion


        private void btn_cerca_avviso_Click_old(object sender, RoutedEventArgs e)
        {

        }

        private void selezione_filtro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txt_cerca_avviso_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {

            }
        }



        private void cmd_periodo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




        private void btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            new WindowAvviso((bool)rdb_agenda.IsChecked).ShowDialog();
            RefreshAgenda();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAgenda();
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Paging.BackHome(this);
        }
    }
}
