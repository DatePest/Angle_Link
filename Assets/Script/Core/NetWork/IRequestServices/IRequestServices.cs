using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWorkServices
{
    public abstract class IRequestServices : IDisposable
    {
        protected IServices services;
        public IRequestServices(IServices services)
        {
            this.services = services;
            Init();
        }

        protected abstract void Init();
        public abstract void Dispose();


    }
}
