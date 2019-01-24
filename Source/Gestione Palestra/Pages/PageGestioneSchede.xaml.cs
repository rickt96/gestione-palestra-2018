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

using Microsoft.VisualBasic;

namespace GestionePalestra.Pages
{
    /// <summary>
    /// Interaction logic for PageGestioneSchede.xaml
    /// </summary>
    public partial class PageGestioneSchede : Page
    {
        /*
         * [Note funzionamento generale]
         * LoadCategorie carica le categorie nell'array categorie
         * e genera partendo da quello gli elementi per la listbox
         * quando seleziono un'elemento della listbox viene recuparata la categoria corrispondente
         * tramite il selectedIndex (categorie[lbx.selectedIndex])
         * 
         * per le schede invece la estione è piu merdosa
         * con ShowSchedeCategoria vengono creati gli oggetti ControlScheda
         * e dentro di loro viene messo l'id della scheda
         * quando clicco su un controllo di questi
         * l'evento di click che definisco in creazione recupera l'id
         * lo assegna a id_scheda_sel che quindi verra utilzzato per le varie oparazioni
         * 
         * [Note gestione wrappanel e scroll view]
         * il wrappanel è contenuto nella scrollview
         * in costruzione puo essere impostato di qualsiasi dimensione
         * durante il refresh degli elementi viene impostato Height Auto
         * in modo che si modelli al contenuto
         * la larghezza invece viene modificata in base alla larghezza dello scrollview
         * ogni volta che cè il WindowResize
         * in questo modo la width è fissata allo scrollview e la height è dinamica in base al contenuto
         * 
         * [Note interazione controlScheda]
         * durante la creazione in ShowSchedeCategoria
         * viene creato l'evento mouseleftbuttonup
         * il click prende l'id del controllo sender
         * e modifica il colore
         */


        /// <summary>
        /// indice dell'elemento selezionato dal wrap panel
        /// </summary>
        int id_scheda_sel;

        List<CategoriaScheda> categorie;

        /// <summary>
        /// costruttore base
        /// </summary>
        public PageGestioneSchede()
        {
            InitializeComponent();
        }


        #region EVENTI PAGE

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategorie();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            wrp_schede.Width = sv_schede.Width;
        }

        #endregion

        /// <summary>
        /// carica le schede apparteneti alla categoria scelta dalla combobox
        /// </summary>
        void ShowSchedeCategoria()
        {
            if (lbx_categorie.SelectedIndex == -1)
                return;

            id_scheda_sel = -1;
            wrp_schede.Children.Clear();

            int id_cat = categorie[lbx_categorie.SelectedIndex].PKCategoria;
            ControlScheda cs;
            foreach (Scheda sc in SchedeController.GetListSchedeCategoria(id_cat))
            {
                cs = new ControlScheda(sc);
                cs.Margin = new Thickness(10, 10, 10, 10);
                cs.uc.MouseLeftButtonUp += new MouseButtonEventHandler(click_control_scheda);
                wrp_schede.Children.Add(cs);
            }

            //creazione label avviso nessun elemento
            if (wrp_schede.Children.Count == 0)
            {
                wrp_schede.Children.Add(new Label() { Content = "Nessuna scheda disponibile", Margin = new Thickness(10, 10, 10, 10), FontSize = 16 });
            }

            //imposta altezza auto per estendersi per adattare al controllo
            wrp_schede.Height = double.NaN;
        }


        /// <summary>
        /// aggiorna la listbox della categorie
        /// </summary>
        void LoadCategorie()
        {
            //pulizia vecchi oggetti
            lbx_categorie.Items.Clear();

            //popolamento nuovi controlli
            categorie = CategorieSchedeController.GetCategorieSchede();
            foreach (CategoriaScheda cs in categorie)
            {
                lbx_categorie.Items.Add(new ControlCategoria(
                    cs.PKCategoria,
                    cs.Nome,
                    (cs.CountSchede.HasValue) ? cs.CountSchede + " schede" : cs.Descrizione)
                );
            }
        }


        /// <summary>
        /// aggiorna il contatore
        /// </summary>
        /// <param name="list_index">indice nella lista</param>
        /// <param name="val">valore da aggiornare</param>
        void UpdateCatCounter(int list_index, int val)
        {
            ////aggiornamento dato categoria
            //categorie[list_index].NumeroSchede += val;

            ////aggiornamento controllo listbox
            //((ControlCategoria)lbx_categorie.SelectedItem).Header2 = categorie[list_index].NumeroSchede + " schede";
        }

        /// <summary>
        /// evento click di selezione sul controllo scheda
        /// </summary>
        private void click_control_scheda(object sender, RoutedEventArgs e)
        {
            //assegnazione id
            int wrp_child_index = wrp_schede.Children.IndexOf(sender as ControlScheda);
            id_scheda_sel = ((ControlScheda)wrp_schede.Children[wrp_child_index]).Id;

            //impostazioni colore
            foreach (object o in wrp_schede.Children)
                (o as ControlScheda).rect.Stroke = Brushes.LightGray;
            (sender as ControlScheda).rect.Stroke = Brushes.LightSkyBlue;
        }


        #region EVENTI FINESTRA

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           
        }

        #endregion


        #region HEADER

       
        private void btn_cerca_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion


        #region CATEGORIE

        private void lbx_categorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSchedeCategoria();
        }

        private void btn_aggiungi_categoria_Click(object sender, RoutedEventArgs e)
        {
            string input = Interaction.InputBox("Inserisci la nuova categoria", "Inserimento", "");
            if (input != "")
            {
                CategoriaScheda c = new CategoriaScheda() { Nome = input, Descrizione = "nessuna" };
                if (CategorieSchedeController.Inserisci(c) >= 1)
                    LoadCategorie();
            }
        }

        private void btn_modifica_categoria_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_categorie.SelectedIndex == -1)
                return;

            CategoriaScheda c = categorie[lbx_categorie.SelectedIndex];

            //richiamo l'input box per modificare il nome
            string newname = Interaction.InputBox("Modifica la categoria", "Modifica", c.Nome);
            if (newname != "" && newname != c.Nome)
            {
                c.Nome = newname;
                if (CategorieSchedeController.Modifica(c) >= 1)
                    LoadCategorie();
            }
        }

        private void btn_elimina_categoria_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_categorie.SelectedIndex == -1)
                return;

            if (Message.Confirm(DialogType.delete, "categoria") == false)
                return;

            int id = categorie[lbx_categorie.SelectedIndex].PKCategoria;
            if (CategorieSchedeController.Elimina(id) > 0)
                LoadCategorie();
        }

        #endregion


        #region SCHEDE

        private void btn_inserisci_Click(object sender, RoutedEventArgs e)
        {
            new WindowSchedaAllenamento().ShowDialog();
            UpdateCatCounter(lbx_categorie.SelectedIndex, 1);
            ShowSchedeCategoria();
        }

        private void btn_modifica_Click(object sender, RoutedEventArgs e)
        {
            if (id_scheda_sel == -1)
                return;

            new WindowSchedaAllenamento(id_scheda_sel).ShowDialog();
            ShowSchedeCategoria();
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (id_scheda_sel < 0)
                return;

            if (Message.Confirm(DialogType.delete, "scheda") == false)
                return;

            if (SchedeController.Elimina(id_scheda_sel) > 0)
            {
                UpdateCatCounter(lbx_categorie.SelectedIndex, -1);
                ShowSchedeCategoria();
            }
        }

        #endregion

        private void txt_cerca_esercizi_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Paging.BackHome(this);
        }
    }
}
