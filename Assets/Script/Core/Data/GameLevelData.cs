using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "GameLevelData", menuName = "AL/GameLevelData", order = 4)]
public class GameLevelData: ScriptableObject
{
    public int Stamina;
    public int Exp;
    public ItemData[] DropItems;
    public Wave[] Waves;
    public AudioClip Bgm;

    [System.Serializable]

    public class Wave
    {
        public Monster[] Enemys;
    }
}

