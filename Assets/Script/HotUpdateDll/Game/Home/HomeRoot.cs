using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Client;
using static SingletonTool;
using UnityEngine.SceneManagement;
using static Client.ClientScene;
using Cysharp.Threading.Tasks;

public class HomeRoot : SigMono<HomeRoot>
{
    Scene Current = default;
    bool LoadLock = true;

    async protected override void Awake()
    {
        base.Awake();


        var handle = UI_SceneLoad.Get().BackLoad(ClientSceneName.News.ToString(), "GameCore");
        while (!(handle.Progress >= 0.9f)) await UniTask.Yield(PlayerLoopTiming.Update);
        handle.UnSuspend();
    }

    private void Start()
    {
        LoadLock = false;
        Current = default;
        SceneManager.sceneLoaded += onSceneLoad;
    }

    private async void onSceneLoad(Scene scene, LoadSceneMode arg1)
    {
        if (Current != default) await SceneManager.UnloadSceneAsync(Current);
        Current = scene;
        LoadLock = false;
    }
    protected  override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= onSceneLoad;
        LoadLock = true;
        Current = default;
    }
    private void OnGUI()
    {
        //if (GUI.Button(new Rect(40, 80, 120, 40), "Login"))
        //{
        //    UI_SceneLoad.Get().SceneLoad(ClientScene.ClientSceneName.Login, UnityEngine.SceneManagement.LoadSceneMode.Single);
        //}
    }

    public void SwichScene(ClientSceneName scene)
    {
        if(LoadLock) return;
        if (Current != default && scene.ToString() == Current.name) return;
        var u = UI_SceneLoad.Get();
        if(u == null) return;
        LoadLock = true;
        u.SceneLoad(scene.ToString(), GameConstant.PackName_GameCore, LoadSceneMode.Additive, false);
        return;
    }
}
