using Client;
using UnityEngine;
using TMPro;
using HybridCLR;
using System.Collections.Generic;
using System.Linq;
using YooAsset;
using System.Threading.Tasks;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class InitializeRoot : MonoBehaviour
{
    public TextMeshProUGUI proUGUI;

    public ITestData testData;
    async void Start()
    {
        Debug.Log(Application.persistentDataPath);
        InitUI();
        await RootStert();
    }

    public static async Task RootStert()
    {
        var c = ClientRoot.Get();
        Custom_reg.Reg_NetWait();
        if (await c.StartInitUpdata())
            AssLoad();
    }
    void  InitUI()
    {
        var tc =  TopCanvas.Create_Resources("OtherPrefab/TopCanvas");
         Ui_LoadIng.Create_Resources("OtherPrefab/Ui_LoadIng", tc.transform);
         Ui_SystemMsg.Create_Resources("OtherPrefab/SystemMsg", tc.transform);
         UI_SceneLoad.Create_Resources("OtherPrefab/SceneLoad", tc.transform);
        NetDownloadProgress.Create_Resources("OtherPrefab/NetDownloadProgress", tc.transform);
       
        Debug.Log("Init UI  End ");
    }

    public static  async void AssLoad()
    {
#if !UNITY_EDITOR
        var c = ClientRoot.Get();
        await c.StartInitUpdata();
        c.clientAsset.StartLoadDll
            ("GameDll", "GameHotDll.dll",
            () => TimeTool.StartCoroutine(LoadMetadataForAOTAssembly("GameDll", AOTGenericReferences.PatchedAOTAssemblyList),GotoLogin)
            ) ;
#else
        GotoLogin();
#endif
    }

    static IEnumerator LoadMetadataForAOTAssembly(string packageName, IReadOnlyList<string> patchedAOTAssemblyList)
    {
        List<string> aotDllList = new List<string>
        {
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll", // 如果使用了Linq，需要这个
        };

        aotDllList = aotDllList.Union(patchedAOTAssemblyList).ToList();
        Debug.Log($"Load Count {aotDllList.Count}");
        var package = YooAssets.GetPackage(packageName);
      
        foreach (var aotDllName in aotDllList)
        {
            Debug.Log($"Try Load  {aotDllName}");
            AssetHandle handle = package.LoadAssetAsync<TextAsset>(aotDllName);
            yield return handle;
            byte[] dllBytes = handle.GetAssetObject<TextAsset>().bytes;
            var err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
            yield return err;
            Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. ret:{err}");
        }
        Debug.Log($"LoadMetadataForAOTAssembly Finish ");
    }

    static async void GotoLogin()
    {
#if UNITY_EDITOR
        GameObject.FindFirstObjectByType<InitializeRoot>().testData?.Excute();
#endif

        while (UI_SceneLoad.Get()== null)
        {
            await UniTask.DelayFrame(5);
        }
        Debug.Log($"GotoLogin");

        FindAnyObjectByType<DebugLog>().Clear();
        UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Login.ToString(), GameConstant.PackName_GameCore, LoadSceneMode.Single,true,20f);
    }
}
[System.Serializable]
public abstract class ITestData : MonoBehaviour 
{
    public abstract void Excute();
}

