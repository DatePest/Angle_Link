using Assets.Script.Core.Server;
using Assets.Script.Core.Server.Services;
using Client;
using GameApi;
using System.Threading.Tasks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Android;
using static Assets.Script.Core.Server.ServerGameLogic;
/// <summary>
/// This authenticator require external UserLogin API asset
/// It works with an actual web API and database containing all user info
/// </summary>
public class Authenticator_API : IAuthenticator
{
    public string User_Token;
    public Api_Services Api => Api_Services.Get();

    public UserData GetUserData()
    {
        UserData U = new();
        U.Access_Token = User_Token;
        U.Username = username;
        U.Email = email;
        return U;
    }
    //public UserGetPlayerDataRequest GetPlayerDataRequest()
    //{
    //    UserGetPlayerDataRequest userGet = new UserGetPlayerDataRequest();
    //    userGet.username = username;
    //    userGet.accesLogin_token = User_Token;
    //    return userGet;
    //}
    public override Task<bool> Login(string username) => throw new System.NotImplementedException("is NotImplemented ");
    public override async Task<bool> Login(string username, string password)
    {
    
        LoginResponse res = await Api.Login(username, password);
        if (res.success)
        {
            this.logged_in = true;
            this.username = res.username;
            this.email = res.email;
            User_Token = res.access_token;
        }
        else
        {
            errormsg = res.error;
          
        }
        return res.success;
    }
    public  async Task<bool> Register(RegisterRequest data)
    {
        RegisterResponse res = await Api.Register(data);

        if (res.success)
        {
            await Login(data.username, data.password);
        }
        else
        {
            errormsg = res.error;
        }

        return res.success;
    }
    public override async Task<bool> Register(string username, string email, string password)
    {
        RegisterResponse res = await Api.Register(username, email, password);

        if (res.success)
        {
            await Login(username, password);
        }
        else
        {
            errormsg = res.error;
        }   

        return res.success;
    }

    public override async Task<bool> SaveUserData()
    {
        //Do nothing, saved on each api request, no need to save to disk
        await Task.Yield();
        return false;
    }
    public override async Task<Api_PlayerData> LoadUserPlayerData()
    {
        if (User_Token == string.Empty || User_Token == "") return null;
        var res = await Api_Services.Get().GetUserPlayerData(User_Token);
        if (res.success)
            return res.Api_PlayerData;
        else
            return null;
    }
  
    public async Task<bool> TokenSavePlayerData(Api_PlayerData data)
    {
        return await PlayerDataLogic.SaveData(data, User_Token);
    }

    

}