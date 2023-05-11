using Common.Izuzeci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.TekstualniIzvestaji
{
    [ServiceContract]
    public interface IUpisUIzvestaj
    {
        // Povratna vrednost je true ako je kalkulacija uspesno upisana u datoteku, u suprotnom false
        // prosledjenu datoteku upisuje na lokaciju koja je predefinisana u App.config konfiguracionoj datoteci
        // na konzoli ispisuje poruku o imenu kreirane datoteke kao i putanju na kojoj je sacuvana
        // npr: Izvestaj uspesno kreiran na lokaciji: 'C:/Temp/kalkulacije/calculations_2023_04_12_1530.txt'
        // moze se desiti izuzetak DirectoryNotFoundException ili DatotekaJeOtvorenaIzuzetak
        [OperationContract]
        [FaultContract(typeof(DatotekaJeOtvorenaIzuzetak))]
        bool KreirajDatotekuKalkulacije(string naziv_datoteke = "calculations_", MemoryStream kalkulacija = null);
    }
}
