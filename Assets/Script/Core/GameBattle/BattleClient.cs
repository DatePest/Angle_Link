using GameApi;
using Assets.Script.Core.Server;
using Client;
using Cysharp.Threading.Tasks;
using EventSystemTool;
using System;
using System.Collections.Generic;
using Tools;

public class BattleClient :IDisposable
{
    public BattleData Game { get; private set; }
    public ClientEvent GameEvent { get; private set; }
    Listener<BattleMsgLogs> listener;
    LogQueue logQueue;
    BattleEvent_Client battleLogEvent;
  
    public BattleClient()
    {
        listener = new((e)=>GameReceiveGameLogs((BattleMsgLogs)e));
         GameEvent = new();
        battleLogEvent = new(GameEvent);
        logQueue = new(Event_Client);
        GameEvent.OnGameUpdata += UpdataGame;
        GameEvent.RequestSettlementToServer += RequestSettlementToServer;
    }
    public void Init(BattleData battle)
    {
        GameEvent.OnGameUpdata?.Invoke(battle);
    }
    async UniTask Event_Client(IEventTag sendData)
    {
        var data = sendData as BattleEventData;
        await battleLogEvent.FindRunAsync_UniTask(data.EventId,data);
    }
    public void GameReceiveGameLogs(BattleMsgLogs battleLogs)
    {
        logQueue.TaskAdd(battleLogs.logs);
    }
    void UpdataGame(BattleData data) => Game = data;
    public void Dispose()
    {
        listener.Stop();
        listener = null;
        GameEvent.Dispose();
        logQueue.Dispose();
    }
   
    public void OnGameStartToServer()
    {
        var msg = new BattleRequest();
        msg.accesLogin_token = ClientRoot.GetToken();
        msg.GameUid = Game.Uid;
        EventSystemToolExpand.Publish(ClientEventTag.SendOnGameStartToServerrRequest, NetworkMsg_HandlerTag.Battle, default, msg);
    }
    public void RequestSettlementToServer()
    {
        var msg = new BattleRequest();
        msg.accesLogin_token = ClientRoot.GetToken();
        msg.GameUid = Game.Uid;
        EventSystemToolExpand.Publish(ClientEventTag.SendBattleEndSettlementRequest, NetworkMsg_HandlerTag.Battle, default, msg);
    }
    public void SelectToServer(List<BattleSelectOrder> orders)
    {
        UnityEngine.Debug.Log($" Req.orders : {orders.Count}");
        var msg = new BattlePlayerSelectRequest();
        msg.accesLogin_token = ClientRoot.GetToken();
        msg.orders_Json = WebTool.ToJson(orders);
        msg.GameUid = Game.Uid;
        EventSystemToolExpand.Publish(ClientEventTag.SendSelectToServerRequest, NetworkMsg_HandlerTag.Battle, default, msg);
    }
   
    public class LogQueue : IDisposable
    {
        Queue<IGameBattleLog> Logs { get; set; } = new();
        Func<IEventTag, UniTask> Event;
        bool _Running = false;
        public LogQueue(Func<IEventTag, UniTask> @event)
        {
            Event = @event;
        }
        public void TaskAdd(List<IGameBattleLog> log)
        {
            foreach (var a in log) Logs.Enqueue(a);
            RunQueue();
        }
        async UniTask ExcuteAction(IGameBattleLog log)
        {
            var data = new BattleEventData(log);
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"Battle Client Log {data.EventId}");
#endif
            await Event(data);
        
        }

        async void RunQueue()
        {
            if (_Running) return;
            _Running = true;
            while (Logs.Count != 0)
            {
                var a = Logs.Dequeue();
                await UniTask.Yield();
                await UniTask.SwitchToMainThread();
                await ExcuteAction(a);
                await UniTask.Delay(200);
            }

            _Running = false;
        }

        public void Dispose()
        {
            Logs.Clear();
            Logs= null;
            Event = null;   
        }
    }
}