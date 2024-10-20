using System;
namespace Client
{
    public class ClientUserData 
    {
        public UserData UserData { get; private set; }
        public Action UpdataPlayerDataCallBack;
        public void SetUserData(UserData data)
        {
            UserData = data;
        }
        public void UpdatePlayerData(PlayerData data)
        {
            UserData.PlayerData = data;
            UpdataPlayerDataCallBack?.Invoke();
        }


    }

}
