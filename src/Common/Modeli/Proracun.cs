using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Modeli
{
    // Klasa koja se koristi za prenos podataka o proracunu i kasnije upis u txt fajl
    // koji ce se slati klijentu kao tok (stream) bajtova
    [DataContract]
    public class Proracun
    {
        // Koji je tip proracuna u pitanju: min, max, std devijacija
        // set metoda nije dostupna jer se vrednost objekta nakon kreiranja ne sme menjati
        [DataMember]
        public string TipProracuna { get; }

        // Proracunata vrednost za odabrani kriterijum
        // set metoda nije dostupna jer se vrednost objekta nakon kreiranja ne sme menjati
        [DataMember]
        public double VrednostProracuna { get; }

        // Konstruktor sa parametrima
        public Proracun(string tipProracuna, double vrednostProracuna)
        {
            TipProracuna = tipProracuna;
            VrednostProracuna = vrednostProracuna;
        }

        // Metoda koja provera jednakost objekata
        public override bool Equals(object obj)
        {
            return obj is Proracun proracun &&
                   TipProracuna == proracun.TipProracuna &&
                   VrednostProracuna == proracun.VrednostProracuna;
        }

        // Metoda za generisanje Hash vrednosti objekta
        public override int GetHashCode()
        {
            int hashCode = 1967106192;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TipProracuna);
            hashCode = hashCode * -1521134295 + VrednostProracuna.GetHashCode();
            return hashCode;
        }
    }
}
