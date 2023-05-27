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

            // Poziv metode koja ce procitati sve Load objekte za tekuci dan i smestiti u listu podataka
            proksi_xml.ProcitajIzBazePodataka(out List<Load>procitano_tekuci_dan);

            return procitano_tekuci_dan;
        }
    }
}
