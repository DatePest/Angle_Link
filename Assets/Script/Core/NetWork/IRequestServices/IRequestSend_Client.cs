using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static NetworkSetting;

// Client Use
public abstract class IRequestSend_Client : IRequestSend
{
    protected ulong TargetID;
    static List<NetSendData> sendData = new();
    static bool WatiConnect = false;
    public IRequestSend_Client(string sendMsgTag, Network network) : base(sendMsgTag, network)
    {
        TargetID = network.ServerID;
    }
    public override void Request(NetSendData a)
    {
        if (a == null ) return;
        if (a.SendTag != SendMsgTag) return;
        if(sendData.Contains(a)) return;
        ReceiveSendRequest?.Invoke(SendMsgTag, a.EventId);
        sendData.Add(a);
        ConnectToServer();
    }
    public virtual async void ConnectToServer()
    {
        UnityEngine.Debug.Log("ConnectToServer");
        if (WatiConnect) return;
        await Task.Yield(); //Wait for initialization to finish
        if (Network.IsActive())
        {
            Exceut();
            return; // Already connected
        }
        if (Network.data.solo_type == SoloType.Offline)
        {
            Network.onConnect += Connect;
            WatiConnect = true;
            Network.StartHostOffline();
            //WebGL dont support hosting a game, must join a dedicated server, in solo it starts a offline mode that doesn't use netcode at all
        }
        else
        {
            Network.onConnect += Connect;
            WatiConnect = true;
            UnityEngine.Debug.Log("StartClient");
            Network.StartClient(Network.data.ServerUrl, Network.data.port);  //Connect server
        }
    }
    void Connect()
    {
        Network.onConnect -= Connect;
        Exceut();
        WatiConnect = false;
    }
    void Exceut()
    {
        if (sendData.Count <= 0) return;
        foreach (NetSendData a in sendData)
        {
            FindRun(a.EventId, a);
            EndCallback?.Invoke(SendMsgTag, a.EventId);
        }
        sendData.Clear();
    }
    public static void ClearSendData()
    {
        sendData.Clear();
        WatiConnect = false;
    }
}