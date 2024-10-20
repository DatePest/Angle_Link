using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using UnityEngine;
using YooAsset;
using System.Collections;
using System.Reflection;

namespace Client
{
    public class ClientAsset
    {
        public ClientAsset(EPlayMode ePlayMode)
        {
            playMode = ePlayMode;
        }
        EPlayMode playMode;
        public List<Assembly> Assemblies { get; private set; } = new List<Assembly>();
        public bool PackageInitialize { get; private set; } = false;
        public void InitCompleted() => PackageInitialize = true;
        public WaitActionHandle Updata(string PackageName, Action callback = null)
        {
            var handle = new WaitActionHandle();
            handle.SetRun(YooAsset_Tool.InitPackage(playMode, PackageName), callback);
            return handle;
        }
        public void StartLoadDll(string packageName, string name, Action callback = null)
        {
#if UNITY_EDITOR
            // Editor下无需加载，直接查找获得HotUpdate程序集
            Assembly hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "GameHotDll");
            if (hotUpdateAss != null) Assemblies.Add(hotUpdateAss);
            return;
#else
            if (callback != null) 
                TimeTool.StartCoroutine(GetDllData(packageName, name), callback);
            else
                TimeTool.StartCoroutine(GetDllData(packageName, name));
#endif
        }
        IEnumerator GetDllData(string packageName, string name)
        {
            Debug.Log($"Try LoadDll {name} ");
            var package = YooAssets.GetPackage(packageName);
            AssetHandle handle = package.LoadAssetAsync<TextAsset>(name);
            yield return handle;
            var bytes = handle.GetAssetObject<TextAsset>().bytes;
            try
            {
                Assembly ass = Assembly.Load(bytes);
                Debug.Log($"Assembly Load Success");
                if (!Assemblies.Contains(ass)) Assemblies.Add(ass);
            }
            catch (Exception ex)
            {
                Debug.LogError("LoadDll Error :" + ex.Message); ;
            }
            package.UnloadUnusedAssetsAsync();
        }

        public async Task InitUpdata(ClientPackageSetting packageSetting)
        {
            if (PackageInitialize)
            {
                Debug.LogError("Initialized");
                return;
            }

            foreach (string name in packageSetting.PackageNames)
            {
                var T = Updata(name);
                while (!T.completed) { await UniTask.Yield(PlayerLoopTiming.Update); }
                Debug.Log($"InitAssPackage {name} ");
            }
            InitCompleted();
            Debug.Log("InitAssPackage  All End ");
            await InitUI();
            return;
        }
        async Task InitUI()
        {
            string pack = "GameCore";
            var tc = await TopCanvas.Create(pack, "TopCanvas");
            await Ui_LoadIng.Create(pack, "Ui_LoadIng", tc.transform);
            await Ui_SystemMsg.Create(pack, "SystemMsg", tc.transform);
            await UI_SceneLoad.Create(pack, "SceneLoad", tc.transform);

            Debug.Log("Init UI  End ");
        }


        #region WaitPackageInitializedAsync
        public async Task<ResourcePackage> SafeGetPackage(string name)
        {
            await WaitPackageInitializedAsync();
            return YooAssets.GetPackage(name);
        }
        public async Task WaitPackageInitializedAsync()
        {
            while (!PackageInitialize)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
        public IEnumerator WaitPackageInitialized()
        {
            while (!PackageInitialize)
            {
                yield return null;
            }
            yield return new WaitForFixedUpdate();
        }
        #endregion

    }
}