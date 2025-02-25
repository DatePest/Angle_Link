using UnityEngine;

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
