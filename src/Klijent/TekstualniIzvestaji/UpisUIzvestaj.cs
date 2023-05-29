using Common.Datoteke;
using Common.Izuzeci;
using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;

namespace Klijent.TekstualniIzvestaji
{
    public class UpisUIzvestaj : IUpisUIzvestaj
    {
        public bool KreirajDatotekuKalkulacije(IRadSaDatotekom datoteka)
        {
            bool uspesno = false;
            string direktorijum_izvestaja = ConfigurationManager.AppSettings["IzvestajiDirektorijum"];
            string lokacija_datoteke = Path.Combine(direktorijum_izvestaja, (datoteka as RadSaDatotekom).NazivDatoteke);

            // Provera da li postoji direktorijum
            if (!Directory.Exists(direktorijum_izvestaja))
            {
                // ako ne postoji kreiramo ga
                Directory.CreateDirectory(direktorijum_izvestaja);
            }

            // Da li datoteka postoji, ako postoji da li je onda vec otvorena
            if(File.Exists(lokacija_datoteke))
            {
                if(DatotekaOtvorena(lokacija_datoteke))
                {
                    throw new FaultException<DatotekaJeOtvorenaIzuzetak>(
                    new DatotekaJeOtvorenaIzuzetak("[Error]: Datoteka " + (datoteka as RadSaDatotekom).NazivDatoteke
                                                   + " je otvorena od strane drugog procesa i nije moguc upis u nju!"));
                }
                else
                {
                    // ako postoji a nije otvorena, obrisi staru datoteku
                    File.Delete(lokacija_datoteke);
                }
                
            }
            else
            {
                // datoteka ne postoji i nije otvorena, te je mozemo kreirati
                // koristimo prosledjeni memorijski strim
                MemoryStream stream = (datoteka as RadSaDatotekom).DatotecniTok;

                using (FileStream txt_fajl = new FileStream(lokacija_datoteke, FileMode.Create, FileAccess.Write))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    txt_fajl.Write(bytes, 0, bytes.Length);
                    txt_fajl.Close();
                    uspesno = true;
                }
            }

            return uspesno;
        }

        public static bool DatotekaOtvorena(string putanja_fajla)
        {
            try
            {
                using (FileStream stream = File.Open(putanja_fajla, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }

    }
}
