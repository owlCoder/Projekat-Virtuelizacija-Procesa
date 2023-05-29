using Common.Datoteke;
using System.ServiceModel;

namespace Common.Proracuni
{
    [ServiceContract]
    public interface IProracun
    {
        [OperationContract]
        IRadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand);
    }
}
