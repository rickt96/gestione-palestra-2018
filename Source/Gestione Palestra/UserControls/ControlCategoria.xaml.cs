using System; using GestionePalestra.MVC;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace GestionePalestra
{
    /// <summary>
    /// Controllo custom per visualizzare le categorie o i gruppi
    /// si puo impostare la scritta header e la caption sotto piccola e facoltativamente un'immagine
    /// </summary>
    public partial class ControlCategoria : UserControl
    {
        public int Id { get; set; }
        public object Obj { get; set; }

        public ControlCategoria(string icon, int id, string header, string header2)
        {
            InitializeComponent();
            //id
            Id = id;
            //caricamento immagine
            img_src.Source = Common.BitmapFromIcon(icon);
            //assegnazione header
            lbl_header.Content = header;
            lbl_header_2.Content = header2;
        }

        public ControlCategoria(int id, string header, string header2)
        {
            InitializeComponent();
            //id
            Id = id;
            //assegnazione header
            lbl_header.Content = header;
            lbl_header_2.Content = header2;
        }

        //public string Header { set { lbl_header.Content = value; } }
        //public string Header2 { set { lbl_header_2.Content = value; } }
    }
}
