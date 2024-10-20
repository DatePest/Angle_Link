using System.Threading.Tasks;
using UnityEngine;

public class Authenticator_Test : IAuthenticator
{
    public override UserData GetUserData()
    {
        throw new System.NotImplementedException();
    }

    public override Task<UserData> LoadUserData()
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
