using System;
using System.Collections.Generic;
using UnityEngine;

namespace RngDropTool
{
    // If you want to use the random system, you must let the target object inherit this interface.
    public interface IRngItem
    {
        public string Name { get; }
    }
    public enum RandomEnum
    {
        WeightedRandom,
        LayeredRandom,
        AllIndividuallyRandom
    }
    [System.Serializable]
    public class RngDrop
    {
        public RandomEnum RandomType;
        [SerializeReference]
        public IRngRandom items;

#if UNITY_EDITOR
        public RngResult TestRandom()
        {
            var data = items.GetRandomDrop();
            foreach (var item in data.Items)
            {
                var isnull = item.GetObj() is null;
                if (!isnull)
                    Debug.Log($" Item : {item.GetObj().Name}");
                else
                    Debug.Log($" Item is null");
            }
            return data;
        }
#endif
         public  RngResult GetRandomDrop() => items.GetRandomDrop();
    }


    public abstract class RngItem
    {
        public abstract int Weight { get; set; }
        // Allowed to be empty. Please customize how to handle it.
        protected IRngItem Obj { get; set; }
        public int Amount;
        // Limit the type otherwise an error will occur
#if UNITY_EDITOR
        [Editor_Tool.LimitType(typeof(IRngItem))]
#endif
        [SerializeField]
        UnityEngine.Object Data;
        public virtual IRngItem GetObj()
        {
            if (Data == null)
            {
                // Debug.LogError("Data is  null!");
                return null;
            }
            if (Obj == null)
            {
                var d = Data as IRngItem;
                if (d == null) Debug.LogError("Data is Not IRngItem!");
                Obj = d;
            }
            return Obj;
        }
    }

    [System.Serializable]
    public class RngItem_Weight : RngItem
    {
        [field:SerializeField] public override int Weight { get; set; }
    }
    [System.Serializable]
    public class RngItem_Individually : RngItem
    {
        [field: SerializeField, Range(0, 10000),Tooltip("10000 = 100% , 100 = 1% and so on")]
        public override int Weight { get; set; }
    }


    [System.Serializable]
    public class RngItemTable<T> where T : RngItem
    {
        [Tooltip("Just help with classification")]
        public string TableName;
#if UNITY_EDITOR
        [ReadOnly, Tooltip("The total weight of the list is for reference only.\r\n Not available for some algorithm types")]
#endif
        public int TotalWeight;
        public List<T> items = new();
        public void AddItem(T item)
        {
            if (item.Weight <= 0) throw new ArgumentException("Weight must be positive.");
            items.Add(item);
            TotalWeight += item.Weight;
        }

        public int CalculateWeight()
        {
            if (items.Count < 0) return 0;
            int currentWeight = 0;
            foreach (var item in items)
            {
                if (item == null) continue;
                currentWeight += item.Weight;
            }
            TotalWeight = currentWeight;
            return TotalWeight;
        }
    }
   
    [System.Serializable]
    public class RngResult : IDisposable
    {
        public List<RngItem> Items { get; private set; }
        public RngResult()
        {
            Items = new();
        }

        public void Add(RngItem item)
        {
            //if (item.GetObj() == null) return;
            Items.Add(item);
        }

        public void Dispose()
        {
            Items.Clear();
            Items = null;
        }
    }
    public interface ICalculateWeight
    {
        public int CalculateWeight();
    }

    [System.Serializable]
    public abstract class IRngRandom : ICalculateWeight
    {
        public abstract int CalculateWeight();
        public abstract RngResult GetRandomDrop();
        public abstract void RandomDrop(RngResult r);
    }
}