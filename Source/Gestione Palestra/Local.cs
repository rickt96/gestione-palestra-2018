using System;
using GestionePalestra.MVC;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;
using Microsoft.VisualBasic;
using GestionePalestra.Pages;
using System.Configuration;

namespace GestionePalestra
{
    /// <summary>
    /// definisce l'azione di un form in una finestra
    /// </summary>
    public enum FormAction { insert, update };

    /// <summary>
    /// definisce il tipo di finestra di dialogo
    /// </summary>
    public enum DialogType { insert, update, delete };

    /// <summary>
    /// definisce il tipo di alert
    /// </summary>
    public enum AlertType { info, warning, error };

    public enum Gender { female=0, male=1}; //TODO





    /// <summary>
    /// i componenti comuni dell'applicazione
    /// </summary>
    public static class Common
    {
        //valori dei campi form
        public static string[] Sesso = new string[] { "donna", "uomo" };
        public static string[] PrioritaAvviso = new string[] { "normale", "urgente" };
        public static List<Tipo> MetodiEsercizi = new List<Tipo>() {
            new Tipo(){ Valore="Serie", Descrizione="Schema classico di esecuzione con esercizi sequenziali, intervallati da un recupero tra ogni serie."},
            new Tipo(){ Valore="Superserie", Descrizione="La metodologia a superserie pervede l'accostamento di 2 o più esercizi da eseguire in sucessione con un recupero al termine."},
            new Tipo(){ Valore="Circuito", Descrizione="Il circuito è composto da numerosi esercizi da svolgere in sequenza con una pausa alla fine."},
        };
        public static List<Tipo> Somatotipi = new List<Tipo>() {
            new Tipo(){ PKTipo=0, Valore="Ectomorfo", Descrizione="descrizione ectomorfo"},
            new Tipo(){ PKTipo=1, Valore="Mesomorfo", Descrizione="descrizione mesomorfo"},
            new Tipo(){ PKTipo=2, Valore="Endomorfo", Descrizione="descrizione endomorfo"}
        };
        public static List<Tipo> Lifestyle = new List<Tipo>() {
            new Tipo(){ PKTipo=1, Valore="Sedentario", Descrizione=""},
            new Tipo(){ PKTipo=2, Valore="Moderato", Descrizione=""},
            new Tipo(){ PKTipo=3, Valore="Attivo", Descrizione="<descrizione>"}
        };
        public static List<Tipo> Obiettivi = new List<Tipo>() {
            new Tipo(){ PKTipo=1, Valore="Mantenimento"},
            new Tipo(){ PKTipo=2, Valore="Massa"},
            new Tipo(){ PKTipo=3, Valore="Definizione"},
            new Tipo(){ PKTipo=3, Valore="Dimagrimento"},
            new Tipo(){ PKTipo=3, Valore="Riabilitazione"},
            new Tipo(){ PKTipo=3, Valore="Forza"}
        };


        //timestamp aggiornamenti
        public static DateTime LastUpdateClienti;



        /// <summary>
        /// popola gli elementi di una combobox passata come riferimento
        /// </summary>
        /// <param name="c">ComboBox passata come riferimento</param>
        /// <param name="ObjectSRC">lista di oggetti per popolare il controllo</param>
        /// <param name="DisplayMemberPath">stringa della proprietà da mostrare nella comobobox</param>
        public static void PopulateComboBox(ref ComboBox c, IEnumerable<object> ObjectSRC, string DisplayMemberPath)
        {
            c.ItemsSource = null;
            c.ItemsSource = ObjectSRC;
            c.DisplayMemberPath = DisplayMemberPath;
        }


        /// <summary>
        /// apre un file dialog per selezionare un'immagine
        /// </summary>
        /// <returns>ritorna il percorso del file, altrimenti ""</returns>
        public static string OpenFileDialogImage()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Files immagini (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (of.ShowDialog() == true)
                return of.FileName;
            else
                return "";
        }
        

        /// <summary>
        /// converte in byte[] l'immagine specificata nel parametro
        /// </summary>
        /// <param name="img_path">percorso immagine da convertire in byte[]</param>
        public static byte[] ByteFromFile(string img_path)
        {
            string full_path = Path.GetFullPath(img_path);
            if (File.Exists(full_path))
            {
                FileStream fs;
                BinaryReader br;
                byte[] res;
                fs = new FileStream(full_path, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                res = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                return res;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// converte in immagine Bitmap l'array di byte preso come parametro,
        /// in alternativa restituisce l'immagine spcificata
        /// </summary>
        /// <param name="img">array di byte dell'immagine</param>
        /// <param name="alt_img">immagine alternativa da caricare dalla cartella /Gestione Palestra;component/Resources/Icons in caso di errore</param>
        /// <returns></returns>
        public static BitmapImage BitmapFromByte(byte[] img, string alt_img)
        {
            BitmapImage b = new BitmapImage();
            if (img != null) //carica l'array
            {
                MemoryStream ms = new MemoryStream(img);
                ms.Position = 0;
                b.BeginInit();
                b.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = null;
                b.StreamSource = ms;
                b.EndInit();
            }
            else //carica l'immagine alternativa
            {
                b = new BitmapImage();
                try
                {
                    b.BeginInit();
                    b.CacheOption = BitmapCacheOption.OnLoad;
                    b.UriSource = new Uri(@"pack://application:,,,/Gestione Palestra;component/Resources/Icons/" + alt_img, UriKind.Absolute);
                    b.EndInit();
                }
                catch { }
            }
            return b;

        }


        /// <summary>
        /// converte in immagine bitmap l'immagine specificata dalla cartella /Gestione Palestra;component/Resources/Icons
        /// </summary>
        /// <param name="icon_name">nome dell'icona</param>
        public static BitmapImage BitmapFromIcon(string icon_name)
        {
            BitmapImage b = new BitmapImage();
            try
            {
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = new Uri(@"pack://application:,,,/Gestione Palestra;component//Gestione Palestra;component/Resources/Icons/" + icon_name, UriKind.Absolute);
                b.EndInit();
            }
            catch { }
            return b;
        }


        /// <summary>
        /// imposta il contenuto della grid mostrando o l'immagine o la label
        /// </summary>
        /// <param name="grid">riferimento alla grid</param>
        /// <param name="img">array di byte dell'immagine</param>
        /// <param name="show_wide_img">se true permette di mostrare l'immagine ingrandita</param>
        public static void SetGridImage(ref Grid grid, byte[] img)
        {
            grid.Children.Clear();

            if (img != null)
            {
                //caricamento immagine
                MemoryStream ms = new MemoryStream(img);
                var image = new BitmapImage();
                ms.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = ms;
                image.EndInit();
                Image UIimg = new Image();
                UIimg.Source = image;
                UIimg.Margin = new Thickness(5);
                grid.Children.Add(UIimg);
            }
            else
            {
                //label avviso nessuna immagine
                grid.Children.Add(new Label()
                {
                    Content = "Nessuna immagine",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                });
            }
        }


        /// <summary>
        /// crea un'immagine in locale dall'array di byte specificato
        /// </summary>
        /// <param name="img">array di byte dell'immagine</param>
        /// <param name="dest_file">percorso di destinazione dell'immagine</param>
        /// <returns></returns>
        public static bool SaveImageFromByte(byte[] img, string dest_file)
        {
            if (img == null)
                return false;

            try {
                MemoryStream ms = new MemoryStream(img);
                System.Drawing.Image i = System.Drawing.Image.FromStream(ms);
                i.Save(dest_file, System.Drawing.Imaging.ImageFormat.Png);
                return true;
            }
            catch {
                return false;
            }
        }

        public static string SaveFileFromByte(byte[] arr, string file_name)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = file_name;

            if (sd.ShowDialog() == true)
            {
                File.WriteAllBytes(sd.FileName, arr);
                return sd.FileName;
            }
            else
                return "";
        }

    }



    public static class ImageManager
    {
        //TODO
    }



    /// <summary>
    /// sessione utente (istruttore loggato)
    /// </summary>
    public static class Session
    {
        public static Istruttore User = null;
        public static LivelloPermesso PermessAttivi = null;

        /// <summary>
        /// verifica le credenziali e carica se corrette l'istruttore corrente loggato
        /// </summary>
        /// <param name="id_istr">id istruttore da far accedere</param>
        /// <param name="pwd">password da verificare</param>
        /// <returns>true se loggato correttamente, altrimenti false</returns>
        public static bool Login(int id_istr, string pwd)
        {
            bool status = false;

            if (IstruttoriController.CheckCredenziali(id_istr, pwd))
            {
                User = IstruttoriController.Seleziona(id_istr);
                PermessAttivi = (User.FKLivelliPermessi.HasValue)
                    ? LivelliPermessiController.Seleziona(User.FKLivelliPermessi.Value)
                    : null;

                if (User != null && PermessAttivi != null)
                {
                    status = true;
                }
                else
                {
                    Message.Alert(AlertType.error, "Impossibile reperire le informazioni dell'istruttore ed i suoi permessi.\nPer definire i permessi modificare il profilo istruttore dalle impostazioni.", "istruttore");
                    status = false;
                }

            }
            else
            {
                Message.Alert(AlertType.warning, "Credenziali di accesso invalide", "istruttore");
                status = false;
            }
            return status;
        }


        /// <summary>
        /// disconnette l'istruttore attivo
        /// </summary>
        public static void Logout()
        {
            Session.User = null;
        }
    }



    /// <summary>
    /// gestione dei messaggi di dialogo, alert e inputbox
    /// </summary>
    public static class Message
    {
        /// <summary>
        /// mostra una inputbox del VB
        /// </summary>
        /// <param name="message">messaggio da mostrare</param>
        /// <param name="caption">titolo</param>
        /// <param name="default_response">valore preimpostato ("" per ignorare)</param>
        /// <returns></returns>
        public static string InputBox(string message, string caption, string default_response)
        {
            return Interaction.InputBox(message, caption, default_response);
        }


        /// <summary>
        /// mostra una finestra di conferma per procedere alle azioni crud
        /// </summary>
        /// <param name="td">insert, update o delete</param>
        /// <returns>true se si preme OK, altrimenti false</returns>
        public static bool Confirm(DialogType td, string caption)
        {
            string message = "";
            switch (td)
            {
                case DialogType.insert:
                    message = "Inserire l'elemento corrente?";
                    break;
                case DialogType.update:
                    message = "Salvare le modifiche?";
                    break;
                case DialogType.delete:
                    message = "Eliminare l'elemento corrente?";
                    break;
            }

            MessageBoxResult res = MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.OK)
                return true;
            else
                return false;
        }


        /// <summary>
        /// mostra una finestra di dialogo OK CANCEL specificando un messaggio
        /// </summary>
        /// <param name="message">testo della domanda</param>
        /// <param name="caption">titolo</param>
        /// <returns>true se si preme OK, altrimenti false</returns>
        public static bool Question(string message, string caption)
        {
            MessageBoxResult res = MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.OK)
                return true;
            else
                return false;
        }


        /// <summary>
        /// mostra una messagebox di conferma contestualizzata all'operazione CRUD eseguita
        /// </summary>
        /// <param name="td">tipo di messaggio da mostrare (insert update o delete)</param>
        public static void Alert(DialogType td, string caption)
        {
            string message = "";
            switch (td)
            {
                case DialogType.insert:
                    message = "Nuovo elemento inserito";
                    break;
                case DialogType.update:
                    message = "Modifiche inserite";
                    break;
                case DialogType.delete:
                    message = "Elemento eliminato";
                    break;
            }
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// mastra una messagebox di conferma specificando un'image ed un testo
        /// </summary>
        /// <param name="ta">immagine da mostrare</param>
        /// <param name="message">testo della messagebox</param>
        /// <param name="caption">titolo</param>
        public static void Alert(AlertType ta, string message, string caption)
        {
            MessageBoxImage image = MessageBoxImage.None;
            switch (ta)
            {
                case AlertType.error: image = MessageBoxImage.Error; break;
                case AlertType.info: image = MessageBoxImage.Information; break;
                case AlertType.warning: image = MessageBoxImage.Warning; break;
            }
            MessageBox.Show(message, caption, MessageBoxButton.OK, image);
        }

    }



    /// <summary>
    /// Gestione navigazione tra le pages
    /// </summary>
    public static class Paging
    {
        public static void BackHome(Page pg)
        {
            pg.NavigationService.Navigate(new PageHome());
        }
    }


    public static class Values
    {
        //da mettere i valori
        //aggiungere liste private, poi metodo get per recuperarla, specificando in un parametro se si deve aggiungere il campo "nessun elemento"
        //la classe potrebbe essere un buon appoggio nel caso certi campi saranno messi sul db
        //sarà sufficente cambiare il source delle combobox e riattivare le fk nel database

        public static int SelectTipo(ref ComboBox cmb_src, int search_value)
        {
           foreach(Tipo t in cmb_src.Items)
                if(t.PKTipo == search_value)
                    return t.PKTipo;
            return -1;

        }

        public static int SearchCombo(ref ComboBox cmb_src, string object_name, string propriety_name, int value)
        {
            var source = cmb_src.Items;


            return 0;
        }
    }



    /// <summary>
    /// percorsi ai file ed alle risorse
    /// </summary>
    public static class Paths
    {
        public static string AppName = "Gestione palestra";
        public static string AppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string Backup = "Backup\\";
        public static string Images = "Images\\";
        public static string Documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + AppName + @"\";
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + AppName + @"\";
        public static string TmpDocumentPath = AppData + @"\" + "tmp_doc";
        public static string TmpEserciziPath = AppData + @"\" + "tmp_es";
        public static string Logo = AppPath + "logo";


        /// <summary>
        /// verifica l'esistenza delle directory e le crea nel caso non ci siano
        /// </summary>
        public static void CheckDirectory(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }



    /// <summary>
    /// classe per la gestione delle voci delle impostazioni in app.config
    /// </summary>
    public static class Settings
    {
        public static bool is_settings_loaded = false;

        public static string ACTIVITY_NAME;
        public static DateTime STARTUP_DATE;
        public static bool CHANGES_LOGGER_ENABLED;
        public static bool ERROR_LOGGER_ENABLED;
        public static int BACKUP_TYPE;
        public static DateTime LAST_RESTORE_DATE; 
        public static DateTime LAST_BACKUP_DATE; //[TEMP] da implementare e studiare bene, basterebbe leggere l'ultima data di creazione sulla directory
        public static string CHROME_PATH;


        public static string GetLastBackupDate()
        {
            if (Directory.GetFiles(Paths.Backup).Count() == 0)
            {
                return DateTime.MinValue.ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(Paths.Backup);
                FileInfo recente = (from f in directory.GetFiles()
                                    orderby f.LastWriteTime
                                    descending
                                    select f).First();
                return recente.CreationTime.ToString("yyyy/MM/dd HH:mm:ss");
            }


        }


        /// <summary>
        /// carica le impostazioni da app.config
        /// </summary>
        public static bool Load()
        {
            try
            {
                ACTIVITY_NAME = ConfigurationManager.AppSettings["ACTIVITY_NAME"].ToString();
                STARTUP_DATE = Convert.ToDateTime(ConfigurationManager.AppSettings["STARTUP_DATE"]);
                ERROR_LOGGER_ENABLED = Convert.ToBoolean(ConfigurationManager.AppSettings["ERROR_LOGGER_ENABLED"]);
                CHANGES_LOGGER_ENABLED = Convert.ToBoolean(ConfigurationManager.AppSettings["CHANGES_LOGGER_ENABLED"]);
                BACKUP_TYPE = Convert.ToInt16(ConfigurationManager.AppSettings["BACKUP_TYPE"]);
                LAST_RESTORE_DATE = Convert.ToDateTime(ConfigurationManager.AppSettings["LAST_RESTORE_DATE"]);
                CHROME_PATH = ConfigurationManager.AppSettings["CHROME_PATH"].ToString();
                is_settings_loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nel caricamento delle impostazioni:\n" + ex.ToString(), "Impostazioni", MessageBoxButton.OK, MessageBoxImage.Error);
                is_settings_loaded = false;
            }

            return is_settings_loaded;
        }


        /// <summary>
        /// salva le impostazioni in app.config
        /// </summary>
        public static bool Save()
        {
            try
            {
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfg.AppSettings.Settings["ACTIVITY_NAME"].Value = ACTIVITY_NAME;
                cfg.AppSettings.Settings["STARTUP_DATE"].Value = STARTUP_DATE.ToString();
                cfg.AppSettings.Settings["CHANGES_LOGGER_ENABLED"].Value = CHANGES_LOGGER_ENABLED.ToString();
                cfg.AppSettings.Settings["ERROR_LOGGER_ENABLED"].Value = ERROR_LOGGER_ENABLED.ToString();
                cfg.AppSettings.Settings["BACKUP_TYPE"].Value = BACKUP_TYPE.ToString();
                cfg.AppSettings.Settings["LAST_RESTORE_DATE"].Value = LAST_RESTORE_DATE.ToString();
                cfg.AppSettings.Settings["CHROME_PATH"].Value = CHROME_PATH.ToString();

                cfg.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile salvare le impostazioni:\n" + ex.Message);
                return false;
            }
        }
    }



    /// <summary>
    /// [DA FINIRE] classe per le stampe e le esportazioni in HTML
    /// </summary>
    public static class Print
    {
        /// <summary>
        /// 
        /// </summary>
        public static void StampaGriglia(ref DataGrid dg)
        {
            //https://stackoverflow.com/questions/5549321/how-to-read-value-from-a-cell-from-a-wpf-datagrid
            //https://stackoverflow.com/questions/10996664/iterate-datagridview-columns-and-change-column-header
            //https://stackoverflow.com/questions/14977697/how-to-read-data-from-one-column-of-datagridview/14977922
            //https://stackoverflow.com/questions/23345094/how-to-loop-through-a-datagrid-and-get-the-column-values-with-column-header

            int columns = dg.Columns.Count;

            //scorre righe
            for (int r = 0; r < dg.Items.Count; r++)
            {
                DataRowView row = (DataRowView)dg.Items[r];

                //scorri colonne
                for (int c = 0; c < columns; c++)
                {
                    //row[c] da inserire qui il valore
                }
            }
        }


        /// <summary>
        /// [DA FINIRE] crea la stringa html della scheda
        /// </summary>
        /// <param name="s">oggetto scheda da stampare</param>
        /// <param name="first_banner_path">percorso all'immagine del primo banner</param>
        /// <param name="close_banner_path">percorso all'immagine del secondo banner</param>
        /// <returns>restituisce la stringa dell'html generato, "" in caso di errore</returns>
        public static string PrintHTML(Scheda s, string first_banner_path, string close_banner_path)
        {
            try
            {
                //caricamento dei dati: cliente e scheda
                Cliente c = ClienteController.Seleziona(s.FKCliente.Value);
                Istruttore i = IstruttoriController.Seleziona(s.FKIstruttore.Value);

                //stringa di output
                StringBuilder html = new StringBuilder();

                //path comuni
                string logo_path = @"File:///" + Path.GetFullPath(Paths.AppPath + "logo.png");
                string banner_1_uri = (File.Exists(first_banner_path)) ? @"File:///" + Path.GetFullPath(first_banner_path).Replace(@"\", "/") : "";      //eseguo il replace per adeguare il percorso e farlo trovare al css
                string banner_2_uri = (File.Exists(close_banner_path)) ? @"File:///" + Path.GetFullPath(close_banner_path).Replace(@"\", "/") : "";


                //[TEMP] il css viene caricato dal file esterno sulle impostazioni, per rendere piu facile la modifica
                //appena il file è valido ed il css viene validato aggiungerlo qui dentro

                #region HEAD - STYLE

                html.Append(@"
                <!DOCTYPE html>
                <html>
                <head>
                <title>Stampa</title>
                <script>
                window.print();
                </script> 
                </head>
                ");

                html.Append(File.ReadAllText(@"pack://application:,,,/Gestione Palestra;component/Resources/PrintStyle.css"));

                #endregion


                html.Append("<body>");


                #region PRIMA PAGINA

                //div prima pagina
                html.Append(@"
                <div class='page'>
                <div class='subpage'>");


                //header
                html.AppendFormat(@"
                <div class='header'>
	                <h2>Buon allenamento!</h2>
	                <img src='{0}'>   
                </div>",
                logo_path);


                //informazioni scheda
                html.AppendFormat(@"
                <div class='infoScheda'>
                <table>
	                <tr>
		                <td colspan='2'>Nome e Cognome: {0}</td>
	                </tr>
	                <tr>
		                <td colspan='2'>Obiettivo del programma: {1}</td>
	                </tr>
	                <tr>
		                <td>Data inzio: {2}</td>
		                <td>Data fine: {3}</td>
	                </tr>
	                <tr>
		                <td>Frequenza settimanale: {4}</td>
		                <td>Numero sedute: {5}</td>
	                </tr>
	                <tr>
		                <td colspan='2'>Obiettivo da raggiungere: {6}</td>
	                </tr>
	                <tr>
		                <td>Istruttore: {7}</td>
		                <td>Data compilazione: {8}</td>
	                </tr>
                </table>
                </div>",
                c.NomeCompleto,
                s.Obiettivo,
                s.DataInizio.Value.ToShortDateString(),
                s.DataFine.Value.ToShortDateString(),
                s.FrequenzaSettimanale,
                s.NumeroSedute,
                "---", //cazzo è
                i.NomeCompleto,
                DateTime.Now.ToShortDateString() //dato del cazzo, stampo la data di creazione
                );


                //primo banner: se è stato impostato lo stampa, altrimenti ignora
                if (File.Exists(first_banner_path))
                    html.Append(@"<div class='bannerLandscape'></div>");


                //scadenza allenamento
                html.Append(@"
                <div class='scadenzaScheda'>
	                <table>
                        <tr>
                            <td class='text'>questo programma deve essere completato entro il: </td>
                            <td class='box'>&nbsp;</td>
                            <td class='box'>&nbsp;</td>
                            <td class='box'>&nbsp;</td>
                        </tr>
                    </table>
                </div>");


                //sedute effettuate
                string box_sedute = "";
                for (int x = 0; x < s.NumeroSedute; x++)
                    box_sedute += "<span>" + (x + 1) + "</span>";

                html.AppendFormat(@"
                <div class='sedute'>
                <p class='title'>Lascia il segno ad ogni allenamento!</p>
                <p class='text'>Segna una casella od ogni seduta di allenamento che svolgi e alla fine del percorso goditi i risultati!</p>
                {0}
                </div>",
                box_sedute);


                //chiusura pagina
                html.Append(@"
                </div>
                </div>");

                #endregion


                #region SEDUTE

                int cs = 0;
                foreach (Seduta sed in s.Sedute)
                {
                    html.Append(@"
	                <div class='page'>
	                <div class='subpage'>");


                    //header
                    string nome_seduta = (sed.Nome != "")
                        ? sed.Nome
                        : "Seduta " + cs++;

                    html.Append(@"
	                <div class='header'>
		                <h2>" + nome_seduta + @"</h2>
		                <img src='" + logo_path + @"'>
	                </div>");


                    //tabella esercizi

                    //thead
                    html.Append(@"
	                <div class='tabellaEsercizi'>
	                <table>
		                <tr>
			                <th></th>
			                <th class='head'>gruppo</th>
			                <th class='head'>esercizio</th>
			                <th class='head'>reps</th>
			                <th class='head'>carico</th>
			                <th class='head'>rec</th>
			                <th class='head'>note</th>
		                </tr>");

                    //generazione tr esercizi: [QUI SI DOVRA INSERIRE TUTTA LA GESTIONE PER I ROW SPAN, GRUPPI E COLORI; LAMADONNA]
                    foreach (EsercizioSeduta es in sed.Esercizi)
                    {
                        //qui ci vorra uno switch per gestire le metodologie: standard, superserie, circuito
                        html.Append(@"
		                <tr>
			                <th class='index'>" + (es.Ordine + 1) + @"</th>
			                <td class='content'>" + es.Esercizio.Categoria.Nome + @"</td>
			                <td class='content'>" + es.Esercizio.Nome + @"</td>
			                <td class='content'>" + es.Serie + @"</td>
			                <td class='content'>" + es.Carico + "kg" + @"</td>
			                <td class='content'>" + es.Recupero.Value.Minutes + "min" + @"</td>
			                <td class='content'>" + "nessuna" + @"</td>
		                </tr>");
                    }

                    //chiusura tabella
                    html.Append(@"
	                </table>
	                </div>");


                    //elenco esercizi cards
                    html.Append(@"<div class='contenitoreImmagini'>");

                    foreach (EsercizioSeduta es in sed.Esercizi)
                    {
                        //creazione immagine locale dal db
                        string file_dest = Paths.Images + "Esercizi\\" + es.Esercizio.PKEsercizio;
                        bool res = Common.SaveImageFromByte(es.Esercizio.Immagine, file_dest);
                        if (res == true)
                        {
                            html.Append(@"
			                <div class='card'>
				                <div class='label'>" + (es.Ordine + 1) + @"</div>
				                <img src='" + @"File:\\\" + Path.GetFullPath(file_dest) + @"'>
			                </div>");
                        }

                    }

                    html.Append("</div>");


                    //chiusura page container
                    html.Append(@"
	                </div>
	                </div>");
                }

                #endregion


                #region BANNER FINALE

                //banner esteso
                if (File.Exists(close_banner_path))
                {
                    //break nuova pagina
                    html.Append("<div class='pagebreak'></div>");

                    //banner esteso
                    html.Append("<div class='pageContainer bannerPotrait'></div>");
                }

                #endregion


                html.Append(@"
                </body>
                </html>");


                //restituisce l'html generato
                return html.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
        }


        /// <summary>
        /// [NON IMPLEMENTATO] stampa il documento pdf utilizzando la modalita headless di Google Chrome
        /// </summary>
        /// <param name="html_file_path">percorso del file html da convertire</param>
        /// <param name="pdf_file_path">percorso del pdf generato</param>
        public static bool PrintPDFChromeHeadless(string html_file_path, string pdf_file_path)
        {
            if (File.Exists(html_file_path))
            {
                //percorsi assoluti files
                string pdf = Path.GetFullPath(pdf_file_path);
                string html = Path.GetFullPath(html_file_path);

                //correzione formato: se non è in estensione pdf la aggiunge
                pdf += (!pdf.EndsWith(".pdf")) ? ".pdf" : "";

                //creazione comando console
                //ToChar genera un " per poter gestire i percorsi con gli spazi vuoti
                //> "percorso_di_chrome" --headless --disable-gpu --print-to-pdf="output.pdf" "html_sorgente.html"
                string cmd = Convert.ToChar(34) + Settings.CHROME_PATH + Convert.ToChar(34) +
                @" --headless --disable-gpu --print-to-pdf=" + Convert.ToChar(34) + pdf + Convert.ToChar(34) + " " +
                Convert.ToChar(34) + @"File:\\\" + html + Convert.ToChar(34);

                //creazione ed esecuzione processo
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = cmd;
                process.StartInfo = startInfo;
                process.Start();

                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// crea il file HTML
        /// </summary>
        /// <param name="html">stringa contenente l'html da generare</param>
        /// <param name="open_file">true: avvia il file su google chrome</param>
        /// <returns>percorso del file creato, altrimenti ""</returns>
        public static string PrintHTMLfromString(string html, bool open_file)
        {
            if (html == "")
                return "";

            try
            {
                Paths.CheckDirectory(Paths.Documents);
                string OutputPath = Paths.Documents + string.Format("scheda {0}.html", DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
                File.AppendAllText(OutputPath, html);


                //apertura del file
                if (open_file)
                {
                    if (File.Exists(Settings.CHROME_PATH))
                        Process.Start(Settings.CHROME_PATH, "\"" + Path.GetFullPath(OutputPath) + "\"");
                    else
                        MessageBox.Show("Impossibile trovare il percorso di installazione di Google Chrome", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Creazione documento completata", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                return OutputPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }
        }


        /// <summary>
        /// [VECCHIO] genera il documento pdf dalla stringa HTML (ironPdf)
        /// </summary>
        /// <returns>percorso del docuemnto pdf creato</returns>
        public static string PrintPDFfromHTML_old(string html, bool open_file)
        {
            if (html == "")
                return "";

            try
            {
                //renderizza pdf dalla stringa html generata
                //var Renderer = new IronPdf.HtmlToPdf();
                //var PDF = Renderer.RenderHtmlAsPdf(html);

                ////Renderer.PrintOptions.MarginBottom = 20;
                //Renderer.PrintOptions.PrintHtmlBackgrounds = true;
                //Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
                //Renderer.PrintOptions.MarginLeft = 10;    //sembra che ignori i margin dalle opzioni, quindi si lavora nei media del css
                //Renderer.PrintOptions.MarginRight = 10;


                //destinazione
                //string OutputPath = Common.DocumentDirPath + string.Format("Schede\\scheda {0}.pdf",DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
                ////PDF.SaveAs(OutputPath);


                ////richiesta apertura file
                //if(open_file)
                //{
                //    //apertura del file
                //    Process.Start(OutputPath);
                //}

                //MessageBox.Show("Stampa documento completata", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //return OutputPath;
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Error);
                return "";
            }

        }

    }



    /// <summary>
    /// Funzioni per il calcolo delle formule
    /// </summary>
    public static class Formule
    {
        /// <summary>
        /// calcola e restituisce il valore del bmi
        /// </summary>
        public static Tuple<double, string> IndiceMassaCorporea(double peso_kg, double altezza_m)
        {
            /*
             * colcolo bmi
             * peso_kg / altezza_mt^2          
            */

            //calcolo bmi
            double bmi = peso_kg / (altezza_m * altezza_m);

            //arrotondamento
            bmi = (Math.Round((double)bmi, 1));

            //stato
            string stato = "";
            if (bmi < 16.5) stato = "grave magrezza";
            else if (bmi >= 16 && bmi <= 18.49) stato = "sottopeso";
            else if (bmi >= 18.5 && bmi <= 24.99) stato = "normopeso";
            else if (bmi >= 25 && bmi <= 29.99) stato = "sovrappeso";

            return new Tuple<double, string>(bmi, stato);
        }

        /// <summary>
        /// calcola e restituisce i valori di prewuenze cardiache (fc max, fondo lento, medio, veloce)
        /// </summary>
        public static List<int> FrequenzeCardiaca(int eta)
        {
            /*
             * formula di tanaka: calcolo frequenza cardiaca:
             * fcmax = 208 - (0.7 * eta)
             * fl = 65% fcmax
             * fm = 75% fcmax
             * fv = 85% fcmax 
            */

            List<int> valori_fc = new List<int>();
            //fc max
            valori_fc.Add(Convert.ToInt16(208 - (0.7 * eta)));
            //fondo lento
            valori_fc.Add(Convert.ToInt16(valori_fc[0] * 0.65));
            //fondo medio
            valori_fc.Add(Convert.ToInt16(valori_fc[0] * 0.75));
            //fondo veloce
            valori_fc.Add(Convert.ToInt16(valori_fc[0] * 0.85));

            return valori_fc;
        }

        /// <summary>
        /// calcola il metabolismo basale con la formula di Harris & Benedict
        /// </summary>
        public static double CalcoloMetabolismoBasale(int sesso, int eta, double peso_kg, double altezza_m)
        {
            double metabolismo_basale = 0;
            switch (sesso)
            {
                case 0: //uomo
                    metabolismo_basale = (66.4730 + (13.7156 * peso_kg) + (5.003 * altezza_m) - (6.775 * eta));
                    break;

                case 1: //donna
                    metabolismo_basale = (655.095 + (9.5634 * peso_kg) + (1.849 * altezza_m) - (4.6756 * eta));
                    break;
            }

            metabolismo_basale = (Math.Round((double)metabolismo_basale, 1));
            return metabolismo_basale;
        }

        /// <summary>
        /// restitusice la lista dei carichi submassimali fino a 20 reps
        /// </summary>
        public static List<Carico> CalcoloMassimale(double carico_kg, int numero_reps)
        {
            //1rm = carico sollevato/ [1.0278 - (0.0278 * reps)]

            List<Carico> carichi = new List<Carico>();

            double carico1RM = Convert.ToInt16(carico_kg / (1.0278 - (0.0278 * numero_reps)));

            carichi.Add(new Carico() { Reps = 1, Percentuale = "100%", Peso = carico1RM });
            carichi.Add(new Carico() { Reps = 2, Percentuale = "95%", Peso = carico1RM * 0.95 });
            carichi.Add(new Carico() { Reps = 4, Percentuale = "90%", Peso = carico1RM * 0.90 });
            carichi.Add(new Carico() { Reps = 6, Percentuale = "85%", Peso = carico1RM * 0.85 });
            carichi.Add(new Carico() { Reps = 8, Percentuale = "80%", Peso = carico1RM * 0.80 });
            carichi.Add(new Carico() { Reps = 10, Percentuale = "75%", Peso = carico1RM * 0.75 });
            carichi.Add(new Carico() { Reps = 12, Percentuale = "70%", Peso = carico1RM * 0.70 });
            carichi.Add(new Carico() { Reps = 15, Percentuale = "65%", Peso = carico1RM * 0.65 });
            carichi.Add(new Carico() { Reps = 20, Percentuale = "60%", Peso = carico1RM * 0.60 });

            return carichi;
        }
    }



    /// <summary>
    /// [DA IMPLEMENTARE NUOVA] Gestione dei permessi per le funzionalita
    /// </summary>
    public static class Permessi
    {
        /// <summary>
        /// verifica se l'istruttore puo creare, modificare, eliminare sedute proprie
        /// </summary>
        /// <returns></returns>
        public static bool check_anamnesi_cud_self(ref Anamnesi a)
        {
            bool res = Session.PermessAttivi.ANAMNESI_CUD_SELF;
            if (res == false)
                Message.Alert(AlertType.warning, "L'istruttore non è abilitato ad inserire, modificare o eliminare anamnesi", "anamenesi");
            return res;
        }

        /// <summary>
        /// verifica accesso al profilo cliente:
        /// 0: admin puo fare tutto su tutti
        /// 1: standard puo gestire solo i suoi
        /// </summary>
        public static bool CheckModificaCliente(ref Cliente c)
        {
            return true;
            //if (Session.IstruttoreAttivo.Permessi == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    if (c.FK_id_istruttore != Session.IstruttoreAttivo.id_istruttore)
            //    {
            //        MessageBox.Show("non è possibile apportare modifiche su un cliente di un altro istruttore", "Verifica permessi", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}

        }

        /// <summary>
        /// verifica modifica credenziali accesso
        /// admin: puo modificare tutto
        /// standard: solo il suo
        /// </summary>
        public static bool CheckModificaCredenziali(ref int id_istruttore_bersaglio)
        {
            return true;
            //if (Session.IstruttoreAttivo.Permessi == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    if (Session.IstruttoreAttivo.id_istruttore == id_istruttore_bersaglio)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("non è possibile modificare le credenziali di un altro istruttore", "Verifica permessi", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return false;
            //    }
            //}
        }

        /// <summary>
        /// verifica aggiunta/rimozione istruttore
        /// </summary>
        public static bool CheckAggiuntaRimozioneIstruttore()
        {
            if (Session.User.PKIstruttore == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("solo l'amministratore può inserire un'altro istruttore", "Verifica permessi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public static class Import
    {
        /// <summary>
        /// 
        /// </summary>
        public static void CaricaCartellaEsercizi()
        {
            string searchPattern = "*.jpeg|*.jpg|*.png";
            string[] searchPatterns = searchPattern.Split('|');
            List<string> dirs = new List<string>();

            CategoriaEsercizio ce;
            Esercizio e;

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //scorre tutte le cartelle dalla root specificata
                    foreach (string dir in Directory.GetDirectories(dialog.SelectedPath, "*", SearchOption.AllDirectories))
                    {
                        //insert categoria (rppresentazione cartella)
                        ce = new CategoriaEsercizio()
                        {
                            Nome = dir.Replace(Path.GetDirectoryName(dir) + Path.DirectorySeparatorChar, ""),
                            Descrizione = "categoria importata da cartella"
                        };
                        int last_cat_id = CategorieEserciziController.InsertUpdate(ce);

                        //per ogni estensione di immagine, aggiungi a files il contenuto della cartella che si sta scorrendo
                        foreach (string sp in searchPatterns)
                        {
                            foreach (string file in Directory.GetFiles(dir, sp))
                            {
                                e = new Esercizio()
                                {
                                    Nome = Path.GetFileNameWithoutExtension(file),
                                    Categoria = new CategoriaEsercizio() { PKCategoria = last_cat_id },
                                    Descrizione = "esercizio importato da file",
                                    Immagine = Common.ByteFromFile(file)
                                };
                                EserciziController.Inserisci(e);
                            }
                        }
                    }
                    MessageBox.Show("importazione da cartella completata");
                }

            }

        }
    }
}
