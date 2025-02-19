using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core.Fx
{
    [CreateAssetMenu(fileName = "FX_Single", menuName = "AL/Fx/FX_Single")]
    public class FX_Single : FXData
    {
        public int millisecondsDelay = 500;
        protected override async UniTask<GameObject> Excute(Unit Caster, Unit Target, float SpeeMmultiplier)
        {
            var t = GetTargetPostint(Caster, Target);
            var Fx = GameObject.Instantiate(FxObj);
            SetPosition(Fx, t.gameObject);
            float time = (millisecondsDelay / SpeeMmultiplier);
            var Ps = Fx.GetComponent<ParticleSystem>();
            if (Ps != null)
            {
                Ps.Play();
                var m =Ps.main;
                m.simulationSpeed = SpeeMmultiplier;
                while (!Ps.isStopped) { await UniTask.Yield(PlayerLoopTiming.Update); }
                return Fx;
            }
            Fx.transform.SetParent(FXData.FindCanvas().transform, false);
            var animation = Fx.GetComponent<Animation>();
            if (animation != null)
            {
                GameUtilityTool.AnimeSpeed(time, animation);
                await GameUtilityTool.PlayAnime(animation);
                return Fx;
            }
            else
            {
                await UniTask.Delay((int)time);
                return Fx;
            }
        }

    }
}
