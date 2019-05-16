using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libMcArgs
{
    /// <summary>
    /// 此类存储玩家信息
    /// </summary>
    public class Playerinfo
    {
        public string PlayerName;
        public string uuid;
        public void RegenerateUUID()
        {
            uuid = Guid.NewGuid().ToString().Replace("-", "");
        }
        public void SetPlayerName(string NewName)
        {
            PlayerName = NewName;
        }
    }
}
