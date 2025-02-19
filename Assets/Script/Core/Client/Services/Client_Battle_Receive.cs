using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Client.ClientScene;
using UnityEngine.SceneManagement;
using GameApi;
using NetWorkServices;

namespace Client
{
    public class Client_Battle_Receive : IRequestReceive
    {
        public Client_Battle_Receive(string listenMsgTag) : base(listenMsgTag)
        {
        }

        ClientUserData userData => Client.ClientRoot.Get().ClientUserData;
        [EventTag(NetworkMsg_HandlerTag.ReturnBattleEnterRequest)]
        public void BattleEnterResponse(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<Response_Net>();
           
            if (msg.success)
            {
                //var d = msg.GetDataJosn();
                var BattleData =ApiTool.JsonToObject<BattleData>(msg.DataJosn);
                userData.AddCache(GameConstant.OriginalBattleData, BattleData,true);
                 var u = UI_SceneLoad.Get();
                u.SceneLoad(ClientSceneName.Battle.ToString(), GameConstant.PackName_GameCore, LoadSceneMode.Single, false);
            }
            else
            {
                var u = Ui_SystemMsg.Get();
                u.WaitConfirm(msg.msg);
            }
        }
        [EventTag(NetworkMsg_HandlerTag.ReturnBattleStartRequest)]
        public void ReturnBattleStartRequest(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<MsgLogs>();
            EventSystem.Publish(new BattleMsgLogs(msg.GetData()));
        }
        [EventTag(NetworkMsg_HandlerTag.ReturnBattlePlayerSelectRequest)]
        public void ReturnBattlePlayerSelectRequest(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<MsgLogs>();
            EventSystem.Publish(new BattleMsgLogs(msg.GetData()));
        }
        [EventTag(NetworkMsg_HandlerTag.ReturnBattleEndSettlement)]
        public void ReturnBattleEndSettlement(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<MsgLogs>();
            EventSystem.Publish(new BattleMsgLogs(msg.GetData()));
        }
        [EventTag(NetworkMsg_HandlerTag.Return_RequestToBattleData)]
        public void ReceiveRequestBattleData(ReceiveNetSerializedData data)
        {
            //var Bdata =userData.GetCache<BattleData>(GameConstant.OriginalBattleData);
            BattleEventLog msg = ApiTool.JsonToObject<BattleEventLog>(data.NData.GetString());
            EventSystem.Publish(new BattleMsgLogs(msg));
        }
    }
}
