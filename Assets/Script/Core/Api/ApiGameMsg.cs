using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApi
{
    public class BattleEnterRequest : IRequest
    {
        public Team team { get; set; }
        public string GameLevelDataName { get; set; }
    }
    public class BattleRequest : IRequest
    {
        public string GameUid { get; set; }
    }
    public class BattlePlayerSelectRequest : BattleRequest
    {
        public string orders_Json { get; set; }

        public List<BattleSelectOrder> GetSelectOrders()
        {
            if (orders_Json == null) return null;
            return ApiTool.JsonToObject<List<BattleSelectOrder>>(orders_Json);
        }
    }
}
