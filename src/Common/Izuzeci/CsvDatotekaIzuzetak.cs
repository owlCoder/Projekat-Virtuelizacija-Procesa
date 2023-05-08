using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    [DataContract]
    public class CsvDatotekaIzuzetak
    {
        [DataMember]
        public string Razlog { get; set; }

        public CsvDatotekaIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
    }
}
