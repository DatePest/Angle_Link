using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Script.HotUpdateDll.Game.Battle_
{
    public class BattleRoot : MonoBehaviour
    {
        BattleClient battleClient;
        BattleUI battleUI;
        GameObject mCanvas, AnimeCanvas, BattleUIIButton;
        public float CurrentSpeed { get; private set; }

        private void Awake()
        {
            mCanvas = GameObject.Find("CanvasPrefab").gameObject;
            AnimeCanvas = GameObject.Find("AnimeCanvas").gameObject;
            BattleUIIButton = GameObject.Find("BattleUI").gameObject;
            battleClient = new();
            battleUI = new();
            Init_UI_BatteSelectAction();
        }
        private void Start()
        {
            battleUI._BattleSpeed.OnSpeed += (sp) => CurrentSpeed = sp;
            BattleUIIButton.transform.SetAsLastSibling();
            var bdata =Client.ClientRoot.Get().ClientUserData.GetCache<BattleData>(GameConstant.OriginalBattleData);
            battleClient.Init(bdata);
            battleClient.OnGameStartToServer();
        }
        void Init_UI_BatteSelectAction()
        {
            var SaObj = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "SelectAction");
            var Sa = GameObject.Instantiate(SaObj, mCanvas.transform, false);
            Sa.SetActive(false);
            Sa.GetComponent<UI_BatteSelectAction>().RegSelectEndSendToServer(battleClient.SelectToServer);
        }
     
        #region TestLoaclServer
        //bool TestServer;
        //private void OnGUI()
        //{
        //    if (!TestServer)
        //    {
        //        if (GUI.Button(new Rect(120, 40, 120, 40), "StartServer"))
        //        {
        //            BattleServer gameServer = new BattleServer();
        //            gameServer.Init(Client.ClientRoot.Get().ClientUserData.LoaclBattleSettings);
        //            TestServer = true;
        //            gameServer.OnSend += (data) =>
        //            {

        //                var burrReader = new FastBufferReader(data, Unity.Collections.Allocator.Temp);
        //                var Ndata = new NetSerializedData(burrReader);
        //                var Logs = Ndata.Get<MsgLogs>();
        //                battleClient.logQueue.TaskAdd(Logs.GameLogs);
        //                burrReader.Dispose();

        //            };

        //            mCanvas.GetComponentInChildren<UI_BatteSelectAction>(true).RegSelectEndSendToServer((e) =>
        //            { gameServer.ReceiveSelect(e); });
        //            gameServer.Start();
        //        }
        //    }
        //}
        #endregion
        private void OnDestroy()
        {
            battleClient?.Dispose();
            battleUI?.Dispose();
            Client.ClientRoot.Get().ClientUserData.RemoveCache(GameConstant.OriginalBattleData);
        }

        public static BattleRoot Get()
        {
            var t = GameObject.FindAnyObjectByType<BattleRoot>();
            if (t == null) return null;
            return t;
        }
        public ClientEvent GetClientEvent() => battleClient.GameEvent;
        public void RegisterBattleSpeed(Action<float> action) => battleUI._BattleSpeed.OnSpeed += action;
        public ref Action<float> GetRegisterBattleSpeedAction() => ref battleUI._BattleSpeed.OnSpeed;
        public byte GetClientPlayerID() => battleClient.Game.GetPlayer().Id;
    }

}