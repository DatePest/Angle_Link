using EventSystemTool;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    public class Client_Account_Receive : IRequestReceive
    {
        public Client_Account_Receive(string listenMsgTag) : base(listenMsgTag) { }
        [EventTag(NetworkMsg_HandlerTag.ReturnMsg)]
        public void ReturnMsg(ReceiveNetSerializedData data)
        {
            var  msg = data.NData.Get<MsgEvent_Net>();
            Ui_SystemMsg.Get().ShowMsg(msg.msg);
            // Todo  Call other functions against event ID
        }
        [EventTag(NetworkMsg_HandlerTag.ReturnErrorMsg_BackLogin)]
        public void ReturnErrorMsg_BackLogin(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<MsgEvent_Net>();
            Ui_SystemMsg.Get().WaitConfirm(msg.msg, () => UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Login.ToString(), GameConstant.PackName_GameCore, UnityEngine.SceneManagement.LoadSceneMode.Single));
        }
        [EventTag(NetworkMsg_HandlerTag.ReturnReg)]
        public void Reg(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<UserData>();
            ClientRoot.Get().ClientUserData.SetUserData(msg);

            Ui_SystemMsg.Get().WaitConfirm("Registration successful",()=> UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Home.ToString(), GameConstant.PackName_GameCore,UnityEngine.SceneManagement.LoadSceneMode.Single));
        }
        [EventTag(NetworkMsg_HandlerTag.Returnlogin)]
        public void Login(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<UserData>();
            ClientRoot.Get().ClientUserData.SetUserData(msg);
            UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Home.ToString(), GameConstant.PackName_GameCore, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        [EventTag(NetworkMsg_HandlerTag.ReturnGetPlayerData)]
        public void UpdatePlayerData(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<PlayerData_Net>();

            ClientRoot.Get().ClientUserData.UpdatePlayerData(msg);
        }
    }
}
