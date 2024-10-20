using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public string Uid;
    public int SoltId;
    public NetworkFilteredData<IUnitData> UnitData;
    public UnitAttribute UnitAttribute;
    public List<Statu> Status = new();
    public CharacterAbilityData_Net characterAbilityData;
    Unit(string uid )
    {
        Uid = uid;
    }
    public void SetSlot(int slotID) => SoltId = slotID;
    
    public void AddStatu(Statu data)
    {
        data.SetOwner(this);
        Status.Add(data);
    }
    public void RemoveStatu(string data)
    {
        Statu target = default;
        foreach (Statu statu in Status)
        {
            if(statu.Uid == data)
                target = statu;
        }
        Status.Remove(target);
    }
    public void RemoveStatu(StatuData data)
    {
        Statu target = default;
        foreach (Statu statu in Status)
        {
            if (statu.StatuData == data)
                target = statu;
        }
        Status.Remove(target);
    }
    public void RemoveStatu(Statu data) => RemoveStatu(data.Uid);
    public static Unit Create(string uid, IUnitData data)
    {
        var u = new Unit(uid);
        u.UnitAttribute = new UnitAttribute(data.Attribute);
        u.UnitData = new NetworkFilteredData<IUnitData>(data, GameConstant.PackName_GaneCore);
        return u;
    }
    public static Unit Create(string uid, Character data)
    {
        var u = new Unit(uid);
        u.UnitAttribute = new UnitAttribute(data.UnitAttribute);
        u.UnitData = new NetworkFilteredData<IUnitData> (data.characterData , GameConstant .PackName_GaneCore);
        u.characterAbilityData = data.characterAbilityData;
        return u;
    }

    public int GetUnitAbilityLV(string AbilityDataName)
    {
        var Ab = UnitData.GetData().Ability;

        if (Ab.Ability_1.name == AbilityDataName)
            return characterAbilityData.AbilityLv_1;
        if (Ab.Ability_2.name == AbilityDataName)
            return characterAbilityData.AbilityLv_2;
        if (Ab.Ability_SP.name == AbilityDataName)
            return characterAbilityData.AbilityLv_SP;
        if (Ab.AbilityPassive_1.name == AbilityDataName)
            return characterAbilityData.AbilityPassiveLv_1;
        if (Ab.AbilityPassive_2.name == AbilityDataName)
            return characterAbilityData.AbilityPassiveLv_2;
        return default;

    }
}
public abstract class IUnitData : ScriptableObject
{
    public string UnitName;
    public UnitAttribute Attribute;
    public UnitArt Art;
    public UnitAbility Ability;
}
public enum AtkType
{
    physics = 0,
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
public class UnitAttribute
{
    public int Lv = 1;
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
    public UnitAttribute() { }
     public UnitAttribute(UnitAttribute other)
    {
        this.Lv = other.Lv;
        this.HP = other.HP;
        this.Atk = other.Atk;
        this.Def = other.Def;
        this.MAtk = other.MAtk;
        this.MDef = other.MDef;
        this.Speed = other.Speed;
        this.CritRate = other.CritRate;

        this.AtkType = other.AtkType;
        this.RoleType = other.RoleType;
    }

    public void CalculateLvGrowth(CharacterAbilityData_Net data)
    {
        //Todo calculate UnitAttribute
        HP = HP + Lv * 100;
        Atk = Atk + Lv * 10;
        Def = Def + Lv * 5;
        MAtk = MAtk + Lv * 10;
        MDef = MDef + Lv * 7;
    }
    public int BattleScore { get { return HP + Atk + Def + MAtk + MDef + Speed; } }
}
[System.Serializable]
public class UnitAbility
{
    public AbilityData Ability_1;
    public AbilityData Ability_2;
    public AbilityData Ability_SP;
    public AbilityData AbilityPassive_1;
    public AbilityData AbilityPassive_2;
}

[System.Serializable]
public class UnitArt
{
    public Sprite CharacterIcon;
    public Sprite SDIcon;
    public Sprite Develop;
    public Sprite Casting;
    public Sprite Overkill;
    // public Sprite Injuried;
}

[CreateAssetMenu(fileName = "Character", menuName = "AL/Character", order = 10)]
public class CharacterData : IUnitData
{
    public int CharacterRank = 1;
    public CharacterPersonal characterPersonal;

}
[System.Serializable]
public class CharacterPersonal
{
    public int Birthday_month;
    public int Birthday_day;
    public int Height;
    public string CV;
    public string Designer;
}
[System.Serializable]
public class Character
{
    public UnitAttribute UnitAttribute;
    public CharacterData characterData;
    public CharacterAbilityData_Net characterAbilityData;
    public void SetCharacterData(CharacterData data)
    {
        UnitAttribute = new UnitAttribute(data.Attribute);
        characterData = data;
    }
    public void SetAttributeData(CharacterAbilityData_Net data)
    {
        characterAbilityData = data;
        UnitAttribute.Lv = data.Lv;
        UnitAttribute.CalculateLvGrowth(characterAbilityData);
    }

    public static Character Create(CharacterAbilityData_Net character_Net)
    {
        var C = new Character();
        var data = YooAsset_Tool.GetPackageData<CharacterData>("GameCore", character_Net.IUnitDataName);
        C.SetCharacterData(data);
        C.SetAttributeData(character_Net);

        return C;

    }
}


public class Monster : IUnitData
{

    public BattleSelectOrder GetMonsterAction(BattleLogic logic)
    {
        // Todo GetMonsterAction
        return default;
    }
}