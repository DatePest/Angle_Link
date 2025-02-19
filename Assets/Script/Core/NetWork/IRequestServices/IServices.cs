using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWorkServices
{
    public interface IServices
    {
        public Dictionary<string, NetRequest> NetRequests { get; set; }
        public static void TryAdd<T1, T2>(IServices Is, string Name, Network network) where T1 : IRequestSend where T2 : IRequestReceive
        {
            var send_ = Activator.CreateInstance(typeof(T1), new object[] { Name, network });
            var rec = Activator.CreateInstance(typeof(T2), new object[] { Name });

            Is.NetRequests.Add(Name, new NetRequest(network, (IRequestSend)send_, (IRequestReceive)rec));
        }
    }
}
