using Assets.Script.Core.Server.Services;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using NetWorkServices;
namespace Assets.Script.Core.Server
{
    public class ServerRequestServices : IDisposable , IServices
    {
     
        public Dictionary<string, NetRequest> NetRequests { get; set; } = new();
      
        public ServerRequestServices() {

            Start();
        }

        void Start()
        {
            Network network = Network.Get();
            network.onClientConnect += OnClientConnected;
            network.onClientDisconnect += OnClientDisconnected;

            IServices.TryAdd<Server_Account_Send, Server_Account_Receive>(this,NetworkMsg_HandlerTag.Account,network);
            IServices.TryAdd<Server_Battle_Send, Server_Battle_Receive>(this,NetworkMsg_HandlerTag.Battle, network);

            IServices.TryAdd<Server_GameEvent_Send, Server_GameEvent_Receive>(this, NetworkMsg_HandlerTag.GameEvent, network);
        }
        public async void Updata()
        {
           foreach(var a in NetRequests)
            {
                await a.Value.Updata();
            }

        }

        private void OnClientDisconnected(ulong client_id)
        {
            //throw new NotImplementedException();
        }

        private void OnClientConnected(ulong client_id)
        {
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            foreach (var a in NetRequests)
            {
               a.Value.Dispose();
            }
        }
    }
}