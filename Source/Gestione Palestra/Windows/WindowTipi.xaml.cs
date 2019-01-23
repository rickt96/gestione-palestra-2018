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
using System.Windows.Shapes;


namespace GestionePalestra
{
    /// <summary>
    /// Logica di interazione per WindowTipi.xaml
    /// </summary>
    public partial class WindowTipi : Window
    {
        class TipoCrud : Tipo
        {
            public bool Elimina { get; set; }
            public bool Aggiorna { get; set; }
            public TipoCrud(Tipo t)
            {
                PKTipo = t.PKTipo;
                Valore = t.Valore;
                Descrizione = t.Descrizione;
                Colore = t.Colore;
            }
        }

        int start_index = -1;
        List<Tipo> tipi;
        List<int> toUpdate;
        public WindowTipi(int start_index)
        {
            InitializeComponent();
            this.start_index = start_index;
        }

        private void cmb_sel_tipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;

            toUpdate = new List<int>();
            switch(cmb_sel_tipo.SelectedIndex)
            {
                case 0: tipi = FactoryTipi.GetTipi(TipiStati.StiliVita); break;
                case 1: tipi = FactoryTipi.GetTipi(TipiStati.TipiObiettivi); break;
                case 2: tipi = FactoryTipi.GetTipi(TipiStati.TipologieAvvisi); break;
                case 3: tipi = FactoryTipi.GetTipi(TipiStati.StatiCliente); break;
                default: tipi = new List<Tipo>(); break;
            }

            dg_tipi.ItemsSource = null;
            dg_tipi.ItemsSource = tipi;
        }

        private void dg_tipi_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            toUpdate.Add(dg_tipi.SelectedIndex);
        }

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            string tabella = "";
            switch (cmb_sel_tipo.SelectedIndex)
            {
                case 0: tabella = "anamnesi_tipi_lifestyle"; break;
                case 1: tabella = "anamnesi_tipi_obiettivi"; break;
                case 2: tabella = "avvisi_tipi_avvisi"; break;
                case 3: tabella = "clienti_tipi_stati"; break;
            }

            FactoryTipi.InsertUpdate(tipi[dg_tipi.SelectedIndex], tabella);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_sel_tipo.SelectedIndex = start_index;
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (dg_tipi.SelectedIndex == -1)
                return;

            string tabella = "";
            switch (cmb_sel_tipo.SelectedIndex)
            {
                case 0: tabella = "anamnesi_tipi_lifestyle"; break;
                case 1: tabella = "anamnesi_tipi_obiettivi"; break;
                case 2: tabella = "avvisi_tipi_avvisi"; break;
                case 3: tabella = "clienti_tipi_stati"; break;
            }


            if (tipi[dg_tipi.SelectedIndex].PKTipo > 0)
                FactoryTipi.EliminaTipo(tipi[dg_tipi.SelectedIndex].PKTipo, tabella);
            else
                tipi.RemoveAt(dg_tipi.SelectedIndex);
            dg_tipi.Items.Refresh();
        }
    }
}
