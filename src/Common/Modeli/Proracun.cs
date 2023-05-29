using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Modeli
{
    #region KLASA KOJA MODELUJE JEDAN OBJEKAT PRORACUNA
    // Klasa koja se koristi za prenos podataka o proracunu i kasnije upis u txt fajl
    // koji ce se slati klijentu kao tok (stream) bajtova
    [DataContract]
    public class Proracun
    {
        #region POLJA KLASE
        // Koji je tip proracuna u pitanju: min, max, std devijacija
        [DataMember]
        public string TipProracuna { get; set; }

        // Proracunata vrednost za odabrani kriterijum
        [DataMember]
        public double VrednostProracuna { get; set; }
        #endregion

        #region  KONSTRUKTOR SA PARAMETRIMA
        public Proracun(string tipProracuna, double vrednostProracuna)
        {
            TipProracuna = tipProracuna;
            VrednostProracuna = vrednostProracuna;
        }
        #endregion

        #region METODA KOJA PROVERA JEDNAKOST OBJEKATA
        public override bool Equals(object obj)
        {
            return obj is Proracun proracun &&
                   TipProracuna == proracun.TipProracuna &&
                   VrednostProracuna == proracun.VrednostProracuna;
        }
        #endregion

        #region METODA ZA GENERISANJE HASH VREDNOSTI OBJEKTA
        public override int GetHashCode()
        {
            int hashCode = 1967106192;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TipProracuna);
            hashCode = hashCode * -1521134295 + VrednostProracuna.GetHashCode();
            return hashCode;
        }
        #endregion
    }
    #endregion
}
