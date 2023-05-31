using System.Collections.Generic;

namespace Server.Interfejsi
{
    #region INTERFEJS ZA PRORACUN DEVIJACIJE VREDNOSTI
    public interface IDevijacija
    {
        // Metoda koja racuna standardnu devijaciju po formuli
        // Ulazni parametar je lista merenja, dok je izlaz izracunata
        // standardna devijacija
        double StandardnaDevijacija(IEnumerable<double> merenja);
    }
    #endregion
}
