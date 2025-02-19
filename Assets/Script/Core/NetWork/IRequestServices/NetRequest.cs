using Client;
using EventSystemTool;
using System;
using System.Threading.Tasks;

namespace NetWorkServices
{
    public class NetRequest : IDisposable
    {
        Network network;
        NetworkMessaging Messaging => network.Messaging;

        public IRequestSend RequestSend;
        public IRequestReceive RequestReceive;
        Listener<NetSendData> listener;

        public NetRequest(Network network, IRequestSend requestSend, IRequestReceive requestReceive)
        {
            this.network = network;
            RequestSend = requestSend;
            RequestReceive = requestReceive;
            Messaging.ListenMsg(RequestReceive.ListenMsgTag, RequestReceive.Request);
            listener = new Listener<NetSendData>(EventSend);
        }
        void EventSend(IEventTag sendData)
        {
            RequestSend.Request((NetSendData)sendData);
        }
        public void Destory()
        {
            Messaging?.UnListenMsg(RequestReceive.ListenMsgTag);
            listener.Stop();
        }

        public async Task Updata()
        {
            await RequestSend.Updata();
        }

        public void Dispose()
        {
            network = null;
            RequestSend.Dispose();
            RequestSend = null;
            RequestReceive.Dispose();
            RequestReceive = null;
            Destory();
            listener = null;
        }
        #region ToSend
        //public void SendAction<T>(ushort type, T data, NetworkDelivery delivery = NetworkDelivery.Reliable) where T : INetworkSerializable
        //{
        //    FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
        //    writer.WriteValueSafe(type);
        //    writer.WriteNetworkSerializable(data);
        //    Messaging.Send(SendMsgTag, ServerID, writer, delivery);
        //    writer.Dispose();
        //}

        //public void SendAction(ushort type, int data)
        //{
        //    FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
        //    writer.WriteValueSafe(type);
        //    writer.WriteValueSafe(data);
        //    Messaging.Send(SendMsgTag, ServerID, writer, NetworkDelivery.Reliable);
        //    writer.Dispose();
        //}

        //public void SendAction(ushort type)
        //{
        //    FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
        //    writer.WriteValueSafe(type);
        //    Messaging.Send(SendMsgTag, ServerID, writer, NetworkDelivery.Reliable);
        //    writer.Dispose();
        //}
        #endregion

        #region Register old  Not used  Please refer to "IRequestReceive"
        //--- Register----------------------
        //private Dictionary<ushort, RefreshEvent> registered_commands = new Dictionary<ushort, RefreshEvent>();
        //public class RefreshEvent
        //{
        //    public ushort tag;
        //    public UnityAction<NetSerializedData> callback;
        //}
        //private void RegisterRefresh(ushort tag, UnityAction<NetSerializedData> callback)
        //{
        //    RefreshEvent cmdevt = new RefreshEvent();
        //    cmdevt.tag = tag;
        //    cmdevt.callback = callback;
        //    registered_commands.Add(tag, cmdevt);
        //}

        //public void OnReceiveRefresh(ulong client_id, FastBufferReader reader)
        //{
        //    reader.ReadValueSafe(out ushort type);
        //    bool found = registered_commands.TryGetValue(type, out RefreshEvent command);
        //    if (found)
        //    {
        //        command.callback.Invoke(new NetSerializedData(reader));
        //    }
        //}
        #endregion

    }
}