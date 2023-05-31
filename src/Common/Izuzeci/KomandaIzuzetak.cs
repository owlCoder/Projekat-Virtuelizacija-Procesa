using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    #region KLASA ZA RAD SA IZUZETKOM KOMANDE
    [DataContract]
    public class KomandaIzuzetak
    {
        #region POLJE KLASE
        [DataMember]
        public string Razlog { get; set; }
        #endregion

        #region KONSTRUKTOR KLASE
        public KomandaIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
        #endregion
    }
    #endregion
}
