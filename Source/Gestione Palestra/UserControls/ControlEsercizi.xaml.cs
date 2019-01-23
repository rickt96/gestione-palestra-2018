using System; using GestionePalestra.MVC;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace GestionePalestra
{
    /// <summary>
    /// Controllo custom thrumbnail per la visualizzazione di un oggetto (immagine + testo)
    /// </summary>
    public partial class ControlEsercizi : UserControl
    {
        public int Id { get; set; }
        public Esercizio es { get; set; }

        public ControlEsercizi(int id, byte[] img, string header, int difficolta)
        {
            InitializeComponent();

            Id = id;
            //assegnazione header
            lbl_header.Content = header;
            lbl_header.ToolTip = header;
            lbl_header_2.Content = String.Concat(Enumerable.Repeat("★", difficolta));
            //caricamento imamgine
            Common.SetGridImage(ref grid_img, FactoryEsercizi.Seleziona(id).Immagine);
        }
        public ControlEsercizi(Esercizio es)
        {
            InitializeComponent();

            //assegnazione controllo
            this.es = es;

            Id = es.PKEsercizio;
            //applicazione campi
            lbl_header.Content = es.Nome;
            lbl_header.ToolTip = es.Nome;
            lbl_header_2.Content = (es.Difficolta.HasValue)
                    ? String.Concat(Enumerable.Repeat("★", es.Difficolta.Value))
                    : "Nessuna difficoltà selezionata";
            //caricamento imamgine
            Common.SetGridImage(ref grid_img, es.Immagine);
        }

        private void uc_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //uc.Background = Brushes.White;
        }

        private void uc_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //uc.Background = Brushes.LightBlue;
        }

        private void uc_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            uc.Background = Brushes.White;
        }

        private void uc_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            uc.Background = Brushes.LightBlue;
        }
    }
}
