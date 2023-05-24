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
        public float PregledPotrosnje()
        {
            // Promenljiva u kojoj se cuva najmanja zabelezena potrosnja
            float potrosnja_float = 0.0f;

            // Lista podataka u kojoj ce biti procitani podaci za tekuci dan
            List<Load> procitano_tekuci_dan = new List<Load>();

            // Povezivanje na server baze podataka
            ChannelFactory<IBazaPodataka> kanal_xml_servis = new ChannelFactory<IBazaPodataka>("BazaPodataka");
            IBazaPodataka proksi_xml = kanal_xml_servis.CreateChannel();

            // Poziv metode koja ce procitati sve Load objekte za tekuci dan i smestiti u listu podataka
            proksi_xml.ProcitajIzBazePodataka(out procitano_tekuci_dan);

            // TO DO
            /// KATARINA
            ///////////////////////////

            // IZ LISTE pronaci najamanju vrednost potrosnje i upisati je u potrosnja_float


            return potrosnja_float;
        }
    }
}
