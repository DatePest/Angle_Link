using Client;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Graphs;

public class UI_TeamEditor : MonoBehaviour
{
    List<GameObject> HiedToShows = new();
    Team team = new();
    Sprite[] RoleTyptSprites;

    Ui_Select Ui_Select_PlayerCharacters;
    GameObject[] TeamSolt;
    Dictionary<string,GameObject> ShowUsedObj = new();
    Action BattleStartAction;
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
        RoleTyptSprites = new Sprite[4];
        var sprites = YooAsset_Tool.GetPackageDataSprites(GameConstant.PackName_GameCore, "CharacterButtonInfo");
        if (sprites == null) throw new Exception("sprites is null");

        for (int i = 0; i < RoleTyptSprites.Length; i++)
        {
            RoleTyptSprites[i] = sprites[i + 1];
        }
    }
    void InitTeamSolt()
    {
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
                   RemoveCharacterChick(team.Characters[CureentIndex]);
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
        Ui_Select_PlayerCharacters = u.GetComponentInChildren<Ui_Select>();
        var cs = ClientRoot.Get().ClientUserData.GetPlayerCharacters();
        var g = YooAsset_Tool.GetPackageData<GameObject>("GameCore", "CharacterSelectButton");

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
               () => { SelectCharacterChick(c); Ubutton.gameObject.SetActive(true);
                   ShowUsedObj.Add(c.characterData.UnitName,Ubutton.gameObject);}
                );
           
            Ubutton.onClick.AddListener(
                ()=> { RemoveCharacterChick(c); Ubutton.gameObject.SetActive(false); ShowUsedObj.Remove(c.characterData.UnitName);  }
                );
            Ui_Select_PlayerCharacters.AddSelectContent(target);
        }
    }
    #endregion

    #region Ui_Select....

    void SelectCharacterChick(Character character)
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
    void RemoveCharacterChick(Character character)
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
        UnitArt data = character.characterData.Art;
       foreach (Transform a in g.transform)
        {
            switch (a.name)
            {
                case "Icon":
                    a.GetComponent<RawImage>().texture = data.SDIcon.texture;
                break;
                case "Type":
                    var r = a.GetComponent<Image>();
                    switch (character.characterData.Attribute.RoleType)
                    {
                        case RoleType.Attacker:
                            r.sprite = RoleTyptSprites[2-1];
                            break;
                        case RoleType.Support:
                            r.sprite = RoleTyptSprites[4-1];
                            break;
                        case RoleType.Tank:
                            r.sprite = RoleTyptSprites[3-1];
                            break;
                        case RoleType.Healer:
                            r.sprite = RoleTyptSprites[1 - 1];
                            break;

                    }
                    break;
                case "Name":
                    a.GetComponent<TextMeshProUGUI>().text = character.characterData.UnitName;
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
        Ui_SystemMsg.Get().WatiUserSelect(" Confirm selection!", BattleStartAction);
        BattleStartAction = null;

    }
    #endregion
    public void StartTeamEditor(Action buttonStartAction)
    {
        BattleStartAction = buttonStartAction;
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
