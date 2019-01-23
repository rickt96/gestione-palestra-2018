using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GestionePalestra.MVC;


namespace GestionePalestra
{
    /// <summary>
    /// Window per eseguire il login al programma
    /// </summary>
    public partial class WindowAccesso : Window
    {
        public WindowAccesso()
        {
            InitializeComponent();

            //elenco istruttori
            Common.PopulateComboBox(ref cmb_user, FactoryIstruttore.GetListIstruttori(), "NomeCompleto");
            cmb_user.Focus();          
        }


        /// <summary>
        /// quando la casella della password prende il focus seleziona tutto il testo
        /// </summary>
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
           (sender as PasswordBox).SelectAll();
        }

        /// <summary>
        /// mostra il link per impostare la connessione [RIMOSSO]
        /// </summary>
        private void hl_connessione_Click(object sender, RoutedEventArgs e)
        {
            new WindowConnessione().ShowDialog();
        }

        /// <summary>
        /// quando cambia instruttore imposta l'immagine di profilo
        /// </summary>
        private void cmb_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Istruttore i = (Istruttore)cmb_user.SelectedItem;
            img_user.ImageSource = Common.BitmapFromByte(i.Immagine, "BlueLogin.png");
        }


        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            if (cmb_user.SelectedIndex == -1)
                return;

            //verifica credenziali accesso
            Istruttore i = (Istruttore)cmb_user.SelectedItem;
            if (Session.Login(i.PKIstruttore, pwb_pw.Password) == true)
                this.DialogResult = true;
        }
        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
