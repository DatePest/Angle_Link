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
    [TextArea(2, 2)]
    public string EffectDesc;

    [TextArea(2,2)]
    [SerializeField]
    string parametersDesc;
    protected abstract string ParametersDesc { get; } // get => "your Desc{int,int}"; 


    public abstract void DoEffect(BattleLogic logic ,AbilityExcuteData ability);

 
    public abstract void UnDoEffect(BattleLogic logic, AbilityExcuteData ability);
    public virtual void DoEffect(Unit Target, AbilityExcuteData ability) { }
    public virtual void UnDoEffect(Unit Target, AbilityExcuteData ability) { }



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

    public void Excute(BattleLogic logic, AbilityExcuteData data)
    {
       
        effect?.DoEffect(logic,data);
    }

    public void SetParameter(AbilityExcuteData data)
    {
        SetOtherParameters(data);
        SettMainParameter(data);
    }
    void SetOtherParameters( AbilityExcuteData data)
    {
        if (OtherParameters == null || OtherParameters.Length <= 0) return;
        data.Parameters = OtherParameters;
    }

    void SettMainParameter( AbilityExcuteData data)
    {
        string description = CalculateFormula.Replace("{Lv}", data.AbilityLv.ToString());
        var f =OtherTool.StringCalculateFormulaCompute(description);
        data.AbilityMainParameter = f;

    }
}