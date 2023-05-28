using Common.Dogadjaji.Publish;
using Common.Dogadjaji.Subscribe;
using System.Runtime.Serialization;

namespace Common.Dogadjaji
{
    public delegate double DelegatProracuna(bool IsMin, bool IsMax, bool IsStand);

    [DataContract]
    public class Dogadjaj
    {
        public static Publisher PublisherInstance { get; set; }

        public static Subscriber SubscriberInstance { get; set; }

        public Dogadjaj()
        {
            PublisherInstance = new Publisher();
            SubscriberInstance = new Subscriber(PublisherInstance);
        }
    }
}
