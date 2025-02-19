using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static EffectData;

public abstract class EffectData : ScriptableObject
{
    [TextArea(5,5)]
    public string EffectDesc;

    [TextArea(5,5)]
    [SerializeField]
    string parametersDesc;
    protected abstract string ParametersDesc { get; } // get => "your Desc{int,int}"; 


    public abstract void DoEffect(BattleLogic logic ,AbilityEffectsExcuteData ability);

 
    public abstract void UnDoEffect(BattleLogic logic, AbilityEffectsExcuteData ability);
    public virtual void DoEffect(Unit Target, AbilityEffectsExcuteData ability) { }
    public virtual void UnDoEffect(Unit Target, AbilityEffectsExcuteData ability) { }



    public EffectData()
    {
        parametersDesc = "Parameters : " +ParametersDesc;
    }
}

[System.Serializable]
public class Effect 
{
    public EffectData effect;
    public string CalculateFormula; // LvCalculateFormula "100 * {Lv} * 50"
    public string[] OtherParameters;

    public void Excute(BattleLogic logic, AbilityEffectsExcuteData data)
    {
       
        effect?.DoEffect(logic,data);
    }

    public void SetParameter(AbilityEffectsExcuteData data)
    {
        SetOtherParameters(data);
        SettMainParameter(data);
    }
    void SetOtherParameters( AbilityEffectsExcuteData data)
    {
        if (OtherParameters == null || OtherParameters.Length <= 0) return;
        data.Parameters = OtherParameters;
    }

    void SettMainParameter( AbilityEffectsExcuteData data)
    {
        string description = CalculateFormula.Replace("{Lv}", data.AbilityLv.ToString());
        var f =OtherTool.StringCalculateFormulaCompute(description);
        data.AbilityMainParameter = f;

    }
}