using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
