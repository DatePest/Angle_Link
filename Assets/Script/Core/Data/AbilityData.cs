using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Assets.Script.Core.Fx;
using Assets.Script.Core.Data;
[CreateAssetMenu(fileName = "Ability", menuName = "AL/AbilityData", order = 5)]
public class AbilityData : iSobj_Name
{
    [Header("Text")]
    public string title;
    [TextArea(5, 7)]
    public string desc;
    [Header("Trigger")]
    public AbilityTrigger trigger;
    [Header("Target")]
    public AbilityTarget target;               //WHO is targeted?
    public ConditionData[] conditions_target;  //Condition checked on the target to know if its a valid taget
    public FilterData[] filters_target;  //Condition checked on the target to know if its a valid taget
    [Header("FX")]
    public AudioClip cast_audio;
    public AudioClip target_audio;
    public FXObj FXData;
    public Sprite Icon;

    [Header("Effect")]
    public int CoolDown;
    [Header("Please refer to the target script, otherwise an error will occur")]
    public Effect[] effects;
    [HideInInspector]
    [Header("AddStatuOnly")]
    public string StatuDurationFormula;

    public void Excute(BattleLogic logic , BattleOrderLog order )
    {
        if (order.SelectOrder.AbilityLv < 1) return ;
        int statuDuration= GetStatuDurationFormula(order.SelectOrder.AbilityLv);
        foreach (var effect in effects)
        {
            var data = new AbilityEffectsExcuteData() { ExcuteUid = OtherTool.GenerateStringID() };
            order.Add_ExcuteData(data);
            data.AbilityLv = order.SelectOrder.AbilityLv;
            data.Caster = order.SelectOrder.Caster;
            data.StatuDuration = statuDuration;
            data.Targets = order.SelectOrder.Targets;
            effect.SetParameter(data);
            effect.Excute(logic, data);
        }
    }
    public void TTT(BattleLogic logic, AbilityEffectsExcuteData excuteData)
    {
        foreach (var effect in effects)
        {
            effect.Excute(logic, excuteData);
        }
    }

    public int GetStatuDurationFormula(int Lv)
    {
        if (StatuDurationFormula == string.Empty) return 0;
        string description = StatuDurationFormula.Replace("{Lv}", Lv.ToString());
        return  (int)OtherTool.StringCalculateFormulaCompute(description);

    }
    public List<Unit> GetTarget(BattleData Game, Unit Caster)
    {
        List<Unit> units = new List<Unit>();
        var All = Game.GetAllUnit();
        if (target == AbilityTarget.Self)
        {
            foreach(var a in All)
            {
                if(a == Caster)
                { units.Add(a); break; }
            }
        }
        if (target == AbilityTarget.OwnTeam)
        {
            foreach (var a in All)
            {
                if (a.OwnerID == Caster.OwnerID)
                { units.Add(a);}
            }
        }
        if (target == AbilityTarget.OpponentTeam)
        {
            foreach (var a in All)
            {
                if (a.OwnerID != Caster.OwnerID)
                { units.Add(a); }
            }
        }
        if (target == AbilityTarget.BothAll)
        {
            units = All;
        }
        if (target == AbilityTarget.NotSelfAll)
        {
            units = All;
            units.Remove(Caster);
        }
        if (target == AbilityTarget.SelectTarget)
        {
           throw new Exception("AbilityTarget.SelectTarget Is Not ");
        }


        //Conditions
        if (conditions_target != null && units.Count > 0)
        {
            var Temp = new List<Unit>();
            foreach (var a in units)
            {
                if (AreTargetConditionsMet(Caster, a))
                {
                    Temp.Add(a);
                }
            }

            units = Temp;
        }
        //Filter targets
        if (filters_target != null && units.Count > 0)
        {
            foreach (FilterData filter in filters_target)
            {
                if (filter != null)
                    units = filter.FilterTargets(this, Caster, units);
            }
        }
        return units;
    }

    bool AreTargetConditionsMet(Unit caster,Unit Target)
    {
        foreach (ConditionData cond in conditions_target)
        {
            if (cond != null && !cond.IsTargetConditionMet(this, caster, Target))
                return false;
        }
        return true;
    }


}
//Suppose there are multiple targets and each needs to be executed separately
[System.Serializable]
public class AbilityEffectsExcuteData
{
    public int AbilityLv;
    public int StatuDuration;
    public string Caster;
    public float AbilityMainParameter;
    public string[] Targets;
    public string[] Parameters;
    public string ExcuteUid;

}

public enum AbilityTrigger
{
    None = 0,

    Ongoing = 2,  //Always active (does not work with all effects)
    Active = 5, //Action


    StartOfTurn = 20, //Every turn
    EndOfTurn = 22, //Every turn

    OnBeforeAttack = 30, //When attacking, before damage
    OnAfterAttack = 31, //When attacking, after damage if still alive
    OnBeforeDefend = 32, //When being attacked, before damage
    OnAfterDefend = 33, //When being attacked, after damage if still alive
    OnKill = 35,        //When killing another  during an attack

    OnDeath = 40, //When dying
    OnDeathOther = 42, //When another dying
}
public enum AbilityTarget  
{
    //Its function is to obtain a starting target range, and the rest is determined by conditions.
    None = 0,
    Self = 1,

    OwnTeam = 3,
    OpponentTeam = 4,

    BothAll = 7,
    NotSelfAll = 8,

    /// Waiting for player selection
    /// Call the corresponding module , valid targets will be screened first and then the user will be provided with options.

    SelectTarget = 10
    
}

