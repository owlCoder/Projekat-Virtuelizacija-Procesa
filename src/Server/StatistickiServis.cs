namespace Server
{
    // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
    delegate dynamic Pregledi();

    public class StatistickiServis // : IPreglediPotrosnje
    {
        #region POLJA KLASE
        InterakcijaDogadjajem Interakcija = new InterakcijaDogadjajem();
        #endregion

        #region METODA KOJA NA OSNOVU EVENT-A POKRENUTOG OD KLIJENTA POZIVA DELEGAT I KREIRA POKRECE IZVESTAJ PRORACUNA
        public RadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand)
        {
            // prikupljanje podataka iz baze podataka za tekuci dan
            IEnumerable<Load> podaci = new DataFetcher().PrikupiPodatkeZaTekuciDan();

            // Ako ne postoje uneti podaci za tekuci dan, ne desava se dogadjaj proracuna
            // vec se izaziva izutetak PregledPotrosnjeIzuzetak
            if (!podaci.Any())
            {
                throw new FaultException<PregledPotrosnjeIzuzetak>(
                    new PregledPotrosnjeIzuzetak("[Error]: Nije zabelezena potrosnja za datum " + DateTime.Now.ToString("yyyy-MM-dd") + ". Unesite podatke potrosnje!"));
            }

            

            // slanje datoteka na klijenta koji je zahtevao proracun
            return generisana_datoteka;
        }
        #endregion
    }
}
