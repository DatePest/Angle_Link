using Client;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YooAsset;

public class Ui_DesktopConfig : MonoBehaviour
{
    GameObject Hide;
    GameObject NotSelect;
    Ui_Layout SelectObj;
    RawImage BackImgae;
    List<GameObject> HiedToShows = new();

    string CoverTag = "Cover";
    [SerializeField ]int High = 240, width = 135;
    private void Start()
    {
        BackImgae = transform.parent.Find("BackImgae").GetComponent<RawImage>();
        CreatTouchShow();
        SetSwichDisplay();
        SwitchCover();
    }

    void SetSwichDisplay()
    {
        var i = transform.Find("SwichDisplay").gameObject;
        if (i == null) { Debug.LogError("Target is null"); return; }
        EventTrigger eventTrigger = i.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick; onChick.callback.AddListener((data) =>
        {
            SwichDisplay(false);
        });
        eventTrigger.triggers.Add(onChick);
    }
    void SwitchCover()
    {
        var i = transform.Find("SwitchCover").gameObject;
        if( i == null ) { Debug.LogError("Target is null"); return; }
        EventTrigger eventTrigger = i.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick; onChick.callback.AddListener((data) =>
        {
            ShowCover();
        });
        eventTrigger.triggers.Add(onChick);
    }
    void ShowCover()
    {
        if (SelectObj == null)
            SelectObj = CreatSelectObj();
        else
            SelectObj.gameObject.SetActive(!SelectObj.gameObject.activeSelf);
        SetNotUse();
    }

    Client.Ui_Layout CreatSelectObj()
    {
        var g =YooAsset_Tool.GetPackageData_Sync<GameObject>("GameCore", "Ui_Select_default");
        var s = GameObject.Instantiate(g,transform.parent).GetComponent<Ui_Layout>();
        s.SetSize(High, width);
        var sp = GetCovers();
        foreach (var a in sp)
        {
            var Handle = Ui_Layout.CreatNewSelectHandle(a,  
                ()=> { ChangeTarget(a); s.gameObject.SetActive(false); });
            s.AddSelectContent(Handle);
        }
         return s ;
    }
    void SetNotUse()
    {
        if(SelectObj == null)
        {
            NotSelect.gameObject.SetActive(false);
            NotSelect.transform.SetParent(transform);
        }
        var tn = BackImgae.texture.name;

       foreach (Transform t in SelectObj.ObjContenet.transform)
        {
            var r =t.GetComponent<RawImage>();
            var e = t.GetComponent<EventTrigger>();
            e.enabled = true;
            if(r.texture.name == tn)
            {
                SetNotSelect(t);
                e.enabled = false;
            }
        }
       
    }

    void SetNotSelect(Transform t)
    {
        if (NotSelect == null) 
        {
            NotSelect = new GameObject($"NotSelect {GetType().ToString()}");
            var r =NotSelect.AddComponent<RawImage>();
            var s = YooAsset_Tool.GetPackageData_Sync<Sprite>("GameCore", "‘I‘ð•s‰Â");
            r.texture = s.texture;
          
        }
        NotSelect.transform.SetParent(t);
        var rt = NotSelect.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMax = Vector2.zero;
        rt.offsetMin = Vector2.zero;
    }
    void ChangeTarget(Sprite sprite)
    {
        BackImgae.texture = sprite.texture;
    }
    List<Sprite> GetCovers()
    {

        return YooAsset_Tool.GetGameDatas<Sprite>("GameCore", CoverTag);

    }
    private void CreatTouchShow()
    {
        var g = new GameObject("TouchShow");
        g.transform.SetParent(transform.parent);
        var I =g.AddComponent<Image>();
        I.color = new Color(0, 0, 0, 0);
        var rt = g.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        Hide = g;

        EventTrigger eventTrigger = g.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick; onChick.callback.AddListener((data) =>
        {
            SwichDisplay(true);
        }
        );
        eventTrigger.triggers.Add(onChick);

        Hide.SetActive(false);
    }

    void SwichDisplay(bool b)
    {
        if (b == false)
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
        Hide.SetActive(!b);
    }

}
