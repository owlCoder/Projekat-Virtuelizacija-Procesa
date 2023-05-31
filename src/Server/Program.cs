using System;
using System.Configuration;
using System.ServiceModel;

namespace Server
{
    class Program
    {
        static void Main()
        {
            // Ucitavanje putanje baze podataka iz konfiguracione datoteke App.config
            // string putanja_baze_podataka = ConfigurationManager.AppSettings["DatotekaBazePodataka"];
            //

            try
            {
                ServiceHost host = new ServiceHost(typeof(StatistickiServis));
                host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
