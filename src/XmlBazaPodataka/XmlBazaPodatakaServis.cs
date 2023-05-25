using Common.Datoteke;
using Common.Izuzeci;
using Common.Modeli;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using XmlBazaPodataka.Interfejsi;

namespace XmlBazaPodataka
{
    // Klasa koja implementira potrebne servise za rad sa Xml bazom podataka
    public class XmlBazaPodatakaServis : IBazaPodataka, IXmlCsvFunkcije
    {
        #region POLJE KLASE ZA ID
        private static ushort ID_LOAD = 1;
        #endregion

        #region METODA ZA CITANJE IZ XML DATOTEKE
        public IRadSaDatotekom OtvoriDatoteku(string putanja_datoteke)
        {

            if (!File.Exists(putanja_datoteke))
            {
                // ako datoteka ne postoji, kreira se nova
                string root_element = (putanja_datoteke.ToLower().Contains("audit")) ? "STAVKE" : "rows";
                XDocument novi_xml = new XDocument(new XDeclaration("1.0", "utf-8", "no"), new XElement(root_element));
                novi_xml.Save(putanja_datoteke);
            }

            // otvoriti po disposable pattern-u xml datoteku
            // promenljive za memorijski tok
            MemoryStream stream = new MemoryStream();

            using (FileStream xml = new FileStream(putanja_datoteke, FileMode.Open, FileAccess.Read))
            {
                xml.CopyTo(stream);
                xml.Dispose();
            }

            stream.Position = 0;

            return new RadSaDatotekom(stream, Path.GetFileName(putanja_datoteke));

        }
        #endregion

        #region CSV PARSER
        public bool ParsiranjeCsvDatoteke(MemoryStream csv, out List<Audit> greske)
        {
            greske = new List<Audit>();
            List<Load> nove_vrednosti = new List<Load>();
            int linija = 1;

            using (StreamReader csv_stream = new StreamReader(csv))
            {
                string csv_podaci = csv_stream.ReadToEnd();
                string[] csv_redovi = csv_podaci.Split('\n');
                string[] redovi = csv_redovi.Take(csv_redovi.Length - 1).ToArray();

                foreach (var red in redovi)
                {
                    string[] splitovano = red.Split(','); // csv - comma separated values

                    if (splitovano.Length != 2)
                    {
                        // nema dovoljno podataka u csv, mora ih imati 2, vreme:merenje
                        greske.Add(
                                new Audit(0, DateTime.Now, MessageType.Error, "Nevalidan format u CSV za datum " + DateTime.Now.ToString("yyyy-MM-dd"))
                            );
                    }
                    else
                    {
                        // ima dva podatka, proveravamo da li su validni
                        if (!TimeSpan.TryParse(splitovano[0], out TimeSpan vreme))
                        {
                            greske.Add(
                                new Audit(0, DateTime.Now, MessageType.Error, "Nevalidan podatak TIME_STAMP za datum " + DateTime.Now.ToString("yyyy-MM-dd"))
                            );
                        }
                        else
                        {
                            // vreme je validno, sada parsiramo da li je merenje validno
                            if (!float.TryParse(splitovano[1].Replace('.', ','), out float vrednost))
                            {
                                greske.Add(
                                new Audit(0, DateTime.Now, MessageType.Error, "Nevalidan podatak MEASURED_VALUE za datum " + DateTime.Now.ToString("yyyy-MM-dd"))
                            );
                            }
                            else
                            {
                                if (vrednost < 0f)
                                {
                                    greske.Add(
                                        new Audit(0, DateTime.Now, MessageType.Error, "Nevalidan podatak MEASURED_VALUE za datum " + DateTime.Now.ToString("yyyy-MM-dd"))
                                    );

                                }

                                else
                                {
                                    // i vreme i merenje su validni
                                    nove_vrednosti.Add(
                                        new Load(0, DateTime.Today + vreme, vrednost)
                                    );
                                }
                            }
                        }
                    }

                    // sledeci red u csv
                    linija += 1;
                }
            }

            // ako je broj redova koji ima gresku jednak svi unetim redovima
            // onda se kreira samo jedan audit objekat
            if (greske.Count == linija - 1)
            {
                greske.Clear(); // upisace se samo jedna greska
                greske.Add(
                        new Audit(0, DateTime.Now, MessageType.Error, "Struktura CSV datoteke nije validna za datum " + DateTime.Now.ToString("yyyy-MM-dd"))
                );

                // upisi gresku u audit
                UpisUAuditTBL(greske, ConfigurationManager.AppSettings["BazaZaGreske"]);

                // nije upisan nijedan podatak
                return false;
            }

            // greske upisati u bazu podataka
            // nove vrednosti upisati u bazu podataka
            int redova = UpisUBazuPodataka(nove_vrednosti, greske);

            // neki deo u poslatoj csv datoteci nije validan
            if (greske.Count > 0)
            {
                return false;
            }
            else
            {
                // cela csv datoteka je korektno ili je neki podatak upisan
                return redova > 0;
            }
        }
        #endregion

        #region METODE CITANJE PODATAKA IZ BAZE PODATAKA
        public bool ProcitajIzBazePodataka(out List<Load> procitano)
        {
            procitano = new List<Load>();

            using (IRadSaDatotekom datoteka = new XmlBazaPodatakaServis().OtvoriDatoteku(ConfigurationManager.AppSettings["DatotekaBazePodataka"]))
            {
                XmlDocument baza = new XmlDocument();
                baza.Load(((RadSaDatotekom)datoteka).DatotecniTok);

                // citanje podataka samo za tekuci dan
                string datum = DateTime.Now.ToString("yyyy-MM-dd");
                XmlNodeList podaci = baza.SelectNodes("//row[TIME_STAMP[contains(., '" + datum + "')]]");

                foreach (XmlNode red in podaci)
                {
                    Load novi = new Load
                    {
                        Id = ID_LOAD++,
                        MeasuredValue = float.Parse(red.SelectSingleNode("MEASURED_VALUE").InnerText),
                        Timestamp = DateTime.Parse(red.SelectSingleNode("TIME_STAMP").InnerText)
                    };

                    // dodavanje procitanog podatka u izlaznu listu
                    procitano.Add(novi);
                }

                // oslobadjanje resursa datoteke
                datoteka.Dispose();
            }

            return procitano.Count > 0;
        }
        #endregion

        #region METODA ZA UPIS U BAZU PODATAKA
        public int UpisUBazuPodataka(List<Load> podaci, List<Audit> greske)
        {
            // upis svih gresaka u audit
            UpisUAuditTBL(greske, ConfigurationManager.AppSettings["BazaZaGreske"]);

            // upis podataka u bazu xml
            return UpisULoadTBL(podaci, ConfigurationManager.AppSettings["DatotekaBazePodataka"]);
        }
        #endregion

        #region UPIS U LOAD
        private static int UpisULoadTBL(List<Load> podaci, string xml_load_path)
        {
            int upisano_redova = 0;

            using (IRadSaDatotekom datoteka = new XmlBazaPodatakaServis().OtvoriDatoteku(xml_load_path))
            {
                XmlDocument xml_load = new XmlDocument();
                xml_load.Load(((RadSaDatotekom)datoteka).DatotecniTok);

                // dodavanje podataka iz csv parsiranih u xml bazu
                foreach (Load l in podaci)
                {
                    string pretraga = "//row[TIME_STAMP='" + l.Timestamp.ToString("yyyy-MM-dd HH:mm") + "']";
                    XmlNode element = xml_load.SelectSingleNode(pretraga);

                    if (element != null)
                    {
                        element.SelectSingleNode("MEASURED_VALUE").InnerText = l.MeasuredValue.ToString().Replace(',', '.');
                        xml_load.Save(ConfigurationManager.AppSettings["DatotekaBazePodataka"]);
                    }
                    else
                    {
                        // posto se koristi ista datoteka, potrebno je vratiti se na pocetak
                        ((RadSaDatotekom)datoteka).DatotecniTok.Position = 0;

                        XDocument xml_dokument = XDocument.Load(((RadSaDatotekom)datoteka).DatotecniTok);
                        var stavke = xml_dokument.Element("rows");

                        // ne postoji red u xml, dodaje se novi
                        var novi = new XElement("row");
                        novi.Add(new XElement("TIME_STAMP", l.Timestamp.ToString("yyyy-MM-dd HH:mm")));
                        novi.Add(new XElement("MEASURED_VALUE", l.MeasuredValue.ToString().Replace(',', '.')));

                        stavke.Add(novi);
                        xml_load.Save(ConfigurationManager.AppSettings["DatotekaBazePodataka"]);
                    }

                    upisano_redova += 1; // jedan red se upisao u tabelu
                }

                // oslobadjanje resursa
                datoteka.Dispose();
            }

            return upisano_redova;
        }
        #endregion

        #region UPIS U AUDIT
        private static void UpisUAuditTBL(List<Audit> greske, string xml_audit_path)
        {
            using (IRadSaDatotekom datoteka = new XmlBazaPodatakaServis().OtvoriDatoteku(xml_audit_path))
            {
                XDocument xml_audit = XDocument.Load(((RadSaDatotekom)datoteka).DatotecniTok);

                var elements = xml_audit.Descendants("ID");
                var max_row_id = elements.Max(e => int.Parse(e.Value));

                // upisi greske u audit tabelu
                // ali pre toga azuriraj id-greske sa max + 1
                foreach (Audit a in greske)
                {
                    a.Id = ++max_row_id;

                    var stavke = xml_audit.Element("STAVKE");
                    var novi = new XElement("row");

                    // dodavanje podataka u xml serijalizaciju
                    novi.Add(new XElement("ID", a.Id));
                    novi.Add(new XElement("TIME_STAMP", a.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    novi.Add(new XElement("MESSAGE_TYPE", a.Message_Type));
                    novi.Add(new XElement("MESSAGE", a.Message));

                    stavke.Add(novi);
                    xml_audit.Save(ConfigurationManager.AppSettings["BazaZaGreske"]);
                }

                // ako nema gresaka upisi u audit da je sve okej
                if (greske.Count == 0)
                {
                    var stavke = xml_audit.Element("STAVKE");
                    var novi = new XElement("row");

                    // dodavanje podataka u xml serijalizaciju
                    novi.Add(new XElement("ID", ++max_row_id));
                    novi.Add(new XElement("TIME_STAMP", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    novi.Add(new XElement("MESSAGE_TYPE", "Info"));
                    novi.Add(new XElement("MESSAGE", "Podaci uspesno procitani i prosledjeni"));

                    stavke.Add(novi);
                    xml_audit.Save(ConfigurationManager.AppSettings["BazaZaGreske"]);
                }

                // oslobadjanje resursa
                datoteka.Dispose();
            }
        }
        #endregion
    }
}
