using Common.Modeli;
using Server.Interfejsi;
using System.Collections.Generic;
using System.ServiceModel;
using XmlBazaPodataka.Interfejsi;

namespace Server.PrikupljanjePodataka
{
    #region KLASA ZA PRIKUPLJANJE PODATAKA IZ XML BAZE PODATAKA
    public class DataFetcher : IDataFetcher
    {
        #region METODA ZA PRIKUPLJANJE PODATAKA IZ BAZE PODATAKA
        public IEnumerable<Load> PrikupiPodatkeZaTekuciDan()
        {
            // Povezivanje na server baze podataka
            ChannelFactory<IBazaPodataka> kanal_xml_servis = new ChannelFactory<IBazaPodataka>("BazaPodataka");
            IBazaPodataka proksi_xml = kanal_xml_servis.CreateChannel();

            // Poziv metode koja ce procitati sve Load objekte za tekuci dan i smestiti u listu podataka
            proksi_xml.ProcitajIzBazePodataka(out List<Load> procitano_tekuci_dan);

            // vratiti procitane podatke pozivaocu metode
            return procitano_tekuci_dan;
        }
        #endregion
    }
}
