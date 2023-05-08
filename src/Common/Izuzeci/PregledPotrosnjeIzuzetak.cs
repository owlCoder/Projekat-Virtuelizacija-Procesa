using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
