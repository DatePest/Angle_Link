using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameApi;
using Tools;


namespace Client.Login
{
    public class Ui_Login : UI_Base
    {
        Button LoginButton;
        Button RegButton;
        TMP_InputField account , password;
        GameObject RegObj;

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
           if(RegObj == null)
            {
                var g =YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "Ui_Regisiter");
                RegObj =GameObject.Instantiate(g,transform.parent,false);
            }
            gameObject.SetActive(false);
            RegObj.SetActive(true);
        }

        private void Login()
        {
            // chkeck
            if (account.text == string.Empty) { return; }
            if (password.text == string.Empty) { return; }
            //
            var msg = new LoginRequest();
            msg.username = account.text;
            msg.password = password.text;
            EventSystemToolExpand.Publish(ClientEventTag.SendLogin, NetworkMsg_HandlerTag.Account, default, msg);
        }
    }
}
