using UnityEngine;
using TMPro;
using static SingletonTool;
using UnityEngine.UI;
using System;
namespace Client
{
    public class Ui_SystemMsg : SigMono<Ui_SystemMsg>
    {

        [SerializeField] Button CloseButton,YesButton,NoButton;
        TextMeshProUGUI textUGU;

        Action TempAction;
        private void Awake()
        {
            base.Awake();
            textUGU = GetComponentInChildren<TextMeshProUGUI>();
            CloseButton.onClick.AddListener(Close);
            YesButton.onClick.AddListener(()=> { TempAction?.Invoke(); Close(); });
            NoButton.onClick.AddListener(Close);
            gameObject.SetActive(false);
        }
        public void Show(string msg)
        {
            textUGU.text = msg;
            gameObject.SetActive(true);
            CloseButton.gameObject.SetActive(true);
        }

        public void Close()
        {
            textUGU.text = string.Empty;
            gameObject.SetActive(false);
            ShowSelectButton(false);
            TempAction = null;
        }

        public void WatiUserSelect(string msg, Action action)
        {
            Show(msg);
            CloseButton.gameObject.SetActive(false);
            TempAction += action;
            ShowSelectButton(true);
        }
        void  ShowSelectButton(bool b)
        {
            YesButton.gameObject.SetActive(b);
            NoButton.gameObject.SetActive(b);
        }
    }

}
