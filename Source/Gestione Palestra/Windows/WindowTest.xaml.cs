using System; using GestionePalestra.MVC;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionePalestra
{
    /// <summary>
    /// finestra per l'inserimento, la modifica e l'eliminazione di un test
    /// </summary>
    public partial class WindowTest : Window
    {
        Test t;

        /// <summary>
        /// creazione o modifica test
        /// </summary>
        /// <param name="funzione">se inserimento l'id è riferito al cliente proprietario,  altrimenti è l'id dell'elemento</param>
        /// <param name="id_el">inserimento: id_cliente | modifica: id_test</param>
        public WindowTest(FormAction funzione, int id_el)
        {
            InitializeComponent();
            t = new Test();

            if (funzione == FormAction.insert) {
                t.FKCliente = id_el;
                btn_elimina_test.IsEnabled = false;
                this.Title = "Nuovo test";
                MessageBox.Show(t.FKCliente.ToString());
            }    
            else {
                t.PKTest = id_el;
                this.Title = "Modifica test";
            }
                
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
            // 1. caricamento campi
            //
            Common.PopulateComboBox(ref cmb_esercizi, EserciziController.GetListEsercizi(), "Nome");
            cmb_reps.ItemsSource = Enumerable.Range(1, 40);


            //
            // 2. caricamento oggetto dal db se è una modifica
            //
            if(t.PKTest > 0)
            {
                t = TestController.Seleziona(t.PKTest);
                if (t != null)
                {
                    //selezione esericizio del test corrente
                    foreach (Esercizio es in cmb_esercizi.Items)
                        if (es.PKEsercizio == t.FKEsercizio)
                            cmb_esercizi.SelectedItem = es;

                    //selezione reps
                    cmb_reps.SelectedItem = t.Ripetizioni;

                    //carico
                    iud_carico.Value = t.Carico;
                }
            }
        }

        private void btn_calcola_test_Click(object sender, RoutedEventArgs e)
        {
            lstv.ItemsSource = Formule.CalcoloMassimale(iud_carico.Value.Value, (int)cmb_reps.SelectedItem);
        }
        


        private void btn_elimina_test_Click(object sender, RoutedEventArgs e)
        {
            if (t.PKTest < 1)
                return;

            if (TestController.Delete(t.PKTest) > 0)
                this.Close();
        }

        private void btn_inserisci_test_Click(object sender, RoutedEventArgs e)
        {
            //aggiorna campi test corrente
            //t = new Test();
            //t.PKTest: non serve se è un insert | è gia recuperato in window loaded
            //t.FKCliente: recuperato dal paramentro se è un insert | preso dal window loaded se è una modififca 
            t.FKEsercizio = ((Esercizio)cmb_esercizi.SelectedItem).PKEsercizio;
            t.Ripetizioni = (int)cmb_reps.SelectedItem;
            t.Carico = iud_carico.Value.Value;

            //insert-update
            if (TestController.InsertUpdate(t) > 0)
                Message.Alert(AlertType.info, "Modifiche inserite", "test");
        }


    }
}
