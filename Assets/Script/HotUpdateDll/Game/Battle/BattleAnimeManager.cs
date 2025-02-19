using Assets.Script.Core.Fx;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
namespace Assets.Script.HotUpdateDll.Game.Battle_
{
    public class BattleAnimeManager : MonoBehaviour
    {
        Anime_CatserAnime catserAnime;
        Anime_NextWave anime_NextWave;
        AbilityExcuteFx onBattleAbility;
        FX_OnText fX_Text;
        FX_OnDead fX_OnDead;
        private void Start()
        {
            var battleRoot = BattleRoot.Get();
            if (battleRoot == null) throw new Exception("BattleRoot is null");
            //var AnimeCanvas = GameObject.Find("AnimeCanvas");
            //if (AnimeCanvas != null) { throw new Exception("AnimeCanvas is null"); }
         
            ref var Af = ref battleRoot.GetRegisterBattleSpeedAction();
            InitCatserAnime(gameObject, battleRoot, ref Af);
            InitNextWaveAnime(gameObject, battleRoot, ref Af);
            InitOnBattleAbility(battleRoot, ref Af);
            InitFx_OnText(battleRoot, ref Af);
            InitFX_OnDead(battleRoot, ref Af);
        }
        void InitCatserAnime(GameObject AnimeCanvas, BattleRoot battleRoot, ref Action<float> action)
        {
            catserAnime = new(AnimeCanvas);
            catserAnime.RegisterSpeed(ref action);
            battleRoot.GetClientEvent().Ability_StartCasting += catserAnime.Excute;
        }
        void InitNextWaveAnime(GameObject AnimeCanvas, BattleRoot battleRoot, ref  Action<float> action)
        {
            anime_NextWave = new(AnimeCanvas);
            anime_NextWave.RegisterSpeed(ref action);
            battleRoot.GetClientEvent().OnNextWave += anime_NextWave.Excute;
            var slots = GameObject.FindFirstObjectByType<SlotsManager>();
            anime_NextWave.SetAction(slots.UpdataSlot, slots.SwichOwnTeam, slots.P1TeamEnters_Animation);
        }
        void InitOnBattleAbility(BattleRoot battleRoot, ref Action<float> action)
        {
            onBattleAbility = new();
            onBattleAbility.RegisterSpeed(ref action);
            battleRoot.GetClientEvent().OnBattleOrder += onBattleAbility.Excute;
        }
        void InitFx_OnText(BattleRoot battleRoot, ref Action<float> action)
        {
            var textfx = GameObject.Find("TextFX").gameObject;
            fX_Text = new(textfx);
            fX_Text.RegisterSpeed(ref action);
            battleRoot.GetClientEvent().OnExecutionResult += fX_Text.Excute;
        }
        void InitFX_OnDead(BattleRoot battleRoot, ref Action<float> action)
        {
            fX_OnDead = new();
            fX_OnDead.RegisterSpeed(ref action);
            battleRoot.GetClientEvent().OnExecutionResult += fX_OnDead.Excute;
        }
        private void OnDestroy()
        {
            catserAnime.Dispose();
            anime_NextWave.Dispose();
            onBattleAbility.Dispose();
            fX_Text.Dispose();
            fX_OnDead.Dispose();
        }
    }

}