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
        #endregion

        #region KONSTRUKTOR KLASE
        // Prazan konstruktor klase InterakcijaDogadjem
        public InterakcijaDogadjajem()
        {
            // Pri svakom instanciranju, lista povezanih delegata se resetuje
            IzvrsiProracun = null;

            // Inicijalizacija prazne liste proracuna
            // Inicijalno su sve izmerene vrednosti -1
            // -1 ==> Trenutno nijedan delegat nije izvrsio proracun 
            Proracuni = new List<Proracun>()
            {
                new Proracun(Tip[0], -1.0),
                new Proracun(Tip[1], -1.0),
                new Proracun(Tip[2], -1.0)
            };
        }
        #endregion

        #region METODA ZA PROZIVKU DELEGATA
        // Metoda koja za prosledjene podatke poziva sve metode 
        // povezane delegatom
        public void Objavi(IEnumerable<Load> podaci)
        {
            if (IzvrsiProracun != null)
            {
                // Poziv delegata preracun vrednosti baznih proracuna
                foreach (ProracunDelegat p in IzvrsiProracun.GetInvocationList().Cast<ProracunDelegat>())
                {
                    string imethod = p.Target.ToString();
                    int index_upisa = imethod.Contains("Max") ? 0 : (imethod.Contains("Min") ? 1 : imethod.Contains("Stand") ? 2 : 0);

                    // Cuvanje podataka proracuna delegata
                    Proracuni[index_upisa].VrednostProracuna = p(podaci);
                }
            }
        }
        #endregion
    }
    #endregion
}
