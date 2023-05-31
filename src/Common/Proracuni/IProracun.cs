using Common.Datoteke;
using Common.Izuzeci;
using System.ServiceModel;

namespace Common.Proracuni
{
    #region INTERFEJS ZA MODELOVANJE POKRETANJA PRORACUNA NA SERVERU
    [ServiceContract]
    public interface IProracun
    {
        // Metoda koja kao ulazne parametre prima koje vrednosti proracuna
        // klijent zeli da izvrsi za tekuci dan
        // Povratna vrednost je tekstualna datoteka koja je kreirana od strane
        // servisa i sadrzi preformatiran ispis proracuna
        // U slucaju da za trenutni dan nisu zabelezeni podaci potrosnje
        // ili potrosnju nije moguce izracunati desava se izuzetak PregledPotrosnjeIzuzetak
        [OperationContract]
        [FaultContract(typeof(PregledPotrosnjeIzuzetak))]
        RadSaDatotekom PokreniProracun(bool IsMin, bool IsMax, bool IsStand);
    }
    #endregion
}
