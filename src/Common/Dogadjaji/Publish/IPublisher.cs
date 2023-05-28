using Common.Datoteke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dogadjaji.Publish
{
    [ServiceContract]
    public interface IPublisher : IDisposable
    {
        [OperationContract]
        IRadSaDatotekom Publish(bool IsMin, bool IsMax, bool IsStand);
    }
}
