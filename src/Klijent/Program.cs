using Klijent.InterfejsMeni;
using System;

namespace Klijent
{
    class Program
    {
        // Objekat klase meni za pozivanje menija
        private static Meni meni = new Meni();

        static void Main()
        {
            Console.WriteLine(meni.pocetni_ispis);

            while (true)
            {
                 meni.IspisiMeni();
            }
        }
    }
}
