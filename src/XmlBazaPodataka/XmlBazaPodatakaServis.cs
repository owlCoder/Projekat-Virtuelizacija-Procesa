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
                            new Audit(0, DateTime.Now, MessageType.Error, "Nema dovoljno podataka u redu " + linija + ": vreme:merenje")
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
                        MeasuredValue = double.Parse(red.SelectSingleNode("MEASURED_VALUE").InnerText.Replace('.', ',')),
                        Timestamp = DateTime.Parse(red.SelectSingleNode("TIME_STAMP").InnerText)
                    };

                    // dodavanje procitanog podatka u izlaznu listu
                    procitano.Add(novi);
                }
                // oslobadjanje resursa datoteke
                datoteka.Dispose();
            }
        }

        public int UpisUBazuPodataka(List<Load> podaci, List<Audit> greske)
        {
            int upisano_redova = 0;

            // upisi podatke u audit tabelu
            var xml_load = XDocument.Load(ConfigurationManager.AppSettings["BazaZaGreske"]);
            var elements = xml_load.Descendants("ID");
            var max_row_id = elements.Max(e => int.Parse(e.Value));

            // upisi greske u audit tabelu
            // ali pre toga azuriraj id-greske sa max + 1
            foreach(Audit a in greske)
            {
                a.Id = ++max_row_id;

                var stavke = xml_load.Element("STAVKE");
                var novi = new XElement("row");

                stavke.Add(novi);
                xml_load.Save(ConfigurationManager.AppSettings["BazaZaGreske"]);
            }

            return upisano_redova;
        }
        #endregion
    }
}
