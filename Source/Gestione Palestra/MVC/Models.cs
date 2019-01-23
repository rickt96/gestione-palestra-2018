using System; using GestionePalestra.MVC;
using System.Collections.Generic;

namespace GestionePalestra.MVC
{
    /*
     * [DESCRIZIONE]
     * il seguente file contiene:
     * classi di sistema:   classi di sistema (da definire bene)
     * classi di appoggio:  classi con dati aggiuntivi che non hanno corrispondenza col DB
     * classi del modello:  classi rappresentative del modello db
     */


    //**************************************
    // classi di sistema
    //**************************************

    public class DataResponse
    {
        public bool Success { get; set; }         //flag per marcare un errore
        public object Data { get; set; }          //valore di ritorno
        public string Caption { get; set; }       //ambito operazione (il titolo da buttare alla messagebox)
        public string Message { get; set; }       //messaggio descrittivo
    }



    //**************************************
    // classi di appoggio
    //**************************************

    public class Carico
    {
        public int Reps { get; set; }
        public string Percentuale { get; set; }
        public double Peso { get; set; }
    }


    public class TipiStati
    {
        public static string StiliVita { get { return "anamnesi_tipi_lifestyle"; } }
        public static string TipiObiettivi { get { return "anamnesi_tipi_obiettivi"; } }
        public static string TipologieAvvisi { get { return "avvisi_tipi_avvisi"; } }
        public static string StatiCliente { get { return "clienti_tipi_stati"; } }
    }




    //**************************************
    // classi del modello DB
    //**************************************

    
    public class Anamnesi
    {
        public int PKAnamnesi { get; set; }

        //cliente
        public DateTime Data { get; set; }
        public int FKCliente { get; set; }
        public int FKIstruttore { get; set; }
        public string Annotazioni { get; set; }
        //fisiologica
        public int? Somatotipo { get; set; }
        public double? Peso { get; set; }
        public int? Altezza { get; set; }
        //misure
        public double? Collo { get; set; }
        public double? Spalle { get; set; }
        public double? Petto { get; set; }
        public double? Girovita { get; set; }
        public double? Fianchi { get; set; }
        public double? Gambe { get; set; }
        public double? Braccia { get; set; }
        public double? Avambracci { get; set; }
        public double? Polpacci { get; set; }
        public double? MassaMagra { get; set; }
        public double? MassaGrassa { get; set; }
        public double? Liquidi { get; set; }
        //generale
        public int? TipoStileDiVita { get; set; }
        public int? TipoObiettivo { get; set; }
        public int? Preferenze { get; set; }
        public int? FrequenzaSettimanale { get; set; }
        public TimeSpan? DurataSeduta { get; set; }
        //sportiva
        public List<string> Sport { get; set; }
        //clinica
        public List<string> Farmaci { get; set; }
        public List<string> Patologie { get; set; }
        public bool? Fumatore { get; set; }
        public bool? Bevitore { get; set; }
        //alimentare
        public List<string> Alimenti { get; set; }
        public List<string> Intolleranze { get; set; }
        public List<string> Integratori { get; set; }
        
    }


    public class ElementoAnamnesi_old
    {
        public int FKAnamnesi { get; set; }
        public string Valore { get; set; }
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public string Descrizione { get; set; }
    }



    public class CategoriaEsercizio
    {
        public int PKCategoria { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public int? CountEsercizi { get; set; }
    }


    public class ClienteBase
    {
        public int PKCliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public string NomeCompleto { get { return Nome + " " + Cognome; } }
    }


    public class Cliente : ClienteBase
    {
        public int? Sesso { get; set; }
        public DateTime? DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string CittaNascita { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime? DataIscrizione { get; set; }
        public int? FKStato { get; set; }
        public string Note { get; set; }
        public Byte[] Immagine { get; set; }
        public int? FKIstruttore { get; set; }
        public int? FKAnamnesi { get; set; }


        public int? Eta
        {
            get
            {
                if (DataNascita != null)
                {
                    int eta = DateTime.Today.Year - ((DateTime)DataNascita).Year;
                    if (DataNascita > DateTime.Today.AddYears(-eta))
                        eta--;
                    return eta;
                }
                else
                {
                    return null;
                }
            }
        }
        public string SessoToString
        {
            get
            {
                if (Sesso.HasValue)
                {
                    switch (Sesso.Value)
                    {
                        case 0: return "donna"; break;
                        case 1: return "uomo"; break;
                        default: return "----"; break;
                    }
                }
                else
                {
                    return "----";
                }
            }
        }
    }


    public class Esercizio
    {
        public int PKEsercizio { get; set; }
        public string Nome { get; set; }
        public int? Difficolta { get; set; }
        public string Descrizione { get; set; }
        public CategoriaEsercizio Categoria { get; set; }
        public byte[] Immagine { get; set; }

        public Esercizio()
        {
            Categoria = new CategoriaEsercizio();
        }
    }


    public class Istruttore
    {
        public int PKIstruttore { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int? Sesso { get; set; }
        public DateTime? DataNascita { get; set; }
        public string Citta { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? FKLivelliPermessi { get; set; }
        public byte[] Immagine { get; set; }

        public string NomeCompleto { get { return Nome + " " + Cognome; } }
    }


    public class Avviso
    {
        public int PKAvviso { get; set; }
        public string Titolo { get; set; }
        public int? FKTipo { get; set; }
        public int? Priorita { get; set; }
        public DateTime? Data { get; set; }
        public string Descrizione { get; set; }
        public bool? isPersonal { get; set; }
        public int FKIstruttore { get; set; }
        public DateTime? DataInserimento { get; set; }
        public int? FKCliente { get; set; }

        public List<AvvisoIstruttore> Destinatari { get; set; }        //elenco dei destinatari

        public Avviso()
        {
            Destinatari = new List<AvvisoIstruttore>();
        }
    }


    public class AvvisoIstruttore
    {
        public int FKAvviso { get; set; }                       //FK id_avviso
        public int FKIstruttoreDestinatario { get; set; }       //FK id_istruttore
        public DateTime? DataLettura { get; set; }
    }


    public class Test
    {
        public int PKTest { get; set; }
        public int FKCliente { get; set; }
        public int FKEsercizio { get; set; }
        public int Tipo { get; set; }
        public int Ripetizioni { get; set; }
        public int Carico { get; set; }
        public DateTime Data { get; set; }
    }


    public class Scheda
    {
        public int PKScheda { get; set; }
        public string Nome { get; set; }
        public string Obiettivo { get; set; }
        public int? Difficolta { get; set; }
        public int? FrequenzaSettimanale { get; set; }
        public int? NumeroSedute { get; set; }
        public int? Fase { get; set; }
        public string Dettagli { get; set; }
        public bool? IsModel { get; set; }
        public int? FKIstruttore { get; set; }
        public DateTime? DataInserimento { get; set; }
        public int? FKCategoriaScheda { get; set; }

        public int? FKCliente { get; set; }
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }

        public List<Seduta> Sedute { get; set; }

        public Scheda()
        {
            Sedute = new List<Seduta>();
        }
    }


    public class Seduta
    {
        public int PKSeduta { get; set; }
        public int FKScheda { get; set; }
        public string Nome { get; set; }
        public int Ordine { get; set; }
        public string Descrizione { get; set; }

        public List<EsercizioSeduta> Esercizi { get; set; }

        public Seduta()
        {
            Esercizi = new List<EsercizioSeduta>();
        }

        public int NumeroEsercizi { get { return Esercizi.Count; } }
    }


    public class EsercizioSeduta
    {
        public int PKEsercizioSeduta { get; set; }
        public int FKSeduta { get; set; }
        public int FKEsercizio_old { get; set; }
        public Esercizio Esercizio { get; set; }
        public int? Serie { get; set; }
        public string Ripetizioni { get; set; }
        public TimeSpan? Recupero { get; set; }
        public double? Carico { get; set; }
        public int Metodo { get; set; }
        public string Note { get; set; }
        public int? Ordine { get; set; }
        public bool? ATempo { get; set; }
        public TimeSpan? Durata { get; set; }
        public int? Gruppo { get; set; }                //indica se l'esercizio fa parte di un gruppo, ovvero il raggruppamento di super serie o circuiti



        public string NomeEsercizio { get { return (Esercizio != null) ? Esercizio.Nome : ""; } }
        public string NomeCategoria { get { return (Esercizio != null) ? Esercizio.Categoria.Nome : ""; } }
    }


    public class CategoriaScheda
    {
        public int PKCategoria { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public int? CountSchede { get; set; }
    }


    public class Annotazione
    {
        public int PKAnnotazione { get; set; }
        public int FKIstruttore { get; set; }
        public string Titolo { get; set; }
        public string Testo { get; set; }
        public DateTime? Data { get; set; }
        public bool? Svolto { get; set; }
    }


    public class Tipo
    {
        public int PKTipo { get; set; }
        public string Valore { get; set; }
        public string Descrizione { get; set; }
        public string Colore { get; set; }
    }


    public class LivelloPermesso
    {
        public int PKLivelloPermesso { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public bool ANAMNESI_CUD_SELF { get; set; }
        public bool ANAMNESI_UD_OTHER { get; set; }
        public bool ANAMNESI_R { get; set; }
        public bool AVVISI_CUD_SELF { get; set; }
        public bool AVVISI_UD_OTHER { get; set; }
        public bool AVVISI_R { get; set; }
        public bool AVVISI_SET_URGENT { get; set; }
        public bool CLIENTI_CUD_SELF { get; set; }
        public bool CLIENTI_UD_OTHER { get; set; }
        public bool CLIENTI_R { get; set; }
        public bool ISTRUTTORI_UD_SELF { get; set; }
        public bool ISTRUTTORI_CUD_OTHER { get; set; }
        public bool ISTRUTTORI_R { get; set; }
        public bool ISTRUTTORI_SHOW_SETTINGS { get; set; }
        public bool SCHEDE_CUD_SELF { get; set; }
        public bool SCHEDE_UD_OTHER { get; set; }
        public bool SCHEDE_R { get; set; }
        public bool TEST_CUD_SELF { get; set; }
        public bool TEST_UD_OTHER { get; set; }
        public bool TEST_R { get; set; }
    }



    /// <summary>
    /// documento appartenente ad un cliente. 
    /// l'oggetto contiene lel varie informazioni del documento ed il file stesso espresso come array di byte
    /// </summary>
    public class DocumentoCliente
    {
        public int PKDocumento { get; set; }
        public string NomeFile { get; set; }
        public string EstensioneFile { get; set; }
        public long Dimensioni { get; set; }
        public DateTime? DataCreazione { get; set; }
        public DateTime? DataInserimento { get; set; }
        public byte[] File { get; set; }
        public int? FKIstruttore { get; set; }
        public int FKCliente { get; set; }

        public string NomeCompleto { get{ return NomeFile + "." + EstensioneFile; } }
        public string DimensioniToString { get { return Dimensioni / 1000 + " KB"; } }
    }



    /// <summary>
    /// Rappresenta i progressi del cliente registrati nel tempo
    /// </summary>
    public class Progressi
    {
        public int PK { get; set; }
        public double Peso { get; set; }
        public int Altezza { get; set; }

        public double MisuraCollo { get; set; }

        public byte[] ImmagineFrontale { get; set; }
        public byte[] ImmaginePosteriore { get; set; }
        public byte[] Immaginelaterale { get; set; }
    }
}
