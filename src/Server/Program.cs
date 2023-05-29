using System;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static void Main()
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(StatistickiServis));
                host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Servis Statistike je uspesno pokrenut!");
            Console.ReadLine();
        }
    }
}
