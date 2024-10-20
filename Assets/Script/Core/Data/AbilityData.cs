using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using static EffectData;
using static Codice.CM.Common.Serialization.PacketFileReader;
using PlasticGui.Configuration.CloudEdition.Welcome;
[CreateAssetMenu(fileName = "Ability", menuName = "AL/AbilityData", order = 5)]
public class AbilityData : ScriptableObject
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
    public GameObject CastFX;

    [Header("Effect")]
    [Header("Please refer to the target script, otherwise an error will occur")]
    public Effect[] effects;
    [HideInInspector]
    [Header("AddStatuOnly")]
    public string StatuDurationFormula;

    public List<AbilityExcuteData> Excute(BattleLogic logic , BattleSelectOrder order )
    {
        if (order.AbilityLv < 1) return null;
        int statuDuration= GetStatuDurationFormula(order.AbilityLv);
        List<AbilityExcuteData> datas = new List<AbilityExcuteData>();
        foreach (var effect in effects)
        {
            var data = new AbilityExcuteData(OtherTool.GenerateStringID()); 
            data.AbilityLv = order.AbilityLv;
            data.Caster = order.OrderUid;
            data.StatuDuration = statuDuration;
            data.Targets = order.Targets;
            effect.SetParameter(data);
            effect.Excute(logic, data);
            datas.Add(data);
        }
        return datas;
    }
    public void TTT(BattleLogic logic, AbilityExcuteData excuteData)
    {
        foreach (var effect in effects)
        {
            effect.Excute(logic, excuteData);
        }
    }

    public int GetStatuDurationFormula(int Lv)
    {
        if (StatuDurationFormula == string.Empty) throw new Exception("GetStatuDurationFormula == 0");
        string description = StatuDurationFormula.Replace("{Lv}", Lv.ToString());
        return  (int)OtherTool.StringCalculateFormulaCompute(description);

    }

}
//Suppose there are multiple targets and each needs to be executed separately
[System.Serializable]
public class AbilityExcuteData
{
    public int AbilityLv;
    public int StatuDuration;
    public string Caster;
    public float AbilityMainParameter;
    public string[] Targets;
    public string[] Parameters;
    public string ExcuteUid;

    public AbilityExcuteData(string uid)
    {
        ExcuteUid = uid;
    }
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

