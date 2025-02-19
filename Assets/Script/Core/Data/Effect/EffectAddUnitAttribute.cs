using PlasticGui.Configuration.CloudEdition.Welcome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Playables;
using UnityEngine;
using static EffectAddUnitAttribute;
using static UnityEngine.GraphicsBuffer;
[CreateAssetMenu(fileName = "EffectAddUnitAttribute", menuName = "AL/EffectData/AddUnitAttribute")]
public class EffectAddUnitAttribute : EffectData
{
    protected override string ParametersDesc => "AttributeType(int)),ModfireType(int))";
    
   
    public override void DoEffect(BattleLogic logic, AbilityEffectsExcuteData ability)
    {
        var target = logic.GetUnit(ability.Caster);
        Modfire(target, ability,true);
    }

    public override void UnDoEffect(BattleLogic logic, AbilityEffectsExcuteData ability)
    {
        var target = logic.GetUnit(ability.Caster);
        Modfire(target, ability, false);
    }
    public override void DoEffect(Unit Target, AbilityEffectsExcuteData ability)
    {
        Modfire(Target, ability, true);
    }

    public override void UnDoEffect(Unit Target, AbilityEffectsExcuteData ability)
    {
        Modfire(Target, ability, false);
    }
    void Modfire(Unit target, AbilityEffectsExcuteData ability , bool isDo)
    {
        var modfireType = ParametersTypeTool.GetModfireType(int.Parse(ability.Parameters[1]));
        var attributeType = ParametersTypeTool.GetAttributeType(int.Parse(ability.Parameters[0]));
        var Value = ability.AbilityMainParameter;
        ref int RefTargetValue = ref ParametersTypeTool.GetTargetAttribute(target, attributeType);
        int _Value = ParametersTypeTool.CalculateAddValue(RefTargetValue, Value, modfireType);

        if (!isDo) _Value = -_Value;

         RefTargetValue += _Value;
    }

   
    

 
}