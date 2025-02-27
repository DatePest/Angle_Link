using Cysharp.Threading.Tasks;
using NetWorkServices;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Netcode;

namespace Client
{
    public class ClientRequestServices : IRequestServices
    {
        public Action<string, ushort, object> SendRequest;
        public Action<string, ushort> SendEnd;
        public Action<string, ushort> ReceiveEnd;

        public ClientConnectionControl ConnectionControl;

        public ClientRequestServices(IServices services) : base(services)
        {
        }

        override protected void Init()
        {
            Network network = Network.Get();

            ConnectionControl = new ClientConnectionControl(this, network);
            foreach (var r in services.NetRequests.Values)
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
            ConnectionControl.Dispose();
        }



        public override void Dispose()
        {
            foreach (var r in services.NetRequests.Values)
            {
                r.Dispose();
            }
            services.NetRequests.Clear();
            SendRequest = null;
            SendEnd = null;
            ReceiveEnd = null;

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

        public Action OnConnectFailed;
        public Action OnSend_StartWait;
        public Action OnReceiveEnd;
        public ClientConnectionControl(ClientRequestServices requestServices, Network network)
        {
            requestServices.SendRequest += Send_ToWait;
            requestServices.SendEnd += Send;
            requestServices.ReceiveEnd += Receive;
            network.OnConnectionEvent += FailedConnect;
            this.network = network;

            SendEventQueue = new();
            waitSer = new(network, 5, async () =>
            {
                await UniTask.SwitchToMainThread();
                await SendEventQueue.Excute();
            }, ConnectFailedToServer);
        }
        private void FailedConnect(Network network, ConnectionEventData @event)
        {
            if (network.IsConnecting())
            {
                ConnectFailedToServer();
            }
        }


        public async void ConnectFailedToServer()
        {
            OnConnectFailed?.Invoke();
            await SendEventQueue.Clear();
            waitReceiveQuantity = 0;
        }

        async void Send_ToWait(string MsgTag, ushort tag, object data)
        {
            await SendEventQueue.Add((EventHandle_NetSendData)data);
            OnSend_StartWait?.Invoke();
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
                OnReceiveEnd?.Invoke();
                waitSer.StopWaitSerReceive();
            }
        }
        void Remove()
        {
            requestServices.SendRequest -= Send_ToWait;
            requestServices.SendEnd -= Send;
            requestServices.ReceiveEnd -= Receive;
            network.OnConnectionEvent -= FailedConnect;
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
                this.WaiTime = WaitTIme;
                this.OnConnectFailed = OnConnectFailed;
                Exceut = action;
                network.OnConnectionEvent += FailedConnect;
            }

            private void FailedConnect(Network network, ConnectionEventData @event)
            {
                if (!network.IsConnected())
                {
                    StopWaitSerReceive();
                    WaitConnect = false;
                }
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
                if (network.data.solo_type == NetworkSetting.SoloType.Offline)
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
                network.OnConnectionEvent -= FailedConnect;
                token.Cancel();
                token.Dispose();
                Exceut = null;
                OnConnectFailed = null;
            }


        }
    }


}