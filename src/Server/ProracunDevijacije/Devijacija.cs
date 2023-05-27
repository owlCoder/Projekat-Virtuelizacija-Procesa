using Server.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ProracunDevijacije
{
    public class Devijacija : IDevijacija
    {
        public double StandardnaDevijacija(IEnumerable<double> merenja)
        {
            double mean = 0.0;
            double M2 = 0.0;
            int count = 0;

            foreach (double value in merenja)
            {
                count++;
                double delta = value - mean;
                mean += delta / count;
                M2 += delta * (value - mean);
            }

            return Math.Sqrt(M2 / (count - 1));
        }
    }
}
