﻿using PlasticGui.Configuration.CloudEdition.Welcome;
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
    public enum ModfireType
    {
        Addition = 1,
        Multiplication = 2
    }
    public enum AttributeType
    {
        Hp = 1,
        Atk = 2,
        Def = 3,
        Matk = 4,
        MDef = 5,
        Speed = 6,
        CritRate = 7
    }

    public override void DoEffect(BattleLogic logic, AbilityExcuteData ability)
    {
        var target = logic.GetUnit(ability.Caster);
        Modfire(target, ability,true);
    }

    public override void UnDoEffect(BattleLogic logic, AbilityExcuteData ability)
    {
        var target = logic.GetUnit(ability.Caster);
        Modfire(target, ability, false);
    }
    public override void DoEffect(Unit Target, AbilityExcuteData ability)
    {
        Modfire(Target, ability, true);
    }

    public override void UnDoEffect(Unit Target, AbilityExcuteData ability)
    {
        Modfire(Target, ability, false);
    }
    void Modfire(Unit target, AbilityExcuteData ability , bool isDo)
    {
        var modfireType = GetModfireType(int.Parse(ability.Parameters[1]));
        var attributeType = GetAttributeType(int.Parse(ability.Parameters[0]));
        var Value = ability.AbilityMainParameter;
        var RefTargetValue = GetTargetAttribute(target, attributeType);
        int _Value = CalculateAddValue(ref RefTargetValue, Value, modfireType);

        if (!isDo) _Value = -_Value;

        RefTargetValue += _Value;
    }
    int CalculateAddValue(ref int a, float Value, ModfireType modfire)
    {
        switch (modfire)
        {
            case ModfireType.Addition:
                return (int)Value; ;
            case ModfireType.Multiplication:
                return (int)Math.Round(a * Value);
            default:
                throw new ArgumentOutOfRangeException(nameof(ModfireType), "Invalid ModfireType .");
        }
    }
   
    ModfireType GetModfireType(int i)
    {
        if (Enum.IsDefined(typeof(ModfireType), i))
        {
            //var t = Enum.ToObject(typeof(ModfireType), i);
            return (ModfireType)i;
        }
        throw new ArgumentOutOfRangeException(nameof(i), "Invalid GetType.");
    }
    AttributeType GetAttributeType(int i)
    {
        if (Enum.IsDefined(typeof(AttributeType), i))
        {
            return (AttributeType)i;
        }
        throw new ArgumentOutOfRangeException(nameof(i), "Invalid GetType.");
    }
    ref int GetTargetAttribute(Unit unit, AttributeType attributeType)
    {

        var data = unit.UnitData.GetData();

        switch (attributeType)
        {
            case AttributeType.Hp:
                return ref data.Attribute.HP;
            case AttributeType.Atk:
                return ref data.Attribute.Atk;
            case AttributeType.Def:
                return ref data.Attribute.Def;
            case AttributeType.Matk:
                return ref data.Attribute.MAtk;
            case AttributeType.MDef:
                return ref data.Attribute.MDef;
            case AttributeType.Speed:
                return ref data.Attribute.Speed;
            case AttributeType.CritRate:
                return ref data.Attribute.CritRate;
            default:
                throw new ArgumentOutOfRangeException(nameof(attributeType), "Invalid attribute type.");
        }
    }

 
}