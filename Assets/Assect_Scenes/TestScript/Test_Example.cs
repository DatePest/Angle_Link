using Editor_Tool;
using RngDropTool;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Test_Example : MonoBehaviour
{
    public bool showHiddenField_A;
    public bool showHiddenField_B;

    [ConditionalHide("showHiddenField_A")]
    public int A = 10;
    [ConditionalHide("showHiddenField_B")]
    public int hiddenField;
    [CustomEditorButton("TestRngItem")]
    public int B = 100;

    public WeightedRandom WeightedRandom = new WeightedRandom();


    public void TestRngItem()
    {
        foreach (var item in WeightedRandom.itemTable.items)
        {
            Debug.Log(item.GetObj().Name);
        }

    }
    [System.Serializable]
    public class TastA : Object
    {
        public int ID;
    }
    [System.Serializable]
    public class TastB : TastA
    {
        public int Name;
    }
}
