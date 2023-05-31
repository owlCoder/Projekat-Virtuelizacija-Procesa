using Common.Izuzeci;
using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace Klijent.Komande
{
    #region INTERFEJS ZA GET I SEND KOMANDE
    [ServiceContract]
    public interface IKomanda
    {
        // Metoda na osnovu lokacija direktorijuma csv datoteka iz App.config
        // kreira novi MemoryStream u kom je otvorena datoteka
        // i taj memorijski tok prosledjuje servisu za parsiranje i obradu datoteke
        // Odredisni EndPoint:
        // XmlBazaPodataka -> bool ParsiranjeCsvDatoteke(MemoryStream csv, out List<Audit> greske);
        // Povratna vrednost: true ako je uspesno kreiran MemoryStream i poslat na servis, u suprotnom false
        // U slucaju da se desio TIMEOUT (Servis nije pokrenut/dostupan) izazvati izuzetak KomandaIzuzetak
        [OperationContract]
        [FaultContract(typeof(KomandaIzuzetak))]
        bool SlanjeCsv(out List<Audit> greske);


        // Metoda koja salje get komandu na servis, ulazni parametri funkcije su podrazumevano
        // false, dok se mogu istovremeno zahtevati i sve tri kalkulacije.
        // Odredisni EndPoint je Server -> StatistickiServer
        // Povratna vrednost je true ako postoji barem jedan zapis, u suprotnom false
        // Ako su sva tri parametra false (korisnik je uneo samo Get, izazvati izuzetak KomandaIzuzetak
        // Na kraju ako postoji podatak, kreira se tekstualna datoteka koja sadrzi primljene proracune
        [OperationContract]
        [FaultContract(typeof(KomandaIzuzetak))]
        bool SlanjeGetKomande(bool IsMin = false, bool IsMax = false, bool IsStand = false);
    }
    #endregion
}
