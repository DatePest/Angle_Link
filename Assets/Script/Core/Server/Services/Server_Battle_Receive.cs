using Cysharp.Threading.Tasks;
using EventSystemTool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tools;
using GameApi;
using static Assets.Script.Core.Server.ServerBattleManager;
using static Assets.Script.Core.Server.ServerGameLogic;
using NetWorkServices;

namespace Assets.Script.Core.Server.Services
{
    public class Server_Battle_Receive : IRequestServerReceive
    {
        public Server_Battle_Receive(string listenMsgTag) : base(listenMsgTag)
        {
            BattleManager = ServerRoot.Get().serverBattleManager;
        }
        ServerBattleManager BattleManager;
        [EventTag(NetworkMsg_HandlerTag.RequestBattleEnter)]
        public async void ReceiveBattleEnterRequest(ReceiveNetSerializedData Rdata)
        {
            var data = Rdata.NData.GetString();
            var Req = WebTool.JsonToObject<BattleEnterRequest>(data);
            Api_PlayerData Udata = await PlayerDataLogic.TokenGetPlayerData(Req.accesLogin_token, Rdata.Client_id);
            if (Udata == null) return;
            var LevelData=await AssetFInd.GetGameLevelData_Async(Req.GameLevelDataName);
            if (LevelData == null)
            {
                SerReturnMsg.ReturnError(Rdata.Client_id, "Error");
                return; 
            }

            Response_Net Msg;
            var b =PlayerDataLogic.TryChange_Stamina(Udata, LevelData.Stamina);
            if (b)
            {
                if (!await PlayerDataLogic.SaveData(Udata, Req.accesLogin_token))
                {
                    SerReturnMsg.ReturnTokenError_BackLogin(Rdata.Client_id);
                    return;
                }
                Msg = new Response_Net() { success = true };
                var GameSettng =  BattleSettings.New(Req.team, LevelData);
                var Bs =await BattleManager.NewBattle_Async(GameSettng);
                Msg.DataJosn = Bs.GetGameDataToJosn();
            }
            else
            {
                Msg = new Response_Net() { success = false, msg = "Not enough Stamina"};
            }
            EventSystemToolExpand.Publish(SerEventTag.ReturnBattleEnterRequest, NetworkMsg_HandlerTag.Battle, Rdata.Client_id, Msg);

        }

        [EventTag(NetworkMsg_HandlerTag.RequestBattleStart)]
        public async void ReceiveBattleStart(ReceiveNetSerializedData Rdata)
        {
            // await UniTask.SwitchToMainThread();
            var Req = WebTool.JsonToObject<BattleRequest>(Rdata.NData.GetString());
            var Rresult = await BattleManager.TryGet_GameStart(Req.GameUid);
            if (Rresult.Success)
            {
                EventSystemToolExpand.Publish(SerEventTag.ReturnBattleStartRequest, NetworkMsg_HandlerTag.Battle, Rdata.Client_id, Rresult.BattleServer_.GetCurrentLogs());
            }
            else
            {
                await GameUtilityTool.DebugAsync("DataExpiredRequestCombatData");
                BattleManager.AddHandle(ServerBattleWaitHandle.New_Start(Req.GameUid));

                EventSystemToolExpand.Publish(SerEventTag.DataExpiredRequestCombatData, NetworkMsg_HandlerTag.Battle, Rdata.Client_id, "DataExpiredRequestCombatData");
            }
            Rresult.Dispose();
        }
        [EventTag(NetworkMsg_HandlerTag.RequestBattlePlayerSelect)]
        public async void ReceiveBattlePlayerSelect(ReceiveNetSerializedData Rdata)
        {
            var Req = WebTool.JsonToObject<BattlePlayerSelectRequest>(Rdata.NData.GetString());
            var Rresult = await BattleManager.TryGet_GameSelect(Req.GameUid, Req.GetSelectOrders());
            if (Rresult.Success)
            {
                EventSystemToolExpand.Publish(SerEventTag.ReturnBattlePlayerSelectRequest, NetworkMsg_HandlerTag.Battle, Rdata.Client_id, Rresult.BattleServer_.GetCurrentLogs());
            }
            else
            {
                await GameUtilityTool.DebugAsync("DataExpiredRequestCombatData");
                BattleManager.AddHandle(ServerBattleWaitHandle.New_ReceiveSelect(Req.GameUid, Req.GetSelectOrders()));
                EventSystemToolExpand.Publish(SerEventTag.DataExpiredRequestCombatData, NetworkMsg_HandlerTag.Battle,Rdata.Client_id, "DataExpiredRequestCombatData");
            }
            Rresult.Dispose();
        }

        [EventTag(NetworkMsg_HandlerTag.RequestBattleEndSettlement)]
        public async void ReceiveBattleEndGetDrop(ReceiveNetSerializedData Rdata)
        {
            var Req = WebTool.JsonToObject<BattleRequest>(Rdata.NData.GetString());
            var Rresult = await BattleManager.TryGet_GameEndToGetDrop(Req.GameUid);
            if (Rresult.Success)
            {
                Api_PlayerData Udata = await PlayerDataLogic.TokenGetPlayerData(Req.accesLogin_token, Rdata.Client_id);

                var Temp = Rresult.TempData as SettlementDatas_Net;
                Udata.AddExp(Temp.Exp);
                Udata.AddMoney(Temp.Money);
                Udata.AddItem(Temp.Items);
                Udata.AddCharacters(Temp.CharactersNets);
                //Todo : Duplicate character switching props 2
                if (!await PlayerDataLogic.SaveData(Udata, Req.accesLogin_token))
                {
                    SerReturnMsg.ReturnTokenError_BackLogin(Rdata.Client_id);
                    Rresult.Dispose();

                    return;
                }
                var b = new BattleEventLog();
                b.ID = BattleEventTag.ToSettlement;
                b.data = Temp;
                try
                {
                    var m = MsgLogs.New(b);
                    EventSystemToolExpand.Publish(SerEventTag.ReturnBattleSettlement, NetworkMsg_HandlerTag.Battle, Rdata.Client_id, m);
                }
                catch(Exception e)
                {
                    GameUtilityTool.DebugErrorAsync(e.Message);
                }
            }
            else
            {
                var Msg = new MsgEvent_Net() {msg = "The data has been settled and expired" };
                EventSystemToolExpand.Publish(SerEventTag.ReturnErrorMsg_BackLogin, NetworkMsg_HandlerTag.Account, Rdata.Client_id, Msg);
            }
            Rresult.Dispose();
        }
        [EventTag(NetworkMsg_HandlerTag.RequestReceiveBattleData)]
        public async void ReceiveBattleData(ReceiveNetSerializedData Rdata)
        {
            var Req = WebTool.JsonToObject<BattleData>(Rdata.NData.GetString());
            var Ser =await BattleManager.NewBattle_Async(Req);
            var Handle = await　BattleManager.FindHandle(Req.Uid);
            await Handle.Excute(Ser,true);
            EventSystemToolExpand.Publish(SerEventTag.ReturnBattlePlayerSelectRequest, NetworkMsg_HandlerTag.Battle, Rdata.Client_id, Ser.GetCurrentLogs());
        }
    }
}
