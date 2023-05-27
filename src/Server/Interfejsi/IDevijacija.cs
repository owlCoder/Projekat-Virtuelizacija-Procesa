using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfejsi
{
    internal interface IDevijacija
    {
        // Metoda koja racuna standardnu devijaciju po formuli
        // Ulazni parametar je lista merenja, dok je izlaz izracunata
        // standardna devijacija
        double StandardnaDevijacija(IEnumerable<double> merenja);
    }
}
