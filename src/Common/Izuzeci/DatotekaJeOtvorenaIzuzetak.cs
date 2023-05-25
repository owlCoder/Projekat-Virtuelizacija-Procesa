using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    [DataContract]
    public class DatotekaJeOtvorenaIzuzetak
    {
        [DataMember]
        public string Razlog { get; set; }

        public DatotekaJeOtvorenaIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
    }
}
