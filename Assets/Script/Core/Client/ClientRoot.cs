using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using YooAsset;

namespace Client
{
    public class ClientRoot
    {
        public readonly ClientPackageSetting packageSetting;
        public readonly ClientRequestServices clientRequestServices;
        public readonly ClientAsset clientAsset;
        readonly ClientMsgEvent clientMsgEvent;

        static ClientRoot instance;
        public ClientUserData ClientUserData { get; private set; } = new();
        public Action UpdataPlayerDataCallBack;

        bool Inited;
        public static ClientRoot Get()
        {
            if (instance == null)
            {
                instance = new ClientRoot();
            }
            return instance;
        }
        ClientRoot()
        {
            var net = Network.Get();
            YooAssets.Initialize();
            packageSetting = ClientPackageSetting.Get();
            clientRequestServices = new ClientRequestServices();
            YooAsset_Tool.SerRemoteServicesUrl(net.data.FileUrl_1, net.data.FileUrl_2);
            clientAsset = new ClientAsset(packageSetting.PlayMode);
            clientMsgEvent = new ClientMsgEvent();
        }
        public async Task StartInitUpdata()
        {
            if (Inited) return;
            await clientAsset.InitUpdata(packageSetting);
            Inited = true;

        }
    }

    

}


