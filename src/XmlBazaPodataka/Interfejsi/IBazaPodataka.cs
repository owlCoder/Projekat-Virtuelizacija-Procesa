using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace XmlBazaPodataka.Interfejsi
{
    #region INTERFEJS ZA RAD SA XML BAZOM PODATAKA
    [ServiceContract]
    public interface IBazaPodataka
    {
        // povratna vrednost je broj uspesno upisanih redova u bazu podataka TBL_LOAD.xml,
        // ako nista nije upisano vraca 0
        // u bazu podataka TBL_LOAD.xml upisuju se Load i Audit podaci iz listi
        [OperationContract]
        int UpisUBazuPodataka(List<Load> podaci, List<Audit> greske);


        // ako je baza podataka prazna ili nije procitan nijedan podatak vraca praznu listu, u suprotnom iscitanu listu podataka
        // iz baze podataka TBL_LOAD.xml iscitava se red po red podataka i kreira objekat Load
        // i cuva u listu procitanih
        // u slucaju da datoteka TBL_LOAD.xml ne postoji kreira se nova xml datoteka
        [OperationContract]
        void ProcitajIzBazePodataka(out List<Load> procitano);
    }
    #endregion
}
