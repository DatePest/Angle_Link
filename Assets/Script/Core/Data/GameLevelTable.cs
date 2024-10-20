using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevelTable", menuName = "AL/GameLevelTable")]
public class GameLevelTable : ScriptableObject
{
    public TableData[] Datas;

    [System.Serializable]
    public class TableData
    {
        public Sprite Art;
        public string Name;
    }
}

