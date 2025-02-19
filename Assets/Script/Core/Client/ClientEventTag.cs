using EventSystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientEventTag
    {
        /// <summary>
        ///  Example : 
        ///     EventSystemToolExpand.Publish(ClientEventTag.SendBattleEndSettlementRequest, NetworkMsg_HandlerTag.Battle, default, msg);
        /// </summary>
        public const ushort Test = 0000;

        //----------Send    ID  Tag---------
        #region ----------Account--------- 1000~1099
        public const ushort SendLogin = 1000;
        public const ushort SendRegister = 1002;
        public const ushort SendGetUserData = 1010;
        #endregion
        #region ----------Battle---------   1100~1199
        public const ushort SendBattleEnterRequest = 1100;
        public const ushort SendOnGameStartToServerrRequest = 1104;
        public const ushort SendSelectToServerRequest = 1108;
        public const ushort SendBattleData = 1112;
        public const ushort SendBattleEndSettlementRequest = 1190;
        #endregion
        #region ----------Develop--------- 1200~1399
        public const ushort SendCharacterDevelopLv = 1200;
        #endregion
        //-------------------
    }
    public class EventReceiveReturnMgs : IEventTag   
    {
        public ushort EventId { get; set; }
    }
}
