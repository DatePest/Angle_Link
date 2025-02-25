using GameApi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestFrame : MonoBehaviour
{
    public string Text;

   
    void Start()
    {
        Application.runInBackground = true;
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 50), "Y"))
        {
            Test();
        }
        if (GUI.Button(new Rect(10, 60, 200, 50), "B"))
        {
            Test1();
        }

    }
    void Test()
    {
        //var A = ApiTool.JsonToObject<IGameBattleLog>(Text);
        //var A = MsgLogs.NewEEEEEEE(Text);
        //UnityEngine.Debug.Log($"Req == null {A == null}");
        //foreach (var item in A)
        //{
        //    UnityEngine.Debug.Log(item.SortId);
        //    UnityEngine.Debug.Log(item.ID);
           
        //    if(item.ID == 1000)
        //    {
        //        var d = item as BattleEventLog;
        //        Debug.Log(d.data);
        //    }
        //}
     
    }
    void Test1()
    {
        var A = new AAA();
        var st = new string[3];
        st[0] = "A";
        st[1] = "B";
        var B = new BBB();
        B.Name = st;
        A.bBB = B;
        A.A1 = 100;
        Text = WebTool.ToJson(A);

        Debug.Log(Text);

        var AA = WebTool.JsonToObject<AAA>(Text);
        UnityEngine.Debug.Log($"Req == null {AA == null}");
        UnityEngine.Debug.Log(AA.A1);
    }
   
    public class AAA
    {
        public BBB bBB { get; set; }
        public int A1 { get;set;  } 
        
    }

    public class BBB
    {
        public string[] Name { get; set; }

    }
}
