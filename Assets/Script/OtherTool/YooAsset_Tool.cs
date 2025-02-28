using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YooAsset;

public class YooAsset_Tool
{
    
    public static string FileUrl_1 { get; private set; }
    public static string FileUrl_2 { get; private set; }
    public static void SerRemoteServicesUrl(string fileUrl_1, string fileUrl_2)
    {
        FileUrl_1 = fileUrl_1;
        FileUrl_2 = fileUrl_2;
    }
    class PackageVersion
    {
        public string Version;
    }
    #region  InitPackageAsync
    public static async Task<bool> InitPackageAsync(EPlayMode playMode, string packageName)
    {
        if (!YooAssets.Initialized)
            YooAssets.Initialize();

        // 創建資源包裹類
        var package = YooAssets.TryGetPackage(packageName) ?? YooAssets.CreatePackage(packageName);
        YooAssets.SetDefaultPackage(package);
        bool succeed = false;
        switch (playMode)
        {
            case EPlayMode.EditorSimulateMode:
                succeed =await InitializeYooAsset_EditorAsync(package); // Editor 模式
                break;
            case EPlayMode.OfflinePlayMode:
                await InitializeYooAsset_OfflinePlayAsync(package); // 單機運行模式
                succeed = true;
                break;
            case EPlayMode.HostPlayMode:
                succeed = await InitializeYooAsset_HostPlayAsync(package); // 聯機運行模式
                break;
            case EPlayMode.WebPlayMode:
                succeed = await InitializeYooAsset_WebPlayModeAsync(package); // 網絡模式
                break;
        }
        if (!succeed) return false;
        var version = new PackageVersion();
        // 獲取資源版本
        Debug.Log($"{packageName} UpdatePackageVersion");
        succeed =await RequestPackageVersionAsync(package, version);
        if (!succeed) return false;
        // 更新資源清單
        Debug.Log("UpdatePackageManifest");
        succeed =await UpdatePackageManifestAsync(package, version);
        if (!succeed) return false;
        // 下載
        Debug.Log("Download");
        succeed =await DownloadAsync(packageName);
        if (!succeed) return false;
        // 清理緩存文件
        Debug.Log("ClearAllCacheFile");
        succeed = await ClearPackageUnusedCacheFilesAsync(package);
        if (!succeed) return false;
        return true;
    }
    #region Mode
    private static async Task<bool> InitializeYooAsset_HostPlayAsync(ResourcePackage package)
    {
        IRemoteServices remoteServices = new RemoteServices(package.PackageName);
        var cacheFileSystem = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
        var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
        var initParameters = new HostPlayModeParameters
        {
            //BuildinFileSystemParameters = buildinFileSystem,
            BuildinFileSystemParameters = null,  // Loaal Not Package File Use
            CacheFileSystemParameters = cacheFileSystem
        };

        var initOperation = package.InitializeAsync(initParameters);
        await initOperation.Task;

        return GetInitializeOperationStatus(package, initOperation);
    }

    private static async Task<bool> InitializeYooAsset_WebPlayModeAsync(ResourcePackage package)
    {
        var webFileSystem = FileSystemParameters.CreateDefaultWebFileSystemParameters();
        var initParameters = new WebPlayModeParameters
        {
            WebFileSystemParameters = webFileSystem
        };

        var initOperation = package.InitializeAsync(initParameters);
        await initOperation.Task;

        return GetInitializeOperationStatus(package, initOperation);

    }

    private static async Task<bool> InitializeYooAsset_EditorAsync(ResourcePackage package)
    {
        var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
        var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, package.PackageName);
        var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
        var initParameters = new EditorSimulateModeParameters
        {
            EditorFileSystemParameters = editorFileSystem
        };

        var initOperation = package.InitializeAsync(initParameters);
        await initOperation.Task;

        return GetInitializeOperationStatus(package, initOperation);
    }
    static bool GetInitializeOperationStatus(ResourcePackage package ,InitializationOperation Operation)
    {
        if (Operation.Status == EOperationStatus.Succeed)
        {
            Debug.Log($"Initialize {package.PackageName} Succeed！");
            return true;
        }
        else
        {
            Debug.LogError($"InitializeYooAsset initOperation Error ：{Operation.Error}");
            return false;
        }
    }
    private static async Task InitializeYooAsset_OfflinePlayAsync(ResourcePackage package)
    {
        var initParameters = new OfflinePlayModeParameters();
        var initOperation = package.InitializeAsync(initParameters);
        await initOperation.Task;
    }
    #endregion
    private static async Task<bool> RequestPackageVersionAsync(ResourcePackage package, PackageVersion packageVersion)
    {
        var operation = package.RequestPackageVersionAsync();
        await operation.Task;

        if (operation.Status == EOperationStatus.Succeed)
        {
            packageVersion.Version = operation.PackageVersion;
            Debug.Log($"Request package Version : {packageVersion.Version}");
            return true ;
        }
        else
        {
            Debug.LogError("Error: " + operation.Error);
            return false;
        }
    }

    private static async Task<bool> UpdatePackageManifestAsync(ResourcePackage package, PackageVersion version)
    {
        var operation = package.UpdatePackageManifestAsync(version.Version);
        await operation.Task;

        if (operation.Status == EOperationStatus.Succeed)
        {
            Debug.Log("Package manifest updated successfully.");
            return true;
        }
        else
        {
            Debug.LogError($"Error updating package manifest: {operation.Error}");
            return false;
        }
    }
    public static event Func<Task<bool>> OnConfirmStartDownload;
    public static async Task<bool> StartDownloadAsync(DownloaderOperation downloader)
    {
        // 如果有人訂閱確認事件，則執行確認邏輯
        if (OnConfirmStartDownload != null)
        {
            bool isConfirmed = await OnConfirmStartDownload.Invoke();
            if (!isConfirmed)
            {
                Debug.Log("用戶取消下載，下載已中止。");
                downloader.CancelDownload();
                return false;
            }
        }

        downloader.BeginDownload();
        await downloader.Task;
        return true;
    }
  
    private static async Task<bool> DownloadAsync(string packageName)
    {
        int downloadingMaxNum = 10;
        int failedTryAgain = 3;
        var package = YooAssets.GetPackage(packageName);
        DownloaderOperation downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);

        if (downloader.TotalDownloadCount == 0)
            return true;

        Debug.Log($"Downloading {downloader.TotalDownloadCount} files ({downloader.TotalDownloadBytes} bytes)...");

        downloader.OnDownloadErrorCallback = DownloadError;
        downloader.OnDownloadProgressCallback = DownloadProgressUpdate;
        downloader.OnDownloadOverCallback = DownloadOver;
        downloader.OnStartDownloadFileCallback = StartDownloadFile;

        if (!await StartDownloadAsync(downloader))
            return false;
       

        if (downloader.Status == EOperationStatus.Succeed)
        {
            Debug.Log("Download completed successfully.");
            return true;
        }
        else
        {
            Debug.LogError("Download failed.");
            return false;
        }
    }

    // 清理文件系统所有的缓存资源文件
    public static async Task ClearPackageAllCacheFilesAsync(string packageName)
    {
        var package = YooAssets.GetPackage("DefaultPackage");
        var operation =  package.ClearAllBundleFilesAsync();
        await operation;
        if (operation.Status == EOperationStatus.Succeed)
        {
            //清理成功
        }
        else
        {
            //清理失败
        }
    }
    static async Task<bool> ClearPackageAllCacheFilesAsync(ResourcePackage package)
    {
        var operation = package.ClearAllBundleFilesAsync();
        await operation.Task;

        if (operation.Status == EOperationStatus.Succeed)
        {
            Debug.Log($"Cleared all cached bundle files for package: {package.PackageName}");
            return true;
        }
        else
        {
            Debug.LogError($"Error clearing all bundle files: {operation.Error}");
            return false;
        }
    }

    // 清理文件系统未使用的缓存资源文件
    static async Task<bool> ClearPackageUnusedCacheFilesAsync(ResourcePackage package)
    {
        var operation = package.ClearUnusedBundleFilesAsync();
        await operation.Task;

        if (operation.Status == EOperationStatus.Succeed)
        {
            Debug.Log($"Cleared unused cached bundle files for package: {package.PackageName}");
            return true;
        }
        else
        {
            Debug.LogError($"Error clearing unused bundle files: {operation.Error}");
            return false;
        }
    }
    #endregion
    #region old IEnumerator
    //public static IEnumerator InitPackage(EPlayMode playMode, string packageName)
    //{
    //    if(!YooAssets.Initialized)
    //        YooAssets.Initialize();
    //    // 创建资源包裹类
    //    var package = YooAssets.TryGetPackage(packageName);
    //    if (package == null)
    //        package = YooAssets.CreatePackage(packageName);
    //    YooAssets.SetDefaultPackage(package);
    //    switch (playMode)
    //    {
    //        case EPlayMode.EditorSimulateMode:
    //            yield return InitializeYooAsset_Editor(package); //Editor
    //            break;
    //        case EPlayMode.OfflinePlayMode:
    //            yield return InitializeYooAsset_OfflinePlay(package); // 单机运行模式
    //            break;
    //        case EPlayMode.HostPlayMode:
    //            yield return InitializeYooAsset_HostPlay(package);  // 联机运行模式
    //            break;
    //        case EPlayMode.WebPlayMode:
    //            yield return InitializeYooAsset_WebPlayMode(package);
    //            break;

    //    }
    //    PackageVersion version = new PackageVersion(); ;
    //    //获取资源版本
    //    Debug.Log($"{packageName} UpdatePackageVersion");
    //    yield return RequestPackageVersion(package, version);
    //    //更新资源清单
    //    Debug.Log("UpdatePackageManifest");
    //    yield return UpdatePackageManifest(package, version);
    //    //下载
    //    Debug.Log("Download");
    //    yield return Download(packageName);
    //    Debug.Log("ClearAllCacheFile");
    //    yield return ClearPackageAllCacheFiles(package);
    //}
    //private static IEnumerator InitializeYooAsset_HostPlay(ResourcePackage package)
    //{

    //    IRemoteServices remoteServices = new RemoteServices(package.PackageName);
    //    var cacheFileSystem = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
    //    var buildinFileSystem = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
    //    var initParameters = new HostPlayModeParameters();
    //    initParameters.BuildinFileSystemParameters = buildinFileSystem;
    //    initParameters.CacheFileSystemParameters = cacheFileSystem;
    //    var initOperation = package.InitializeAsync(initParameters);
    //    yield return initOperation;

    //    if (initOperation.Status == EOperationStatus.Succeed)
    //        Debug.Log($"Initialize {package.PackageName} Succeed！");
    //    else
    //        Debug.LogError($"InitializeYooAsset initOperation Error ：{initOperation.Error}");
    //}
    //private static IEnumerator InitializeYooAsset_WebPlayMode(ResourcePackage package)
    //{
    //    new System.Exception("is not implemented");

    //    var webFileSystem = FileSystemParameters.CreateDefaultWebFileSystemParameters();
    //    var initParameters = new WebPlayModeParameters();
    //    initParameters.WebFileSystemParameters = webFileSystem;
    //    var initOperation = package.InitializeAsync(initParameters);
    //    yield return initOperation;
    //    if (initOperation.Status == EOperationStatus.Succeed)
    //        Debug.Log($"Initialize {package.PackageName} Succeed！");
    //    else
    //        Debug.LogError($"InitializeYooAsset initOperation Error ：{initOperation.Error}");
    //}
    //private static IEnumerator InitializeYooAsset_Editor(ResourcePackage package)
    //{
    //    var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
    //    var simulateBuildResult = EditorSimulateModeHelper.SimulateBuild(buildPipeline, package.PackageName);
    //    var editorFileSystem = FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
    //    var initParameters = new EditorSimulateModeParameters();
    //    initParameters.EditorFileSystemParameters = editorFileSystem;
    //    var initOperation = package.InitializeAsync(initParameters);
    //    yield return initOperation;
    //    if (initOperation.Status == EOperationStatus.Succeed)
    //        Debug.Log($"Initialize {package.PackageName} Succeed！");
    //    else
    //        Debug.LogError($"InitializeYooAsset initOperation Error ：{initOperation.Error}");
    //}

    //private static IEnumerator InitializeYooAsset_OfflinePlay(ResourcePackage package)
    //{
    //    var initParameters = new OfflinePlayModeParameters();
    //    yield return package.InitializeAsync(initParameters);
    //}


    //private static IEnumerator RequestPackageVersion(ResourcePackage package, PackageVersion packageVersion)
    //{
    //    RequestPackageVersionOperation operation = package.RequestPackageVersionAsync();
    //    yield return operation;

    //    if (operation.Status == EOperationStatus.Succeed)
    //    {
    //        packageVersion.Version = operation.PackageVersion;
    //        Debug.Log($"Request package Version : {packageVersion.Version}");
    //    }
    //    else
    //    {
    //        Debug.LogError("Error" + operation.Error);
    //    }
    //}
    //class PackageVersion { public string Version; }
    //private static IEnumerator UpdatePackageManifest(ResourcePackage package, PackageVersion version)
    //{
    //    // 更新成功后自动保存版本号，作为下次初始化的版本。
    //    // 也可以通过operation.SavePackageVersion()方法保存。
    //    // bool savePackageVersion = true;
    //    var operation = package.UpdatePackageManifestAsync(version.Version);
    //    yield return operation;

    //    if (operation.Status == EOperationStatus.Succeed)
    //    {
    //        //Succed
    //    }
    //    else
    //    {
    //        //Error
    //        Debug.LogError(operation.Error);
    //    }
    //}
    //static IEnumerator Download(string PackageName)
    //{
    //    int downloadingMaxNum = 10;
    //    int failedTryAgain = 3;
    //    var package = YooAssets.GetPackage(PackageName);
    //    var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);

    //    //没有需要下载的资源
    //    if (downloader.TotalDownloadCount == 0)
    //    {
    //        yield break;
    //    }

    //    //需要下载的文件总数和总大小
    //    int totalDownloadCount = downloader.TotalDownloadCount;
    //    long totalDownloadBytes = downloader.TotalDownloadBytes;

    //    //注册回调方法
    //    downloader.OnDownloadErrorCallback = OnDownloadErrorFunction;
    //    downloader.OnDownloadProgressCallback = OnDownloadProgressUpdateFunction;
    //    downloader.OnDownloadOverCallback = OnDownloadOverFunction;
    //    downloader.OnStartDownloadFileCallback = OnStartDownloadFileFunction;

    //    //开启下载
    //    downloader.BeginDownload();
    //    yield return downloader;

    //    //检测下载结果
    //    if (downloader.Status == EOperationStatus.Succeed)
    //    {
    //        //下载成功
    //    }
    //    else
    //    {
    //        //下载失败
    //    }
    //} 
    //清理文件系统所有的缓存资源文件
    //static IEnumerator ClearPackageAllCacheFiles(ResourcePackage package)
    //{
    //    var operation = package.ClearAllBundleFilesAsync();
    //    yield return operation;

    //    if (operation.Status == EOperationStatus.Succeed)
    //    {
    //    }
    //    else
    //    {
    //        Debug.LogError(operation.Error);
    //    }
    //}
    ////清理文件系统未使用的缓存资源文件
    //static IEnumerator ClearPackageUnusedCacheFiles(ResourcePackage package)
    //{
    //    var operation = package.ClearUnusedBundleFilesAsync();
    //    yield return operation;

    //    if (operation.Status == EOperationStatus.Succeed)
    //    {
    //    }
    //    else
    //    {
    //        Debug.LogError(operation.Error);
    //    }
    //}
    #endregion

    #region Callback

    public static event Action<string, long> OnStartDownloadFile;
    public static event Action<int, int, long, long> OnDownloadProgressUpdate;
    public static event Action<bool> OnDownloadOver;
    public static event Action<string, string> OnDownloadError;
    private static void StartDownloadFile(string fileName, long sizeBytes)
    {
        Debug.Log($"開始下載文件：{fileName}，大小：{sizeBytes} 字節");
        OnStartDownloadFile?.Invoke(fileName, sizeBytes);
    }

    private static void DownloadProgressUpdate(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
    {
        Debug.Log($"下載進度：{currentDownloadCount}/{totalDownloadCount} 文件，已下載 {currentDownloadBytes}/{totalDownloadBytes} 字節");
        OnDownloadProgressUpdate?.Invoke(totalDownloadCount, currentDownloadCount, totalDownloadBytes, currentDownloadBytes);
    }
    private static void DownloadOver(bool isSucceed)
    {
        Debug.Log($"下載完成：{(isSucceed ? "成功" : "失敗")}");
        OnDownloadOver?.Invoke(isSucceed);  
    }
    private static void DownloadError(string fileName, string error)
    {
        Debug.Log($"下載錯誤：文件 {fileName}，錯誤信息：{error}");
        OnDownloadError.Invoke(fileName, error);
    }

    #endregion
    #region Get
    public static async Task UnloadUnusedAssets(string packagname)
    {
        var package = YooAssets.GetPackage(packagname);
        var operation = package.UnloadUnusedAssetsAsync();
        if (operation.Status != EOperationStatus.Succeed) await UniTask.Yield();
        return;
    }
    public static Sprite[] GetPackageDataSprites(string packagname, string name) 
    {
        var p = YooAssets.GetPackage(packagname);
        if (p == null) return null;
        var a = p.LoadSubAssetsSync<Sprite>(name).GetSubAssetObjects<Sprite>();
        return a;
    }
    public static T GetPackageData_Sync<T>(string packagname, string name) where T : UnityEngine.Object
    {
        var p = YooAssets.GetPackage(packagname);
        if (p == null) return null;
        var a = p.LoadAssetSync<T>(name).GetAssetObject<T>();
        return  a;
    }
    public static async Task<T> GetPackageData_Async<T>(string packagname, string name) where T : UnityEngine.Object
    {
        await UniTask.SwitchToMainThread();
        var p = YooAssets.GetPackage(packagname);
        if (p == null) return null;
        AssetHandle Target = p.LoadAssetSync<T>(name);
        await Target;
        return Target.GetAssetObject<T>();
    }
    public static async Task<T> GetPackageData_Async<T>(string packagname, string tag, string name) where T : UnityEngine.Object
    {
        await UniTask.SwitchToMainThread();
        var p = YooAssets.GetPackage(packagname);
        if (p == null) return null;

        var t = p.GetAssetInfos(tag);
        foreach (var a in t)
        {
            if (a.AssetPath.Contains(name))
            {
//#if UNITY_EDITOR
//                Debug.Log(a.AssetPath);
//#endif  
                AssetHandle Target = p.LoadAssetAsync<T>(a.AssetPath);
                await Target;
                return Target.GetAssetObject<T>();

            }
        }
        return null;
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
    #endregion
    public class RemoteServices : IRemoteServices
    {
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteServices(string package)
        {
            _defaultHostServer = FileUrl_1 + package;
            _fallbackHostServer = FileUrl_2 + package;
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
