using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Assets.Script.Core.Server.Services
{
    public class SerReturnMsg
    {
        public static void ReturnError(ulong Client_id, string Msg)
        {
            var MsgEvent = new MsgEvent_Net();
            MsgEvent.msg = Msg;
            EventSystemToolExpand.Publish(SerEventTag.ReturnMsg, NetworkMsg_HandlerTag.Account, Client_id, MsgEvent);
        }
        public static void ReturnTokenError_BackLogin(ulong Client_id)
        {
            var MsgEvent = new MsgEvent_Net();
            MsgEvent.msg = "Data Error";
            EventSystemToolExpand.Publish(SerEventTag.ReturnErrorMsg_BackLogin, NetworkMsg_HandlerTag.Account, Client_id, MsgEvent);
        }

        public static void ReturnPlayerData(ulong Client_id, PlayerData_Net data)
        {
            EventSystemToolExpand.Publish(SerEventTag.ReturnPlayerData, NetworkMsg_HandlerTag.Account, Client_id, data);
        }

        //public static void Publish<T>(ushort tagId, string sendID, ulong targetId, T data) where T : INetworkSerializable
        //{
        //    var sd = new NetSendData(tagId, sendID, targetId);
        //    sd.data = data;
        //    EventSystemTool.EventSystem.Publish(sd);
        //}
    }
}
