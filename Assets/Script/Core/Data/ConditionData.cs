using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ConditionData : ScriptableObject
{
    public virtual bool IsTargetConditionMet(AbilityData ability, Unit caster, Unit target)
    {
        return true; //Override this, condition targeting player
    }

    public bool CompareBool(bool condition, ConditionOperatorBool oper)
    {
        if (oper == ConditionOperatorBool.IsFalse)
            return !condition;
        return condition;
    }

    public bool CompareInt(int ival1, ConditionOperatorInt oper, int ival2)
    {
        if (oper == ConditionOperatorInt.Equal)
        {
            return ival1 == ival2;
        }
        if (oper == ConditionOperatorInt.NotEqual)
        {
            return ival1 != ival2;
        }
        if (oper == ConditionOperatorInt.GreaterEqual)
        {
            return ival1 >= ival2;
        }
        if (oper == ConditionOperatorInt.LessEqual)
        {
            return ival1 <= ival2;
        }
        if (oper == ConditionOperatorInt.Greater)
        {
            return ival1 > ival2;
        }
        if (oper == ConditionOperatorInt.Less)
        {
            return ival1 < ival2; ;
        }
        return false;
    }

    public enum ConditionOperatorInt
    {
        Equal,
        NotEqual,
        GreaterEqual,
        LessEqual,
        Greater,
        Less,
    }

    public enum ConditionOperatorBool
    {
        IsTrue,
        IsFalse,
    }
}
