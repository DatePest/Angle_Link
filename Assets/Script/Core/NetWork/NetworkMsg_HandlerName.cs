using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Here defines the header name of the custom message interaction
/// </summary>

public class NetworkMsg_HandlerName
{
    public const ushort Test = 9999;

    //----------Send    Mag Tag---------
    public const string Account = "Account";
    public const string Battle = "Battle";
    public const ushort ReturnMsg = 0001;
    public const ushort Returnlogin = 1101;
    public const ushort ReturnGetPlayerData = 1101;
    
    //----------Receive Msg Tag---------
    #region ----------Account---------

    public const ushort Login = 1001;
    public const ushort Register = 1002;
    public const ushort GetPlayerData = 1010;
    #endregion
    //-------------------
}