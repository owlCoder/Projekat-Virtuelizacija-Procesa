using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace Server.Interfejsi
{
    #region INTERFEJS ZA GENERISANJE IZVESTAJA PRORACUNA NAKON INVOKACIJE DELEGATA/DOGADJAJA
    [ServiceContract]
    public interface IIzvestajProracuna
    {
        // Metoda koja se poziva nakon poziva delegata metoda proracuna
        // povratna vrednost je tok bajtova na teksutalni fajl u kojem su upisani 
        // prosledjeni proracuni u formatu: Tip_Proracuna: Vrednost_Proracuna
        // dok su ulazni parametri lista proracuna koja se upisuje u izlazni txt fajl
        // u slucaju da je lista proracuna prazna ili neki od podataka nevalidan
        // desava se IzvestajIzuzetak
        [OperationContract]
        [FaultContract(typeof(IzvestajIzuzetak))]
        IRadSaDatotekom NapraviIzvestajNakonProracuna(IEnumerable<Proracun> podaci);
    }
    #endregion
}
