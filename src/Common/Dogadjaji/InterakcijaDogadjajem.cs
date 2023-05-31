using Common.Modeli;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.Dogadjaji
{
    #region DELEGAT PRORACUNA
    // Delegat ciji potpis odgovara metodi definisanoj u interfjesu IProracun
    public delegate double ProracunDelegat(IEnumerable<Load> podaci);
    #endregion

    #region KLASA ZA UPRAVLJANJE INTERAKCIJOM DOGADJAJA I DELEGATA
    // Klasa modeluje interakciju event i delegates
    [DataContract]
    public class InterakcijaDogadjajem : IInterakcijaDogadjajem
    {
        #region POLJA KLASE
        // Dogadjaj na serveru koji ce se aktivirati pri odgovarajucoj akciji klijenta
        public event ProracunDelegat IzvrsiProracun;

        // Kolekcija podataka u kojoj ce se cuvati zahtevani proracuni
        public List<Proracun> Proracuni;

        // Niz tipova potrosnje koja se zahteva
        public readonly string[] Tip = { "Max Load: ", "Min Load: ", "Stand Deviation: " };

        public void Objavi(IEnumerable<Load> podaci)
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }
    #endregion
}
