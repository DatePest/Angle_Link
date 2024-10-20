using EventSystemTool;
using System.Diagnostics;
using static IRequestSend;

namespace Server
{
    public class Server_Account_Receive : IRequestReceive
    {
        public Server_Account_Receive(string listenMsgTag) : base(listenMsgTag) { }

        [EventTag(NetworkMsg_HandlerName.Test)]
        public void ReceiveTest(ReceiveNetSerializedData Rdata)
        {
            var data = Rdata.NData.GetString();
            var msg = ApiTool.JsonToObject<MsgEvent>(data);
            UnityEngine.Debug.Log($"mgs : {msg.msg} _ ID = {msg.MsgId}");
        }

        [EventTag(NetworkMsg_HandlerName.Register)]
        public void ReceiveRegister(ReceiveNetSerializedData Rdata)
        {
        }
        [EventTag(NetworkMsg_HandlerName.Login)]
        public async void ReceiveLogin(ReceiveNetSerializedData Rdata)
        {
            IAuthenticator auth = new Authenticator_API();
            var data = Rdata.NData.GetString();
            var loginRequest = ApiTool.JsonToObject<LoginRequest>(data);
            var b = await auth.Login(loginRequest.username);
            if (b)
            {
                UserData userdata = await auth.LoadUserData();

                var sd = new NetSendData(SerEventTag.Returnlogin,NetworkMsg_HandlerName.Account, Rdata.Client_id);
                sd.data = userdata;
                EventSystemTool.EventSystem.Publish(sd);
            }
            else
            {
                var Msg = new MsgEvent();
                Msg.msg = auth.GetError();

                var sd = new NetSendData(SerEventTag.ReturnMsg, NetworkMsg_HandlerName.Account, Rdata.Client_id);
                sd.data = Msg;
                EventSystemTool.EventSystem.Publish(sd);
            }
        }
        [EventTag(NetworkMsg_HandlerName.GetPlayerData)]
        public async void ReceiveGetPlayerData(ReceiveNetSerializedData Rdata)
        {
            var data = Rdata.NData.GetString();
            var Request = ApiTool.JsonToObject<UserGetPlayerDataRequest>(data);
            IAuthenticator auth = new Authenticator_API();
            var Udata = await Authenticator_API.TokenGetUserData(Request);
            if(Udata == default)
            {
                var Msg = new MsgEvent();
                Msg.msg = auth.GetError();    //Expired or does not need to be updated

                var sd = new NetSendData(SerEventTag.ReturnMsg, NetworkMsg_HandlerName.Account, Rdata.Client_id);
                sd.data = Msg;
                EventSystemTool.EventSystem.Publish(sd);
            }
            else
            {
                UserData userdata = await auth.LoadUserData();
                var sd = new NetSendData(SerEventTag.ReturnPlayerData, NetworkMsg_HandlerName.Account, Rdata.Client_id);
                sd.data = userdata;
                EventSystemTool.EventSystem.Publish(sd);
            }
           
        }



    } 
}