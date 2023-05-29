using Server.Interfejsi;
using System;
using System.Collections.Generic;

namespace Server.ProracunDevijacije
{
    #region KLASA ZA PRORACUN DEVIJACIJE
    public class Devijacija : IDevijacija
    {
        #region PRAZAN KONSTRUKTOR
        public Devijacija()
        {
            // Prazan konstruktor
        }
        #endregion

        #region METODA ZA PRORACUN STANDARDNE DEVIJACIJE
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
        #endregion
    }
    #endregion
}
