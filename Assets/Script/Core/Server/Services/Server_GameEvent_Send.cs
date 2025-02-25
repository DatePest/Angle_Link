using EventSystemTool;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.Script.Core.Server.Services
{
    public class Server_GameEvent_Send : IRequestSend_Server
    {
        public Server_GameEvent_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }
        [EventTag(SerEventTag.ReturnCharacterDevelop_Lv)] 
        public void ReturnMsg(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnCharacterDevelop_Lv, (Response_Net)Sdata.data, Sdata.TargetId);
        }

    }
}
