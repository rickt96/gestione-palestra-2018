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
using System.Data;

namespace GestionePalestra.Pages
{
    /// <summary>
    /// Interaction logic for PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {
        public PageHome()
        {
            InitializeComponent();

            if (Session.User != null)
                grid_main.IsEnabled = true;
            else
                grid_main.IsEnabled = false;
        }


        //METODI
        void GetRiepiloghi()
        {
            //da fare
        }


        #region CLIENTI

        private void hl_inserisci_cliente_Click(object sender, RoutedEventArgs e)
        {
            new WindowInserisciUtente().Show();
        }
        private void btn_clienti_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestionePalestra());
        }

        #endregion


        #region ALLENAMENTI

        private void hl_inserisci_scheda_Click(object sender, RoutedEventArgs e)
        {
            new WindowSchedaAllenamento().Show();
        }
        private void btn_schede_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestioneSchede());
        }

        #endregion


        #region ESERCIZI

        private void hl_aggiungi_esercizio_Click(object sender, RoutedEventArgs e)
        {
            new WindowEsercizio(-1, FormAction.insert).Show();
        }
        private void btn_esercizi_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestioneEsercizi());
        }

        #endregion


        #region ISTRUTTORI
        
        private void btn_profilo_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestioneIstruttori());
        }
        private void hl_pannello_istruttore_Click(object sender, RoutedEventArgs e)
        {
            new WindowPannelloIstruttore().Show();
        }

        #endregion


        #region AVVISI

        private void btn8_annotazioni_Click(object sender, RoutedEventArgs e)
        {
            new WindowDiarioAnnotazioni().Show();
        }
        private void btn_agenda_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestioneAvvisi());
        }

        private void hl_inserisci_avviso_Click(object sender, RoutedEventArgs e)
        {
            new WindowAvviso(false).Show();
        }

        private void hl_inserisci_annotazione_Click(object sender, RoutedEventArgs e)
        {
            new WindowDiarioAnnotazioni().Show();
        }

        #endregion


        #region ANAMNESI

        private void btn_anamnesi_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageGestioneAnamnesi());
        }
        private void hl_inserisci_anamnesi_Click(object sender, RoutedEventArgs e)
        {
            WindowSelezioneCliente sc = new WindowSelezioneCliente();
            sc.ShowDialog();
            if (sc.id_cliente_selezionato != -1)
            {
                WindowAnamnesi wa = new WindowAnamnesi(FormAction.insert, sc.id_cliente_selezionato);
                wa.Show();
            }
        }


        #endregion

        
    }
}
