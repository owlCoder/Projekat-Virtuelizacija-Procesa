using Common.Izuzeci;
using System.Configuration;
using System.ServiceModel;

namespace Server.Interfejsi
{
    // Interfejs koji predstavlja metode koje ce racunati pregled potrosnje (min, max, stand)
    [ServiceContract]
    public interface IPreglediPotrosnje
    {
        // Metoda koja pronalazi min, max, std potrosnju za tekuci datum
        [OperationContract]
        [FaultContract(typeof(PregledPotrosnjeIzuzetak))]
        float PregledPotrosnje();
    }
}
