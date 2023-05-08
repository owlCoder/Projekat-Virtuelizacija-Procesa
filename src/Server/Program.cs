using System.Configuration;

namespace Server
{
    class Program
    {
        static void Main()
        {
            // Ucitavanje putanje baze podataka iz konfiguracione datoteke App.config
            string putanja_baze_podataka = ConfigurationManager.AppSettings["DatotekaBazePodataka"];
        }
    }
}
