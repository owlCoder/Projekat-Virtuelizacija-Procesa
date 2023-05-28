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
        InterakcijaDogadjajem interakcija = new InterakcijaDogadjajem();

        PregledMaksimalnePotrosnje p_max = new PregledMaksimalnePotrosnje();
        PregledMinimalnePostrosnje p_min = new PregledMinimalnePostrosnje();
        PregledStandardneDevijacijePotrosnje p_stand = new PregledStandardneDevijacijePotrosnje();

        public IRadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand)
        {
            IEnumerable<Load> podaci = new DataFetcher().PrikupiPodatkeZaTekuciDan();

            if(IsMin)
                interakcija.IzvrsiProracun += new ProracunDelegat(p_max.PregledPotrosnje);
            
            if(IsMax)
                interakcija.IzvrsiProracun += new ProracunDelegat(p_min.PregledPotrosnje);
            
            if(IsStand)
                interakcija.IzvrsiProracun += new ProracunDelegat(p_stand.PregledPotrosnje);



            return new RadSaDatotekom(null, null);
        }
    }
}
