using System.Windows;
using System;
using GestionePalestra.MVC;
using GestionePalestra.Pages;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Data;

using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionePalestra
{
    /// <summary>
    /// finestra home screen per l'accesso alle funzione del pogramma
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * schermata iniziale del programma:
         * in window loaded vengono inizializzate le parti significative del programma:
         * apertura connessione db
         * verifica online licenza
         * caricamento impostazioni da file
         * avvio window login
         * caricamento notifiche
         */

        DataTable dt_notifiche;

        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// inizializzazione programma
        /// </summary>
        void Init()
        {
            //tenta la connessione al database e prosegue se va bene
            if (Database.Open() == true)
            {
                //caricamento configurazione
                Settings.Load();

                //login
                Login();

                //verifica notifiche
                GetNotifiche();
            }
            else
            {
                Button btn_conn = new Button() { Content = "Imposta la connessione", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Width=150, Height=30 };
                btn_conn.Click += new RoutedEventHandler(btn_connessione_Click);
                frame_main.Content = btn_conn;
            }
        }


        /// <summary>
        /// caricamento finestra
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// avento prima della chiusura effettiva
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool chiedi_conferma = false;
            if (App.Current.Windows.Count > 2)
            {
                chiedi_conferma = true;
            }

            if (chiedi_conferma == true)
            {
                MessageBoxResult res = MessageBox.Show("Ci sono finestre aperte, sei sicuro di voler chiudere il programma?", "Avviso", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (res == MessageBoxResult.OK)
                {
                    //chiusura pulita sistema
                    Database.Close();
                    Environment.Exit(0);
                }
                else
                    e.Cancel = true;
            }
            else
            {
                //chiusura pulita sistema
                Database.Close();
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// evento pressione tasto [da rimuovere alla consegna]
        /// </summary>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F5)
                new WindowSysUtility().Show();
        }


        /// <summary>
        /// carica l'immagine a seconda dello stato, immagine dell'istruttore altrimenti un'icona di sistema
        /// </summary>
        void caricaImagineProfilo()
        {
            img_profilo.ImageSource = null;
            byte[] img = (Session.User != null) ? Session.User.Immagine : null;
            img_profilo.ImageSource = Common.BitmapFromByte(img, "BlueLogin.png");
        }


        void Login()
        {
            new WindowAccesso().ShowDialog();
            if(Session.User != null)
            {
                lbl_welcome.Content = Session.User.Nome;
                caricaImagineProfilo();
                img_setting.Visibility = Visibility.Visible;

                frame_main.Content = new PageHome();
            }
        }

        void Logout()
        {
            Session.Logout();
            lbl_welcome.Content = "Accesso non effettuato";
            caricaImagineProfilo();
            img_setting.Visibility = Visibility.Collapsed;

            frame_main.Content = null;
        }


        /// <summary>
        /// ottieni la lista delle notifiche
        /// </summary>
        void GetNotifiche()
        {
            if(Session.User == null)
                return;

            dt_notifiche = new DataTable();
            dt_notifiche.Merge(AvvisoController.GetNotificheAvvisiPersonali(Session.User.PKIstruttore, DateTime.Now, DateTime.Now.AddDays(1)));
            dt_notifiche.Merge(AvvisoController.GetNotificheComunicazioni(Session.User.PKIstruttore, DateTime.Now, DateTime.Now.AddDays(1)));
            

            lbl_notifiche.Content = dt_notifiche.Rows.Count;
        }


        #region BARRA ALTA

        private void img_setting_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frame_main.Content = new PageImpostazioni();
        }
        private void lbl_notifiche_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dt_notifiche.Rows.Count > 0)
                new WindowNotifiche(dt_notifiche).Show();

        }
        private void Ellipse_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Session.User == null)
                Login();
            else
                Logout();
        }

        #endregion


        /// <summary>
        /// evento di click del bottone per impostare la connessione al db
        /// apre la finestra di connessione manuale
        /// e tenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_connessione_Click(object sender, RoutedEventArgs e)
        {
            new WindowConnessione().ShowDialog();
            Init();
        }
    }
}