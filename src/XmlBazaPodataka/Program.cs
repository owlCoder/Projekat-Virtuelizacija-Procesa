using System;
using System.ServiceModel;

namespace XmlBazaPodataka
{
    #region GLAVNA PROGRAMSKA NIT
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

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[INFO]: " + DateTime.Now + " Servis XML Baze Podataka je uspesno pokrenut!");
            Console.ReadLine();
        }
    }
    #endregion
}
