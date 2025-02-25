using System;
using UnityEngine;

namespace Assets.Script.Core.Fx
{
    public class FX_OnDead : IFx_Speed, IDisposable
    {
        public float Time { get; set; } = 0.5f;
        public float CurrentSp { get; set; } = 1f;

        public void Excute(ExecutionResult result, Unit Caster, Unit target)
        {
            if (result.EventID == BattleEventTag.OnKill)
            {
                ToHide(target.Solt);
                return;
            }
        }

        public async void ToHide(GameObject target)
        {
            var Sp = target.GetComponent<SpriteRenderer>();
            if(Sp == null) { throw new Exception("target SpriteRenderer is null"); }
            await AnimFX_Tool.AlphanAnimeFx(Sp, new Color(1,1,1,0), Time / CurrentSp);
            target.SetActive(false);
            Sp.color = new Color(1, 1, 1, 1);
        }
        public void RegisterSpeed(ref Action<float> action)
        {
            action += (f) => CurrentSp = f;
        }

        public void Dispose()
        {
            return;
        }
    }

   
}
