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

        bool Inited;
        public static ClientRoot Get()
        {
            if (instance == null)
            {
                instance = new ClientRoot();
            }
            return instance;
        }
        public static string GetToken()
        {
            if(instance == null) return null;
            return instance.ClientUserData.GetToken();
        }
        ClientRoot()
        {
            var net = Network.Get();
            packageSetting = ClientPackageSetting.Get();
            clientRequestServices = new ClientRequestServices();
            YooAsset_Tool.SerRemoteServicesUrl(net.data.FileUrl_1, net.data.FileUrl_2);
            clientAsset = new ClientAsset(packageSetting.PlayMode);
            clientMsgEvent = new ClientMsgEvent();
        }
        public async Task<bool> StartInitUpdata()
        {
            if (Inited) return true;
            var b = await clientAsset.InitUpdata(packageSetting);
            if (b)
            {
                Inited = true;
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    

}


