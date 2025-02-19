using EventSystemTool;
using GameApi;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client_Account_Send : IRequestSend_Client
    {
        public Client_Account_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }
        [EventTag(ClientEventTag.Test)]
        public void Test(NetSendData Sdata)    
        {
            UnityEngine.Debug.Log("Test");
            var data = (MsgEvent_Net)Sdata.data;
            var jdata = ApiTool.ToJson(data);
            SendAction(NetworkMsg_HandlerTag.Test, jdata, TargetID);
        }


        [EventTag(ClientEventTag.SendLogin)]
        public void Login(NetSendData Sdata)    // ApiMsg LoginRequest 
        {
            var data = (LoginRequest)Sdata.data;
            var jdata = ApiTool.ToJson(data);
            SendAction(NetworkMsg_HandlerTag.RequestLogin, jdata, TargetID);
        }
        [EventTag(ClientEventTag.SendRegister)]
        public void Register(NetSendData Sdata)  // ApiMsg RegisterRequest 
        {
            var data = (RegisterRequest)Sdata.data;
            var jdata = ApiTool.ToJson(data);
            SendAction(NetworkMsg_HandlerTag.RequestRegister, jdata, TargetID);
        }
        [EventTag(ClientEventTag.SendGetUserData)]
        public void GetUserData(NetSendData Sdata)  // ApiMsg GetUserDataRequest 
        {
            var data = (UserGetPlayerDataRequest)Sdata.data;
            var jdata = ApiTool.ToJson(data);
            SendAction(NetworkMsg_HandlerTag.RequestGetPlayerData, jdata, TargetID);
        }
    }
}
