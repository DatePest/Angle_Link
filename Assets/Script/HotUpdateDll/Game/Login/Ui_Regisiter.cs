using Client;
using Client.Login;
using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Regisiter : MonoBehaviour
{
    Button ReturnButton;
    Button RegButton;
    TMP_InputField account, password , email;
    void Start()
    {
        ReturnButton = transform.Find("ReturnButton").GetComponent<Button>();
        RegButton = transform.Find("RegButton").GetComponent<Button>();
        account = transform.Find("Account").GetComponent<TMP_InputField>();
        email = transform.Find("AccountEmail").GetComponent<TMP_InputField>();
        password = transform.Find("Password").GetComponent<TMP_InputField>();
        ReturnButton.onClick.AddListener(Return);
        RegButton.onClick.AddListener(Reg);
    }
    bool Check()
    {
        if (account.text == string.Empty) { return false; }
        if (password.text == string.Empty  ) { return false; }
        if (email.text == string.Empty) { return false; }

        if (account.text.Length < 4)
        {
            Ui_SystemMsg.Get().ShowMsg("The account must be at least 4 characters long.");
            return false;
        }
        if (account.text.Contains(" "))
        {
            Ui_SystemMsg.Get().ShowMsg("The account cannot contain spaces.");
            return false;
        }
        if (password.text.Length < 6)
        {
            Ui_SystemMsg.Get().ShowMsg("The password must be at least 6 characters long.");
            return false;
        }
        if (password.text.Contains(" "))
        {
            Ui_SystemMsg.Get().ShowMsg("The password cannot contain spaces.");
            return false;
        }
        if (email.text.Contains(" "))
        {
            Ui_SystemMsg.Get().ShowMsg("The email cannot contain spaces.");
            return false;
        }

        return true;
    }
    private void Reg()
    {
        if (!Check())
        {
            return;

        }
        

       
        var msg = new GameApi.RegisterRequest();
        msg.username = account.text;
        msg.password = password.text;
        msg.email = email.text;
        EventSystemToolExpand.Publish(ClientEventTag.SendRegister, NetworkMsg_HandlerTag.Account, default, msg);
    }

    private void Return()
    {
        gameObject.SetActive(false);
        GameObject.FindFirstObjectByType<Ui_Login>(FindObjectsInactive.Include).gameObject.SetActive(true);
    }

}
