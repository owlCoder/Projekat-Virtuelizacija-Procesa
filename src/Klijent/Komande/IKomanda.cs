using Common.Izuzeci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Komande
{
    [ServiceContract]
    public interface IKomanda
    {
        // Metoda na osnovu lokacija direktorijuma csv datoteke iz App.config
        // kreira
        [OperationContract]
        [FaultContract(typeof(KomandaIzuzetak))]
        bool SlanjeCsv();
    }
}
