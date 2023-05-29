using System;
using System.Collections.Generic;
using System.Linq;
using Common.Datoteke;
using Common.Dogadjaji;
using Common.Modeli;
using Server.Interfejsi;
using Server.PregledPotrosnje;
using Server.PrikupljanjePodataka;

namespace Server
{
    public class StatistickiServis : IProracun
    {
        InterakcijaDogadjajem Interakcija = new InterakcijaDogadjajem();

        PregledMaksimalnePotrosnje p_max = new PregledMaksimalnePotrosnje();
        PregledMinimalnePostrosnje p_min = new PregledMinimalnePostrosnje();
        PregledStandardneDevijacijePotrosnje p_stand = new PregledStandardneDevijacijePotrosnje();

        public IRadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand)
        {
            IEnumerable<Load> podaci = new DataFetcher().PrikupiPodatkeZaTekuciDan();

            if (IsMin)
                Interakcija.IzvrsiProracun += new ProracunDelegat(p_max.PregledPotrosnje);
            
            if(IsMax)
                Interakcija.IzvrsiProracun += new ProracunDelegat(p_min.PregledPotrosnje);

            if (IsStand)
                Interakcija.IzvrsiProracun += new ProracunDelegat(p_stand.PregledPotrosnje);

            Interakcija.Objavi(podaci);

            foreach(Proracun p in Interakcija.Proracuni)
            {
                Console.WriteLine(p.TipProracuna + p.VrednostProracuna);
            }

            return new RadSaDatotekom(null, null);
        }
    }
}
