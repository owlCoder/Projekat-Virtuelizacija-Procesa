using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
using Common.Proracuni;
using Klijent.TekstualniIzvestaji;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
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
            greske = new List<Audit>();

            // komunikacija sa xml csv funkcijama iz xml baze podataka
            ChannelFactory<IXmlCsvFunkcije> kanal_xml_servis = new ChannelFactory<IXmlCsvFunkcije>("XmlCsvParser");
            IXmlCsvFunkcije proksi_csv = kanal_xml_servis.CreateChannel();

            // pokupi sve datoteke iz direktorijuma
            string[] svi_fajlovi = Directory.GetFiles(putanja_csv_datoteka);

            if (svi_fajlovi.Length == 0)
            {
                // nijedan csv fajl se nalazi u direktorijumu
                throw new FaultException<KomandaIzuzetak>(
                    new KomandaIzuzetak("[ERROR]: " + DateTime.Now.ToString() + " Ne postoji nijedna CSV datoteka u direktorijumu! Komanda 'Send' neuspesno izvrsena!"));
            }

            // kreiranje stream-a i slanje csv
            // citanje csv datoteke
            using (FileStream csv = new FileStream(svi_fajlovi[0], FileMode.Open, FileAccess.Read))
            {
                csv.CopyTo(stream);
                csv.Dispose();
            }

            stream.Position = 0;

            // citanje sadrzaja datoteke i slanje na server
            using (IRadSaDatotekom datoteka = new RadSaDatotekom(stream, "data"))
            {
                uspesno = proksi_csv.ParsiranjeCsvDatoteke((datoteka as RadSaDatotekom).DatotecniTok, out greske);
                datoteka.Dispose();
            }

            // uspesno obrisan fajl obrisati
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

            // Ako je Get komanda prosla od strane servisa
            // povratna vrednost metode sa servera je memory stream
            // upisati datoteku pomocu metode iz TekstualniIzvestaji/UpisUIzvestaj.cs
            // na putanju C:\Temp\kalkulacije\
            // izvestaj ne mora biti upisan pa se moze desiti izuzetak
            using (IRadSaDatotekom datoteka = proksi.PokreniProracun(IsMin, IsMax, IsStand))
            {
                // ispis poruke
                uspesno = new UpisUIzvestaj().KreirajDatotekuKalkulacije(datoteka);

                // poruka korisniku
                if (uspesno)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[INFO]: " + DateTime.Now + " Datoteka sa trazenim proracunima uspesno pristigla!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[INFO]: " + DateTime.Now + " Lokacija pristigle datoteke je '" + Path.Combine(ConfigurationManager.AppSettings["IzvestajiDirektorijum"], (datoteka as RadSaDatotekom).NazivDatoteke) + "'");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                datoteka.Dispose();
            }

            return uspesno;
        }
    }
}
