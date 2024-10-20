using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleClient 
{
    EventSystemTool.Listener<BattleEventData> listener;
    Battle Game;
    BattleEvent_Client battleEvent;
    public ClientEvent GameEvent { get; private set; }
    public BattleClient()
    {
        listener = new(Event);
        GameEvent = new();
        battleEvent = new(GameEvent);
    }
    public void Init(Battle battle)
    {
        Game = battle;
        GameEvent.OnGameUpdata?.Invoke(Game);
        battleEvent.SetGameUid(Game.Uid);
    }
    void Event(IEventTag sendData)
    {
        var data = sendData as BattleEventData;
        battleEvent.FindRun(data);
    }
    public void OnDestroy()
    {
        listener.Stop();
    }

}
public class ClientEvent : IBattleEvent_Client
{
    public Action<Battle> OnGameUpdata { get; set; }
    public Action OnStartTurn { get; set; }
    public Action OnEndTurn { get; set; }
    public Action OnWaitSelect { get; set; }
    public Action<bool> OnGameEnd { get; set; }
    public Func<Task> Ability_StartCasting { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Func<Task> Ability_CastingFX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Func<Task> Ability_MakeEffect { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}