using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
namespace Server
{
    public class ServerRequestServices : MonoBehaviour
    {
        private static ServerRequestServices _instance;
        public ushort port;
        NetRequest Account;
        protected virtual void Awake()
        {
            _instance = this;
            Application.runInBackground = true;
        }
        void Start()
        {
            Network network = Network.Get();
            network.onClientConnect += OnClientConnected;
            network.onClientDisconnect += OnClientDisconnected;
            Account = new NetRequest(network,
                new Server_Account_Send(NetworkMsg_HandlerName.Account, network),
                new Server_Account_Receive(NetworkMsg_HandlerName.Account));

            network.StartServer(port);
        }
        private void OnDestroy()
        {
            Account.Destory();
        }

        private void OnClientDisconnected(ulong client_id)
        {
            //throw new NotImplementedException();
        }

        private void OnClientConnected(ulong client_id)
        {
            //throw new NotImplementedException();
        }

    }
}