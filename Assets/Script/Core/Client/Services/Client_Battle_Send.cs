using EventSystemTool;
using GameApi;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

namespace Client
{
    public class Client_Battle_Send : IRequestSend_Client
    {
        public Client_Battle_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }

        [EventTag(ClientEventTag.SendBattleEnterRequest)]
        public  void SendBattleEnterRequest(NetSendData Sdata)
        {
            var data = (BattleEnterRequest)Sdata.data;
            var jdata = WebTool.ToJson(data);
            SendAction(NetworkMsg_HandlerTag.RequestBattleEnter, jdata, TargetID, NetworkDelivery.ReliableFragmentedSequenced);
        }
        [EventTag(ClientEventTag.SendOnGameStartToServerrRequest)]
        public void SendOnGameStartToServerrRequest(NetSendData Sdata) 
        {
            SendAction(NetworkMsg_HandlerTag.RequestBattleStart, WebTool.ToJson(Sdata.data), TargetID, NetworkDelivery.ReliableFragmentedSequenced);
        }
        [EventTag(ClientEventTag.SendSelectToServerRequest)]
        public void SendSelectToServerRequest(NetSendData Sdata)  
        {
            SendAction(NetworkMsg_HandlerTag.RequestBattlePlayerSelect, WebTool.ToJson(Sdata.data), TargetID, NetworkDelivery.ReliableFragmentedSequenced);
        }
        [EventTag(ClientEventTag.SendBattleEndSettlementRequest)]
        public void SendBattleEndReqDrop(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.RequestBattleEndSettlement, WebTool.ToJson(Sdata.data), TargetID, NetworkDelivery.ReliableFragmentedSequenced);
        }
        [EventTag(ClientEventTag.SendBattleData)]
        public void SendBattleData(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.RequestReceiveBattleData, WebTool.ToJson(Sdata.data), TargetID, NetworkDelivery.ReliableFragmentedSequenced);
        }
      

    }
}
