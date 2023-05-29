using Common.Modeli;
using System.Collections.Generic;

namespace Server.Interfejsi
{
    #region INTERFEJS KOJI MODELUJE METODU POTREBNU ZA PRIKUPLJANJE PODATAKA
    public interface IDataFetcher
    {
        // Metoda se povezuje sa serverom xml baze podataka
        // i prikuplja sve podatke za tekuci dan i smesta u kolekciju podataka
        // Povratna vrednost metode je lista svih podataka merenja
        IEnumerable<Load> PrikupiPodatkeZaTekuciDan();
    }
    #endregion
}
