using Common.Dogadjaji.Publish;
using System.Runtime.Serialization;

namespace Common.Dogadjaji.Subscribe
{
    [DataContract]
    public class Subscriber : ISubscriber
    {
        private Publisher publisher;

        //public IRadSaDatotekom ObradaZahteva(bool IsMin, bool IsMax, bool IsStand)
        //{


        //    return new RadSaDatotekom(null, null); // promeni kasnije
        //}

        public Subscriber(Publisher pub)
        {
            publisher = pub;
        }

        [DataMember]
        public Publisher PublisherProperty
        {
            get
            {
                return publisher;
            }

            set
            {
                publisher = value;
            }
        }
    }
}
