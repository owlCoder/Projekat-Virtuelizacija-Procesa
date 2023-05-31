using Common.Modeli;
using Common.Potrosnja;
using System.Collections.Generic;
using System.Linq;

namespace Server.PregledPotrosnje
{
    #region KLASA ZA IMPLEMENTACIJU PRORACUNA MAKSIMALNE POTROSNJE
    public class PregledMinimalnePostrosnje : IPreglediPotrosnje
    {
        #region METODA KOJA RACUNA MINIMALNU POTROSNJU ZA TEKUCI DAN
        public double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan)
        {
            // Promenljiva u kojoj se cuva najmanja zabelezena potrosnja
            double potrosnja = 0.0;

            // pronaci najmanju vrednost potrosnje i upisati je u potrosnja
            potrosnja = procitano_tekuci_dan.Any() ? procitano_tekuci_dan.Min(p => p.MeasuredValue) : 0.0;

            return potrosnja;
        }
        #endregion
    }
    #endregion
}
