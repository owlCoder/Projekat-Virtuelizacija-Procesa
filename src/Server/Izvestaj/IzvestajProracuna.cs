using Common.Modeli;
using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.IO;

namespace Server.Izvestaj
{
    public class IzvestajProracuna : IIzvestajProracuna // mozda treba i : IDisposable
    {
        public MemoryStream NapraviIzvestajNakonProracuna(List<Proracun> podaci)
        {
            // ako nema podataka - tj nije generisan nijedan proracun, izazvati izuzetak
            if (podaci.ToList().Count == 0 || (podaci.ToList().FindAll(p => p.VrednostProracuna == -1)).Count == 3)
            {
                throw new FaultException<IzvestajIzuzetak>(
                    new IzvestajIzuzetak("[ERROR]: Nema podataka za generisanje izvestaja!"));
            }

            // ako ima podataka, serijalizovati ih i pretvoriti u niz bajtova
            string za_slanje = "";
        }
    }
}
