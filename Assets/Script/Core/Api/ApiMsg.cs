using System;
/// <summary>
/// List of data structure used by the Server ,WebApi requests and responses
/// </summary>
//--------- Requests -----------

[Serializable]
    public struct LoginRequest
    {
        public string email;
        public string username;
        public string password;
    }

    [Serializable]
    public struct AutoLoginRequest
    {
        public string refresh_token;
    }

    [Serializable]
    public struct RegisterRequest
    {
        public string email;
        public string username;
        public string password;
        public string avatar;
    }

    [Serializable]
    public struct EditEmailRequest
    {
        public string email;
    }

    [Serializable]
    public struct EditPasswordRequest
    {
        public string password_previous;
        public string password_new;
    }

    [Serializable]
    public struct FriendAddRequest
    {
        public string username;
    }
    [Serializable]
    public struct UserGetPlayerDataRequest
    {
        public string username;
        public string accesLogin_token;
        public string data_auth_token;
}

//--------- Response -----------

[Serializable]
    public struct VersionResponse
    {
        public string version;
    }

    [Serializable]
    public struct RegisterResponse
    {
        public string id;
        public string username;
        public string version;
        public bool success;
        public string error;
    }

    [Serializable]
    public struct LoginResponse
    {
        public string id;
        public string username;
        //public string refresh_token;
        public string access_token;
        public string error;
        public bool success;
    }

    [Serializable]
    public struct UserIdResponse
    {
        public string id;
        public string username;
        public string error;
    }
