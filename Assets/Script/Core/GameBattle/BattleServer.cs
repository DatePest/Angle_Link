using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

public class BattleServer
{
    public BattleLogic GameLogic { get; private set; }
    public Battle GamedData { get; private set; }
    public string GameUid => GamedData.Uid;
    ulong Client;
    public void Init(BattleSettings settings)
    {
        if (settings == null) throw new Exception("BattleSetting is null");
        GamedData = new(settings, OtherTool.GenerateStringID());
        GameLogic = new (GamedData);
    }
    public void Start()
    {
        GameLogic.StartGame();
    }
    public void ReceiveSelect(List<BattleSelectOrder> orders) => GameLogic.ReceiveSelect(orders);

    void TSendClient()
    {
        var mdata = new MsgLogs();
        mdata.GameLogs = GameLogic.GetCacheLogs;
        SendAction(NetworkMsg_HandlerName.Register, mdata, Client);
    }

    protected void SendAction<T>(ushort type, T data, ulong TargetID, NetworkDelivery delivery = NetworkDelivery.Reliable) where T : INetworkSerializable
    {
        FastBufferWriter writer = new FastBufferWriter(128, Unity.Collections.Allocator.Temp, Network.Msg_size);
        writer.WriteValueSafe(type);
        writer.WriteNetworkSerializable(data);
        //Network.Get().Messaging.Send(SendMsgTag, TargetID, writer, delivery);
        writer.Dispose();
    }
}
public class MsgLogs : INetworkSerializable
{
    public List<IGameBattleLog> GameLogs;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            int size = 0;
            serializer.SerializeValue(ref size);
            UnityEngine.Debug.Log($"IsReader Size = {size}");
            if (size > 0)
            {
                byte[] bytes = new byte[size];
                serializer.SerializeValue(ref bytes);
                GameLogs = NetworkTool.Deserialize<List<IGameBattleLog>>(bytes);
            }
        }

        if (serializer.IsWriter)
        {
            byte[] bytes = NetworkTool.Serialize(GameLogs);
            int size = bytes.Length;
            UnityEngine.Debug.Log($"IsWriter Size = {size}");
            serializer.SerializeValue(ref size);
            if (size > 0)
                serializer.SerializeValue(ref bytes);
        }
    }
}
public class MsgRefreshAll : INetworkSerializable
{
    public Battle game_data;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            int size = 0;
            serializer.SerializeValue(ref size);
            if (size > 0)
            {
                byte[] bytes = new byte[size];
                serializer.SerializeValue(ref bytes);
                game_data = NetworkTool.Deserialize<Battle>(bytes);
            }
        }

        if (serializer.IsWriter)
        {
            byte[] bytes = NetworkTool.Serialize(game_data);
            int size = bytes.Length;
            serializer.SerializeValue(ref size);
            if (size > 0)
                serializer.SerializeValue(ref bytes);
        }
    }
}
