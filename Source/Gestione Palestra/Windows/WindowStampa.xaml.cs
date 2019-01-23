using System; using GestionePalestra.MVC;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace GestionePalestra
{
    /// <summary>
    /// Stampa la scheda passata come parametro, permettendo di impostare i banner
    /// </summary>
    public partial class WindowStampa : Window
    {
        Scheda s;
        string banner_1_path;
        string banner_2_path;

        string pdf_path = "";

        /// <summary>
        /// permette di impostare i banner e stampare la scheda
        /// </summary>
        /// <param name="s">scheda da stampare</param>
        public WindowStampa(Scheda s)
        {
            InitializeComponent();
            this.s = s;
        }


        /// <summary>
        /// file dialog per selezionare il banner
        /// </summary>
        /// <returns>percorso del file selezionato</returns>
        string caricaImmagine()
        {
            string return_file = "";

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Image Files(*.jpg, *.png) | *.jpg; *.png ";
            if (of.ShowDialog() == true)
            {
                return_file = of.FileName;
            }
            return return_file;
        }


        private void btn_carica_banner_1_Click(object sender, RoutedEventArgs e)
        {
            banner_1_path = caricaImmagine();

            if (File.Exists(banner_1_path))
            {
                Image i = new Image();
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = new Uri(banner_1_path, UriKind.Absolute);
                b.EndInit();
                i.Source = b;
                i.Margin = new Thickness(5);
                grid_banner_1.Children.Add(i);
            }
                
        }
        private void btn_carica_banner_2_Click(object sender, RoutedEventArgs e)
        {
            banner_2_path = caricaImmagine();

            if(File.Exists(banner_2_path))
            {
                Image i = new Image();
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = new Uri(banner_2_path, UriKind.Absolute);
                b.EndInit();
                i.Source = b;
                i.Margin = new Thickness(5);
                grid_banner_2.Children.Add(i);
            }      
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(s.FKCliente.HasValue == false)
            {
                MessageBox.Show("la scheda non è stata assegnata, impossibile stampare", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }
        }

        private void btn_apri_dir_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists(pdf_path))
                Process.Start("EXPLORER.EXE", "/select, \"" + pdf_path + "\"");
        }

        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            pdf_path = Print.PrintHTMLfromString(
                            Print.PrintHTML(s, banner_1_path, banner_2_path),
                            (bool)ckb_apri_file.IsChecked
                        );
        }
    }
}
