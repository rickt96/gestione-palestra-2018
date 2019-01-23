using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;

namespace GestionePalestra
{
    public static class Database
    {
        public static string DB_HOST;
        public static int DB_PORT;
        public static string DB_NAME;
        public static string DB_USER;
        public static string DB_PASSWORD;

        public static MySqlConnection conn;


        /// <summary>
        /// Apre la connessione al database:  
        /// carica i parametri,  
        /// tenta di aprire la connessione, 
        /// se la connessione è già aperta ignora la procedura, 
        /// altrimenti la apre
        /// </summary>
        /// <returns>true se la connessione è valida, altrimenti false in caso di errore</returns>
        public static bool Open()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                return true;

            LoadCfg();

            try
            {
                string ConnectionString = string.Format("server = {0}; port = {1}; database = {2}; uid = {3}; password = {4}; Convert Zero Datetime = True; Allow Zero Datetime = True", DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD);
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Impossibile connettersi al database:\n" + ex.ToString(), "Connettore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        /// <summary>
        /// chiude la connessione corrente
        /// </summary>
        public static bool Close()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Impossibile chiudere la connessione:\n" + ex.ToString(), "Connessione");
                return false;
            }
        }


        /// <summary>
        /// restituisce lo stato della connessione
        /// </summary>
        public static string GetState()
        {
            return conn.State.ToString();
        }


        /// <summary>
        /// carica  i parametri di connessione da app.config
        /// </summary>
        public static void LoadCfg()
        {
            //encrypt md5 password - https://www.junian.net/csharp-md5/
            DB_HOST = ConfigurationManager.AppSettings["DB_HOST"].ToString();
            DB_PORT = Convert.ToInt16(ConfigurationManager.AppSettings["DB_PORT"]);
            DB_NAME = ConfigurationManager.AppSettings["DB_NAME"].ToString();
            DB_USER = ConfigurationManager.AppSettings["DB_USER"];
            DB_PASSWORD = ConfigurationManager.AppSettings["DB_PASSWORD"].ToString();
        }


        /// <summary>
        /// scrive i parametri di connessione in app.config
        /// </summary>
        public static bool SaveCfg()
        {
            try
            {
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfg.AppSettings.Settings["DB_HOST"].Value = DB_HOST;
                cfg.AppSettings.Settings["DB_PORT"].Value = DB_PORT.ToString();
                cfg.AppSettings.Settings["DB_USER"].Value = DB_USER;
                cfg.AppSettings.Settings["DB_PASSWORD"].Value = DB_PASSWORD;
                cfg.AppSettings.Settings["DB_NAME"].Value = DB_NAME;

                cfg.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Impossibile scrivere i parametri:\n"+ex.Message);
                return false;
            }
        }


        /// <summary>
        /// esegue una connessione di test specificando i parametri
        /// </summary>
        public static bool TestConnection(string server, int port, string db, string username, string password)
        {
            MySqlConnection test_conn;
            string ConnectionString = string.Format("server={0}; port={1}; database={2}; uid={3}; password={4}", server, port, db, username, password);
            test_conn = new MySqlConnection(ConnectionString);

            try
            {
                test_conn.Open();
                test_conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                test_conn.Close();
                return false;
            }
        }
    }

}
