using System; using GestionePalestra.MVC;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Collections.Generic;

namespace GestionePalestra
{
    /// <summary>
    /// Finestra per le annotazioni stile notepad telefono
    /// </summary>
    public partial class WindowDiarioAnnotazioni : Window
    {
        string caption = "annotazione";
        //Annotazione a;

        public WindowDiarioAnnotazioni()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshAnnotazioni();
        }


        /// <summary>
        /// carica la lista delle annotazioni
        /// </summary>
        void refreshAnnotazioni()
        {
            lbx_elementi.IsEnabled = true;
            lbx_elementi.Items.Clear();
            foreach (Annotazione a in FactoryAnnotazioni.GetAnnotazioni(Session.User.PKIstruttore))
                lbx_elementi.Items.Add(new ControlElementoDiario(a));

            if (lbx_elementi.Items.Count == 0)
            {
                lbx_elementi.Items.Add(new Label() { Content = "Nessuna annotazione", FontSize = 14, IsEnabled = false });
                lbx_elementi.IsEnabled = false;
            }
        }


        #region FUNZIONI

        private void btn_inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_elementi.IsEnabled == false)
            {
                lbx_elementi.Items.Clear();
                lbx_elementi.IsEnabled = true;
            }

            ControlElementoDiario ed = new ControlElementoDiario();
            lbx_elementi.Items.Add(ed);
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_elementi.SelectedIndex == -1)
                return;

            if (Message.Confirm(DialogType.delete, caption) == false)
                return;

            int id = ((ControlElementoDiario)lbx_elementi.SelectedItem).a.PKAnnotazione;
            if (id > 0)
            {
                int res = FactoryAnnotazioni.Delete(id);
                if (res > 0)
                    refreshAnnotazioni();

            }
            else
            {
                lbx_elementi.Items.Remove(((ControlElementoDiario)lbx_elementi.SelectedItem));
            }
        }

        #endregion


    }
}
