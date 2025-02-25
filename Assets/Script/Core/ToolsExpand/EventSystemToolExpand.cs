using Client;
using GameApi;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

namespace Tools
{
    public class EventSystemToolExpand
    {
        public static void Publish<T>(ushort tagId, string sendID, ulong targetId, T data) where T : INetworkSerializable
        {
            var sd = new NetSendData(tagId, sendID, targetId);
            sd.data = data;
            EventSystemTool.EventSystem.Publish(sd);
        }
        public static void Publish(ushort tagId, string sendID, ulong targetId, string data)
        {
            var msg = new MsgEvent_Net();
            msg.msg = data;
            Publish(tagId, sendID, targetId, msg);
        }
        //  This cannot be used for Ser To Client  Plz Use Publish_INet ! 
        //public static void Publish(ushort tagId, string sendID, ulong targetId, string Jsondata)
        //{
        //    var sd = new NetSendData(tagId, sendID, targetId);
        //    sd.data = Jsondata;
        //    EventSystemTool.EventSystem.Publish(sd);
        //}
        public static void Publish(ushort tagId, string sendID, ulong targetId, object data)
        {
            var sd = new NetSendData(tagId, sendID, targetId);
            sd.data = data;
            EventSystemTool.EventSystem.Publish(sd);

        }
        public static void Publish(ushort tagId, string sendID, ulong targetId, IRequest data)
        {
            if (data.accesLogin_token == null)
            {
                data.accesLogin_token = ClientRoot.GetToken();
            }
            var sd = new NetSendData(tagId, sendID, targetId);
            sd.data = data;
            EventSystemTool.EventSystem.Publish(sd);
        }
    }
}
