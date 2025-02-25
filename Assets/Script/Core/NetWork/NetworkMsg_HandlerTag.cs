/// <summary>
/// Here defines the header name of the custom message interaction
/// </summary>

public class NetworkMsg_HandlerTag
{
    public const ushort Test = 9999;

    public const string Account = "Account";
    public const string Battle = "Battle";
    public const string GameEvent = "GameEvent";

    #region ----------Account--------- 1000 ~ 1399

    public const ushort RequestLogin = 1001;
    public const ushort RequestRegister = 1002;
    public const ushort RequestGetPlayerData = 1010;
    //
    public const ushort ReturnGetPlayerData = 1200;
    public const ushort ReturnMsg = 1204;
    public const ushort ReturnErrorMsg_BackLogin = 1208;
    public const ushort Returnlogin = 1212;
    public const ushort ReturnReg = 1216;
    #endregion

    #region ----------Battle--------- 1400 ~ 1599
    public const ushort RequestBattleEnter = 1400;
    public const ushort RequestBattleStart = 1404;
    public const ushort RequestBattlePlayerSelect = 1408;
    public const ushort RequestBattleEndSettlement = 1412;
    public const ushort RequestReceiveBattleData = 1416; // From Client Data
    //
    public const ushort ReturnBattleEnterRequest = 1500;
    public const ushort ReturnBattleStartRequest = 1504;
    public const ushort ReturnBattlePlayerSelectRequest = 1508;
    public const ushort ReturnBattleEndSettlement = 1512;
    public const ushort Return_RequestToBattleData = 1516;
    #endregion


    //-------------------

    #region ----------GameEvent--------- 1600 ~ 2000
    public const ushort RequestCharacterDevelop_Lv = 1600;


    public const ushort ReturnCharacterDevelop_Lv = 1600;
    #endregion
}