using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.GraphicsBuffer;
namespace Server
{
    public class Server_Account_Send : IRequestSend_Server
    {
        public Server_Account_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }
        [EventTag(SerEventTag.ReturnMsg)] // To Josn
        public void ReturnMsg(NetSendData Sdata)
        {
            var data = (MsgEvent)Sdata.data;
            SendAction(NetworkMsg_HandlerName.ReturnMsg, data, Sdata.TargetId);
        }
        [EventTag(SerEventTag.Returnlogin)]
        public void Returnlogin(NetSendData Sdata)
        {
            var data = (UserData)Sdata.data;
            SendAction(NetworkMsg_HandlerName.Returnlogin, data, Sdata.TargetId);
        }
    }
}
