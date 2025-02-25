using Client;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.HotUpdateDll.Game.Battle_
{
    public class BattleUI : IDisposable
    {
        public Client_BattleSpeed _BattleSpeed;
        Client_BattleUIGameState client_BattleUIGameState;
        Client_MenuButton client_MenuButton;
        Client_AutoButton AutoButton;
        Client_GameEnd client_GameEnd;
        Client_Exitutton client_Exitutton;
        public BattleUI()
        {
            _BattleSpeed = new();
            client_BattleUIGameState = new();
            client_MenuButton = new();
            AutoButton = new();
            client_GameEnd = new ();
            client_Exitutton = new ();
        }
        public void Dispose()
        {
            _BattleSpeed.Dispose();
            _BattleSpeed = null;
            client_BattleUIGameState.Dispose();
            client_BattleUIGameState = null;
            client_MenuButton.Dispose();
            client_MenuButton = null;
            AutoButton.Dispose();
            AutoButton = null;
            client_GameEnd.Dispose();
            client_GameEnd = null;
            client_Exitutton.Dispose();
            client_Exitutton =null; 
        }

    }
    public class Client_BattleUIGameState : IDisposable
    {
        TextMeshProUGUI ProUGUI;
        public Client_BattleUIGameState()
        {
            var t = GameObject.Find("GameState");
            if (t == null) throw new Exception("GameState Is null");
            ProUGUI = t.GetComponent<TextMeshProUGUI>();
            BattleRoot.Get().GetClientEvent().OnGameState += UiUpdata;
        }
        // Wave : 1/4	Turn : 01/30
        private void UiUpdata(BattleData battle)
        {
            ProUGUI.text = $" Wave : {battle.CurrentWave}/{battle.WaveMax}	Turn : {battle.Trun}/{battle.TrunMax}";
        }

        public void Dispose()
        {
            ProUGUI = null;
        }
    }
    public class Client_BattleSpeed : IDisposable
    {
        GameObject Target;
        public Action<float> OnSpeed;
        public Client_BattleSpeed()
        {
            var t = GameObject.Find("Button_Speed");
            if (t == null) throw new Exception("Button_Speed Is null");
            Target = t;
            var b = Target.GetComponent<Button>();
            b.onClick.AddListener(onClick);
            var text = Target.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Normal";
        }

        private void onClick()
        {
            var text = Target.GetComponentInChildren<TextMeshProUGUI>();

            switch (text.text)
            {
                case "Fast":  // >>> => >
                    text.text = "Slow";
                    OnSpeed?.Invoke(0.5f);
                    break;
                case "Slow":  // > =>   >>
                    text.text = "Normal";
                    OnSpeed?.Invoke(1f);
                    break;
                case "Normal": // >> => >>>
                    text.text = "Fast";
                    OnSpeed?.Invoke(2f);
                    break;

            }
        }

        public void Dispose()
        {
            Target = null;
            OnSpeed = null;
        }
    }
    public class Client_MenuButton : IDisposable
    {
        public Client_MenuButton()
        {
            var t = GameObject.Find("Button_Menu");
            if (t == null) throw new Exception("Button_Menu Is null");
            t.GetComponent<Button>().onClick.AddListener(_Button);
        }

        public void Dispose()
        {
            return;
        }

        private void _Button()
        {
            Debug.Log("Button_Menu");
        }
    }
    public class Client_Exitutton : IDisposable
    {
        public Client_Exitutton()
        {
            var t = GameObject.Find("Button_Exit");
            if (t == null) throw new Exception("Button_Exit Is null");
            t.GetComponent<Button>().onClick.AddListener(_Button);
        }

        public void Dispose()
        {
            return;
        }

        private void _Button()
        {
            ClientScene.Goto(ClientScene.ClientSceneName.Home);
        }
    }
    public class Client_AutoButton : IDisposable
    {
        public Client_AutoButton()
        {
            var t = GameObject.Find("Button_Auto");
            if (t == null) throw new Exception("Button_Auto Is null");
            t.GetComponent<Button>().onClick.AddListener(_Button);
        }

        public void Dispose()
        {
            return;
        }

        private void _Button()
        {
            Debug.Log("Client_AutoButton");
        }
    }
    public class Client_GameEnd : IDisposable
    {
        GameObject Target;
        const string Name = "BattleEnd";
        bool iswin;
        Ui_Layout  ui_Layout;
        public Client_GameEnd()
        {
            init();
            BattleRoot.Get().GetClientEvent().OnGameEnd += Excute;
            BattleRoot.Get().GetClientEvent().GetItem += updataItem;
        }
        void NewBox(Sprite sp,int amount)
        {
            Target = GameObject.Instantiate(ui_Layout.DefaultObj);
            var text = Target.GetComponentInChildren<TextMeshProUGUI>();
            if (amount != 0)
            {
                text.text = amount.ToString();
            }
            else
                text.text = "";

            var img = Target.GetComponentInChildren<Image>();
            img.sprite = sp;

            ui_Layout.AddSelectContent(Target);

        }
        private async void updataItem(SettlementDatas_Net net)
        {
            if (net.Items != null)
                foreach (var item in net.Items)
                {
                    var sp = await AssetFInd.GetItemData_Async(item.AssetName);
                    NewBox(sp.Icon, item.Amount);

                }
            if (net.Characters != null)
                foreach (var item in net.Characters)
                {
                    var sp = await AssetFInd.GetCharacterData_Async(item.AssetName);
                    NewBox(sp.Art.SDIcon, 0);
                }
            if (net.Money > 0)
                NewBox(null, net.Money);
        }

        void init()
        {
            var t = GameObject.Find("CanvasPrefab");
            if (t == null) throw new Exception("CanvasPrefab Is null");
            var Asset = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, Name);
            Target =GameObject.Instantiate(Asset,t.transform,false);
            ui_Layout =Target.GetComponentInChildren<Ui_Layout>();
            var b = Target.GetComponentInChildren<Button>(); 
            b.onClick.AddListener(Win_Button);
            Target.SetActive(false);
        }
        private async void Excute(bool obj)
        {
            await Task.Delay(1000);
            Target.SetActive(true);
            Target.transform.SetAsLastSibling();
            var Text = Target.transform.Find("EndText").GetComponent<TextMeshProUGUI>();
            if (obj)
            {
                Text.text = " Win ";
            }
            else
            {
                Text.text = " Lose ";
            }
            iswin = obj;
        }

        public void Dispose()
        {
            Target = null;
            ui_Layout = null;
        }
        void Exit()
        {
            ClientScene.Goto(ClientScene.ClientSceneName.Home);
        }
        private void Win_Button()
        {
            if (iswin)
            {
                BattleRoot.Get().GetClientEvent().RequestSettlementToServer?.Invoke();
                var b = Target.GetComponentInChildren<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(Exit);
                b.GetComponentInChildren<TextMeshProUGUI>().text = "Exit";

            }
            else
                Exit();
        }
    }
}
