using Assets.Script.Core.Server.Services;
using EventSystemTool;
using System;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
namespace NetWorkServices
{
    /// <summary>
    /// Reflection is used to automatically register, providing convenient development
    /// </summary>
    public abstract class IRequestReceive : EventSystemTool.IEventBase<EventTagAttribute>, IDisposable
    {
        public string ListenMsgTag;
        public Action<string, ushort> EndCallback;
        public IRequestReceive(string listenMsgTag) : base()
        {
            ListenMsgTag = listenMsgTag;
        }
        // Example
        //[EventTag($$$$)] //Use NetworkMsg_HandlerName Receive Msg Tag
        //public void Example(ReceiveNetSerializedData data)
        //{
        //    var msg = data.NData.Get<T>();
        //}
        public virtual void Request(ulong client_id, FastBufferReader reader)
        {

            reader.ReadValueSafe(out ushort tag);
            Debug.Log($"Request {tag}");
            var data = new NetSerializedData(reader);
            FindRun(tag, new ReceiveNetSerializedData(client_id, data));
            EndCallback?.Invoke(ListenMsgTag, tag);

        }

        public virtual void Dispose()
        {
            ListenMsgTag = null;
            EndCallback = null;
        }
    }
    public abstract class IRequestServerReceive : IRequestReceive
    {
        public IRequestServerReceive(string listenMsgTag) : base(listenMsgTag)
        {
        }

        public override void Request(ulong client_id, FastBufferReader reader)
        {
            try
            {
                Task.Run(() => base.Request(client_id, reader));
            }
            catch (Exception ex)
            {
                var msg = $"Server Request failed: {ex.Message}";
                GameUtilityTool.DebugErrorAsync(msg);
                SerReturnMsg.ReturnError(client_id, msg);
            }
        }
    }
}