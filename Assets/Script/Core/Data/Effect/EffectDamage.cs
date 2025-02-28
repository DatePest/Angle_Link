using UnityEngine;
[CreateAssetMenu(fileName = "EffectDamage", menuName = "AL/EffectData/Damage")]
public class EffectDamage : EffectData
{
    public EffectDamage()
    {
        EffectDesc = "Damage = (MainP + TypeAtk)To [ModfireType][Parameters] ";
    }
   
    protected override string ParametersDesc { get => "AtkType{int},ModfireType(int),ModfireParametersTypeTool{f}, "; }
    
    public override void DoEffect(BattleLogic logic, AbilityEffectsExcuteData ability)
    {
        var type = ParametersTypeTool.GetAtkType(int.Parse(ability.Parameters[0]));
        var modfireType = ParametersTypeTool.GetModfireType(int.Parse(ability.Parameters[1]));
        var ModfireParameters = float.Parse(ability.Parameters[2]);

  
        var C = logic.GetUnit(ability.Caster);
        GameUtilityTool.DebugAsync($"{C.characterAbilityData.AssetName} Atk :{C.UnitAttribute.Atk} ");
        foreach (var target in ability.Targets)
        {
            var T = logic.GetUnit(target);
            int damage = (int)ability.AbilityMainParameter;
            if(type == AtkType.physics)
            {
                damage += C.UnitAttribute.Atk;
            }
            else
            {
                damage += C.UnitAttribute.MAtk;
            }
            int _Value = ParametersTypeTool.CalculateAddValue(damage, ModfireParameters, modfireType);

            logic.Damage(C, T, _Value, type, ability.ExcuteUid);
        }
    }

    public override void UnDoEffect(BattleLogic logic, AbilityEffectsExcuteData ability)
    {
        return;
    }


}