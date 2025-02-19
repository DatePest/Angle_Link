using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Netcode;
using WithLockQueueTool;

namespace Assets.Script.Core.Server
{
    public class Rresult_BattleSerData : IDisposable
    {
        public bool Success = false;
        public object TempData;
        public BattleServer BattleServer_;

        public void Dispose()
        {
            TempData = null;
            BattleServer_ = null;
        }
    }
    public class ServerBattleManager
    {
        WithLockList<ServerBattleWaitHandle> serverBattleWaitTempHandles = new();
        WithLockList<ServerBattleData> ServerBattleDatas = new();

        int WaitUsedTime = 5; // min
        // New ServerBattleData
        public async Task<BattleServer> NewBattle_Async(object data)
        {
            var b = new BattleServer();
            await UniTask.SwitchToMainThread();
            if (data is BattleSettings settings)
            {
                b.Init(settings);
            }
            else if (data is BattleData battleData)
            {
                b.Init(battleData);
            }
            else
            {
                await GameUtilityTool.DebugErrorAsync($"Invalid  type  {nameof(data)}");
                throw new ArgumentException("Invalid  type", nameof(data));
            }
            //battleList.Add(b);
         
            var SData =  ServerBattleData.New(b);
            await ServerBattleDatas.Add_Async(SData);
            return b;
        }

        public async Task<BattleServer> Find(string Uid)
        {
            var data = await ServerBattleDatas.Find_Async(Uid);
            if(data == null) { return null; }
            return data.battleServer;
        }
        public async void Remove(string Uid)
        {
            await ServerBattleDatas.Remove_Async(Uid);
        }
        public async Task<Rresult_BattleSerData> TryGet_GameStart(string Uid)
        {
            return await TryGet_Action(Uid, BattleServer.a_Start);
        }
        public async Task<Rresult_BattleSerData> TryGet_GameSelect(string Uid, List<BattleSelectOrder> orders)
        {
            return await TryGet_Action(Uid, BattleServer.GetAction_ReceiveSelect(orders), true);
        }
        public async Task<Rresult_BattleSerData> TryGet_GameEndToGetDrop(string Uid)
        {
            return await TryGet_Func(Uid, BattleServer.GetLevelDrop, true);
        }
        async Task<Rresult_BattleSerData> TryGet_Action(string Uid , Action<BattleServer> action , bool SwitchToMainThread = false)
        {
            var data = new Rresult_BattleSerData();
            var server = await Find(Uid);
            if (server == null) return data;    
            data.Success = true;
            data.BattleServer_ = server;
            if(SwitchToMainThread)  await UniTask.SwitchToMainThread();
            action?.Invoke(server);
        
            return data;
        }
        async Task<Rresult_BattleSerData> TryGet_Func(string Uid, Func<BattleServer,object> action, bool SwitchToMainThread = false)
        {
            var data = new Rresult_BattleSerData();
            var server = await Find(Uid);
            if (server == null) return data;
            data.Success = true;
            data.BattleServer_ = server;
            if (SwitchToMainThread) await UniTask.SwitchToMainThread();
            var d =action?.Invoke(server);
            data.TempData = d;
            return data;
        }
        #region HeartTime
        public void SetWaitUsedTime(int waitUsedTime) => WaitUsedTime = waitUsedTime;
        public async Task CheckBattleUsed() => await ServerBattleDatas.Action_Async(CheckHeart);

        public void CheckHeart()
        {
            List<ServerBattleData> Bs = null;
            foreach (var data in ServerBattleDatas.Datas)
            {

                if (data.battleServer.HeartbeatCheck(WaitUsedTime))
                {

                    if(Bs == null) Bs = new List<ServerBattleData>();
                    Bs.Add(data);
                }

            }
            if (Bs != null)
            {
                foreach (var battle in Bs)
                {
                    GameUtilityTool.DebugAsync(battle.Uid);
                    ServerBattleDatas.Datas.Remove(battle);
                    battle.Dispose();
                }
            }
            
        }
        #endregion

        /// <summary>
        /// If the data is expired, it will be temporarily stored here during the waiting stage.
        /// Get the information from the client side(trust the client)
        /// No regular cleaning is done.Only the ones in use are removed.Please note
        /// <summary>

        #region WaitHandle
        public async void AddHandle(ServerBattleWaitHandle handle)
        {
            await serverBattleWaitTempHandles.Add_Async(handle);
        }
        public async Task<ServerBattleWaitHandle> FindHandle(string Uid)
        {
            return await serverBattleWaitTempHandles.Find_Async(Uid,true);
        }
        public class ServerBattleWaitHandle : Idata, IDisposable
        {
            public Action<BattleServer> Action;
            public static ServerBattleWaitHandle New_Start(string Uid)
            {
                var S = new ServerBattleWaitHandle();
                S.Action = BattleServer.a_Start;
                //S.Action = (x) => x.Start();
                S.Uid = Uid;
                return S;

            }
            public static ServerBattleWaitHandle New_ReceiveSelect(string Uid, List<BattleSelectOrder> Ldata)
            {
                var S = new ServerBattleWaitHandle();
                S.data = Ldata;
                S.Uid = Uid;
                S.Action = BattleServer.GetAction_ReceiveSelect((List<BattleSelectOrder>)S.data);
                //S.Action = (x) => x.ReceiveSelect((List<BattleSelectOrder>)S.data);

                return S;

            }
            public async Task Excute(BattleServer battleServer , bool SwitchToMainThread = false)
            {
                if(SwitchToMainThread)
                await UniTask.SwitchToMainThread();
                Action?.Invoke(battleServer);
                Dispose();
            }

            public void Dispose()
            {
                Action = null;
                data = null;
                Uid = null;
            }
        }

        #endregion
        public class ServerBattleData : Idata ,IDisposable
        {
            public BattleServer battleServer { get; set; }

            public static ServerBattleData New(BattleServer server)
            {
                var data =  new ServerBattleData();
                data.battleServer = server;
                data.Uid = server.GameUid;
                return data;

            }

            public void Dispose()
            {
                battleServer.Dispose();
                battleServer = null;
                data = null;
                Uid = null;
            }
        }
        //protected void SendAction<T>(ushort type, T data, ulong TargetID, NetworkDelivery delivery = NetworkDelivery.Reliable) where T : INetworkSerializable
        //{
        //    FastBufferWriter writer = new FastBufferWriter(2048, Unity.Collections.Allocator.Temp, Network.Msg_size);
        //    writer.WriteValueSafe(type);
        //    writer.WriteNetworkSerializable(data);
        //    Network.Get().Messaging.Send(SendMsgTag, TargetID, writer, delivery);
        //    writer.Dispose();
        //}
    }
}
