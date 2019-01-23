using System.Windows.Controls;
using GestionePalestra.MVC;


namespace GestionePalestra
{
    /// <summary>
    /// Controllo custom per la card di visualizzazione di un enercizio
    /// </summary>
    public partial class ControlEsercizioScheda : UserControl
    {
        /// <summary>
        /// Esercizio seduta con cui sarà popolato il controllo
        /// </summary>
        EsercizioSeduta es { get; set; }

        public ControlEsercizioScheda()
        {
            InitializeComponent();
        }

        /// <summary>
        /// prende l'esercizio e lo usa per compilare il controllo
        /// </summary>
        public ControlEsercizioScheda(EsercizioSeduta es)
        {
            InitializeComponent();
            this.es = es;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

            //immagine esercizio
            Common.SetGridImage(ref grid_img, es.Esercizio.Immagine);

            //nome esercizio
            lbl_nome.Content = es.Esercizio.Nome;

            //nome categoria
            lbl_categoria.Content = es.Esercizio.Categoria.Nome;

            //set(serie-ripetizioni / durata)
            if(es.ATempo == false)
            {
                lbl_set.Content = es.Serie;
            }
            else
            {
                lbl_set.Content = (es.Durata.HasValue) ? es.Durata.Value.ToString(@"hh\:mm") : "--:--";
            }

            //carico
            lbl_carico.Content = (es.Carico.HasValue) ? es.Carico + " kg" : "-- kg";

            //recupero
            if(es.Recupero.HasValue)
                lbl_recupero.Content = string.Format("{0}'{1}\"", es.Recupero.Value.Minutes, es.Recupero.Value.Seconds);

            //metodo esercizio (standard, ss, circuito)
            //lbl_tipo.Content = Common.MetodoEsercizi[es.Metodo];

            //note svolgimento
            lbl_note.Content = es.Note;
            lbl_note.ToolTip = es.Note;

            //ordine
            lbl_ordine.Content = es.Ordine;
        }

        public void SetOrdine(int o)
        {
            es.Ordine = o;
            lbl_ordine.Content = o;
        }
    }
}
