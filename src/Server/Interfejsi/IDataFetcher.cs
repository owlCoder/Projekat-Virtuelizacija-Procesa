using Common.Modeli;
using System.Collections.Generic;

namespace Server.Interfejsi
{
    // Interfejs koji modeluje metodu potrebnu za prikupljanje podataka
    public interface IDataFetcher
    {
        // Metoda se povezuje sa serverom xml baze podataka
        // i prikuplja sve podatke za tekuci dan i smesta u kolekciju podataka
        // Povratna vrednost metode je lista svih podataka merenja
        IEnumerable<Load> PrikupiPodatkeZaTekuciDan();
    }
}
