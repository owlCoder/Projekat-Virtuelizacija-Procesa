using Common.Izuzeci;
using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Potrosnja
{
    // Interfejs koji predstavlja metode koje ce racunati pregled potrosnje (min, max, stand)
    [ServiceContract]
    public interface IPreglediPotrosnje
    {
        // Metoda koja pronalazi min, max, std potrosnju za tekuci datum
        [OperationContract]
        [FaultContract(typeof(PregledPotrosnjeIzuzetak))]
        double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan);
    }
}
