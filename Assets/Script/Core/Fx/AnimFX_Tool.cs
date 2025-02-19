
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using static Assets.Script.Core.Fx.AnimFX_Tool;
using Codice.CM.Common;
using NUnit.Framework;
using UnityEngine.UI;
using TMPro;

namespace Assets.Script.Core.Fx
{
    public class AnimFX_Tool
    {
        public static async UniTask RunAnimeFx(AnimAction animAction)
        {

            Vector3 start_pos = animAction.target.transform.position;
            float timer = 0f;
            while (timer < animAction.duration)
            {
                timer += Time.deltaTime;
                var current_pos = animAction.target.transform.position;
                if (animAction.type == AnimActionType.Move)
                {
                    float dist = (animAction.target_pos - start_pos).magnitude;
                    float speed = dist / Mathf.Max(animAction.duration, 0.01f);
                    current_pos = Vector3.MoveTowards(current_pos, animAction.target_pos, speed * Time.deltaTime);
                    animAction.target.transform.position = current_pos;
                }

                if (animAction.type == AnimActionType.Size)
                {
                    float dist = Mathf.Abs(animAction.target.transform.localScale.y - animAction.value);
                    float speed = dist / Mathf.Max(animAction.duration, 0.01f);
                    animAction.target.transform.localScale = Vector3.MoveTowards(animAction.target.transform.localScale, animAction.value * Vector3.one, speed * Time.deltaTime);
                }
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (animAction.type == AnimActionType.Move)
                animAction.target.transform.position = animAction.target_pos;
            if (animAction.type == AnimActionType.Size)
                animAction.target.transform.localScale = animAction.value * Vector3.one;
            animAction.callback?.Invoke();
        }
      
        public static AnimAction MoveTo(GameObject target ,Vector3 pos, float duration,Action CallBack = null)
        {
            AnimAction action = new AnimAction();
            action.type = AnimActionType.Move;
            action.target = target;
            action.duration = duration;
            action.target_pos = pos;
            if (CallBack != null) action.duration = duration;
            return action;
        }
        public static AnimAction ScaleTo(GameObject target, float value, float duration,Action CallBack = null)
        {
            AnimAction action = new AnimAction();
            action.type = AnimActionType.Size;
            action.target = target;
            action.duration = duration;
            action.value = value;
            if(CallBack != null) action.duration = duration;
            return action;
        }
       
        public enum AnimActionType
        {
            None = 0,
            Move = 5,
            Size = 10,
            Color = 15
        }
        public class AnimAction
        {
            public AnimActionType type;
            public GameObject target;
            public Vector3 target_pos;
            public float value = 0f;
            public float duration = 1f;
            public UnityAction callback = null;
        }
        #region  AlphanAnimeFx
        public static async UniTask AlphanAnimeFx(SpriteRenderer sp, Color color, float duration)
        {
            if (sp == null) return;
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                Color currentColor = sp.color;
                float dist = ColorDistance(sp.color, color);
                float speed = dist / Mathf.Max(duration, 0.01f);
                Color newColor = Color.Lerp(currentColor, color, speed * Time.deltaTime);
                sp.color = newColor;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            sp.color = color;
        }
        public static async UniTask AlphanAnimeFx(Image sp, Color color, float duration)
        {
            if (sp == null) return;
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                Color currentColor = sp.color;
                float dist = ColorDistance(sp.color, color);
                float speed = dist / Mathf.Max(duration, 0.01f);
                Color newColor = Color.Lerp(currentColor, color, speed * Time.deltaTime);
                sp.color = newColor;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            sp.color = color;
        }
        public static async UniTask AlphanAnimeFx(TextMeshPro sp, Color color, float duration)
        {
            if (sp == null) return;
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                Color currentColor = sp.color;
                float dist = ColorDistance(sp.color, color);
                float speed = dist / Mathf.Max(duration, 0.01f);
                Color newColor = Color.Lerp(currentColor, color, speed * Time.deltaTime);
                sp.color = newColor;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            sp.color = color;
        }
        public static async UniTask AlphanAnimeFx(GameObject Target, Color color, float duration)
        {
            var sp = Target.GetComponent<SpriteRenderer>();
            if (sp != null)
            {
                AlphanAnimeFx(sp, color, duration);
                return;
            }
            var image = Target.GetComponent<Image>();
            if (sp != null)
            {
                AlphanAnimeFx(image, color, duration);
                return;
            }
            var Tmp = Target.GetComponent<TextMeshPro>();
            if (sp != null)
            {
                AlphanAnimeFx(Tmp, color, duration);
                return;
            }
        }
        private static float ColorDistance(Color a, Color b)
        {
            return Mathf.Sqrt(Mathf.Pow(a.r - b.r, 2) + Mathf.Pow(a.g - b.g, 2) + Mathf.Pow(a.b - b.b, 2) + Mathf.Pow(a.a - b.a, 2));
        }
        #endregion
    }
}
