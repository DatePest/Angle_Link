using EventSystemTool;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Assets.Script.Core.Server.Services
{
    public class Server_Account_Send : IRequestSend_Server
    {
        public Server_Account_Send(string sendMsgTag, Network network) : base(sendMsgTag, network)
        {
        }
        [EventTag(SerEventTag.ReturnMsg)] // To Josn
        public void ReturnMsg(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnMsg, (MsgEvent_Net)Sdata.data, Sdata.TargetId);
        }
        [EventTag(SerEventTag.ReturnErrorMsg_BackLogin)] 
        public void ReturnErrorMsg_BackLogin(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnErrorMsg_BackLogin, (MsgEvent_Net)Sdata.data, Sdata.TargetId);
        }
        [EventTag(SerEventTag.Returnlogin)]
        public void Returnlogin(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.Returnlogin, (UserData)Sdata.data, Sdata.TargetId);
        }
        [EventTag(SerEventTag.ReturnReg)]
        public void ReturnReg(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnReg, (UserData)Sdata.data, Sdata.TargetId);
        }
        [EventTag(SerEventTag.ReturnPlayerData)]
        public void ReturnPlayerData(NetSendData Sdata)
        {
            SendAction(NetworkMsg_HandlerTag.ReturnGetPlayerData, (PlayerData_Net)Sdata.data, Sdata.TargetId,Unity.Netcode.NetworkDelivery.ReliableFragmentedSequenced);
        }
    }
}
