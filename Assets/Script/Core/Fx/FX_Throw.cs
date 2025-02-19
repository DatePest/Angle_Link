using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Script.Core.Fx.AnimFX_Tool;

namespace Assets.Script.Core.Fx
{
    [CreateAssetMenu(fileName = "FX_Throw", menuName = "AL/Fx/FX_Throw")]
    public class FX_Throw : FXData
    {
        public float Speed = 0.5f;
        protected override async UniTask<GameObject> Excute(Unit Caster, Unit Target, float SpeeMmultiplier)
        {
          
            var Tpos = GetTargetPostint(Caster, Target);
            var Fx = GameObject.Instantiate(FxObj);
            SetPosition(Fx, Caster.Solt);

            //Fx.transform.LookAt(Tpos);
            GameUtilityTool.FaceTowards_2D_Z(Fx.transform, Tpos.position);
            float time = (Speed / SpeeMmultiplier);
            var Ps = Fx.GetComponent<ParticleSystem>();
            if (Ps != null)
            {
                Ps.Play();
                var m = Ps.main;
                m.simulationSpeed = SpeeMmultiplier;
                AnimFX_Tool.RunAnimeFx(AnimFX_Tool.MoveTo(Fx, Tpos.position, time));
                while (!Ps.isStopped) { await UniTask.Yield(PlayerLoopTiming.Update); }
                return Fx;
            }

            else
            {
                var animation = Fx.GetComponent<Animation>();

                if (animation != null)
                {
                    GameUtilityTool.AnimeSpeed(time, animation);
                    animation.Play();
                }
                await AnimFX_Tool.RunAnimeFx(AnimFX_Tool.MoveTo(Fx, Tpos.position, time));
                return Fx;
            }
        }
    }
}
