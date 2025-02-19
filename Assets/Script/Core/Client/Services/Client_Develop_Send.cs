using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetWorkServices;

namespace Client
{
    // Tag is GameEvent
    public class Client_Develop_Send : IRequestSend_Client
    {
        public Client_Develop_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }
        [EventTag(ClientEventTag.SendCharacterDevelopLv)]
        public void SendCharacterDevelopLv(NetSendData Sdata)    
        {
            var jdata = GameApi.ApiTool.ToJson(Sdata.data);
            SendAction(NetworkMsg_HandlerTag.RequestCharacterDevelop_Lv, jdata, TargetID);
        }


     
    }
}
