using EventSystemTool;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.Experimental.GraphView;
using static UnityEngine.GraphicsBuffer;

public class BattleLogic
{
    Battle Game;

    GameLevelData LevelData => Game.BattleSettings.LevelData.GetData();

    List<Unit> Player => Game.GetPlayer();
    List<Unit> Monster => Game.GetMonster();

    BattleLogManager logManager;
    public BattleLogic(Battle battle)
    {
        this.Game = battle;
        logManager = new();
        InitGame();
    }
    void InitGame()
    {
        InitUnit(0, Game.BattleSettings.PlayerTeam.Characters);
        var e = GetGameLevelDataUnit(Game.CurrentWave);
        InitUnit(1, e);
        Game.gameState = GameState.inited;
    }
    //  Receive // game input
    #region
    public void StartGame()
    {
        Game.gameState = GameState.Ing;
        OnUpdata();
        OnGameStart();
        Trun_Start();

        // ToDo send to client 
    }
    public void ReceiveSelect(List<BattleSelectOrder> orders)
    {
        if (Game.trunState != TrunState.WaitSelect) return;
        ClearCacheLogs();
        UpdataOrder_AddMonsterActionAndSortSpeed(orders);
        ExcuteOrder(orders);
        OnUpdata();

        //TODO send logs to client
    }
    #endregion
    //----- Turn Phases ----------
    #region
    void Trun_Start()
    {
        if (Game.gameState == GameState.GameEnd) return;
        Game.trunState = TrunState.Start;
        Game.Trun++;
        UpdataUnitStatus();
        OnStartTurn();
        Trun_WaitSelect();
    }
    void Trun_WaitSelect()
    {
        if (Game.gameState == GameState.GameEnd) return;
        Game.trunState = TrunState.WaitSelect;
        OnUserWaitSelect();
    }

    void Trun_End()
    {
        if (Game.gameState == GameState.GameEnd) return;
        OnEndTurn();
        Trun_Start();
    }
    #endregion
    //----- ---------- ----------

    void CheckWinner()
    {
        if (Player.Count <= 0)
        {
            OnGameEnd(false);

            Game.gameState = GameState.GameEnd;
        }
        if (Monster.Count <= 0)
        {

            Game.CurrentWave++;
            var e = GetGameLevelDataUnit(Game.CurrentWave);
            if (e == default)
            {
                OnGameEnd(true);
                Game.gameState = GameState.GameEnd;
            }
            else
            {
                InitUnit(1, e);
            }
        }

    }
    //----- ---------- ----------

    void ExcuteOrder(List<BattleSelectOrder> orders)
    {
        foreach (BattleSelectOrder order in orders)
        {
            if (GetUnit(order.Caster) == null) continue;
            logManager.Add_OrderLog(order, out var battleLog);
            AddCacheLogs(battleLog);
            // TODO add target
            var data = order.Ability.GetData().Excute(this, order);
            foreach (var item in data)
            {
                battleLog.Add_ExcuteData(item);
            }

        }
    }
    void UpdataOrder_AddMonsterActionAndSortSpeed(List<BattleSelectOrder> orders)
    {
        var Mo = CalculateMonsterActions();
        orders.AddRange(Mo);
        orders.Sort((x, y) => x.Speed);
    }
    List<BattleSelectOrder> CalculateMonsterActions()
    {
        var MOrder = new List<BattleSelectOrder>();
        foreach (var u in Monster)
        {
            var Idata = u.UnitData.GetData();
            var M = Idata as Monster;
            var o = M.GetMonsterAction(this);
            MOrder.Add(o);
        }
        return MOrder;
    }

    //----- Game Logic ----------
    public void Heal(Unit Target, int value, string ExcuteId)
    {
        Target.UnitAttribute.HP += value;
    }
    public void Damage(Unit Caster, Unit Target, int value, string ExcuteId)
    {
        Target.UnitAttribute.HP -= value;

        // ------ ExecutionResult ----
        {
            var log = new ExecutionResult();
            log.Caster = Caster.Uid;
            log.Target = Target.Uid;
            log.EventID = BattleEventTag.OnDamage;
            log.Msg = value.ToString();
            var L = logManager.GetBattleOrderLog(ExcuteId);
            L?.Add_ExecutionResult(log);
        }

        // ---------------
        if (Target.UnitAttribute.HP < 0)
            Kill(Target, ExcuteId);

    }
    public void Kill(Unit Target, string ExcuteId)
    {
        RemoveUnit(Target);
        CheckWinner();
    }


    //----- Otehr ----------
    #region 
    public void RemoveUnit(Unit unit)
    {
        foreach (List<Unit> u in Game.Player)
        {
            if (u.Contains(unit))
            {
                u.Remove(unit);
            }
        }
    }

    public Unit GetUnit(string Uid) => Game.GetUnit(Uid);
    public Unit CreateUnit(Character data)
    {
        var id = OtherTool.GenerateStringID();
        return Unit.Create(id, data);
    }
    public Unit CreateUnit(IUnitData data)
    {
        var id = OtherTool.GenerateStringID();
        return Unit.Create(id, data);
    }
    public void InitUnit(int id, IUnitData[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] == null) continue;
            var u = CreateUnit(datas[i]);
            u.SetSlot(i);

            var p = id == 0 ? Player : Monster;
            p.Add(u);
        }
    }
    public void InitUnit(int id, Character[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] == null) continue;
            var u = CreateUnit(datas[i]);
            u.SetSlot(i);
            var p = id == 0 ? Player : Monster;
            p.Add(u);
        }
    }
    public IUnitData[] GetGameLevelDataUnit(int i)
    {
        if (i >= LevelData.Waves.Length) return default;
        return LevelData.Waves[i].Enemys;
    }
    public void ClearCacheLogs()
    {
        logManager.ClearCacheLogs();
    }
    public List<IGameBattleLog> GetCacheLogs => logManager.GetLogs();
    void AddCacheLogs(IGameBattleLog log)
    {
        logManager.Add_Log(log);
    }
    void UpdataUnitStatus()
    {
        foreach (List<Unit> u in Game.Player)
        {
            foreach (Unit v in u)
            {
                foreach (var s in v.Status)
                {
                    s.UpdataTimeDuration(-1);
                }
            }
        }
    }
    #endregion
    //----- Event  ----------
    #region
    void OnEndTurn()
    {
        var e = new BattleEventLog();
        e.ID = BattleEventTag.EndTurn;
        AddCacheLogs(e);
    }
    void OnUserWaitSelect()
    {
        var e = new BattleEventLog();
        e.ID = BattleEventTag.WaitSelect;
        AddCacheLogs(e);
    }
    void OnStartTurn()
    {
        OnUpdata();
        var e = new BattleEventLog();
        e.ID = BattleEventTag.StartTurn;
        AddCacheLogs(e);
    }
    void OnGameStart()
    {
        var e = new BattleEventLog();
        e.ID = BattleEventTag.GameStart;
        AddCacheLogs(e);
    }
    void OnGameEnd(bool PlayerIsWinner)
    {
        var e = new BattleEventLog();
        e.data = PlayerIsWinner;
        e.ID = BattleEventTag.GameEnd;
        AddCacheLogs(e);
    }
    void OnUpdata()
    {
        var e = new BattleEventLog();
        e.data = Game;
        e.ID = BattleEventTag.Updata;
        AddCacheLogs(e);
    }
    #endregion
}
public class BattleLogManager
{
    List<IGameBattleLog> Logs = new();
    int Logid;
    public int Add_OrderLog(BattleSelectOrder order, out BattleOrderLog battleLog)
    {
        battleLog = new BattleOrderLog();
        battleLog.Order = order;
        Add_Log(battleLog);
        return Logid;
    }
    public int Add_Log(IGameBattleLog order)
    {
        Logid++;
        this.Logs.Add(order);
        order.SortId = Logid;
        return Logid;
    }
    public T GetLogForSortId<T>(int id) where T : IGameBattleLog
    {
        foreach (var a in Logs)
        {
            if (a.SortId == id)
            {
                if (a is T)
                    return (T)a;
                else
                    throw new Exception($"Target is {typeof(T)} Type ");
            }
        }

        return default;
    }
    public BattleOrderLog GetBattleOrderLog(string ExcuteId)
    {
        for (int i = Logs.Count - 1; i >= 0; i--)
        {
            var a = Logs[i] as BattleOrderLog;
            if (a == null) continue;
            foreach (var item in a.ExcuteData)
            {
                if (item.ExcuteUid == ExcuteId)
                {
                    return a;
                }
            }
        }
        return default;
    }
    public void ClearCacheLogs() => Logs.Clear();
    public List<IGameBattleLog> GetLogs() => Logs;

}
public interface IGameBattleLog
{
    public int SortId { get; set; }
    public int ID { get; set; }
}
[System.Serializable]
public class BattleOrderLog : IGameBattleLog
{
    public int SortId { get; set; }

    public int ID { get; set; }
    public BattleSelectOrder Order;
    public List<AbilityExcuteData> ExcuteData = new();
    public List<ExecutionResult> ExecutionResult = new();
    public void Add_ExcuteData(AbilityExcuteData data)
    {
        ExcuteData.Add(data);
    }
    public void Add_ExecutionResult(ExecutionResult result)
    {
        ExecutionResult.Add(result);
    }
}
[System.Serializable]
public class BattleEventLog : IGameBattleLog
{
    public int SortId { get; set; }
    public int ID { get; set; }
    public object data;
}
[System.Serializable]
public class ExecutionResult
{
    public string Caster;
    public string Target;
    public int EventID;

    public string Msg;
}
[System.Serializable]
public class BattleSelectOrder
{
    public BattleSelectOrder() { }
    public BattleSelectOrder(Unit caster, AbilityData ability, Unit[] targets)
    {
        Caster = caster.Uid;
        Speed = caster.UnitAttribute.Speed;
        AbilityLv = caster.GetUnitAbilityLV(ability.name);

        Ability = new NetworkFilteredData<AbilityData>(ability, GameConstant.PackName_GameCore);
        Targets = new string[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            Targets[i] = targets[i].Uid;
        }
    }
    public string OrderUid;
    public string Caster;
    public int Speed;
    public int AbilityLv;
    public NetworkFilteredData<AbilityData> Ability;
    public string[] Targets;
}