using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;
using static Unity.Netcode.Transports.UTP.UnityTransport;
/// <summary>
/// Main script handling network connection betweeen server and client
/// It's one of the few scripts in this asset that needs to be on a DontDestroyOnLoad object
/// </summary>


[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(NetworkManager))]
[RequireComponent(typeof(CustomTransport))]

public class Network : MonoBehaviour
{
    [field: SerializeField]
    public NetworkSetting data { get; private set; }

    //Server & Client events
    public UnityAction onTick; //Every network tick
    public UnityAction onConnect;  //Event when self connect, happens before onReady, before sending any data
    public UnityAction onDisconnect; //Event when self disconnect
    //Client only events
    public UnityAction<ClientFailedConnectEvent> onClientFailedConnect; //Client Events Connection Errors
    //Server only events
    public UnityAction<ulong> onClientConnect; //Server event when any client connect
    public UnityAction<ulong> onClientDisconnect; //Server event when any client disconnect

    public delegate bool ApprovalEvent(ulong client_id, ConnectionData connect_data);
    public ApprovalEvent checkApproval; //Additional approval validations for when a client connects



    //---------

    public NetworkManager NetManager { get; private set; }
    public CustomTransport CoustmTransport { get; private set; }
    public NetworkMessaging Messaging { get; private set; }
    public IAuthenticator Auth { get; private set; }
    public ConnectionData Connection { get; private set; }

    [System.NonSerialized]
    private static bool inited = false;
    private static Network instance;

    public const int Msg_size = 1024 * 1024;
    private bool offline_mode = false;
    private bool connected = false;
    UnityTransport transport;

    //---------
    public static Network Get()
    {
        if (instance == null)
        {
            //Network net = FindFirstObjectByType<Network>();
            //net?.Init();
            var i = GameObject.Instantiate(Resources.Load<GameObject>("NetWork")).GetComponent<Network>();
            instance = i;
        }
        return instance;
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }
        Init();
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        if (!inited || CoustmTransport == null)
        {
            instance = this;
            inited = true;
            NetManager = GetComponent<NetworkManager>();
            CoustmTransport = GetComponent<CustomTransport>();
            transport = GetComponent<UnityTransport>();
            Messaging = new NetworkMessaging(this);
            Connection = new ConnectionData();
            CoustmTransport.Init();
            NetManager.ConnectionApprovalCallback += ApprovalCheck;
            NetManager.OnClientConnectedCallback += OnClientConnect;
            NetManager.OnClientDisconnectCallback += OnClientDisconnect;
            NetManager.NetworkConfig.NetworkTransport = transport;
            NetManager.NetworkConfig.ConnectionApproval = false;
            transport.ClientFailedConnect+= ClientFailedConnect;
            //InitAuth();
        }
    }

    
    private async void InitAuth()
    {
        Auth = IAuthenticator.Create(data.auth_type);
        await Auth.Initialize();
    }
    //Start a host (client + server)
    public void StartHost(ushort port)
    {
        Debug.Log("Host Server Port " + port);
        CoustmTransport.SetServer(port);
        //Connection.user_id = Auth.UserID;
        //Connection.username = Auth.Username;
        NetManager.NetworkConfig.ConnectionData = NetworkTool.NetSerialize(Connection);
        offline_mode = false;
        NetManager.StartHost();
        AfterConnected();
    }

    //Start a dedicated server
    public void StartServer(ushort port)
    {
        Debug.Log("Start Server Port " + port);
        CoustmTransport.SetServer(port);
        Connection.user_id = "";
        Connection.username = "";
        NetManager.NetworkConfig.ConnectionData = NetworkTool.NetSerialize(Connection);
        offline_mode = false;
        NetManager.StartServer();
        AfterConnected();
    }

    //If is_host is set to true, it means this player created the game on a dedicated server
    //so its still a client (not server) but is the one who selected game settings
    public void StartClient(string server_url, ushort port)
    {
        //Debug.Log("Join Server: " + server_url + " " + port);
        CoustmTransport.SetClient(server_url, port);
        //Connection.user_id = Auth.UserID;
        //Connection.username = Auth.Username;
        NetManager.NetworkConfig.ConnectionData = NetworkTool.NetSerialize(Connection);
        offline_mode = false;
        NetManager.StartClient();
    }

    //Start simulated host with all networking turned off
    public void StartHostOffline()
    {
        Debug.Log("Host Offline");
        Disconnect();
        offline_mode = true;
        AfterConnected();
    }

    public void Disconnect()
    {
        if (!IsClient && !IsServer)
            return;

        Debug.Log("Disconnect");
        NetManager.Shutdown();
        AfterDisconnected();
    }
# region SetConnectionExtraData
    public void SetConnectionExtraData(byte[] bytes)
    {
        Connection.extra = bytes;
    }

    public void SetConnectionExtraData(string data)
    {
        Connection.extra = NetworkTool.SerializeString(data);
    }

    public void SetConnectionExtraData<T>(T data) where T : INetworkSerializable, new()
    {
        Connection.extra = NetworkTool.NetSerialize(data);
    }
    #endregion

    private void AfterConnected()
    {
        if (connected)
            return;

        if (NetManager.NetworkTickSystem != null)
            NetManager.NetworkTickSystem.Tick += OnTick;
        connected = true;
        onConnect?.Invoke();
    }
    private void AfterDisconnected()
    {
        if (!connected)
            return;

        if (NetManager.NetworkTickSystem != null)
            NetManager.NetworkTickSystem.Tick -= OnTick;
        offline_mode = false;
        connected = false;
        onDisconnect?.Invoke();
    }

    private void OnClientConnect(ulong client_id)
    {
        if (IsServer && client_id != ServerID)
        {
            Debug.Log("Client Connected: " + client_id);
            onClientConnect?.Invoke(client_id);
        }

        if (!IsServer)
            AfterConnected(); //AfterConnected wasn't called yet for client
    }
   

    private void OnClientDisconnect(ulong client_id)
    {
        if (IsServer && client_id != ServerID)
        {
           // Debug.Log("Client Disconnected: " + client_id);
            onClientDisconnect?.Invoke(client_id);
        }

        if (ClientID == client_id || client_id == ServerID)
            AfterDisconnected();
    }

    private void ClientFailedConnect(ClientFailedConnectEvent FailedEvent)
    {
        onClientFailedConnect?.Invoke(FailedEvent);
    }
    //private void TransportEvent(NetworkEvent eventType, ulong clientId, ArraySegment<byte> payload, float receiveTime)
    //{
    //    Debug.LogError(eventType + clientId.ToString());
    //    switch (eventType)
    //    {
    //        case NetworkEvent.Data:
    //            break;
    //        case NetworkEvent.Connect:
    //            break;
    //        case NetworkEvent.Disconnect:
    //            break;
    //        case NetworkEvent.TransportFailure:
    //            break;
    //        case NetworkEvent.Nothing:
    //            break;

    //    }
    //}
    private void OnTick()
    {
        onTick?.Invoke();
    }
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest req, NetworkManager.ConnectionApprovalResponse res)
    {
        ConnectionData connect = NetworkTool.NetDeserialize<ConnectionData>(req.Payload);
        bool approved = ApproveClient(req.ClientNetworkId, connect);
        res.Approved = approved;
    }
    private bool ApproveClient(ulong client_id, ConnectionData connect)
    {
        if (client_id == ServerID)
            return true; //Server always approve itself

        if (offline_mode)
            return false;

        if (connect == null)
            return false; //Invalid data

        if (string.IsNullOrEmpty(connect.username) || string.IsNullOrEmpty(connect.user_id))
            return false; //Invalid username

        if (checkApproval != null && !checkApproval.Invoke(client_id, connect))
            return false; //Custom approval condition

        return true; //New Client approved
    }

    public IReadOnlyList<ulong> GetClientsIds() =>  NetManager.ConnectedClientsIds;
    public bool IsConnecting() => IsActive() && !IsConnected(); //Trying to connect but not yet
    public bool IsConnected() => offline_mode || NetManager.IsServer || NetManager.IsConnectedClient;
    public bool IsActive() => offline_mode || NetManager.IsServer || NetManager.IsClient;
    public string Address { get { return CoustmTransport.GetAddress(); } }
    public ushort Port{get { return CoustmTransport.GetPort(); }}

    public ulong ClientID { get { return offline_mode ? ServerID : NetManager.LocalClientId; } } //ID of this client (if host, will be same than ServerID), changes for every reconnection, assigned by Netcode
    public ulong ServerID { get { return NetworkManager.ServerClientId; } } //ID of the server
    public bool IsServer { get { return offline_mode || NetManager.IsServer; } }
    public bool IsClient { get { return offline_mode || NetManager.IsClient; } }
    public bool IsHost { get { return IsClient && IsServer; } } 
    public bool IsOnline { get { return !offline_mode && IsActive(); } }

    public NetworkTime LocalTime { get { return NetManager.LocalTime; } }
    public NetworkTime ServerTime { get { return NetManager.ServerTime; } }
    public float DeltaTick { get { return 1f / NetManager.NetworkTickSystem.TickRate; } }
    

}
[System.Serializable]
public class ConnectionData : INetworkSerializable
{
    public string user_id = "";
    public string username = "";

    public byte[] extra = new byte[0];

    //If you add extra data, make sure the total size of ConnectionData doesn't exceed Netcode max unfragmented msg (1400 bytes)
    //Fragmented msg are not possible for connection data, since connection is done in a single request

    public string GetExtraString()
    {
        return NetworkTool.DeserializeString(extra);
    }

    public T GetExtraData<T>() where T : INetworkSerializable, new()
    {
        return NetworkTool.NetDeserialize<T>(extra);
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref user_id);
        serializer.SerializeValue(ref username);
        serializer.SerializeValue(ref extra);
    }
}
public class NetSerializedData
{
    public FastBufferReader reader;
    public INetworkSerializable data;
    public byte[] bytes;

    public NetSerializedData(FastBufferReader r) { reader = r; data = null; }
    public NetSerializedData(INetworkSerializable d) { data = d; }

    public string GetString()
    {
        reader.ReadValueSafe(out string msg);
        return msg;
    }

    public T Get<T>() where T : INetworkSerializable, new()
    {
        if (data != null)
        {
            return (T)data;
        }
        else if (bytes != null)
        {
            data = NetworkTool.NetDeserialize<T>(bytes);
            return (T)data;
        }
        else
        {
            reader.ReadNetworkSerializable(out T val);
            data = val;
            return val;
        }
    }

    //PreRead in advance without knowing the object type, since FastBufferReader will get disposed by netcode
    public void PreRead()
    {
        int size = reader.Length - reader.Position;
        bytes = new byte[size];
        reader.ReadBytesSafe(ref bytes, size);
    }
}
public class ReceiveNetSerializedData
{
    public ulong Client_id;
    public NetSerializedData NData;

    public ReceiveNetSerializedData(ulong client_id, NetSerializedData data) 
    {
        Client_id = client_id;
        NData = data; 
    }
}
