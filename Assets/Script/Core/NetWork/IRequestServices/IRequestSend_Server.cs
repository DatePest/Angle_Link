using Cysharp.Threading.Tasks;
using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


// Server Use
namespace NetWorkServices
{
    public class IRequestSend_Server : IRequestSend
    {
        public IRequestSend_Server(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
            serEventQueue = new();
        }

        WithLockQueueTool.WithLockQueue<EventHandle_NetSendData> serEventQueue;

        public override async void Request(NetSendData a)
        {
            if (a == null) return;
            if (a.SendTag != SendMsgTag) return;
            if (Network.NetManager.ConnectedClientsIds.Contains(a.TargetId))
            {
                var T = Find(a.EventId);
                if (T == null) return;
                var e = new EventHandle_NetSendData(T, this, a);
                await serEventQueue.Add(e);
            }
        }

        public override async Task Updata()
        {
            await serEventQueue.Excute();
        }

        public override void Dispose()
        {
            serEventQueue.Dispose();
            serEventQueue = null;
            base.Dispose();
        }

    }
}




