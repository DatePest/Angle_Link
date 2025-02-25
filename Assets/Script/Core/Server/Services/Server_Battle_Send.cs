using EventSystemTool;
using NetWorkServices;
using GameApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Script.Core.Server.Services
{
    public class Server_Battle_Send : IRequestSend_Server
    {
        public Server_Battle_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }

        [EventTag(SerEventTag.ReturnBattleEnterRequest)] 
        public void ReturnBattleEnterRequest(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnBattleEnterRequest, (Response_Net)Sdata.data, Sdata.TargetId,Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced);
        }
        //[EventTag(SerEventTag.ReturnBattleDataRequest)]
        //public void ReturnBattleDataRequest(NetSendData Sdata)
        //{
        //    SendAction(NetworkMsg_HandlerName.ReturnBattleStartRequest, (MsgLogs)Sdata.data, Sdata.TargetId);
        //}
        [EventTag(SerEventTag.ReturnBattleStartRequest)] 
        public void ReturnBattleStartRequest(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnBattleStartRequest, (MsgLogs)Sdata.data, Sdata.TargetId, Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced);
        }
        [EventTag(SerEventTag.ReturnBattlePlayerSelectRequest)] 
        public void ReturnBattlePlayerSelectRequest(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnBattlePlayerSelectRequest, (MsgLogs)Sdata.data, Sdata.TargetId, Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced);
        }
        [EventTag(SerEventTag.DataExpiredRequestCombatData)]
        public void DataExpiredRequestCombatData(NetSendData Sdata)
        {
            BattleEventLog Redata = BattleLogic.RequestData();
            SendAction(NetworkMsg_HandlerTag.Return_RequestToBattleData, WebTool.ToJson(Redata), Sdata.TargetId, Unity.Netcode.NetworkDelivery.Reliable);
        }
        //[EventTag(SerEventTag.DataExpiredBackHome)]
        //public void DataExpiredBackHome(NetSendData Sdata)
        //{
        //    SendAction(NetworkMsg_HandlerName.ReturnErrorMsg_BackLogin, WebTool.ToJson(Redata), Sdata.TargetId, Unity.Netcode.NetworkDelivery.Reliable);
        //}
        [EventTag(SerEventTag.ReturnBattleSettlement)]
        public void ReturnBattleSettlement(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnBattleEndSettlement, (MsgLogs)Sdata.data, Sdata.TargetId, Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced);
        }

    }
}
