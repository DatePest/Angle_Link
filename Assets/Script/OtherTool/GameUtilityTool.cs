using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameUtilityTool
{
    private static System.Random random = new System.Random();
    public static List<T> PickXRandom<T>(List<T> source, List<T> dest, int x)
    {
        if (source.Count <= x || x <= 0)
            return source; //No need to pick anything

        if (dest.Count > 0)
            dest.Clear();

        for (int i = 0; i < x; i++)
        {
            int r = random.Next(source.Count);
            dest.Add(source[r]);
            source.RemoveAt(r);
        }

        return dest;
    }

    public static void AnimeSpeed(float Speed, Animation animation)
    {
        foreach (AnimationState state in animation)
        {
            state.speed = Speed;
        }
    }

    public static async Task DebugAsync(string msg)
    {
        await UniTask.SwitchToMainThread();
        Debug.Log(msg); 
    }
    public static async Task DebugErrorAsync(string msg)
    {
        await UniTask.SwitchToMainThread();
        Debug.LogError(msg);
    }

    public static async UniTask PlayAnime(string Name, Animation animation ,bool EndActirve = false)
    {
        await UniTask.SwitchToMainThread();
        animation.gameObject.SetActive(true);
        var t = animation.GetClip(Name);
        if (t == null) throw new Exception($"animation {Name} is null");
        animation.clip = t;
        AnimationState state = animation[Name];
        animation.Play();
        await UniTask.Delay(100);
        while (state.normalizedTime != 0f)
        {
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }
        animation.gameObject.SetActive(EndActirve);
        //Debug.Log($"{Name} End");
    }
    public static async UniTask PlayAnime(Animation animation, bool EndActirve = false)
    {
        await UniTask.SwitchToMainThread();
        animation.gameObject.SetActive(true);
        AnimationState state = animation[animation.clip.name];
        animation.Play();
        await UniTask.Delay(100);
        while (state.normalizedTime != 0f)
        {
            await UniTask.Yield(PlayerLoopTiming.LastFixedUpdate);
        }
        animation.gameObject.SetActive(EndActirve);
        //Debug.Log($"{animation.clip.name} End");
    }
    public static void FaceTowards_2D_Z(Transform A, Vector3 B)
    {
        Vector3 direction = B - A.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        A.rotation = Quaternion.Euler(0, 0, angle);
    }
    public static  Vector3  GetUI_WorldPosition(RectTransform Rt)
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, Rt.position);
        float depth = 10.0f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depth));

        return worldPosition;
    }
     public static  void SetToUI_WorldPosition(GameObject Target, RectTransform Goto)
    {
      
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, Goto.position);
       float depth = 10.0f;
       Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depth));
        Debug.Log($"{worldPosition}");
        Target.transform.position = worldPosition;
    }
    public static  void SetToUI_WorldPosition(GameObject Target, GameObject Goto)
    {
        var r = Goto.GetComponent<RectTransform>();
        if (r == null)
        {
            Target.transform.position = Goto.transform.position;
            return;
        }
        SetToUI_WorldPosition(Target, r);
    }

}