using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YooAsset;

namespace Client
{
    public class UI_SceneLoad : SingletonTool.SigMono<UI_SceneLoad>
    {
        Slider slider;
        public float display_speed = 0.5f;
        AlphaAnime alphaAnime;
        protected override void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            alphaAnime = new AlphaAnime(gameObject, display_speed);
            base.Awake();
            gameObject.SetActive(false);
        }
        void Update()
        {
            alphaAnime.SwitchaAnime();
        }
        public void Show(bool visible) => alphaAnime.SetVisible(visible);
        public void Showslider(bool visible) => slider.gameObject.SetActive(visible);
        public void Setdisplay_speed(float speed)
        {
            display_speed = speed;
            alphaAnime.display_speed = display_speed;
        }
        public SceneHandle BackLoad(ClientScene.ClientSceneName Scene)
        {
            string sceneename = Scene.ToString();
            string packagename = ClientScene.GetSceneInPackName(Scene);
            return YooAssets.GetPackage(packagename).LoadSceneAsync(sceneename, LoadSceneMode.Additive, LocalPhysicsMode.None, true);
        }
        public async void SceneLoad(ClientScene.ClientSceneName Scene,LoadSceneMode mode,bool slidervisible = true,float speed = 3)
        {
            if (SceneManager.GetActiveScene().name == Scene.ToString()) return;

            Setdisplay_speed(speed);
            Showslider(slidervisible);
            Show(true);
            string sceneename = Scene.ToString();
            string packagename = ClientScene.GetSceneInPackName(Scene);
            await YooLoad(sceneename, packagename, mode);
            Show(false);
        }
        async Task<bool>  YooLoad(string sceneename, string packagename, LoadSceneMode mode)
        {
            SceneHandle handle = YooAssets.GetPackage(packagename).LoadSceneAsync(sceneename, mode, LocalPhysicsMode.None,true);
            while (!handle.IsDone)
            {
                slider.value = handle.Progress;
                if (handle.Progress >= 0.9f)
                {
                    slider.value = 1;
                    break;
                }
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            await YooAsset_Tool.UnloadUnusedAssets(packagename);
            await UniTask.WaitForSeconds(1);
            handle.UnSuspend();

            return true;
        }
        //IEnumerator Load(ClientScene.ClientSceneName Scene)
        //{
        //    Showslider(true);
        //    Show(true);
        //    slider.value = 0;
        //    AsyncOperation operation = SceneManager.LoadSceneAsync(Scene.ToString());
        
        //    operation.allowSceneActivation = false;
        //    while (!operation.isDone)
        //    {
        //        slider.value = operation.progress;
        //        // text.text = operation.progress *100 + "%";
        //        if (operation.progress >= 0.9f)
        //        {
        //            slider.value = 1;
        //            break;
        //        }

        //        yield return null;
        //    }

        //    yield return new WaitForSeconds(1);
        //    operation.allowSceneActivation = true;
        //    Show(false);
        //}
     

    }

}


