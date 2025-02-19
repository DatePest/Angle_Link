using Cysharp.Threading.Tasks;
using NUnit;
using System;
using System.Collections;
using UnityEngine;
public class WaitActionHandle
{
    public Action Completedcallback,Stopcallback;
    public bool completed;
    Coroutine coroutine;
   

    public async void SetRun(Action action,bool RunMainThread = false, Action end = null, Action error =null)
    {
        if (end != null) Completedcallback += end;
        if (error != null) Stopcallback += error;
        if (RunMainThread)
            await UniTask.SwitchToMainThread();
        action?.Invoke();
        CallCompleted();
    }
    public void SetRun(IEnumerator e, Action end = null, Action error = null)
    {
        if (end != null) Completedcallback += end;
        if (error != null) Stopcallback += error;
        coroutine = TimeTool.StartCoroutine(e, CallCompleted);
    }
    public void Stop()
    {
        TimeTool.StopCoroutine(coroutine);
        Stopcallback?.Invoke();
        Dis();
    }
    void CallCompleted()
    {
        Completedcallback?.Invoke();
        Dis();
    }
    void Dis()
    {
        completed = true;
        Completedcallback = null;
        Stopcallback = null;
        coroutine = null;
    }
}
