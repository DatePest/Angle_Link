using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WithLockQueueTool
{
    public abstract class IEventHandle
    {
        public MethodInfo method { get; protected set; }
        public object MethodInstance { get; protected set; }
        public Action CallBack;

        public IEventHandle(MethodInfo method, object methodInstance)
        {
            this.method = method;
            MethodInstance = methodInstance;
        }

        public abstract void Excute();
    }
    public  class Idata
    {
        public string Uid;
        public object data;
    }
    public class WithLockQueue<T> : IDisposable where T : IEventHandle
    {
        LockTool lockTool = new LockTool();
        public Queue<T> Handles { get; private set; } = new();

        public async Task Excute(bool SwitchToMainThread = false)
        {
            if (Handles.Count > 0)
                await lockTool.TryExecuteWithLockAsync(Send,SwitchToMainThread);
        }

        void Send()
        {
            while (Handles.Count > 0)
            {
                var evt = Handles.Dequeue();
                evt.Excute();
                evt.CallBack?.Invoke();
            }
        }
        public async Task Clear()
        {
            if (Handles == null) return;
            await lockTool.TryExecuteWithLockAsync( () => Handles.Clear());
        }
        public async Task Add(T data)
        {
            if (Handles == null ) return;
            await lockTool.TryExecuteWithLockAsync( () => Handles.Enqueue(data));
        }

   
        public void Dispose()
        {
            Handles = null;
            lockTool.Dispose();
            lockTool = null;
        }
    }
    public class WithLockList<T> : IDisposable where T : Idata
    {
        public List<T> Datas { get; private set; } = new();
        LockTool lockTool = new ();
        public async Task Action_Async(Action action, bool SwitchToMainThread = false)
        {
            if (Datas == null) return;
            await lockTool.TryExecuteWithLockAsync(action, SwitchToMainThread);
        }
        public async Task Add_Async(T data)
        {
            if (Datas == null ) return;
            await lockTool.TryExecuteWithLockAsync(()=> Datas.Add(data));
        }
        public async Task<T> Find_Async(string Uid,bool Remove = false)
        {
            if (Datas == null ) return null;
            return  await lockTool.TryExecuteWithLockAsync(() => FindHandles(Uid, Remove));
        }
        public async Task Remove_Async(string Uid)
        {
            if (Datas == null) return;
            await lockTool.TryExecuteWithLockAsync(() => FindHandles(Uid, true));
        }
        T FindHandles(string Uid , bool Remove = false)
        {
            T data = null;  
            foreach (var b in Datas)
            {
                if (b.Uid == Uid)
                {
                    data = b;
                    break;
                }
            }
            if (Remove)
                Datas.Remove(data);

            return data;
        }
        public void Dispose()
        {
            Datas.Clear();
            Datas = null;
            lockTool.Dispose();
            lockTool = null;
        }
    }
    public class LockTool : IDisposable
    {
        object QueueLock = new object(); 
        bool _isDisposed = false;

        public void Dispose()
        {
            if (_isDisposed) return;
            QueueLock = null;
            _isDisposed = true;
        }

        public async Task TryExecuteWithLockAsync( Action action, bool SwitchToMainThread = false)
        {
            if(QueueLock == null) return; 

            bool lockAcquired = false;

            while (!lockAcquired)
            {
                lockAcquired = Monitor.TryEnter(QueueLock);

                if (lockAcquired)
                {
                    try
                    {
                        if(SwitchToMainThread)
                        await UniTask.SwitchToMainThread();
                        action();
                    }
                    finally
                    {
                        Monitor.Exit(QueueLock);
                    }
                }
                else
                {
                    await UniTask.NextFrame();
                    //await UniTask.Yield();
                }
            }

        }
        public async Task<T> TryExecuteWithLockAsync<T>(Func<T> func, bool SwitchToMainThread = false) 
        {
            if (_isDisposed) return default;

            bool lockAcquired = false;

            while (!lockAcquired)
            {
                lockAcquired = Monitor.TryEnter(QueueLock);

                if (lockAcquired)
                {
                    try
                    {
                        if (SwitchToMainThread)
                            await UniTask.SwitchToMainThread();
                        return func();
                    }
                    finally
                    {
                        Monitor.Exit(QueueLock);
                    }
                }
                else
                {
                    //await UniTask.NextFrame();
                    await UniTask.Yield();
                }
            }
            return default;
        }
    }
}


