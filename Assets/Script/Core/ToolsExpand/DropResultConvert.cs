using RngDropTool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class DropResultConvert
    {
        public static SettlementDatas_Net IRngItemConvert(RngResult result)
        {
            var data = new SettlementDatas_Net();
            Debug.Log(result.Items.Count);
            foreach (var item in result.Items)
            {
                var now = item.GetObj();
                if (now == null) { Debug.Log("is null"); continue; }
                switch (now)
                {
                    case ItemData a:
                        Debug.Log($"{a.AssetName} is {a.GetType()} Aomut {item.Amount}");
                        data.AddItem(a, item.Amount);
                        break;
                    case CharacterData a:
                        Debug.Log($"{a.AssetName} is {a.GetType()} Aomut {item.Amount}");
                        data.AddCharacter(a);
                        break;

                    default:
                        Debug.LogError($"{item.GetObj().Name} is unknow type");
                        break;
                }
            }
            return data;
        }


    }
}
