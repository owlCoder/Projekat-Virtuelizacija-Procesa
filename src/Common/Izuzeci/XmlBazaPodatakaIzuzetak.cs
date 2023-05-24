using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    [DataContract]
    public class XmlBazaPodatakaIzuzetak
    {
        [DataMember]
        public string Razlog { get; set; }

        public XmlBazaPodatakaIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
    }
}
