using Common.Modeli;
using Server.Interfejsi;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using XmlBazaPodataka.Interfejsi;

namespace Server.PregledPotrosnje
{
    public class PregledMinimalnePostrosnje : IPreglediPotrosnje
    {
        // Metoda koja racuna minimalnu potrosnju za tekuci dan
        public double PregledPotrosnje()
        {
            // Promenljiva u kojoj se cuva najmanja zabelezena potrosnja
            double potrosnja = 0.0;

            // Lista podataka u kojoj ce biti procitani podaci za tekuci dan
            List<Load> procitano_tekuci_dan = new List<Load>();

            // Povezivanje na server baze podataka
            ChannelFactory<IBazaPodataka> kanal_xml_servis = new ChannelFactory<IBazaPodataka>("BazaPodataka");
            IBazaPodataka proksi_xml = kanal_xml_servis.CreateChannel();

            // Poziv metode koja ce procitati sve Load objekte za tekuci dan i smestiti u listu podataka
            proksi_xml.ProcitajIzBazePodataka(out procitano_tekuci_dan);

            // pronaci najamanju vrednost potrosnje i upisati je u potrosnja
            potrosnja = procitano_tekuci_dan.Any() ? procitano_tekuci_dan.Max(p => p.MeasuredValue) : 0.0;

            return potrosnja;
        }
    }
}
