using Common.Modeli;
using Common.Potrosnja;
using System.Collections.Generic;
using System.Linq;

namespace Server.PregledPotrosnje
{
    #region KLASA ZA IMPLEMENTACIJU PRORACUNA MAKSIMALNE POTROSNJE
    public class PregledMaksimalnePotrosnje : IPreglediPotrosnje
    {
        #region METODA KOJA RACUNA MAKSIMALNU POTROSNJU ZA TEKUCI DAN
        public double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan)
        {
            // Promenljiva u kojoj se cuva najveca zabelezena potrosnja
            double potrosnja = 0.0;

            // pronaci najvecu vrednost potrosnje i upisati je u potrosnja
            potrosnja = procitano_tekuci_dan.Any() ? procitano_tekuci_dan.Max(p => p.MeasuredValue) : 0.0;

            return potrosnja;
        }
        #endregion
    }
    #endregion
}
