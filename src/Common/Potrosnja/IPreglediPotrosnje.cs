using Common.Izuzeci;
using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Potrosnja
{
    #region INTERFEJS KOJI PREDSTAVLJA METODE KOJE CE RACUNATI PREGLED POTROSNJE (MIN, MAX, STAND)
    [ServiceContract]
    public interface IPreglediPotrosnje
    {
        // Metoda koja pronalazi min, max, std potrosnju za tekuci datum
        [OperationContract]
        [FaultContract(typeof(PregledPotrosnjeIzuzetak))]
        double PregledPotrosnje(IEnumerable<Load> procitano_tekuci_dan);
    }
    #endregion
}
