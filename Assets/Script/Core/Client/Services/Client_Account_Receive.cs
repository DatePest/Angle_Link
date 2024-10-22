using EventSystemTool;
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
        [EventTag(NetworkMsg_HandlerName.ReturnMsg)]
        public void ReturnMsg(ReceiveNetSerializedData data)
        {
            var  msg = data.NData.Get<MsgEvent>();
            Ui_SystemMsg.Get().SendMessage(msg.msg);
            // Todo  Call other functions against event ID
        }

        [EventTag(NetworkMsg_HandlerName.Returnlogin)]
        public void Login(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<UserData>();
            ClientRoot.Get().ClientUserData.SetUserData(msg);
            UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Home,UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        [EventTag(NetworkMsg_HandlerName.ReturnGetPlayerData)]
        public void UpdatePlayerData(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<PlayerData_Net>();
            ClientRoot.Get().ClientUserData.UpdatePlayerData(msg);
        }
    }
}
