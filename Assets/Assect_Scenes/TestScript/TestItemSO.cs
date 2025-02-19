using RngDropTool;
using UnityEngine;
[CreateAssetMenu(fileName = "TestItem", menuName = "Items/Test Item")]
public class TestItemSO : ScriptableObject, IRngItem
{
    [SerializeField]  string ItemName;
    [SerializeField]  int weight;

    public string Name { get => ItemName; set => ItemName = value; }
    public int Weight { get => weight; set => weight = value; }

    public TestItemSO GetInstance()
    {
        throw new System.NotImplementedException();
    }
}
