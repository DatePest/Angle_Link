using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.Server
{
    public class SerEventTag
    {
        public const ushort Test = 0000;

        //----------Send    ID  Tag---------
        public const ushort Returnlogin = 1000;
        public const ushort ReturnReg = 1001;
        public const ushort ReturnPlayerData = 1002;
        public const ushort ReturnMsg = 1990;
        public const ushort ReturnErrorMsg_BackLogin = 1991;

      


        public const ushort ReturnBattleEnterRequest = 2100;
        public const ushort ReturnBattleDataRequest = 2101;
        public const ushort ReturnBattleStartRequest = 2102;
        public const ushort ReturnBattlePlayerSelectRequest = 2104;
        public const ushort ReturnBattleSettlement = 2108;
        public const ushort DataExpiredRequestCombatData = 2110;
        public const ushort DataExpiredBackHome = 2112;
        //public const ushort ReturnReg = 1001;
        //public const ushort ReturnPlayerData = 1002;
        //public const ushort ReturnMsg = 1999;

        public const ushort ReturnCharacterDevelop_Lv = 3000;
        //----------Receive Msg Tag---------
        //-------------------
    }

}
