using System; using GestionePalestra.MVC;
using System.Linq;
using System.Windows.Controls;


namespace GestionePalestra
{
    /// <summary>
    /// Controllo custom card per la visualizzazione dei dati di una scheda
    /// </summary>
    public partial class ControlScheda : UserControl
    {
        public int Id { get; set; }
        public Scheda s { get; set; }

        //public ControlScheda(int id, string nome_scheda, string descr, string obiettivo, int diff, int n_sedute, int freq, DateTime ins)
        //{
        //    InitializeComponent();

        //    Id = id;
        //    lbl_nome_scheda.Content = (nome_scheda != "") ? nome_scheda : "nessun nome";
        //    acctxt_descr.Text = (descr != "") ? descr : "nessuna descrizione";
        //    lbl_obiettivo.Content = (obiettivo != "") ? obiettivo : "nessuno";
        //    lbl_difficolta.Content = String.Concat(Enumerable.Repeat("★", diff)); //✩
        //    lbl_sedute_totali.Content = n_sedute+" sedute";
        //    lbl_freq_sett.Content = freq;
        //    lbl_data_ins.Content = ins.ToShortDateString();
        //}
        public ControlScheda(Scheda s)
        {
            InitializeComponent();
            this.s = s;
            Id = s.PKScheda;
            lbl_nome_scheda.Content = (s.Nome != "") ? s.Nome : "nessun nome";
            acctxt_descr.Text = (s.Dettagli != "") ? s.Dettagli : "nessuna descrizione";
            lbl_obiettivo.Content = (s.Obiettivo != "") ? s.Obiettivo : "nessuno";
            lbl_difficolta.Content = String.Concat(Enumerable.Repeat("★", (int)s.Difficolta)); //✩
            lbl_sedute_totali.Content = s.NumeroSedute + " sedute";
            lbl_freq_sett.Content = s.FrequenzaSettimanale;
            lbl_data_ins.Content = (s.DataInserimento.HasValue) ? s.DataInserimento.Value.ToString("dd/MM/yy hh:mm") : "";

        }

    }
}
