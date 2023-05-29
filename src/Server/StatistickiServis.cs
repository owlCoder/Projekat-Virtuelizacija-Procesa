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

            // delegati
            ProracunDelegat P1 = new ProracunDelegat(p_max.PregledPotrosnje);
            ProracunDelegat P2 = new ProracunDelegat(p_min.PregledPotrosnje);
            ProracunDelegat P3 = new ProracunDelegat(p_stand.PregledPotrosnje);
            ProracunDelegat PA = null;
            if (IsMin)
                PA += P1;
            
            if(IsMax)
                PA += P2;
            
            if(IsStand)
                PA += P3;

            Interakcija.IzvrsiProracun += PA;

            Interakcija.Objavi(podaci);

            foreach (ProracunDelegat pd in PA.GetInvocationList())
            {
                double retVal = pd(podaci);
                Console.WriteLine("\tOutput: " + retVal);
            }

            return new RadSaDatotekom(null, null);
        }
    }
}
