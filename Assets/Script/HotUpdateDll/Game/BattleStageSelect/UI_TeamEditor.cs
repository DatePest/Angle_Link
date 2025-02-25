using Client;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Tools;

public class UI_TeamEditor : MonoBehaviour
{
    List<GameObject> HiedToShows = new();
    Team team;
    Sprite[] RoleTyptSprites;

    Ui_Layout Ui_Select_PlayerCharacters;
    GameObject[] TeamSolt;
    Dictionary<string,GameObject> ShowUsedObj = new();
    GameLevelData GameLevelDataTemp;
    private void Awake()
    {
        InittInfoSprite();
        InitTeamSolt();
        InitButtons();
        LoadPlayerCharacters();
    }
    #region Init
    void InittInfoSprite()
    {
        RoleTyptSprites = YooAsset_Tool.GetPackageDataSprites(GameConstant.PackName_GameCore, "CharacterButtonInfo");
        if (RoleTyptSprites == null) throw new Exception("sprites is null");
    }
    Sprite GetSP(string Name)
    {
        foreach (Sprite sprite in RoleTyptSprites)
        {
            if(sprite.name == Name)
                return sprite;
        }
        return default;
    }
    void InitTeamSolt()
    {
        team = Team.Create(6);
        TeamSolt = new GameObject[6];
        var Slots =transform.Find("TeamSlots");
        int i = 0;
        foreach (Transform t in Slots)
        {
            int CureentIndex = i;
            TeamSolt[CureentIndex] = t.gameObject;
            TeamSolt[CureentIndex].GetComponent<Button>().onClick.AddListener(
               () =>
               {
                   if (team.Characters[CureentIndex] == null) return;

                   var Name = t.Find("Name").GetComponent<TextMeshProUGUI>().text;
                   ShowUsedObj.TryGetValue(Name, out var obj);
                   obj.gameObject.SetActive(false);
                   ShowUsedObj.Remove(Name);
                   RemoveCharacterClick(team.Characters[CureentIndex]);
               });

            HideCharacterButtonInfos(TeamSolt[CureentIndex]);
            i++;
            if(i >= 6 ) { break; }
        }
    }
    void InitButtons()
    {
        var target = transform.Find("Buttons");
        if (target == null) throw new System.Exception("Buttons is null");


        foreach (Transform t in target)
        {
            var b = t.GetComponent<Button>();
            switch (t.name)
            {
                case "Reset":
                    b.onClick.AddListener(ButtonReset);
                    break;
                case "Auto":
                    b.onClick.AddListener(ButtonAuto);
                    break;
                case "Record":
                     b.onClick.AddListener(ButtonRecord);
                    break;
                case "Cancel":
                    b.onClick.AddListener(ButtonCanecl);
                    break;
                case "Start":
                    b.onClick.AddListener(ButtonStart);
                    break;
            }
        }
    }
    void LoadPlayerCharacters()
    {
        var u =transform.Find("Ui_PlayerCharacters");
        if (u == null) throw new System.Exception("Ui_PlayerCharacters is null");
        Ui_Select_PlayerCharacters = u.GetComponentInChildren<Ui_Layout>();
        var cs = ClientRoot.Get().ClientUserData.GetPlayerCharacters();
        var g = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "Team_EditorCharacterSelectButton");

        foreach(var c in cs)
        {
            var target = GameObject.Instantiate(g, transform.parent);
            SetCharacterButton(target, c);
            // SelectCharacterButton
            var Sbutton = target.GetComponent<Button>();
            // UsedButton
            var Ubutton = target.transform.Find("Useed").GetComponent<Button>();
            Ubutton.gameObject.SetActive(false);

            Sbutton.onClick.AddListener(
               () => { SelectCharacterClick(c); Ubutton.gameObject.SetActive(true);
                   ShowUsedObj.Add(c.characterData.GetData().UnitName,Ubutton.gameObject);}
                );
           
            Ubutton.onClick.AddListener(
                ()=> { RemoveCharacterClick(c); Ubutton.gameObject.SetActive(false); ShowUsedObj.Remove(c.characterData.GetData().UnitName);  }
                );
            Ui_Select_PlayerCharacters.AddSelectContent(target);
        }
    }
    #endregion

    #region Ui_Select....

    void SelectCharacterClick(Character character)
    {
        if (character == null) return;
        for (int i = 0; i < team.Characters.Length; i++)
        {
            if (team.Characters[i] == null)
            {
                team.Characters[i] = character;
                SetCharacterButton(TeamSolt[i], character);
                return;
            }
        }
    }
    void RemoveCharacterClick(Character character)
    {
        if (character == null) return;
        bool find = false;
        for (int i = 0; i < team.Characters.Length; i++)
        {
            
            if (team.Characters[i] == character)
            {
                team.Characters[i] = null;
                HideCharacterButtonInfos(TeamSolt[i]);
                find = true;
                continue;
            }
            if (find)
            {
                HideCharacterButtonInfos(TeamSolt[i]);
                if (team.Characters[i] == null)
                {

                   continue;
                }

                SetCharacterButton(TeamSolt[i - 1], team.Characters[i]);
                team.Characters[i - 1] = team.Characters[i];
                team.Characters[i] = null;

            }
        }
    }
    void HideCharacterButtonInfos(GameObject g)
    {
        foreach (Transform a in g.transform)
        {
            a.gameObject.SetActive(false);
        }
    }
    void SetCharacterButton(GameObject g, Character character)
    {
        ArtData data = character.characterData.GetData().Art;
       foreach (Transform a in g.transform)
        {
            switch (a.name)
            {
                case "Icon":
                    a.GetComponent<RawImage>().texture = data.SDIcon.texture;
                break;
                case "Type":
                    var r = a.GetComponent<Image>();
                    switch (character.characterData.GetData().Attribute.RoleType)
                    {
                        case RoleType.Attacker:
                            r.sprite = GetSP("CharacterButtonInfo_Attacker");
                            break;
                        case RoleType.Support:
                            r.sprite = GetSP("CharacterButtonInfo_Support");
                            break;
                        case RoleType.Tank:
                            r.sprite = GetSP("CharacterButtonInfo_Tank");
                            break;
                        case RoleType.Healer:
                            r.sprite = GetSP("CharacterButtonInfo_Healer");
                            break;

                    }
                    break;
                case "Name":
                    a.GetComponent<TextMeshProUGUI>().text = character.characterData.GetData().UnitName;
                    break;
            }
            a.gameObject.SetActive(true);
        }
    }
    #endregion
  
    #region Buttons
    void ButtonRecord()
    {
        throw new NotImplementedException();
    }

    void ButtonAuto()
    {
        throw new NotImplementedException();
    }
    void ButtonReset()
    {
        team.Clear();
        foreach (var a in ShowUsedObj)
        {
            a.Value.SetActive(false);
        }
        ShowUsedObj.Clear();
        foreach (var a in TeamSolt) HideCharacterButtonInfos(a);
    }
    void ButtonCanecl()
    {
        SwichDisplay(false);
    }
    void ButtonStart()
    {
        Ui_SystemMsg.Get().WaitUserSelect($"{GameLevelDataTemp.name}  Confirm selection!", SendToSer);
    }
    #endregion
    void SendToSer()
    {
        var msg = new GameApi.BattleEnterRequest();
        var Client = ClientRoot.Get();
        msg.accesLogin_token = ClientRoot.GetToken();
        msg.GameLevelDataName = GameLevelDataTemp.name;
        msg.team = team;

        EventSystemToolExpand.Publish(ClientEventTag.SendBattleEnterRequest, NetworkMsg_HandlerTag.Battle, default, msg);
        #region LocalSet Test 
        //var Bs = new BattleSettings(team, GameLevelDataTemp);
        //Client.ClientUserData.AddCache("GameLevelDataTemp", Bs);
        #endregion
    }

    public void StartTeamEditor(GameLevelData  data)
    {
        GameLevelDataTemp = data;
        SwichDisplay(true);
    }
    void SwichDisplay(bool b)
    {
        var BackImgae =transform.parent.Find("BackImgae").GetComponent<RawImage>().gameObject;

        if (b)
        {
            foreach (Transform i in transform.parent)
            {
                if (i.gameObject.activeSelf)
                {
                    i.gameObject.SetActive(false);
                    HiedToShows.Add(i.gameObject);
                }
            }
            var g = GameObject.FindFirstObjectByType<HomeSwich>().gameObject;
            if (g != null)
            {
                HiedToShows.Add(g);
                g.gameObject.SetActive(false);
            }
            BackImgae.gameObject.SetActive(true);
        }
        else
        {
            foreach (GameObject i in HiedToShows)
            {
                i.SetActive(true);
            }
            HiedToShows.Clear();
        }
        gameObject.SetActive(b);
    }

    //GameObject GetTeamEdiotObj()
    //{
    //    if(TeamEdiotObj == null)
    //    {
    //        var g = YooAsset_Tool.GetPackageData<GameObject>("GameCore", "Team_Editor");
    //        var s = GameObject.Instantiate(g, transform.parent).GetComponent<UI_TeamEditor>();
    //    }

    //    return TeamEdiotObj;
    //}


}
