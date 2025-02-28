using GameApi;
using UnityEngine;
using YooAsset;
using static Assets.Script.Core.Server.ServerGameLogic;

public class Test1 : MonoBehaviour
{
    public EPlayMode ePlayMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await YooAsset_Tool.InitPackageAsync(ePlayMode, GameConstant.PackName_GameCore);
        var data = new Api_PlayerData(); 
        await PlayerDataLogic.AddCharacter(data, new[] { "A001", "A002" });
        Debug.Log(data.Characters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
