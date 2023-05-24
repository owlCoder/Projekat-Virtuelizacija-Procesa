using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    [DataContract]
    public class PregledPotrosnjeIzuzetak
    {
        [DataMember]
        public string Razlog { get; set; }

        public PregledPotrosnjeIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
    }
}
