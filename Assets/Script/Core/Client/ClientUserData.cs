using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
namespace Client
{
    public class ClientUserData 
    {
        public UserData UserData { get; private set; }
        public Action UpdataPlayerDataCallBack;
        UserLoaclData LoaclData= new();

        public void SetUserData(UserData data)
        {
            UserData = data;
            UpdatePlayerData(data.PlayerData);
        }
        public void UpdatePlayerData(PlayerData_Net data)
        {
            UserData.PlayerData = data;
            Updata();
            UpdataPlayerDataCallBack?.Invoke();
        }
        public PlayerData GetPlayerData() => LoaclData.PlayerData;
        public List<Character> GetPlayerCharacters() => LoaclData.Characters;
        public List<Item> GetPlayerItems() => LoaclData.Items;

        void Updata()
        {
            LoaclData.UpdataPlayerData(UserData.PlayerData);
            LoaclData.UpdataCharacters(UserData.PlayerData.Characters);
            LoaclData.UpdataItem(UserData.PlayerData.Items);
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
            PlayerData.PlayerName = data_Net.PlayerName;
            PlayerData.Money = data_Net.Money;
            PlayerData.PaidCoins = data_Net.PaidCoins;
            PlayerData.Lv = data_Net.Lv;
            PlayerData.Exp = data_Net.Exp;
            PlayerData.Stamina = data_Net.Stamina;
        }
        public void UpdataCharacters(CharacterData_Net[] CharactersnNet)
        {

            if (Hash_Character == CharactersnNet.GetHashCode()) return;
            Hash_Character = CharactersnNet.GetHashCode();

            Characters.Clear();
            foreach (var data in CharactersnNet)
            {
                var c = Character.Create(data);
                Characters.Add(c);
            }
        }
       public void UpdataItem(Item_Net[] itemsNet)
        {
            if (Hash_Item == itemsNet.GetHashCode()) return;
            Hash_Item = itemsNet.GetHashCode();

            Items.Clear();
            foreach (var data in itemsNet)
            {
                var c = Item.Create(data);
                Items.Add(c);
            }
        }
    }
}
