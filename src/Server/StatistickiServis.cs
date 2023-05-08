using Server.Interfejsi;
using Server.PregledPotrosnje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
    delegate dynamic Pregledi();

    public class StatistickiServis // : IPreglediPotrosnje
    {
        // TO DO
        // Hint: Standardna implementacija ipregledipotrosnje
        // a u njima se poziva PregledPotrosnje.Pregled Min/Max/Std klase

        /*
        Pregledi pregledi = new PregledMaksimalnePotrosnje().PregledPotrosnje();
        public StatistickiServis() 
        {
            pregledi();
        }
        */
    }
}
