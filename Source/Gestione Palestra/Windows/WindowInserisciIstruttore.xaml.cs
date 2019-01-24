using Microsoft.Win32;
using System; using GestionePalestra.MVC;
using System.Windows;
using System.Windows.Input;


namespace GestionePalestra
{
    /// <summary>
    /// FInestra per l'inserimento di un nuovo istruttore
    /// </summary>
    public partial class WindowInserisciIstruttore : Window
    {
        string caption = "istruttore";
        Istruttore i = new Istruttore();

        public WindowInserisciIstruttore()
        {
            InitializeComponent();
        }

        private void img_indietro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmb_sesso.ItemsSource = Common.Sesso;
            Common.PopulateComboBox(ref cmb_permessi, LivelliPermessiController.GetListPermessi(), "Nome");
            dp_data_nasc.SelectedDate = new DateTime(1970, 1, 1);
        }

        private void btn_carica_immagine_Click(object sender, RoutedEventArgs e)
        {
            string file = Common.OpenFileDialogImage();
            if(file != "")
            {
                i.Immagine = Common.ByteFromFile(file);
                Common.SetGridImage(ref grid_img, i.Immagine);
            }
        }

        private void btn_rimuovi_immagine_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.delete, caption))
            {
                i.Immagine = null;
                Common.SetGridImage(ref grid_img, i.Immagine);
            }
        }

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            if (pwb_password.Password != pwb_password_ripeti.Password || pwb_password.Password.Length < 5)
            {
                MessageBox.Show("Le password non sono valide", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Message.Confirm(DialogType.insert, caption) == false)
                return;

            //inserimento istruttore
            i.Nome = txt_nome.Text;
            i.Cognome = txt_cognome.Text;
            i.Sesso = cmb_sesso.SelectedIndex;
            i.Citta = txt_citta_nasc.Text;
            i.DataNascita = dp_data_nasc.SelectedDate.Value;
            i.Telefono = txt_tel.Text;
            i.Email = txt_email.Text;
            i.Password = pwb_password.Password;
            i.FKLivelliPermessi = (cmb_permessi.SelectedIndex > -1) ? (cmb_permessi.SelectedItem as LivelloPermesso).PKLivelloPermesso : (int?)null;

            //scrivi db
            if (IstruttoriController.Inserisci(i) > 0)
            {
                Message.Alert(DialogType.insert, caption);
                this.Close();
            }
        }
    }
}
