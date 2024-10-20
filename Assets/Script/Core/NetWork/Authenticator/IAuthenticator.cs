using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Base class for all Authenticators, must be inherited
/// </summary>
public abstract class IAuthenticator 
{
    protected string user_id = null;
    protected string username = null;
    protected string password = null;
    protected string errormsg = null;
    protected bool logged_in = false;
    public bool inited { get; protected set; } = false;
    public static IAuthenticator Create(AuthenticatorType type)
    {
        if (type == AuthenticatorType.Api)
            return new Authenticator_API();
        else
            return new Authenticator_Test();
    }

    public virtual async Task Initialize()
    {
        inited = true;
        await Task.Yield(); //Do nothing
    }
    //Bypass login system by just assigning your own values, for testing
    public virtual void LoginTest(string username)
    {
        this.user_id = username;
        this.username = username;
        logged_in = true;
    }
    #region  Please subclass to implement
    public abstract Task<bool> Login(string username);
    public abstract Task<bool> Login(string username, string token); //Some authenticator dont define this function
    public abstract Task<bool> Register(string username, string email, string token); //Some authenticator dont define this function
    //public abstract Task<bool> RefreshLogin() //Same as Login if not defined
    public abstract Task<UserData> LoadUserData();
    public abstract Task<bool> SaveUserData(); 
    public abstract UserData GetUserData(); 
    public virtual void Logout()
    {
        logged_in = false;
        user_id = null;
        username = null;
    }
    #endregion

    public virtual bool IsConnected() => IsSignedIn() && !IsExpired();

    public virtual bool IsSignedIn() => logged_in; //IsSignedIn will still be true if the login expires

    public virtual bool IsExpired() => false;


    public virtual int GetPermission() => logged_in ? 1 : 0;



    public virtual string GetError() => errormsg;    //Should return the latest error

    public bool IsTest() =>  NetworkSetting.Get().auth_type == AuthenticatorType.LocalSave;
    public bool IsApi() => NetworkSetting.Get().auth_type == AuthenticatorType.Api;


  
    public static IAuthenticator Get() => Network.Get().Auth; //Access authenticator
    public enum AuthenticatorType
    {
        LocalSave = 0,   //Test Mode, Fake login for quick testing without the need to login each time
        Api = 10,        //Actual online login
    }

    


}