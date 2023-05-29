using System;
using System.Collections.Generic;
using System.Linq;
using Common.Datoteke;
using Common.Dogadjaji;
using Common.Modeli;
using Common.Proracuni;
using Server.Izvestaj;
using Server.PregledPotrosnje;
using Server.PrikupljanjePodataka;
using System.IO;

namespace Server
{
    public class StatistickiServis : IProracun
    {
        InterakcijaDogadjajem Interakcija = new InterakcijaDogadjajem();

        public RadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand)
        {
            IEnumerable<Load> podaci = new DataFetcher().PrikupiPodatkeZaTekuciDan();

            if (IsMin)
                Interakcija.IzvrsiProracun += new ProracunDelegat(new PregledMaksimalnePotrosnje().PregledPotrosnje);

            if(IsMax)
                Interakcija.IzvrsiProracun += new ProracunDelegat(new PregledMinimalnePostrosnje().PregledPotrosnje);

            if (IsStand)
                Interakcija.IzvrsiProracun += new ProracunDelegat(new PregledStandardneDevijacijePotrosnje().PregledPotrosnje);

            Interakcija.Objavi(podaci);

            IzvestajProracuna ip = new IzvestajProracuna();
            RadSaDatotekom dat = ip.NapraviIzvestajNakonProracuna(Interakcija.Proracuni) as RadSaDatotekom;

            return dat;            
        }
    }
}
