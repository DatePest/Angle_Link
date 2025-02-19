using Client;
using Codice.Client.BaseCommands;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Ui_PlayerInfo : MonoBehaviour
{

    GameObject Lv;
    GameObject Expbar;
    GameObject Stamian;
    GameObject PaidCoins;
    GameObject Money;


    ClientUserData clientUser => ClientRoot.Get().ClientUserData;
    void Awake()
    {
        Init();
        clientUser.UpdataPlayerDataCallBack += UpdatePlayerInfo;


    }
    private void Start()
    {
        UpdatePlayerInfo(clientUser.UserData);

    }
    private void OnDestroy()
    {
        clientUser.UpdataPlayerDataCallBack -= UpdatePlayerInfo;
    }
    void Init()
    {
        Lv = transform.Find("Lv").gameObject;
        Expbar = transform.Find("ExpBar").gameObject;
        Stamian = transform.Find("Stamina").gameObject;
        SetdClickStamian(Stamian);
        PaidCoins = transform.Find("PaidCoins").gameObject;
        SetdClickPaidCoins(PaidCoins);
        Money = transform.Find("Money").gameObject;
    }
    private void UpdatePlayerInfo(UserData U)
    {
        if (U.PlayerData == null) return;
      
        var data = U.PlayerData;
        SetExpBar(data.Exp, data.GetNextLvExp());
        SetStamian(data.Stamina, data.GetStaminaMax());
        SetLv(data.Lv);
        SetMoney(data.Money);
        SetPaidCoins(data.PaidCoins);
    }
    private void SetExpBar(int Current ,int max)
    {
        var i = Expbar.GetComponentInChildren<Image>();
        i.fillAmount = (float)Current / max;
    }
    private void SetStamian(int Current, int max)
    {
        // <size=70%>Stamina </size> 10/300
        var t =Stamian.GetComponent<TextMeshProUGUI>();
        t.text = ($"<size=70%>Stamina </size> {Current}/{max}");
    }
    private void SetLv(int i)
    {
        var t = Lv.GetComponent<TextMeshProUGUI>();
        t.text = ($"<size=70%>Lv </size> {i}");
    }
    private void SetMoney(int i)
    {
        var t = Money.GetComponentInChildren<TextMeshProUGUI>();
        t.text = i.ToString();
    }
    private void SetPaidCoins(int i)
    {
        var t = PaidCoins.GetComponentInChildren<TextMeshProUGUI>();
        t.text = i.ToString();
    }
    void SetdClickStamian(GameObject g)
    {
        GameObject Target= g.transform.Find("Add").gameObject;
        EventTrigger eventTrigger = Target.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick;
        onChick.callback.AddListener((data) =>
        {
            ClickAddStamian();
        }
        );
        eventTrigger.triggers.Add(onChick);
    }
    void ClickAddStamian()
    {
        //Todo AddStamian;
        Debug.Log("ClickAddStamian");
    }
    void SetdClickPaidCoins(GameObject g)
    {
        GameObject Target = g.transform.Find("Add").gameObject;
        EventTrigger eventTrigger = Target.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick;
        onChick.callback.AddListener((data) =>
        {
            ClickAddPaidCoins();
        }
        );
        eventTrigger.triggers.Add(onChick);

    }
   
    void ClickAddPaidCoins()
    {
        //Todo AddPaidCoins;
        Debug.Log("ClickAddPaidCoins");
    }

}

