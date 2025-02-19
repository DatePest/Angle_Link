using Codice.CM.Common;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Script.Core.Fx.AnimFX_Tool;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Script.Core.Fx
{
    [CreateAssetMenu(fileName = "FXObj", menuName = "AL/Fx/FXObj")]
    public class FXObj : ScriptableObject
    {
       
        public StartMoveType moveType;
        public bool ReadyAnime;
        public FXData StartFX;
        public FXData MakeFX;
        public FXData EndFX;
        public enum StartMoveType
        {
            No,
            CenterOfMap,
            Target
        }
    }
}
