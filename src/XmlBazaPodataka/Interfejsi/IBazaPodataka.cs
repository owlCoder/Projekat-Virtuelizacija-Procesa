using Common.Izuzeci;
using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace XmlBazaPodataka.Interfejsi
{
    [ServiceContract]
    public interface IBazaPodataka
    {
        // povratna vrednost je broj uspesno upisanih redova u bazu podataka TBL_LOAD.xml,
        // ako nista nije upisano vraca -1
        // u bazu podataka TBL_LOAD.xml upisuju se Load i Audit podaci iz listi
        [OperationContract]
        [FaultContract(typeof(XmlBazaPodatakaIzuzetak))]
        int UpisUBazuPodataka(List<Load> podaci, List<Audit> greske);


        // ako je baza podataka prazna ili nije procitan nijedan podatak vraca false, u suprotnom true
        // iz baze podataka TBL_LOAD.xml iscitava se red po red podataka i kreira objekat Load
        // i cuva u listu procitanih
        // u slucaju da datoteka TBL_LOAD.xml ne postoji desava se izuzetak XmlBazaPodatakaIzuzetak
        [OperationContract]
        [FaultContract(typeof(XmlBazaPodatakaIzuzetak))]
        bool ProcitajIzBazePodataka(out List<Load> procitano);
    }
}
