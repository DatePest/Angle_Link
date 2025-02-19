using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Netcode;

namespace NetWorkServices
{
    public abstract class IRequestSend : IEventBase<EventTagAttribute>, IDisposable
    {
        public Action<string, ushort, object> ReceiveSendRequest;
        public Action<string, ushort> EndCallback;
        public string SendMsgTag;
        protected Network Network;
        protected NetworkMessaging Messaging;
        public IRequestSend(string sendMsgTag, Network network)
        {
            SendMsgTag = sendMsgTag;
            Network = network;
            Messaging = network.Messaging;
        }

        // Example
        //[EventTag($$$$)]  //Use Server/Client EventTag
        //public void Example(SendData Sdata)
        //{
        //    RegisterRequest data = (RegisterRequest)a.data;
        //    XXX.msg = msg;
        //    XXX.player_id = player_id;
        //    SendAction(Receive Msg Tage, data,TargetID); 
        //}
        public virtual void Dispose()
        {
            ReceiveSendRequest = null;
            EndCallback = null;
            SendMsgTag = null;
            Network = null;
            Messaging = null;
        }
        public virtual void Request(NetSendData a)
        {
            if (a.SendTag != SendMsgTag) return;
            ReceiveSendRequest?.Invoke(SendMsgTag, a.EventId, a);
            FindRun(a.EventId, a);
            EndCallback?.Invoke(SendMsgTag, a.EventId);
        }

        public abstract Task Updata();
        #region SendAction

        protected void SendAction<T>(ushort type, T data, ulong TargetID, NetworkDelivery delivery = NetworkDelivery.Reliable) where T : INetworkSerializable
        {
            FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
            writer.WriteValueSafe(type);
            writer.WriteNetworkSerializable(data);
            Messaging.Send(SendMsgTag, TargetID, writer, delivery);
            writer.Dispose();
        }

        protected void SendAction(ushort type, int data, ulong TargetID)
        {
            FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
            writer.WriteValueSafe(type);
            writer.WriteValueSafe(data);
            Messaging.Send(SendMsgTag, TargetID, writer, NetworkDelivery.Reliable);
            writer.Dispose();
        }
        protected void SendAction(ushort type, string data, ulong TargetID, NetworkDelivery delivery = NetworkDelivery.Reliable)
        {
            FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
            writer.WriteValueSafe(type);
            writer.WriteValueSafe(data);
            Messaging.Send(SendMsgTag, TargetID, writer, delivery);
            writer.Dispose();

        }

        protected void SendAction(ushort type, ulong TargetID)
        {
            FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
            writer.WriteValueSafe(type);
            Messaging.Send(SendMsgTag, TargetID, writer, NetworkDelivery.Reliable);
            writer.Dispose();
        }


        #endregion
      
    }
    public class NetSendData : IEventTag
    {
        public ushort EventId { get; set; } // SerEventTag
        public string SendTag; // NetworkMsg_HandlerName
        public ulong TargetId;
        public object data;
        public NetSendData(ushort tagId, string sendID, ulong targetId)
        {
            EventId = tagId;
            SendTag = sendID;
            TargetId = targetId;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is NetSendData))
                return false;

            NetSendData other = (NetSendData)obj;

            return EventId == other.EventId &&
                   SendTag == other.SendTag &&
                   TargetId == other.TargetId &&
                   Equals(data, other.data);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + EventId.GetHashCode();
            hash = hash * 31 + (SendTag != null ? SendTag.GetHashCode() : 0);
            hash = hash * 31 + TargetId.GetHashCode();
            hash = hash * 31 + (data != null ? data.GetHashCode() : 0);
            return hash;
        }
    }

    public class EventHandle_NetSendData : WithLockQueueTool.IEventHandle
    {
        public NetSendData data;

        public EventHandle_NetSendData(MethodInfo method, object methodInstance, NetSendData data) : base(method, methodInstance)
        {
            this.data = data;
        }
        public override void Excute()
        {
            method.Invoke(MethodInstance, new object[] { data });
        }
    }
}