using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GestionePalestra.MVC
{
    /*
     * [FACTORY(CLASSE BASE)]
     * classe di base a cui tutte le varie factory fanno riferimento per l'esecuzione delle query
     * 
     * [note utilizzo]
     * Select:          per recuperare dei dati, quindi a livello generico il datatable è ottimo
     *                  
     * ExecuteNonQuery: per le query che restituiscono le affected_rows, quindi le operazioni di insert, update, delete
     * 
     * ExecuteScalar:   per quelle query che restituiscono un valore specifico (esempio COUNT). il tipo restituito è object
     *                  cosi lo si converte a seconda delle esigenze
     *                  una utilità della classe è quella di mandare in esecuzione in un comando 2 query sequenziali
     *                  un'INSERT seguito da SELECT LAST_INSERT_ID() in questo modo con una sola query eseguo l'inserimento e ottengo il suo id
     * 
     * Gli overload accettano alternativamente alla query sql direttamente il comando, utile per preparare nelle factory le query parametrizzate
     * _GetAlert() prepara la stringa del messaggio più appropriata in base alla query che viene passata
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * 
     * [DBGET]
     * classe che contiene i metodi per il recupero del dato dal database
     * come parametro prende un object ed a seconda del metodo (es GetInt) imposta un valore di dafault
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * 
     * [DBSET]
     * classe che contiene i metodi per l'mpostazione dei dati nel DB con gli oppurtuni null
     * la classe nasce perchè nell'assegnazione dei valori utilizzando la forma ternaria per gestire gli eventuali null
     * non potevo restituire il DBNull.Value perche di tipi diverso dall'eventuale dato (banana)
     * come parametro i metodi prendono un tipo di dato specifico per il caso (es una stringa per SetString  (.-.) )
     * elaborano il contenuto e restituiscono il tipo dbnull nel caso
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * 
     * [FACTORY<OGGETTO>]
     * contengono tutte le operazioni di accesso ai dati del DB
     * le classi sono statiche e non devono essere istanziate
     * in ogni classe si trovano i metodi per le operazioni crud di base
     * che fanno tutte eco alla classe factory(vedi sopra), dove li viene fatta l'effettiva escuzione
     * 
     * [note sui nomi dei metodi]
     * le liste di oggetti devono chiamarsi GetList<altro>
     * i datatable dei dati devono chiamarsi GetTable<altro>
     * dove possibile impostare sempre InsertUpdate, cosi con un metodo si gestiscono due casistiche
     * in questo metodo viene fatto il controllo sulla PK dell'oggetto, se arriva gia impostato (> 0) il record gia esiste, quindi deve fare l'update
     * altrimenti si tratta di un insert
     * 
     * [note sul dizionario queries]
     * ogni classe per risultare più pulita ha un dizionario contenente el queries che usa
     * nel nomi ho usato regole sintattiche per non incasinarmi
     * alla fine si trova "obj" o "dt"
     * obj: la query viene impostata con un SELECT *, ed i dati vengono impacchettati in un'oggetto
     * dt:  qui i dati devono essere mostrati in maniera civile e decorosa perchè il datatable risultante verrà subito assegnato ad un controllo
     * 
     * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
     * 
     * [NOTE AGGIUNTIVE SULLA GESTIONE DEI CAMPI NULL]
     * 
     * [lettura]
     * per mantenere consistenza tra i campi NULL nel db e le classi del modello
     * ho dovuto implementare queste due classi
     * le proprieta degli oggetti (tranne le stringhe ed altri) sono tutte nullable
     * in questo modo possono essere valorizzate con null in fase di caricamento
     * durante l'assegnazione ai campi del form queste verrano assegnate solo se valorizzate con un processo del tipo
     * if(ogg.Proprieta.hasvalue) txt_campo.Text = ogg.Proprieta
     * oppure
     * txt_campo.Text = (ogg.hasvalue) ? valore : valore vuoto a scelta
     * 
     * [scrittura]
     * nelle procedure di inserimento invece nelle proprieta posso assegnare null
     * per inserire il corrispettivo NULL nel database
     * facoltativamente per i valori numerici posso inserire anche -1 e verra gestito come NULL
     * per rimuovre questa cosa basta togliere la seconda condizione negli if della classe DBSet
     * 
     */


    static class Factory
    {
        static string _GetAlert(string sql)
        {
            string res = "Operazione eseguita";
            string[] lines = (sql.Contains(';')) ? sql.Split(';') : new string[] { sql };
            int valid_line = -1;
            for(int i = 0; i<= lines.Length -1 ; i++)
            {
                string tmp = lines[i].ToUpper();
                if (tmp.Contains("INSERT") || tmp.Contains("UPDATE") || tmp.Contains("DELETE") || tmp.Contains("SELECT")) //se contiene up, del o insert è una riga valida
                {
                    valid_line = i; //riga valida
                    break;          //interrompe la ricerca
                }                    
            }
            
            if(valid_line > -1)
            {
                string valid_sql = lines[valid_line].ToUpper();
                if (valid_sql.Contains("INSERT"))
                    res = "Nuovo elemento inserito";
                if (valid_sql.Contains("UPDATE"))
                    res = "Elemento modificato";
                if (valid_sql.Contains("DELETE"))
                    res = "Elemento rimosso";
                if (valid_sql.Contains("SELECT"))
                    res = "Elemento caricato";
            }

            //if (!valid_sql.Contains("INSERT") || !valid_sql.Contains("UPDATE") || !valid_sql.Contains("DELETE"))
            //    res = "Operazione eseguita";

            return res;
        }

        /// <summary>
        /// Esecuzione query di select
        /// </summary>
        static public DataTable Select(string sql)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, Database.conn);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }


        /// <summary>
        /// Esecuzione query di select
        /// </summary>
        static public DataTable Select(MySqlCommand cmd)
        {
            try
            {
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }


        /// <summary>
        /// manda in esecuzione la query per insert, update e delete e tutte le operazioni che restituiscono delle affected rows
        /// </summary>
        /// <param name="sql">stringa del comando da eseguire</param>
        /// <param name="alert">se true mostra la messagebox di conferma</param>
        /// <param name="ambito">stringa dell'ambito dell'avviso, esempio: clienti, esercizi</param>
        /// <returns>numero di record modificati</returns>
        static public int ExecuteNonQuery(string sql)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, Database.conn);
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }


        /// <summary>
        /// manda in esecuzione il comando per insert, update e delete e tutte le operazioni che restituiscono delle affected rows
        /// </summary>
        /// <param name="sql">stringa della query da eseguire</param>
        /// <param name="alert">se true mostra la messagebox di conferma</param>
        /// <param name="ambito">stringa dell'ambito dell'avviso, esempio: clienti, esercizi</param>
        /// <returns>numero di record modificati</returns>
        static public int ExecuteNonQuery(MySqlCommand cmd)
        {
            try
            {
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }


        /// <summary>
        /// manda in esecuzione la query restituendo la prima cella della prima riga
        /// </summary>
        /// <param name="sql">query da eseguire</param>
        /// /// <param name="alert"></param>
        /// <param name="ambito">categoria di appartenenza dell'azione (es. clienti, schede, esercizi ecc ecc)</param>
        /// <returns>restituisce un oggetto appartenente ad un tipo di dato, dioende la query</returns>
        static public object ExecuteScalar(string sql)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, Database.conn);
                object data = cmd.ExecuteScalar();
                return data;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        /// <summary>
        /// manda in esecuzione il comando restituendo la prima cella della prima riga
        /// </summary>
        /// <param name="sql">query da eseguire</param>
        /// /// <param name="alert"></param>
        /// <param name="ambito">categoria di appartenenza dell'azione (es. clienti, schede, esercizi ecc ecc)</param>
        /// <returns>restituisce un oggetto appartenente ad un tipo di dato, dioende la query</returns>
        static public object ExecuteScalar(MySqlCommand cmd)
        {
            try
            {
                object data = cmd.ExecuteScalar();
                return data;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }

    public static class DBSet
    {
        public static object SetInt(int? value)
        {
            //se ha un valore assegnato ed è maggiore di -1
            if (value.HasValue && value > -1)
                return value;
            else
                return DBNull.Value;
        }
        public static object SetDouble(double? value)
        {
            if (value.HasValue && value > 0)
                return value;
            else
                return DBNull.Value;
        }
        public static object SetString(string value)
        {
            if(value != null && value.Length > 0)
                return value;
            else
                return DBNull.Value;
        }
        public static object SetDateTime(DateTime? value)
        {
            if (value.HasValue && value > DateTime.MinValue)
                return value;
            else
                return DBNull.Value;
        }
        public static object SetBoolean(bool? value)
        {
            if (value.HasValue)
                return value;
            else
                return DBNull.Value;
        }
        public static object SetTimeSpan(TimeSpan? value)
        {
            if (value.HasValue)
                return value;
            else
                return DBNull.Value;
        }
        public static object SetBytes(byte[] value)
        {
            if (value != null)
                return value;
            else
                return DBNull.Value;
        }
    }

    public static class DBGet
    {
        public static int? GetInt(object value)
        {
            if (value != DBNull.Value)
                return Convert.ToInt16(value);
            else
                return null;
        }
        public static double? GetDouble(object value)
        {
            if (value != DBNull.Value)
                return Convert.ToDouble(value);
            else
                return null;
        }
        public static string GetString(object value)
        {
            if(value != DBNull.Value)
                return value.ToString();
            else
                return null;
            ////il valore deve essere una stringa e deve contenere almeno un carattere
            //if ((value is string) && ((string)value).Length > 0)
            //    return value.ToString();

        }
        public static DateTime? GetDateTime(object value)
        {
            if (value != DBNull.Value)
                return Convert.ToDateTime(value);
            else
                return null;
        }
        public static bool? GetBoolean(object value)
        {
            if (value != DBNull.Value)
                return Convert.ToBoolean(value);
            else
                return null;
        }
        public static TimeSpan? GetTimeSpan(object value)
        {
            if (value != DBNull.Value)
                return TimeSpan.Parse(value.ToString());
            else
                return null;
        }
        public static byte[] GetBytes(object value)
        {
            if (value != DBNull.Value)
                return (byte[])value;
            else
                return null;
        }
    }


    public static class FactoryBackup
    {
        public static bool Create(string dir, bool ShowAlert)
        {
            string file = string.Format("backup {0}.sql", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            string path = dir+"//"+file;

            try
            {
                //esecuzione backup
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = Database.conn;
                    using (MySqlBackup ba = new MySqlBackup(cmd))
                    {
                        //da aggiungere l'elenco delle tabelle qui
                        //ba.ExportInfo.ExcludeTables = new List<string> {
                        //    "sys_users",
                        //    "sys_log"
                        //};

                        ba.ExportToFile(path);

                        if (ShowAlert == true)
                            MessageBox.Show("Backup eseguito", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile creare backup:\n" + ex.Message, "Errore ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool Restore(string file_path)
        {
            try
            {
                //esecuzione ripristino
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = Database.conn;
                    using (MySqlBackup ba = new MySqlBackup(cmd))
                    {
                        ba.ImportFromFile(file_path);

                        MessageBox.Show("Ripristino di sistema eseguito", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossibile ripristinare il sistema:\n" + ex.Message, "Errore ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }


    //////////////////////////////////////

    
    public static class AnamnesiController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select",               "SELECT * FROM anamnesi WHERE id_anamnesi = {0};" },
            { "insert",               "INSERT INTO anamnesi VALUES(DEFAULT, @fk_istr, @fk_cli, @data, @ann, @somat, @peso, @alt, @stile_vita, @obiet, @pref, @freq, @dur,  @sport, @farmaci, @patologie, @fumatore, @bevitore, @alimenti, @intolleranze, @integratori, @collo, @petto, @spalle, @braccia, @avambracci, @vita, @fianchi, @cosce, @polp, @mm, @mg, @liq); SELECT LAST_INSERT_ID();" },
            { "update",               "UPDATE anamnesi SET fk_id_istr=@id_istr, data=@data, peso=@w, altezza=@h, fk_id_tipo_lifestyle=@stile_vita, fk_id_tipo_obiettivo=@obiet, preferenze=@pref, frequenza_settimanale=@freq_sett, durata_seduta=@dur_sed, annotazioni=@ann, fumatore=@fum, bevitore=@bev, somatotipo=@som WHERE id_anamnesi=@id_a;" },
            { "delete",               "DELETE FROM anamnesi WHERE id_anamnesi = {0};" },
            { "select_elemento",      "SELECT * FROM {0} WHERE FK_ID_ANAMNESI={1};" },
            { "sel_anam_by_cli_dt",   @"SELECT id_anamnesi as '#', peso, altezza, tipo_lifestyle as 'stile di vita', tipo_obiettivo as 'obiettivo', data, CONCAT(istruttori.nome,' ',istruttori.cognome) as 'istruttore' 
                                      FROM anamnesi
                                      LEFT JOIN istruttori ON anamnesi.fk_id_istruttore = istruttori.id_istruttore 
                                      WHERE fk_id_cliente = {0};"
            }
            
        };
        
        public static Anamnesi Select(int id)
        {
            string query = string.Format(queries["select"], id);
            Anamnesi a = null;
            DataTable dt = Factory.Select(query);
            if (dt.Rows.Count > 0)
            {
                a = _GetAnamnesi(dt.Rows[0]);
                
            }
            return a;
        }
        public static int InsertUpdate(Anamnesi a)
        {
            string q = (a.PKAnamnesi > 0)   //se ha la PK è gia assegnata vuol dire che il record esiste
                ? queries["update"]         //quindi richiama update
                : queries["insert"];        //altrimenti insert

            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            cmd.Parameters.AddWithValue("@id_istr", DBSet.SetInt(a.FKIstruttore));
            cmd.Parameters.AddWithValue("@data", DBSet.SetDateTime(a.Data));
            cmd.Parameters.AddWithValue("@peso", DBSet.SetDouble(a.Peso));
            cmd.Parameters.AddWithValue("@alt", DBSet.SetInt(a.Altezza));
            //cmd.Parameters.AddWithValue("@stile_vita", DBSet.SetInt(a.FKStileDiVita));
            //cmd.Parameters.AddWithValue("@obiet", DBSet.SetInt(a.FKObiettivo));
            cmd.Parameters.AddWithValue("@pref", DBSet.SetInt(a.Preferenze));
            cmd.Parameters.AddWithValue("@freq", DBSet.SetInt(a.FrequenzaSettimanale));
            cmd.Parameters.AddWithValue("@dur", DBSet.SetTimeSpan(a.DurataSeduta));
            cmd.Parameters.AddWithValue("@ann", DBSet.SetString(a.Annotazioni));
            cmd.Parameters.AddWithValue("@fum", DBSet.SetDouble(a.MassaMagra));
            cmd.Parameters.AddWithValue("@bev", DBSet.SetDouble(a.MassaGrassa));
            if (a.PKAnamnesi > 0)
                cmd.Parameters.AddWithValue("@id_a", a.PKAnamnesi);

            if(a.PKAnamnesi > 0)
                Factory.ExecuteNonQuery(cmd);
            else
                a.PKAnamnesi = Convert.ToInt16(Factory.ExecuteScalar(cmd));

            return a.PKAnamnesi;
        }
        public static int Delete(int id)
        {
            string q = string.Format(queries["delete"], id);
            return (int)Factory.ExecuteNonQuery(q);
        }
        public static DataTable GetTableAnamnesiCliente(int id_utente)
        {
            string q = string.Format(queries["sel_anam_by_cli_dt"], id_utente);
            return Factory.Select(q);
        }

        private static Anamnesi _GetAnamnesi(DataRow dr)
        {
            Anamnesi a = new Anamnesi();
            a.PKAnamnesi = Convert.ToInt16(dr["ID_ANAMNESI"]);
            a.FKCliente = Convert.ToInt16(dr["FK_ID_CLIENTE"]);
            a.FKIstruttore = Convert.ToInt16(dr["FK_ID_ISTRUTTORE"]);
            a.Data = Convert.ToDateTime(dr["DATA"]);
            a.Annotazioni = DBGet.GetString(dr["ANNOTAZIONI"]);
            a.Somatotipo = DBGet.GetInt(dr["SOMATOTIPO"]);
            a.Peso = DBGet.GetDouble(dr["PESO"]);
            a.Altezza = DBGet.GetInt(dr["ALTEZZA"]);
            a.TipoStileDiVita = DBGet.GetInt(dr["TIPO_LIFESTYLE"]);
            a.TipoObiettivo = DBGet.GetInt(dr["TIPO_OBIETTIVO"]);
            a.Preferenze = DBGet.GetInt(dr["PREFERENZE_ALLENAMENTO"]);
            a.FrequenzaSettimanale = DBGet.GetInt(dr["FREQUENZA_SETTIMANALE"]);
            a.DurataSeduta = DBGet.GetTimeSpan(dr["DURATA_SEDUTA"]);
            //gestire i null nelle liste
            a.Sport = new List<string>(DBGet.GetString(dr["SPORT"]).Split(new char[';'], StringSplitOptions.RemoveEmptyEntries));
            a.Farmaci = new List<string>(DBGet.GetString(dr["FARMACI"]).Split(new char[';'], StringSplitOptions.RemoveEmptyEntries));
            a.Patologie = new List<string>(DBGet.GetString(dr["PATOLOGIE"]).Split(new char[';'], StringSplitOptions.RemoveEmptyEntries));
            a.Fumatore = DBGet.GetBoolean(dr["FUMATORE"]);
            a.Bevitore = DBGet.GetBoolean(dr["BEVITORE"]);
            a.Alimenti = new List<string>(DBGet.GetString(dr["ALIMENTI"]).Split(new char[';'], StringSplitOptions.RemoveEmptyEntries));
            a.Intolleranze = new List<string>(DBGet.GetString(dr["INTOLLERANZE"]).Split(new char[';'], StringSplitOptions.RemoveEmptyEntries));
            a.Intolleranze = new List<string>(DBGet.GetString(dr["INTEGRATORI"]).Split(new char[';'], StringSplitOptions.RemoveEmptyEntries));
            a.Collo = DBGet.GetDouble(dr["COLLO"]);
            a.Petto = DBGet.GetDouble(dr["PETTO"]);
            a.Spalle = DBGet.GetDouble(dr["SPALLE"]);
            a.Braccia = DBGet.GetDouble(dr["BRACCIA"]);
            a.Avambracci = DBGet.GetDouble(dr["AVAMBRACCI"]);
            a.Girovita = DBGet.GetDouble(dr["VITA"]);
            a.Fianchi = DBGet.GetDouble(dr["FIANCHI"]);
            a.Gambe = DBGet.GetDouble(dr["COSCE"]);
            a.Polpacci = DBGet.GetDouble(dr["POLPACCI"]);
            a.MassaMagra = DBGet.GetDouble(dr["MASSA_MAGRA"]);
            a.MassaGrassa= DBGet.GetDouble(dr["MASSA_GRASSA"]);
            a.Liquidi = DBGet.GetDouble(dr["LIQUIDI"]);
            return a;
        }
    }



    public static class AnnotazioniController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select", "SELECT * FROM annotazioni WHERE id_annotazione = {0}" },
            { "insert", "INSERT INTO annotazioni VALUES(DEFAULT, @id_istr,@titolo, @testo, @data, @svolto); SELECT LAST_INSERT_ID();" },
            { "update", "UPDATE annotazioni SET titolo=@titolo, testo=@testo, data=@data, svolto=@svolto WHERE id_annotazione=@id_ann;" },
            { "delete", "DELETE FROM annotazioni WHERE id_annotazione = {0};" },
            { "select_annotazioni_istruttore", "SELECT * FROM annotazioni WHERE fk_id_istruttore={0} ORDER BY svolto DESC,data;" }
        };
        public static int InsertUpdate(Annotazione a)
        {
            string q = (a.PKAnnotazione > 0)
                ? queries["update"]
                : queries["insert"];

            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            cmd.Parameters.AddWithValue("@id_istr", DBSet.SetInt(a.FKIstruttore));
            cmd.Parameters.AddWithValue("@titolo", DBSet.SetString(a.Titolo));
            cmd.Parameters.AddWithValue("@testo", DBSet.SetString(a.Testo));
            cmd.Parameters.AddWithValue("@data", DBSet.SetDateTime(a.Data));
            cmd.Parameters.AddWithValue("@svolto", DBSet.SetBoolean(a.Svolto));
            if (a.PKAnnotazione > 0)
                cmd.Parameters.AddWithValue("@id_ann", DBSet.SetInt(a.PKAnnotazione));

            return (a.PKAnnotazione > 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Delete(int id_annotazione)
        {
            string q = string.Format(queries["delete"], id_annotazione);
            return (int)Factory.ExecuteNonQuery(q);
        }
        public static List<Annotazione> Select(int id_istruttore)
        {
            List<Annotazione> annotazioni = new List<Annotazione>();
            string query = string.Format(queries["select_annotazioni_istruttore"], id_istruttore);

            DataTable dt = Factory.Select(query);
            foreach (DataRow dr in dt.Rows)
            {
                annotazioni.Add(_GetAnnotazione(dr));
            }
            return annotazioni;
        }
        private static Annotazione _GetAnnotazione(DataRow dr)
        {
            return new Annotazione()
            {
                PKAnnotazione = Convert.ToInt16(dr[0]),
                FKIstruttore = Convert.ToInt16(dr[1]),
                Titolo = DBGet.GetString(dr[2]),
                Testo = DBGet.GetString(dr[3]),
                Data = DBGet.GetDateTime(dr[4]),
                Svolto = DBGet.GetBoolean(dr[5])
            };
        }
    }


    
    public static class AvvisoController
    {
        //da rivedere delle cose dove indicato

        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select",                     "SELECT * FROM avvisi WHERE id_avviso = {0}" },
            { "select_all",                 "SELECT * FROM avvisi {0};" },
            { "insert_avviso_return",       "INSERT INTO avvisi VALUES(DEFAULT, @titolo, @tipo, @prior, @data, @descr, @pers, @id_istr, NOW(), @id_cli); SELECT LAST_INSERT_ID();" },
            { "insert_avviso_istruttore",   "INSERT INTO avvisi_istruttori VALUES(@id_avviso, @id_istr, NULL);"},
            { "update_avviso",              "UPDATE avvisi SET TITOLO=@titolo, FK_ID_TIPO_AVVISO=@tipo, PRIORITA=@prior, DATA=@data, DESCRIZIONE=@descr, PERSONALE=@pers, FK_ID_ISTRUTTORE=@id_istr, FK_ID_CLIENTE=@id_cli WHERE ID_AVVISO=@id_avviso;" },
            { "delete_avviso_istruttore",   "DELETE FROM avvisi_istruttori WHERE FK_ID_AVVISO = {0};"},
            { "delete",                     "DELETE FROM avvisi WHERE id_avviso={0};" },
            { "update_avviso_istruttore",   "UPDATE avvisi_istruttori SET data_lettura = NOW() WHERE fk_id_avviso = {0} AND fk_id_istruttore = {1};"},
            {
              "select_nome_data_lettura",   @"SELECT CONCAT(istruttori.nome,' ',istruttori.cognome) as 'istruttore', avvisi_istruttori.data_lettura as 'data lettura' 
                                            FROM avvisi_istruttori INNER JOIN istruttori ON avvisi_istruttori.fk_id_istruttore = istruttori.id_istruttore 
                                            WHERE avvisi_istruttori.fk_id_avviso = {0};"
            },
            { "select_stato_lettura",       "SELECT data_lettura FROM avvisi_istruttori WHERE fk_id_avviso = {0} AND fk_id_istruttore = {1};" },
            { "select_avvisi_personali",    "SELECT * FROM avvisi WHERE fk_id_istruttore={0} and personale=1 and data between '{1}' and '{2}';" },
            { "select_avvisi_pubblici",     "da fare"},
            { "select_avvisi_istruttori" ,  "SELECT * FROM avvisi_istruttori WHERE fk_id_avviso = {0};" },
            { "notifiche_personali",        "SELECT * FROM avvisi WHERE fk_id_istruttore={0} and personale=1 and data between '{1}' and '{2}';" },
            { "notifiche_comunicazioni",    @"SELECT avvisi.* FROM avvisi 
                                            INNER JOIN avvisi_istruttori ON avvisi.id_avviso = avvisi_istruttori.fk_id_avviso 
                                            WHERE avvisi_istruttori.fk_id_istruttore = {0} AND (avvisi.data BETWEEN '{1}' AND '{2}');" }
        };
        public static Avviso Select(int id_avviso)
        {
            Avviso a = null;
            DataTable dt;
            string q;

            q = string.Format(queries["select"], id_avviso);
            dt = Factory.Select(q);
            if(dt.Rows.Count > 0)
            {
                //dati avviso
                a = _GetAvviso(dt.Rows[0]);

                //carica istruttori destinatari
                q = string.Format(queries["select_avvisi_istruttori"], id_avviso);
                dt = Factory.Select(q);
                foreach (DataRow row in dt.Rows)
                    a.Destinatari.Add(_GetAvvisoIstruttore(row, id_avviso));
            }
            return a; 
        }
        public static int insert(Avviso a)
        {
            if (!_FieldsCheck(a))
                return -1;

            MySqlTransaction trans = Database.conn.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Database.conn;

            // inserimento avviso
            cmd.CommandText = queries["insert_avviso_return"];
            cmd.Parameters.AddWithValue("@titolo", DBSet.SetString(a.Titolo));
            cmd.Parameters.AddWithValue("@tipo", DBSet.SetInt(a.FKTipo));
            cmd.Parameters.AddWithValue("@prior", DBSet.SetInt(a.Priorita));
            cmd.Parameters.AddWithValue("@data", DBSet.SetDateTime(a.Data));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(a.Descrizione));
            cmd.Parameters.AddWithValue("@pers", DBSet.SetBoolean(a.isPersonal));
            cmd.Parameters.AddWithValue("@id_istr", DBSet.SetInt(a.FKIstruttore));
            cmd.Parameters.AddWithValue("@id_cli", DBSet.SetInt(a.FKCliente));
            a.PKAvviso = Convert.ToInt16(Factory.ExecuteScalar(cmd));

            if (a.PKAvviso > 0 && a.isPersonal == false) //se l'inserimento dell'avviso va a buon fine e l'avviso è pubblico
            {
                //2. inserimento destinatari
                foreach (AvvisoIstruttore ai in a.Destinatari)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = queries["insert_avviso_istruttore"];
                    cmd.Parameters.AddWithValue("@id_avviso", DBSet.SetInt(a.PKAvviso));
                    cmd.Parameters.AddWithValue("@id_istr", DBSet.SetInt(ai.FKIstruttoreDestinatario));
                    Factory.ExecuteNonQuery(cmd);
                }
            }
            trans.Commit();
            return a.PKAvviso;
        }
        public static int Update(Avviso a)
        {
            if (!_FieldsCheck(a))
                return -1;

            MySqlTransaction trans = Database.conn.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Database.conn;

            //1. inserimento scheda
            cmd.Parameters.Clear();
            cmd.CommandText = queries["update_avviso"];
            cmd.Parameters.AddWithValue("@id_avviso", DBSet.SetInt(a.PKAvviso));
            cmd.Parameters.AddWithValue("@titolo", DBSet.SetString(a.Titolo));
            cmd.Parameters.AddWithValue("@tipo", DBSet.SetInt(a.FKTipo));
            cmd.Parameters.AddWithValue("@prior", DBSet.SetInt(a.Priorita));
            cmd.Parameters.AddWithValue("@data", DBSet.SetDateTime(a.Data));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(a.Descrizione));
            cmd.Parameters.AddWithValue("@pers", DBSet.SetBoolean(a.isPersonal));
            cmd.Parameters.AddWithValue("@id_istr", DBSet.SetInt(a.FKIstruttore));
            cmd.Parameters.AddWithValue("@id_cli", DBSet.SetInt(a.FKCliente));
            int affected_rows = Factory.ExecuteNonQuery(cmd);


            if (affected_rows > 0) //se l'inserimento dell'avviso
            {
                //2. cancellazione vecchi destinatari (nel foreach potrei capire i record gia inseriti dai nuovi, ma la tabella non ha PK)
                //   quindi cancello tutti i vecchi e li riscrivo
                Factory.ExecuteNonQuery(string.Format(queries["delete_avviso_istruttore"],a.PKAvviso));

                //3. inserimento destinatari
                foreach (AvvisoIstruttore ai in a.Destinatari)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = queries["insert_avviso_istruttore"];
                    cmd.Parameters.AddWithValue("@id_avviso", a.PKAvviso);
                    cmd.Parameters.AddWithValue("@id_istr", ai.FKIstruttoreDestinatario);
                    cmd.Parameters.AddWithValue("@data", ai.DataLettura);
                    Factory.ExecuteNonQuery(cmd);
                }
            }
            trans.Commit();
            return affected_rows;
        }
        public static int Delete(int id_avviso)
        {
            return Factory.ExecuteNonQuery(string.Format(queries["delete"], id_avviso));
        }
        
        public static bool ConfermaLetturaAvviso(int id_avviso, int id_istruttore)
        {
            string query = string.Format(queries["update_avviso_istruttore"], id_avviso, id_istruttore);
            int res = Factory.ExecuteNonQuery(query);
            return (res > 0) ? true : false;
        }
        public static DataTable GetStatiLetture(int id_avviso)
        {
            //restituisce la tabella nome_istruttore | data_lettura per l'avviso specificato
            string query = string.Format(queries["select_nome_data_lettura"], id_avviso);
            return Factory.Select(query);
        }
        public static DateTime? GetStatoLettura(int id_avviso, int id_istruttore)
        {
            string query = string.Format(queries["select_stato_lettura"], id_avviso, id_istruttore);
            object res = Factory.ExecuteScalar(query);
            return DBGet.GetDateTime(res);
        }

        //notifiche
        public static DataTable GetNotificheAvvisiPersonali(int id_istruttore, DateTime date_start, DateTime date_end)
        {
            string sql = string.Format(queries["notifiche_personali"], id_istruttore, date_start.ToString("yyyy-MM-dd hh:mm:ss"), date_end.ToString("yyyy-MM-dd hh:mm:ss"));
            return Factory.Select(sql);
        }
        public static DataTable GetNotificheComunicazioni(int id_istruttore, DateTime date_start, DateTime date_end)
        {
            string query = string.Format(queries["notifiche_comunicazioni"], id_istruttore, date_start.ToString("yyyy-MM-dd"), date_end.ToString("yyyy-MM-dd"));
            return Factory.Select(query);
        }

        //visualizzazione: si potrebbe fare il ritorno in oggetti, sarebbe meglio
        public static DataTable GetComunicazioni(int id_istr, string where_cluse)
        {
            //elenco degli avvisi pubblici destinati e creati dall'istruttore indicato
            string query = string.Format(
            "SELECT avvisi.id_avviso as '#', data as 'data scadenza', data_inserimento as 'data inserimento', CONCAT(istruttori.nome,' ', istruttori.cognome) as 'creato da', descrizione, data_lettura as 'letto' " +
            "FROM avvisi LEFT JOIN avvisi_istruttori ON avvisi.id_avviso = avvisi_istruttori.fk_id_avviso " +
            "INNER JOIN istruttori ON avvisi.fk_id_istruttore = istruttori.id_istruttore " +
            "WHERE avvisi_istruttori.fk_id_istruttore = {0} " +
            "OR (avvisi.fk_id_istruttore = {0} AND personale = 0) " +
            "{1} " +
            "GROUP BY avvisi.id_avviso " +
            "ORDER BY data DESC; ", id_istr, where_cluse);
            return Factory.Select(query);
        }
        public static DataTable GetAgenda(int id_istr, string where_clause)
        {
            //solo gli avvisi persinali
            string query = string.Format(
                "SELECT id_avviso as '#', data, descrizione FROM avvisi WHERE fk_id_istruttore={0} AND personale=1 {1} ORDER BY data DESC;",
                id_istr, where_clause);
            return Factory.Select(query);
        }

        private static bool _FieldsCheck(Avviso a)
        {
            string text = "Impossibile inserire le modifiche";
            int base_text = text.Length;

            if (a.Titolo == "") text += "\n-Titolo non impostato";
            if (a.Data == null) text += "\n-Data non impostata";
            if (a.Descrizione == "") text += "\n-Descrizione non impostata";

            if (text.Length > base_text)
            {
                MessageBox.Show(text, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        private static Avviso _GetAvviso(DataRow dr)
        {
            return new Avviso()
            {
                PKAvviso = Convert.ToInt16(dr[0]),
                Titolo = DBGet.GetString(dr[1]),
                FKTipo = DBGet.GetInt(dr[2]),
                Priorita = DBGet.GetInt(dr[3]),
                Data = DBGet.GetDateTime(dr[4]),
                Descrizione = DBGet.GetString(dr[5]),
                isPersonal = DBGet.GetBoolean(dr[6]),
                FKIstruttore = Convert.ToInt16(dr[7]),
                DataInserimento = DBGet.GetDateTime(dr[8]),
                FKCliente = DBGet.GetInt(dr[9]),
            };
        }
        private static AvvisoIstruttore _GetAvvisoIstruttore(DataRow dr, int id_avviso)
        {
            AvvisoIstruttore ai = new AvvisoIstruttore();
            ai.FKAvviso = Convert.ToInt16(dr[0]);
            ai.FKIstruttoreDestinatario = Convert.ToInt16(dr[1]);
            ai.DataLettura = DBGet.GetDateTime(dr[2]);
            return ai;
        }
    }



    public static class CategorieEserciziController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select",             "SELECT * FROM categorie_esercizi WHERE id_categoria = {0}" },
            { "insert",             "INSERT INTO categorie_esercizi VALUES(DEFAULT, @nome, @descr); SELECT LAST_INSERT_ID();" },
            { "update",             "UPDATE categorie_esercizi SET nome = @nome,  descrizione = @descr WHERE id_categoria = @id_cat;"},
            { "delete",             "DELETE FROM categorie_esercizi WHERE id_categoria = {0};" },
            { "select_categorie",   "SELECT categorie_esercizi.*, COUNT(esercizi.id_esercizio) " +
                                    "FROM categorie_esercizi " +
                                    "LEFT JOIN esercizi ON categorie_esercizi.id_categoria = esercizi.fk_id_categoria " +
                                    "GROUP BY esercizi.fk_id_categoria, categorie_esercizi.nome " +
                                    "ORDER BY categorie_esercizi.nome ASC;"
            }
        };
        public static CategoriaEsercizio Seleziona(int id)
        {
            CategoriaEsercizio c = null;
            string query = string.Format(queries["select"], id);
            DataTable dt = Factory.Select(query);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                c = new CategoriaEsercizio()
                {
                    PKCategoria = Convert.ToInt16(dt.Rows[0][0]),
                    Nome = DBGet.GetString(dt.Rows[0][1]),
                    Descrizione = DBGet.GetString(dt.Rows[0][2])
                };
            }
            return c;
        }
        public static int InsertUpdate(CategoriaEsercizio c)
        {
            if (!_FieldsCheck(c))
                return -1;

            string q = (c.PKCategoria > 0)
                ? queries["update"]
                : queries["insert"];

            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(c.Nome));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(c.Descrizione));
            if(c.PKCategoria > 0)
                cmd.Parameters.AddWithValue("@id_cat", c.PKCategoria);

            return (c.PKCategoria > 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Delete(int id)
        {
            string query = string.Format(queries["delete"], id);
            return Factory.ExecuteNonQuery(query);
        }
        public static List<CategoriaEsercizio> GetCategorie()
        {
            List<CategoriaEsercizio> categorie = new List<CategoriaEsercizio>();
            DataTable dt = Factory.Select(queries["select_categorie"]);
            categorie.Add(new CategoriaEsercizio()
            {
                PKCategoria = -1,
                Nome = "-nessuna categoria-",
                Descrizione = "esercizi senza categoria"
            });
            foreach (DataRow dr in dt.Rows)
            {
                categorie.Add(new CategoriaEsercizio()
                {
                    PKCategoria = Convert.ToInt32(dr[0]),
                    Nome = DBGet.GetString(dr[1]),
                    Descrizione = DBGet.GetString(dr[2]),
                    CountEsercizi = DBGet.GetInt(dr[3])
                });
            }
            
            return categorie;
        }
        private static bool _FieldsCheck(CategoriaEsercizio c)
        {
            string text = "Impossibile inserire le modifiche";
            int base_text = text.Length;

            if (c.Nome == "") text += "\n-Nome non impostato";

            if (text.Length > base_text)
            {
                MessageBox.Show(text, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    
    public static class CategorieSchedeController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select",             "SELECT * FROM categorie_schede WHERE id_categoria = {0}" },
            { "insert",             "INSERT INTO categorie_schede VALUES(DEFAULT, @nome, @descr);" },
            { "update",             "UPDATE categorie_schede SET nome = @nome,  descrizione = @descr WHERE id_categoria = @id_cat;"},
            { "delete",             "DELETE FROM categorie_schede WHERE id_categoria = {0};" },
            { "select_categorie",   @"SELECT categorie_schede.*, COUNT(schede.fk_id_categoria_scheda) FROM categorie_schede 
                                    LEFT JOIN schede ON categorie_schede.id_categoria = schede.fk_id_categoria_scheda 
                                    GROUP BY schede.fk_id_categoria_scheda, categorie_schede.nome
                                    ORDER BY categorie_schede.nome ASC;"
            }
        };
        public static int Inserisci(CategoriaScheda c)
        {
            if (!_FieldsCheck(c))
                return -1;

            MySqlCommand cmd = new MySqlCommand(queries["insert"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", c.Nome);
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(c.Descrizione));
            return Convert.ToInt16(Factory.ExecuteNonQuery(cmd));
        }
        public static int Modifica(CategoriaScheda c)
        {
            if (!_FieldsCheck(c))
                return -1;

            MySqlCommand cmd = new MySqlCommand(queries["update"], Database.conn);
            cmd.Parameters.AddWithValue("@id_cat", c.PKCategoria);
            cmd.Parameters.AddWithValue("@nome", c.Nome);
            cmd.Parameters.AddWithValue("@descr", c.Descrizione);
            return Convert.ToInt16(Factory.ExecuteNonQuery(cmd));
        }
        public static int Elimina(int id_cat)
        {
            string query = string.Format(queries["delete"], id_cat);
            return Convert.ToInt16(Factory.ExecuteNonQuery(query));
        }
        public static int CountSchedeCategoria(int id_cat)
        {
            string sql = "SELECT COUNT(*) FROM schede WHERE id_categoria_scheda = " + id_cat;
            return Convert.ToInt16(Factory.ExecuteScalar(sql));
        }
        public static DataTable GetCategorieSchedeDT_old()
        {
            string q =
                "SELECT categorie_schede.*, COUNT(schede.id_categoria_scheda) " +
                "FROM categorie_schede " +
                "LEFT JOIN schede ON categorie_schede.id_categoria_scheda = schede.id_categoria_scheda " +
                "GROUP BY schede.id_categoria_scheda, categorie_schede.nome_categoria " +
                "ORDER BY categorie_schede.nome_categoria ASC";
            //string sql = "SELECT *, count(*) FROM categorie_schede ORDER BY nome_categoria;";
            return Factory.Select(q);
        }
        public static List<CategoriaScheda> GetCategorieSchede()
        {
            List<CategoriaScheda> categorie = new List<CategoriaScheda>();
            DataTable dt = Factory.Select(queries["select_categorie"]);
            categorie.Add(new CategoriaScheda()
            {
                PKCategoria = -1,
                Nome = "-nessuna categoria-",
                Descrizione = "schede senza categoria"
            });
            foreach (DataRow dr in dt.Rows)
            {
                categorie.Add(new CategoriaScheda()
                {
                    PKCategoria = Convert.ToInt16(dr[0]),
                    Nome = DBGet.GetString(dr[1]),
                    Descrizione = DBGet.GetString(dr[2]),
                    CountSchede = DBGet.GetInt(dr[3])
                });
            }
            return categorie;
        }
        private static bool _FieldsCheck(CategoriaScheda c)
        {
            string text = "Impossibile inserire le modifiche";
            int base_text = text.Length;

            if (c.Nome == "") text += "\n-Nome non impostato";

            if (text.Length > base_text) {
                MessageBox.Show(text, "avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } else {
                return true;
            }
        }
    }



    public static class ClienteController
    {
        //sicuro saltano gabole sulle select del datagrid

        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_one",     "SELECT * FROM clienti WHERE id_cliente = {0};" },
            { "select_all",     "SELECT * FROM clienti ORDER BY id_cliente DESC;" },
            { "insert_return",  @"INSERT INTO clienti VALUES(DEFAULT, @nome, @cogn, @sesso, @cf, @cn, @dn, @tel, @email, @di, @stato, @note, @img, @id_istr); 
                                SELECT LAST_INSERT_ID();"
            },
            { "update",         @"UPDATE clienti SET nome=@nome, cognome=@cogn, codice_fiscale=@cf, sesso=@sesso, citta_nascita=@cittn, 
                                data_nascita=@dn, telefono=@tel, email=@email, data_iscrizione=@dataiscr, note=@note, fk_id_tipo_stato=@stato, fk_id_istruttore=@id_istr, immagine=@img  
                                WHERE id_cliente=@id_cli;"
            },
            { "delete",         "DELETE FROM clienti WHERE id_cliente = {0};" },
            { "select_all_dt",  @"SELECT id_cliente as `#`, clienti.nome, clienti.cognome, IF(clienti.sesso = 0, 'donna', 'uomo') AS sesso, clienti.codice_fiscale as `codice fiscale`, clienti.citta_nascita as `citta nascita`, clienti.data_nascita as `data nascita`, clienti.telefono, clienti.email, data_iscrizione as `data iscrizione`, clienti_tipi_stati.valore as 'stato', CONCAT(istruttori.nome,' ',istruttori.cognome) as 'istruttore' 
                                FROM clienti LEFT JOIN istruttori ON clienti.fk_id_istruttore = istruttori.id_istruttore
                                LEFT JOIN clienti_tipi_stati ON clienti.fk_id_tipo_stato = clienti_tipi_stati.id_tipo " },
            { "order_by_desc", " ORDER BY id_cliente DESC;"}
        };
        public static DataRow GetCliente(int id_cliente)
        {
            string query = string.Format(queries["select_one"], id_cliente);
            DataTable dt = Factory.Select(query);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }
        public static Cliente Seleziona(int id_cliente)
        {
            string query = string.Format(queries["select_one"], id_cliente);
            Cliente u = null;
            DataTable dt = Factory.Select(query);
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                u = _GetCliente(dr);
            }
            return u;
            
        }
        public static int Inserisci(Cliente c)
        {
            if (!_FieldsCheck(c))
                return -1;

            MySqlCommand cmd = new MySqlCommand(queries["insert_return"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(c.Nome));
            cmd.Parameters.AddWithValue("@cogn", DBSet.SetString(c.Cognome));
            cmd.Parameters.AddWithValue("@sesso", DBSet.SetInt(c.Sesso));
            cmd.Parameters.AddWithValue("@cf", DBSet.SetString(c.CodiceFiscale));
            cmd.Parameters.AddWithValue("@cn", DBSet.SetString(c.CittaNascita));
            cmd.Parameters.AddWithValue("@dn", DBSet.SetDateTime(c.DataNascita));
            cmd.Parameters.AddWithValue("@tel", DBSet.SetString(c.Telefono));
            cmd.Parameters.AddWithValue("@email", DBSet.SetString(c.Email));
            cmd.Parameters.AddWithValue("@di", DBSet.SetDateTime(c.DataIscrizione));
            cmd.Parameters.AddWithValue("@stato", DBSet.SetInt(c.FKStato));
            cmd.Parameters.AddWithValue("@note", DBSet.SetString(c.Note));
            cmd.Parameters.AddWithValue("@img", DBSet.SetBytes(c.Immagine));
            cmd.Parameters.AddWithValue("@id_istr", c.FKIstruttore);

            int res = Convert.ToInt16(Factory.ExecuteScalar(cmd));
            return res;
        }
        public static int Modifica(Cliente c)
        {
            if (!_FieldsCheck(c))
                return -1;

            MySqlCommand cmd = new MySqlCommand(queries["update"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", c.Nome);
            cmd.Parameters.AddWithValue("@cogn", c.Cognome);
            cmd.Parameters.AddWithValue("@cf", c.CodiceFiscale);
            cmd.Parameters.AddWithValue("@sesso", c.Sesso);
            cmd.Parameters.AddWithValue("@cittn", c.CittaNascita);
            cmd.Parameters.AddWithValue("@dn", c.DataNascita);
            cmd.Parameters.AddWithValue("@tel", c.Telefono);
            cmd.Parameters.AddWithValue("@email", c.Email);
            cmd.Parameters.AddWithValue("@dataiscr", c.DataIscrizione);
            cmd.Parameters.AddWithValue("@note", c.Note);
            cmd.Parameters.AddWithValue("@stato", DBSet.SetInt(c.FKStato));
            cmd.Parameters.AddWithValue("@id_istr", c.FKIstruttore);
            cmd.Parameters.AddWithValue("@img", c.Immagine);
            cmd.Parameters.AddWithValue("@id_cli", c.PKCliente);

            int res = Factory.ExecuteNonQuery(cmd);
            return res;
        }
        public static int Elimina(int id)
        {
            string query = string.Format(queries["delete"], id);
            return Factory.ExecuteNonQuery(query);
        }
        public static List<Cliente> GetListClienti()
        {
            List<Cliente> clienti = new List<Cliente>();
            DataTable dt = Factory.Select(queries["select_all"]);
            foreach (DataRow dr in dt.Rows)
                clienti.Add(_GetCliente(dr));
            return clienti;
        }
        public static List<ClienteBase> GetListClientiBase(string where_clause)
        {
            List<ClienteBase> clienti = new List<ClienteBase>();
            DataTable dt = Factory.Select(queries["select_all"]);
            foreach (DataRow dr in dt.Rows)
                clienti.Add(new ClienteBase() {
                    PKCliente = Convert.ToInt16(dr[0]),
                    Nome = DBGet.GetString(dr[1]),
                    Cognome = DBGet.GetString(dr[2])
                });
            return clienti;
        }
        public static DataTable GetTableClienti(string where_clause)
        {
            MySqlCommand cmd = new MySqlCommand(queries["select_all_dt"] + where_clause + queries["order_by_desc"], Database.conn);
            DataTable dt_src = Factory.Select(cmd);

            //DataTable dt_dest = dt_src.Clone();
            //dt_dest.Columns["sesso"].DataType = typeof(string);

            ////rende tutte le colonne stringhe (per avere meno beghe sui tipi di dato)
            ////foreach (DataColumn dc in dt_dest.Columns)
            ////    dc.DataType = typeof(string);

            ////import record nella nuova datatable
            //foreach (DataRow dr in dt_src.Rows)
            //{
            //    DataRow processed = dr;
            //    processed
            //    if ((int)processed["sesso"] == 0)
            //        processed["sesso"] = "uomo";
            //    else
            //        processed["sesso"] = "donna";
            //    dt_dest.ImportRow(processed);
            //}


            ////for (int i = 0; i < dt_dest.Columns.Count; i++)
            ////{
            ////    if (dt_dest.Columns[i].DataType != typeof(string))
            ////        dt_dest.Columns[i].DataType = typeof(string);
            ////}



            ////modifica dati
            ////foreach (DataRow row in dt_dest.Rows)
            ////{
            ////    if ((string)row["sesso"] == "0")
            ////        row["sesso"] = "Uomo";
            ////    else
            ////        row["sesso"] = "Donna";
            ////}
            return dt_src;
        }
        public static DataTable GetIdNomeCognome(string where_clause)
        {
            //nel where non serve lo spazio alla fine e non serve il ; alla fine
            MySqlCommand cmd = new MySqlCommand("SELECT id_cliente as '#', CONCAT(nome,' ',cognome) as 'nome completo' FROM clienti "+where_clause+";", Database.conn);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        private static bool _FieldsCheck(Cliente c)
        {
            string text = "Impossibile inserire le modifiche";
            int base_text = text.Length;

            if (c.Nome == "") text += "\n-Nome non impostato";
            if (c.Cognome == "") text += "\n-Cognome non impostato";

            if (text.Length > base_text) {
                MessageBox.Show(text, "Errore", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            } else {
                return true;
            }
        }
        private static Cliente _GetCliente(DataRow dr)
        {
            Cliente u = new Cliente();
            u.PKCliente = Convert.ToInt16(dr[0]);
            u.Nome = DBGet.GetString(dr[1]);
            u.Cognome = DBGet.GetString(dr[2]);
            u.Sesso = DBGet.GetInt(dr[3]);
            u.CodiceFiscale = DBGet.GetString(dr[4]);
            u.CittaNascita = DBGet.GetString(dr[5]);
            u.DataNascita = DBGet.GetDateTime(dr[6]);
            u.Telefono = DBGet.GetString(dr[7]);
            u.Email = DBGet.GetString(dr[8]);
            u.DataIscrizione = DBGet.GetDateTime(dr[9]);
            u.FKStato = DBGet.GetInt(dr[10]);
            u.Note = DBGet.GetString(dr[11]);
            u.Immagine = DBGet.GetBytes(dr[12]); //(!dr.IsDBNull(12)) ? (byte[])dr[12] : null;
            u.FKIstruttore = DBGet.GetInt(dr[13]);
            return u;
        } 
    }



    public static class EserciziController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_one",         "SELECT * FROM esercizi WHERE id_esercizio = {0} ORDER BY nome;" },
            { "select_all",         "SELECT * FROM esercizi ORDER BY nome;" },
            { "select_all_by_cat",  "SELECT * FROM esercizi WHERE fk_id_categoria = {0} ORDER BY nome;" },
            { "select_all_no_cat",  "SELECT * FROM esercizi WHERE fk_id_categoria IS NULL ORDER BY nome;" },
            { "select_search_name", "SELECT * FROM esercizi WHERE UPPER(nome) like '%{0}%' ORDER BY nome;" },
            { "insert",             "INSERT INTO esercizi VALUES(DEFAULT, @nome, @diff, @descr, @img,  @id_cat); SELECT LAST_INSERT_ID();" },
            { "update",             "UPDATE esercizi SET nome = @nome, difficolta = @diff, descrizione = @descr, immagine=@img, fk_id_categoria = @id_cat WHERE id_esercizio =  @id_es;" },
            { "delete",             "DELETE FROM esercizi WHERE id_esercizio = {0};" },
            { "count_es",           "SELECT COUNT(*) FROM esercizi;" },
            { "sel_es_show_cat",    @"SELECT id_esercizio as '#', esercizi.nome, esercizi.difficolta, categorie_esercizi.nome as 'categoria' 
                                    FROM esercizi INNER JOIN categorie_esercizi ON esercizi.fk_id_categoria = categorie_esercizi.id_categoria 
                                    ORDER BY categorie_esercizi.id_categoria, esercizi.nome;"
            }
        };
        public static Esercizio Seleziona(int id_esercizio)
        {
            Esercizio e = null;
            string query = string.Format(queries["select_one"], id_esercizio);
            DataTable dt = Factory.Select(query);
            if (dt.Rows.Count > 0)
                e = _GetEsercizio(dt.Rows[0]);
            return e;
        }
        public static int Inserisci(Esercizio e)
        {
            if (!_FieldsCheck(e))
                return -1;

            MySqlCommand cmd = new MySqlCommand(queries["insert"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(e.Nome));
            cmd.Parameters.AddWithValue("@diff", DBSet.SetInt(e.Difficolta));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(e.Descrizione));
            cmd.Parameters.AddWithValue("@img", DBSet.SetBytes(e.Immagine));
            cmd.Parameters.AddWithValue("@id_cat", DBSet.SetInt(e.Categoria.PKCategoria));
            return Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Modifica(Esercizio e)
        {
            if (!_FieldsCheck(e))
                return -1;

            MySqlCommand cmd = new MySqlCommand(queries["update"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(e.Nome));
            cmd.Parameters.AddWithValue("@diff", DBSet.SetInt(e.Difficolta));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(e.Descrizione));
            cmd.Parameters.AddWithValue("@img", DBSet.SetBytes(e.Immagine));
            cmd.Parameters.AddWithValue("@id_cat", DBSet.SetInt(e.Categoria.PKCategoria));
            cmd.Parameters.AddWithValue("@id_es", e.PKEsercizio);
            return Factory.ExecuteNonQuery(cmd);
        }
        public static int InsertUpdate(Esercizio e)
        {
            if (!_FieldsCheck(e))
                return -1;

            string q = (e.PKEsercizio > 0)
                ? queries["update"]
                : queries["insert"];

            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(e.Nome));
            cmd.Parameters.AddWithValue("@diff", DBSet.SetInt(e.Difficolta));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(e.Descrizione));
            cmd.Parameters.AddWithValue("@img", DBSet.SetBytes(e.Immagine));
            cmd.Parameters.AddWithValue("@id_cat", DBSet.SetInt(e.Categoria.PKCategoria));
            if (e.PKEsercizio > 0)
                cmd.Parameters.AddWithValue("@id_es", e.PKEsercizio);

            return (e.PKEsercizio > 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt32(Factory.ExecuteScalar(cmd));
        }

        public static int Elimina(int id_esercizio)
        {
            string query = string.Format(queries["delete"], id_esercizio);
            return Factory.ExecuteNonQuery(query);
        }

        public static int NumeroEsercizi()
        {
            return Convert.ToInt16(Factory.ExecuteScalar(queries["count_es"]));
        }
        public static DataTable GetTableEsercizi()
        {
            return Factory.Select(queries["sel_es_show_cat"]);
        }
        public static List<Esercizio> GetListEsercizi()
        {
            List<Esercizio> esercizi = new List<Esercizio>();
            DataTable dt = Factory.Select(queries["select_all"]);
            foreach (DataRow dr in dt.Rows)
                esercizi.Add(_GetEsercizio(dr));
            return esercizi;
        }
        public static List<Esercizio> GetListEserciziCategoria(int id_categoria)
        {
            List<Esercizio> esercizi = new List<Esercizio>();

            string q = (id_categoria > 0)
                ? string.Format(queries["select_all_by_cat"], id_categoria)
                : queries["select_all_no_cat"];

            DataTable dt = Factory.Select(string.Format(q,id_categoria));
            foreach (DataRow dr in dt.Rows)
                esercizi.Add(_GetEsercizio(dr));
            return esercizi;
        }
        public static DataTable GetTableEserciziCategoria(int id_cat)
        {
            string query = (id_cat > 0)
                ? string.Format(queries["select_all"], id_cat)
                : queries["select_all_no_cat"];
            return Factory.Select(query);
        }
        public static List<Esercizio> GetListCercaEsercizi(string nome)
        {
            List<Esercizio> esercizi = new List<Esercizio>();
            string q = string.Format(queries["select_search_name"], nome.ToUpper());
            DataTable dt = Factory.Select(q);
            foreach (DataRow dr in dt.Rows)
                esercizi.Add(_GetEsercizio(dr));
            return esercizi;
        }
        public static DataTable GetTableCercaEsercizi(string nome)
        {
            string query = string.Format(queries["select_search_name"], nome.ToUpper());
            return Factory.Select(query);
        }
        public static byte[] GetByteImage(int id)
        {
            string query = "SELECT immagine FROM esercizi WHERE id_esercizio=" + id;
            return  DBGet.GetBytes(Factory.ExecuteScalar(query));
        }
        private static bool _FieldsCheck(Esercizio e)
        {
            string text = "Impossibile inserire le modifiche";
            int base_text = text.Length;

            if (e.Nome == "")
                text += "\n-Nome non impostato";

            if (text.Length > base_text)
            {
                MessageBox.Show(text,"Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }

        }
        private static Esercizio _GetEsercizio(DataRow dr)
        {
            Esercizio e = new Esercizio();
            e.PKEsercizio = Convert.ToInt16(dr[0]);
            e.Nome = DBGet.GetString(dr[1]);
            e.Difficolta = DBGet.GetInt(dr[2]);
            e.Descrizione = DBGet.GetString(dr[3]);
            e.Immagine = DBGet.GetBytes(dr[4]);
            e.Categoria = (!dr.IsNull(5)) ? CategorieEserciziController.Seleziona(Convert.ToInt16(dr[5])) : new CategoriaEsercizio();
            return e;
        }
    }



    public static class IstruttoriController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_one",     "SELECT * FROM istruttori WHERE id_istruttore = {0}" },
            { "sel_all",        "SELECT * FROM istruttori;" },
            { "insert",         "INSERT INTO istruttori VALUES(DEFAULT, @nome, @cogn, @sesso, @dn, @cn, @tel, @email, @pw, @permessi, @img);" },
            { "update",         "UPDATE istruttori SET nome=@nome, cognome=@cogn, sesso=@sesso, data_nascita=@dn, citta_nascita=@cn, telefono=@tel, email=@email, password=@pw, fk_id_livello_permesso=@per, immagine=@img WHERE id_istruttore=@id"},
            { "delete",         "DELETE FROM istruttori WHERE id_istruttore = {0}" },
            { "select_pwd",     "SELECT password FROM istruttori WHERE id_istruttore = {0};" },
            { "insert_accesso", "INSERT INTO istruttori_accessi VALUES(NOW(), {0});" },
            { "select_dt",      @"SELECT id_istruttore as '#', CONCAT(istruttori.nome,' ',istruttori.cognome) as 'Nome', istruttori_livelli_permessi.nome as 'livello permesso' 
                                FROM istruttori LEFT JOIN istruttori_livelli_permessi ON istruttori.fk_id_livello_permesso = istruttori_livelli_permessi.id_livello_permesso "
            }
        };
        public static DataTable SelezionaDT_old(int id_istruttore)
        {
            string query = string.Format(queries["select_one"], id_istruttore);
            return Factory.Select(query);
        }
        public static Istruttore Seleziona(int id_istruttore)
        {
            Istruttore i = null;
            string query = string.Format(queries["select_one"], id_istruttore);
            DataTable dt = Factory.Select(query);
            if (dt.Rows.Count > 0)
            {
                i = _GetIstruttore(dt.Rows[0]);
            }
            return i;
        }
        public static Istruttore _GetIstruttore(DataRow dr)
        {
            Istruttore i = new Istruttore();
            i.PKIstruttore = Convert.ToInt16(dr[0]);
            i.Nome = DBGet.GetString(dr[1]);
            i.Cognome = DBGet.GetString(dr[2]);
            i.Sesso = DBGet.GetInt(dr[3]);
            i.DataNascita = DBGet.GetDateTime(dr[4]);
            i.Citta = DBGet.GetString(dr[5]);
            i.Telefono = DBGet.GetString(dr[6]);
            i.Email = DBGet.GetString(dr[7]);
            i.Password = DBGet.GetString(dr[8]);
            i.FKLivelliPermessi = DBGet.GetInt(dr[9]);
            i.Immagine = DBGet.GetBytes(dr[10]);
            return i;
        }
        public static int Modifica(Istruttore i)
        {
            MySqlCommand cmd = new MySqlCommand(queries["update"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", i.Nome);
            cmd.Parameters.AddWithValue("@cogn", i.Cognome);
            cmd.Parameters.AddWithValue("@sesso", i.Sesso);
            cmd.Parameters.AddWithValue("@dn", i.DataNascita);
            cmd.Parameters.AddWithValue("@cn", i.Citta);
            cmd.Parameters.AddWithValue("@tel", i.Telefono);
            cmd.Parameters.AddWithValue("@email", i.Email);
            cmd.Parameters.AddWithValue("@pw", i.Password);
            cmd.Parameters.AddWithValue("@per", i.FKLivelliPermessi);
            cmd.Parameters.AddWithValue("@img", i.Immagine);
            cmd.Parameters.AddWithValue("@id", i.PKIstruttore);
            return Factory.ExecuteNonQuery(cmd);
        }
        public static int Elimina(int id_istruttore)
        {
            string query = string.Format(queries["delete"], id_istruttore);
            return Factory.ExecuteNonQuery(query);
        }
        public static int Inserisci(Istruttore i)
        {
            MySqlCommand cmd = new MySqlCommand(queries["insert"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(i.Nome));
            cmd.Parameters.AddWithValue("@cogn", DBSet.SetString(i.Cognome));
            cmd.Parameters.AddWithValue("@sesso", DBSet.SetInt(i.Sesso));
            cmd.Parameters.AddWithValue("@cn", DBSet.SetString(i.Citta));
            cmd.Parameters.AddWithValue("@dn", DBSet.SetDateTime(i.DataNascita));
            cmd.Parameters.AddWithValue("@tel", DBSet.SetString(i.Telefono));
            cmd.Parameters.AddWithValue("@email", DBSet.SetString(i.Email));
            cmd.Parameters.AddWithValue("@pw", DBSet.SetString(i.Password));
            cmd.Parameters.AddWithValue("@permessi", DBSet.SetInt(i.FKLivelliPermessi));
            cmd.Parameters.AddWithValue("@img", DBSet.SetBytes(i.Immagine));
            return Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static bool CheckCredenziali(int id_istruttore, string pw)
        {
            string query = string.Format(queries["select_pwd"], id_istruttore);
            string right_pw = Convert.ToString(Factory.ExecuteScalar(query));
            if (pw == right_pw)
            {
                //InserisciAccesso(id_istruttore);
                return true;
            }
            else
            {
                return false;
            }
        }     
        private static void InserisciAccesso(int id)
        {
            string query = string.Format(queries["insert_accesso"], id);
            Factory.ExecuteNonQuery(query);
        }
        public static DataTable GetTableIstruttori_old()
        {
            string query = "SELECT id_istruttore as '#', CONCAT(nome,' ',cognome) as 'istruttore' FROM istruttori;";
            return Factory.Select(query);
        }
        public static List<Istruttore> GetListIstruttori()
        {
            List<Istruttore> istruttori = new List<Istruttore>();
            foreach (DataRow dr in Factory.Select(queries["sel_all"]).Rows)
                istruttori.Add(_GetIstruttore(dr));
            return istruttori;
        }
        public static List<Istruttore> GetListElencoIstruttori_old(int id_istruttore_escluso)
        {
            try
            {
                List<Istruttore> istruttori = new List<Istruttore>();
                string query = "SELECT id_istruttore as '#', nome, cognome FROM istruttori WHERE id_istruttore != " + id_istruttore_escluso;
                MySqlCommand cmd = new MySqlCommand(query, Database.conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    istruttori.Add(new Istruttore()
                    {
                        PKIstruttore = dr.GetInt16("#"),
                        Nome = dr.GetString("nome"),
                        Cognome = dr.GetString("cognome")
                    });
                }
                dr.Close();
                return istruttori;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Impossibile caricare la lista:\n" + ex.Message, "Istruttori", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Istruttore>();
            }
        }
        public static DataTable GetTableIstruttori(int? id_istruttore_escluso)
        {
            //se id è nullo ottiene tutti, altrimenti esclude quello specificato dalla select
            string sql = (id_istruttore_escluso == (int?)null) 
                ? queries["select_dt"] + ";"
                : queries["select_dt"] + " WHERE id_istruttore <> " + id_istruttore_escluso + ";";

            return Factory.Select(sql);

            
        }
        public static byte[] GetByteImage(int id_istruttore)
        {
            string query = "SELECT immagine FROM istruttori WHERE id_istruttore=" + id_istruttore;
            return (byte[])Factory.ExecuteScalar(query);
        }
    }


    
    public static class LivelliPermessiController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_all",     "SELECT * FROM istruttori_livelli_permessi" },
            { "select_by_id",   "SELECT * FROM istruttori_livelli_permessi WHERE id_livello_permesso = {0};" },
            { "insert",         "INSERT INTO istruttori_livelli_permessi VALUES(DEFAULT, @nome, @descr, @anamn_cud_s, @anamn_ud_o, @anamn_r, @avv_cud_s, @avv_ud_o, @avv_r, @avv_su, @cli_cud_s, @cli_ud_o, @cli_r, @istr_ud_s, @istr_cud_o, @istr_r, @istr_ss, @sch_cud_s, @sch_ud_o, @sch_r, @tst_cud_s, @tst_ud_o, @tst_r); SELECT LAST_INSERT_ID();" },
            { "update",         "UPDATE istruttori_livelli_permessi SET NOME=@nome, DESCRIZIONE=@descr, ANAMNESI_CUD_SELF=@anamn_cud_s, ANAMNESI_UD_OTHER=@anamn_ud_o, ANAMNESI_R=@anamn_r, AVVISI_CUD_SELF=@avv_cud_s, AVVISI_UD_OTHER=@avv_ud_o, AVVISI_R=@avv_r, AVVISI_SET_URGENT=@avv_su, CLIENTI_CUD_SELF=@cli_cud_s, CLIENTI_UD_OTHER=@cli_ud_o, CLIENTI_R=@cli_r, ISTRUTTORI_UD_SELF=@istr_ud_s, ISTRUTTORI_CUD_OTHER=@istr_cud_o, ISTRUTTORI_R=@istr_r, ISTRUTTORI_SHOW_SETTINGS=@istr_ss, SCHEDE_CUD_SELF=@sch_cud_s, SCHEDE_UD_OTHER=@sch_ud_o, SCHEDE_R=@sch_r, TEST_CUD_SELF=@tst_cud_s, TEST_UD_OTHER=@tst_ud_o, TEST_R=@tst_r WHERE ID_LIVELLO_PERMESSO=@id;" },
            { "delete",         "DELETE FROM istruttori_livelli_permessi WHERE id_livello_permesso={0};"}
        };
        public static LivelloPermesso Seleziona(int id_permesso)
        {
            string q = string.Format(queries["select_by_id"], id_permesso);
            LivelloPermesso lp = null;
            DataTable dt = Factory.Select(q);
            if (dt.Rows.Count > 0)
                lp = _GetLivello(dt.Rows[0]);
            return lp;
        }
        public static List<LivelloPermesso> GetListPermessi()
        {
            List<LivelloPermesso> livelli = new List<LivelloPermesso>();
            foreach (DataRow dr in Factory.Select(queries["select_all"]).Rows)
                livelli.Add(_GetLivello(dr));
            return livelli;
        }
        public static int InsertUpdate(LivelloPermesso lp)
        {
            string q = (lp.PKLivelloPermesso > 0)
                ? queries["update"]
                : queries["insert"];
            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            if (lp.PKLivelloPermesso > 0)
                cmd.Parameters.AddWithValue("@id", lp.PKLivelloPermesso);
            cmd.Parameters.AddWithValue("@nome", DBSet.SetString(lp.Nome));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(lp.Descrizione));
            cmd.Parameters.AddWithValue("@anamn_cud_s", DBSet.SetBoolean(lp.ANAMNESI_CUD_SELF));
            cmd.Parameters.AddWithValue("@anamn_ud_o", DBSet.SetBoolean(lp.ANAMNESI_UD_OTHER));
            cmd.Parameters.AddWithValue("@anamn_r", DBSet.SetBoolean(lp.ANAMNESI_R));
            cmd.Parameters.AddWithValue("@avv_cud_s", DBSet.SetBoolean(lp.AVVISI_CUD_SELF));
            cmd.Parameters.AddWithValue("@avv_ud_o", DBSet.SetBoolean(lp.AVVISI_UD_OTHER));
            cmd.Parameters.AddWithValue("@avv_r", DBSet.SetBoolean(lp.AVVISI_R));
            cmd.Parameters.AddWithValue("@avv_su", DBSet.SetBoolean(lp.AVVISI_SET_URGENT));
            cmd.Parameters.AddWithValue("@cli_cud_s", DBSet.SetBoolean(lp.CLIENTI_CUD_SELF));
            cmd.Parameters.AddWithValue("@cli_ud_o", DBSet.SetBoolean(lp.CLIENTI_UD_OTHER));
            cmd.Parameters.AddWithValue("@cli_r", DBSet.SetBoolean(lp.CLIENTI_R));
            cmd.Parameters.AddWithValue("@istr_ud_s", DBSet.SetBoolean(lp.ISTRUTTORI_UD_SELF));
            cmd.Parameters.AddWithValue("@istr_cud_o", DBSet.SetBoolean(lp.ISTRUTTORI_CUD_OTHER));
            cmd.Parameters.AddWithValue("@istr_r", DBSet.SetBoolean(lp.ISTRUTTORI_R));
            cmd.Parameters.AddWithValue("@istr_ss", DBSet.SetBoolean(lp.ISTRUTTORI_SHOW_SETTINGS));
            cmd.Parameters.AddWithValue("@sch_cud_s", DBSet.SetBoolean(lp.SCHEDE_CUD_SELF));
            cmd.Parameters.AddWithValue("@sch_ud_o", DBSet.SetBoolean(lp.SCHEDE_UD_OTHER));
            cmd.Parameters.AddWithValue("@sch_r", DBSet.SetBoolean(lp.SCHEDE_R));
            cmd.Parameters.AddWithValue("@tst_cud_s", DBSet.SetBoolean(lp.TEST_CUD_SELF));
            cmd.Parameters.AddWithValue("@tst_ud_o", DBSet.SetBoolean(lp.TEST_UD_OTHER));
            cmd.Parameters.AddWithValue("@tst_r", DBSet.SetBoolean(lp.TEST_R));

            return (lp.PKLivelloPermesso > 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Delete(int id)
        {
            string q = string.Format(queries["delete"], id);
            return Factory.ExecuteNonQuery(q);
        }
        private static LivelloPermesso _GetLivello(DataRow dr)
        {
            LivelloPermesso lp = new LivelloPermesso();
            lp.PKLivelloPermesso = Convert.ToInt16(dr["ID_LIVELLO_PERMESSO"]);
            lp.Nome = Convert.ToString(dr["NOME"]);
            lp.Descrizione = Convert.ToString(dr["DESCRIZIONE"]);
            lp.ANAMNESI_CUD_SELF = (Convert.ToBoolean(dr["ANAMNESI_CUD_SELF"]));
            lp.ANAMNESI_UD_OTHER = (Convert.ToBoolean(dr["ANAMNESI_UD_OTHER"]));
            lp.ANAMNESI_R = (Convert.ToBoolean(dr["ANAMNESI_R"]));
            lp.AVVISI_CUD_SELF = (Convert.ToBoolean(dr["AVVISI_CUD_SELF"]));
            lp.AVVISI_UD_OTHER = (Convert.ToBoolean(dr["AVVISI_UD_OTHER"]));
            lp.AVVISI_R = (Convert.ToBoolean(dr["AVVISI_R"]));
            lp.AVVISI_SET_URGENT = (Convert.ToBoolean(dr["AVVISI_SET_URGENT"]));
            lp.CLIENTI_CUD_SELF = (Convert.ToBoolean(dr["CLIENTI_CUD_SELF"]));
            lp.CLIENTI_UD_OTHER = (Convert.ToBoolean(dr["CLIENTI_UD_OTHER"]));
            lp.CLIENTI_R = (Convert.ToBoolean(dr["CLIENTI_R"]));
            lp.ISTRUTTORI_UD_SELF = (Convert.ToBoolean(dr["ISTRUTTORI_UD_SELF"]));
            lp.ISTRUTTORI_CUD_OTHER = (Convert.ToBoolean(dr["ISTRUTTORI_CUD_OTHER"]));
            lp.ISTRUTTORI_R = (Convert.ToBoolean(dr["ISTRUTTORI_R"]));
            lp.ISTRUTTORI_SHOW_SETTINGS = (Convert.ToBoolean(dr["ISTRUTTORI_SHOW_SETTINGS"]));
            lp.SCHEDE_CUD_SELF = (Convert.ToBoolean(dr["SCHEDE_CUD_SELF"]));
            lp.SCHEDE_UD_OTHER = (Convert.ToBoolean(dr["SCHEDE_UD_OTHER"]));
            lp.SCHEDE_R = (Convert.ToBoolean(dr["SCHEDE_R"]));
            lp.TEST_CUD_SELF = (Convert.ToBoolean(dr["TEST_CUD_SELF"]));
            lp.TEST_UD_OTHER = (Convert.ToBoolean(dr["TEST_UD_OTHER"]));
            lp.TEST_R = (Convert.ToBoolean(dr["TEST_R"]));
            return lp;
        }
    }



    public static class SchedeController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_by_id",                   "SELECT * FROM schede WHERE id_scheda = {0};" },
            { "select_by_cat",                  "SELECT * FROM schede WHERE fk_id_categoria_scheda = {0} ORDER BY nome;" },
            { "select_by_cli",                  "SELECT * FROM schede WHERE fk_id_cliente = {0};" },
            { "select_no_cat",                  "SELECT * FROM schede WHERE fk_id_categoria_scheda IS NULL ORDER BY nome;" },
            { "select_libere",                  "SELECT * FROM schede WHERE schede.fk_id_cliente IS NULL AND is_model = 0;" },
            { "select_modelli",                 "SELECT * FROM schede WHERE is_model = 1;" },        
            { "insert",                         "INSERT INTO schede VALUES(DEFAULT, @nome, @obiet, @diff, @freq, @n_sed, @dett, @is_mod, @id_istr, NOW(), @id_cli, @d_inizio, @d_fine, @id_cat_sch); SELECT LAST_INSERT_ID();" },
            { "update",                         "UPDATE schede SET NOME=@nome, OBIETTIVO=@obiet, DIFFICOLTA=@diff, FREQUENZA_SETTIMANALE=@freq, NUMERO_SEDUTE=@n_sed, DETTAGLI=@dett, IS_MODEL=@is_mod, FK_ID_ISTRUTTORE=@id_istr, FK_ID_CLIENTE=@id_cli, DATA_INIZIO=@d_inizio, DATA_FINE=@d_fine, FK_ID_CATEGORIA_SCHEDA=@id_cat_sch WHERE ID_SCHEDA=@id_scheda;" },
            { "delete",                         "DELETE FROM schede WHERE id_scheda = {0};" }
        };
        
        public static Scheda SelezionaSchedaCompleta(int id_scheda)
        {
            Scheda scheda = null;
            DataTable dt = Factory.Select(string.Format(queries["select_by_id"],id_scheda));
            if(dt.Rows.Count > 0)
            {
                //caricamento dati scheda
                scheda = _GetSchedaDati(dt.Rows[0]);
                //caricamento sedute (dentro carica gia gli esercizi)
                scheda.Sedute = SeduteController.GetListSeduteScheda(id_scheda);
            }
            return scheda;

        }

        public static int CreaSchedaCompleta(Scheda s)
        {
            MySqlTransaction trans = Database.conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(queries["insert"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", s.Nome);
            cmd.Parameters.AddWithValue("@obiet", DBSet.SetString(s.Obiettivo));
            cmd.Parameters.AddWithValue("@diff", DBSet.SetInt(s.Difficolta));
            cmd.Parameters.AddWithValue("@freq", DBSet.SetInt(s.FrequenzaSettimanale));
            cmd.Parameters.AddWithValue("@n_sed", DBSet.SetInt(s.NumeroSedute));
            cmd.Parameters.AddWithValue("@dett", DBSet.SetString(s.Dettagli));
            cmd.Parameters.AddWithValue("@is_mod", DBSet.SetBoolean(s.IsModel));
            cmd.Parameters.AddWithValue("@id_istr", DBSet.SetInt(s.FKIstruttore));
            cmd.Parameters.AddWithValue("@id_cli", DBSet.SetInt(s.FKCliente));
            cmd.Parameters.AddWithValue("@d_inizio", DBSet.SetDateTime(s.DataInizio));
            cmd.Parameters.AddWithValue("@d_fine", DBSet.SetDateTime(s.DataFine));
            cmd.Parameters.AddWithValue("@id_cat_sch", DBSet.SetInt(s.FKCategoriaScheda));
            s.PKScheda = Convert.ToInt16(Factory.ExecuteScalar(cmd));    //inserisci ed ottengo l'id della scheda

            int sed_ordine = 0;
            int es_ordine = 0;
            if (s.PKScheda > 0)
            {
                foreach (Seduta sed in s.Sedute)                        //scorre le sedute
                {
                    sed.Ordine = sed_ordine;
                    sed.FKScheda = s.PKScheda;                          //assegno alla seduta l'id della scheda
                    if (sed.PKSeduta > 0) {
                        SeduteController.Update(sed);                      //se la pk vale piu di 0 si deve aggiornare il record
                    } else {
                        sed.PKSeduta = SeduteController.Insert(sed);       //altrimenti l'id diventa quello dato dall'insert
                    }

                    if(sed.PKSeduta > 0)                                //Se ha un id valido
                    {
                        foreach (EsercizioSeduta es in sed.Esercizi) {
                            es.Ordine = es_ordine;
                            es.FKSeduta = sed.PKSeduta;                 //assegno all'esercizio la fk della seduta
                            EserciziSeduteController.InsertUpdate(es);     //modifica dell'esercizio
                            es_ordine++;
                        }
                    }
                    sed_ordine++;
                }
            }       

            trans.Commit();
            return s.PKScheda;
        }
        public static int ModificaSchedaCompleta(Scheda s)
        {
            MySqlTransaction tran = Database.conn.BeginTransaction();

            // 1. modifica dati scheda
            MySqlCommand cmd = new MySqlCommand(queries["update"], Database.conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id_scheda", s.PKScheda);
            cmd.Parameters.AddWithValue("@nome", s.Nome);
            cmd.Parameters.AddWithValue("@obiet", s.Obiettivo);
            cmd.Parameters.AddWithValue("@diff", s.Difficolta);
            cmd.Parameters.AddWithValue("@freq", s.FrequenzaSettimanale);
            cmd.Parameters.AddWithValue("@n_sed", s.NumeroSedute);
            cmd.Parameters.AddWithValue("@dett", s.Dettagli);
            cmd.Parameters.AddWithValue("@is_mod", s.IsModel);
            cmd.Parameters.AddWithValue("@id_istr", s.FKIstruttore);
            cmd.Parameters.AddWithValue("@id_cli", s.FKCliente);
            cmd.Parameters.AddWithValue("@d_inizio", s.DataInizio);
            cmd.Parameters.AddWithValue("@d_fine", s.DataFine);
            cmd.Parameters.AddWithValue("@id_cat_sch", s.FKCategoriaScheda);
            Factory.ExecuteNonQuery(cmd);

            // 2. eliminazione vecchie sedute (a cascata gli esercizi) per evitare problemi
            SeduteController.DeleteSeduteScheda(s.PKScheda);
            
            // 3. reinserimento sedute
            int sed_ordine = 0;
            int es_ordine = 0;
            foreach (Seduta sed in s.Sedute)
            {
                sed.Ordine = sed_ordine;
                sed.PKSeduta = SeduteController.Insert(sed);

                // 4. reinserimento esercizi
                foreach (EsercizioSeduta es in sed.Esercizi)
                {
                    es.Ordine = es_ordine;
                    es.FKSeduta = sed.PKSeduta;
                    EserciziSeduteController.Insert(es);
                    es_ordine++;
                }
                sed_ordine++;
            }
            tran.Commit();
            return s.PKScheda;
        }
        public static int Elimina(int id_scheda)
        {
            string query = string.Format(queries["delete"], id_scheda);
            return Factory.ExecuteNonQuery(query);

        }

        public static DataTable GetTableSchedeCategoria(int id_categoria)
        {
            string q = (id_categoria > 0)
                ? string.Format(queries["select_by_cat"], id_categoria)
                : queries["select_no_cat"];
            return Factory.Select(q);
        }
        public static DataTable GetTableSchedeLibere()
        {
            return Factory.Select(queries["select_libere"]);
        }
        public static DataTable GetTableSchedeModelli()
        {
            return Factory.Select(queries["select_modelli"]);
        }
        public static DataTable GetTableSchedeCliente(int id_cliente)
        {
            string q = string.Format(queries["select_by_cli"], id_cliente);
            return Factory.Select(q);
        }
        public static List<Scheda> GetListSchedeCategoria(int id_categoria)
        {
            List<Scheda> schede = new List<Scheda>();

            string q = (id_categoria > 0)
                ? string.Format(queries["select_by_cat"], id_categoria)
                : queries["select_no_cat"];
           
            DataTable dt = Factory.Select(q);
            if(dt.Rows.Count > 0)
                foreach (DataRow dr in dt.Rows)
                    schede.Add(_GetSchedaDati(dr));
            return schede;
        }

        private static Scheda _GetSchedaDati(DataRow dr)
        {
            return new Scheda()
            {
                PKScheda = Convert.ToInt16(dr[0]),
                Nome = DBGet.GetString(dr[1]),
                Obiettivo = DBGet.GetString(dr[2]),
                Difficolta = DBGet.GetInt(dr[3]),
                FrequenzaSettimanale = DBGet.GetInt(dr[4]),
                NumeroSedute = DBGet.GetInt(dr[5]),
                Dettagli = DBGet.GetString(dr[6]),
                IsModel = DBGet.GetBoolean(dr[7]),
                FKIstruttore = DBGet.GetInt(dr[8]),
                DataInserimento = DBGet.GetDateTime(dr[9]),
                FKCliente = DBGet.GetInt(dr[10]),
                DataInizio = DBGet.GetDateTime(dr[11]),
                DataFine = DBGet.GetDateTime(dr[12]),
                FKCategoriaScheda = DBGet.GetInt(dr[13])
            };
        }
        private static bool _FieldsCheck(Scheda s)
        {
            string text = "Impossibile inserire le modifiche";
            int base_text = text.Length;

            if (s.Nome == "") text += "\n-Nome non impostato";

            if (text.Length > base_text) {
                MessageBox.Show(text, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } else {
                return true;
            }
        }
    }



    public static class SeduteController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_seduta",              "SELECT * FROM SEDUTE WHERE ID_SEDUTA = {0};" },
            { "select_sedute_by_scheda",    "SELECT * FROM SEDUTE WHERE FK_ID_SCHEDA = {0};" },
            { "insert_seduta",              "INSERT INTO sedute VALUES(DEFAULT, @id_scheda, @nome, @ordine, @descr); SELECT LAST_INSERT_ID();" },
            { "update_seduta",              "UPDATE sedute SET FK_ID_SCHEDA=@id_scheda, NOME=@nome, ORDINE=@ordine, DESCRIZIONE=@descr WHERE ID_SEDUTA=@id_sed;" },
            { "delete",                     "DELETE FROM sedute WHERE id_seduta = {0};" },
            { "delete_scheda",              "DELETE FROM sedute WHERE fk_id_scheda = {0};" }
        };
        public static Seduta Seleziona(int id_seduta)
        {
            Seduta s = null;
            string q = string.Format(queries["select_seduta"], id_seduta);
            DataTable dt = Factory.Select(q);
            if (dt.Rows.Count > 0)
            {
                //caricamento seduta
                s = _GetSeduta(dt.Rows[0]);
                //caricamento lista esercizi seduta
                s.Esercizi = EserciziSeduteController.GetListEserciziSeduta(s.PKSeduta);
            }   
            return s;
        }
        public static int Insert(Seduta sed)
        {
            MySqlCommand cmd = new MySqlCommand(queries["insert_seduta"], Database.conn);
            cmd.Parameters.AddWithValue("@id_scheda", sed.FKScheda);
            cmd.Parameters.AddWithValue("@nome", sed.Nome);
            cmd.Parameters.AddWithValue("@ordine", sed.Ordine);
            cmd.Parameters.AddWithValue("@descr", sed.Descrizione);
            return Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Update(Seduta sed)
        {
            MySqlCommand cmd = new MySqlCommand(queries["update_seduta"], Database.conn);
            cmd.Parameters.AddWithValue("@id_scheda", sed.FKScheda);
            cmd.Parameters.AddWithValue("@nome", sed.Nome);
            cmd.Parameters.AddWithValue("@ordine", sed.Ordine);
            cmd.Parameters.AddWithValue("@descr", sed.Descrizione);
            cmd.Parameters.AddWithValue("@id_sed", sed.PKSeduta);
            return Factory.ExecuteNonQuery(cmd);
        }
        public static int Delete(int id_seduta)
        {
            string q = string.Format(queries["delete"], id_seduta);
            return Factory.ExecuteNonQuery(q);
        }
        public static List<Seduta> GetListSeduteScheda(int id_scheda)
        {
            List<Seduta> sedute = new List<Seduta>();
            string q = string.Format(queries["select_sedute_by_scheda"], id_scheda);
            DataTable dt = Factory.Select(q);
            if(dt.Rows.Count > 0)
            {
                int progr_sed = 0;
                foreach(DataRow dr in dt.Rows)
                {
                    Seduta sed = Seleziona(Convert.ToInt16(dr[0]));
                    sed.Ordine = progr_sed;
                    sedute.Add(sed);
                    progr_sed++;
                }   
            }
            return sedute;
        }
        public static int DeleteSeduteScheda(int id_scheda)
        {
            string q = string.Format(queries["delete_scheda"], id_scheda);
            return  Convert.ToInt16(Factory.ExecuteScalar(q));
        }
        private static Seduta _GetSeduta(DataRow dr)
        {
            Seduta s = new Seduta();
            s.PKSeduta = Convert.ToInt16(dr[0]);
            s.FKScheda = Convert.ToInt16(dr[1]);
            s.Nome = DBGet.GetString(dr[2]);
            s.Ordine = Convert.ToInt16(dr[3]);
            s.Descrizione = DBGet.GetString(dr[4]);
            return s;
        }
    }



    public static class EserciziSeduteController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_esercizi_by_seduta",  "SELECT * from esercizi_sedute where fk_id_seduta = {0};" },
            { "select_esercizio_seduta",    "SELECT * from esercizi_sedute where id_es = {0};" },
            { "insert",                     "INSERT INTO esercizi_sedute VALUES(DEFAULT, @id_sed, @id_ese, @serie, @reps, @rec, @car, @note, @ordine, @a_tempo, @durata, @metodo, @gruppo);"},
            { "update",                     "UPDATE esercizi_sedute SET FK_ID_SEDUTA=@id_sed, FK_ID_ESERCIZIO=@id_ese, SERIE=@serie, RIPETIZIONI=@reps, RECUPERO=@rec, CARICO=@car, NOTE=@note, ORDINE=@ordine, A_TEMPO=@a_tempo, DURATA=@durata, METODO=@metodo, GRUPPO=@gruppo WHERE ID_ES=@id_es" },
            { "delete",                     "DELETE FROM esercizi_sedute WHERE id_es = {0};" },
            { "select_distinct_reps",       "SELECT DISTINCT ripetizioni FROM esercizi_sedute;" }
        };
        public static EsercizioSeduta Seleziona(int id_es)
        {
            string q = string.Format(queries["select_esercizio_seduta"], id_es);
            EsercizioSeduta es = null;
            DataTable dt = Factory.Select(q);
            if (dt.Rows.Count > 0)
                es = _GetEsercizioSeduta(dt.Rows[0]);
            return es;
        }
        public static int InsertUpdate(EsercizioSeduta es)
        {
            string q = (es.PKEsercizioSeduta > 0)
                ? queries["update"]
                : queries["insert"];
            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            cmd.Parameters.AddWithValue("@id_sed", es.FKSeduta);
            cmd.Parameters.AddWithValue("@id_ese", DBSet.SetInt(es.Esercizio.PKEsercizio));
            cmd.Parameters.AddWithValue("@serie", DBSet.SetInt(es.Serie));
            cmd.Parameters.AddWithValue("@reps", DBSet.SetString(es.Ripetizioni));
            cmd.Parameters.AddWithValue("@rec", DBSet.SetTimeSpan(es.Recupero));
            cmd.Parameters.AddWithValue("@car", DBSet.SetDouble(es.Carico));
            cmd.Parameters.AddWithValue("@note", DBSet.SetString(es.Note));
            cmd.Parameters.AddWithValue("@ordine", es.Ordine);
            cmd.Parameters.AddWithValue("@a_tempo", DBSet.SetBoolean(es.ATempo));
            cmd.Parameters.AddWithValue("@durata", DBSet.SetTimeSpan(es.Durata));
            cmd.Parameters.AddWithValue("@metodo", DBSet.SetInt(es.Metodo));
            cmd.Parameters.AddWithValue("@gruppo", DBSet.SetInt(es.Gruppo));
            if (es.PKEsercizioSeduta > 0)
                cmd.Parameters.AddWithValue("@id_es", es.PKEsercizioSeduta);

            return (es.PKEsercizioSeduta> 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Insert(EsercizioSeduta es)
        {
            MySqlCommand cmd = new MySqlCommand(queries["insert"], Database.conn);
            cmd.Parameters.AddWithValue("@id_sed", es.FKSeduta);
            cmd.Parameters.AddWithValue("@id_ese", DBSet.SetInt(es.Esercizio.PKEsercizio));
            cmd.Parameters.AddWithValue("@serie", DBSet.SetInt(es.Serie));
            cmd.Parameters.AddWithValue("@reps", DBSet.SetString(es.Ripetizioni));
            cmd.Parameters.AddWithValue("@rec", DBSet.SetTimeSpan(es.Recupero));
            cmd.Parameters.AddWithValue("@car", DBSet.SetDouble(es.Carico));
            cmd.Parameters.AddWithValue("@note", DBSet.SetString(es.Note));
            cmd.Parameters.AddWithValue("@ordine", es.Ordine);
            cmd.Parameters.AddWithValue("@a_tempo", DBSet.SetBoolean(es.ATempo));
            cmd.Parameters.AddWithValue("@durata", DBSet.SetTimeSpan(es.Durata));
            cmd.Parameters.AddWithValue("@metodo", DBSet.SetInt(es.Metodo));
            cmd.Parameters.AddWithValue("@gruppo", DBSet.SetInt(es.Gruppo));
            return Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Delete(int id_es)
        {
            string q = string.Format(queries["delete"], id_es);
            return Factory.ExecuteNonQuery(q);
        }
        public static List<EsercizioSeduta> GetListEserciziSeduta(int id_seduta)
        {
            List<EsercizioSeduta> eserciziSeduta = new List<EsercizioSeduta>();
            string q = string.Format(queries["select_esercizi_by_seduta"], id_seduta);
            DataTable dt = Factory.Select(q);
            if (dt.Rows.Count > 0)
            {
                int progr_es = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    EsercizioSeduta es = _GetEsercizioSeduta(dr);
                    es.Ordine = progr_es;
                    eserciziSeduta.Add(es);
                    progr_es++;
                }
            }
            return eserciziSeduta;
        }
        public static List<string> GetListRipetizioniDinstict()
        {
            List<string> serie = new List<string>();
            DataTable dt = Factory.Select(queries["select_distinct_reps"]);
            foreach (DataRow dr in dt.Rows)
                serie.Add(dr[0].ToString());
            return serie;
        }
        private static EsercizioSeduta _GetEsercizioSeduta(DataRow dr)
        {
            EsercizioSeduta es = new EsercizioSeduta();
            es.PKEsercizioSeduta = Convert.ToInt16(dr[0]);
            es.FKSeduta = Convert.ToInt16(dr[1]);
            es.Esercizio = EserciziController.Seleziona(Convert.ToInt16(dr[2]));
            es.Serie = DBGet.GetInt(dr[3]);
            es.Ripetizioni = DBGet.GetString(dr[4]);
            es.Recupero = DBGet.GetTimeSpan(dr[5]);
            es.Carico = DBGet.GetDouble(dr[6]);
            es.Note = DBGet.GetString(dr[7]);
            es.Ordine = Convert.ToInt16(dr[8]);
            es.ATempo = DBGet.GetBoolean(dr[9]);
            es.Durata = DBGet.GetTimeSpan(dr[10]);
            es.Metodo = Convert.ToInt16(dr[11]);
            es.Gruppo = DBGet.GetInt(dr[12]);
            return es;
        }
    }



    public static class TestController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "select_by_cli_obj",      @"SELECT test.* FROM test 
                                        WHERE test.fk_id_cliente = {0} 
                                        ORDER BY test.data DESC;"
            },
            { "select_by_id_obj",       "SELECT * FROM test WHERE id_test = {0};" },
            { "select_by_cli_dt",       "SELECT test.id_test as '#', esercizi.nome, test.reps as 'ripetizioni', test.carico, test.data " +
                                        "FROM test " +
                                        "INNER JOIN esercizi ON test.fk_id_esercizio = esercizi.id_esercizio " +
                                        "WHERE fk_id_cliente = {0} " +
                                        "ORDER BY test.data DESC;"
            },
            { "insert",                 "INSERT INTO test VALUES(DEFAULT, @id_cliente, @id_es, @reps, @carico, NOW()); SELECT LAST_INSERT_ID();" },
            { "update",                 "UPDATE test SET fk_id_esercizio = @id_es, reps = @reps, carico = @carico WHERE id_test = @id_test;" },
            { "delete",                 "DELETE FROM test WHERE id_test = {0};"}
        };
        public static Test Seleziona(int id_test)
        {
            Test t = null;
            string q = string.Format(queries["select_by_id_obj"], id_test);
            DataTable dt = Factory.Select(q);
            if (dt.Rows.Count > 0)
                t = _GetTest(dt.Rows[0]);
            return t;
        }
        public static int InsertUpdate(Test t)
        {
            string q = (t.PKTest > 0)
                ? queries["update"]
                : queries["insert"];
            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            if(t.PKTest > 0)
                cmd.Parameters.AddWithValue("@id_test", t.PKTest);
            cmd.Parameters.AddWithValue("@id_cliente", t.FKCliente);
            cmd.Parameters.AddWithValue("@id_es", t.FKEsercizio);
            cmd.Parameters.AddWithValue("@reps", t.Ripetizioni);
            cmd.Parameters.AddWithValue("@carico", t.Carico);

            return (t.PKTest > 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Delete(int id_test)
        {
            string query = string.Format(queries["delete"], id_test);
            return Factory.ExecuteNonQuery(query);
        }
        public static DataTable GetTableTestCliente(int id_cliente)
        {
            string query = string.Format(queries["select_by_cli_dt"], id_cliente);
            return Factory.Select(query);
        }
        public static List<Test> GetListTestCliente(int id_cliente)
        {
            List<Test> tests = new List<Test>();
            string q = string.Format(queries["select_by_cli_obj"], id_cliente);
            DataTable dt = Factory.Select(q);
            if (dt.Rows.Count > 0)
                foreach(DataRow dr in dt.Rows)
                    tests.Add(_GetTest(dt.Rows[0]));
            return tests;
        }
        private static Test _GetTest(DataRow dr)
        {
            Test t = new Test();
            t.PKTest = Convert.ToInt16(dr[0]);
            t.FKCliente = Convert.ToInt16(dr[1]);
            t.FKEsercizio = Convert.ToInt16(dr[2]);
            t.Ripetizioni = Convert.ToInt16(dr[3]);
            t.Carico = Convert.ToInt16(dr[4]);
            t.Data = Convert.ToDateTime(dr[5]);
            return t;
        }
    }


    
    public static class TipiController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "insert", "INSERT INTO {0} VALUES(DEFAULT, @val, @descr, @col); SELECT LAST_INSERT_ID();" },
            { "update", "UPDATE {0} SET VALORE=@val, DESCRIZIONE=@descr, COLORE=@col WHERE ID_TIPO=@id_tipo;" },
            { "delete", "DELETE FROM @tabella WHERE ID_TIPO=@id_tipo;" },
            { "select", "SELECT * FROM {0} ORDER BY valore;" }
        };
        public static Tipo Seleziona(int id_tipo, string tabella)
        {
            Tipo t = null;
            string query = "SELECT * FROM " + tabella + " WHERE ID_TIPO=" + id_tipo;
            DataTable dt = Factory.Select(query);
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                t = new Tipo() {
                    PKTipo = Convert.ToInt16(dr[0]),
                    Valore = Convert.ToString(dr[1]),
                    Descrizione = (!dr.IsNull(2)) ? Convert.ToString(dr[2]) : "",
                    Colore = (!dr.IsNull(3)) ? Convert.ToString(dr[3]) : ""
                };
            }
            return t;
        }
        public static int InsertUpdate(Tipo t, string tabella)
        {
            string q = (t.PKTipo > 0)
                ? string.Format(queries["update"], tabella)
                : string.Format(queries["insert"], tabella);

            MySqlCommand cmd = new MySqlCommand(q, Database.conn);
            cmd.Parameters.AddWithValue("@val", DBSet.SetString(t.Valore));
            cmd.Parameters.AddWithValue("@descr", DBSet.SetString(t.Descrizione));
            cmd.Parameters.AddWithValue("@col", DBSet.SetString(t.Colore));
            if(t.PKTipo > 0)
                cmd.Parameters.AddWithValue("@id_tipo", t.PKTipo);

            return (t.PKTipo > 0)
                ? Factory.ExecuteNonQuery(cmd)
                : Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int InserisciTipo(Tipo t, string tabella)
        {
            string query = "INSERT INTO @tabella VALUES(DEFAULT, @val, @descr, @col); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand(query, Database.conn);
            cmd.Parameters.AddWithValue("@tabella", tabella);
            cmd.Parameters.AddWithValue("@val", t.Valore);
            cmd.Parameters.AddWithValue("@descr", t.Descrizione);
            cmd.Parameters.AddWithValue("@col", t.Colore);
            return Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int ModificaTipo(Tipo t, string tabella)
        {
            string query = "UPDATE @tabella SET VALORE=@val, DESCRIZIONE=@descr, COLORE=@col WHERE ID_TIPO=@id_tipo;";
            MySqlCommand cmd = new MySqlCommand(query, Database.conn);
            cmd.Parameters.AddWithValue("@tabella", tabella);
            cmd.Parameters.AddWithValue("@val", t.Valore);
            cmd.Parameters.AddWithValue("@descr", t.Descrizione);
            cmd.Parameters.AddWithValue("@col", t.Colore);
            return Convert.ToInt16(Factory.ExecuteNonQuery(cmd));
        }
        public static int EliminaTipo(int id_tipo, string tabella)
        {
            string query = "DELETE FROM @tabella WHERE ID_TIPO=@id_tipo;";
            MySqlCommand cmd = new MySqlCommand(query, Database.conn);
            cmd.Parameters.AddWithValue("@tabella", tabella);
            cmd.Parameters.AddWithValue("@id_tipo", id_tipo);
            return Convert.ToInt16(Factory.ExecuteNonQuery(cmd));
        }
        public static List<Tipo> GetTipi(string tabella)
        {
            List<Tipo> tipi = new List<Tipo>();
            string q = string.Format(queries["select"], tabella);
            DataTable dt = Factory.Select(q);
            tipi.Add(new Tipo() {
                PKTipo = -1,
                Valore = "-nessun tipo-"
            });
            foreach (DataRow dr in dt.Rows)
            {
                tipi.Add(new Tipo()
                {
                    PKTipo = Convert.ToInt32(dr[0]),
                    Valore = Convert.ToString(dr[1]),
                    Descrizione = (!dr.IsNull(2)) ? Convert.ToString(dr[2]) : "",
                    Colore = (!dr.IsNull(3)) ? Convert.ToString(dr[3]) : ""
                });
            }
            return tipi;
        }

    }



    public static class DocumentiController
    {
        static Dictionary<string, string> queries = new Dictionary<string, string>()
        {
            { "insert",         "INSERT INTO clienti_documenti VALUES(DEFAULT, @nome, @ext, @dim, @dc, @di, @file, @fkcli, @fkistr); SELECT last_insert_id();" },
            { "update",         "UPDATE clienti_documenti SET NOME_FILE=@nome, ESTENSIONE=@ext, DIMENSIONI=@dim, DATA_CREAZIONE=@dc, DATA_INSERIMENTO=@di, DOCUMENTO=@file, FK_ID_CLIENTE=@fkcli, FK_ID_ISTRUTTORE=@fkistr WHERE ID_DOCUMENTO=@id_doc;" },
            { "delete",         "DELETE FROM clienti_documenti WHERE ID_DOCUMENTO = @id_doc;" },
            { "select",         "SELECT * FROM clienti_documenti WHERE id_documento = @id_doc;" },
            { "select_by_cli",  "SELECT * FROM clienti_documenti WHERE fk_id_cliente = @id_cli;" },
            { "update_rinomina","UPDATE clienti_documenti SET NOME_FILE=@nome WHERE id_documento = @id_doc;" }
        };

        public static DocumentoCliente Seleziona(int id_doc)
        {
            MySqlCommand cmd = new MySqlCommand(queries["select"], Database.conn);
            cmd.Parameters.AddWithValue("@id_doc", id_doc);

            DocumentoCliente dc = null;
            DataTable dt = Factory.Select(cmd);
            if(dt.Rows.Count > 0)
                dc = _GetDoc(dt.Rows[0]);
            return dc;
        }
        public static int Insert(DocumentoCliente d)
        {
            MySqlCommand cmd = new MySqlCommand(queries["insert"], Database.conn);
            cmd.Parameters.AddWithValue("@nome", d.NomeFile);
            cmd.Parameters.AddWithValue("@ext", d.EstensioneFile);
            cmd.Parameters.AddWithValue("@dim", d.Dimensioni);
            cmd.Parameters.AddWithValue("@dc", DBSet.SetDateTime(d.DataCreazione));
            cmd.Parameters.AddWithValue("@di", DBSet.SetDateTime(d.DataInserimento));
            cmd.Parameters.AddWithValue("@file", DBSet.SetBytes(d.File));
            cmd.Parameters.AddWithValue("@fkcli", DBSet.SetInt(d.FKCliente));
            cmd.Parameters.AddWithValue("@fkistr", DBSet.SetInt(d.FKIstruttore));
            return Convert.ToInt16(Factory.ExecuteScalar(cmd));
        }
        public static int Rename(DocumentoCliente c)
        {
            MySqlCommand cmd = new MySqlCommand(queries["update_rinomina"], Database.conn);
            cmd.Parameters.AddWithValue("@id_doc", c.PKDocumento);
            cmd.Parameters.AddWithValue("@nome", c.NomeFile);
            return Factory.ExecuteNonQuery(cmd);
        }

        public static int Delete(int id_doc)
        {
            MySqlCommand cmd = new MySqlCommand(queries["delete"], Database.conn);
            cmd.Parameters.AddWithValue("@id_doc", id_doc);
            return Factory.ExecuteNonQuery(cmd);
        }
        public static List<DocumentoCliente> GetListDocumentiCliente(int id_cli)
        {
            MySqlCommand cmd = new MySqlCommand(queries["select_by_cli"], Database.conn);
            cmd.Parameters.AddWithValue("@id_cli", id_cli);

            List<DocumentoCliente> docs = new List<DocumentoCliente>();
            DataTable dt = Factory.Select(cmd);
            if (dt.Rows.Count > 0)
                foreach(DataRow dr in dt.Rows)
                    docs.Add(_GetDoc(dt.Rows[0]));
            return docs;
        }
        public static DataTable GetTableDocumentiCliente(int id_cli)
        {
            MySqlCommand cmd = new MySqlCommand(queries["select_by_cly"], Database.conn);
            cmd.Parameters.AddWithValue("@id_cli", id_cli);
            return Factory.Select(cmd);
        }
        private static DocumentoCliente _GetDoc(DataRow dr)
        {
            DocumentoCliente dc = new DocumentoCliente();
            dc.PKDocumento = Convert.ToInt16(dr[0]);
            dc.NomeFile = Convert.ToString(dr[1]);
            dc.EstensioneFile = Convert.ToString(dr[2]);
            dc.Dimensioni = Convert.ToInt64(dr[3]);
            dc.DataCreazione = DBGet.GetDateTime(dr[4]);
            dc.DataInserimento = DBGet.GetDateTime(dr[5]);
            dc.File = DBGet.GetBytes(dr[6]);
            dc.FKCliente = Convert.ToInt16(dr[7]);
            dc.FKIstruttore = DBGet.GetInt(dr[7]);
            return dc;
        }
    }


    //public static class FactoryProveTestTMP
    //{
    //    public static bool Insert()
    //    {
    //        try
    //        {
    //            DateTime? data = null;
    //            //non serve giocare con virgolette, apici e ste cose, studai bene ed applcialo
    //            //vale anche per i null (vedi data)
    //            string val = "ciao";
    //            string query = "INSERT INTO prova VALUES( DEFAULT, @campo1, @campo2, @campo3 );";
    //            MySqlCommand cmd = new MySqlCommand(query, Database.connessione);
    //            cmd.Parameters.AddWithValue("@campo1", val);
    //            cmd.Parameters.AddWithValue("@campo2", 5);
    //            cmd.Parameters.AddWithValue("@campo3", data);

    //            cmd.ExecuteNonQuery();
    //            cmd.Dispose();

    //            MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //            return true;
    //        }
    //        catch (MySqlException ex)
    //        {
    //            MessageBox.Show("NO:\n" + ex.Message, "Errore "+ex.Number, MessageBoxButtons.OK, MessageBoxIcon.Error);
    //            return false;
    //        }
    //    }

    //    public static bool CaricaImmagineTest(string img_path)
    //    {
    //        //funziona correttamente, ho usato questo link
    //        //https://www.c-sharpcorner.com/UploadFile/deepak.sharma00/how-to-save-images-in-mysql-database-using-C-Sharp/

    //        FileStream fs;
    //        BinaryReader br;
    //        byte[] ImageData;
    //        fs = new FileStream(img_path, FileMode.Open, FileAccess.Read);
    //        br = new BinaryReader(fs);
    //        ImageData = br.ReadBytes((int)fs.Length);
    //        br.Close();
    //        fs.Close();

    //        string query = "INSERT INTO test_inserimento_immagini VALUES(DEFAULT, @img);";
    //        MySqlCommand cmd = new MySqlCommand(query, Database.connessione);
    //        cmd.Parameters.Add("@img", MySqlDbType.MediumBlob);
    //        cmd.Parameters["@img"].Value = ImageData;

    //        cmd.ExecuteNonQuery();
    //        cmd.Dispose();

    //        MessageBox.Show("ok");
    //        return true;
    //    }

    //    public static MemoryStream PrendiImmagineTest(int id)
    //    {
    //        //https://www.google.it/search?q=get+image+mysql+c%23&oq=get+image+mysql+c%23&aqs=chrome..69i57j0l3.7575j0j7&sourceid=chrome&ie=UTF-8
    //        //http://1bestcsharp.blogspot.com/2015/04/c-how-to-retrieve-image-from-mysql-database-Using-C.html

    //        String query = "SELECT * FROM test_inserimento_immagini WHERE id="+id;

    //        MySqlCommand cmd = new MySqlCommand(query, Database.connessione);
    //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
    //        DataTable table = new DataTable();

    //        da.Fill(table);

    //        byte[] img = (byte[])table.Rows[0][1];  //cooridnata che serve a me, in qusto caso di prova 2

    //        MemoryStream ms = new MemoryStream(img);
    //        da.Dispose();
    //        return ms;
    //        //pictureBox1.Image = Image.FromStream(ms);

            
    //    }

    //    public static void CaricaImmaginiEsercizi()
    //    {
    //        string query = "UPDATE esercizi SET immagine=@img WHERE id_esercizio=@id;";
    //        MySqlCommand cmd;

    //        string[] files = Directory.GetFiles(Common.ImgDirPath + "\\Esercizi");
    //        foreach(string file in files)
    //        {
    //            string full_path = Path.GetFullPath(file);
    //            //MessageBox.Show(Path.GetFileNameWithoutExtension(full_path));
    //            FileStream fs;
    //            BinaryReader br;
    //            byte[] ImageData;
    //            fs = new FileStream(full_path, FileMode.Open, FileAccess.Read);
    //            br = new BinaryReader(fs);
    //            ImageData = br.ReadBytes((int)fs.Length);
    //            br.Close();
    //            fs.Close();


    //            cmd = new MySqlCommand(query, Database.connessione);
    //            cmd.Parameters.AddWithValue("@img", ImageData);
    //            cmd.Parameters.AddWithValue("@id", Path.GetFileNameWithoutExtension(full_path));
    //            cmd.ExecuteNonQuery();
    //        }
    //        MessageBox.Show("fine");
    //    }
    //}

}