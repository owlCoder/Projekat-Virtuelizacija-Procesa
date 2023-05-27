﻿using Common.Modeli;
using Server.Interfejsi;
using Server.ProracunDevijacije;
using System.Collections.Generic;
using System.Linq;

namespace Server.PregledPotrosnje
{
    public class PregledStandardneDevijacijePotrosnje : IPreglediPotrosnje
    {
        // Metoda koja racuna standardnu devijaciju potrosnje za tekuci dan
        public double PregledPotrosnje(List<Load> procitano_tekuci_dan)
        {
            // Promenljiva u kojoj se cuva standardna devijacija zabelezene potrosnje
            double potrosnja = 0.0;

            // pronaci vrednost potrosnje po standardnoj devijaciji i upisati je u potrosnja
            potrosnja = new Devijacija().StandardnaDevijacija(procitano_tekuci_dan.Select(p => p.MeasuredValue));

            return potrosnja;
        }
    }
}
