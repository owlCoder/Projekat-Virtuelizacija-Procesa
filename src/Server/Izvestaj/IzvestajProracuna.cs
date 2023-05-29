using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Server.Izvestaj
{
    public class IzvestajProracuna : IIzvestajProracuna
    {
        public IRadSaDatotekom NapraviIzvestajNakonProracuna(IEnumerable<Proracun> podaci)
        {
            // ako nema podataka - tj nije generisan nijedan izvestaj, izazvati izuzetak
            if (podaci.ToList().Count == 0 || (podaci.ToList().FindAll(p => p.VrednostProracuna == -1)).Count == 3)
            {
                throw new FaultException<IzvestajIzuzetak>(
                    new IzvestajIzuzetak("[ERROR]: Nema podataka za generisanje izvestaja!"));
            }

            // ako ima podataka, serijalizovati ih i pretvoriti u niz bajtova
            string za_slanje = "";

            foreach (Proracun p in podaci)
            {
                if (p.VrednostProracuna != -1)
                {
                    za_slanje += p.TipProracuna + p.VrednostProracuna.ToString() + "\n";
                }
            }

            // string se pretrava u niz bajtova
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(za_slanje.Replace(',', '.')));
            string ime_datoteke = "calculations_" + DateTime.Now.ToString("yyyy_MM_dd_HHmm") + ".txt";

            return new RadSaDatotekom(stream, ime_datoteke);
        }
    }
}
