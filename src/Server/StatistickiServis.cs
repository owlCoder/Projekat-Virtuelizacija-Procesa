using Common.Datoteke;

namespace Server
{
    // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
    delegate dynamic Pregledi();

    public class StatistickiServis // : IPreglediPotrosnje
    {
        public delegate float PregledPotrosnjeDelegat();
        public event PregledPotrosnjeDelegat PPD;

        public IRadSaDatotekom Racunaj()
        {


            return new RadSaDatotekom(null, "");
        }
    }
}
