using System;
using System.Security.Principal;

namespace Klijent.InterfejsMeni
{
    public class Meni : IMeni
    {
        #region POLJA KLASE
        public readonly string pocetni_ispis = "Welcome to PNZ 2.4.17 (GNU/PNZ 5.15.90.1-standard x86_64)\r\n\r\n " +
                "* Documentation:  https://bit.ly/3BeKFUj\r\n " +
                "* Management:     https://bit.ly/3Mf7rBE\r\n " +
                "* Support:        https://bit.ly/3VUcz1n\r\n";
        #endregion

        #region METODA ZA ISPIS MENIJA
        public void IspisiMeni()
        {
            // Trenutni prijavljeni korisnik
            string[] korisnik = WindowsIdentity.GetCurrent().Name.Split('\\');
            string trenutni_korisnik = korisnik[1] + "@" + korisnik[0] + ":";

            // Dekoracija konzole da ispisuje nalik Linux terminalu
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(trenutni_korisnik);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("/mnt/c/users/public");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" $ ");

            // Korisnikov unos
            string unos = Console.ReadLine();

            // Da li je uneta help komanda
            if (unos.Trim().Equals("help"))
            {
                IspisPomoci();
            }
            else if (unos.Trim().StartsWith("Send"))
            {
                // TO DO
            }
            else if (unos.Trim().StartsWith("Get"))
            {
                // TO DO
                // PROVERA BROJA PARAMETRA da li je sledeci min, max, stand
                // KOLIKO PARAMETARA IMA ITD
            }
            else
            {
                Console.WriteLine("Komanda '{0}' nije pronadjena u listi komandi", unos);
                Console.WriteLine("  da li ste mislili 'help'");
            }
        }
        #endregion

        #region METODA ZA SEND MENI
        // TO DO
        #endregion

        #region METODA ZA GET MENI
        // TO DO
        #endregion

        #region METODA ZA PRIKAZ HELP MENIJA
        private void IspisPomoci()
        {
            Console.WriteLine("Dostupne naredbe su: ");
            Console.WriteLine("\t - Send (iz predefisanog direktorijuma salje se CSV na servis");
            Console.WriteLine("\t - Get [min max stand] (zahteva se od servera min, max ili stand. dev. potrosnje za tekuci dan)");
            Console.Write("\t - help (prikazuje meni dostupnih komandi)\n");
        }
        #endregion
    }
}
