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
    /// Interaction logic for PageGestioneEsercizi.xaml
    /// </summary>
    public partial class PageGestioneEsercizi : Page
    {
        /*
          * [Note di funzionamento wrapPanel esercizi]
          * il wrappanel ha dimensioni fisse nello xaml,
          * ma nel code behind viene impostato come height = Auto (Double.NaN) dopo che gli esercizi sono stati caricati
          * per poter impostare l'altezza in base al contenuto che ospita
          * essendo quindi il wrappanel contenuto in uno scrollViewer superando le dimensioni dello stesso
          * lo scroll mostra la barra, in questo modo è possibile sorrere gli esercizi
          * metodo burino ma funzionante
          * 
          * [note gestione cateogorie]
          * le classi factory che caricano le categorie (GetListCategorie()) aggiungono
          * hard coded l'oggetto per gli elementi 'senza categoria'
          * creandolo con l'id a -1 esso sarà selezionabile nelle varie listbox o combobox
          * ed assegnato all'oggetto regolarmente, ma una volta arrivato nei rispettivi metodi Insert / Update
          * DBSet.SetInt lo trasforma in NULL
         */

        public PageGestioneEsercizi()
        {
            InitializeComponent();
        }

        string caption = "esercizi";
        int id_esercizio = -1;                      //id esercizio selezionato dal wrappanel
        int id_categoria = -1;                      //id dell'ultima categoria selezionata
        List<CategoriaEsercizio> categorie;         //lista delle categorie



        /// <summary>
        /// carica le categorie e popola la listbox con i controlli custom
        /// </summary>
        void LoadCategorie()
        {
            //pulizia vecchi elementi
            lbx_gruppi.Items.Clear();
            id_categoria = -1;

            //popolamento array e lista
            categorie = FactoryCategorieEsercizi.GetCategorie();
            foreach (CategoriaEsercizio c in categorie)
            {
                lbx_gruppi.Items.Add(new ControlCategoria(
                    c.PKCategoria,
                    c.Nome,
                    (c.CountEsercizi.HasValue) ? c.CountEsercizi + " esercizi" : c.Descrizione)
                );
            }

            //stampa contatori sulla statusbar
            txt_cat_count.Text = string.Format("{0} categorie", categorie.Count, FactoryEsercizi.NumeroEsercizi());
            txt_es_count.Text = string.Format("{0} esercizi", FactoryEsercizi.NumeroEsercizi());
        }



        /// <summary>
        /// popola il wrap panel con i controlli custom
        /// </summary>
        /// <param name="eserciziSrc">sorgente di esercizi da utilizzare</param>
        /// <param name="altText">testo da mostrare nella label in caso la lista sia vuota</param>
        void LoadEsercizi(List<Esercizio> eserciziSrc, string altText)
        {
            //pulizia vecchi controlli
            wrp_esercizi.Children.Clear();
            id_esercizio = -1;

            //popolamento wrappanel
            foreach (Esercizio es in eserciziSrc)
            {
                ControlEsercizi ce = new ControlEsercizi(es);
                ce.Margin = new Thickness(5, 5, 5, 5);
                ce.uc.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(ControlEsercizi_mouse_left);
                wrp_esercizi.Children.Add(ce);
            }

            //set dimensione automatica (vedi note sul funzionamento in alto)
            wrp_esercizi.Height = double.NaN;

            //stampa label (se la lista non contiene elementi)
            if (wrp_esercizi.Children.Count == 0)
            {
                string caption = (altText.Length > 0)
                    ? " per " + altText
                    : "";
                wrp_esercizi.Children.Add(new Label()
                {
                    Content = "Nessun esercizio trovato" + caption,
                    FontSize = 16,
                    Margin = new Thickness(20, 20, 0, 0)
                });
            }
        }



       



        #region CONTROL ESERCIZI

        private void ControlEsercizi_mouse_left(object sender, RoutedEventArgs e)
        {
            //assegnazione id
            id_esercizio = (sender as ControlEsercizi).Id;

            //impostazioni colore
            foreach (object o in wrp_esercizi.Children)
                (o as ControlEsercizi).rect.Stroke = Brushes.LightGray;
            (sender as ControlEsercizi).rect.Stroke = Brushes.LightSkyBlue;
        }

        #endregion



        #region RICERCA

        private void txt_cerca_esercizi_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txt_cerca_esercizi.Text != "")
                {
                    lbx_gruppi.SelectedIndex = -1;
                    LoadEsercizi(FactoryEsercizi.GetListCercaEsercizi(txt_cerca_esercizi.Text), "'" + txt_cerca_esercizi.Text + "'");
                    txt_cerca_esercizi.SelectAll();
                }
            }

            if (e.Key == Key.Delete)
                txt_cerca_esercizi.Clear();

            
        }

        private void btn_cerca_es_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion



        #region ESERCIZI

        private void btn_modifica_Click(object sender, RoutedEventArgs e)
        {
            if (id_esercizio <= 0)
                return;

            new WindowEsercizio(id_esercizio, FormAction.update).ShowDialog();
            LoadCategorie();
            LoadEsercizi(FactoryEsercizi.GetListEserciziCategoria(id_categoria), "la categoria selezionata");
        }

        private void btn_inserisci_Click(object sender, RoutedEventArgs e)
        {
            new WindowEsercizio(id_categoria, FormAction.insert).ShowDialog();
            LoadCategorie();
            LoadEsercizi(FactoryEsercizi.GetListEserciziCategoria(id_categoria), "la categoria selezionata");
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (id_esercizio == -1)
                return;

            if (Message.Confirm(DialogType.delete, caption) == false)
                return;

            if (FactoryEsercizi.Elimina(id_esercizio) > 0)
            {
                LoadCategorie();
                LoadEsercizi(FactoryEsercizi.GetListEserciziCategoria(id_categoria), "la categoria selezionata");
            }
        }

        #endregion



        #region CATEGORIE

        private void lbx_gruppi_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbx_gruppi.SelectedIndex == -1)
                return;

            ControlCategoria cc = ((ControlCategoria)lbx_gruppi.SelectedItem);
            id_esercizio = -1;
            id_categoria = cc.Id;
            LoadEsercizi(FactoryEsercizi.GetListEserciziCategoria(id_categoria), "la categoria selezionata");
        }

        private void btn_elimina_categoria_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_gruppi.SelectedIndex == -1)
            {
                Message.Alert(AlertType.warning, "nessuna categoria selezionata", "categorie");
                return;
            }
                

            if (Message.Question("Eliminare la categoria corrente?\nGli esercizi contenuti precendentemente saranno visibili nell'elenco degli esercizi senza categoria", caption) == true)
            {
                if (FactoryCategorieEsercizi.Delete(categorie[lbx_gruppi.SelectedIndex].PKCategoria) > 0)
                {
                    Message.Alert(DialogType.delete, caption);
                    LoadCategorie();
                }
            }
        }

        private void btn_aggiungi_categoria_Click(object sender, RoutedEventArgs e)
        {
            string input = Interaction.InputBox("Inserisci la nuova categoria", "Inserimento", "");
            if (input != "")
            {
                CategoriaEsercizio c = new CategoriaEsercizio() { Nome = input, Descrizione = "" };
                if (FactoryCategorieEsercizi.InsertUpdate(c) > 0)
                    LoadCategorie();
            }
        }

        private void btn_modifica_categoria_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_gruppi.SelectedIndex == -1)
                return;

            CategoriaEsercizio c = categorie[lbx_gruppi.SelectedIndex];
            string newname = Interaction.InputBox("Modifica la categoria", "Modifica", c.Nome);
            if (newname != "" && newname != c.Nome)
            {
                c.Nome = newname;
                if (FactoryCategorieEsercizi.InsertUpdate(c) > 0)
                    LoadCategorie();
            }
        }

        #endregion



        #region HEADER

        private void btn_import_cartella_Click(object sender, RoutedEventArgs e)
        {
            string mex = "Sarà necessario selezionare una cartella, le immagini contenute in essa e nelle sue sottocartelle verranno rielaborate ed importate come esercizi.\n Le categorie di appartenenza saranno generate partendo dal nome delle cartelle.";
            MessageBox.Show(mex, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            Import.CaricaCartellaEsercizi();

            id_esercizio = -1;
            id_categoria = -1;
            LoadCategorie();
            lbx_gruppi.SelectedIndex = -1;
        }


        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategorie();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //il wrappanel viene ridimensionato quando la finestra cambia dimensione
            wrp_esercizi.Width = sv.Width;
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Paging.BackHome(this);
        }
    }
}
