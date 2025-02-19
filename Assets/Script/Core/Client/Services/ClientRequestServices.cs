using Cysharp.Threading.Tasks;
using NetWorkServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using static NetworkSetting;

namespace Client
{
    public class ClientRequestServices : IServices
    {
        public Action<string, ushort, object> SendRequest;
        public Action<string, ushort> SendEnd;
        public Action<string, ushort> ReceiveEnd;



        ClientConnectionControl connectionControl;

        public Dictionary<string, NetRequest> NetRequests { get;  set; } = new();

        //public Dictionary<string, NetRequest> NetRequests { get ; protected set; } = new();

        public ClientRequestServices()
        {
            Network network = Network.Get();

            connectionControl = new ClientConnectionControl(this, network);
            #region NetRequestsAdd
            IServices.TryAdd<Client_Account_Send, Client_Account_Receive>(this, NetworkMsg_HandlerTag.Account, network);
            IServices.TryAdd<Client_Battle_Send, Client_Battle_Receive>(this, NetworkMsg_HandlerTag.Battle, network);
            IServices.TryAdd<Client_Develop_Send, Client_Develop_Receive>(this, NetworkMsg_HandlerTag.GameEvent, network);
            #endregion
            foreach (var r in NetRequests.Values)
            {
                r.RequestSend.EndCallback += SendEndCallback;
                r.RequestSend.ReceiveSendRequest += ReceiveSendRequest;
                r.RequestReceive.EndCallback += ReceiveCallback;
            }
        }

        private void ReceiveSendRequest(string MsgTag, ushort tag, object data)
        {
            SendRequest?.Invoke(MsgTag, tag, data);
        }

        private void SendEndCallback(string MsgTag, ushort tag)
        {
            SendEnd?.Invoke(MsgTag, tag);
        }

        private void ReceiveCallback(string MsgTag, ushort tag)
        {
            ReceiveEnd?.Invoke(MsgTag, tag);
        }

        public void Destory()
        {
            connectionControl.Dispose();
        }
    }

    /// <summary>
    /// Used to manage when to automatically disconnect from the agent to reduce server pressure
    /// Applicable to situations where the server is not WebApi and needs to be reconnected
    /// </summary>
    public class ClientConnectionControl : IDisposable
    {
        byte waitReceiveQuantity;
        Network network;
        ClientRequestServices requestServices;
        WaitSer waitSer;
        WithLockQueueTool.WithLockQueue<EventHandle_NetSendData> SendEventQueue;
        public ClientConnectionControl(ClientRequestServices requestServices, Network network)
        {
            requestServices.SendRequest += SendR;
            requestServices.SendEnd += Send;
            requestServices.ReceiveEnd += Receive;
            network.onClientFailedConnect += FailedConnect;
            this.network = network;
          
            SendEventQueue = new();
            waitSer = new(network, 5, async () =>
            { 
                await UniTask.SwitchToMainThread(); 
                await SendEventQueue.Excute();
            }, ConnectFailedToServer) ;
        }
        private void FailedConnect(UnityTransport.ClientFailedConnectEvent @event)
        {
            switch (@event)
            {
                case UnityTransport.ClientFailedConnectEvent.Disconnected:
                    break;
                case UnityTransport.ClientFailedConnectEvent.ConnectFailed:

                    //UnityEngine.Debug.LogError("IsConnectedClient__" + network.NetManager.IsConnectedClient);
                    ConnectFailedToServer();
                    break;
            }
        }

        public  async void ConnectFailedToServer()
        {
            Ui_LoadIng.Get()?.Show(false);
            Ui_SystemMsg.Get()?.ShowMsg("ConnectFailedToServer");
            SendEventQueue.Clear();
            waitReceiveQuantity = 0;
        }

        async void SendR(string MsgTag, ushort tag, object data)
        {
            await SendEventQueue.Add((EventHandle_NetSendData)data);
            Ui_LoadIng.Get().Show(true);
            waitSer.ConnectToServer();
        }

        void Send(string MsgTag, ushort tag)
        {
            waitReceiveQuantity++;
        }
        async void Receive(string MsgTag, ushort tag)
        {
            waitReceiveQuantity--;

            if (waitReceiveQuantity <= 0)
            {
                if (network.IsActive()) network.Disconnect();
                waitReceiveQuantity = 0;
                Ui_LoadIng.Get().Show(false);
                waitSer.StopWaitSerReceive();
            }
        }
         void Remove()
        {
            requestServices.SendRequest -= SendR;
            requestServices.SendEnd -= Send;
            requestServices.ReceiveEnd -= Receive;
            network.onClientFailedConnect -= FailedConnect;
        }

        public void Dispose()
        {
            Remove();
            waitSer.Dispose();
            SendEventQueue.Dispose();
        }

        public class WaitSer : IDisposable
        {
            Action Exceut, OnConnectFailed;
            Network network;
            bool WaitConnect = false, WaitReveive = false;
            int WaiTime = 10, Time;
            CancellationTokenSource token;
            public WaitSer(Network network, int WaitTIme, Action action, Action OnConnectFailed)
            {
                this.network = network;
                this.WaiTime=WaitTIme;
                this.OnConnectFailed = OnConnectFailed;
                Exceut =action;
                network.onClientFailedConnect += FailedConnect;
            }

            private void FailedConnect(UnityTransport.ClientFailedConnectEvent @event)
            {
                StopWaitSerReceive();
                WaitConnect = false;
            }

            public async void ConnectToServer()
            {
                if (WaitConnect) return;
                await UniTask.Yield(); //Wait for initialization to finish
                if (network.IsActive())
                {
                    Exceut?.Invoke();
                    StartWait(true);
                    return; // Already connected
                }
                if (network.data.solo_type == SoloType.Offline)
                {

                    network.StartHostOffline();
                    //WebGL dont support hosting a game, must join a dedicated server, in solo it starts a offline mode that doesn't use netcode at all
                }
                else
                {
                    network.StartClient(network.data.ServerUrl, network.data.port);  //Connect server
                }
                network.onConnect += Connect;
                WaitConnect = true;
                StartWait();
            }
            public void StopWaitSerReceive()
            {
                if (!token.IsCancellationRequested)
                {
                    token.Cancel();
                    token.Dispose();
                }
                WaitReveive = false;
            }
            void StartWait(bool Reset = false)
            {
                if (Reset)
                {
                    StopWaitSerReceive();
                }
                if (!WaitReveive)
                {
                    token = new();
                    WaitSerReceive(token.Token);
                }
                else
                    Time = 0;
            }
            async UniTaskVoid WaitSerReceive(CancellationToken token)
            {
                WaitReveive = true;
                Time = 0;
                while (Time <= WaiTime)
                {
                    await Task.Delay(1000);
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    Time += 1;
                }
                await UniTask.SwitchToMainThread();
                network.Disconnect();
                OnConnectFailed?.Invoke();
                StopWaitSerReceive();
            }
     
            void Connect()
            {
                network.onConnect -= Connect;
                WaitConnect = false;
                StartWait(true); 
                Exceut?.Invoke();
            }
          
            public void Dispose()
            {
                network.onClientFailedConnect -= FailedConnect;
                token.Cancel();
                token.Dispose();
                Exceut = null;
                OnConnectFailed = null; 
            }
            

        }
    }
   

}