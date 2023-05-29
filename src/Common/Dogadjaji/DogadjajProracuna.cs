using Common.Modeli;
using System;
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
    public class InterakcijaDogadjajem
    {
        #region POLJA KLASE
        // Dogadjaj na serveru koji ce se aktivirati pri odgovarajucoj akciji klijenta
        public event ProracunDelegat IzvrsiProracun;

        // Kolekcija podataka u kojoj ce se cuvati zahtevani proracuni
        public List<Proracun> Proracuni;

        // Niz tipova potrosnje koja se zahteva i iterator kroz cirkularnu listu 
        public readonly string[] Tip = { "Max Load: ", "Min Load: ", "Stand Deviation: " };
        public ushort TipBrojac = 0;
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
                // Poziv metoda vezanih za delegat
                IzvrsiProracun(podaci);

                // Poziv metode za ponovni preracun vrednosti baznih proracuna
                AzuriranjeProracuna(podaci);
            }
        }
        #endregion

        #region METODA ZA AZURIRANJE PRORACUNA NASTALIH POZIVIMA DELEGATA
        // Metoda koja za primljenu listu podataka, preracunava sve tekuce vrednosti
        // proracuna nastalih na osnovu veznih metoda delegata
        public void AzuriranjeProracuna(IEnumerable<Load> podaci)
        {
            // Prolazak kroz listu svih povezanih metoda za delegat
            foreach (ProracunDelegat p in IzvrsiProracun.GetInvocationList().Cast<ProracunDelegat>())
            {
                // Cuvanje podataka proracuna delegata
                Proracuni[TipBrojac++ % 3].VrednostProracuna = p(podaci);
            }
        }
        #endregion
    }
    #endregion
}
