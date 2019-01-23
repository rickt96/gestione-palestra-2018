using System.Windows;
using GestionePalestra.MVC;


namespace GestionePalestra
{
    /// <summary>
    /// Finestra di inserimento e modifica di un esercizio
    /// </summary>
    public partial class WindowEsercizio : Window
    {
        Esercizio e;
        FormAction _tipoFunzione;
        int _gruppoSelezionato;

        /// <summary>
        /// crea o modifica un esercizio
        /// </summary>
        /// <param name="id">se insert: id gruppo preselezionato | se modifica: id esercizio</param>
        /// <param name="funzione"></param>
        public WindowEsercizio(int id, FormAction funzione)
        {
            InitializeComponent();

            _tipoFunzione = funzione;
            e = new Esercizio();

            switch (funzione)
            {
                case FormAction.insert:
                    btn_elimina.Visibility = Visibility.Collapsed;
                    _gruppoSelezionato = id;
                    break;

                case FormAction.update:
                    e.PKEsercizio = id;
                    break;
            }
        }


        /// <summary>
        /// carica l'oggetto dell'esercizio dal database
        /// </summary>
        void CaricaEsercizio()
        {
            e = FactoryEsercizi.Seleziona(e.PKEsercizio);
            if (e != null)
            {
                //impostazione dati
                txt_nome.Text = e.Nome;
                sld_difficolta.Value = (e.Difficolta.HasValue) ? e.Difficolta.Value : 1;
                txt_descrizione.Text = e.Descrizione;

                //impostazione categoria esercizio
                foreach (CategoriaEsercizio ce in cmb_categoria.Items)
                    if (ce.PKCategoria == e.Categoria.PKCategoria)
                        cmb_categoria.SelectedItem = ce;
            }
        }

        /// <summary>
        /// caricamento finestra ed impostazione dati
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //***************************************
            // caricamento controlli form
            //***************************************

            //categorie esercizi
            Common.PopulateComboBox(ref cmb_categoria, FactoryCategorieEsercizi.GetCategorie(), "Nome");

            //impostazione categoria preselezionata
            foreach (CategoriaEsercizio c in cmb_categoria.Items)
                if (c.PKCategoria == _gruppoSelezionato)
                    cmb_categoria.SelectedItem = c;


            //***************************************
            // applicazione dati
            //***************************************
            switch (_tipoFunzione)
            {
                case FormAction.insert:
                    Title = "Nuovo esercizio";
                    btn_salva.ToolTip = "Inserisci il nuovo esercizio";
                    break;

                case FormAction.update:
                    Title = "Modifica esercizio";
                    btn_salva.ToolTip = "Salva le modifiche";
                    CaricaEsercizio();
                    break;
            }

            

            //carica immagine (in qualsiasi caso)
            Common.SetGridImage(ref grid_img, this.e.Immagine);
        }


        #region HEADER

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            //impostazione dati
            this.e.Nome = txt_nome.Text;
            this.e.Difficolta = (int)sld_difficolta.Value;
            this.e.Descrizione = txt_descrizione.Text;
            this.e.Categoria = (cmb_categoria.SelectedIndex > -1) ? (cmb_categoria.SelectedItem as CategoriaEsercizio) : new CategoriaEsercizio();

            if (FactoryEsercizi.InsertUpdate(this.e) > 0)
            {
                Message.Alert(DialogType.update, "esercizio");
                this.Close();
            }
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if(Message.Confirm( DialogType.delete, "Esercizio"))
            {
                if (FactoryEsercizi.Elimina(this.e.PKEsercizio) > 0)
                    this.DialogResult = true;
            }
        }

        #endregion



        /// <summary>
        /// carica una nuova immagine
        /// </summary>
        private void btn_carica_Click(object sender, RoutedEventArgs e)
        {
            string file = Common.OpenFileDialogImage();
            if (file != "")
            {
                this.e.Immagine = Common.ByteFromFile(file);
                Common.SetGridImage(ref grid_img, this.e.Immagine);
            }
        }

        /// <summary>
        /// rimuove l'immagine seleziona
        /// </summary>
        private void btn_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            this.e.Immagine = null;
            Common.SetGridImage(ref grid_img, this.e.Immagine);
        }

        
    }
}
