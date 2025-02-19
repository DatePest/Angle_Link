using System;
using GameApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetWorkServices;
using Tools;
namespace Client
{
    public class ClientUserData : IDisposable
    {
        public UserData UserData { get; private set; }
        public Action<UserData> UpdataPlayerDataCallBack;
        UserLoaclData LoaclData = new();
        Dictionary<string, object> TempCache = new();

        public T GetCache<T>(string name, bool Remove = false)
        {

            if (TempCache.TryGetValue(name, out var value))
            {
                if (Remove) RemoveCache(name);
                return (T)value;
            }
            return default;
        }
        public void RemoveCache(string name) => TempCache.Remove(name);
     
        public bool AddCache(string name,object data , bool Force=false)
        {
            if (TempCache.ContainsKey(name))
            {
                if (!Force)
                {
                    return false;
                }
                else
                {
                    TempCache.Remove(name);
                }
            }
            TempCache.Add(name, data);
            return true;
        }
        public void ClearCache() => TempCache.Clear();
        public void ReqUpdataPlayerData()
        {
            if (UserData == null) { return; }
            
            var msg= new UserGetPlayerDataRequest();
            msg.accesLogin_token =UserData.Access_Token;
            msg.username =UserData.Username;
            EventSystemToolExpand.Publish(ClientEventTag.SendGetUserData, NetworkMsg_HandlerTag.Account, default, msg);
        }
        public void SetUserData(UserData data)
        {
            UserData = data;
            UpdatePlayerData(data.PlayerData);
        }
        public async void UpdatePlayerData(PlayerData_Net data)
        {
            UserData.PlayerData = data;
            await Updata();
            UpdataPlayerDataCallBack?.Invoke(UserData);
        }
        public PlayerData GetPlayerData() => LoaclData.PlayerData;
        public List<Character> GetPlayerCharacters() => LoaclData.Characters;
        public List<Item> GetPlayerItems() => LoaclData.Items;
        public Item GetPlayerItem(string name) => LoaclData.GetItem(name);
        public string GetToken() => UserData.Access_Token;


        async Task  Updata()
        {
            if (UserData.PlayerData != null)
            {
                LoaclData.UpdataPlayerData(UserData.PlayerData);
                if (UserData.PlayerData.Characters != null)
                    await LoaclData.UpdataCharacters(UserData.PlayerData.Characters);
                if (UserData.PlayerData.Items != null)
                    await LoaclData.UpdataItem(UserData.PlayerData.Items);
            }
         
        }

        public void Dispose()
        {
            UpdataPlayerDataCallBack = null;
            LoaclData = null;
            UserData = null;
            TempCache.Clear();
            TempCache = null;
        }
    }
    
    public class PlayerData
    {
        public string PlayerName;
        public int Money;
        public int PaidCoins;
        public int Lv;
        public int Exp;
        public int Stamina;
    }
    public class UserLoaclData
    {
        public PlayerData PlayerData = new();
        public List<Character> Characters = new();
        public List<Item> Items = new();
        public int Hash_Player, Hash_Character, Hash_Item;

        public void UpdataPlayerData(PlayerData_Net data_Net)
        {
            PlayerData.PlayerName = data_Net.Name;
            PlayerData.Money = data_Net.Money;
            PlayerData.PaidCoins = data_Net.PaidCoins;
            PlayerData.Lv = data_Net.Lv;
            PlayerData.Exp = data_Net.Exp;
            PlayerData.Stamina = data_Net.Stamina;
        }
        public async Task UpdataCharacters(List<CharacterData_Net> CharactersnNet)
        {

            if (Hash_Character == CharactersnNet.GetHashCode()) return;
            Hash_Character = CharactersnNet.GetHashCode();

            Characters.Clear();
            foreach (var data in CharactersnNet)
            {
                var c =  await Character.Create(data);
                Characters.Add(c);
            }
        }
       public async Task UpdataItem(List<Item_Net> itemsNet)
        {
            if (Hash_Item == itemsNet.GetHashCode()) return;
            Hash_Item = itemsNet.GetHashCode();

            Items.Clear();
            foreach (var data in itemsNet)
            {
                var c = await Item.Create_async(data);
                Items.Add(c);
            }
        }

        public Item GetItem(string Name)
        {
            foreach (var item in Items)
            {
                if (item.AssetName == Name) return item;
            }
            return null;
        }
        public Character GetCharacter(string Name)
        {
            foreach (var character in Characters)
            {
                if (character.characterNetData.AssetName == Name) return character;

            }
            return null;
        }
    }
}
