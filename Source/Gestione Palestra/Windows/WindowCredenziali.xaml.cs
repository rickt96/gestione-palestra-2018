using System.Windows;
using GestionePalestra.MVC;


namespace GestionePalestra
{
    /// <summary>
    /// Impostazione delle credenziali di login per gli istruttori
    /// </summary>
    public partial class WindowCredenziali : Window
    {
        string caption = "credenziali istruttore";
        Istruttore i = null;

        public WindowCredenziali(int id_istruttore)
        {
            InitializeComponent();
            i = new Istruttore();
            i.PKIstruttore = id_istruttore;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //caricamento permessi
            Common.PopulateComboBox(ref cmb_permessi, FactoryLivelliPermessi.GetListPermessi(), "Nome");

            //caricamento istruttore
            i = FactoryIstruttore.Seleziona(i.PKIstruttore);
            if (i != null)
            {
                lbl_nome.Content = i.NomeCompleto;
                pwb1.Password = i.Password;
                pwb2.Password = pwb1.Password;
                foreach (LivelloPermesso lp in cmb_permessi.Items)
                    if (lp.PKLivelloPermesso == i.FKLivelliPermessi)
                        cmb_permessi.SelectedItem = lp;
            }
            else
            {
                this.Close();
            }
        }

        private void btn_modifica_credenziali_Click(object sender, RoutedEventArgs e)
        {
            if(pwb1.Password != pwb2.Password)
            {
                MessageBox.Show("Le password non corrispondono", "Credenziali", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //impostazione credenziali e permessi
            i.Password = pwb1.Password;
            i.FKLivelliPermessi = (cmb_permessi.SelectedItem as LivelloPermesso).PKLivelloPermesso;

            //scrittura
            if (FactoryIstruttore.Modifica(i) > 0)
            {
                Message.Alert(DialogType.update, caption);
                this.Close();
            }
                
        }

        private void cmb_permessi_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LivelloPermesso lp = (cmb_permessi.SelectedItem as LivelloPermesso);
            cmb_permessi.ToolTip = (lp.Descrizione != "") ? lp.Descrizione : "nessuna descrizione";
        }
    }
}
