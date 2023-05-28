using Common.Datoteke;
using Common.Izuzeci;
using Common.Potrosnja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dogadjaji.Publish
{
    [DataContract]
    public class Publisher : IPublisher
    {
        // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
        public delegate IRadSaDatotekom PregledPotrosnjeDelegat(bool IsMin, bool IsMax, bool IsStand);

        // Dogadjaj koji ce biti kreiran
        public event PregledPotrosnjeDelegat PPD;

        // Da li je u toku proces disposinga
        private bool disposed = false;

        private Publisher() 
        {
            // Podrazumevano delegat nije vezan ni za jednu metodu
            PPD = null;
        }

        // Metoda koja delegatu dodaje odgovarajucu metodu proracuna
        public void DodajMetoduProracuna(IPreglediPotrosnje metoda)
        {

        }

        public IRadSaDatotekom Publish(bool IsMin, bool IsMax, bool IsStand)
        {
            if(PPD !=  null)
            {
                return PPD(IsMin, IsMax, IsStand);
            }
            else
            {
                throw new FaultException<PregledPotrosnjeIzuzetak>(
                    new PregledPotrosnjeIzuzetak("[Error]: Nije moguce dobiti pregled potrosnje za datum " + DateTime.Now.ToString("dd-MM-yyyy")));
            }
        }

        // Metode za oslobadjanje resursa nakon upotrebe
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged resources.
                disposed = true;
            }
        }
    }
}
