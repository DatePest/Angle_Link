using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.TextCore.Text;

public class AssetFInd
{
    public static async Task<GameLevelData> GetGameLevelData_Async(string Name )
    {
        var data = await YooAsset_Tool.GetPackageData_Async<GameLevelData>(GameConstant.PackName_GameCore, Name);
        if (data == null) throw new Exception($"GameLevelData {Name} is null");
      
        return data;
    }
    public static async Task<CharacterData> GetCharacterData_Async(string Name)
    {
        var data = await YooAsset_Tool.GetPackageData_Async<CharacterData>(GameConstant.PackName_GameCore, "CharactersData", Name);
        if (data == null) throw new Exception($"CharacterData {Name} is null");
        return data;
    }

    public static async Task<CharacterData_Net> GetNewCharacterNet_Async(string Name )
    {
        var data = await GetCharacterData_Async(Name);
        var net = CharacterData_Net.Create(data);
        return net;
    }
    public static async Task<ItemData> GetItemData_Async(string Name)
    {
        var data = await YooAsset_Tool.GetPackageData_Async<ItemData>(GameConstant.PackName_GameCore, "Items", Name);
        if (data == null) throw new Exception($"Item {Name} is null");
        return data;
    }
    public static async Task<Item_Net> GetNewItemNet_Async(string Name)
    {
        var data = await GetItemData_Async(Name);
        return Item_Net.Create(data);
    }
}