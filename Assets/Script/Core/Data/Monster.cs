using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Playables;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "AL/Monster", order = 11)]
public class Monster : IUnitData
{

    public BattleSelectOrder GetMonsterAction(BattleLogic logic,Unit U)
    {
        // Todo GetMonsterAction
         return Ability_NormalAttack(U);
    }
    BattleSelectOrder Ability_NormalAttack(Unit U)
    {
        var b = new BattleSelectOrder(U, U.GetUnitAbility(AbilityOrderTag.Ability_NormalAttack));
        return b;
    }
    BattleSelectOrder Ability_1(Unit U)
    {
        var b = new BattleSelectOrder(U, U.GetUnitAbility(AbilityOrderTag.Ability_1));
        return b;
    }
    BattleSelectOrder Ability_2(Unit U)
    {
        var b = new BattleSelectOrder(U, U.GetUnitAbility(AbilityOrderTag.Ability_2));
        return b;
    }
    BattleSelectOrder Ability_SP(Unit U)
    {
        var b = new BattleSelectOrder(U, U.GetUnitAbility(AbilityOrderTag.Ability_SP));
        return b;
    }


}