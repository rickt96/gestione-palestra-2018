using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace GestionePalestra
{
    /// <summary>
    /// Logica di interazione per WindowSysUtility.xaml
    /// </summary>
    public partial class WindowSysUtility : Window
    {
        public WindowSysUtility()
        {
            InitializeComponent();

            //for(int i = 0; i <= 10; i++)
            //{
            //    Expander ex = new Expander() {
            //        Header = new Label() { Content = "prova "+i, Foreground = Brushes.Blue, FontSize = 18},
            //        Padding = new Thickness(5,5,5,0)
            //    };
            //    TextBox t = new TextBox() { Text = "sono un testoa  caso asasasadad", Padding = new Thickness(3, 3, 3, 3), IsReadOnly = true };
            //    StackPanel sp = new StackPanel();
            //    sp.Children.Add(t);
            //    sp.Children.Add(new CheckBox() { Content = "ciao", HorizontalAlignment = HorizontalAlignment.Left});

            //    ex.Content = sp;
            //    ex.IsExpanded = false;
            //    ex.Width = lbx_prova_expander.Width - 35;
            //    lbx_prova_expander.Items.Add(ex);
                
            //}
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void prova_parametr_insert_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //lbx_prova_avvisi.Items.Clear();
            //for(int i = 0; i<5; i++)
            //{
            //    //ControlAvviso cv = new ControlAvviso();
            //    ControlEsercizioScheda cv = new ControlEsercizioScheda();
            //    cv.Width = lbx_prova_avvisi.Width;
            //    cv.lbl_ordine.Content = i;
            //    lbx_prova_avvisi.Items.Add(cv);
            //}


            //List<Esercizio> lst = new List<Esercizio>() {
            //    new Esercizio(){Nome="asd"},
            //    new Esercizio(){Nome="aaaaa"},
            //    new Esercizio(){Nome="bbbbb"},
            //    new Esercizio(){Nome="eeee"}
            //};

            //foreach(Esercizio es in lst)
            //{
            //    DataGridRow dr = new DataGridRow();
            //    dr.Background = Brushes.Red;
            //    dr.DataContext = es;
            //    dg_prova.Items.Add(dr);
            //}


        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
             
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void grid_clienti_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("click griglia");
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowSelezioneCliente sc = new WindowSelezioneCliente();
            sc.Show();
        }

        private void btn_cm_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Import.CaricaCartellaEsercizi();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            new WindowSelezioneEsercizio().Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //DocumentoCliente dc = FactoryDocumentiClienti.Seleziona(1);
            //MemoryStream ms = new MemoryStream(dc.File);
            //File.WriteAllBytes("prova", dc.File);

        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            new WindowTest(FormAction.insert, 5).ShowDialog();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {

        }
    }
}
