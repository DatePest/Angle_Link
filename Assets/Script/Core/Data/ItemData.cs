using Assets.Script.Core.Data;
using RngDropTool;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "AL/Item", order = 8)]
public class ItemData : iSobj_Name, IRngItem
{
    [TextArea(5,5)]
    public string Desc;
    public Sprite Icon;
    public bool CanStack;
    public string Name { get => AssetName; }
}
[System.Serializable]
public class Item 
{
    public ItemData Data;
    public string AssetName => Data.AssetName;
    public int Amount;

    public static async Task<Item> Create_async(Item_Net dataNet)
    {
        var item = new Item();
        var data = await AssetFInd.GetItemData_Async(dataNet.AssetName);
        //item.AssetName = dataNet.AssetName;
        item.Amount = dataNet.Amount;
        item.Data = data;
        return item;
    }

    public static Item Create(ItemData itemData,int Amount)
    {
        var item = new Item();
        item.Amount = Amount;
        item.Data = itemData;
        return item;
    }

}