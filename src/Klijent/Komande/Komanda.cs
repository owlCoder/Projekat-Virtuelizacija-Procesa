using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
using Common.Proracuni;
using Klijent.TekstualniIzvestaji;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using XmlBazaPodataka.Interfejsi;

namespace Klijent.Komande
{
    public class Komanda : IKomanda
    {
        public bool SlanjeCsv(out List<Audit> greske)
        {
            bool uspesno = false;
            string putanja_csv_datoteka = ConfigurationManager.AppSettings["CsvDirektorijum"];

            // promenljive za memorijski tok
            MemoryStream stream = new MemoryStream();

            // inicijalizacija liste u kojoj se beleze audit zapisi
            greske = new List<Audit>();

            // komunikacija sa xml csv funkcijama iz xml baze podataka
            ChannelFactory<IXmlCsvFunkcije> kanal_xml_servis = new ChannelFactory<IXmlCsvFunkcije>("XmlCsvParser");
            IXmlCsvFunkcije proksi_csv = kanal_xml_servis.CreateChannel();

            // ako ne postoji direktorijum za csv - kreirati ga
            if (!Directory.Exists(putanja_csv_datoteka))
            {
                Directory.CreateDirectory(putanja_csv_datoteka);
            }

            // pokupi sve datoteke iz direktorijuma
            string[] svi_fajlovi = Directory.GetFiles(putanja_csv_datoteka);

            if (svi_fajlovi.Length == 0)
            {
                // nijedan csv fajl se nalazi u direktorijumu
                throw new FaultException<KomandaIzuzetak>(
                    new KomandaIzuzetak("[ERROR]: " + DateTime.Now.ToString() + " Ne postoji nijedna CSV datoteka u direktorijumu! Komanda 'Send' neuspesno izvrsena!"));
            }

            // kreiranje stream-a i slanje csv datoteke na server
            // citanje csv datoteke, ako postoji vise csv datoteka, uzima se prva iz direktorijuma
            using (FileStream csv = new FileStream(svi_fajlovi[0], FileMode.Open, FileAccess.Read))
            {
                // kopiranje datoteke u novi memorijski tok podataka
                csv.CopyTo(stream);

                // oslobadjanja resursa zauzetih prilikom otvaranja csv datoteke
                csv.Dispose();
            }

            // pozicioniranje na pocetak memorijskog toka podataka
            stream.Position = 0;

            // citanje sadrzaja datoteke i slanje na server
            using (IRadSaDatotekom datoteka = new RadSaDatotekom(stream, "data"))
            {
                uspesno = proksi_csv.ParsiranjeCsvDatoteke((datoteka as RadSaDatotekom).DatotecniTok, out greske);
                datoteka.Dispose();
            }

            // uspesno obradjen fajl - obrisati ga
            if (uspesno && File.Exists(svi_fajlovi[0]))
            {
                File.Delete(svi_fajlovi[0]);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("[INFO]: " + DateTime.Now + " Datoteka " + svi_fajlovi[0] + " uspesno obrisana!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            return uspesno;
        }

        public bool SlanjeGetKomande(bool IsMin = false, bool IsMax = false, bool IsStand = false)
        {
            bool uspesno = false;

            if (IsMin == false && IsMax == false && IsStand == false)
            {
                // samo je poslat get, desava se izuzetak komande
                throw new FaultException<KomandaIzuzetak>(
                    new KomandaIzuzetak("[ERROR]: " + DateTime.Now.ToString() + " Ne postoji nijedan parametar uz Get zahtev! Komanda 'Get' neuspesno izvrsena!"));
            }

            // komunikacija sa xml csv funkcijama iz xml baze podataka
            ChannelFactory<IProracun> kanal_statistika_servis = new ChannelFactory<IProracun>("StatistikaServis");
            IProracun proksi = kanal_statistika_servis.CreateChannel();

            // dodatni opis:
            // Ako je Get komanda prosla od strane servisa
            // povratna vrednost metode sa servera je memory stream
            // iz klase RadSaDatotekom pozvati konstruktor kreirati novi objekat
            // iz njega proslediti objekatKlase.DatotecniTok konstrutkoru memory stream
            // upisati datoteku pomocu metode iz TekstualniIzvestaji/UpisUIzvestaj.cs
            // na putanju C:\Temp\kalkulacije\
            // izvestaj ne mora biti upisan pa se moze desiti izuzetak
            // TO DO

            using (IRadSaDatotekom datoteka = proksi.PokreniProracun(IsMin, IsMax, IsStand))
            {
                // kreiranje teksutalne datoteke na strani klijenta nakon izvrsenog proracuna
                uspesno = new UpisUIzvestaj().KreirajDatotekuKalkulacije(datoteka);

                // poruka korisniku
                if (uspesno)
                {
                    // Ispis poruke o uspesnom prijemu datoteke
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[INFO]: " + DateTime.Now + " Datoteka sa trazenim proracunima uspesno pristigla!");
                    Console.ForegroundColor = ConsoleColor.White;

                    // ispis poruke o lokaciji pristigle datoteke
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[INFO]: " + DateTime.Now + " Lokacija pristigle datoteke je '" + Path.Combine(ConfigurationManager.AppSettings["IzvestajiDirektorijum"], (datoteka as RadSaDatotekom).NazivDatoteke) + "'");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // oslobadjanje zauzetih resursa
                datoteka.Dispose();
            }

            return uspesno;
        }
    }
}
