using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "AL/Monster", order = 11)]
public class Monster : IUnitData
{

    public BattleSelectOrder GetMonsterAction(BattleLogic logic)
    {
        // Todo GetMonsterAction
        return default;
    }
}