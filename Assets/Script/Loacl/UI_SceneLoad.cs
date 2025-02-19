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
        CanvasGroupAlphaAnime alphaAnime;
        protected override void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            alphaAnime = new CanvasGroupAlphaAnime(gameObject, display_speed);
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
        public SceneHandle BackLoad(string Scene , string packagename)
        {
            string sceneename = Scene.ToString();
            return YooAssets.GetPackage(packagename).LoadSceneAsync(sceneename, LoadSceneMode.Additive, LocalPhysicsMode.None, true);
        }
        public async void SceneLoad(string  Scene, string packagename , LoadSceneMode mode,bool slidervisible = true,float speed = 3)
        {
            if (SceneManager.GetActiveScene().name == Scene.ToString()) return;

            Setdisplay_speed(speed);
            Showslider(slidervisible);
            Show(true);
            await YooLoad(Scene, packagename, mode);
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
                await Task.Delay(100);
            }

            await YooAsset_Tool.UnloadUnusedAssets(packagename);
            await Task.Delay(1000);
            handle.UnSuspend();

            return true;
        }
       

    }

}


