using Cysharp.Threading.Tasks;
using RngDropTool;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;

public class BattleServer : IDisposable
{
    public BattleLogic GameLogic { get; private set; }
    public BattleData GamedData { get; private set; }
    UsedTime usedTime = new();
    public string GameUid => GamedData.Uid;
    //public bool IsEnd;
    //public Action<FastBufferWriter> OnSend; //Local Test Use
    public void Init(BattleSettings settings)
    {
        if (settings == null) throw new Exception("BattleSetting is null");
    
        GamedData = BattleData.New(settings, OtherTool.GenerateStringID());
        GameLogic = new (GamedData);
    }
    public void Init(BattleData data)
    {
        if (data == null) throw new Exception("BattleData is null");
        GamedData = data;
        GameLogic = new(GamedData);
    }
    public string GetGameDataToJosn() => GameApi.ApiTool.ToJson(GamedData);
    public void Start()
    {
        UpdataHeartbeat();
        GameLogic.StartGame();
    }
    public void ReceiveSelect(List<BattleSelectOrder> orders) 
    {
        UpdataHeartbeat();
        GameLogic.ReceiveSelect(orders);
    } 
    public MsgLogs GetCurrentLogs()
    {
        return GameLogic.GetCurrentLogs();
    }
    public SettlementDatas_Net GetDrop()
    {
       // IsEnd = true;
        return GamedData.GetRandomDrop();
    }
    public bool HeartbeatCheck(int Time) => usedTime.IsExpired(Time);
    public void UpdataHeartbeat() => usedTime.UpdateLastUsedTime();
    //private void SerSend(MsgLogs logs)
    //{
    //    #region Local Test
    //    //var writer = new FastBufferWriter(2048, Unity.Collections.Allocator.Temp,Network.Msg_size);
    //    //writer.WriteNetworkSerializable(logs);
    //    //OnSend?.Invoke(writer);
    //    //writer.Dispose();
    //    #endregion
    //}

    public void Dispose()
    {
        GameLogic = null;
        GamedData = null;
    }

    public class UsedTime
    {
        public DateTime LastUsedTime { get; private set; }
        public UsedTime()
        {
            UpdateLastUsedTime();
        }

        public void UpdateLastUsedTime()
        {
            LastUsedTime = DateTime.UtcNow;
        }
        public bool IsExpired(int minutes)
        {
            return (DateTime.UtcNow - LastUsedTime).TotalMinutes > minutes;
        }
    }



    #region Action Tag

    public static Action<BattleServer> a_Start = (x) => x.Start();
    public static Action<BattleServer> GetAction_ReceiveSelect(List<BattleSelectOrder> orders)
    {
        return (x) => x.ReceiveSelect(orders);
    }
    public static Func<BattleServer, object> GetLevelDrop = (x) => x.GetDrop();
    #endregion
}
