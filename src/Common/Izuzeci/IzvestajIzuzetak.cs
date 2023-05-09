using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    [DataContract]
    public class IzvestajIzuzetak
    {
        [DataMember]
        public string Razlog { get; set; }

        public IzvestajIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
    }
}
