using System;
using Common.Modeli;
using System.Collections.Generic;
using System.IO;
using XmlBazaPodataka.Interfejsi;
using Common.Datoteke;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;

namespace XmlBazaPodataka
{
    // Klasa koja implementira potrebne servise za rad sa Xml bazom podataka
    public class XmlBazaPodatakaServis : IBazaPodataka, IXmlCsvFunkcije
    {
        #region METODE ZA RAD SA XML I CSV datotekama
        public bool OtvoriDatoteku(string putanja_datoteke, IRadSaDatotekom otvorena_datoteka)
        {
            throw new NotImplementedException();
        }

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

                foreach(var red in redovi)
                {
                    string[] splitovano = red.Split(','); // csv - comma separated values
                    
                    if(splitovano.Length != 2)
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
                                // i vreme i merenje su validni
                                nove_vrednosti.Add(
                                        new Load(0, DateTime.Today + vreme, vrednost)
                                    );
                            }
                        }
                    }

                    // sledeci red u csv
                    linija += 1;
                }
            }

            // greske upisati u bazu podataka
            // nove vrednosti upisati u bazu podataka
            int redova = UpisUBazuPodataka(nove_vrednosti, greske);

            // neki deo u poslatoj csv datoteci nije validan
            if(greske.Count > 0)
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

        #region METODE ZA RAD SA BAZOM PODATAKA
        public bool ProcitajIzBazePodataka(out List<Load> procitano)
        {
            throw new NotImplementedException();
        }

        public int UpisUBazuPodataka(List<Load> podaci, List<Audit> greske)
        {
            int upisano_redova = 0;

            // upisi podatke u audit tabelu
            var xml_audit = XDocument.Load(ConfigurationManager.AppSettings["BazaZaGreske"]);
            var xml_load = XDocument.Load(ConfigurationManager.AppSettings["DatotekaBazePodataka"]);

            #region UPIS U AUDIT
            var elements = xml_audit.Descendants("ID");
            var max_row_id = elements.Max(e => int.Parse(e.Value));

            // upisi greske u audit tabelu
            // ali pre toga azuriraj id-greske sa max + 1
            foreach(Audit a in greske)
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
            if(greske.Count == 0)
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
            #endregion

            #region UPIS U LOAD
            XmlDocument load = new XmlDocument();
            load.Load(ConfigurationManager.AppSettings["DatotekaBazePodataka"]);

            // dodavanje podataka iz csv parsiranih u xml bazu
            foreach (Load l in podaci)
            {
                string pretraga = "//row[TIME_STAMP='" + l.Timestamp.ToString("yyyy-MM-dd HH:mm") + "']";
                XmlNode element = load.SelectSingleNode(pretraga);

                if (element != null)
                {
                    element.SelectSingleNode("MEASURED_VALUE").InnerText = l.MeasuredValue.ToString();
                    // element.Attributes["MEASURED_VALUE"].Value = l.MeasuredValue.ToString();
                    load.Save(ConfigurationManager.AppSettings["DatotekaBazePodataka"]);
                }
                else
                {
                    var stavke = xml_load.Element("rows");

                    // ne postoji red u xml, dodaje se novi
                    var novi = new XElement("row");
                    novi.Add(new XElement("TIME_STAMP", l.Timestamp.ToString("yyyy-MM-dd HH:mm")));
                    novi.Add(new XElement("MEASURED_VALUE", l.MeasuredValue.ToString()));

                    stavke.Add(novi);
                    xml_load.Save(ConfigurationManager.AppSettings["DatotekaBazePodataka"]);
                }

                upisano_redova += 1; // jedan red se upisao u tabelu
            }
            #endregion

            return upisano_redova;
        }
        #endregion
    }
}
