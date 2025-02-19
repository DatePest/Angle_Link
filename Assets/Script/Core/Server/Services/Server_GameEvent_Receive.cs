using Assets.Script.Core.GameDevelop;
using EventSystemTool;
using GameApi;
using JetBrains.Annotations;
using NetWorkServices;
using System;
using Tools;
using static Assets.Script.Core.Server.ServerGameLogic;

namespace Assets.Script.Core.Server.Services
{
    public class Server_GameEvent_Receive : IRequestServerReceive
    {
        public Server_GameEvent_Receive(string listenMsgTag) : base(listenMsgTag) { }

        [EventTag(NetworkMsg_HandlerTag.RequestCharacterDevelop_Lv)]
        public async void ReceiveTest(ReceiveNetSerializedData Rdata)
        {
            var Req = ApiTool.JsonToObject<ItemListRequst>(Rdata.NData.GetString());
            //GameUtilityTool.DebugAsync(Rdata.NData.GetString());
            Api_PlayerData Udata = await PlayerDataLogic.TokenGetPlayerData(Req.accesLogin_token, Rdata.Client_id);

            if (Req.ItemList1.Count == 0) return;

            var exp =Develop_CharacterLv.GetExp(Req.ItemList1);
            Udata.AddCharacterToDevelop((string)Req.Arg1, exp);
            Udata.RemoveItem(Req.ItemList1);
            if (!await PlayerDataLogic.SaveData(Udata, Req.accesLogin_token))
            {
                return;
            }

            EventSystemToolExpand.Publish(SerEventTag.ReturnCharacterDevelop_Lv, NetworkMsg_HandlerTag.GameEvent, Rdata.Client_id, "Error");
        }
    } 
}