using Common.Modeli;
using Common.Potrosnja;
using System.Collections.Generic;
using System.Linq;

namespace Server.PregledPotrosnje
{
    public class PregledMinimalnePostrosnje : IPreglediPotrosnje
    {
        // Metoda koja racuna minimalnu potrosnju za tekuci dan
        public double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan)
        {
            // Promenljiva u kojoj se cuva najmanja zabelezena potrosnja
            double potrosnja = 0.0;

            // pronaci najamanju vrednost potrosnje i upisati je u potrosnja
            potrosnja = procitano_tekuci_dan.Any() ? procitano_tekuci_dan.Max(p => p.MeasuredValue) : 0.0;

            return potrosnja;
        }
    }
}
