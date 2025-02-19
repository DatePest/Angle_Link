using Assets.Script.Core.GameDevelop;
using EventSystemTool;
using GameApi;
using NetWorkServices;
using System;
using Tools;
using static Assets.Script.Core.Server.ServerGameLogic;

namespace Assets.Script.Core.Server.Services
{
    public class Server_Account_Receive : IRequestServerReceive
    {
        public Server_Account_Receive(string listenMsgTag) : base(listenMsgTag) { }

        [EventTag(NetworkMsg_HandlerTag.Test)]
        public void ReceiveTest(ReceiveNetSerializedData Rdata)
        {
            var data = Rdata.NData.GetString();
            var msg = ApiTool.JsonToObject<MsgEvent_Net>(data);
            UnityEngine.Debug.Log($"mgs : {msg.msg} _ ID = {msg.MsgId}");
        }

        [EventTag(NetworkMsg_HandlerTag.RequestRegister)]
        public async void ReceiveRegister(ReceiveNetSerializedData Rdata)
        {
            var auth = new Authenticator_API();
            var data = Rdata.NData.GetString();
            var Request = ApiTool.JsonToObject<RegisterRequest>(data);
            var b = await auth.Register(Request);
            if (b)
            {
                var Udata = await auth.LoadUserPlayerData();

                //New account initial information
                await PlayerDataLogic.AddCharacter(Udata, new[] { "A001", "A002" });
                Udata.Stamina = Develop_PlayerLv.GetStaminaMax(1);
                Udata.LastUpDataTime = ApiTool.ToJson(DateTime.Now);
                //
                await auth.TokenSavePlayerData(Udata);
                var U = auth.GetUserData();
                EventSystemToolExpand.Publish(SerEventTag.Returnlogin, NetworkMsg_HandlerTag.Account, Rdata.Client_id, U);

            }
            else
            {
                SerReturnMsg.ReturnError(Rdata.Client_id, auth.GetError());
            }
        }
        [EventTag(NetworkMsg_HandlerTag.RequestLogin)]
        public async void ReceiveLogin(ReceiveNetSerializedData Rdata)
        {
            var auth = new Authenticator_API();
            var data = Rdata.NData.GetString();
            var Request = ApiTool.JsonToObject<LoginRequest>(data);
            var b = await auth.Login(Request.username,Request.password);
            if (b)
            {
                var U = auth.GetUserData();
                EventSystemToolExpand.Publish(SerEventTag.Returnlogin, NetworkMsg_HandlerTag.Account, Rdata.Client_id, U);
            }
            else
            {
                SerReturnMsg.ReturnError(Rdata.Client_id, auth.GetError());
            }
        }
        [EventTag(NetworkMsg_HandlerTag.RequestGetPlayerData)]
        public async void ReceiveGetPlayerData(ReceiveNetSerializedData Rdata)
        {
            var Request = ApiTool.JsonToObject<UserGetPlayerDataRequest>(Rdata.NData.GetString());
            Api_PlayerData Udata = await PlayerDataLogic.TokenGetPlayerData(Request.accesLogin_token, Rdata.Client_id,false);
            if(Udata == null)   return;

            if (await PlayerDataLogic.UpdataAndSave(Udata, Request.accesLogin_token, Rdata.Client_id))
            SerReturnMsg.ReturnPlayerData(Rdata.Client_id,PlayerDataLogic.DTO_Netdata(Udata));
        }
    } 
}