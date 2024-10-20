using System;
using System.Collections;
using System.Security.Principal;
using UnityEngine;
using YooAsset;
using static IRequestSend;

namespace Client.Login
{
    public class Login_Root : MonoBehaviour
    {
        Canvas canvas;
        ClientRoot  clientRoot;
        Ui_Login ui_Login;
        Ui_GameStart ui_GameStart;
        bool inited;
        void Start()
        {
            canvas = FindAnyObjectByType<Canvas>();
            ui_Login = FindAnyObjectByType<Ui_Login>();
            ui_GameStart = FindAnyObjectByType<Ui_GameStart>();
            ui_GameStart.gameObject.SetActive(true);
            ui_GameStart.ClickHandler += () => ui_Login.Show();
            inited = true;

        }
        private void OnGUI()
        {
            if (!inited) return;
            if (GUI.Button(new Rect (40,80 , 120 ,40),"GoHome"))
            {
                UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Home,UnityEngine.SceneManagement.LoadSceneMode.Single);
            }


            if (GUI.Button(new Rect(40, 120, 120, 40), "Test"))
            {
                var sd = new NetSendData(ClientEventTag.Test, NetworkMsg_HandlerName.Account, default);
                var msg = new MsgEvent();
                msg.msg = "Hello!";
                msg.MsgId = 100;
                sd.data = msg;
                EventSystemTool.EventSystem.Publish(sd);
            }
        }
    }
}
