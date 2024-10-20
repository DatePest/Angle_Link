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
        public const ushort Test = 0000;

        //----------Send    ID  Tag---------
        #region ----------Account---------

        public const ushort SendLogin = 1000;
        public const ushort SendRegister = 1002;
        public const ushort SendGetUserData = 1010;
        #endregion
        //-------------------
    }
    #region IEventTags
    public class EventReceiveReturnMgs : IEventTag   
    {
        public ushort EventId { get; set; }
    }
    #endregion
}
