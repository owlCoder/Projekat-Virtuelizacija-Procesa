using System.Runtime.Serialization;

namespace Common.Izuzeci
{
    #region KLASA ZA RAD SA IZUZETKOM OTVORENE DATOTEKE
    [DataContract]
    public class DatotekaJeOtvorenaIzuzetak
    {
        #region POLJE KLASE
        [DataMember]
        public string Razlog { get; set; }
        #endregion

        #region KONSTRUKTOR KLASE
        public DatotekaJeOtvorenaIzuzetak(string razlog)
        {
            Razlog = razlog;
        }
        #endregion
    }
    #endregion
}
