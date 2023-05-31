﻿using Common.Datoteke;
using Common.Izuzeci;
using System.ServiceModel;

namespace Klijent.TekstualniIzvestaji
{
    #region INTERFEJS KOJI MODELUJE METODU ZA UPIS STRIMA U TEKSUTALNU DATOTEKU NA KLIJENTU
    [ServiceContract]
    public interface IUpisUIzvestaj
    {
        // Povratna vrednost je true ako je kalkulacija uspesno upisana u datoteku, u suprotnom false
        // prosledjenu datoteku upisuje na lokaciju koja je predefinisana u App.config konfiguracionoj datoteci
        // na konzoli ispisuje poruku o imenu kreirane datoteke kao i putanju na kojoj je sacuvana
        // npr: Izvestaj uspesno kreiran na lokaciji: 'C:/Temp/kalkulacije/calculations_2023_04_12_1530.txt'
        // moze se desiti izuzetak DatotekaJeOtvorenaIzuzetak
        [OperationContract]
        [FaultContract(typeof(DatotekaJeOtvorenaIzuzetak))]
        bool KreirajDatotekuKalkulacije(IRadSaDatotekom datoteka);
    }
    #endregion
}
