using Unity.Netcode;
using UnityEngine;

/// <summary>
// Player data for client and server
/// </summary>
/// 


[System.Serializable]
public class UserData : INetworkSerializable
{
    public string   Email;
    public string   Username;
    public string   Password;
    public string   Access_Token;
    public PlayerData_Net PlayerData;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Email);
        serializer.SerializeValue(ref Username);
        serializer.SerializeValue(ref Password);
        serializer.SerializeValue(ref Access_Token);
        if (serializer.IsReader)
        {
            int size = 0;
            serializer.SerializeValue(ref size);
            if (size > 0)
            {
                byte[] bytes = new byte[size];
                serializer.SerializeValue(ref bytes);
                PlayerData = NetworkTool.Deserialize<PlayerData_Net>(bytes);
            }
        }

        if (serializer.IsWriter)
        {
            byte[] bytes = NetworkTool.Serialize(PlayerData);
            int size = bytes.Length;
            serializer.SerializeValue(ref size);
            if (size > 0)
                serializer.SerializeValue(ref bytes);
        }

    }

}

[System.Serializable]
public class PlayerData_Net : INetworkSerializable
{
    public string PlayerName;
    public int Money;
    public int PaidCoins;
    public int Lv = 1;
    public int Exp;
    public int Stamina;
    public CharacterData_Net[] Characters;
    public Item_Net[] Items;


    public int GetNextLvExp()
    {
        return Lv * 100;
    }
    public int GetStaminaMax()
    {
        return Lv * 10;
    }
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Money);
        serializer.SerializeValue(ref PaidCoins);
        serializer.SerializeValue(ref Lv);
        serializer.SerializeValue(ref Exp);
        NetworkTool.NetSerializeArray(serializer, ref Characters);
        NetworkTool.NetSerializeArray(serializer, ref Items);
    }
}

[System.Serializable]
public class CharacterData_Net : INetworkSerializable
{
    public string IUnitDataName;
    public int Lv = 1;
    public int AbilityLv_1 = 1;
    public int AbilityLv_2 = 1;
    public int AbilityLv_SP = 1;
    public int AbilityPassiveLv_1 = 1;
    public int AbilityPassiveLv_2 = 1;
    public int Favorability;
    public int CharacterRank;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref IUnitDataName); 
    }
    public static CharacterData_Net Create(CharacterData character)
    {
        var data =new CharacterData_Net();
        data.CharacterRank = character.CharacterRank;
        data.IUnitDataName = character.name;
        return data;
    }
}

    [System.Serializable]
public class Item_Net : INetworkSerializable
{
    public string ID;
    public int Quantity;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ID);
        serializer.SerializeValue(ref Quantity);
    }
}

[System.Serializable]
public struct MsgEvent : INetworkSerializable
{
    public string msg;
    public int MsgId;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref msg);
        serializer.SerializeValue(ref MsgId);
    }
}