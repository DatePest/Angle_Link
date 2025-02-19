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
        public async Task<bool> Updata(string PackageName, Action callback = null)
        {
            return await YooAsset_Tool.InitPackageAsync(playMode, PackageName);
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

        public async Task<bool> InitUpdata(ClientPackageSetting packageSetting)
        {
            if (PackageInitialize)
            {
                Debug.LogError("Initialized");
                return true;
            }

            foreach (string name in packageSetting.PackageNames)
            {
                var T = await Updata(name);
                if (T)
                {
                    Debug.Log($"InitAssPackage {name} ");
                }
                else
                {
                    Debug.LogError($"Pack {name} Error");
                    return false;
                }
            }
            InitCompleted();
            Debug.Log("InitAssPackage  All End ");
            //await InitUI();
            return true;
        }

        //Not used anymore for some reason
        async Task InitUI()
        {
            string pack = "GameCore";
            var tc = await TopCanvas.Create_YooAsset(pack, "TopCanvas");
            await Ui_LoadIng.Create_YooAsset(pack, "Ui_LoadIng", tc.transform);
            await Ui_SystemMsg.Create_YooAsset(pack, "SystemMsg", tc.transform);
            await UI_SceneLoad.Create_YooAsset(pack, "SceneLoad", tc.transform);

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