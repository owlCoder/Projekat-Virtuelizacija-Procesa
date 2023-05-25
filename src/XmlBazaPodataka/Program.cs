using Common.Modeli;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace XmlBazaPodataka
{
    class Program
    {
        static void Main()
        {
            ServiceHost host;

            try
            {
                host = new ServiceHost(typeof(XmlBazaPodatakaServis));
                host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            XmlBazaPodatakaServis servis = new XmlBazaPodatakaServis();
            servis.ProcitajIzBazePodataka(out List<Load> procitano);

            Console.WriteLine("Servis XML Baze Podataka je uspesno pokrenut!");
            Console.ReadLine();
        }
    }
}
