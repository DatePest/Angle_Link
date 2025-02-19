using Editor_Tool;
using EditorTool;
using RngDropTool;
using System.Collections.Generic;
using UnityEngine;

public class Rng_Example : MonoBehaviour
{

    #region 1
    //[CustomEditorButton("Test1")]
    //public RandomEnum myEnum;

    //[RandomBindEnum("myEnum"), SerializeReference]
    //public IRngRandom items;
    //public void Test1()
    //{
    //    var data = items.GetRandomDrop();
    //    foreach (var item in data.Items)
    //    {
    //        var isnull = item.GetObj() is null;
    //        if (!isnull)
    //            Debug.Log($" Item : {item.GetObj().Name}");
    //        else
    //            Debug.Log($" Item is null");
    //    }
    //}
    #endregion

    #region 2

    public RngDrop RngDrop;
    #endregion
 
}
