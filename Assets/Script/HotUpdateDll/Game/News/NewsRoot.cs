using Client;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class NewsRoot : MonoBehaviour
{
    bool checkupdata;
    ClientUserData clientUser => ClientRoot.Get().ClientUserData;
    async void Awake()
    {
       GetPlayerData();
        while (!checkupdata) { await UniTask.Yield(PlayerLoopTiming.Update); }
    }
    void GetPlayerData()
    {
        checkupdata = false;
        clientUser.ReqUpdataPlayerData();
        var msg = new GameApi.UserGetPlayerDataRequest();
        clientUser.UpdataPlayerDataCallBack += callback;
    }

    private void callback(UserData data)
    {
        checkupdata = true;
        clientUser.UpdataPlayerDataCallBack -= callback;
    }
    private void OnDestroy()
    {

    }
}
