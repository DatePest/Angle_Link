#if UNITY_EDITOR
using Editor_Tool;
#endif
using RngDropTool;
using UnityEngine;

public class Test_Example : MonoBehaviour
{
    public bool showHiddenField_A;
    public bool showHiddenField_B;
#if UNITY_EDITOR
    [ConditionalHide("showHiddenField_A")]
#endif
    public int A = 10;
#if UNITY_EDITOR
    [ConditionalHide("showHiddenField_B")]
#endif
    public int hiddenField;
#if UNITY_EDITOR
    [CustomEditorButton("TestRngItem")]
#endif
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
