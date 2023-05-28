using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dogadjaji.Publish
{
    [DataContract]
    public class Publisher : IPublisher
    {
        public double Publish(bool IsMin, bool IsMax, bool IsStand)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Dispose unmanaged resources.
                disposed = true;
            }
        }
    }
}
