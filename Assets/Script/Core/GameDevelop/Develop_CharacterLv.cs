using GameApi;
using Assets.Script.Core.Data;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core.GameDevelop
{

    public class Develop_CharacterLv
    {
        public static Dictionary<string, int> ExpItems { get; private set; } = new()
        {
            {"ExpItem01", 200},
            {"ExpItem02", 500},
            {"ExpItem03", 1000}
        };
        public static List<Item_Net> GetUserItem(UserData user)
        {
            var t = new List<Item_Net>();
            foreach (var PlayerDataItems in user.PlayerData.Items)
            {
                foreach (var ExpName in ExpItems)
                {
                    if(PlayerDataItems.AssetName == ExpName.Key)
                    {
                        t.Add(PlayerDataItems);
                        if(t.Count >= ExpItems.Count)
                        {
                            return t;
                        }
                    }
                }
            }
            return t;
        }
        public static void AddExp(int Exp,CharacterData_Net character)
        {
            character.CurrentExp += Exp;
            LevelUP(character);
        }
        public static void LevelUP(CharacterData_Net character)
        {
            var need = GetLvNeedExp(character.Lv);
            if (character.CurrentExp >= need)
            {
                character.Lv++;
                character.CurrentExp -= need;
                LevelUP(character);
            }
            else
                return;
        }
        public static void ToLevelUP(CharacterData_Net character ,IEnumerable<DataObject> Items)
        {
            ToLevelUP(character, GetExp(Items));
        }
        public static void ToLevelUP(CharacterData_Net character, int exp)
        {
            if (exp <= 0) return;
            AddExp(exp, character);
        }
        public static PreviewCharacterLevel PreviewLevelUP(Character character,int Exp) => PreviewLevelUP(character.characterNetData.Lv, character.characterNetData.CurrentExp+Exp);
        public static PreviewCharacterLevel PreviewLevelUP(int Lv, int Exp)
        {
            while (true)
            {
                int currentLvNeed = GetLvNeedExp(Lv);

                if (Exp >= currentLvNeed)
                {
                    Lv++;
                    Exp -= currentLvNeed;
                }
                else
                {
                    return new PreviewCharacterLevel(Lv, Exp);
                }
            }
        }
        public static int GetLvNeedExp(int Lv)
        {

            return ExperienceCalculator.GetSmoothXP(GameConstant.CharacterExp_Smooth_StartXP, GameConstant.CharacterExp_Smooth_EndXP, GameConstant.CharacterMaxLevel, Lv, GameConstant.CharacterExp_Smooth_P);
        }
        public static int GetExp(IEnumerable<DataObject> Items)
        {
            int e = 0;
            foreach (var i in Items)
            {
                 e += GetExp(i.AssetName, i.Amount);
            }
            return e;
        }

        public static int GetExp(string name,int aomunt)
        {
            foreach (var s in ExpItems)
            {
                if (s.Key != name) continue;
                return  s.Value * aomunt;
            }
            return 0;
        }


       
        public class PreviewCharacterLevel
        {
            public int Level;
            public int Exp; 
            public PreviewCharacterLevel(int level, int exp)
            {
                Level = level;
                Exp = exp;
            }
        }

    }
    public class Develop_PlayerLv
    {
        public static void LevelUP(Api_PlayerData P)
        {
            var need = GetLvNeedExp(P.Lv);
            if (P.Exp >= need)
            {
                P.Lv++;
                P.Exp -= need;
                LevelUP(P);
            }
            else
                return;
        }
        public static void LevelUP(PlayerData_Net P)
        {
            var need = GetLvNeedExp(P.Lv);
            if (P.Exp >= need)
            {
                P.Lv++;
                P.Exp -= need;
                LevelUP(P);
            }
            else
                return;
        }
        public static int GetLvNeedExp(int Lv)
        {

            return ExperienceCalculator.GetSmoothXP(GameConstant.Player_Smooth_StartXP, GameConstant.Player_Smooth_EndXP, GameConstant.PlayerMaxLevel, Lv, GameConstant.PlayerExp_Smooth_P);
        }
        public static int GetStaminaMax(int Lv)
        {
            return Lv * 10;
        }
    }
}
