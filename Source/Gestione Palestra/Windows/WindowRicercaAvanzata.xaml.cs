using System; using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Linq;

namespace GestionePalestra
{
    /// <summary>
    /// Finestra per la ricerca avanzata e combinata sui clienti
    /// </summary>
    public partial class WindowRicercaAvanzata : Window
    {
        DataGrid dg;

        public WindowRicercaAvanzata(ref DataGrid dg)
        {
            InitializeComponent();

            //datagrid riferimento
            this.dg = dg;
        }

        private void btn_annulla_Click(object sender, RoutedEventArgs e)
        {
            foreach (object c in grid_campi.Children)
            {
                if (c.GetType().ToString() == "System.Windows.Controls.TextBox")
                    (c as TextBox).Text = string.Empty;
                if (c.GetType().ToString() == "System.Windows.Controls.RadioButton")
                    (c as RadioButton).IsChecked = false;
                if (c.GetType().ToString() == "System.Windows.Controls.ComboBox")
                    (c as ComboBox).SelectedIndex = -1;
                if (c.GetType().ToString() == "System.Windows.Controls.DatePicker")
                    (c as DatePicker).SelectedDate = null;
            }
        }
        private void img_indietro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void btn_cerca_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();

            string query = "";

            List<string> clauses = new List<string>();

            //nome
            if (txt_nome.Text != string.Empty)
                clauses.Add(string.Format(" UPPER(clienti.nome) LIKE '%{0}%'", txt_nome.Text.ToUpper()));
            //cognome
            if (txt_cognome.Text != string.Empty)
                clauses.Add(string.Format(" UPPER(clienti.cognome) LIKE '%{0}%'", txt_cognome.Text.ToUpper()));
            //codice fiscale
            if (txt_cf.Text != string.Empty)
                clauses.Add(string.Format(" UPPER(clienti.codice_fiscale) LIKE '%{0}%'", txt_cf.Text.ToUpper()));
            //sesso
            if (cmb_sesso.SelectedIndex > -1)
                clauses.Add(string.Format(" clienti.sesso = {0}", cmb_sesso.SelectedIndex));
            //citta
            if (txt_citta.Text != string.Empty)
                clauses.Add(string.Format(" citta LIKE '%{0}%'", txt_citta.Text.ToUpper()));
            //anno - mese nascita
            if (cmb_anno_nascita.SelectedIndex > -1)
                clauses.Add(string.Format(" YEAR(clienti.data_nascita) = {0}", cmb_anno_nascita.SelectedItem));
            if (cmb_mese_nascita.SelectedIndex > -1)
                clauses.Add(string.Format(" MONTH(clienti.data_nascita) = {0}", cmb_mese_nascita.SelectedIndex+1));
            //tel
            if (txt_tel.Text != string.Empty)
                clauses.Add(string.Format(" UPPER(clienti.telefono) LIKE '%{0}%'", txt_tel.Text.ToUpper()));
            //mail
            if (txt_email.Text != string.Empty)
                clauses.Add(string.Format(" UPPER(clienti.email) LIKE '%{0}%'", txt_email.Text.ToUpper()));
            //stato
            if (cmb_stato.SelectedIndex > -1)
                clauses.Add(string.Format(" stato = {0}", cmb_stato.SelectedIndex));
            //istruttore
            if (cmb_istruttore.SelectedIndex > -1)
                clauses.Add(string.Format("clienti.id_istruttore = {0}", ((Istruttore)cmb_istruttore.SelectedItem).PKIstruttore));

            //applicazione
            if (clauses.Count > 0)
            {
                query += " WHERE (";
                bool primo = true;
                foreach (string c in clauses)       //scorre tutte le stringhe inserite
                {
                    if (primo == true)              //se è il primo inserimento scrive direttamente la condizione
                    {
                        query += c;
                        primo = false;
                    }
                    else                            //altrimenti se non è la prima scrive prima l'and, poi la ocndizione
                    {
                        query += " AND " + c;
                    }
                }
                query += ")";                       //chiude le parentesi

                DataTable dataSrc = ClienteController.GetTableClienti(query);
                if (dataSrc != null)
                {
                    Title = string.Format("Ricerca avanzata clienti - {0} campi impostati | {1} risultati", clauses.Count, dataSrc.Rows.Count);
                    if (dataSrc.Rows.Count > 0)
                        dg.ItemsSource = dataSrc.DefaultView;
                    else
                        MessageBox.Show("Nessun risultato di ricerca", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            //Clipboard.SetText(query);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //FORM FILLING

            //sesso
            cmb_sesso.ItemsSource = Common.Sesso;

            //stato
            Common.PopulateComboBox(ref cmb_stato, TipiController.GetTipi(TipiStati.StatiCliente), "Valore");

            //istruttori
            cmb_istruttore.ItemsSource = null;
            cmb_istruttore.ItemsSource = IstruttoriController.GetTableIstruttori(null).DefaultView;
            cmb_istruttore.DisplayMemberPath = "Nome";

            //anno
            cmb_anno_nascita.ItemsSource = Enumerable.Range(1920, DateTime.Now.Year).Reverse();
            //for (int i = DateTime.Now.Year; i >= 1920; i--)
            //    cmb_anno_nascita.Items.Add(i);

            //mese
            cmb_mese_nascita.ItemsSource = new string[] { "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "ottobre", "Novembre", "Dicembre" };
        }
    }
}