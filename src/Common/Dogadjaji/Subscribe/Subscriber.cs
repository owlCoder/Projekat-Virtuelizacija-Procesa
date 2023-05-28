using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dogadjaji.Subscribe
{
    [DataContract]
    public class Subscriber : ISubscriber
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
