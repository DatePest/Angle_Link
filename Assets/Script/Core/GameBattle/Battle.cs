using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Serialization;
using UnityEngine;
[System.Serializable]
public class Battle
{
    //[NonSerialized]public BattleLogManager battleLogs = new();
    public BattleSettings BattleSettings;
    public GameState gameState;
    public TrunState trunState;
    public string Uid;
    public List<Unit>[] Player = new List<Unit>[2];
    public int Trun;
    public int CurrentWave;

    public Battle(BattleSettings battleSettings,string uid)
    {
        BattleSettings = battleSettings;
        Uid = uid;
    }

    public List<Unit> GetPlayer() => Player[0];
    public List<Unit> GetMonster() => Player[1];


    public Unit GetUnit(string Uid)
    {
        foreach (List<Unit> u in Player)
        {
            foreach (Unit v in u)
            {
                if (v.Uid == Uid)
                    return v;
            }
        }
        return null;
    }

}
[System.Serializable]
public enum TrunState
{
    Start, WaitSelect, Running, Anime, End
}
[System.Serializable]
public enum GameState
{
    inited,
    Ing,
    GameEnd
}
[System.Serializable]
public class BattleSettings
{
    public Team PlayerTeam;
    public NetworkFilteredData<GameLevelData> LevelData;
    public void SetGameLevelData(GameLevelData levelData, string levelData_InPackageName )
    {
        LevelData = new NetworkFilteredData<GameLevelData>(levelData, levelData_InPackageName);
    }
}

