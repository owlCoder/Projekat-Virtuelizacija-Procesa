using System;
using System.ServiceModel;

namespace Common.Datoteke
{
    [ServiceContract]
    public interface IRadSaDatotekom : IDisposable
    {
        // Samo nasledjuje IDisposable - Disposable sablon za rad sa datotekama
    }
}
