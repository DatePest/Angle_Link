using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

public class YooAsset_Tool
{
    //public void GoLoadDll(string PackageName, string tag)
    //{
    //    var package = YooAssets.GetPackage(PackageName);
    //    //热更新Dll名称
    //    var Allassets = new List<string>
    //    {
    //    "GameCore.dll",
    //    };
    //    .Concat(AOTMetaAssemblyNames);
    //    foreach (var asset in Allassets)
    //    {
    //        var handle = package.LoadRawFileSync(asset);
    //        byte[] fileData = handle.GetRawFileData();
    //        //s_assetDatas[asset] = fileData;
    //        Debug.Log($"dll:{asset} size:{fileData.Length}");
    //    }
    //}
    public static string FileUrl_1 { get; private set; }
    public static string FileUrl_2 { get; private set; }
    public static void SerRemoteServicesUrl(string fileUrl_1, string fileUrl_2)
    {
        FileUrl_1 = fileUrl_1;
        FileUrl_2 = fileUrl_2;
    }

    public static IEnumerator InitPackage(EPlayMode playMode, string packageName)
    {

        // 创建资源包裹类
        var package = YooAssets.TryGetPackage(packageName);
        if (package == null)
            package = YooAssets.CreatePackage(packageName);
        YooAssets.SetDefaultPackage(package);
        switch (playMode)
        {
            case EPlayMode.EditorSimulateMode:
                yield return InitializeYooAsset_Editor(package); //Editor
                break;
            case EPlayMode.OfflinePlayMode:
                yield return InitializeYooAsset_OfflinePlay(package); // 单机运行模式
                break;
            case EPlayMode.HostPlayMode:
                yield return InitializeYooAsset_HostPlay(package);  // 联机运行模式
                break;
            case EPlayMode.WebPlayMode:
                yield return InitializeYooAsset_WebPlayMode(package);
                break;

        }
        PackageVersion version = new PackageVersion(); ;
        //获取资源版本
        Debug.Log($"{packageName} UpdatePackageVersion");
        yield return RequestPackageVersion(package, version);
        //更新资源清单
        Debug.Log("UpdatePackageManifest");
        yield return UpdatePackageManifest(package, version);
        //下载
        Debug.Log("Download");
        yield return Download(packageName);
        Debug.Log("ClearAllCacheFile");
        yield return ClearPackageAllCacheFiles(package);
    }
    private static IEnumerator InitializeYooAsset_HostPlay(ResourcePackage package)
    {

        IRemoteServices remoteServices = new RemoteServices(package.PackageName);
        var cacheFileSystem = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
        var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
        var initParameters = new HostPlayModeParameters();
        initParameters.BuildinFileSystemParameters = buildinFileSystem;
        initParameters.CacheFileSystemParameters = cacheFileSystem;
        var initOperation = package.InitializeAsync(initParameters);
        yield return initOperation;

        if (initOperation.Status == EOperationStatus.Succeed)
            Debug.Log($"Initialize {package.PackageName} Succeed！");
        else
            Debug.LogError($"InitializeYooAsset initOperation Error ：{initOperation.Error}");
    }
    private static IEnumerator InitializeYooAsset_WebPlayMode(ResourcePackage package)
    {
        new System.Exception("is not implemented");

        var webFileSystem = FileSystemParameters.CreateDefaultWebFileSystemParameters();
        var initParameters = new WebPlayModeParameters();
        initParameters.WebFileSystemParameters = webFileSystem;
        var initOperation = package.InitializeAsync(initParameters);
        yield return initOperation;
        if (initOperation.Status == EOperationStatus.Succeed)
            Debug.Log($"Initialize {package.PackageName} Succeed！");
        else
            Debug.LogError($"InitializeYooAsset initOperation Error ：{initOperation.Error}");
    }
    private static IEnumerator InitializeYooAsset_Editor(ResourcePackage package)
    {
        var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
        var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, package.PackageName);
        var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
        var initParameters = new EditorSimulateModeParameters();
        initParameters.EditorFileSystemParameters = editorFileSystem;
        var initOperation = package.InitializeAsync(initParameters);
        yield return initOperation;
        if (initOperation.Status == EOperationStatus.Succeed)
            Debug.Log($"Initialize {package.PackageName} Succeed！");
        else
            Debug.LogError($"InitializeYooAsset initOperation Error ：{initOperation.Error}");
    }

    private static IEnumerator InitializeYooAsset_OfflinePlay(ResourcePackage package)
    {
        var initParameters = new OfflinePlayModeParameters();
        yield return package.InitializeAsync(initParameters);
    }


    private static IEnumerator RequestPackageVersion(ResourcePackage package, PackageVersion packageVersion)
    {
        RequestPackageVersionOperation operation = package.RequestPackageVersionAsync();
        yield return operation;

        if (operation.Status == EOperationStatus.Succeed)
        {
            packageVersion.Version = operation.PackageVersion;
            Debug.Log($"Request package Version : {packageVersion.Version}");
        }
        else
        {
            Debug.LogError("Error" + operation.Error);
        }
    }
    class PackageVersion { public string Version; }
    private static IEnumerator UpdatePackageManifest(ResourcePackage package, PackageVersion version)
    {
        // 更新成功后自动保存版本号，作为下次初始化的版本。
        // 也可以通过operation.SavePackageVersion()方法保存。
        // bool savePackageVersion = true;
        var operation = package.UpdatePackageManifestAsync(version.Version);
        yield return operation;

        if (operation.Status == EOperationStatus.Succeed)
        {
            //Succed
        }
        else
        {
            //Error
            Debug.LogError(operation.Error);
        }
    }
    static IEnumerator Download(string PackageName)
    {
        int downloadingMaxNum = 10;
        int failedTryAgain = 3;
        var package = YooAssets.GetPackage(PackageName);
        var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);

        //没有需要下载的资源
        if (downloader.TotalDownloadCount == 0)
        {
            yield break;
        }

        //需要下载的文件总数和总大小
        int totalDownloadCount = downloader.TotalDownloadCount;
        long totalDownloadBytes = downloader.TotalDownloadBytes;

        //注册回调方法
        //downloader.OnDownloadErrorCallback = OnDownloadErrorFunction;
        //downloader.OnDownloadProgressCallback = OnDownloadProgressUpdateFunction;
        //downloader.OnDownloadOverCallback = OnDownloadOverFunction;
        //downloader.OnStartDownloadFileCallback = OnStartDownloadFileFunction;

        //开启下载
        downloader.BeginDownload();
        yield return downloader;

        //检测下载结果
        if (downloader.Status == EOperationStatus.Succeed)
        {
            //下载成功
        }
        else
        {
            //下载失败
        }
    }

    public static List<T> GetGameDatas<T>(string PackageName, string Tag) where T : UnityEngine.Object
    {
        var p = YooAssets.GetPackage(PackageName);
        var As = p.GetAssetInfos(Tag);
        var Sp = new List<T>();
        foreach (var a in As)
        {
            var s = p.LoadAssetSync<T>(a.AssetPath).GetAssetObject<T>();
            Sp.Add(s);
        }

        return Sp;

    }

    //private static void OnDownloadOverFunction(bool isSucceed)
    //{
    //    Debug.Log("OnDownloadOverFunction");
    //    var e = new InitGame.DownloaderStateEvent();
    //    e.DownloaderState = true;
    //    EventSystemCore.Publish(e);
    //}
    //private static void OnDownloadErrorFunction(string fileName, string error)
    //{
    //    var e = new InitGame.DownloaderStateEvent();
    //    e.DownloaderState = false;
    //    EventSystemCore.Publish(e);
    //}
    public static async Task UnloadUnusedAssets(string packagname)
    {
        var package = YooAssets.GetPackage(packagname);
        var operation = package.UnloadUnusedAssetsAsync();
        if(operation.Status != EOperationStatus.Succeed) await UniTask.Yield();
        return;
    }
    public static T GetPackageData<T>(string packagname, string name) where T : UnityEngine.Object
    {
        var p = YooAssets.GetPackage(packagname);
        if (p == null) return null;
        var a = p.LoadAssetSync<T>(name).GetAssetObject<T>();
        return  a;
    }
    public static T GetPackageData<T>(string packagname, string tag, string name) where T : UnityEngine.Object
    {
        var p = YooAssets.GetPackage(packagname);
        if (p == null) return null;

        var t = p.GetAssetInfos(tag);
        foreach (var a in t)
        {
            if (a.AssetPath.Contains(name))
            {
#if UNITY_EDITOR
                Debug.Log(a.AssetPath);
#endif  
                return p.LoadAssetAsync<T>(a.AssetPath).GetAssetObject<T>();

            }
        }
        return null;
    }

    //清理文件系统所有的缓存资源文件
    static IEnumerator ClearPackageAllCacheFiles(ResourcePackage package)
    {
        var operation = package.ClearAllBundleFilesAsync();
        yield return operation;

        if (operation.Status == EOperationStatus.Succeed)
        {
        }
        else
        {
            Debug.LogError(operation.Error);
        }
    }
    //清理文件系统未使用的缓存资源文件
    static IEnumerator ClearPackageUnusedCacheFiles(ResourcePackage package)
    {
        var operation = package.ClearUnusedBundleFilesAsync();
        yield return operation;

        if (operation.Status == EOperationStatus.Succeed)
        {
        }
        else
        {
            Debug.LogError(operation.Error);
        }
    }
    public class RemoteServices : IRemoteServices
    {
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteServices(string package)
        {
            _defaultHostServer = FileUrl_1 + package;
            _fallbackHostServer = FileUrl_1 + package;
        }
        string IRemoteServices.GetRemoteMainURL(string fileName)
        {
            return $"{_defaultHostServer}/{fileName}";
        }
        string IRemoteServices.GetRemoteFallbackURL(string fileName)
        {
            return $"{_fallbackHostServer}/{fileName}";
        }
    } 
}
