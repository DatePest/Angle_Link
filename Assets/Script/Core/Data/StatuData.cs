using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "StatuData", menuName = "AL/StatuData" ,order =6)]
public class StatuData : ScriptableObject
{
    [Header("Display")]
    public string title;
    public Sprite icon;
    [TextArea(2, 4)]
    public string desc;
    [Header("FX")]
    public GameObject status_fx;

    public List<EffectData> effectDatas;

    public void DoStatu(Unit Target, AbilityExcuteData AData)
    {
        foreach (EffectData data in effectDatas)
        {
            data.DoEffect(Target,AData);
        }
    }
    public void UnStatu(Unit Target,AbilityExcuteData AData)
    {
        foreach (EffectData data in effectDatas)
        {
            data.UnDoEffect(Target, AData);
        }
    }
}

public class Statu
{
    public Unit Owner;
    public bool Ongoing;
    public StatuData StatuData;
    public AbilityExcuteData  excuteData;

    public int TimeDuration;

    public string Uid => excuteData.ExcuteUid;
    public Statu(AbilityExcuteData Adata, StatuData statuData)
    {
        StatuData = statuData;
        excuteData = Adata;
        TimeDuration = excuteData.StatuDuration;
    }

    public void SetOwner(Unit u) => Owner = u;
    public void SetOngoing(bool b) => Ongoing = b;
    public void AddDoDoStatu()
    {
        StatuData.DoStatu(Owner, excuteData);
    }
    public void RemoveUnDoStatu() 
    {
        StatuData.UnStatu(Owner, excuteData);
    }
    public void SetTimeDuration(int i)  => TimeDuration = i;
    public void UpdataTimeDuration(int i)
    {
        if(Ongoing) { TimeDuration = -1; return; }
        TimeDuration--;
        if (TimeDuration > 0) return;
        RemoveUnDoStatu();
        Owner.RemoveStatu(this);

    }
}