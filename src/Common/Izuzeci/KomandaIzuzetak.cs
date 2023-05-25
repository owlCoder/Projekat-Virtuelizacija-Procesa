using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    [DataContract]
    public class KomandaIzuzetak
    {
        [DataMember]
        public string Razlog { get; set; }

        public KomandaIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
    }
}
