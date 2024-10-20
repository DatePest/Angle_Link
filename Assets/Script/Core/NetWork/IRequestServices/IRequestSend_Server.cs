using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// Server Use
public class IRequestSend_Server : IRequestSend
{
    public IRequestSend_Server(string sendMsgTag, Network network) : base(sendMsgTag, network)
    {
    }
    public override void Request(NetSendData a)
    {
        if (a == null) return;
        if (a.SendTag != SendMsgTag) return;

        if (Network.NetManager.ConnectedClientsIds.Contains(a.TargetId))
        {
            base.Request(a);
        }
    }
}