using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    #region KLASA ZA RAD SA IZUZETKOM IZVESTAJA
    [DataContract]
    public class IzvestajIzuzetak
    {
        #region POLJE KLASE
        [DataMember]
        public string Razlog { get; set; }
        #endregion

        #region KONSTRUKTOR KLASE
        public IzvestajIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
        #endregion
    }
    #endregion
}
