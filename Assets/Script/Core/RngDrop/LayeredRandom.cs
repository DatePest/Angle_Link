using System;
using System.Collections.Generic;
using UnityEngine;

namespace RngDropTool
{
    [System.Serializable]
    public class LayeredRandom : IRngRandom
    {
        [SerializeReference]
        public List<IRngRandom> ItemTables = new();

        public override int CalculateWeight()
        {
            int i = 0;
            foreach (var table in ItemTables)
            {
                if (table == null || table == null)
                    continue;
                i += table.CalculateWeight();
            }
            return i;
        }

        public void AddLayer(IRngRandom dropTable)
        {
            if (dropTable == null) throw new ArgumentException("Layer weight must be positive.");
            ItemTables.Add(dropTable);
            dropTable.CalculateWeight();
        }

        public override RngResult GetRandomDrop()
        {
            if (ItemTables.Count == 0) throw new InvalidOperationException("No layers to select.");
            RngResult results = new();
            foreach (var Table in ItemTables)
            {
                Table.RandomDrop(results);
            }
            return results;
        }

        public override void RandomDrop(RngResult r)
        {
            if (ItemTables.Count == 0) throw new InvalidOperationException("No layers to select.");
            foreach (var Table in ItemTables)
            {
                Table.RandomDrop(r);
            }
        }
    }
    
   
  
}

