using NetWorkServices;

namespace Client
{
    public class ClientService : IServices
    {
        protected override void NetRequestsAdd()
        {
            var network = Network.Get();
            TryAdd<Client_Account_Send, Client_Account_Receive>(NetworkMsg_HandlerTag.Account, network);
            TryAdd<Client_Battle_Send, Client_Battle_Receive>(NetworkMsg_HandlerTag.Battle, network);
            TryAdd<Client_Develop_Send, Client_Develop_Receive>(NetworkMsg_HandlerTag.GameEvent, network);

        }
    }
}
