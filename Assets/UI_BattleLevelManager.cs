using Client;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;
using YooAsset;
public class UI_BattleLevelManager : MonoBehaviour
{
    Ui_Select UI;
    GameObject Asset_LevelContenet;

    private void Awake()
    {
        var m = transform.Find("Main");
        Add_PointerClick(m, ()=>LoadMainBattleLavel("MainGameLevelTable"));
        AddSeletUI();


    }
    GameObject GetGameLevelContenet()
    {
        if(Asset_LevelContenet == null)
        {
            var g = YooAsset_Tool.GetPackageData<GameObject>("GameCore", "GameLevelContenet");
            Asset_LevelContenet = g;
        }
        return Instantiate(Asset_LevelContenet);
    }

    void AddSeletUI()
    {
        var g = YooAsset_Tool.GetPackageData<GameObject>("GameCore", "Ui_Select_GameLevel");
        var s = GameObject.Instantiate(g, transform.parent).GetComponent<Ui_Select>();
        UI = s;
        UI.gameObject.SetActive(false);
    }

    void Add_PointerClick(Transform t,Action a)
    {
        EventTrigger eventTrigger = t.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick;
        onChick.callback.AddListener((data) =>
        {
            a();
        }
        );
        eventTrigger.triggers.Add(onChick);
    }
    void LoadMainBattleLavel(string Name)
    {
        UI.Clear();
        UI.gameObject.SetActive(true);
        var g = YooAsset_Tool.GetPackageData<GameLevelTable>("GameCore", Name);
        foreach (var t in g.Datas)
        {
            var Handle = Ui_Select.CreatNewSelectHandle(t.Art,
               () => { ChangeUI_ShowMainBattle(t.Name); });
            UI.AddSelectContent(Handle);
        }
    }


    void ChangeUI_ShowMainBattle(string Tag)
    {
        UI.Clear();
        var data = YooAsset_Tool.GetGameDatas<GameLevelData>("GameCore", Tag);
        foreach (var t in data)
        {
            var g = GetGameLevelContenet();
            SetGameLevelInfo(g, t);
            UI.AddSelectContent(g,()=> BattleCheck(t));
        }
    }
    void BattleCheck(GameLevelData data)
    {
        Ui_SystemMsg.Get().WatiUserSelect(" Confirm selection!", ()=>SendToServerRequestPlayerGame(data));
    }
    void SetGameLevelInfo(GameObject obj, GameLevelData data)
    {
        //No.1-1  <br>Stamina 10<br>Exp $$
        var Test = obj.GetComponentInChildren<TextMeshProUGUI>();
        Test.text = $"No.{data.name}  <br>Stamina {data.Stamina}<br>Exp {data.Exp}";
    }
    void SendToServerRequestPlayerGame(GameLevelData levelData)
    {
        Debug.Log("Test SendToServerRequestPlayerGame! ");
    }

 
}
