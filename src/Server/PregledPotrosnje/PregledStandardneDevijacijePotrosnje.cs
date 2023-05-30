using Common.Modeli;
using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using XmlBazaPodataka.Interfejsi;

namespace Server.PregledPotrosnje
{
    public class PregledStandardneDevijacijePotrosnje : IPreglediPotrosnje
    {
        // Metoda koja racuna standardnu devijaciju potrosnje za tekuci dan
        public float PregledPotrosnje()
        {
            // Promenljiva u kojoj se cuva standardna devijacija zabelezene potrosnje
            float potrosnja_float = 0.0f;

            // Lista podataka u kojoj ce biti procitani podaci za tekuci dan
            List<Load> procitano_tekuci_dan = new List<Load>();

           

            return potrosnja_float;
        }
    }
}
