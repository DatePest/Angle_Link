using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static Client.ClientScene;

public class HomeSwich : MonoBehaviour
{
    void Start()
    {
        int index = 1;
        foreach (Transform item in transform)
        {            Addevent(item, index);
            index++;
        }
    }

    void Addevent(Transform t,int i)
    {
        EventTrigger eventTrigger = t.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry onChick = new EventTrigger.Entry();
        onChick.eventID = EventTriggerType.PointerClick;
        onChick.callback.AddListener((data) => 
        {
            SwichScene(i);}   
        );
        eventTrigger.triggers.Add(onChick);
    }

    void SwichScene(int i)
    {
        var h = CheckNeme(i);
        if (h == default) return;
        var s = ToSceneName(h);
        if (s == default) return;

        HomeRoot.Get().SwichScene(s);
    }

    ClientSceneName ToSceneName(HomeSceneName homeSceneName)
    {
       
        if (Enum.IsDefined(typeof(ClientSceneName), homeSceneName.ToString()))
        {
            var sceneName = Enum.Parse(typeof(ClientSceneName),homeSceneName.ToString());
            return (ClientSceneName)sceneName;
        }
        throw new ArgumentOutOfRangeException("Invalid ToSceneName.");
    }
    HomeSceneName CheckNeme(int i)
    {
        if (Enum.IsDefined(typeof(HomeSceneName), i))
        {
            var sceneName = Enum.ToObject(typeof(HomeSceneName),i);
            return (HomeSceneName)sceneName;
        }
        throw new ArgumentOutOfRangeException(nameof(i), "Invalid GetType.");
    }
    enum HomeSceneName
    {
        News = 1,
        Character = 2,
        Story = 3,
        BattleStageSelect = 4,
        Radio = 5,
        Gasha = 6,
        Config = 7
    }
}
