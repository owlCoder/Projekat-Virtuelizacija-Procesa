using Common.Izuzeci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Komande
{
    [ServiceContract]
    public interface IKomanda
    {
        // Metoda na osnovu lokacija direktorijuma csv datoteka iz App.config
        // kreira novi MemoryStream u kom je otvorena datoteka
        // i taj memorijski tok prosledjuje servisu za parsiranje i obradu datoteke
        // Odredisni EndPoint:
        // XmlBazaPodataka -> bool ParsiranjeCsvDatoteke(MemoryStream csv, out List<Audit> greske);
        // Povratna vrednost: true ako je uspesno kreiran MemoryStream i poslat na servis, u suprotnom false
        [OperationContract]
        [FaultContract(typeof(KomandaIzuzetak))]
        bool SlanjeCsv();


        // Metoda koja salje get komandu na servis, ulazni parametri funkcije su podrazumevano
        // false, dok se mogu istovremeno zahtevati i sve tri kalkulacije.
        // Odrednisni EndPoint je Server -> StatistickiServer
        // Povratna vrednost je true ako postoji barem ijedan zapis, u suprotnom false
        // Na kraju ako postoji podataka, kreira se tekstualna datoteka koja sadrzi primljene proracune
        [OperationContract]
        [FaultContract(typeof (KomandaIzuzetak))]
        bool SlanjeGetKomande(bool IsMin = false, bool IsMax = false, bool IsStand = false);
    }
}
