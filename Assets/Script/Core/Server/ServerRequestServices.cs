using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using NetWorkServices;
namespace Assets.Script.Core.Server
{
    public class ServerRequestServices : IRequestServices
    {
        public ServerRequestServices(IServices services) : base(services)
        {
        }

        override protected void Init()
        {
            Network network = Network.Get();
            network.onClientConnect += OnClientConnected;
            network.onClientDisconnect += OnClientDisconnected;
        }
        public async void Updata()
        {
            foreach (var a in services.NetRequests)
            {
                await a.Value.Updata();
            }

        }

        protected virtual void OnClientDisconnected(ulong client_id)
        {
        }

        protected virtual void OnClientConnected(ulong client_id)
        {
        }

        override public void Dispose()
        {
            foreach (var a in services.NetRequests)
            {
                a.Value.Dispose();
            }
        }
    }
}