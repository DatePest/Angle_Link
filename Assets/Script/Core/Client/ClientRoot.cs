using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using YooAsset;

namespace Client
{
    public class ClientRoot
    {
        public  ClientPackageSetting packageSetting;
        public  ClientRequestServices clientRequestServices;
        public  ClientAsset clientAsset;
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
            clientRequestServices = new ClientRequestServices( new ClientService());
            YooAsset_Tool.SetRemoteFileUrl(net.data.FileUrl_1, net.data.FileUrl_2);
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

    public class Custom_reg
    {
        public static void Reg_NetWait()
        {
            var c = ClientRoot.Get().clientRequestServices;
            c.ConnectionControl.OnConnectFailed += ConnectFailed;
            c.ConnectionControl.OnSend_StartWait += () => ShowWait(true);
            c.ConnectionControl.OnReceiveEnd += () => ShowWait(false);

        }
       static void ConnectFailed()
        {
            Ui_LoadIng.Get()?.Show(false);
            Ui_SystemMsg.Get()?.ShowMsg("ConnectFailedToServer");
        }
        static void ShowWait(bool b)
        {
            Ui_LoadIng.Get().Show(b);
        }
    }

}


