using Assets.Script.Core.Fx;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using static Codice.CM.Common.CmCallContext;

namespace Assets.Script.Core.Fx
{
    public class FX_OnText : IDisposable , IFx_Speed
    {
        ObjectPool<GameObject> TextPool;
        GameObject Target;
        public float Time { get; set; } = 1f;
        public float CurrentSp { get; set; } = 1f;
        public FX_OnText(GameObject target)
        {
            Target = target;
            Init_NewPool();
        }
       
        public void Excute(ExecutionResult result, Unit Caster, Unit target)
        {
            if (result.EventID == BattleEventTag.OnHeal)
            {
                Debug.Log("OnHeal");
                ShowText(target.Solt, result.Msg, Color.green);
                return;
            }
            if (result.EventID == BattleEventTag.OnDamage)
            {
                ShowText(target.Solt, result.Msg, Color.red);
                return;
            }
        }
        //public const ushort OnHeal = 1500;
        //public const ushort OnDamage = 1504;
        //public const ushort OnKill = 1508;

        async void ShowText(GameObject target, string Msg, Color color)
        {
            var g = TextPool.Get();
            var t = g.GetComponent<TextMeshPro>();
            t.color = color;
            t.text = Msg;

            var Sp = target.GetComponent<SpriteRenderer>();
            Vector3 StarttV3 = GetTopOffset(Sp, 0);
            Vector3 EndV3 = new Vector3(StarttV3.x, StarttV3.y + 1, StarttV3.z);

            g.transform.position = StarttV3;
            AnimFX_Tool.AlphanAnimeFx(t, GetAlpha0(t.color), Time / CurrentSp);
            await AnimFX_Tool.RunAnimeFx(AnimFX_Tool.MoveTo(g, EndV3, Time/ CurrentSp));
            ReleaseToPool(g);
        }


        private void ReleaseToPool(GameObject target)
        {
            TextPool.Release(target);
        }
        Color GetAlpha0(Color color)
        {
            return new Color(color.r, color.g, color.b, 0);
        }
        Vector3 GetTopOffset(SpriteRenderer Sp, float Offset)
        {
            Vector3 topPoint = Sp.bounds.max;
            Vector3 abovePosition = new Vector3(Sp.transform.position.x, topPoint.y + Offset, topPoint.z);
            return abovePosition;
        }
        void Init_NewPool()
        {
            TextPool = new(
               createFunc: () =>
               {
                   var g = new GameObject("Fx_taxt");
                   g.transform.SetParent(Target.transform, false);
                   var t = g.AddComponent<TextMeshPro>();
                   t.sortingOrder = 3;
                   t.fontSize = 12;
                   t.alignment = TextAlignmentOptions.Center;
                   t.textWrappingMode = TextWrappingModes.NoWrap;
                   var Rt = g.GetComponent<RectTransform>();
                   Rt.sizeDelta = Vector2.zero;
                   return g;
               },

                       actionOnGet: obj => { obj.SetActive(true); },
                      actionOnRelease: obj => { obj.SetActive(false); },
                      actionOnDestroy: obj => GameObject.Destroy(obj),
                      collectionCheck: true,
                      defaultCapacity: 12,
                      maxSize: 60
               );
        }

        public void Dispose()
        {
            TextPool.Clear();
            TextPool = null;
            Target = null;
        }

        public void RegisterSpeed(ref Action<float> action)
        {
            action += (f) => CurrentSp = f;
        }
    }
}