using Common.Izuzeci;
using Common.Modeli;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace Server.Interfejsi
{
    [ServiceContract]
    public interface IIzvestajProracuna
    {
        // Metoda koja se poziva nakon poziva delegata metoda proracuna
        // povratna vrednost je tok bajtova na teksutalni fajl u kojem su upisani 
        // prosledjeni proracuni u formatu: Tip_Proracuna: Vrednost_Proracuna
        // dok su ulazni parametri lista proracuna koja se upisuje u izlazni txt fajl
        // u slucaju da je lista proracuna prazna ili neki od podataka nevalidan
        // desava se IzvestajIzuzetak
        // ako nije moguce kreirati tekstualni fajl takodje se desava IzvestajIzuzetak
        [OperationContract]
        [FaultContract(typeof(IzvestajIzuzetak))]
        Stream NapraviIzvestajNakonProracuna(List<Proracun> podaci);
    }
}
