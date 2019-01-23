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

namespace GestionePalestra.Pages
{
    /// <summary>
    /// Logica di interazione per PageGestioneIstruttori.xaml
    /// </summary>
    public partial class PageGestioneIstruttori : Page
    {
        public PageGestioneIstruttori()
        {
            InitializeComponent();
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Paging.BackHome(this);
        }
    }
}
