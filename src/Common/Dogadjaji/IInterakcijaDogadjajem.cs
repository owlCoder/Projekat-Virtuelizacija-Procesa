using Common.Modeli;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Dogadjaji
{
    [ServiceContract]
    public interface IInterakcijaDogadjajem
    {
        #region METODA ZA IZVRSAVANJE DELEGATA
        [OperationContract]
        void Objavi(IEnumerable<Load> podaci);
        #endregion
    }
}