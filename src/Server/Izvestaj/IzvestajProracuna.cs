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
    public class IzvestajProracuna : IIzvestajProracuna // mozda treba i : IDisposable
    {
        public RadSaDatotekom NapraviIzvestajNakonProracuna(List<Proracun> podaci)
        {
            // ako nema podataka - tj nije generisan nijedan proracun, izazvati izuzetak
            if (podaci.ToList().Count == 0 || (podaci.ToList().FindAll(p => p.VrednostProracuna == -1)).Count == 3)
            {
                throw new FaultException<IzvestajIzuzetak>(
                    new IzvestajIzuzetak("[ERROR]: Nema podataka za generisanje izvestaja!"));
            }

            // ako ima podataka, serijalizovati ih i pretvoriti u niz bajtova
            string za_slanje = "";

              foreach (Proracun p in podaci)
            {
                // ako se vrednost proracuna nije promenila - onda ona nije ni zahtevana od klijenta
                // kao konacni izvestaj ulaze samo validni proracuni i proracuni koji su se desili
                if (p.VrednostProracuna != -1)
                {
                    // izlazni format je 'Max Load: 4238.322321'
                    za_slanje += p.TipProracuna + p.VrednostProracuna.ToString() + "\n";
                }
            }

             // string se pretrava u niz bajtova
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(za_slanje.Replace(',', '.')));

            string ime_datoteke = "calculations_" + DateTime.Now.ToString("yyyy_MM_dd_HHmm") + ".txt";

             // klijentu se vraca memorijski tok novo kreirane teksutalne datoteke
            return new RadSaDatotekom(stream, ime_datoteke);
        }
    }
}
