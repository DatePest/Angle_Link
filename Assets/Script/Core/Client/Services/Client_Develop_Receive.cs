using EventSystemTool;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
    // Tag is GameEvent
    public class Client_Develop_Receive : IRequestReceive
    {
        public Client_Develop_Receive(string listenMsgTag) : base(listenMsgTag) { }
        [EventTag(NetworkMsg_HandlerTag.ReturnCharacterDevelop_Lv)]
        public void ReturnMsg(ReceiveNetSerializedData data)
        {
            GameUtilityTool.DebugAsync("A");
            //var msg = data.NData.Get<MsgEvent_Net>();
            //Ui_SystemMsg.Get().ShowMsg(msg.msg);
            // Todo  Call other functions against event ID
        }

    }
}
