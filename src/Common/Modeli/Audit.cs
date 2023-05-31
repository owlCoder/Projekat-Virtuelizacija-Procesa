using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Modeli
{
    #region ENUMERACIJA ZA TIP PORUKE U AUDIT TABELI
    // Enumerativni tip za potrebe upisa tipa greske odnosno belezenja tipa izvestaja
    public enum MessageType {[EnumMember] Info, [EnumMember] Warning, [EnumMember] Error };
    #endregion

    #region KLASA MODELUJE JEDAN RED U AUDIT TABELI
    // Klasa koja modeluje Audit tabelu datu ulaznom XML datotetom TBL_AUDIT.xml
    [DataContract]
    public class Audit
    {
        #region POLJA KLASE
        // Jedinstveni identifikator zapisa u XML tabeli
        [DataMember]
        public int Id { get; set; }

        // Vremenski trenutak zapisa u XML tabeli
        [DataMember]
        public DateTime Timestamp { get; set; }

        // Koji tip poruke se upisuje u XML tabelu
        [DataMember]
        public MessageType Message_Type { get; set; }

        // Detaljniji opis zapisa u XML tabeli
        [DataMember]
        public string Message { get; set; }
        #endregion

        #region KONSTRUKTOR KLASE
        public Audit()
        {
            // Prazan konstruktor zbog serijalizacije
        }
        #endregion

        #region KONSTRUKTOR SA PARAMETRIMA
        // Konstruktor sa parametrima
        public Audit(int id, DateTime timestamp, MessageType message_Type, string message)
        {
            Id = id;
            Timestamp = timestamp;
            Message_Type = message_Type;
            Message = message;
        }
        #endregion

        #region METODA ZA POREDJENJE OBJEKATA KLASE AUDITTABELA
        public override bool Equals(object obj)
        {
            return obj is Audit tabela &&
                   Id == tabela.Id &&
                   Timestamp == tabela.Timestamp &&
                   Message_Type == tabela.Message_Type &&
                   Message == tabela.Message;
        }
        #endregion

        #region METODA ZA FORMATIRAN ISPIS OBJEKTA KLASE AUDIT TABELA
        public override string ToString()
        {
            // Izlazni format: [Info]: 2023-5-8 Uspesno izmeren podatak (ID: 102)
            return string.Format("[{0}]: {1} {2}", Message_Type, Timestamp.ToString(), Message);
        }
        #endregion

        #region  METODA ZA RACUNANJE HASH VREDNOSTI
        public override int GetHashCode()
        {
            int hashCode = 795156360;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + Message_Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            return hashCode;
        }
        #endregion
    }
    #endregion
}
