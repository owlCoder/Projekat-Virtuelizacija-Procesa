using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    #region KLASA ZA RAD SA IZUZETKOM POGRESNOG PRORACUNA
    [DataContract]
    public class PregledPotrosnjeIzuzetak
    {
        #region POLJE KLASE
        [DataMember]
        public string Razlog { get; set; }
        #endregion

        #region KONSTRUKTOR KLASE
        public PregledPotrosnjeIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
        #endregion
    }
    #endregion
}
