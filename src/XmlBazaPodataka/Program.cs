using System;
using System.ServiceModel;

namespace XmlBazaPodataka
{
    class Program
    {
        static void Main()
        {
            ServiceHost host;

            // server
            try
            {
                host = new ServiceHost(typeof(XmlBazaPodatakaServis));
                host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
