using Common.Izuzeci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Komande
{
    public class Komanda : IKomanda
    {
        public bool SlanjeCsv()
        {
            /// /////////////////////
            /// SANJA
            /// ////////////////////
            bool uspesno = false;

            // TO DO

            return uspesno;
        }

        public bool SlanjeGetKomande(bool IsMin = false, bool IsMax = false, bool IsStand = false)
        {
            /// ////////////////
            /// ANDREA
            /// ///////////////
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
