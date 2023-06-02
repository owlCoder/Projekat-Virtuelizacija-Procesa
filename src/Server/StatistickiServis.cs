using Common.Datoteke;
using Common.Dogadjaji;
using Common.Izuzeci;
using Common.Modeli;
using Common.Proracuni;
using Server.Izvestaj;
using Server.PregledPotrosnje;
using Server.PrikupljanjePodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Server
{
    #region KLASA STATISTICKOG SERVISA ZA PRUZANJE USLUGA PRORACUNA KLIJENTIMA PREKO DELEGATA I EVENT-A
    public class StatistickiServis : IProracun
    {
        #region POLJA KLASE
        InterakcijaDogadjajem Interakcija = new InterakcijaDogadjajem();
        #endregion

        #region METODA KOJA NA OSNOVU EVENT-A POKRENUTOG OD KLIJENTA POZIVA DELEGAT I KREIRA POKRECE IZVESTAJ PRORACUNA
        public RadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand)
        {
            // prikupljanje podataka iz baze podataka za tekuci dan
            IEnumerable<Load> podaci = new DataFetcher().PrikupiPodatkeZaTekuciDan();

            // Ako ne postoje uneti podaci za tekuci dan, ne desava se dogadjaj proracuna
            // vec se izaziva izutetak PregledPotrosnjeIzuzetak
            if (!podaci.Any())
            {
                throw new FaultException<PregledPotrosnjeIzuzetak>(
                    new PregledPotrosnjeIzuzetak("[Error]: Nije zabelezena potrosnja za datum " + DateTime.Now.ToString("yyyy-MM-dd") + ". Unesite podatke potrosnje!"));
            }

            // ako je klijent zahtevao proracun minimuma, metoda za racunanje minimuma se vezuje za delegat
            if (IsMin)
                Interakcija.IzvrsiProracun += new ProracunDelegat(new PregledMinimalnePostrosnje().PregledPotrosnje);

            // ako je klijent zahtevao proracun maksimuma, metoda za racunanje maksimuma se vezuje za delegat
            if (IsMax)
                Interakcija.IzvrsiProracun += new ProracunDelegat(new PregledMaksimalnePotrosnje().PregledPotrosnje);

            // ako je klijent zahtevao proracun devijacije, metoda za racunanje devijacije se vezuje za delegat
            if (IsStand)
                Interakcija.IzvrsiProracun += new ProracunDelegat(new PregledStandardneDevijacijePotrosnje().PregledPotrosnje);

            // kada su za delegat vezane sve zahtevane metode proracuna - izvrsiti proracun
            Interakcija.Objavi(podaci);

            // generisanje datoteke u kojoj se nalaze zahtevani proracuni
            IzvestajProracuna ip = new IzvestajProracuna();
            RadSaDatotekom generisana_datoteka = ip.NapraviIzvestajNakonProracuna(Interakcija.Proracuni) as RadSaDatotekom;

            // info poruka
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[INFO]: " + DateTime.Now + " Statisticki servis je obradio zahtev za proracun potrosnje!");
            Console.WriteLine("[INFO]: " + DateTime.Now + " Zahtevani proracuni su: " + (IsMin ? "'Min Load' " : "") + (IsMax ? "'Max Load' " : "") + (IsStand ? "'Stand Deviation'" : ""));

            // slanje datoteka na klijenta koji je zahtevao proracun
            return generisana_datoteka;
        }
        #endregion
    }
    #endregion
}
