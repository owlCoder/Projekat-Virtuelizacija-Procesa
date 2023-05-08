using Common.Izuzeci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfejsi
{
    // Interfejs koji predstavlja metode koje ce racunati pregled potrosnje (min, max, stand)
    [ServiceContract]
    public interface IPreglediPotrosnje
    {
        // Metoda koja pronalazi min, max, std potrosnju za tekuci datum
        [OperationContract]
        [FaultContract(typeof(PregledPotrosnjeIzuzetak))]
        dynamic PregledPotrosnje();
    }
}
