using Microsoft.Win32;
using System; using GestionePalestra.MVC;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace GestionePalestra
{
    /// <summary>
    /// Registrazione di un nuovo cliente [NOME VECCHIO, DOVREBE ESSERE INSERISCI CLIENTE]
    /// </summary>
    public partial class WindowInserisciUtente : Window
    {
        string caption = "cliente";
        Cliente c;

        /// <summary>
        /// la finestra consente l'inserimento di un cliente
        /// </summary>
        public WindowInserisciUtente()
        {
            InitializeComponent();
            ControlliForm();
            c = new Cliente();
        }

        /// <summary>
        /// imposta tutti i controlli e li riempie opportunamente
        /// </summary>
        void ControlliForm()
        {
            //impostazione date
            dp_data_nasc.SelectedDate = new DateTime(1970, 1, 1);
            dp_iscr.SelectedDate = DateTime.Today;

            //impostazione sesso
            cmb_sesso.ItemsSource = Common.Sesso;

            //istruttori
            Common.PopulateComboBox(ref cmb_istruttori, FactoryIstruttore.GetListIstruttori(), "NomeCompleto");

            //focus controllo
            txt_nome.Focus();
        }

        /// <summary>
        /// scorre tutti i controlli e li svuota
        /// </summary>
        void SvuotaForm()
        {
            foreach (object el in grid_main.Children)
            {
                if (el is TextBox)
                    (el as TextBox).Clear();

                if (el is ComboBox)
                    (el as ComboBox).SelectedIndex = 0;
            }
        }

        private void ControlloInserimentoNumeri(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text[0] >= 48 && e.Text[0] <= 57)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void btn_carica_immagine_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Image Files(*.bmp, *.jpg, *.png) | *.bmp; *.jpg; *.png ";
            if (of.ShowDialog() == true)
            {
                c.Immagine = Common.ByteFromFile(of.FileName);
                Common.SetGridImage(ref grid_img, c.Immagine);
            }
        }

        private void img_salva_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();

            
        }
        private void img_indietro_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btn_rimuovi_immagine_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Confirm(DialogType.delete, caption) == true)
            {
                c.Immagine = null;
                Common.SetGridImage(ref grid_img, c.Immagine);
            }
        }

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            //impostazione oggetto campi comuni
            c.Nome = txt_nome.Text;
            c.Cognome = txt_cognome.Text;
            c.CodiceFiscale = txt_cod.Text;
            c.Sesso = cmb_sesso.SelectedIndex;
            c.DataNascita = (DateTime)dp_data_nasc.SelectedDate;
            c.CittaNascita = txt_citta_nasc.Text;
            c.Telefono = txt_tel.Text;
            c.Email = txt_email.Text;
            c.DataIscrizione = (DateTime)dp_iscr.SelectedDate;
            c.FKIstruttore = ((Istruttore)cmb_istruttori.SelectedItem).PKIstruttore;
            c.FKStato = null;
            //*l'immagine viene gia impostata prima di arrivare qui


            //conferma
            if (Message.Confirm(DialogType.insert, caption) == false)
                return;

            //scrittura db
            int last_id = FactoryCliente.Inserisci(c);
            if (last_id > 0)
            {
                Common.LastUpdateClienti = DateTime.Now;
                Message.Alert(DialogType.insert, caption);
                c.PKCliente = last_id;
                if (ckb_apri_scheda.IsChecked == true)
                    new WindowSchedaUtente(c.PKCliente).Show();
                //FactoryStoricoEventi.Inserisci(c.PKCliente, "Inserito cliente");
                this.Close();
            }
        }

        private void cmb_istruttori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
