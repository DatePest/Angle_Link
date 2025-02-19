using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core.Fx
{
    public class Anime_NextWave : IFx
    {
        Action<BattleData> UpdataSlot;
        Action<bool> SwichOwnTeam;
        Func<UniTask> Anime;

        public Anime_NextWave(GameObject TargetParent)
        {
            var Ass = YooAsset_Tool.GetPackageData_Sync<GameObject>(GameConstant.PackName_GameCore, "NextWaveAnime");
            var g = GameObject.Instantiate(Ass, TargetParent.transform, false);
            Init(g);
            g.gameObject.SetActive(false);
        }

        public void SetAction(Action<BattleData> updataSlot, Action<bool> swichOwnTeam, Func<UniTask> anime)
        {
            UpdataSlot = updataSlot;
            SwichOwnTeam = swichOwnTeam;
            Anime = anime;
        }
        public override async UniTask Excute(object Battle)
        {
            await UniTask.Delay(1000);

            var battle = Battle as BattleData;
            await GameUtilityTool.PlayAnime("Black_ToHide", mAnimation);
            UpdataSlot?.Invoke(battle);
            SwichOwnTeam?.Invoke(false);
            await GameUtilityTool.PlayAnime("Black_ToShow", mAnimation);
            await Anime.Invoke();
        }

        protected override void Init(GameObject g)
        {
            mAnimation = g.GetComponent<Animation>();
        }
        public override void Dispose()
        {
            base.Dispose();
            UpdataSlot = null;
            SwichOwnTeam = null;
            Anime = null;
        }

    }
}
