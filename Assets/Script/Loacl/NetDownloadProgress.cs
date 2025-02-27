using Client;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SingletonTool;

public class NetDownloadProgress : SigMono<NetDownloadProgress>
{
    Slider slider;
    TextMeshProUGUI textUGU;
    protected override void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        textUGU = GetComponentInChildren<TextMeshProUGUI>();

        base.Awake();
        gameObject.SetActive(false);
        YooAsset_Tool.OnStartDownloadFile += OnStartDownloadFile;
        YooAsset_Tool.OnDownloadProgressUpdate += OnDownloadProgressUpdate;
        YooAsset_Tool.OnDownloadOver += OnDownloadOver;
        YooAsset_Tool.OnDownloadError += OnDownloadError;
        YooAsset_Tool.OnConfirmStartDownload += ShowDownloadConfirmDialog;

    }

    void swWaitHandle(WaitHandle waitHandle,bool b)
    {
        waitHandle.complete = true;
        waitHandle.result = b;
    }
    private async Task<bool> ShowDownloadConfirmDialog()
    {
        var w = new WaitHandle();
        Ui_SystemMsg.Get().WaitUserSelect("Download Start?",
           ()=>swWaitHandle(w,true), () => swWaitHandle(w, false)
            );

        while (w.complete == false)
        {
           await Task.Delay(100);
        }
        return w.result;
    }
    /// When starting to download a file
    private void OnStartDownloadFile(string fileName, long sizeBytes)
    {
        gameObject.SetActive(true);
        slider.value = 0;
    }
    /// When the download progress changes
    private void OnDownloadProgressUpdate(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
    {
        float progress = (float)currentDownloadBytes / totalDownloadBytes;

        var v =  Math.Round(progress, 3);
        slider.value = (float)v;
        //Debug.Log($"下載進度：{currentDownloadCount}/{totalDownloadCount} 文件，已下載 {currentDownloadBytes}/{totalDownloadBytes} 字節，進度：{progress * 100}%");
        textUGU.text = $"{(v) * 100} %";

    }
    /// When the downloader finishes (whether successful or failed)
    private void OnDownloadOver(bool isSucceed)
    {
        Debug.Log("OnDownloadOverFunction");
      gameObject.SetActive(false);  
    }
    /// When a file fails to download
    private void OnDownloadError
        (string fileName, string error)
    {
        Ui_SystemMsg.Get().ShowMsg($"{fileName} DownloadError : {error}");
    }

    public class WaitHandle
    {
        public bool complete = false;
        public bool result = false;
    }
}
