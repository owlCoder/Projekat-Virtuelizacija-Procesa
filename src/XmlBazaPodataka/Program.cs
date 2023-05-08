using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
