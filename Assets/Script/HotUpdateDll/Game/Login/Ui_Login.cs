using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static IRequestSend;


namespace Client.Login
{
    public class Ui_Login : UI_Base
    {
        Button LoginButton;
        Button RegButton;
        TMP_InputField account , password;

        protected override void Awake()
        {
            base.Awake();
            LoginButton = transform.Find("LoginButton").GetComponent<Button>();
            RegButton = transform.Find("RegButton").GetComponent<Button>();
            account = transform.Find("Account").GetComponent<TMP_InputField>();
            password = transform.Find("Password").GetComponent<TMP_InputField>();
            LoginButton.onClick.AddListener(Login);
            RegButton.onClick.AddListener(Reg);
        }

        private void Start()
        {
            Hide();
        }
        private void Reg()
        {
            if (account.text == string.Empty) { return; }
            if (password.text == string.Empty) { return; }
        }

        private void Login()
        {
            // chkeck
            if (account.text == string.Empty) { return; }
            if (password.text == string.Empty) { return; }
            //
            var sd = new NetSendData(ClientEventTag.SendLogin, NetworkMsg_HandlerName.Account, default);
            var msg = new LoginRequest();
            msg.username = account.text;
            msg.password = password.text;
            sd.data = msg;
            EventSystemTool.EventSystem.Publish(sd);
        }
    }
}
