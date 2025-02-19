using Client;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using YooAsset;
public class UI_BattleLevelManager : MonoBehaviour
{
    Ui_Layout UI;
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
            var g = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "GameLevelContenet");
            Asset_LevelContenet = g;
        }
        return Instantiate(Asset_LevelContenet);
    }

    void AddSeletUI()
    {
        var g = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "Ui_Select_GameLevel");
        var s = GameObject.Instantiate(g, transform.parent).GetComponent<Ui_Layout>();
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
        var g = YooAsset_Tool.GetPackageData_Sync<GameLevelTable>(GameConstant.PackName_GameCore, Name);
        foreach (var t in g.Datas)
        {
            var newg = GetGameLevelContenet();
            var T = newg.GetComponentInChildren<TextMeshProUGUI>();
            T.text = t.DisplayTitle;
            UI.AddSelectContent(newg, ()=>ChangeUI_ShowMainBattle(t.TagName));
        }
    }


    void ChangeUI_ShowMainBattle(string Tag)
    {
        UI.Clear();
        var data = YooAsset_Tool.GetGameDatas<GameLevelData>(GameConstant.PackName_GameCore, Tag);
        foreach (var t in data)
        {
            var g = GetGameLevelContenet();
            SetGameLevelInfo(g, t);
            UI.AddSelectContent(g,()=> SelectBattleClick(t));
        }
    }
    void SelectBattleClick(GameLevelData data)
    {
        var t =transform.parent.Find("Team_Editor");
        if (t == null) throw new Exception("Team_Editor is null");
        t.GetComponent<UI_TeamEditor>()?.StartTeamEditor(data);
        
    }
    void SetGameLevelInfo(GameObject obj, GameLevelData data)
    {
        //No.1-1  <br>Stamina 10<br>Exp $$
        var T = obj.GetComponentInChildren<TextMeshProUGUI>();
        T.text = $"No.{data.name}  <br>Stamina {data.Stamina}<br>Exp {data.Exp}";
    }
 
}
