using Assets.Script.Core.Data;
using NUnit.Framework;
using RngDropTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Playables;
using UnityEngine;


public abstract class IUnitData : iSobj_Name, IRngItem
{
    public string UnitName;
    public AttributeData Attribute;
    public ArtData Art;
    public AbilityDatas Ability;
    public string Name { get => AssetName; }
}
public enum AtkType
{
    physics = 1,
    magic = 2
}
public enum RoleType
{
    Attacker = 0,
    Support = 2,
    Healer = 4,
    Tank = 8
}

[System.Serializable]
public class AttributeData
{
    public int HP;
    public int Atk;
    public int Def;
    public int MAtk;
    public int MDef;
    public int Speed;
    public int CritRate;
    //
    public AtkType AtkType;
    public RoleType RoleType;
    //
}
[System.Serializable]
public class AbilityDatas
{
    public AbilityData Ability_NormalAttack;
    public AbilityData Ability_1;
    public AbilityData Ability_2;
    public AbilityData Ability_SP;
    public AbilityData AbilityPassive_1;
    public AbilityData AbilityPassive_2;


    public List<AbilityData> GetAbilityDatas()
    {
        List<AbilityData> datas = new List<AbilityData>();
        if (Ability_NormalAttack != null)
            datas.Add(Ability_NormalAttack);
        if (Ability_1 != null)
            datas.Add(Ability_1);
        if (Ability_2 != null)
            datas.Add(Ability_2);
        if (Ability_SP != null)
            datas.Add(Ability_SP);
        if (AbilityPassive_1 != null)
            datas.Add(AbilityPassive_1);
        if (AbilityPassive_2 != null)
            datas.Add(AbilityPassive_2);
        return datas;
    }
}

[System.Serializable]
public class ArtData
{
    public Sprite CharacterIcon;
    public Sprite SDIcon;
    public Sprite Develop;
    public Sprite Casting;
    public Sprite Overkill;
    // public Sprite Injuried;
}
