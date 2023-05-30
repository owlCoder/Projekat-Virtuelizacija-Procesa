using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
using Klijent.Komande;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Principal;
using System.ServiceModel;

namespace Klijent.InterfejsMeni
{
    public class Meni : IMeni
    {
        #region POLJA KLASE
        public readonly string pocetni_ispis = "Aplikacija PNZ 2.4.17 (GNU/PNZ 5.15.90.1-standard x86_64)\r\n\r\n " +
                "* Dokumentacija:  https://bit.ly/3BeKFUj\r\n " +
                "* Upravljanje:    https://bit.ly/3Mf7rBE\r\n " +
                "* Podrska:        https://bit.ly/3VUcz1n\r\n";
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
                MeniSend();
            }
            else if (unos.Trim().StartsWith("Get"))
            {
                MeniGet();
            }
            else
            {
                Console.WriteLine("Komanda '{0}' nije pronadjena u listi komandi", unos);
                Console.WriteLine("  da li ste mislili 'help'");
            }
        }
        #endregion

        #region METODA ZA SEND MENI
        public void MeniSend()
        {
            try
            {
                List<Audit> greske = new List<Audit>();

                bool uspesno = new Komanda().SlanjeCsv(out greske);

                if (uspesno)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[INFO]: ", DateTime.Now, " Podaci o merenju uspesno parsirani!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    // ispis svih gresaka koje su se desile
                    foreach (Audit audit in greske) 
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine(audit);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (FaultException<KomandaIzuzetak> ke)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ke.Detail.Razlog);
                Console.ForegroundColor = ConsoleColor.White;
            }
             catch (FaultException ke)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ke.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region METODA ZA GET MENI
        public void MeniGet()
        {
            /// ///////////////
            /// ANDREA
            /// ///////////////
            try
            {
                // Promenljive koje se prosledjuju metodi bool SlanjeCsv();
                bool IsMin = false, IsMax = false, IsStand = false;

                // proveriti da li je broj parametara veci od 3, ako jeste ispisati gresku
                string[] parametri = unos.Trim().Split(' '); // komande su razdvojene sa razmakom

                if (parametri.Length < (1)) // prazan string
                {
                    // unet je samo Get
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niste uneli dovoljan broj parametra za Get servis!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (parametri.Length > (3 + 1))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uneli ste preveliki broj parametra za Get servis!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            catch (DirectoryNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nije moguce upisati izvestaj jer direktorijum izvestaja ne postoji!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (FaultException < DatotekaJeOtvorenaIzuzetak > de)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(de.Detail.Razlog);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
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
