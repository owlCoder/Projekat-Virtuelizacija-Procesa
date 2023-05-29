using Common.Datoteke;
using Common.Izuzeci;
using System.ServiceModel;

namespace Common.Proracuni
{
    [ServiceContract]
    public interface IProracun
    {
        [OperationContract]
        [FaultContract(typeof(PregledPotrosnjeIzuzetak))]
        RadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand);
    }
}
