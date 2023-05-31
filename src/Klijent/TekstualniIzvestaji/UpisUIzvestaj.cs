using Common.Datoteke;
using Common.Izuzeci;
using System.Configuration;
using System.IO;
using System.ServiceModel;

namespace Klijent.TekstualniIzvestaji
{
    #region KLASA ZA UPIS IZVESTAJA NA KLIJENTU
    public class UpisUIzvestaj : IUpisUIzvestaj
    {
        #region METODA KOJA KREIRA TEKSTUALNU DATOTEKU NA KLIJENTU
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
            if (File.Exists(lokacija_datoteke))
            {
                if (DatotekaOtvorena(lokacija_datoteke))
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

            // datoteka ne postoji i nije otvorena, te je mozemo kreirati
            // koristimo prosledjeni memorijski strim
            MemoryStream stream = (datoteka as RadSaDatotekom).DatotecniTok;

            // citanje primljenog datotecnog toka sa servera i konverzija u tekstualnu datoteku
            using (FileStream txt_fajl = new FileStream(lokacija_datoteke, FileMode.Create, FileAccess.Write))
            {
                // niz bajtova u koji ce se cuvati strim
                byte[] bytes = new byte[stream.Length];

                // citanje memorijskog strima i prebacivanje u niz bajtova
                stream.Read(bytes, 0, (int)stream.Length);

                // upis u tekstualnu datoteku na klijentu
                txt_fajl.Write(bytes, 0, bytes.Length);

                // datoteka je upisana - zatvaranje datotecnog toka 
                txt_fajl.Close();

                uspesno = true;
            }

            return uspesno;
        }
        #endregion

        #region METODA ZA PROVERU DA LI JE DATOTEKA OTVORENA
        public static bool DatotekaOtvorena(string putanja_fajla)
        {
            try
            {
                // Ukoliko je datoteka otvorena od strane drugog procesa - desava se IOException
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
        #endregion
    }
    #endregion
}
