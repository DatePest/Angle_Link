using Assets.Script.Core.Data;
using RngDropTool;
using UnityEngine;
[CreateAssetMenu(fileName = "GameLevelData", menuName = "AL/GameLevelData", order = 4)]
public class GameLevelData: iSobj_Name
{
    public int Stamina;
    public int Exp;
    public int TurnMax = 30;

    public Wave[] Waves;
    public AudioClip Bgm;
    public RngDrop RngDrop ;



    [System.Serializable]

    public class Wave
    {
        public Monster[] Enemys;
    }

    public SettlementDatas_Net GetRngItem()
    {
        return Tools.DropResultConvert.IRngItemConvert(RngDrop.GetRandomDrop());
    }

}

