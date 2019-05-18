using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libMcArgs;
using Newtonsoft.Json.Linq;
namespace HikazeLauncher
{
    class AllConfigure
    {
        public AllConfigure()
        {
            JavaPath = "";
            player = null;
            ExtraPara = "";
            MaxMem = 0;
            MinMem = 0;
        }
        public string JavaPath;
        public Playerinfo player;
        public string ExtraPara;
        public int MaxMem;
        public int MinMem;
        public JObject ConvertToJson()
        {
            JObject ReturnJson = new JObject();
            ReturnJson.Add("JavaPath", JavaPath);
            ReturnJson.Add("PlayerName", player.PlayerName);
            ReturnJson.Add("PlayerUUID", player.uuid);
            ReturnJson.Add("ExtraPara", ExtraPara);
            ReturnJson.Add("MaxMem", MaxMem);
            ReturnJson.Add("MinMem", MinMem);
            return ReturnJson;
        }
    }
}