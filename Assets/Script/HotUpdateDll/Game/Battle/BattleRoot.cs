using EventSystemTool;
using UnityEngine;

public class BattleRoot : MonoBehaviour
{
    BattleServer game;
    BattleClient battleClient;
    IBattleEvent_Client Event ;
    private void Awake()
    {
        battleClient = new BattleClient();
    }
    private void Start()
    {
        //game = new BattleServer();
        //game.Init(new BattleSettings());
        //game.Start();
    }
    private void OnDestroy()
    {
        battleClient.OnDestroy();
    }


    public void StartClient(Battle battle)
    {
      
        battleClient.Init(battle);
    }
    public static BattleClient GetBattleData()
    {
        var t =GameObject.FindAnyObjectByType<BattleRoot>();
        if (t == null ) return null;
        return t.battleClient;
    }
}