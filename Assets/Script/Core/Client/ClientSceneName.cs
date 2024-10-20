
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
            Config
            //
        }


        public static string GetSceneInPackName(ClientSceneName sceneName)
        {
            //if (sceneName == ClientSceneName.Login || sceneName == ClientSceneName.Home)
            return "GameCore";

            return string.Empty;
        }
    }
}
