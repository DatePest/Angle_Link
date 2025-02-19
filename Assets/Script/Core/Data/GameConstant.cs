using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameConstant
{

    public const string PackName_GameCore = "GameCore";

    // Player
    public const int Player_Smooth_StartXP = 100;
    public const int Player_Smooth_EndXP = 100000;
    public const int PlayerMaxLevel = 300;
    public const float PlayerExp_Smooth_P = 2.0F;

    // Characte Exp
    public const int CharacterExp_Smooth_StartXP = 100;
    public const int CharacterExp_Smooth_EndXP = 100000;
    public const int CharacterMaxLevel = 50;
    public const float CharacterExp_Smooth_P = 2.0F;


    //Client Cache Tag
    public const string OriginalBattleData = "OriginalBattleData"; //The original data returned initially

    public const string Item_Money = "ItemMoney";
}