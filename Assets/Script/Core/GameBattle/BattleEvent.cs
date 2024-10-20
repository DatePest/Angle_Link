using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IBattleEvent_Client
{
    public Action<Battle> OnGameUpdata { get; set; }
    public Action OnStartTurn { get; set; }
    public Action OnEndTurn { get; set; }
    public Action OnWaitSelect { get; set; }
    public Action<bool> OnGameEnd { get; set; }


    public Func<Task> Ability_StartCasting { get; set; }
    public Func<Task> Ability_CastingFX { get; set; }
    public Func<Task> Ability_MakeEffect { get; set; }

}
public class BattleEvent_Client : IEventBase<EventTagAttribute>
{
    IBattleEvent_Client Event;
    string GameUid;
    public BattleEvent_Client(IBattleEvent_Client @event)
    {
        Event = @event;
    }
    public void SetGameUid(string Uid) => GameUid = Uid;
     [EventTag(BattleEventTag.Updata)]
    public void Updata(BattleEventData data)
    {
        var d = data.Data as Battle;
        Event.OnGameUpdata?.Invoke(d); ;
    }
    [EventTag(BattleEventTag.StartTurn)]
    public void StartTurn(BattleEventData data)
    {
        Event.OnStartTurn?.Invoke(); ;
    }

    [EventTag(BattleEventTag.WaitSelect)]
    public void WaitSelect(BattleEventData data)
    {
        Event.OnWaitSelect?.Invoke(); ;
    }
    
    [EventTag(BattleEventTag.EndTurn)]
    public void EndTurn(BattleEventData data)
    {
        Event.OnEndTurn?.Invoke(); ;
    }
    [EventTag(BattleEventTag.GameEnd)]
    public void GameEnd(BattleEventData data)
    {
        Event.OnGameEnd?.Invoke((bool)data.Data); ;
    }
    [EventTag(BattleEventTag.RequestSend_SelectOrder)]
    public void SelectOrder(BattleEventData data)
    {
        var d = data.Data as List<BattleSelectOrder>; ;
        EventSystem.Publish(new BattleEventData(BattleEventTag.Receive_SelectOrder, GameUid, d));
    }

}
public class BattleEvent_Server : IEventBase<EventTagAttribute>
{
    BattleLogic GameLogic;
    public BattleEvent_Server(BattleLogic logic)
    {
        GameLogic = logic;
    }

    [EventTag(2001)]
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
    public const ushort GameStart = 1090;
    public const ushort GameEnd = 1099;

    public const ushort OnHeal = 1500;
    public const ushort OnDamage = 1504;
    public const ushort OnKill = 1508;

    public const ushort RequestSend_SelectOrder = 2000;

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
    public BattleEventData(ushort tagId, string battleUid)
    {
        EventId = tagId;
        BattleUid = battleUid;
    }
    public BattleEventData(ushort tagId, string battleUid , object data)
    {
        EventId = tagId;
        BattleUid = battleUid;
        Data = data;
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