using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NetWorkServices
{
    public abstract class IServices
    {
        public Dictionary<string, NetRequest> NetRequests { get; set; } = new();
        public IServices()
        {
            NetRequestsAdd();
        }
        public void TryAdd<T1, T2>(string Name, Network network) where T1 : IRequestSend where T2 : IRequestReceive
        {
            var send_ = Activator.CreateInstance(typeof(T1), new object[] { Name, network });
            var rec = Activator.CreateInstance(typeof(T2), new object[] { Name });

            NetRequests.Add(Name, new NetRequest(network, (IRequestSend)send_, (IRequestReceive)rec));
        }
        protected abstract void NetRequestsAdd();
    }
}
