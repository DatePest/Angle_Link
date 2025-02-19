using EventSystemTool;
using RngDropTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
[System.Serializable]
public class BattleData
{
    //[NonSerialized]public BattleLogManager battleLogs = new();
    public BattleSettings BattleSettings;
    public GameState gameState;
    public TrunState trunState;
    public string Uid;
    public BattlePlayer[] Player;
    public int Trun, TrunMax;
    public int CurrentWave, WaveMax;
    public static BattleData New(BattleSettings battleSettings, string uid)

    {
        var Bd = new BattleData();
        Bd.BattleSettings = battleSettings;
        Bd.Uid = uid;
        Bd.Player = new BattlePlayer[2];
        Bd.Player[0] = new BattlePlayer() { Id = 1 };
        Bd.Player[1] = new BattlePlayer() { Id = 2 };

        var tempdata =  battleSettings.LevelData.GetData();
        Bd.TrunMax = tempdata.TurnMax;
        Bd.WaveMax = tempdata.Waves.Length;
        return Bd;
    }

    public BattlePlayer GetPlayer() => Player[0];
    public BattlePlayer GetMonster() => Player[1];

    public List<Unit> GetAllUnit()
    {
        var t = new List<Unit>();
        foreach (var p in Player)
        {
            t.AddRange(p.Units);
        }
        return t;
    }
    public Unit GetUnit(string Uid)
    {
        foreach (var p in Player)
        {
            foreach (Unit v in p.Units)
            {
                if (v.Uid == Uid)
                    return v;
            }
        }
        return null;
    }

    public SettlementDatas_Net GetRandomDrop() => BattleSettings.LevelData.GetData().GetRngItem();
 
}
[System.Serializable]
public class BattlePlayer
{
    public byte Id;
    public List<Unit> Units = new();
}
[System.Serializable]
public enum TrunState
{
    Start, WaitSelect, Running, Clear, End
}
[System.Serializable]
public enum GameState
{
    inited,
    Ing,
    GameEnd,
    WaitNextWave

}
[System.Serializable]
public class BattleSettings
{
    public Team PlayerTeam;
    public NetworkFilteredData<GameLevelData> LevelData;
    public static BattleSettings New(Team team, GameLevelData gamedata, string PackageName = GameConstant.PackName_GameCore)
    {
        var Bs = new BattleSettings();
        Bs.PlayerTeam = team;
        Bs.SetGameLevelData(gamedata, PackageName);
        return Bs;
    }
    public  static async Task<BattleSettings> New_Async(Team team, GameLevelData gamedata, string PackageName = GameConstant.PackName_GameCore)
    {
        var Bs = new BattleSettings();
        Bs.PlayerTeam = team;
        await Bs.SetGameLevelData_Async(gamedata, PackageName);
        return Bs;
    }
    void SetGameLevelData(GameLevelData levelData, string levelData_InPackageName)
    {
        LevelData = new NetworkFilteredData<GameLevelData>(levelData, levelData_InPackageName);
    }
    async Task SetGameLevelData_Async(GameLevelData levelData, string levelData_InPackageName)
    {
        LevelData =  await NetworkFilteredData<GameLevelData>.New_Async(levelData, levelData_InPackageName); 
    }
}
public class MsgLogs : INetworkSerializable
{
    public string Json;
    static JsonSerializerSettings settings = new JsonSerializerSettings() { 

        TypeNameHandling = TypeNameHandling.Auto,
        //Formatting = Formatting.Indented,
       // SerializationBinder = new CustomBinder()
    };
    public IEnumerable<IGameBattleLog> GetData()
    {
        return JsonConvert.DeserializeObject<IEnumerable<IGameBattleLog>>(Json, settings);
    }
    public static MsgLogs New(IEnumerable<IGameBattleLog> logs)
    {
        var Ms = new MsgLogs();
        Ms.Json = JsonConvert.SerializeObject(logs, settings);
        return Ms;
    }
    public static MsgLogs New(params IGameBattleLog[] logs)
    {
        return New((IEnumerable<IGameBattleLog>)logs);
    }
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        NetworkTool.NetSerialize(serializer, ref Json);
    }

}

public class BattleMsgLogs : IEventTag
{
    public ushort EventId { get; set; } // no Use
    public List<IGameBattleLog> logs;
    public BattleMsgLogs(IEnumerable<IGameBattleLog> _logs)
    {
        logs = new();
       foreach (var a in _logs)
        {
            logs.Add(a);
        }
    }
    public BattleMsgLogs(IGameBattleLog log)
    {
        logs = new()
        {
            log
        };
    }
}
