﻿using Common.Modeli;
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
        public double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan)
        {
            // Promenljiva u kojoj se cuva najveca zabelezena potrosnja
            double potrosnja = 0.0;

            // pronaci najvecu vrednost potrosnje i upisati je u potrosnja
            potrosnja = procitano_tekuci_dan.Any() ? procitano_tekuci_dan.Max(p => p.MeasuredValue) : 0.0;

            return potrosnja;
        }
    }
}
