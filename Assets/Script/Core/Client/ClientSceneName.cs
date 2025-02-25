
namespace Client
{
    
    public class ClientScene
    {
        public enum ClientSceneName
        {
            Login,
            Home,
            //HomeScene
            News ,
            Character ,
            Story ,
            BattleStageSelect,
            Radio,
            Gasha,
            Config,
            //
            Battle
        }


        public static void Goto(ClientSceneName sceneName)
        {
            UI_SceneLoad.Get().SceneLoad(sceneName.ToString(), GameConstant.PackName_GameCore, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}
