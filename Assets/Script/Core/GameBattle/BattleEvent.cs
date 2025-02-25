using Client;
using Cysharp.Threading.Tasks;
using EventSystemTool;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;


public  class ClientEvent : IDisposable
{
    public Action<BattleData> OnGameUpdata { get; set; }
    public Action<BattleData> OnGameState { get; set; }
    public Action<BattleData> SelecetAceion { get; set; }
    public Action OnStartTurn { get; set; }
    public Action OnEndTurn { get; set; }
    public Action OnWaitSelect { get; set; }
    public Action RequestSettlementToServer { get; set; }
    public Action<SettlementDatas_Net> GetItem { get; set; }
    public Action<bool> OnGameEnd { get; set; }

    public Func<BattleData, UniTask> OnNextWave { get; set; }
    public Func<Unit,AbilityData,UniTask> Ability_StartCasting { get; set; }
    public Func<Client_BattleOrder, UniTask> OnBattleOrder { get; set; }

    public Action<ExecutionResult,Unit,Unit> OnExecutionResult { get; set; }
    public void Dispose()
    {
        OnGameUpdata = null;
        OnGameState = null;
        SelecetAceion = null;
        OnStartTurn = null;
        OnEndTurn = null;
        OnWaitSelect = null;
        RequestSettlementToServer = null;
        OnGameEnd = null;

        OnNextWave = null;
        Ability_StartCasting = null;
        OnExecutionResult = null;
        OnBattleOrder = null;
    }
}
public class BattleEvent_Client : IEventBase<EventTagAttribute>
{
    ClientEvent Event;
    BattleData Game;
    public BattleEvent_Client(ClientEvent @event)
    {
        Event = @event;
    }
    [EventTag(BattleEventTag.OnCreateUnit)]
    public void CreateUnit(BattleEventData data)
    {
        var d = data.Data as BattleEventLog;
        var U = (Unit)d.data;

        foreach(var a in Game.Player)
        {
            if(a.Id == U.OwnerID)
            {
                a.Units.Add(U);
                return;
            }
        }
    }
    [EventTag(BattleEventTag.SerRequestData)]
    public void SerRequestData(BattleEventData data)
    {
        EventSystemToolExpand.Publish(ClientEventTag.SendBattleData, NetworkMsg_HandlerTag.Battle, default, Game);
    }

    [EventTag(BattleEventTag.Updata)]
    public  void Updata(BattleEventData data)
    {
        var b = DTO_BattleData(data);
        Game = b;
        Event.OnGameUpdata?.Invoke(b);
    }
    [EventTag(BattleEventTag.StartTurn)]
    public  void StartTurn(BattleEventData data)
    {
        Event.OnStartTurn?.Invoke();
    }

    [EventTag(BattleEventTag.WaitSelect)]
    public  void WaitSelect(BattleEventData data)
    {
        Event.SelecetAceion?.Invoke(Game);
        Event.OnWaitSelect?.Invoke();
    }
    
    [EventTag(BattleEventTag.EndTurn)]
    public  void EndTurn(BattleEventData data)
    {
        Event.OnEndTurn?.Invoke();
    }
    [EventTag(BattleEventTag.GameNextWave)]
    public async UniTask NextWave(BattleEventData data)
    {
        var b = DTO_BattleData(data);
        Game = b;
        await UniTask.WhenAll(Event.OnNextWave(b));
        Event.OnGameState?.Invoke(b);
    }
    [EventTag(BattleEventTag.BattleOrderLog)]
    public async UniTask BattleOrderLog(BattleEventData data)
    {
        BattleOrderLog BOL= DTO_BattleOrderLog(data);
        var Cb = new Client_BattleOrder(BOL, Game, Event);
         await UniTask.WhenAll(Event.OnBattleOrder(Cb));
    }
    [EventTag(BattleEventTag.GameStart)]
    public  void GameStart(BattleEventData data)
    {
        Event.OnStartTurn?.Invoke();
    }

    [EventTag(BattleEventTag.GameEnd)]
    public  void GameEnd(BattleEventData data)
    {
        var d = data.Data as BattleEventLog;
        if (d == null) { throw new Exception("Data is Not BattleEventLog"); }
        Event.OnGameEnd?.Invoke((bool)d.data);
    }
    [EventTag(BattleEventTag.RequestSend_SelectOrder)]
    public void SelectOrder(BattleEventData data)
    {
       // var d = data.Data as List<BattleSelectOrder>; ;
       // EventSystem.Publish(new BattleEventData(BattleEventTag.Receive_SelectOrder, GameUid, d));
    }
    [EventTag(BattleEventTag.ToSettlement)]
    public void GameDrop(BattleEventData data)
    {
        var d = data.Data as BattleEventLog;
        if (d == null) { throw new Exception("Data is Not BattleEventLog"); }
        Event.GetItem?.Invoke((SettlementDatas_Net)d.data);
    }
    BattleData DTO_BattleData(BattleEventData data)
    {
        var d = data.Data as BattleEventLog;
        if (d == null) { throw new Exception("Data is Not BattleEventLog"); }
        //var game = ApiTool.JsonToObject<BattleData>(d.data.ToString());
        var game = (BattleData)d.data;
        if (game == null) { throw new Exception("Data.Battle is null"); }
        return game;
    }
    BattleOrderLog DTO_BattleOrderLog(BattleEventData data)
    {
        var d = data.Data as BattleOrderLog;
        if (d == null) { throw new Exception("Data is Not BattleEventLog"); }
        return d;
    }
}

public class Client_BattleOrder : IDisposable
{
    public BattleOrderLog OrderLog;
    public BattleData Game;
    public ClientEvent ClientEvent;

    public AbilityData Adata;
    public Unit Caster;
    public List<Unit> Targets;
    public Vector3 StartPosition;

    public float Speed = 1f, MoveTime = 0.5f;  
    bool AbilityUsed = false;
    Dictionary<string,bool> WaitUse = new Dictionary<string,bool>();
    public Client_BattleOrder(BattleOrderLog orderLog, BattleData game, ClientEvent clientEvent)
    {
        OrderLog = orderLog;
        Game = game;
        ClientEvent = clientEvent;

        Adata = OrderLog.GetAbilityData();
        Caster = OrderLog.GetCaster(game);
        Targets = OrderLog.GetTargets(game);
        StartPosition = Caster.Solt.transform.position;
    }
    public void SetSpeed(float Sp) => Speed = Sp;
    public void AddWaitUse(string Name)
    {
        WaitUse.TryAdd(Name, false);
    }

    public void SetWaitUse(string Name,bool b)
    {
        if (WaitUse.ContainsKey(Name))
        { 
            WaitUse[Name] = b;
            Check();
        }

    }
    void Check()
    {
        foreach (var a in WaitUse)
        {
            if (a.Value == false)
                return;
        }
        Dispose();
    }
    public void Dispose()
    {
        OrderLog = null;
        Game = null;
        ClientEvent = null;

        Adata = null;
        Caster = null;
        Targets = null;
    }
}
public class BattleEvent_Server : IEventBase<EventTagAttribute>
{
    BattleLogic GameLogic;
    public BattleEvent_Server(BattleLogic logic)
    {
        GameLogic = logic;
    }

    [EventTag(BattleEventTag.SerRequestData)]
    public void SendSelectOrder(BattleEventData data)
    {
        var o = data.Data as List<BattleSelectOrder>;
        GameLogic.ReceiveSelect(o);
    }
}
public class BattleEventTag
{
    public const ushort Test = 0000;

    /// <summary>
    /// Client
    /// </summary
    public const ushort Updata = 1000;
    public const ushort StartTurn = 1010;
    public const ushort WaitSelect = 1014;
    public const ushort EndTurn = 1020;
    public const ushort BattleOrderLog = 1050;

    public const ushort GameNextWave = 1080;
    public const ushort GameStart = 1090;
    public const ushort GameEnd = 1099;
    //

    public const ushort OnHeal = 1500;
    public const ushort OnDamage = 1504;
    public const ushort OnKill = 1508;

    public const ushort OnCreateUnit = 1400;

    public const ushort RequestSend_SelectOrder = 2000;
    public const ushort ToSettlement = 2010;

    public const ushort SerRequestData = 2100;

    /// <summary>
    /// Server
    /// </summary>
    public const ushort Receive_SelectOrder = 3001;
}
public class BattleEventData : IEventTag
{
    public ushort EventId { get; set; } // SerEventTag
    public string BattleUid;
    public object Data;
    public BattleEventData(ushort tagId, object data)
    {
        EventId = tagId;
        Data = data;
    }
    public BattleEventData(IGameBattleLog log)
    {
        EventId = log.ID;
        Data = log;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is BattleEventData))
            return false;

        BattleEventData other = (BattleEventData)obj;

        return EventId == other.EventId &&
               Equals(Data, other.Data);
    }
    public override int GetHashCode()
    {
        int hash = 27;
        hash = hash * 21 + EventId.GetHashCode();
        hash = hash * 21 + (Data != null ? Data.GetHashCode() : 0);
        return hash;
    }
}
