using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;

namespace Client
{
    public class ClientRequestServices
    {
        public Action<string, ushort> SendRequest;
        public Action<string, ushort> SendEnd;
        public Action<string, ushort> ReceiveEnd;

        public readonly Dictionary<string, NetRequest> NetRequests = new();


        ClientConnectionControl connectionControl;
        public ClientRequestServices()
        {
            Network network = Network.Get();

            connectionControl = new ClientConnectionControl(this, network);
            #region NetRequestsAdd
            NetRequests.Add(NetworkMsg_HandlerName.Account, new NetRequest(network,
                new Client_Account_Send(NetworkMsg_HandlerName.Account, network),
                new Client_Account_Receive(NetworkMsg_HandlerName.Account)));
            #endregion
            foreach (var r in NetRequests.Values)
            {
                r.RequestSend.EndCallback += SendEndCallback;
                r.RequestSend.ReceiveSendRequest += ReceiveSendRequest;
                r.RequestReceive.EndCallback += ReceiveCallback;
            }
        }

        private void ReceiveSendRequest(string MsgTag, ushort tag)
        {
            SendRequest?.Invoke(MsgTag, tag);
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
            connectionControl.Remove();
        }
    }

    /// <summary>
    /// Used to manage when to automatically disconnect from the agent to reduce server pressure
    /// Applicable to situations where the server is not WebApi and needs to be reconnected
    /// </summary>
    public class ClientConnectionControl
    {
        byte waitReceiveQuantity;
        Network network;
        ClientRequestServices requestServices;
        public ClientConnectionControl(ClientRequestServices requestServices , Network network)
        {
            requestServices.SendRequest += SendR;
            requestServices.SendEnd += Send;
            requestServices.ReceiveEnd += Receive;
            this.network = network;
            network.onClientFailedConnect += FailedConnect;
        }

        private void FailedConnect(UnityTransport.ClientFailedConnectEvent @event)
        {
           switch (@event)
            {
                case UnityTransport.ClientFailedConnectEvent.Disconnected:
                    break; 
                 case UnityTransport.ClientFailedConnectEvent.ConnectFailed:

                    //UnityEngine.Debug.LogError("IsConnectedClient__" + network.NetManager.IsConnectedClient);
                    Ui_LoadIng.Get().Show(false);
                    Ui_SystemMsg.Get().Show("ConnectFailedToServer");
                    IRequestSend_Client.ClearSendData();
                    break;
            }
        }

        void SendR(string MsgTag, ushort tag)
        {
            Ui_LoadIng.Get().Show(true);
        }

        void Send(string MsgTag, ushort tag)
        {
            waitReceiveQuantity++;
        }
        void Receive(string MsgTag, ushort tag)
        {
            waitReceiveQuantity--;

           if (waitReceiveQuantity <= 0) 
            {
                if(network.IsActive()) network.Disconnect();
                waitReceiveQuantity = 0;
                Ui_LoadIng.Get().Show(false);
            }
        }
        public void Remove()
        {
            requestServices.SendRequest -= SendR;
            requestServices.SendEnd -= Send;
            requestServices.ReceiveEnd -= Receive;
            network.onClientFailedConnect -= FailedConnect;
        }

    }


}
