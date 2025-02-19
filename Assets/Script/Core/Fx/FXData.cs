using Assets.Script.Core.Fx;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public abstract class FXData : ScriptableObject
{
    public FxTarget fxTarget;
    public GameObject FxObj;
    public async UniTask Excute(Unit Caster, Unit Target,float SpeeMmultiplier, Action CallBack = null)
    {
        if (FxObj == null)
        {
            CallBack?.Invoke();
            return;
        }

        var Fx =await Excute(Caster, Target, SpeeMmultiplier);
        CallBack?.Invoke();
        if (Fx != null)
        GameObject.Destroy(Fx);
    }

    protected abstract UniTask<GameObject> Excute(Unit Caster, Unit Target, float SpeeMmultiplier);

    protected Transform GetTargetPostint(Unit Caster, Unit Target)
    {
        if (fxTarget == FxTarget.Caster)
        {
            return Caster.Solt.transform;
        }
        if (fxTarget == FxTarget.Target)
        {
            return Target.Solt.transform;
        }
        if (fxTarget == FxTarget.TargetTeam)
        {
            return Target.Solt.transform.parent;
        }
        return default;
    }
    public static void SetPosition(GameObject F , GameObject Target)
    {
        var r = F.GetComponent<RectTransform>();
        if(r != null)
        {
            F.transform.SetParent(FXData.FindCanvas().transform, false);
            return;
        }

        GameUtilityTool.SetToUI_WorldPosition(F, Target);
      

    }

    public enum FxTarget
    {
        Caster,
        Target,
        TargetTeam
    }

    public static GameObject FindCanvas()
    {
        var A = GameObject.Find("AnimeCanvas");
        if (A != null) return A;
        var Fx = GameObject.Find("FxCanvas");
        if (Fx != null) return Fx;

        return default;
    }
}
