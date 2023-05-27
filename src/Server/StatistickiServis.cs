using Common.Datoteke;

namespace Server
{
    // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
    delegate dynamic Pregledi();

    public class StatistickiServis // : IPreglediPotrosnje
    {
        public delegate float PregledPotrosnjeDelegat();
        public event PregledPotrosnjeDelegat PPD;

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

        public IRadSaDatotekom Racunaj()
        {


            return new RadSaDatotekom(null, "");
        }
    }
}
