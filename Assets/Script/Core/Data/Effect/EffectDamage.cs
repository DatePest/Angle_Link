using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "EffectDamage", menuName = "AL/EffectData/Damage")]
public class EffectDamage : EffectData
{
    protected override string ParametersDesc { get => "null"; }
    public override void DoEffect(BattleLogic logic, AbilityExcuteData ability)
    {
        var C = logic.GetUnit(ability.Caster);
        foreach(var target in ability.Targets)
        {
            var T = logic.GetUnit(target);
            var d = C.UnitAttribute.Atk;
            logic.Damage(C, T, d, ability.ExcuteUid);
        }
    }

    public override void UnDoEffect(BattleLogic logic, AbilityExcuteData ability)
    {
        return;
    }

}