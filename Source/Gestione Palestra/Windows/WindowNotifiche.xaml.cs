using System; using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Data;
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
    /// finestra task piccola per mostrare l'anteprima delle notifhce
    /// </summary>
    public partial class WindowNotifiche : Window
    {
        DataTable dtSrc;
        public WindowNotifiche(DataTable dtSrc)
        {
            InitializeComponent();
            this.dtSrc = dtSrc;
            loadNotifiche();

            //posizione
            //this.Top = System.Windows.Forms.Cursor.Position.Y;
            //this.Left = System.Windows.Forms.Cursor.Position.X;
        }

        void loadNotifiche()
        {
            foreach(DataRow dr in dtSrc.Rows)
            {
                lbx_notifiche.Items.Add(new ControlCategoria(
                    "BlueMessage.png",
                    Convert.ToInt32(dr[0]),
                    dr[1].ToString(),
                    dr[4].ToString()));
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void lbx_notifiche_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbx_notifiche.SelectedIndex == -1)
                return;

            WindowMostraAvviso ma = new WindowMostraAvviso(Convert.ToInt16(dtSrc.Rows[lbx_notifiche.SelectedIndex][0]));
            ma.Topmost = true;
            ma.ShowDialog();
        }
    }
}
