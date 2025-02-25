using System;
namespace RngDropTool
{
    [System.Serializable]
    public class WeightedRandom  : IRngRandom 
    {
        public RngItemTable<RngItem_Weight> itemTable = new();

        public override int CalculateWeight()
        {
           return  itemTable.CalculateWeight();
        }

        public override RngResult GetRandomDrop() 
        {
            if (itemTable.items.Count == 0) throw new InvalidOperationException("No items to select.");
            int randomValue = new System.Random().Next(0, itemTable.TotalWeight);
            int currentWeight = 0;

            foreach (var item in itemTable.items)
            {
                currentWeight += item.Weight;
                if (randomValue < currentWeight)
                {
                    RngResult Res = new();
                    Res.Items.Add(item);
                    return Res;
                }
            }
            throw new Exception("Unexpected error in random selection.");
        }
        public override void RandomDrop(RngResult result)
        {
            if (itemTable.items.Count == 0) throw new InvalidOperationException("No items to select.");
            int randomValue = new System.Random().Next(0, itemTable.TotalWeight);
            int currentWeight = 0;

            foreach (var item in itemTable.items)
            {
                currentWeight += item.Weight;
                if (randomValue < currentWeight)
                {
                    result.Add(item);
                    return;
                }
            }
            throw new Exception("Unexpected error in random selection.");
        }
    }
}