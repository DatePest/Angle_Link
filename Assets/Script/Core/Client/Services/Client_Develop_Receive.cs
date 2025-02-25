using EventSystemTool;
using GameApi;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Client.ClientScene;
using UnityEngine.SceneManagement;


namespace Client
{
    // Tag is GameEvent
    public class Client_Develop_Receive : IRequestReceive
    {
        public Client_Develop_Receive(string listenMsgTag) : base(listenMsgTag) { }
        [EventTag(NetworkMsg_HandlerTag.ReturnCharacterDevelop_Lv)]
        public void ReturnMsg(ReceiveNetSerializedData data)
        {
            var msg = data.NData.Get<Response_Net>();

            if (msg.success)
            {
                var a = ClientRoot.Get().ClientUserData.GetCache<Action>(GameConstant.DevelopEvent, true);
                a?.Invoke();
            }
            var u = Ui_SystemMsg.Get();
            u.WaitConfirm(msg.msg);
        }

    }
}
