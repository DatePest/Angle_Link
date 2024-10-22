using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "AL/Item", order = 8)]
public class ItemData : ScriptableObject
{

    public string ID;
}

public class Item
{
    Item() { }
    public ItemData Data;
   

    public static Item Create(Item_Net data)
    {
        var item = new Item();
        return item;
    }
}