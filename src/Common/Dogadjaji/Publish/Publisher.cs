using System.Runtime.Serialization;

namespace Common.Dogadjaji.Publish
{
    [DataContract]
    public class Publisher : IPublisher
    {
        public event DelegatProracuna DelegatProracuna;

        public void Publish(bool IsMin, bool IsMax, bool IsStand)
        {
            DelegatProracuna?.Invoke(IsMin, IsMax, IsStand);
        }
    }
}
