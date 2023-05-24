using System;
using System.IO;
using System.Runtime.Serialization;

namespace Common.Datoteke
{
    // Klasa koja modeluje rad sa datotekama po Disposable sablonu oslobadjanja memorije
    [DataContract]
    public class RadSaDatotekom : IRadSaDatotekom
    {
        // Tok bajtova u kojoj se nalazi trenutno obradjivana datoteka
        [DataMember]
        public MemoryStream DatotecniTok { get; set; }

        // Naziv datoteke koja je trenutno otvorena
        [DataMember]
        public string NazivDatoteke { get; set; }

        // Konstruktor sa parametrima
        public RadSaDatotekom(MemoryStream datotecniTok, string nazivDatoteke)
        {
            DatotecniTok = datotecniTok;
            NazivDatoteke = nazivDatoteke;
        }

        // Disposable sablon za rad sa datotekama
        public void Dispose()
        {
            // Samo u slucaju da postoji otvoren i aktivan datotecni tok - pokusace se oslobadjanje memorije
            if (DatotecniTok != null)
            {
                try
                {
                    DatotecniTok.Dispose();
                    DatotecniTok.Close();
                    DatotecniTok = null;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Greska prilikom oslobadjanja memorije datoteke '{0}'!", NazivDatoteke);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
