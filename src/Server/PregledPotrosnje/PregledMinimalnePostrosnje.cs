using Common.Modeli;
using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using XmlBazaPodataka.Interfejsi;

namespace Server.PregledPotrosnje
{
    public class PregledMinimalnePostrosnje : IPreglediPotrosnje
    {
        // Metoda koja racuna minimalnu potrosnju za tekuci dan
         public double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan)
        {
            // Promenljiva u kojoj se cuva najmanja zabelezena potrosnja
            double potrosnja = 0.0;

            
        }
    }
}
