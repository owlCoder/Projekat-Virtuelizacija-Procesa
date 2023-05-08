using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XmlBazaPodataka.Tabele
{
    [DataContract]
    public class LoadTabela
    {
        // Jedinstevni identifikator merenja
        [DataMember]
        public int Id { get; set; }

        // Vreme kada je merenje zabelezeno
        [DataMember]
        public DateTime Timestamp { get; set; }

        // Izmerena vrednost za dati trenutak
        [DataMember]
        public double MeasuredValue { get; set; }

        public LoadTabela() 
        { 
            // Prazan konstruktor zbog serijalizacije
        }

        // Konstruktor sa parametrima
        public LoadTabela(int id, DateTime timestamp, double measuredValue)
        {
            Id = id;
            Timestamp = timestamp;
            MeasuredValue = measuredValue;
        }

        // Metoda za proveru jednakosti objekata klase LoadTabela
        public override bool Equals(object obj)
        {
            return obj is LoadTabela tabela &&
                   Id == tabela.Id &&
                   Timestamp == tabela.Timestamp &&
                   MeasuredValue == tabela.MeasuredValue;
        }

        // Metoda za formatiran ispis objekta klase LoadTabela
        public override string ToString()
        {
            // Izlazni format: [122]: 2023-5-8 = 12223.143
            return string.Format("[{0}]: {1} = {2}", Id, Timestamp.ToString(), MeasuredValue);
        }


        // Metoda za generisanje Hask vrednosti
        public override int GetHashCode()
        {
            int hashCode = 326352841;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + MeasuredValue.GetHashCode();
            return hashCode;
        }
    }
}
