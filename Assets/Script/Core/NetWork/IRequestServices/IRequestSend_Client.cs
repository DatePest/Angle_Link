using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static NetworkSetting;

// Client Use
namespace NetWorkServices
{
    public abstract class IRequestSend_Client : IRequestSend
    {
        protected ulong TargetID;

        public IRequestSend_Client(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
            TargetID = network.ServerID;
        }
        public override void Request(NetSendData a)
        {
            if (a == null) return;
            if (a.SendTag != SendMsgTag) return;
            //if (sendData.Contains(a)) return;
            var T = Find(a.EventId);
            if (T == null) return;
            var e = new EventHandle_NetSendData(T, this, a);
            ReceiveSendRequest?.Invoke(SendMsgTag, a.EventId, e);
            e.CallBack += () => EndCallback?.Invoke(SendMsgTag, a.EventId);

        }
        public override Task Updata()
        {
            return default;
        }
    }
}