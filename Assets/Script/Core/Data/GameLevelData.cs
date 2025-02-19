using Assets.Script.Core.Data;
using Editor_Tool;
using RngDropTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "GameLevelData", menuName = "AL/GameLevelData", order = 4)]
public class GameLevelData: iSobj_Name
{
    public int Stamina;
    public int Exp;
    public int TurnMax = 30;

    public Wave[] Waves;
    public AudioClip Bgm;
    [CustomEditorButton("TestRandom")]
    public RngDrop RngDrop ;


#if UNITY_EDITOR
    public void TestRandom()
    {
       var data = RngDrop.TestRandom();

        Tools.DropResultConvert.IRngItemConvert(data);

    }
#endif

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

