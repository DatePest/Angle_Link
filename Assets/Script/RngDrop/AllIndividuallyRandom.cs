using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RngDropTool
{
    [System.Serializable]
    public class AllIndividuallyRandom : IRngRandom
    {
        public RngItemTable<RngItem_Individually> itemTable = new();
        public override int CalculateWeight()
        {
            return 0;
        }

        public override RngResult GetRandomDrop()
        {
            if (itemTable.items.Count == 0) throw new InvalidOperationException("No items to select.");
          
            RngResult result = new();
            foreach (var item in itemTable.items)
            {
                int randomValue = new System.Random().Next(0, 10000);
                if (randomValue < item.Weight)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public override void RandomDrop(RngResult r)
        {
            if (itemTable.items.Count == 0) throw new InvalidOperationException("No items to select.");

            foreach (var item in itemTable.items)
            {
                int randomValue = new System.Random().Next(0, 10000);
                if (randomValue < item.Weight)
                {
                    r.Add(item);
                }
            }
        }
    }
}
