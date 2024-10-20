using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;
/// <summary>
/// This authenticator require external UserLogin API asset
/// It works with an actual web API and database containing all user info
/// </summary>
public class Authenticator_API : IAuthenticator
{

    string User_Token;
    UserData userData;
    public Api_Services Api => Api_Services.Get();
    public override Task<bool> Login(string username) => throw new System.NotImplementedException("is NotImplemented ");
    public override async Task<bool> Login(string username, string password)
    {
        LoginResponse res = await Api.Login(username, password);
        if (res.success)
        {
            this.logged_in = true;
            this.user_id = res.id;
            this.username = res.username;
            User_Token = res.access_token;
        }
        else
        {
            res.error = errormsg;
        }
        return res.success;
    }

    public override async Task<bool> Register(string username, string email, string password)
    {
        RegisterResponse res = await Api.Register(username, email, password);

        if (res.success)
            await Login(username, password);

        return res.success;
    }

    public override async Task<bool> SaveUserData()
    {
        //Do nothing, saved on each api request, no need to save to disk
        await Task.Yield();
        return false;
    }
    public override async Task<UserData> LoadUserData()
    {
        UserData res = await Api.LoadUserData(User_Token);
        return  res;
    }
    public override UserData GetUserData() => userData;
    public static async Task<UserData> TokenGetUserData(UserGetPlayerDataRequest data)
    {
        UserData res = await Api_Services.Get().LoadUserData(data.accesLogin_token);
        return res;
    }
}