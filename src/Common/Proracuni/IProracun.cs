using Common.Datoteke;
using System.ServiceModel;

namespace Common.Proracuni
{
    [ServiceContract]
    public interface IProracun
    {
        [OperationContract]
        RadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand);
    }
}
