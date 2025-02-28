using Assets.Script.Core.GameDevelop;
using GameApi;
using System;
using System.Collections.Generic;
using Unity.Netcode;

/// <summary>
// Player data for client and server
/// </summary>
/// 


[System.Serializable]
public class UserData : INetworkSerializable
{
    public string   Email;
    public string   Username;
    public string   Access_Token;
    public PlayerData_Net PlayerData;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Email);
        serializer.SerializeValue(ref Username);
        serializer.SerializeValue(ref Access_Token);
        NetworkTool.SerializeObject(serializer,ref PlayerData);
    }

}

[System.Serializable]
public class PlayerData_Net : INetworkSerializable
{
    public string Name;
    public int Money;
    public int PaidCoins;
    public int Lv;
    public int Exp;
    public int Stamina;
    public string UID;
    public List<CharacterData_Net> Characters;
    public List<Item_Net> Items;

    public void AddCharacter(CharacterData_Net characterData)
    {
        if(Characters == null) Characters = new List<CharacterData_Net>();
        Characters.Add(characterData);
    }
    public int GetNextLvExp() => Develop_PlayerLv.GetLvNeedExp(Lv);
    public int GetStaminaMax() => Develop_PlayerLv.GetStaminaMax(Lv);
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Name);
        serializer.SerializeValue(ref Money);
        serializer.SerializeValue(ref PaidCoins);
        serializer.SerializeValue(ref Lv);
        serializer.SerializeValue(ref Exp);
        serializer.SerializeValue(ref Stamina);
        serializer.SerializeValue(ref UID);
        NetworkTool.NetSerialize(serializer, ref Characters);
        NetworkTool.NetSerialize(serializer, ref Items);
    }
   

    public static PlayerData_Net DTO(Api_PlayerData res)
    {
        var P = new PlayerData_Net();
        P.Name = res.Name;
        P.Money = res.Money;
        P.PaidCoins = res.PaidCoins;
        P.Lv = res.Lv;
        P.Exp = res.Exp;
        P.UID = res.UID;
        P.Stamina = res.Stamina;

        if (res.Characters != null)
            P.Characters = WebTool.JsonToObject<List<CharacterData_Net>>(res.Characters);
        if (res.Items != null)
            P.Items = WebTool.JsonToObject<List<Item_Net>>(res.Items);
        return P;
    }
    public Api_PlayerData DTO()
    {
        var P = new Api_PlayerData();
        P.Name = Name;
        P.Money = Money;
        P.PaidCoins = PaidCoins;
        P.Lv = Lv;
        P.Exp = Exp;
        P.UID = UID;
        P.Stamina = Stamina;
        if (Characters != null)
            P.Characters = WebTool.ToJson(Characters);
        if (Items != null)
            P.Items = WebTool.ToJson(Items);
        return P;
    }

}

[System.Serializable]
public class CharacterData_Net :INetworkSerializable
{
    public string AssetName;
    public int Lv;
    public int CurrentExp;
    public int AbilityLv_1;
    public int AbilityLv_2;
    public int AbilityLv_SP;
    public int AbilityPassiveLv_1;
    public int AbilityPassiveLv_2;
    public int Favorability;
    public int CharacterRank;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref AssetName);
        serializer.SerializeValue(ref Lv);
        serializer.SerializeValue(ref CurrentExp);
        serializer.SerializeValue(ref AbilityLv_1);
        serializer.SerializeValue(ref AbilityLv_2);
        serializer.SerializeValue(ref AbilityLv_SP);
        serializer.SerializeValue(ref AbilityPassiveLv_1);
        serializer.SerializeValue(ref AbilityPassiveLv_2);
        serializer.SerializeValue(ref Favorability);
        serializer.SerializeValue(ref CharacterRank);
    }
    public static CharacterData_Net Create(IUnitData Unitdata)
    {
        var data = Create();
        data.AssetName = Unitdata.AssetName;
        return data;
    }
    public static CharacterData_Net Create(CharacterData character)
    {
        var data = Create();
        data.CharacterRank = character.CharacterRank;
        data.AssetName = character.AssetName;
        return data;
    }
    public static CharacterData_Net Create()
    {
        var data = new CharacterData_Net();
        data.Lv = 1;
        data.AbilityLv_1 = 1;
        data.AbilityLv_2 = 1;
        data.AbilityLv_SP = 1;
        return data;
    }
    public static CharacterData_Net DTO(CharacterDataResponse response)
    {
        var data = new CharacterData_Net();
        data.AssetName = response.AssetName;
        data.Lv = response.Lv;
        data.CurrentExp = response.CurrentExp;
        data.AbilityLv_1 = response.AbilityLv_1;
        data.AbilityLv_2 = response.AbilityLv_2;
        data.AbilityLv_SP = response.AbilityLv_SP;
        data.AbilityPassiveLv_1 = response.AbilityPassiveLv_1;
        data.AbilityPassiveLv_2 = response.AbilityPassiveLv_2;
        data.Favorability = response.Favorability;
        data.CharacterRank = response.CharacterRank;

        return data;

    }

}

    [System.Serializable]
public class Item_Net : INetworkSerializable
{
    public string AssetName;
    public int Amount;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref AssetName);
        serializer.SerializeValue(ref Amount);
    }
    public static Item_Net DTO(ItemDataResponse response)
    {
        var data = new Item_Net();
        data.AssetName = response.Name;
        data.Amount = response.Amount;
        return data;
    }
    public static Item_Net Create(ItemData item)
    {
        var data = new Item_Net();
        data.AssetName = item.name;
        data.Amount = 1;
        return data;
    }
    public static Item_Net Create(DataObject item)
    {
        var data = new Item_Net();
        data.AssetName = item.AssetName;
        data.Amount = item.Amount;
        return data;
    }
}

[System.Serializable]
public class MsgEvent_Net : INetworkSerializable
{
    public string msg = "";
    public int MsgId;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref msg);
        serializer.SerializeValue(ref MsgId);
    }
}
[System.Serializable]
public class Response_Net : INetworkSerializable
{
    public string msg = "";
    public bool success;
    public string DataJosn = "";
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref msg);
        serializer.SerializeValue(ref success);
        serializer.SerializeValue(ref DataJosn);
       
    }
}
public class SettlementDatas_Net : INetworkSerializable
{
    public int Money;
    public int Exp;
    public List<DataObject> Characters;
    public List<DataObject> Items;
    [NonSerialized] public List<CharacterData_Net> CharactersNets;

    public void AddItem(ItemData item, int Amount)
    {
        Amount = Math.Max(Amount, 1);

        if (item.AssetName == GameConstant.Item_Money)
        {
            Money = Amount;
            return;
        }

        if (Items == null) Items = new();
        var i = new DataObject(item.AssetName, Amount);
        i.Arg1 = item.CanStack; // Tag CanStack
        Items.Add(i);

    }
    public void AddCharacter(CharacterData Cdata)
    {
        if (Characters == null) Characters = new();
        if (CharactersNets == null) CharactersNets = new();
        var i = new DataObject(Cdata.AssetName,1);
        Characters.Add(i);
        CharactersNets.Add(CharacterData_Net.Create(Cdata));
    }
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Money);
        serializer.SerializeValue(ref Exp);
        NetworkTool.NetSerialize(serializer, ref Characters);
        NetworkTool.NetSerialize(serializer, ref Items);
    }
}