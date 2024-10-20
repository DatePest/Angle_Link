using System.Net;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.Networking;
public class Api_Services
{
    //private int sending = 0;
    //private string last_error = "";
    //private float refresh_timer = 0f;
    //private float online_timer = 0f;
    //private long expiration_timestamp = 0;


    public UnityAction<RegisterResponse> onRegister; //Triggered after register, even if failed
    public UnityAction<LoginResponse> onLogin; //Triggered after login, even if failed
    public UnityAction<LoginResponse> onRefresh; //Triggered after login refresh, even if failed 
    public UnityAction onLogout; //Triggered after logout

    private ClinetData Client = new();
    private bool logged_in = false;
    private bool expired = false;
   

    static Api_Services instance;
    public static Api_Services Get()
    {
        if (instance == null)
        {
            instance = new Api_Services();
        }
        return instance;
    }
    public static string ServerURL
    {
        get
        {
            NetworkSetting data = NetworkSetting.Get();
            string protocol = data.api_https ? "https://" : "http://";
            return protocol + data.api_url;
        }
    }

    public async Task<LoginResponse> Login(string user, string password)
    {
        Logout(); //Disconnect

        LoginRequest data = new LoginRequest();
        data.password = password;

        if (user.Contains("@"))
            data.email = user;
        else
            data.username = user;

        string url = ServerURL + "/auth";
        string json = ApiTool.ToJson(data);

        WebResponse res = await SendPostRequest(url, json);
        LoginResponse login_res = GetLoginRes(res);
        //AfterLogin(login_res);

        onLogin?.Invoke(login_res);
        return login_res;
    }
    public void Logout()
    {
        Client.Clear();
        logged_in = false;
        onLogout?.Invoke();
        // SaveTokens();
    }
    #region Register
    public async Task<RegisterResponse> Register(string email, string user, string password)
    {
        RegisterRequest data = new RegisterRequest();
        data.email = email;
        data.username = user;
        data.password = password;
        data.avatar = "";
        return await Register(data);
    }

    public async Task<RegisterResponse> Register(RegisterRequest data)
    {
        Logout(); //Disconnect

        string url = ServerURL + "/users/register";
        string json = ApiTool.ToJson(data);

        WebResponse res = await SendPostRequest(url, json);
        RegisterResponse regist_res = ApiTool.JsonToObject<RegisterResponse>(res.data);
        regist_res.success = res.success;
        regist_res.error = res.error;
        onRegister?.Invoke(regist_res);
        return regist_res;
    }
    #endregion
    public async Task<UserData> LoadUserData(UserGetPlayerDataRequest data)
    {
        //Todo LoadUserData
        UserData udata = null;
        string url = ServerURL + "/users/" + data;
        var json = ApiTool.ToJson(data);
        WebResponse res = await SendPostRequest(url, json, data.accesLogin_token);
        if (res.success)
        {
            if(res.error != null) 
            {
                return default;
            }
            udata = ApiTool.JsonToObject<UserData>(res.data);
        }

        return udata;
    }
    public async Task<UserData> LoadUserData(string Token)
    {
        if (!IsConnected())
            return null;

        string url = ServerURL + "/users/" + Token;
        WebResponse res = await SendGetRequest(url);

        UserData udata = null;
        if (res.success)
        {
            udata = ApiTool.JsonToObject<UserData>(res.data);
        }

        return udata;
    }
    public bool IsConnected() => logged_in && !expired;

    class ClinetData
    {
        public UserData UserData { get; private set; }

        public string user_id = "";
        public string username = "";
        public string access_token = "";
        public string refresh_token = "";
        public string api_version = "";
        public string last_error = "";
        public void Clear()
        {
            user_id = "";
            username = "";
            access_token = "";
            refresh_token = "";
            api_version = "";
            last_error = "";
        }
    }

    public async Task<WebResponse> SendGetRequest(string url, string token = "")
    {
        return await SendRequest(url, WebRequest.METHOD_GET, token);
    }

    public async Task<WebResponse> SendPostRequest(string url, string json_data, string token = "")
    {
        return await SendRequest(url, WebRequest.METHOD_POST,  token, json_data);
    }

    public async Task<WebResponse> SendRequest(string url, string method,string token, string json_data = "" )
    {
        UnityWebRequest request = WebRequest.Create(url, method, json_data, token);
        return await SendRequest(request);
    }

    private async Task<WebResponse> SendRequest(UnityWebRequest request)
    {
        int wait = 0;
        int wait_max = request.timeout * 1000;
        request.timeout += 1; //Add offset to make sure it aborts first
        //sending++;

        var async_oper = request.SendWebRequest();
        while (!async_oper.isDone)
        {
            await Task.Delay(200);
            wait += 200;
            if (wait >= wait_max)
                request.Abort(); //Abort to avoid unity errors on timeout
        }

        WebResponse response = WebRequest.GetResponse(request);
        response.error = GetError(response);
        //last_error = response.error;
        request.Dispose();
        //sending--;

        return response;
    }
    private string GetError(WebResponse res)
    {
        if (res.success)
            return "";

        ErrorResponse err = ApiTool.JsonToObject<ErrorResponse>(res.data);
        if (err != null)
            return err.error;
        else
            return res.error;
    }
    private LoginResponse GetLoginRes(WebResponse res)
    {
        LoginResponse login_res = ApiTool.JsonToObject<LoginResponse>(res.data);
        login_res.success = res.success;
        login_res.error = res.error;

        //Uncomment to force having same client version as api
        /*if (!IsVersionValid())
        {
            login_res.error = "Invalid Version";
            login_res.success = false;
        }*/

        return login_res;
    }
}
