using UnityEngine;
using TMPro;
using static SingletonTool;
using UnityEngine.UI;
using System;
namespace Client
{
    public class Ui_SystemMsg : SigMono<Ui_SystemMsg>
    {

        [SerializeField] Button CloseButton,YesButton,NoButton, ConfirmButton;
        TextMeshProUGUI textUGU;

        Action TempAction_1, TempAction_2;
        private void Awake()
        {
            base.Awake();
            textUGU = GetComponentInChildren<TextMeshProUGUI>();
            CloseButton.onClick.AddListener(Close);
            YesButton.onClick.AddListener(()=> { TempAction_1?.Invoke(); Close(); });
            ConfirmButton.onClick.AddListener(() => { TempAction_1?.Invoke(); Close(); });
            NoButton.onClick.AddListener(()=> { TempAction_2?.Invoke(); Close(); });
            gameObject.SetActive(false);
        }
        public void ShowMsg(string msg)
        {
            textUGU.text = msg;
            gameObject.SetActive(true);
            ShowSelectButton(false);
            CloseButton.gameObject.SetActive(true);
            ConfirmButton.gameObject.SetActive(false);
        }
        public void WaitUserSelect(string msg, Action yes_action, Action no_action = null)
        {
            ShowMsg(msg);
            if (yes_action != null)
                TempAction_1 += yes_action;
            if (no_action != null)
                TempAction_2 += no_action;
            ShowSelectButton(true);
            CloseButton.gameObject.SetActive(false);
            ConfirmButton.gameObject.SetActive(false);
        }
        public void WaitConfirm(string msg, Action action = null)
        {
            ShowMsg(msg);
            if (action != null) { TempAction_1 += action; }
            ShowSelectButton(false);
            CloseButton.gameObject.SetActive(false);
            ConfirmButton.gameObject.SetActive(true);
        }
        public void Close()
        {
            textUGU.text = string.Empty;
            gameObject.SetActive(false);
            CloseButton.gameObject.SetActive(false);
            ShowSelectButton(false);
            TempAction_1 = null;
            TempAction_2 = null;
        }
        void  ShowSelectButton(bool b)
        {
            YesButton.gameObject.SetActive(b);
            NoButton.gameObject.SetActive(b);
        }
        
    }

}
