using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Reflection is used to automatically register, providing convenient development
/// </summary>
public abstract class IRequestReceive : EventSystemTool.IEventBase<EventTagAttribute>
{
    public readonly string ListenMsgTag;
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
        var data = new NetSerializedData(reader);
        FindRun(tag, new ReceiveNetSerializedData(client_id, data));
        EndCallback?.Invoke(ListenMsgTag, tag);
        Debug.Log($"Request {tag}");
    }
}
