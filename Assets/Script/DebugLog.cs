using System;
using TMPro;
using UnityEngine;
public class DebugLog : MonoBehaviour
{
    public TextMeshProUGUI proUGUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        proUGUI = GetComponentInChildren<TextMeshProUGUI>();
        proUGUI.text = string.Empty;
        Application.logMessageReceived += Msg;
    }

    private void Msg(string mgs, string stackTrace, LogType type)
    {
        if(type != LogType.Assert&&type != LogType.Warning)
        proUGUI.text += (mgs + "\n");
    }
    public void Clear()
    {
        proUGUI.text = string.Empty;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Msg;
    }
}
