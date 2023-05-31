using System;
using System.Runtime.Serialization;

namespace Common.Modeli
{
    #region KLASA KOJE MODELUJE JEDAN LOAD OBJEKAT U TABELI LOAD
    [DataContract]
    public class Load
    {
        #region POLJA KLASE
        // Jedinstevni identifikator merenja
        [DataMember]
        public int Id { get; set; }

        // Vreme kada je merenje zabelezeno
        [DataMember]
        public DateTime Timestamp { get; set; }

        // Izmerena vrednost za dati trenutak
        [DataMember]
        public double MeasuredValue { get; set; }
        #endregion

        #region KONSTRUTKOR KLASE
        public Load()
        {
            // Prazan konstruktor zbog serijalizacije
        }
        #endregion

        #region KONSTRUKTOR SA PARAMETRIMA
        public Load(int id, DateTime timestamp, double measuredValue)
        {
            Id = id;
            Timestamp = timestamp;
            MeasuredValue = measuredValue;
        }
        #endregion


        #region METODA ZA PROVERU JEDNAKOSTI OBJEKATA KLASE LOADTABELA
        public override bool Equals(object obj)
        {
            return obj is Load tabela &&
                   Id == tabela.Id &&
                   Timestamp == tabela.Timestamp &&
                   MeasuredValue == tabela.MeasuredValue;
        }
        #endregion

        #region METODA ZA FORMATIRAN ISPIS OBJEKTA KLASE LOADTABELA
        public override string ToString()
        {
            // Izlazni format: [122]: 2023-5-8 = 12223.143
            return string.Format("[{0}]: {1} = {2}", Id, Timestamp.ToString(), MeasuredValue);
        }
        #endregion


        #region METODA ZA GENERISANJE HASH VREDNOSTI
        public override int GetHashCode()
        {
            int hashCode = 326352841;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + MeasuredValue.GetHashCode();
            return hashCode;
        }
        #endregion
    }
    #endregion
}
