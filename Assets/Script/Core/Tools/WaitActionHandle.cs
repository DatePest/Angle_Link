using Cysharp.Threading.Tasks;
using System;
using System.Collections;
public class WaitActionHandle
{
    public Action callback;
    public bool completed;
    public async void SetRun(Action action, Action end = null)
    {
        if (end != null) callback += end;
        await UniTask.RunOnThreadPool(action);
        CallCompleted();
    }
    public void SetRun(IEnumerator e, Action end = null)
    {
        if (end != null) callback += end;
        var c = TimeTool.StartCoroutine(e, CallCompleted);
    }
    void CallCompleted()
    {
        callback?.Invoke();
        completed = true;
    }
}
