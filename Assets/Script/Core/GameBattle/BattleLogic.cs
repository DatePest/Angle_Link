using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleLogic
{
    public BattleData Game;

    GameLevelData LevelData => Game.BattleSettings.LevelData.GetData();

    List<Unit> Player => Game.GetPlayer().Units;
    List<Unit> Monster => Game.GetMonster().Units;

    BattleLogManager logManager;

  
    public BattleLogic(BattleData battle)
    {
        this.Game = battle;
        logManager = new();
        InitGame();
    }
    void InitGame()
    {
        InitUnit(Game.GetPlayer().Id, Game.BattleSettings.PlayerTeam.Characters);
        Game.CurrentWave++;
        var e = GetGameLevelDataUnit(Game.CurrentWave);
        InitUnit(Game.GetMonster().Id, e);
        Game.gameState = GameState.inited;
    }
    //  Receive // game input
    #region
    public void StartGame()
    {
        ClearCacheLogs();
        Game.gameState = GameState.Ing;
        OnGameStart();
        Trun_Start();
    }
    public void ReceiveSelect(List<BattleSelectOrder> orders)
    {
        if (Game.trunState != TrunState.WaitSelect) return;
        ClearCacheLogs();
        Game.trunState = TrunState.Running;
        UpdataOrder_AddMonsterActionAndSortSpeed(orders);
        ExcuteOrder(orders);
        Trun_End();
    }
   
    public MsgLogs GetCurrentLogs()
    {
        var Logs = MsgLogs.New(GetCacheLogs);
        return Logs;
    }

    #endregion
    //----- Turn Phases ----------
    #region
    void Trun_Start()
    {
        CheckWinner();
        if (Game.gameState == GameState.GameEnd) return;
        Game.trunState = TrunState.Start;
        Game.Trun++;
        UpdataUnitStatus();
        OnStartTurn();
        OnUpdata();
        Trun_WaitSelect();
    }
    void Trun_WaitSelect()
    {
        CheckWinner();
        if (Game.gameState == GameState.GameEnd) return;
        Game.trunState = TrunState.WaitSelect;
        OnUserWaitSelect();
    }

    void Trun_End()
    {
        CheckWinner();
        if (Game.gameState == GameState.GameEnd) return;
        //OnUpdata();
        OnEndTurn();
        Trun_Start();
    }
    #endregion
    //----- ---------- ----------

    void CheckWinner()
    {
        if (Game.gameState == GameState.GameEnd) return;
        if (Player.Count <= 0)
        {
            OnGameEnd(false);
            return;
        }
        if (Monster.Count <= 0)
        {
            var e = GetGameLevelDataUnit(Game.CurrentWave);
            if (e == default)
            {
                OnGameEnd(true);
            }
            else
            {
                if (Game.trunState == TrunState.Running)
                {
                    Game.trunState = TrunState.Clear;
                    return;
                }
                Game.CurrentWave++;
                InitUnit(Game.GetMonster().Id, e);
                OnNextWave();
            }
        }

    }
    //----- ---------- ----------

    void ExcuteOrder(List<BattleSelectOrder> orders)
    {
        foreach (BattleSelectOrder order in orders)
        {
            if (Game.gameState == GameState.GameEnd) return;
            if (Game.trunState == TrunState.Clear) return;
            if (GetUnit(order.Caster) == null) continue;
            logManager.Add_OrderLog(order, out var battleLog);
            SetOrderTarget(order);
            order.Ability.GetData().Excute(this, battleLog);
        }
    }

    void SetOrderTarget(BattleSelectOrder order)
    {
        var C = GetUnit(order.Caster);
        var T = order.Ability.GetData().GetTarget(Game, C);
        order.SetTarget(T.ToArray());
    }

    void UpdataOrder_AddMonsterActionAndSortSpeed(List<BattleSelectOrder> orders)
    {
        var Mo = CalculateMonsterActions();
        if(Mo != null) orders.AddRange(Mo);
        orders.Sort((x, y) => y.Speed.CompareTo(x.Speed));
    }
    List<BattleSelectOrder> CalculateMonsterActions()
    {
        var MOrder = new List<BattleSelectOrder>();
        foreach (var u in Monster)
        {
            var Idata = u.UnitData.GetData();
            var M = Idata as Monster;
            var o = M.GetMonsterAction(this);
            if(o ==  null) continue;
            MOrder.Add(o);
        }
        return MOrder;
    }

    //----- Game Logic ----------
    public void Heal(Unit Caster, Unit Target, int value, string ExcuteId)
    {
        Target.UnitAttribute.HP += value;

        // ------ ExecutionResult ----
        var log = new ExecutionResult(ExcuteId, Caster.Uid, Target.Uid, BattleEventTag.OnHeal);
        log.Msg = value.ToString();
        var L = logManager.GetBattleOrderLog(ExcuteId);
        L?.Add_ExecutionResult(log);
        // ---------------
    }
    public void Damage(Unit Caster, Unit Target, int value, AtkType atkType, string ExcuteId)
    {
        Target.UnitAttribute.HP -= value;

        // ------ ExecutionResult ----
        var log = new ExecutionResult(ExcuteId, Caster.Uid, Target.Uid, BattleEventTag.OnDamage);
        log.Msg = value.ToString();
        var L = logManager.GetBattleOrderLog(ExcuteId);
        L?.Add_ExecutionResult(log);

        // ---------------
        if (Target.UnitAttribute.HP < 0)
            Kill(Caster, Target, ExcuteId, log);

    }
    public void Kill(Unit Caster, Unit Target, string ExcuteId , ExecutionResult result = null)
    {
        // ------ ExecutionResult ----
        if(result !=null)
        {
            var log = new ExecutionResult(ExcuteId, Caster.Uid, Target.Uid, BattleEventTag.OnKill);
            log.Msg =$"{Caster.Uid} Kill {Target}";
            result.After = log;
        }
        else
        {
            var log = new ExecutionResult(ExcuteId, Caster.Uid, Target.Uid, BattleEventTag.OnKill);
            log.Msg = $"{Caster.Uid} Kill {Target}";
            var L = logManager.GetBattleOrderLog(ExcuteId);
            L?.Add_ExecutionResult(log);
        }
        // ---------------


        RemoveUnit(Target);
        CheckWinner();
    }


    //----- Otehr ----------
    #region 
    public void RemoveUnit(Unit unit)
    {
        foreach (var p in Game.Player)
        {
            if (p.Units.Contains(unit))
            {
                p.Units.Remove(unit);
            }
        }
    }

    public Unit GetUnit(string Uid) => Game.GetUnit(Uid);
    public Unit CreateUnit(Character data, byte ownerID)
    {
        var id = OtherTool.GenerateStringID();
        return Unit.Create(id, data,ownerID);
    }
    public Unit CreateUnit(IUnitData data,byte ownerID)
    {
        var id = OtherTool.GenerateStringID();
        return Unit.Create(id, data, ownerID);
    }
    public void InitUnit(int id, IUnitData[] datas)
    {
        if (datas == null) return;
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] == null) continue;
            var u = CreateUnit(datas[i], (byte)id);
            u.SetSlot(i);

            var p = id ==  Game.GetPlayer().Id ? Player : Monster;
            p.Add(u);
            OnNewUnit(u);
        }
    }
    public void InitUnit(int id, Character[] datas)
    {
        if (datas == null) return;
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] == null) continue;
            var u = CreateUnit(datas[i], (byte)id);
            u.SetSlot(i);
            var p = id == Game.GetPlayer().Id ? Player : Monster;
            p.Add(u);
            OnNewUnit(u);
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
        foreach (var u in Game.Player)
        {
            foreach (Unit v in u.Units)
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
    void OnNewUnit(Unit u)
    {
        var e = new BattleEventLog();
        e.ID = BattleEventTag.OnCreateUnit;
        e.data = u;
        AddCacheLogs(e);
    }
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
        Game.gameState = GameState.GameEnd;
        var e = new BattleEventLog();
        e.data = PlayerIsWinner;
        e.ID = BattleEventTag.GameEnd;
        AddCacheLogs(e);
    }
    void OnNextWave()
    {
        var e = new BattleEventLog();
        e.data = Game;
        e.ID = BattleEventTag.GameNextWave;
        AddCacheLogs(e);
    }
    void OnUpdata()
    {
        var e = new BattleEventLog();
        e.data = Game;
        e.ID = BattleEventTag.Updata;
        AddCacheLogs(e);
    }

    public static BattleEventLog RequestData()
    {
        var data = new BattleEventLog();
        data.ID = BattleEventTag.SerRequestData;
        return data;
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
        battleLog.ID= BattleEventTag.BattleOrderLog;
        battleLog.SelectOrder = order;
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
[System.Serializable]
public class IGameBattleLog
{
    public int SortId { get; set; }
    public ushort ID { get; set; }
}
[System.Serializable]
public class BattleOrderLog : IGameBattleLog
{
    public BattleSelectOrder SelectOrder;
    public List<AbilityEffectsExcuteData> ExcuteData = new();
    public List<ExecutionResult> ExecutionResult = new();
    public void Add_ExcuteData(AbilityEffectsExcuteData data)
    {
        ExcuteData.Add(data);
    }
    public void Add_ExecutionResult(ExecutionResult result)
    {
        ExecutionResult.Add(result);
    }

    public  ExecutionResult GetExecutionResult(string ExcuteId , string Target)
    {
        foreach (var a in ExecutionResult)
        {
            if (a.ExcuteId == ExcuteId && a.Target == Target)
            {
                return a;
            }
        }
        return default;
    }



    public AbilityData GetAbilityData() => SelectOrder.Ability.GetData();
    public Unit GetCaster(BattleData b) => b.GetUnit(SelectOrder.Caster);
    public List<Unit> GetTargets(BattleData b)
    {
        List<Unit> targets = new List<Unit>();
        foreach (var a in SelectOrder.Targets)
        {
            var u =b.GetUnit(a);
            if (u!= null)
            {
                targets.Add(u);
            }
        }
        return targets;
    }
}
[System.Serializable]
public class BattleEventLog : IGameBattleLog
{
    public object data;
}
[System.Serializable]
public class ExecutionResult
{
    public ExecutionResult() { }
    public  ExecutionResult(string ExcuteId, string Caster, string Target, int eventID)
    {
        this.ExcuteId = ExcuteId;
        this.Caster = Caster;
        this.Target = Target;
        EventID = eventID;  
    }
    public ExecutionResult After;
    public string Caster;
    public string Target;
    public string ExcuteId;
    public int EventID;

    public string Msg;
}
[System.Serializable]
public class BattleSelectOrder
{
    public BattleSelectOrder() { }
    public BattleSelectOrder(Unit caster, UnitAbility ability) 
    {
        Init(caster, ability);
    }
    public BattleSelectOrder(Unit caster, UnitAbility ability, Unit[] targets)
    {
        Init(caster, ability);
        SetTarget(targets);
    }
    void Init(Unit caster, UnitAbility ability)
    {
        Caster = caster.Uid;
        Speed = caster.UnitAttribute.Speed;
        AbilityLv = ability.Lv;

        Ability = new NetworkFilteredData<AbilityData>(ability.abilityData.GetData(), GameConstant.PackName_GameCore);
    }
    public void SetTarget(Unit[] targets)
    {
        Targets = new string[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            Targets[i] = targets[i].Uid;
        }
    }
    //public string OrderUid;
    public string Caster;
    public int Speed;
    public int AbilityLv;
    public NetworkFilteredData<AbilityData> Ability;
    public string[] Targets;
}