using Server.PrikupljanjePodataka;
using System;
using System.Linq;
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

            //PregledPotrosnje.PregledMaksimalnePotrosnje pm = new PregledPotrosnje.PregledMaksimalnePotrosnje();
            //DataFetcher dataFetcher = new DataFetcher();
            //Console.WriteLine("Max Load: " + pm.PregledPotrosnje(dataFetcher.PrikupiPodatkeZaTekuciDan().ToList()));


            Console.WriteLine("Servis Statistike je uspesno pokrenut!");
            Console.ReadLine();
        }
    }
}
