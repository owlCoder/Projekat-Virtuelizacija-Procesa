using System.ServiceModel;

namespace Common.Dogadjaji.Publish
{
    [ServiceContract]
    public interface IPublisher
    {
        [OperationContract]
        void Publish(bool IsMin, bool IsMax, bool IsStand);
    }
}
