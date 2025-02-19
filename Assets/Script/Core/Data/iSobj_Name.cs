using RngDropTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Core.Data
{
    public abstract class iSobj_Name : ScriptableObject 
    {
        [HideInInspector] public string AssetName;
        private void OnValidate()
        {
            if (this.name != AssetName)
            {
                AssetName = this.name;
            }
        }
    }
}
