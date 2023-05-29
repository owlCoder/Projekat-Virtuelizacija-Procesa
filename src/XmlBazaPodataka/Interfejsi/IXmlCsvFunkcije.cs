using Common.Datoteke;
using Common.Modeli;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace XmlBazaPodataka.Interfejsi
{
    #region INTERFEJS ZA PARSIRANJE CSV DATOTEKE I OTVARANJE DATOTEKE
    [ServiceContract]
    public interface IXmlCsvFunkcije
    {
        // povratna vrednost je true ako se nije desila nijedna greska prilikom obrade csv datoteke,
        // u suprotnom false
        // parsira se ulazna datoteka, ukoliko datoteka ne postoji, unet je pogresan tip podatka, pogresan datum
        // kreira Audit objekat i dodaje ga u listu gresaka (kasnije se prosledjuje klijentu)
        // ako je parsirani red validan, kreira se novi objekat klase Load i smesta u privremenu
        // listu List<Load> ucitano
        // metoda poziva posebnu metodu za upis u bazu podataka
        [OperationContract]
        bool ParsiranjeCsvDatoteke(MemoryStream csv, out List<Audit> greske);

        // Ako xml datoteka postoji i uspesno se otvori vraca true, u suprotnom false
        // putanja datoteke je predifinisana u App.config
        // ako datoteka ne postoji kreira se nova prazna xml datoteka
        [OperationContract]
        IRadSaDatotekom OtvoriDatoteku(string putanja_datoteke);
    }
    #endregion
}
