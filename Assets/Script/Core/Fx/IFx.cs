using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Script.Core.Fx
{
    public abstract class IFx : IDisposable
    {
        protected Animation mAnimation;

        public virtual void RegisterSpeed(ref Action<float> action)
        {
            action += (f) => GameUtilityTool.AnimeSpeed(f, mAnimation);
        }
        public virtual void Dispose()
        {
            mAnimation = null;
        }
        protected abstract void Init(GameObject g);

        public virtual UniTask Excute(object obj1)
        {
            throw new NotImplementedException();
        }
        public virtual UniTask Excute(object obj1, object obj2)
        {
            throw new NotImplementedException();
        }
        public virtual UniTask Excute(object obj1, object obj2, object obj3)
        {
            throw new NotImplementedException();
        }
    }
}

