using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public byte OwnerID;
    public string Uid;
    public int SoltId;
    public NetworkFilteredData<IUnitData> UnitData;
    public UnitAttribute UnitAttribute;
    public List<UnitAbility> unitAbilitys;
    public CharacterData_Net characterAbilityData;
    public List<Statu> Status = new();

    [NonSerialized] public GameObject Solt;
    public void SetSlot(int slotID) => SoltId = slotID;

    #region Statu
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
            if (statu.Uid == data)
                target = statu;
        }
        Status.Remove(target);
    }
    public void RemoveStatu(StatuData data)
    {
        Statu target = default;
        foreach (Statu statu in Status)
        {
            if (statu.StatuData.GetData() == data)
                target = statu;
        }
        Status.Remove(target);
    }
    public void RemoveStatu(Statu data) => RemoveStatu(data.Uid);
    #endregion
    public static Unit Create(string uid, IUnitData data, byte OwnerID, CharacterData_Net data_Net = null)
    {
        var u = new Unit();
        u.Uid = uid;
        u.OwnerID = OwnerID;
        u.UnitAttribute = UnitAttribute.Create(data.Attribute);
        u.UnitData = new NetworkFilteredData<IUnitData>(data, GameConstant.PackName_GameCore);
        if (data_Net == null)
        {
            data_Net =  CharacterData_Net.Create(data);
        }
        u.characterAbilityData = data_Net;
        u.unitAbilitys = new();
        foreach (var ability in u.UnitData.GetData().Ability.GetAbilityDatas())
        {
            UnitAbility Ua =  UnitAbility.Create(u, ability);
            u.unitAbilitys.Add(Ua);
        }
        u.UnitAttribute.CalculateLvGrowth(data_Net);
        return u;
    }
    public static Unit Create(string uid, Character data, byte OwnerID)
    {
        return Create(uid, data.characterData.GetData(), OwnerID, data.characterNetData);
        //var u = new Unit();
        //u.Uid = uid;
        //u.OwnerID = OwnerID;
        //u.UnitAttribute = UnitAttribute.Create(data.UnitAttribute);
        //u.UnitData = new NetworkFilteredData<IUnitData>(data.characterData.GetData(), GameConstant.PackName_GameCore);
        //u.characterAbilityData = data.characterNetData;

        //u.unitAbilitys = new();
        //foreach (var ability in u.UnitData.GetData().Ability.GetAbilityDatas())
        //{
        //    UnitAbility Ua = UnitAbility.Create(u, ability);
        //    u.unitAbilitys.Add(Ua);
        //}
        //return u;
    }
    public UnitAbility GetUnitAbility(AbilityOrderTag Ordar)
    {
        foreach (var a in unitAbilitys)
        {
            if (a.Ordar == Ordar)
                return a;
        }
        return null;
    }
    public UnitAbility GetUnitAbility(int Ordar)
    {
       var tag = AbilityOrderTagExtensions.ToAbilityOrderTag(Ordar);
       return GetUnitAbility(tag);
    }
    
    public UnitAbility GetUnitAbility(string Name)
    {
        foreach (var a in unitAbilitys)
        {
            if (a.Name == Name)
                return a;
        }
        return null;
    }
    public ArtData GetArt() => UnitData.GetData().Art;
}
[System.Serializable]
public class UnitAbility
{
    public string Name;
    public NetworkFilteredData<AbilityData> abilityData;
    public int CurrentCoolDown;
    public int Lv;
    public AbilityOrderTag Ordar;
    public int BaseCoolDown => abilityData.GetData().CoolDown;
    public static UnitAbility Create(Unit u, AbilityData Adata)
    {
        var Ua = new UnitAbility();
        Ua.Name = Adata.name;
        Ua.abilityData = new NetworkFilteredData<AbilityData>(Adata);
        Ua.SetOrder(u);
        Ua.Lv = Ua.SetLv(u);
        return Ua;
    }

    int SetLv(Unit u)
    {
        var Ab = u.UnitData.GetData().Ability;
        if (Ab.Ability_NormalAttack.name == Name)
            return 1;
        if (Ab.Ability_1.name == Name)
            return u.characterAbilityData.AbilityLv_1;
        if (Ab.Ability_2.name == Name)
            return u.characterAbilityData.AbilityLv_2;
        if (Ab.Ability_SP.name == Name)
            return u.characterAbilityData.AbilityLv_SP;
        if (Ab.AbilityPassive_1.name == Name)
            return u.characterAbilityData.AbilityPassiveLv_1;
        if (Ab.AbilityPassive_2.name == Name)
            return u.characterAbilityData.AbilityPassiveLv_2;

        return 1;

    }
    void SetOrder(Unit u)
    {
        var Ab = u.UnitData.GetData().Ability;
        if (Ab.Ability_NormalAttack.name == Name)
        {
            Ordar = AbilityOrderTag.Ability_NormalAttack;
            return;
        }
        if (Ab.Ability_1.name == Name)
        {
            Ordar = AbilityOrderTag.Ability_1;
            return;
        }
        if (Ab.Ability_2.name == Name)
        {
            Ordar = AbilityOrderTag.Ability_2;
            return;
        }
        if (Ab.Ability_SP.name == Name)
        {
            Ordar = AbilityOrderTag.Ability_SP;
            return;
        }
        if (Ab.AbilityPassive_1.name == Name)
        {
            Ordar = AbilityOrderTag.AbilityPassive_1;
            return;
        }
        if (Ab.AbilityPassive_2.name == Name)
        {
            Ordar = AbilityOrderTag.AbilityPassive_2;
            return;
        }
    }
}
[System.Serializable]
public enum AbilityOrderTag
{
    None = 0,
    Ability_NormalAttack = 1,
    Ability_1 = 2,
    Ability_2 = 3,
    Ability_SP = 4,
    AbilityPassive_1 = 5,
    AbilityPassive_2 = 6

}
public static class AbilityOrderTagExtensions
{
    public static AbilityOrderTag ToAbilityOrderTag(this int value)
    {
        if (Enum.IsDefined(typeof(AbilityOrderTag), value))
        {
            return (AbilityOrderTag)value;
        }
        return AbilityOrderTag.None; // 預設回傳 None
    }
}



[System.Serializable]
public class UnitAttribute : AttributeData
{
    public int Lv;
    public int HPMax;
    public static UnitAttribute Create(AttributeData other)
    {
        var data =new UnitAttribute();
        data.HP = other.HP;
        data.HPMax = other.HP;
        data.Atk = other.Atk;
        data.Def = other.Def;
        data.MAtk = other.MAtk;
        data.MDef = other.MDef;
        data.Speed = other.Speed;
        data.CritRate = other.CritRate;

        data.AtkType = other.AtkType;
        data.RoleType = other.RoleType;
        return data;
    }
    public void CalculateLvGrowth(CharacterData_Net data)
    {
        Lv = data.Lv;
        //Todo calculate UnitAttribute
        HP = HP + Lv * 100;
        HPMax = HP;
        Atk = Atk + Lv * 10;
        Def = Def + Lv * 5;
        MAtk = MAtk + Lv * 10;
        MDef = MDef + Lv * 7;
    }
    public int BattleScore { get { return HP + Atk + Def + MAtk + MDef + Speed; } }
   
}
