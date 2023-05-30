using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.TekstualniIzvestaji
{
    public class UpisUIzvestaj : IUpisUIzvestaj
    {
        public bool KreirajDatotekuKalkulacije(string naziv_datoteke = "calculations_", MemoryStream kalkulacija = null)
        {
            bool uspesno = false;
            string direktorijum_izvestaja = ConfigurationManager.AppSettings["IzvestajiDirektorijum"];
            string lokacija_datoteke = Path.Combine(direktorijum_izvestaja, (datoteka as RadSaDatotekom).NazivDatoteke);

            // Provera da li postoji direktorijum
            if (!Directory.Exists(direktorijum_izvestaja))
            {
                // ako ne postoji kreiramo ga
                Directory.CreateDirectory(direktorijum_izvestaja);
            }

            
        }
    }
}
