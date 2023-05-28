using Common.Datoteke;
using System.ServiceModel;

namespace Server.Interfejsi
{
    [ServiceContract]
    public interface IProracun
    {
        [OperationContract]
        IRadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand);
    }
}
