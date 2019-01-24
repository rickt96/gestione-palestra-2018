
using Microsoft.Win32;
using System;
using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;


namespace GestionePalestra.Pages
{
    /// <summary>
    /// Pagina delle impostazioni
    /// </summary>
    public partial class PageImpostazioni : Page
    {
        string _newLogo = "";


        public PageImpostazioni()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Verifica caricamento impostazioni
            if (Settings.is_settings_loaded != true)
            {
                Message.Alert( AlertType.warning, "Impossibile caricare le impostazioni", "Impostazioni");
                Paging.BackHome(this);
            }

            //generali
            txt_nome_attivita.Text = Settings.ACTIVITY_NAME;
            dp_data_avvio.SelectedDate = Settings.STARTUP_DATE;
            if (File.Exists(Paths.Logo))
                Common.SetGridImage(ref grid_img_logo, Common.ByteFromFile(Paths.Logo));


            //backup
            lbx_tipo_backup.SelectedIndex = Settings.BACKUP_TYPE;
            txt_ultimo_backup.Text = Settings.GetLastBackupDate();


            //ripristino
            RefreshFileBackup();
            txt_ultimo_ripristino.Text = Settings.LAST_RESTORE_DATE.ToString("dd/MM/yyyy hh:mm:ss");


            //utenti
            refreshIstruttori();


            //connessione
            RefreshParamConnessione();


            //percorsi
            //da fare
            //accedere app config - https://stackoverflow.com/questions/806174/how-to-use-a-app-config-file-in-wpf-applications
            //salvare app config - https://stackoverflow.com/questions/4758598/write-values-in-app-config-file
            //MessageBox.Show(ConfigurationManager.AppSettings["DB_HOST"].ToString());
            
        }


        /// <summary>
        /// ottiene la versione corrente del programma
        /// </summary>
        void MostraVersione()
        {
            try
            {
                string location = System.AppDomain.CurrentDomain.BaseDirectory + "Gestione Palestra.exe";
                var versionInfo = FileVersionInfo.GetVersionInfo(location);
                //lbl_versione.Content = "versione " + versionInfo.ProductVersion;
            }
            catch { }
        }

        /// <summary>
        /// aggiorna la lista dei backup
        /// </summary>
        void RefreshFileBackup()
        {
            lbx_backup.ItemsSource = null;
            List<FileInfo> bk = new List<FileInfo>();
            string[] files = Directory.GetFiles(Paths.Backup, "*.sql");
            foreach (string f in files)
                bk.Add(new FileInfo(f));
            lbx_backup.DisplayMemberPath = "CreationTime";
            lbx_backup.ItemsSource = bk;
            txt_ultimo_ripristino.Text = Settings.GetLastBackupDate();
        }

        /// <summary>
        /// aggiorna lista utenti/istruttori
        /// </summary>
        void refreshIstruttori()
        {
            //TAB UTENTI
            dg_utenti.ItemsSource = null;
            dg_utenti.ItemsSource = IstruttoriController.GetTableIstruttori(null).DefaultView;
        }

        /// <summary>
        /// aggiorna i valori della connessione al db
        /// </summary>
        void RefreshParamConnessione()
        {
            lbl_connessione.Content =
                "Server:\t\t\t" + Database.DB_HOST + Environment.NewLine +
                "User:\t\t\t" + Database.DB_USER + Environment.NewLine +
                "Password:\t\t" + String.Concat(Enumerable.Repeat("*", Database.DB_PASSWORD.Length)) + Environment.NewLine +
                "Porta servizio:\t\t" + Database.DB_PORT + Environment.NewLine +
                "Nome database:\t\t" + Database.DB_NAME + Environment.NewLine;
        }


        #region GENERALI

        private void btn_carica_logo_Click(object sender, RoutedEventArgs e)
        {
            string file = Common.OpenFileDialogImage();
            if (File.Exists(file))
            {
                _newLogo = file;
                Common.SetGridImage(ref grid_img_logo, Common.ByteFromFile(file));
            }
        }

        #endregion


        #region BACKUP

        private void btn_crea_backup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("da fare");
            FactoryBackup.Create(Paths.Backup ,true);
            txt_ultimo_backup.Text = Settings.GetLastBackupDate().ToString();
            RefreshFileBackup();
        }

        private void lbx_backup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_backup.SelectedIndex == -1)
                return;

            lbl_selected_file_backup.Content = ((FileInfo)lbx_backup.SelectedItem).Name;
        }

        #endregion


        #region RIPRISTINO

        private void btn_avvio_ripristino_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_backup.SelectedIndex == -1)
                return;

            FileInfo selectedFile = (FileInfo)lbx_backup.SelectedItem;
            FactoryBackup.Restore(selectedFile.FullName);
            txt_ultimo_ripristino.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void btn_elimina_ripristino_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_backup.SelectedIndex == -1)
                return;

            MessageBoxResult res = MessageBox.Show("Rimuovere il file di ripristino?", "Avviso", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    ((FileInfo)lbx_backup.SelectedItem).Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossibile eliminare il file:\n" + ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                RefreshFileBackup();
            }

        }

        private void btn_esporta_ripristino_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_backup.SelectedIndex == -1)
                return;

            FileInfo f = ((FileInfo)lbx_backup.SelectedItem);
            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = f.Name;
            sd.Filter = "File backup (*.sql)|*.sql";
            sd.CheckFileExists = true;
            if (sd.ShowDialog() == true)
            {
                ((FileInfo)lbx_backup.SelectedItem).CopyTo(sd.FileName, true);
                MessageBox.Show("File di ripristino salvato su:\n" + sd.FileName, "Avviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_dettagli_backup_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_backup.SelectedIndex == -1)
                return;

            FileInfo selectedFile = (FileInfo)lbx_backup.SelectedItem;
            string info = string.Format(
                "Dettagli file di backup:\n" +
                "Percorso: {0}\n" +
                "Dimensioni: {1}\n" +
                "Data creazione: {2}",
                selectedFile.FullName,
                selectedFile.Length + " byte",
                selectedFile.CreationTime.ToString()
                );
            MessageBox.Show(info);
        }

        #endregion


        #region UTENTI

        private void btn_inserisci_utente_Click(object sender, RoutedEventArgs e)
        {
            //WindowSysUser su = new WindowSysUser();
            //su.ShowDialog();
            //CaricaUtenti();
        }
        private void btn_modifica_utente_Click(object sender, RoutedEventArgs e)
        {
            if (dg_utenti.SelectedIndex == -1)
                return;

            //string username = ((SysUser)dg_utenti.SelectedItem).Username;
            //WindowSysUser su = new WindowSysUser(username);
            //su.ShowDialog();

            //CaricaUtenti();
        }
        private void btn_elimina_utente_Click(object sender, RoutedEventArgs e)
        {
            if (dg_utenti.SelectedIndex == -1)
                return;

            MessageBox.Show("da fare");
            //string username = ((SysUser)dg_utenti.SelectedItem).Username;
            //FactoryAccesso.Elimina(username);
            //CaricaUtenti();
        }

        #endregion


        #region CONNESSIONE

        private void btn_modifica_connessione_Click(object sender, RoutedEventArgs e)
        {
            new WindowConnessione().ShowDialog();
            RefreshParamConnessione();
        }

        #endregion


        #region ISTRUTTORI

        private void btn_inserisci_istruttore_Click(object sender, RoutedEventArgs e)
        {
            new WindowInserisciIstruttore().ShowDialog();
            refreshIstruttori();

        }

        private void btn_modifica_credenziali_istruttore_Click(object sender, RoutedEventArgs e)
        {
            if (dg_utenti.SelectedIndex == -1)
                return;

            DataRowView row = (DataRowView)dg_utenti.SelectedItem;
            int id = Convert.ToInt16(row[0]);
            if (Permessi.CheckModificaCredenziali(ref id) == true)
            {
                new WindowCredenziali(id).ShowDialog();
                refreshIstruttori();
            }
        }

        private void btn_elimina_istruttore_Click(object sender, RoutedEventArgs e)
        {
            if (dg_utenti.SelectedIndex == -1)
                return;

            DataRowView dr = (DataRowView)dg_utenti.SelectedItem;
            int id = Convert.ToInt16(dr[0]);

            if (Message.Confirm(DialogType.delete, "istruttori") == false)
                return;

            if (IstruttoriController.Elimina(id) > 0)
                refreshIstruttori();
        }

        private void btn_livelli_permessi_Click(object sender, RoutedEventArgs e)
        {
            new WindowGestionePermessi().ShowDialog();
            refreshIstruttori();
        }

        #endregion


        #region PERCORSI

        private void btn_sel_chrome_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            {
                txt_chrome_path.Text = of.FileName;
            }
        }

        #endregion


        private void btn_salva_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.is_settings_loaded != true)
                return;

            //generale
            Settings.ACTIVITY_NAME = txt_nome_attivita.Text;
            Settings.STARTUP_DATE = Convert.ToDateTime(dp_data_avvio.SelectedDate);
            if (File.Exists(_newLogo))
                File.Copy(_newLogo, Paths.Logo, true);

            //backup - ripristino
            Settings.BACKUP_TYPE = lbx_tipo_backup.SelectedIndex;
            Settings.LAST_RESTORE_DATE = Convert.ToDateTime(txt_ultimo_ripristino.Text);

            //percorsi file
            Settings.CHROME_PATH = txt_chrome_path.Text;

            //logger
            Settings.ERROR_LOGGER_ENABLED = false;// (bool)ckb_log_errori.IsChecked;
            Settings.CHANGES_LOGGER_ENABLED = false;//(bool)ckb_log_modifiche.IsChecked;

            //scrive le modifiche
            Settings.Save();
        }

        private void btn_indietro_Click(object sender, RoutedEventArgs e)
        {
            Paging.BackHome(this);
        }
    }
}
