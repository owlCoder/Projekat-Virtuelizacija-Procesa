using System;
using Common.Modeli;
using System.Collections.Generic;
using System.IO;
using XmlBazaPodataka.Interfejsi;
using Common.Datoteke;

namespace XmlBazaPodataka
{
    // Klasa koja implementira potrebne servise za rad sa Xml bazom podataka
    public class XmlBazaPodatakaServis : IBazaPodataka, IXmlCsvFunkcije
    {
        #region METODE ZA RAD SA XML I CSV datotekama
        public bool OtvoriDatoteku(string putanja_datoteke, IRadSaDatotekom otvorena_datoteka)
        {
            throw new NotImplementedException();
        }

        public bool ParsiranjeCsvDatoteke(MemoryStream csv, out List<Audit> greske)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region METODE ZA RAD SA BAZOM PODATAKA
        public bool ProcitajIzBazePodataka(out List<Load> procitano)
        {
            throw new NotImplementedException();
        }

        public int UpisUBazuPodataka(List<Load> podaci, List<Audit> greske)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
