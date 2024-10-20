using Client;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using static IRequestSend;

public class NewsRoot : MonoBehaviour
{
    bool checkupdata;
    async void Awake()
    {
        GetPlayerData();
        while (!checkupdata) { await UniTask.Yield(PlayerLoopTiming.Update); }
    }
    void GetPlayerData()
    {
        checkupdata = false;
        var sd = new NetSendData(ClientEventTag.SendLogin, NetworkMsg_HandlerName.Account, default);
        var msg = new UserGetPlayerDataRequest();
        var user = ClientRoot.Get().ClientUser;
        user.UpdataPlayerDataCallBack += callback;
        msg.username = user.UserData.Username;
        msg.accesLogin_token = user.UserData.Access_Token;
        sd.data = msg;
        EventSystemTool.EventSystem.Publish(sd);
    }


    public void callback()
    {
        checkupdata = true;
        ClientRoot.Get().UpdataPlayerDataCallBack -= callback;
    }
}
