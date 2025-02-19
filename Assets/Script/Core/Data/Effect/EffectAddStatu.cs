using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "EffectAddStatu", menuName = "AL/EffectData/AddStatu")]
public class EffectAddStatu : EffectData
{
    public StatuData statuData;
    //Duration passed to the effect (usually for status, 0 =permanent)
    protected override string ParametersDesc { get => "null"; }

    public override void DoEffect(BattleLogic logic, AbilityEffectsExcuteData ExcuteData)
    {
        var u = logic.GetUnit(ExcuteData.Caster);
        var s = new Statu(ExcuteData, statuData);
        u.AddStatu(s);
    }

    public override void UnDoEffect(BattleLogic logic, AbilityEffectsExcuteData ExcuteData)
    {
        throw new NotImplementedException();
    }

}