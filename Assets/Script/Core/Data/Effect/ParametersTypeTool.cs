using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParametersTypeTool
{
    public static ModfireType GetModfireType(int i)
    {
        if (Enum.IsDefined(typeof(ModfireType), i))
        {
            return (ModfireType)i;
        }
        throw new ArgumentOutOfRangeException(nameof(i), "Invalid GetType.");
    }
    public static AttributeType GetAttributeType(int i)
    {
        if (Enum.IsDefined(typeof(AttributeType), i))
        {
            return (AttributeType)i;
        }
        throw new ArgumentOutOfRangeException(nameof(i), "Invalid GetType.");
    }
    public static AtkType GetAtkType(int i)
    {
        if (Enum.IsDefined(typeof(AtkType), i))
        {
            return (AtkType)i;
        }
        throw new ArgumentOutOfRangeException(nameof(i), "Invalid GetType.");
    }
    public static ref int GetTargetAttribute(Unit unit, AttributeType attributeType)
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

    public static int CalculateAddValue(int a, float Value, ModfireType modfire)
    {
        switch (modfire)
        {
            case ModfireType.Addition:
                return a + (int)Value; ;
            case ModfireType.Multiplication:
                return (int)Math.Round(a * Value);
            default:
                throw new ArgumentOutOfRangeException(nameof(ModfireType), "Invalid ModfireType .");
        }
    }


}
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
