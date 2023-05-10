using Common.Modeli;
using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Izvestaj
{
    public class IzvestajProracuna : IIzvestajProracuna // mozda treba i : IDisposable
    {
        public Stream NapraviIzvestajNakonProracuna(List<Proracun> podaci)
        {
            throw new NotImplementedException();
        }
    }
}
