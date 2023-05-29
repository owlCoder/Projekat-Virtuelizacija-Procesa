using Common.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dogadjaji
{
    public delegate double ProracunDelegat(IEnumerable<Load> podaci);

    [DataContract]
    public class InterakcijaDogadjajem
    {
        public event ProracunDelegat IzvrsiProracun;

        public IEnumerable<Proracun> Proracuni;

        public string[] Tip = { "Max Load: ", "Min Load: ", "Stand Load: " } ;
        public ushort TipBrojac = 0;

        public InterakcijaDogadjajem()
        {
            IzvrsiProracun = null;
            Proracuni = new List<Proracun>();
        }

        public void Objavi(IEnumerable<Load> podaci)
        {
            if (IzvrsiProracun != null)
            {
                IzvrsiProracun(podaci);
            }
        }
    }
}
