using System.Collections;
using UnityEngine;

public class TimeToolMono : MonoBehaviour
{
    private static TimeToolMono _instance;

    public Coroutine StartRoutine(IEnumerator routine, System.Action callback)
    {
        return StartCoroutine(this.routine(routine, callback));
    }
    IEnumerator routine(IEnumerator Main, System.Action callback)
    {
        yield return Main;
        callback?.Invoke();
    }

    public void StopRoutine(Coroutine routine)
    {
        StopCoroutine(routine);
    }
   
   
    public static TimeToolMono Inst
    {
        get
        {
            if (_instance == null)
            {
                GameObject ntool = new GameObject("TimeTool");
                _instance = ntool.AddComponent<TimeToolMono>();
            }
            return _instance;
        }
    }
}
