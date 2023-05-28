using Common.Datoteke;

namespace Server
{
    public class StatistickiServis // : IPreglediPotrosnje
    {
        // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
        public delegate double PregledPotrosnjeDelegat();

        // Dogadjaj koji ce biti kreiran
        public event PregledPotrosnjeDelegat PPD;

        public IRadSaDatotekom Racunaj()
        {


            return new RadSaDatotekom(null, "");
        }
    }
}
