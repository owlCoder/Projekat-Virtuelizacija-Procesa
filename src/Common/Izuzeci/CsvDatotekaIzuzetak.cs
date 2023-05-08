using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
