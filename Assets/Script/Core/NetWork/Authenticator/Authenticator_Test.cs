using System.Threading.Tasks;
using GameApi;
using UnityEngine;

public class Authenticator_Test : IAuthenticator
{
    public override Task<Api_PlayerData> LoadUserPlayerData()
    {
        throw new System.NotImplementedException();
    }

    public override Task<bool> Login(string username)
    {
        throw new System.NotImplementedException();
    }

    public override Task<bool> Login(string username, string token)
    {
        throw new System.NotImplementedException();
    }

    public override Task<bool> Register(string username, string email, string token)
    {
        throw new System.NotImplementedException();
    }

    public override Task<bool> SaveUserData()
    {
        throw new System.NotImplementedException();
    }
}
