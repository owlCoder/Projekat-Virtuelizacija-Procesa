using Common.Modeli;
using Server.Interfejsi;
using System.Collections.Generic;
using System.ServiceModel;
using XmlBazaPodataka.Interfejsi;

namespace Server.PrikupljanjePodataka
{
    public class DataFetcher : IDataFetcher
    {
        public IEnumerable<Load> PrikupiPodatkeZaTekuciDan()
        {
            // Povezivanje na server baze podataka
            ChannelFactory<IBazaPodataka> kanal_xml_servis = new ChannelFactory<IBazaPodataka>("BazaPodataka");
            IBazaPodataka proksi_xml = kanal_xml_servis.CreateChannel();

            // Lista u kojoj se cuvaju svi procitani podaci
            List<Load> procitano_tekuci_dan = new List<Load>();

            // Poziv metode koja ce procitati sve Load objekte za tekuci dan i smestiti u listu podataka
            proksi_xml.ProcitajIzBazePodataka(out procitano_tekuci_dan);

            return procitano_tekuci_dan;
        }
    }
}
