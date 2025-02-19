using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;
using Cysharp.Threading.Tasks;

public class SingletonTool
{
    public interface Sig<T> where T : class, new()
    {
        static T instance;
        public static T Get()
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    public abstract class SigMono<T> : MonoBehaviour
    {
        static T instance;
        public static T Get()
        {
            if (instance == null)
            {
                Debug.LogError($"{typeof(T).ToString()} is null}}");
                var t = FindAnyObjectByType(typeof(T));
                if (t == null)
                {
                    return default;
                }
                var g = (GameObject)t;
                instance = g.GetComponent<T>();
                return instance;
            }
            return instance;
        }
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = GetComponent<T>();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        protected virtual void OnDestroy()
        {
            if (instance != null && instance.Equals(this)) instance = default;
        }
        public static async Task<T> Create_YooAsset(string packagname, string name, Transform transform = null)
        {
            var op =  YooAssets.GetPackage(packagname).LoadAssetAsync<GameObject>(name).InstantiateAsync(transform);
            while (op.Status!= EOperationStatus.Succeed && op.Result==null)
            {
                //Debug.Log($"{name} {op.Status} __+ {op.Result == null} ");
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            Debug.Log($"Success Creat {name}  ");
            return op.Result.GetComponent<T>();
        }
        public static T Create_Resources(string Path, Transform transform = null)
        {
            var op = Resources.Load<GameObject>(Path);
            Debug.Log(op == null);
            var tar =GameObject.Instantiate(op, transform);
            Resources.UnloadUnusedAssets();
            return tar.GetComponent<T>();
        }
    }

}