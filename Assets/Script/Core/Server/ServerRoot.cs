using Assets.Script.Core.Server.Services;
using UnityEngine;
using YooAsset;

namespace Assets.Script.Core.Server
{
    public class ServerRoot : SingletonTool.SigMono<ServerRoot>
    {
        public ushort port;
        public ServerRequestServices serverRequestServices;
        public ServerBattleManager serverBattleManager;
        public EPlayMode ePlayMode;

        protected override void Awake()
        {
            base.Awake();
            Application.runInBackground = true;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        async void Start()
        {
          await YooAsset_Tool.InitPackageAsync(ePlayMode, GameConstant.PackName_GameCore);

            Network network = Network.Get();
            network.StartServer(port);
            serverBattleManager = new();
            serverRequestServices = new(new ServerServices());


        }
        
        private void FixedUpdate()
        {
            serverRequestServices?.Updata();
            serverBattleManager?.CheckHeart();

        }
        protected override void OnDestroy()
        {
            serverRequestServices?.Dispose();
            serverBattleManager = null;
            base.OnDestroy();
        }


    }

    


}
