using System;
using System.Collections.Generic;
#nullable disable
/// <summary>
/// List of data structure used by the Server ,WebApi requests and responses
/// </summary>
/// 
namespace GameApi
{
    public class IRequest
    {
        public string accesLogin_token { get; set; }
    }
    public class IWebResponseState
    {
        public bool success { get; set; }
        public string error { get; set; }
    }
    #region //--------- Requests -----------

    public class RegisterRequest
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class LoginRequest
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AutoLoginRequest
    {
        public string refresh_token { get; set; }
    }
   
    public class EditEmailRequest
    {
        public string email { get; set; }
    }
    public class EditPasswordRequest
    {
        public string email { get; set; }
        public string username { get; set; }
        //public string password_previous;
        public string password_new { get; set; }
    }

    public class FriendAddRequest
    {
        public string username { get; set; }
    }
    public class UserGetPlayerDataRequest
    {
        public string username { get; set; }
        public string accesLogin_token { get; set; }
    }

    public class SavePlayerDataRequest
    {
        public string accesLogin_token { get; set; }
        public Api_PlayerData api_PlayerData { get; set; }
    }

    public class DataObject
    {
        public string AssetName { get; set; }
        public int Amount { get; set; }

        public object Arg1 { get; set; }
        public object Arg2 { get; set; }
        public object Arg3 { get; set; }
        public object Arg4 { get; set; }
        public object Arg5 { get; set; }
        public DataObject(string assName, int amount )
        {
            AssetName = assName;
            Amount = amount;
        }
    }
    public class ItemListRequst : IRequest
    {
        public List<DataObject> ItemList1 { get; set; }
        public List<DataObject> ItemList2 { get; set; }

        public object Arg1 { get; set; }
        public object Arg2 { get; set; }
        public object Arg3 { get; set; }
    }



    #endregion


    #region //--------- Response -----------

    public class VersionResponse
    {
        public string version { get; set; }
    }

    public class EditPasswordResponse : IWebResponseState
    {
    }

    public class RegisterResponse : IWebResponseState
    {
        public string id { get; set; }
        public string username { get; set; }
        public string version { get; set; }
    }


    public class LoginResponse : IWebResponseState
    {
        public string email { get; set; }
        public string username { get; set; }
        public string access_token { get; set; }
    }

    public class Api_PlayerDataRespons : IWebResponseState
    {
        public Api_PlayerData Api_PlayerData { get; set; }
    }
    public class UserIdResponse : IWebResponseState
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    public class CharacterDataResponse : IWebResponseState
    {
        public string AssetName { get; set; }
        public string UnitName { get; set; }
        public int Lv { get; set; }
        public int CurrentExp { get; set; }
        public int AbilityLv_1 { get; set; }
        public int AbilityLv_2 { get; set; }
        public int AbilityLv_SP { get; set; }
        public int AbilityPassiveLv_1 { get; set; }
        public int AbilityPassiveLv_2 { get; set; }
        public int Favorability { get; set; }
        public int CharacterRank { get; set; }
    }
    public class ItemDataResponse : IWebResponseState
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }


    #endregion

    #region DataClass
    public class Api_PlayerData
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int PaidCoins { get; set; }
        public int Lv { get; set; }
        public int Exp { get; set; }
        public string UID { get; set; }
        public int Stamina { get; set; }

        public string Characters { get; set; }
        public string Items { get; set; }
        public string LastUpDataTime { get; set; }
    }

    #endregion

}
