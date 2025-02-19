using GameApi;
using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.TextCore.Text;
using Assets.Script.Core.GameDevelop;
using static Assets.Script.Core.Server.ServerGameLogic;
using UnityEditor.PackageManager.Requests;

public static class PlayerData_NetExtensions
{
    public static void AddItem(this PlayerData_Net player, IEnumerable<DataObject> item)
    {
        foreach (var i in item)
        {
            if (i.AssetName == GameConstant.Item_Money)
            {
                player.Money = i.Amount;
                return;
            }
            ItemListAdd(player.Items, i);
        }
       
    }
    public static void ItemListAdd(List<Item_Net> Items,DataObject item)
    {
        if ((bool)item.Arg1)
        {
            foreach (var i in Items)
            {
                if (i.AssetName == item.AssetName)
                {
                    i.Amount += item.Amount;
                    return;
                }
            }
        }
        Items.Add(Item_Net.Create(item));
    }
    public static void ItemListRemove(List<Item_Net> Items, string AssetName,int Amount)
    {
        Item_Net tar = null;
        foreach (var i in Items)
        {
            if (i.AssetName == AssetName)
            {
                tar = i;
            }
        }
        tar.Amount -= Amount;
        if(tar.Amount <= 0)  Items.Remove(tar);
    }

    public static bool AddCharacter(List<CharacterData_Net> characterDatas , CharacterData_Net newcharacter)
    {
        foreach(var c in characterDatas)
        {
           if(c.AssetName == newcharacter.AssetName)
            {
                return true;
            }
        }
        characterDatas.Add(newcharacter);
        return false;
    }

}
public static class Api_PlayerDataExtensions
{
    public static List<CharacterData_Net> GetListCharacterData_Nets(this Api_PlayerData player)
    {
        var Pc = ApiTool.JsonToObject<List<CharacterData_Net>>(player.Characters);
        if (Pc == null) Pc = new();
        return Pc;
    }
    public static CharacterData_Net GetCharacterData_Net(this Api_PlayerData player, string name , out List<CharacterData_Net> characters)
    {
        characters = player.GetListCharacterData_Nets();
        return GetCharacterData_Net(characters, name);
    }
    public static CharacterData_Net GetCharacterData_Net(this Api_PlayerData player,string name)
    {
        var Pc = player.GetListCharacterData_Nets();
        return GetCharacterData_Net(Pc, name);
    }
    public static CharacterData_Net GetCharacterData_Net(IEnumerable<CharacterData_Net> characters, string name)
    {
        CharacterData_Net p = null;
        foreach (var c in characters)
        {
            if (c.AssetName.Equals(name))
            {
                p = c; break;
            }
        }
        return p;
    }


    public static List<Item_Net> GetListItem_Net(this Api_PlayerData player)
    {
        var PItems = ApiTool.JsonToObject<List<Item_Net>>(player.Items);
        if (PItems == null) PItems = new();
        return PItems;
    }
    public static void AddExp(this Api_PlayerData player, int Money)
    {
        player.Money += Money;
    }
    public static void AddMoney(this Api_PlayerData player, int Exp)
    {
        player.Exp += Exp;
    }
    public static void AddItem(this Api_PlayerData player, IEnumerable<DataObject> items)
    {
        if (items == null || items.Count() <= 0) return;
        var PItems = player.GetListItem_Net();

        foreach (var item in items)
        {
            if (item.AssetName == GameConstant.Item_Money)
            {
                player.Money += item.Amount;
                continue;
            }

            PlayerData_NetExtensions.ItemListAdd(PItems, item);
        }
        player.Items = ApiTool.ToJson(PItems);
    }
    public static void RemoveItem(this Api_PlayerData player, IEnumerable<DataObject> items)
    {
        if (items == null || items.Count() <= 0) return;
        var PItems = player.GetListItem_Net();

        foreach (var item in items)
        {
            if (item.AssetName == GameConstant.Item_Money)
            {
                player.Money -= item.Amount;
                continue;
            }

            PlayerData_NetExtensions.ItemListRemove(PItems, item.AssetName, item.Amount);
        }
        player.Items = ApiTool.ToJson(PItems);
    }
    public static  void AddCharacters(this Api_PlayerData player, IEnumerable<CharacterData_Net> characters)
    {
        if (characters == null || characters.Count() <= 0) return;
        var Pc =  player.GetListCharacterData_Nets();
        foreach (var c in characters)
        {
            var b =  PlayerData_NetExtensions.AddCharacter(Pc, c);
            //Todo : Duplicate character switching props
        }
        player.Characters = ApiTool.ToJson(Pc);
    }
    public static void AddCharacterToDevelop(this Api_PlayerData player,string name ,int exp)
    {
        if (name == null ||  exp <= 0) return;
        var c = player.GetCharacterData_Net(name,out var cs);
        if (c == null) return;

        Develop_CharacterLv.ToLevelUP(c, exp);
        player.Characters = ApiTool.ToJson(cs);
       
    }

}


