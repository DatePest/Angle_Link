using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.Server.Services
{
    public class ServerServices : IServices
    {

        protected override void NetRequestsAdd()
        {
            var network = Network.Get();

            TryAdd<Server_Account_Send, Server_Account_Receive>( NetworkMsg_HandlerTag.Account, network);
            TryAdd<Server_Battle_Send, Server_Battle_Receive>( NetworkMsg_HandlerTag.Battle, network);
            TryAdd<Server_GameEvent_Send, Server_GameEvent_Receive>( NetworkMsg_HandlerTag.GameEvent, network);

        }
    }
}
