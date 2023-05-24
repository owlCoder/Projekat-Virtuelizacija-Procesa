using System;
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

            Console.WriteLine("Servis XML Baze Podataka je uspesno pokrenut!");
            Console.ReadLine();
        }
    }
}
