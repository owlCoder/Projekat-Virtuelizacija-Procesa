using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
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
            greske = new List<Audit>();

            // komunikacija sa xml csv funkcijama iz xml baze podataka
            ChannelFactory<IXmlCsvFunkcije> kanal_xml_servis = new ChannelFactory<IXmlCsvFunkcije>("XmlCsvParser");
            IXmlCsvFunkcije proksi_csv = kanal_xml_servis.CreateChannel();

            // pokupi sve datoteke iz direktorijuma
            string[] svi_fajlovi = Directory.GetFiles(putanja_csv_datoteka);

            // kreiranje stream-a i slanje csv
            // citanje csv datoteke
            using (FileStream csv = new FileStream(svi_fajlovi[0], FileMode.Open, FileAccess.Read))
            {
                csv.CopyTo(stream);
            }

            stream.Position = 0;

            // citanje sadrzaja datoteke i slanje na server
            using (IRadSaDatotekom datoteka = new RadSaDatotekom(stream, "data"))
            {
                uspesno = proksi_csv.ParsiranjeCsvDatoteke((datoteka as RadSaDatotekom).DatotecniTok, out greske);
                datoteka.Dispose();
            }

            return uspesno;
        }

        public bool SlanjeGetKomande(bool IsMin = false, bool IsMax = false, bool IsStand = false)
        {
            bool uspesno = false;

            // dodatni opis:
            // Ako je Get komanda prosla od strane servisa
            // povratna vrednost metode sa servera je memory stream
            // iz klase RadSaDatotekom pozvati konstruktor kreirati novi objekat
            // iz njega proslediti objekatKlase.DatotecniTok konstrutkoru memory stream
            // upisati datoteku pomocu metode iz TekstualniIzvestaji/UpisUIzvestaj.cs
            // na putanju C:\Temp\kalkulacije\
            // izvestaj ne mora biti upisan pa se moze desiti izuzetak
            // TO DO

            return uspesno;
        }
    }
}
