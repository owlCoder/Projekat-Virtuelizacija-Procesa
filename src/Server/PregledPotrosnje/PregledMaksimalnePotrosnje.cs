using Common.Modeli;
using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using XmlBazaPodataka.Interfejsi;

namespace Server.PregledPotrosnje
{
    public class PregledMaksimalnePotrosnje : IPreglediPotrosnje
    {
        // Metoda koja racuna maksimalnu potrosnju za tekuci dan
        public float PregledPotrosnje()
        {
            // Promenljiva u kojoj se cuva najveca zabelezena potrosnja
            float potrosnja_float = 0.0f;

            // Lista podataka u kojoj ce biti procitani podaci za tekuci dan
            List<Load> procitano_tekuci_dan = new List<Load>();

           

            return potrosnja_float;
        }
    }
}
