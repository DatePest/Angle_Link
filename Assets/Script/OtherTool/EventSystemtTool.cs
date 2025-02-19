using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Cysharp.Threading.Tasks;

namespace EventSystemTool
{
    public  class EventSystem
    {
        private static Dictionary<Type, List<Action<IEventTag>>> eventHandlers = new Dictionary<Type, List<Action<IEventTag>>>();

        public static void Subscribe<T>(Action<IEventTag> handler) where T : IEventTag
        {
            Type eventType = typeof(T);

            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new List<Action<IEventTag>>();
            }

            eventHandlers[eventType].Add(handler);
        }

        public static void Unsubscribe<T>(Action<IEventTag> handler) where T : IEventTag
        {
            Type eventType = typeof(T);

            if (eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType].Remove(handler);
                if (eventHandlers[eventType].Count == 0)
                {
                    eventHandlers.Remove(eventType);
                    Console.WriteLine($"{eventType}_eventHandlers_Remove");
                }

            }
        }

        public static void Publish(IEventTag gameEvent)
        {
            Type eventType = gameEvent.GetType();

            if (eventHandlers.ContainsKey(eventType))
            {
                foreach (var handler in eventHandlers[eventType])
                {
                    handler.Invoke(gameEvent);
                }
            }
        }
    }

    public interface IEventTag { public ushort EventId { get; set; } }
    public abstract class I_Listener<T> where T : IEventTag
    {
        public void Start()
        {
            EventSystem.Subscribe<T>(Handler);
        }

        public abstract void Handler(IEventTag obj);

        public virtual void Stop(bool b = false)
        {
            EventSystem.Unsubscribe<T>(Handler);
        }
    }
    public class Listener<T> : I_Listener<T> where T : IEventTag
    {
        public Action<IEventTag> action;
        public Listener(Action<IEventTag> action = null)
        {
            if (action != null) this.action += action;
            Start();
        }
        public override void Handler(IEventTag obj)
        {
            action?.Invoke(obj);
        }

        public override void Stop(bool ClearEvent = false)
        {
            base.Stop();
            if (ClearEvent) action = null;
        }
    }


    public abstract class IEventBase<T> where T : EventTagAttribute
    {
        public Dictionary<ushort, MethodInfo> _Methods { get; protected set; } = new Dictionary<ushort, MethodInfo>();
        public IEventBase()
        {
            MethodInfo[] MyMethods = GetType().GetMethods();
            foreach (var a in MyMethods)
            {
                var T = a.GetCustomAttribute<T>();
                if (T != null)
                {
                    _Methods.TryAdd(T.EventId, a);
                }
            }
        }
        public MethodInfo Find(ushort e)
        {
            _Methods.TryGetValue(e, out var T);
            return T;
        }
        public void FindRun(ushort e)
        {
            var M = Find(e);
            if (M != null) { M.Invoke(this, new object[] {}); }
            else { Console.WriteLine($"Find ID = {M} is null"); }

        }
        public void FindRun(IEventTag e)
        {
            FindRun(e.EventId);
        }
        public void FindRun<D>(ushort e,D data)
        {
            var M = Find(e);
            if (M != null) { M.Invoke(this, new object[] { data }); }
            else { Console.WriteLine($"Find ID = {M} is null"); }

        }
        public async Task FindRunAsync<D>(ushort e, D data)
        {
            var M = Find(e);
            if (M == null)   { Console.WriteLine($"Find ID = {M} is null");  return; }
            bool isAsync = typeof(Task).IsAssignableFrom(M.ReturnType);

            if (isAsync)
            {
                var result = M.Invoke(this, new object[] { data });
                if (result != null && result is Task task)
                {
                    await task;
                }
            }
            else
            {
                M.Invoke(this, new object[] { data });
            }
        }
        public async UniTask FindRunAsync_UniTask<D>(ushort e, D data)
        {
            var M = Find(e);
            if (M == null) { Console.WriteLine($"Find ID = {M} is null"); return; }
            bool isAsync = typeof(UniTask).IsAssignableFrom(M.ReturnType);

            if (isAsync)
            {
                var result = M.Invoke(this, new object[] { data });
                if (result != null && result is UniTask task)
                {
                    await task;
                }
            }
            else
            {
                M.Invoke(this, new object[] { data });
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class EventTagAttribute : Attribute
    {
        public ushort EventId;

        public EventTagAttribute(ushort TagId)
        {
            EventId = TagId;
        }
    }

}
