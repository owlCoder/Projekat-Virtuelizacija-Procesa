namespace Server
{
    // Delegat koji se koristi za pozivanje odgovarajucih metoda za preglede potrosnje
    delegate dynamic Pregledi();

    public class StatistickiServis // : IPreglediPotrosnje
    {
        #region POLJA KLASE
        InterakcijaDogadjajem Interakcija = new InterakcijaDogadjajem();
        #endregion

    }
}
